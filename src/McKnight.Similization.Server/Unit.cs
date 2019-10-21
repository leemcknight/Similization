using System;
using System.Globalization;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Xml;
using System.Drawing;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Base class for all units in Similization.
	/// </summary>
	public class Unit : UnitBase, IOwnable
	{			        
        private Unit upgrade;      
        private Country country;
        private Country originalParentCountry;                
        private event EventHandler<UnitCapturedEventArgs> captured;
        private event EventHandler<CombatEventArgs> combatStarted;
        private event EventHandler<VillageEventArgs> villageEncountered;
        private event EventHandler hitPointLost;
        private event EventHandler killed;
        private event EventHandler moved;
        private event EventHandler turnFinished;

        /// <summary>
        /// Initializes a new instance of the <see cref="Unit"/> class.
        /// </summary>
        public Unit()
            : base()
        {
        }
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Unit"/> class.
		/// </summary>
		/// <param name="coordinates"></param>
		public Unit(Point coordinates) : base(coordinates)
		{
            AddToGrid();
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Unit"/> class.
        /// </summary>
        /// <param name="unitClone"></param>
        public Unit(Unit unitClone) : this(Point.Empty, unitClone)
        {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="Unit"/> class.
		/// </summary>
		/// <param name="coordinates"></param>
		/// <param name="clone"></param>
		public Unit(Point coordinates, UnitBase clone) : base(coordinates, clone)
		{
            AddToGrid();
		}

        /// <summary>
        /// Adds the unit to the <see cref="Grid"/> in the game.
        /// </summary>
        private void AddToGrid()
        {
            GameRoot root = GameRoot.Instance;
            GridCell cell = root.Grid.GetCell(this.Coordinates);
            cell.Units.Add(this);
        }
										
		/// <summary>
		/// Gets or sets the parent country of the unit.
		/// </summary>
		public Country ParentCountry
		{
			get { return this.country; }
			set 
			{ 
				this.country = value; 
				if(this.originalParentCountry == null)
					this.originalParentCountry = value;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="Country"/> that was the original owner of 
		/// this <see cref="Unit"/>.
		/// </summary>
		/// <remarks>For units that have been captured, the <i>ParentCountry</i> property 
		/// will show the current owner, and this <i>OriginalParentCountry</i> property will 
		/// contain the <see cref="Country"/> that originally produced the <see cref="Unit"/>.</remarks>
		public Country OriginalParentCountry
		{
			get { return this.originalParentCountry; }
		}		
		
		/// <summary>
		/// The <see cref="Unit"/> that this unit upgrades to.
		/// </summary>
		public Unit Upgrade
		{
			get { return this.upgrade; }
			set { this.upgrade = value; }
		}		
		
		/// <summary>
		/// Bombards the given cell.
		/// </summary>
        /// <remarks>Not all bombardment attempts are successfull.  To determine the 
        /// outcome of an attempt, review the <i>BombardmentResult</i> return value.</remarks>
		/// <param name="cell"></param>
		public BombardmentResult Bombard(GridCell cell)
		{
			if(cell == null)
				throw new ArgumentNullException("cell");

			if(!this.CanBombard)
				throw new InvalidOperationException(ServerResources.UnitCannotBombard);

			bool success = IsBombardmentSuccessfull();

			if(!success)
				return BombardmentResult.Failed;
			
			if(cell.HasCity)
			{
				City city = cell.City;                    
				if(city.Improvements.Count > 0)
				{
					//which improvement was destroyed?
					int bound = city.Improvements.Count - 1;
					int idx = RandomNumber.Between(0, bound);
					Improvement destroyed = city.Improvements[idx];
					city.DestroyImprovement(destroyed, this.country);
					return BombardmentResult.SucceededDestroyingCityImprovement;
				}
				else if(cell.Units.Count > 0)
				{                    
					int bound = cell.Units.Count - 1;
					int idx = RandomNumber.Between(0, bound);
					cell.Units[idx].LoseHitPoint();
					return BombardmentResult.SucceededInjuredUnit;
				}
				else if(city.Population > 1)
				{
					city.BombardPopulation(this.country);
					return BombardmentResult.SucceededKilledCitizens;
				}
			}
			else
			{
				if(cell.IsIrrigated)
				{
                    cell.IsIrrigated = false;
					return BombardmentResult.SucceededDestroyingCellImprovement;
				}
				else if(cell.HasMine)
				{
                    cell.HasMine = false;
					return BombardmentResult.SucceededDestroyingCellImprovement;
				}
				else if(cell.HasRailroad)
				{
                    cell.HasRailroad = false;
                    cell.HasRoad = true;    
					return BombardmentResult.SucceededDestroyingCellImprovement;
				}
				else if(cell.HasRoad)
				{
                    cell.HasRoad = false;
					return BombardmentResult.SucceededDestroyingCellImprovement;
				}				
			}

			return BombardmentResult.Failed;

		}

		/// <summary>
		/// Performs a precision strike on the improvement specified.
		/// </summary>
		/// <param name="cell"></param>
		/// <param name="improvement"></param>
		public bool Bombard(GridCell cell, Improvement improvement)
		{
			if(cell == null)
				throw new ArgumentNullException("cell");

			if(improvement == null)
				throw new ArgumentNullException("improvement");

			if(!this.CanBombard)
				throw new InvalidOperationException(ServerResources.UnitCannotBombard);

			if(!this.PrecisionBombardment)
				throw new InvalidOperationException(ServerResources.UnitCannotPrecisionStrike);

			if(!cell.HasCity)
				throw new ArgumentException(ServerResources.CellDoesNotContainCity, "cell");

			if(!cell.City.Improvements.Contains(improvement))
				throw new ArgumentException(ServerResources.CityDoesNotHaveImprovement);

			bool success = IsBombardmentSuccessfull();

			if(success)			
				cell.City.Improvements.Remove(improvement);			
			return success;
		}

		internal void BombardAttackingUnit(Unit attacker)
		{
			if(attacker == null)
				throw new ArgumentNullException("attacker");

			if(IsBombardmentSuccessfull())
			{
				attacker.LoseHitPoint();
			}
		}

		/// <summary>
		/// Captures the unit for the invader.
		/// </summary>
		/// <param name="invadingUnit">The <see cref="Unit"/> that has captured this unit.</param>
		public void Capture(Unit invadingUnit)
		{
			if(invadingUnit == null)
				throw new ArgumentNullException("invadingUnit");

			this.originalParentCountry = this.ParentCountry;
			this.ParentCountry.Units.Remove(this);
			invadingUnit.ParentCountry.Units.Add(this);
			this.ParentCountry = invadingUnit.ParentCountry;
			OnCaptured(new UnitCapturedEventArgs(this, invadingUnit));
		}

		/// <summary>
		/// Takes a turn for the unit.
		/// </summary>
		public virtual void DoTurn()
		{
			this.MovesLeft = this.MovesPerTurn;
			if((this.Destination != Point.Empty) && (this.Destination != this.Coordinates))			
				MoveTo(this.Destination);			
		}

		/// <summary>
		/// Gets a value indicating the defensive power of the unit, based
		/// on location, terrain, and unit type.
		/// </summary>
		/// <returns></returns>
		public int CalculateDefensivePower()
		{
			int defensivePower = Defense;
			double scaledMultipler;
            GameRoot root = GameRoot.Instance;
            GridCell currentCell = root.Grid.GetCell(this.Coordinates);

			//start with the terrain the defender is on.
			int defenseMultiplier = currentCell.Terrain.Defense;
			
			//if the defender is in a city, the multiplier can 
			//go up
            if (currentCell.City != null)
			{
                if (currentCell.City.Population >= 13)
				{
					defenseMultiplier += 100;
				}
                else if (currentCell.City.Population >= 7)
				{
					defenseMultiplier += 50;
				}

				//FIXME: Hard-coded string
                /*
				if(this.unitLocation.City.Improvements.Contains("Walls"))
				{
					defenseMultiplier += 50;
				}
                 * */
			}

			//if the defender is in a fortress, the multipler goes up
            if (currentCell.HasFortress)			
				defenseMultiplier += 25;
			
			//if the defender is fortified, the multiplier goes up
			if (this.Fortified)			
				defenseMultiplier += 50;
			
			//get a number we can multiply the defensive power by
			//to get a new defense strength
			scaledMultipler = defenseMultiplier / 100;
			scaledMultipler += 1;

			defensivePower = (int)(defensivePower * scaledMultipler);
			return defensivePower;
		}

		/// <summary>
		/// Takes 1 hit point away from the unit.
		/// </summary>
		public void LoseHitPoint()
		{
			this.HitPoints--;
			OnHitPointLost();
			if(this.HitPoints <=0)
			{
				OnKilled();
			}
		}

		/// <summary>
		/// Merges with the <see cref="City"/> at the unit's location.
		/// </summary>
		public void MergeWithCity()
		{
            Grid grid = GameRoot.Instance.Grid;
            GridCell cell = grid.GetCell(this.Coordinates);
            City city = cell.City;
			if(city == null)			
				return;			
			city.UnitMerge(this);
		}

        /// <summary>
        /// Creates and returns the server implementation of the <see cref="IJourneyCalculator"/> class.
        /// </summary>
        /// <returns></returns>
        protected override IJourneyCalculator CreateJourneyCalculator()
        {
            return new JourneyCalculator();
        }
		
		/// <summary>
		/// Moves the unit to the desired cell.  Takes the appropriate 
		/// number of turns.
		/// </summary>
		/// <param name="destination">The coordinates on the <see cref="Grid"/> 
        /// the <see cref="Unit"/> is trying to move to.</param>
		/// <returns></returns>
		public virtual MoveResult MoveTo(Point destination)
		{					
            GameRoot root = GameRoot.Instance;
            Grid grid = root.Grid;

            //create a journey to the destination
            Journey journey = CreateJourney(destination);
            ReadOnlyCollection<Point> path = journey.CalculateExistingPath();

            if (journey == null)
                return MoveResult.UnreachableTerrain;

            GridCell currentCell = grid.GetCell(this.Coordinates);            
            GridCell nextCell = grid.GetCell(this.Journey.PeekNextPoint());
            int roadBonus = root.Ruleset.RoadMovementBonus;
            if (roadBonus == 0)
                throw new InvalidOperationException(ServerResources.RulesetHasInvalidRoadBonus);
            	
			if(this.MovesLeft == 0)
			{
				OnTurnFinished();
				return MoveResult.NoMovesLeft;
			}
										
			//fight any enemies at the destination
            MoveResult result = CombatUnits(nextCell);

			if(result == MoveResult.MoveSuccess)
			{                
				//we're still alive, so we must have taken the new cell.
                int moveCost = nextCell.Terrain.MovementCost;

                if (currentCell.HasRoad && nextCell.HasRoad)
				{
                    if (currentCell.HasRailroad && nextCell.HasRailroad)					
						moveCost = 0;					
					else					
						moveCost = moveCost/roadBonus;
				}		
		
                //we can now actually move to the new cell.               
                this.Journey = journey;
                ContinueJourney();
                
                if (this.MovesLeft >= moveCost)
                    this.MovesLeft -= moveCost;
                else
                    this.MovesLeft = 0;

				//check for villages
				if(nextCell.Village != null)
				{
					VillageGoody goody = nextCell.Village.Discover(this);
					VillageEventArgs e = new VillageEventArgs(nextCell.Village, goody);
					OnVillageEncountered(e);
					nextCell.Village = null;
				}

				// Check to see if we just invaded a foreign city.
				if(nextCell.City != null && nextCell.City.ParentCountry != ParentCountry)				
					nextCell.City.Capture(this);
				
				OnMoved();

				if(this.MovesLeft <= 0)				
					OnTurnFinished();				
			}
			
			return result;
		}

        /// <summary>
        /// Moves the <see cref="Unit"/> from the current <see cref="GridCell"/> to the next 
        /// cell in the <see cref="Journey"/>.
        /// </summary>
        protected void ContinueJourney()
        {
            if (this.Journey == null)
                return;

            GameRoot root = GameRoot.Instance;
            Point next = this.Journey.Continue();
            GridCell thisCell = root.Grid.GetCell(this.Coordinates);
            GridCell nextCell = root.Grid.GetCell(next);
            thisCell.Units.Remove(this);
            nextCell.Units.Add(this);
            this.Coordinates = next;
            root.Grid.ExploreArea(next, this.ParentCountry);
            if (this.Journey.Finished)
                this.Journey = null;            
        }

		/// <summary>
		/// Pillages the current cell.
		/// </summary>
		public void Pillage()
		{
            GameRoot root = GameRoot.Instance;
            GridCell currentCell = root.Grid.GetCell(this.Coordinates);
            if (currentCell.IsIrrigated)
                currentCell.IsIrrigated = false;
            else if (currentCell.HasRailroad)
                currentCell.HasRailroad = false;
            else if (currentCell.HasRoad)
                currentCell.HasRoad = false;
            else if (currentCell.HasMine)
                currentCell.HasMine = false;
		}

		/// <summary>
		/// Attempts to retreat.  If there's a safe cell around,
		/// the unit will retreat to the safe cell.  Otherwise,
		/// the unit stays put and <i>Retreat</i> returns false
		/// </summary>
		/// <returns><i>true</i> for a successfull retreat; <i>false</i> for an unsuccessfull retreat.</returns>
		public bool Retreat()
		{						
            Point upperLeft = new Point(this.Coordinates.X - 1, this.Coordinates.Y - 1);
            GameRoot root = GameRoot.Instance;            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Point coords = new Point(upperLeft.X + i, upperLeft.Y + j);
                    if (coords == this.Coordinates)
                        continue;
                    GridCell cell = root.Grid.GetCell(coords);
                    if (IsSafeCell(cell))
                    {
                        if (MoveTo(coords) == MoveResult.MoveSuccess)
                            return true;
                        else
                            return false;
                    }
                }
            }

            return false;						
		}

        //Determines whether the specified cell is considered safe.  Safe cells are     
        //either unoccupied or occupied only by local units.
        private bool IsSafeCell(GridCell cell)
        {
            if (cell.Units.Count > 0 && cell.Units[0].ParentCountry == this.ParentCountry)
                return true;
            else if (cell.City != null && cell.City.ParentCountry == this.ParentCountry)
                return true;
            else if (cell.Units.Count == 0)
                return true;
            return false;
        }
				
		/// <summary>
		/// Occurs when the <see cref="Unit"/> has been captured by an 
		/// enemy country.
		/// </summary>
		public event EventHandler<UnitCapturedEventArgs> Captured
		{
			add
			{
				this.captured += value;
			}

			remove
			{
				this.captured -= value;
			}
		}

		/// <summary>
		/// Fires the <i>Captured</i> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnCaptured(UnitCapturedEventArgs e)
		{
			if(this.captured != null)
			{
				this.captured(this, e);
			}
		}
		
		/// <summary>
		/// Occurs when combat is started between this unit and another unit.
		/// </summary>
		public event EventHandler<CombatEventArgs> CombatStarted
		{
			add
			{
				this.combatStarted += value; 
			}

			remove
			{
				this.combatStarted -= value;
			}
		}

		/// <summary>
		/// Fires the <i>CombatStarted</i> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnCombatStarted(CombatEventArgs e)
		{
			if(this.combatStarted != null)
			{
				this.combatStarted(this, e);
			}
		}		

		/// <summary>
		/// Event that fires whenever the unit loses a hit point
		/// in combat.
		/// </summary>
		public event EventHandler HitPointLost
		{
			add
			{
				this.hitPointLost += value;
			}

			remove
			{
				this.hitPointLost -= value;
			}
		}

		/// <summary>
		/// Fires the <i>HitPointLost</i> event.
		/// </summary>
		protected virtual void OnHitPointLost()
		{
			if(this.hitPointLost != null)
			{
				this.hitPointLost(this, null);
			}
		}
		
		/// <summary>
		/// Event that fires whenever the unit is killed in combat
		/// </summary>
		public event EventHandler Killed
		{
			add
			{
				this.killed += value; 
			}

			remove
			{
				this.killed -= value; 
			}
		}

		/// <summary>
		/// Fires the <i>Killed</i> event
		/// </summary>
		protected virtual void OnKilled()
		{
			if(this.killed != null)
			{
				this.killed(this,null);
			}
		}
		
		/// <summary>
		/// Event that fires whenever the unit moves to a new cell.
		/// </summary>
		public event EventHandler Moved
		{
			add
			{
				this.moved += value;
			}

			remove
			{
				this.moved -= value;
			}
		}

		/// <summary>
		/// Fires the <i>Moved</i> event.
		/// </summary>
		protected virtual void OnMoved()
		{
			if(this.moved != null)
			{
				this.moved(this, EventArgs.Empty);
			}
		}
		
		/// <summary>
		/// Event that fires whenever the unit has finished its' turn.
		/// </summary>
		public event EventHandler TurnFinished
		{
			add
			{
				this.turnFinished += value; 
			}

			remove
			{
				this.turnFinished -= value;
			}
		}

		/// <summary>
		/// Fires the <i>TurnFinished</i> event.
		/// </summary>
		protected virtual void OnTurnFinished()
		{
			if(this.turnFinished != null)
			{
				this.turnFinished(this, EventArgs.Empty);
			}
		}
		
		/// <summary>
		/// Occurs when the <see cref="Unit"/> enters a <see cref="Village"/>.
		/// </summary>
		public event EventHandler<VillageEventArgs> VillageEncountered
		{
			add
			{
				this.villageEncountered += value;
			}

			remove
			{
				this.villageEncountered -= value;
			}
		}

		/// <summary>
		/// Fires the <i>VillageEncountered</i> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnVillageEncountered(VillageEventArgs e)
		{
			if(this.villageEncountered != null)
			{
				this.villageEncountered(this, e);
			}
		}
		
		//Combat any units at the specified location.
		private MoveResult CombatUnits(GridCell battleLocation)
		{
			if(battleLocation == null)
				throw new ArgumentNullException("battleLocation");

			//Before actually starting a fight, there are a few 
			//preliminary checks we need to make.  First and 
			//foremost, we need to ensure there are foreign units 
			//in the cell.  Next, we need to make sure that any
			//foreign units are indeed enemies, and not allies.
			//Finally, we check to ensure that enemy has defenders
			//in the cell.  If there are enemy units with no defense, 
			//we have a strong chance of capturing some unit(s).

			if(battleLocation.Units.Count == 0)
				return MoveResult.MoveSuccess;

			if(battleLocation.Units[0].ParentCountry == this.ParentCountry)
				return MoveResult.MoveSuccess;

			if(!battleLocation.Units[0].ParentCountry.IsFoeOf(this.ParentCountry))
				return MoveResult.CellTaken;

			int defense = 0;

			foreach(Unit unit in battleLocation.Units)			
				defense += unit.Defense;
			

			if(battleLocation.Units.Count > 0 && defense == 0)
			{
				//we have just attempted to enter a cell with enemy units
				//that have no defensive power.  This is a special case, 
				//becase we can capture all of these units.

				//However, before we can capture the units, any bombard-capable
				//units get 1 free shot at us.  If we survive bombardment, 
				//we can capture them.
				foreach(Unit unit in battleLocation.Units)				
					if(unit.CanBombard)					
						unit.BombardAttackingUnit(this);					
				
				return MoveResult.MoveSuccess;
			}


			//if we made it here, we need to actually attack any foreign units 
			//in the desination cell.  
			Unit foe = battleLocation.Units[0];
							
			CombatResult cr;
			cr = Combat(foe);

			switch(cr)
			{
				case CombatResult.Capture:
					return MoveResult.MoveSuccess;
				case CombatResult.Unresolved:
					return MoveResult.UnresolvedCombat;
				case CombatResult.Killed:
					return MoveResult.Killed;
				case CombatResult.Win:
					if(battleLocation.Units.Count == 0)
						return MoveResult.MoveSuccess;
					else
						return MoveResult.UnresolvedCombat;		
			}

			throw new InvalidOperationException(ServerResources.UnknownCombatResult);
			
		}

		/// <summary>
		/// Engages the unit in combat with an enemy.
		/// </summary>
		/// <param name="defender">The enemy to engage in combat with.</param>
		/// <returns>a <see cref="CombatResult"/> enumeration representing the results of the
		/// fight.</returns>
		protected virtual CombatResult Combat(Unit defender)
		{
			if(defender == null)
				throw new ArgumentNullException("defender");

			int defendersDefensivePower;
			int chanceToWin;
			int randNum;
			bool combatOver;
			CombatResult retval = CombatResult.Unresolved;
			OnCombatStarted(new CombatEventArgs(defender));
			defendersDefensivePower = defender.CalculateDefensivePower();

			//calculate the chance to win.  (will be between 1 and 100)
			chanceToWin = CalcAttackersChanceToWin(this.OffensivePower, defendersDefensivePower);

			combatOver = false;
	
			do 
			{
				randNum = RandomNumber.Between(1,100);
				if(randNum <= chanceToWin)
				{
					//attacker wins round
					defender.LoseHitPoint();

					if(defender.HitPoints == 1 && defender.Fast && !this.Fast)
					{
						combatOver = defender.Retreat();
						if(combatOver)
						{
							retval = CombatResult.Unresolved;
						}
					}
					else if (defender.HitPoints <= 0)
					{
						combatOver = true;
						retval = CombatResult.Win;
					}
				}
				else
				{
					//defender wins round
					LoseHitPoint();

					if(this.HitPoints == 1 && this.Fast && !defender.Fast)
					{
						//attempt to retreat.
						combatOver = Retreat();
						if(combatOver)
						{
							retval = CombatResult.Unresolved;
						}
					}
					else if (this.HitPoints <= 0)
					{
						combatOver = true;
						retval = CombatResult.Killed;
					}
				}

			} while (!combatOver);

			return retval;
		}

		private static int CalcAttackersChanceToWin(int attackPower, int defensePower)
		{
			int chance;
			double exact;

			double den = Convert.ToDouble(attackPower + defensePower);			

			if(den == 0)
			{
				//two settlers duking it out.  
				return 50;
			}
		
			exact = ((double)attackPower) / ((double)(attackPower + defensePower));
			exact *= 100d;
			chance = Convert.ToInt32(exact);
			return chance;
		}

		private bool IsBombardmentSuccessfull()
		{
			int chance = CalculateChanceForBombardmentSuccess();
			int rand = RandomNumber.Between(0,100);
			bool success = (rand >= (100-chance));
			return success;
		}

		private int CalculateChanceForBombardmentSuccess()
		{
			int chance = 0;
			switch(this.Rank)
			{
				case UnitRank.Conscript:
					chance = 25;
					break;
				case UnitRank.Regular:
					chance = 35;
					break;
				case UnitRank.Veteran:
					chance = 50;
					break;
				case UnitRank.Elite:
					chance = 66;
					break;
			}

			return chance;
		}

		/// <summary>
		/// Saves the unit information.
		/// </summary>
		/// <param name="writer"></param>
		public virtual void Save(XmlWriter writer)
		{
			if(writer == null)
				throw new ArgumentNullException("writer");

			writer.WriteStartElement("Unit");
			writer.WriteElementString("Name", this.Name);
			writer.WriteElementString("Rank", Enum.GetName(typeof(UnitRank), Array.IndexOf(Enum.GetValues(typeof(UnitRank)), this.Rank) ));
			writer.WriteElementString("Active", this.Active.ToString());
			writer.WriteElementString("Fortified", this.Fortified.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("MovesLeft", this.MovesLeft.ToString(CultureInfo.InvariantCulture));
			if(this.Destination != Point.Empty)
			{
				writer.WriteStartElement("Destination");
				writer.WriteElementString("X", this.Destination.X.ToString(CultureInfo.InvariantCulture));
				writer.WriteElementString("Y", this.Destination.Y.ToString(CultureInfo.InvariantCulture));
				writer.WriteEndElement();
			}
			writer.WriteElementString("HitPoints", this.HitPoints.ToString(CultureInfo.InvariantCulture));
			writer.WriteStartElement("Coordinates");
			writer.WriteElementString("X", this.Coordinates.X.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Y", this.Coordinates.Y.ToString(CultureInfo.InvariantCulture));
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		/// <summary>
		/// Loads the unit information into the unit properties.
		/// </summary>
		/// <param name="reader"></param>
		public virtual void Load(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			GameRoot root = GameRoot.Instance;
			string last = "";
			string name = "";
			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "Unit")
					break;
				if(reader.NodeType == XmlNodeType.Element)
				{
					last = reader.Name;
					switch(last)
					{
						case "Destination":
							LoadDestination(reader);
							break;
						case "Coordinates":
							LoadCoordinates(reader);
							break;
					}
				}
				else if(reader.NodeType == XmlNodeType.Text)
				{
					switch(last)
					{
						case "Name":
							name = reader.Value;
							break;
						case "Rank":
							this.Rank = (UnitRank)Enum.Parse(typeof(UnitRank), reader.Value, true);
							break;
						case "Active":
							this.Active = Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture);
							break;
						case "Fortified":
							this.Fortified = Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture);
							break;
						case "MovesLeft":
							this.MovesLeft = XmlConvert.ToInt32(reader.Value);
							break;
						case "HitPoints":
							this.HitPoints = XmlConvert.ToInt32(reader.Value);
							break;
					}
				}
			}
			
			UnitBase clone = root.Ruleset.Units[name];
			this.OffensivePower = clone.OffensivePower;
			this.BombardmentRange = clone.BombardmentRange;
			this.CanSettle = clone.CanSettle;
			this.CanWork = clone.CanWork;
			this.Cost = clone.Cost;
			this.Defense = clone.Defense;
			this.MovesPerTurn = clone.MovesPerTurn;
			this.RateOfFire = clone.RateOfFire;
		}

		private void LoadDestination(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			string last = "";
            Point destination = new Point();
			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "Destination")
					break;
				if(reader.NodeType == XmlNodeType.Element)
					last = reader.Name;
				else if(reader.NodeType == XmlNodeType.Text)
				{
					switch(last)
					{
						case "X":
							destination.X = XmlConvert.ToInt32(reader.Value);
							break;
						case "Y":
							destination.Y = XmlConvert.ToInt32(reader.Value);
							break;
					}
				}
			}

            CreateJourney(destination);            
		}

		private void LoadCoordinates(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			string last = "";
			Point coordinates = new Point();
			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "Coordinates")
					break;
				if(reader.NodeType == XmlNodeType.Element)
					last = reader.Name;
				else if(reader.NodeType == XmlNodeType.Text)
				{
					switch(last)
					{
						case "X":
							coordinates.X = XmlConvert.ToInt32(reader.Value);
							break;
						case "Y":
							coordinates.Y = XmlConvert.ToInt32(reader.Value);
							break;
					}
				}
			}
			
            this.Coordinates = coordinates;
		}
	}
}
