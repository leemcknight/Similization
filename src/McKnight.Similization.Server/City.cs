using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Xml;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents a city in the game.
	/// </summary>
	public class City : CityBase, ITradable
	{  
        private event EventHandler<CitizensKilledEventArgs> citizensKilled;
        private event EventHandler<ImprovementBuiltEventArgs> improvementBuilt;
        private event EventHandler<ImprovementDestroyedEventArgs> improvementDestroyed;
        private event EventHandler<CityStatusEventArgs> cityStatusChanged;
        private event EventHandler<CannotGrowEventArgs> cannotGrow;
        private event EventHandler<PollutionEventArgs> pollutionCreated;
        private event EventHandler<CapturedEventArgs> captured;
        private event EventHandler culturalInfluenceExpanded;
        private event EventHandler turnStarted;
        private event EventHandler starved;       
        private event EventHandler disorderStarted;                
        private event EventHandler growthNeededForUnit;
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="City"/> class.
        /// </summary>
        public City()
        {                                    
            
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="City"/> class.
		/// </summary>
		/// <param name="coordinates">The <see cref="GridCell"/> the City should be located on.</param>
		/// <param name="parentCountry">The <see cref="Country"/> that is building the city.</param>
		public City(Point coordinates, Country parentCountry) : base(coordinates)
		{
			if(parentCountry == null)
				throw new ArgumentNullException("parentCountry");

            this.ParentCountry = parentCountry;
		    this.YearFounded = GameRoot.Instance.Year;
			InitializeCityRadius(1);
			RefreshUsedCells();
			UpdateBuildableItems();			
			this.NextImprovement = this.BuildableItems[0];
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="City"/> class.
        /// </summary>
        /// <remarks>
		/// This constructor is useful when the programmer needs to 
		/// convert an AICity to a regular City, perhaps when a human 
		/// player captures an AI City (AI Cities will break when underneath 
		/// human players).
        /// </remarks>
		/// <param name="coordinates"></param>
		/// <param name="parentCountry"></param>
		/// <param name="capturedCity"></param>
		public City(Point coordinates, Country parentCountry, AICity capturedCity) : this(coordinates, parentCountry)
		{
			if(capturedCity == null)
				throw new ArgumentNullException("capturedCity");

			this.Name = capturedCity.Name;
			this.YearFounded = capturedCity.YearFounded;
			this.Population = capturedCity.Population;
			this.SizeClass = capturedCity.SizeClass;
			this.Status = CityStatus.Disorder;			
			InitializeCityRadius(capturedCity.Radius);			
			foreach(Improvement imp in capturedCity.Improvements)			
				this.Improvements.Add(imp);                
		}
		
		/// <summary>
		/// Calculates the value of the <see cref="City"/> to the specified <see cref="Country"/>.
		/// </summary>
		/// <returns></returns>
		public int CalculateValueForCountry(CountryBase country)
		{
            if (country == null)
                throw new ArgumentNullException("country");
            
            int calculatedValue = 0;
            foreach (Improvement improvement in this.Improvements)
            {
                if (improvement is Wonder)
                    calculatedValue += 20;
                else
                    calculatedValue += 10;
            }

            calculatedValue += (10 * (this.Population));

            if (calculatedValue >= 100)
                return 100;
            else
                return calculatedValue;			
		}

		/// <summary>
		/// Adds the GridCell to the cities influence radius.  Once a
		/// cell is inside city radius, the city can take advantage 
		/// of food, commerce, and resources that the cell contains.
		/// </summary>
		/// <param name="coordinates"></param>
		protected override void AddCoordinatesToCityRadius(Point coordinates)
		{
            GameRoot root = GameRoot.Instance;
            GridCell cell = root.Grid.GetCell(coordinates);
			cell.Explore(this.ParentCountry);
			cell.Owner = this.ParentCountry;
            cell.RefreshBorders();
		}

		/// Changes the country that control the city.  This is useful for city captures 
		/// and also useful for cities that choose to change countries based on culture.
		private void ChangeParentCountry(Country newCountry)
		{
			if(newCountry == null)
				throw new ArgumentNullException("newCountry");

			City newCity = this;
			bool ai = (newCountry.GetType() == typeof(AICountry));
			AICity city = this as AICity;
			if(city != null && !ai)	
				newCity = new City(this.Coordinates, newCountry, city);
			else if(ai)
				newCity = new AICity(this.Coordinates, newCountry, this);

			//switch sides...
            Country parent = (Country)this.ParentCountry;
			parent.Cities.Remove(this);
			newCountry.Cities.Add(newCity);
			newCity.ParentCountry = newCountry;
		}

		#region Properties		
		
		/// <summary>
		/// Gets the amount of gold generated each turn by the city.
		/// </summary>
		public int GoldPerTurn
		{
			get
			{
                GameRoot root = GameRoot.Instance;                
				int gold = 0;
				foreach(Point coordinate in this.UsedCells)
				{
                    GridCell cell = root.Grid.GetCell(coordinate);
					gold += cell.GoldPerTurn;
				}
				return gold;
			}
		}

		/// <summary>
		/// Gets the number of shields per turn produced by the city.
		/// </summary>
		public int ShieldsPerTurn
		{
			get 
			{
                GameRoot root = GameRoot.Instance;
				int shields = 0;
				foreach(Point coord in this.UsedCells)
				{
                    GridCell cell = root.Grid.GetCell(coord);
					shields += cell.Shields;	
				}

				return shields;
			}
		}


		/// <summary>
		/// Gets the amount of food produced by the city each turn.
		/// </summary>
		public int FoodPerTurn
		{
			get
			{
                GameRoot root = GameRoot.Instance;
				int food = 0;
				foreach(Point coord in this.UsedCells)
				{
                    GridCell cell = root.Grid.GetCell(coord);
					food += cell.FoodUnits;
				}

				return food;
			}
		}

	
		/// <summary>
		/// Gets the size of the food bins for the city.
		/// </summary>
		public int BinSize
		{
			get 
			{
                GameRoot root = GameRoot.Instance;

				int binSize = 0;
				switch(this.SizeClass)
				{
					case CitySizeClass.City:
						binSize = root.Ruleset.CityBinSize;
						break;
					case CitySizeClass.Metropolis:
						binSize = root.Ruleset.MetropolisBinSize;
						break;
					case CitySizeClass.Town:
						binSize = root.Ruleset.TownBinSize;
						break;
				}
				return binSize;
			}
		}

		/// <summary>
		/// Gets the number of turns before the city will grow in population.
		/// </summary>
		/// <remarks>This property returns an <i>estimate</i> of the number of 
		/// turns before the <see cref="City"/> grows in size.  This estimate is based 
		/// on the amount of food currently in the bins, and the amount of food 
		/// currently being produced per turn.  The accuracy of this estimate is
		/// dependant on those two factors being consistent.</remarks>
		public int TurnsUntilGrowth
		{
			get 
			{ 
				int needed = this.Population;
				int saved = FoodPerTurn - this.Population;

				if(saved > needed)
				{
					int turns = (BinSize - this.AvailableFood)/saved;
					return turns;
				}
				else
				{
					return 999;
				}
			}
		}

        /// <summary>
        /// Gets the number of research units produced by the city each turn.
        /// </summary>
        public int ResearchPerTurn
        {
            get
            {
                double percentage = (double)this.ParentCountry.SciencePercentage / 100;
                double units = percentage * GoldPerTurn;
                int researchPerTurn = Convert.ToInt32(Math.Ceiling(units));
                return researchPerTurn;
            }
        }

        /// <summary>
        /// Gets the number of turns before the improvement the city is 
        /// building is finished.
        /// </summary>
        public int TurnsUntilComplete
        {
            get
            {
                if (this.NextImprovement == null)                
                    return int.MaxValue;                
                else if (ShieldsPerTurn == 0)                
                    return int.MaxValue;                
                
                double totalTurns = this.NextImprovement.Cost / ShieldsPerTurn;
                double takenTurns = this.Shields / ShieldsPerTurn;
                int outer = Convert.ToInt32(Math.Ceiling(totalTurns));
                int inner = Convert.ToInt32(Math.Ceiling(takenTurns));

                return (outer - inner);
            }
        }
		
		#endregion

		#region Public Methods

		/// <summary>
		/// Bombards and kills 1 population point of the city's population.
		/// </summary>
		/// <param name="attacker"></param>
		public void BombardPopulation(Country attacker)
		{
			if(this.Population > 1)
			{
				this.Population--;
				OnCitizensKilled(new CitizensKilledEventArgs(attacker));
			}
		}

		/// <summary>
		/// Destorys the specified improvement.
		/// </summary>
		/// <param name="destroyedImprovement"></param>
		/// <param name="attacker"></param>
		public void DestroyImprovement(Improvement destroyedImprovement, Country attacker)
		{
			this.Improvements.Remove(destroyedImprovement);
			OnImprovementDestroyed(new ImprovementDestroyedEventArgs(destroyedImprovement, attacker));
		}

		/// <summary>
		/// Gets a value indicating whether or not there is a worker unit in 
		/// the city radius.
		/// </summary>
		/// <returns><c>true</c> if there is a worker in the fields, <c>false</c> otherwise</returns>
		/// <remarks>To be considered "in the fields", a worker must be in one of the cells
		/// directly available to the city.</remarks>
		public bool HasWorkerInFields()
		{
            GameRoot root = GameRoot.Instance;
            Grid grid = root.Grid;
			Point[] fieldCoords = GetCellsInCityRadius();
			bool hasWorker = false;

			foreach(Point fieldCoord in fieldCoords)
			{
                GridCell cell = grid.GetCell(fieldCoord);
				foreach(Unit unit in cell.Units)
				{
					if(unit.CanWork)
					{
						hasWorker = true;
						break;
					}
				}

				if(hasWorker)
					break;
			}

			return hasWorker;
		}

		/// <summary>
		/// Attempts to capture the city.
		/// </summary>
		/// <param name="foreignUnit"></param>
		public void Capture(Unit foreignUnit)
		{
			if(foreignUnit == null)
				throw new ArgumentNullException("foreignUnit");
			CapturedEventArgs e;
            Country parent = (Country)this.ParentCountry;
			int goldPlundered = parent.GetPlunderedAmountForCity(this);
			e = new CapturedEventArgs(foreignUnit.ParentCountry, this.ParentCountry, goldPlundered);
			OnCaptured(e);
			ChangeParentCountry(foreignUnit.ParentCountry);
		}

		/// <summary>
		/// Upgrades the unit to the best upgradable unit.
		/// </summary>
		/// <param name="unit"></param>
		public void UpgradeUnit(Unit unit)
		{
            GameRoot root = GameRoot.Instance;
            GridCell currentCell = root.Grid.GetCell(this.Coordinates);
			if(!currentCell.Units.Contains(unit))			
				return;
						
			Unit nextUnit = unit;
			while(this.BuildableItems.Contains(nextUnit.Upgrade))			
				nextUnit = nextUnit.Upgrade;
			
			Unit newUnit = new Unit(this.Coordinates, nextUnit);
			newUnit.ParentCountry = (Country)this.ParentCountry;						
		}

		#endregion

		#region Events
		/// <summary>
		/// Occurs when an attacking country kills a portion of the
		/// city's population.
		/// </summary>
		public event EventHandler<CitizensKilledEventArgs> CitizensKilled
		{
			add
			{
				this.citizensKilled += value;
			}

			remove
			{
				this.citizensKilled -= value;
			}
		}

		/// <summary>
		/// Raises the <i>CitizensKilled</i> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnCitizensKilled(CitizensKilledEventArgs e)
		{
			if(this.citizensKilled != null)
			{
				this.citizensKilled(this, e);
			}
		}

		/// <summary>
		/// Event that fires whenever a new improvement is built by the city.
		/// </summary>
		public event EventHandler<ImprovementBuiltEventArgs> ImprovementBuilt
		{
			add
			{
				this.improvementBuilt += value;
			}

			remove
			{
				this.improvementBuilt -= value;
			}
		}

		/// <summary>
		/// Raises the <i>ImprovementBuilt</i> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnImprovementBuilt(ImprovementBuiltEventArgs e)
		{
			if(this.improvementBuilt != null)
			{
				this.improvementBuilt(this, e);
			}
		}

		/// <summary>
		/// Occurs when an <see cref="Improvement"/> within the <see cref="City"/> is destroyed.
		/// </summary>
		public event EventHandler<ImprovementDestroyedEventArgs> ImprovementDestroyed
		{
			add
			{
				this.improvementDestroyed += value;
			}

			remove
			{
				this.improvementDestroyed -= value;
			}
		}

		/// <summary>
		/// Raises the <i>ImprovementDestroyed</i> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnImprovementDestroyed(ImprovementDestroyedEventArgs e)
		{
			if(this.improvementDestroyed != null)
			{
				this.improvementDestroyed(this, e);
			}
		}

		/// <summary>
		/// Fires the <i>CityStatusChanged</i> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnCityStatusChanged(CityStatusEventArgs e)
		{
			if(this.cityStatusChanged != null)
			{
				this.cityStatusChanged(this, e);
			}
		}

		/// <summary>
		/// Event that fires whenever the status of the city changes.  
		/// </summary>
		/// <remarks>Status changes happen when the balance of happiness and
		/// unhappiness is changed.  Cities with a larger percentage of unhappy people 
		/// will tend to be in disorder, and happier citizens will result in orderly 
		/// cities (or even celebrations).</remarks>
		public event EventHandler<CityStatusEventArgs> CityStatusChanged
		{
			add
			{
				this.cityStatusChanged += value; 
			}

			remove
			{
				this.cityStatusChanged -= value; 
			}
		}

		/// <summary>
		/// Event that fires whenever the cultural influence of the
		/// city expands.
		/// </summary>
		public event EventHandler CulturalInfluenceExpanded
		{
			add
			{
				this.culturalInfluenceExpanded += value;
			}

			remove
			{
				this.culturalInfluenceExpanded -= value;
			}
		}

		/// <summary>
		/// Fires the <i>CulturalInfluenceExpanded</i> event.
		/// </summary>
		protected virtual void OnCulturalInfluenceExpanded()
		{
			if(this.culturalInfluenceExpanded != null)
				this.culturalInfluenceExpanded(this, null);			
            this.Radius++;
			this.CultureThreshold *= this.BorderFactor;
		}

		/// <summary>
		/// Event that fires whenever a turn has begun for a city.
		/// </summary>
		public event EventHandler TurnStarted
		{
			add
			{
				this.turnStarted += value;
			}

			remove
			{
				this.turnStarted -= value;
			}
		}

		/// <summary>
		/// Fires the <i>TurnStarted</i> event.
		/// </summary>
		protected virtual void OnTurnStarted()
		{
			if(this.turnStarted != null)
			{
				this.turnStarted(this, null);
			}
		}

		/// <summary>
		/// Event that fires whenever the city experiences starvation.
		/// </summary>
		public event EventHandler Starved
		{
			add
			{
				this.starved += value;
			}

			remove
			{
				this.starved -= value;
			}
		}

		/// <summary>
		/// Raises the <i>Starved</i> event.
		/// </summary>
		protected virtual void OnStarved()
		{
			if(this.starved != null)
			{
				this.starved(this, null);
			}

			this.AvailableFood = 0;
			this.Population = this.Population - 1;
		}

		/// <summary>
		/// Event that fires whenever the city is attempting to 
		/// grow past its' maximum size.  This happens when a city
		/// requires an improvement to allow growth past its' current
		/// size.
		/// </summary>
		public event EventHandler<CannotGrowEventArgs> CannotGrow
		{
			add
			{
				this.cannotGrow += value;
			}

			remove
			{
				this.cannotGrow -= value;
			}
		}

		/// <summary>
		/// Fires the <i>CannotGrow</i> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnCannotGrow(CannotGrowEventArgs e)
		{
			if(this.cannotGrow != null)
			{
				this.cannotGrow(this, e);
			}
		}

		/// <summary>
		/// Event that fires whenever the city is captured.
		/// </summary>
		public event EventHandler<CapturedEventArgs> Captured
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
		protected virtual void OnCaptured(CapturedEventArgs e)
		{
			if(this.captured != null)
			{
				this.captured(this, e);
			}
		}

		/// <summary>
		/// Event that fires whenever a city is thrown into disorder.
		/// </summary>
		public event EventHandler DisorderStarted
		{
			add
			{
				this.disorderStarted += value;
			}

			remove
			{
				this.disorderStarted -= value;
			}
		}

		/// <summary>
		/// Fires the <i>DisorderStarted</i> event.
		/// </summary>
		protected virtual void OnDisorderStarted()
		{
			if(this.disorderStarted != null)
			{
				this.disorderStarted(this, null);
			}
		}
		
		/// <summary>
		/// Event that fires whenever pollution is created near the city.
		/// </summary>
		public event EventHandler<PollutionEventArgs> PollutionCreated
		{
			add
			{
				this.pollutionCreated += value;
			}

			remove
			{
				this.pollutionCreated -= value;
			}
		}

		/// <summary>
		/// Fires the <i>PollutionCreated</i> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnPollutionCreated(PollutionEventArgs e)
		{
			if(this.pollutionCreated != null)
			{
				this.pollutionCreated(this,e);
			}
		}
		
		/// <summary>
		/// Fires the <i>PopulationChanged</i> event.
		/// </summary>
		protected override void OnPopulationChanged()
		{
            base.OnPopulationChanged();
			RefreshUsedCells();			
		}
		
		/// <summary>
		/// Event that fires when a population-reducing unit is being built, but 
		/// is stopped by the fact that the population is insufficient and the population 
		/// is not growing.
		/// </summary>
		public event EventHandler GrowthNeededForUnit
		{
			add
			{
				this.growthNeededForUnit += value;
			}

			remove
			{
				this.growthNeededForUnit -= value;
			}
		}

		/// <summary>
		/// Fires the <i>GrowthNeededForUnit</i> event.
		/// </summary>
		protected virtual void OnGrowthNeededForUnit()
		{
			if(this.growthNeededForUnit != null)
			{
				this.growthNeededForUnit(this,null);
			}
		}
		


		#endregion
		/// <summary>
		/// Gets a <see cref="WorkerActionInfo"/> object representing any work that needs 
		/// to be done around the city.
		/// </summary>
		/// <returns>A <see cref="WorkerActionInfo"/> object with information regarding the
		/// priority, location, and action to perform.</returns>
		public WorkerActionInfo RetrieveWorkItem()
		{
			Point[] cityLocations = GetCellsInCityRadius();
			WorkerActionInfo info;
			WorkerActionInfo highestPriority = new WorkerActionInfo(WorkerAction.None, null);

            GameRoot root = GameRoot.Instance;
			foreach(Point cityLocation in cityLocations)
			{
				if(cityLocation != this.Coordinates)
				{
                    GridCell cell = root.Grid.GetCell(cityLocation);
					info = cell.FindWorkerAction();
					if(info.Priority > highestPriority.Priority)					
						highestPriority = info;					
				}
			}

			return highestPriority;
		}





		/// <summary>
		/// Gets the most appropriate item for a city to build, based on the type of improvement 
		/// and the need of a city.
		/// </summary>
		/// <param name="need"></param>
		/// <param name="improvementType"></param>
		/// <returns></returns>
		public BuildableItem GetBuildableItemForNeed(AICityNeed need, System.Type improvementType)
		{
			BuildableItem item = null;
			Unit unit = null;
			foreach(BuildableItem buildable in this.BuildableItems)
			{
				if(buildable.GetType() == improvementType)
				{
					if(item == null)
					{
						item = buildable;
					}
					else
					{
						if(improvementType == typeof(Improvement))
						{
							//FIXME:  take into account city need.
							item = buildable;
							break;
						}
						else if(improvementType == typeof(Unit))
						{
							unit = buildable as Unit;

							switch(need)
							{
								case AICityNeed.Commerce:
									//TODO: Wealth
									item = unit;
									break;
								case AICityNeed.Culture:
									item = unit;
									break;
								case AICityNeed.Food:
									item = unit;
									break;
								case AICityNeed.UnitDefense:
									if(unit.Defense > ((Unit)item).Defense)
									{
										item = unit;
									}
									break;
							}							
						}
					}
				}
			}

			return item;
		}

		/// <summary>
		/// Gets the total number of defense points in the city.
		/// </summary>
		/// <returns></returns>
		/// <remarks>The total number of defense points is the sum of all defense points
		/// for each of the defensive units in the city.</remarks>
		protected internal int CalcCityDefense()
		{
            GameRoot root = GameRoot.Instance;
            GridCell cell = root.Grid.GetCell(this.Coordinates);
			int total = 0;
			foreach(Unit unit in cell.Units)			
				total += unit.Defense;			
			return total;
		}

		/// <summary>
		/// Makes the unit merge with the city.
		/// </summary>
		/// <param name="unit"></param>
		public void UnitMerge(Unit unit)
		{			
			this.Population = this.Population + 1;
		}

		/// <summary>
		/// Takes a turn for the city.  This function will update the food, shields, gold,
		/// and other information about the city.  It also handles the cases when an improvement
		/// is built, or a unit is trained. 
		/// </summary>
		public virtual void DoTurn()
		{
			//Inform listeners that we have started the turn
			OnTurnStarted();

			bool improvementBuilt = false;
			ApplyCellEffects();
			UpdateAvailableFood();
			Unit newUnit = null;
			if(this.Shields >= this.NextImprovement.Cost)
			{
                if (this.NextImprovement.GetType() == typeof(Improvement))
				{
					improvementBuilt = true;
                    this.Improvements.Add((Improvement)this.NextImprovement);
                    ApplyImprovementEffects((Improvement)this.NextImprovement);
				}
                else if (((Unit)this.NextImprovement).PopulationPoints < this.Population)
				{
					improvementBuilt = true;
					//if it's not an improvement, it's a unit
                    newUnit = UnitFactory.CreateUnit(this, (Unit)this.NextImprovement);
					newUnit.ParentCountry = (Country)this.ParentCountry;
					this.ParentCountry.Units.Add(newUnit);

					if(newUnit.PopulationPoints > 0)
					{
						this.Population -= newUnit.PopulationPoints;
						RefreshUsedCells();
					}
				}
				else if(this.TurnsUntilGrowth == 999)
				{
					OnGrowthNeededForUnit();
				}
				
				if(improvementBuilt)
					this.Shields = 0;
			}

			if(this.AvailableFood < 0)
				OnStarved();
			
			if(improvementBuilt)
			{
				ImprovementBuiltEventArgs e;
				BuildableItem recommendedImprovement;

				//get the improvement to recommend the user build next
				recommendedImprovement = GetNextImprovement();

				//if the new improvement is a unit, we don't want to 
				//send nextImprovement to the event handler, since
				//it doesn't represent the actual unit that will be 
				//added.  It actually just represents a base for the
				//unit being added.
				if(newUnit == null)
				{
					e = new ImprovementBuiltEventArgs(this.NextImprovement, 
						this, recommendedImprovement);
				}
				else
				{
					e = new ImprovementBuiltEventArgs(newUnit, 
						this, recommendedImprovement);
				}

				//By default, the next improvement is the recommended
				//improvement, unless the user changes it.
				this.NextImprovement = recommendedImprovement;

				//fire the event
				OnImprovementBuilt(e);
			}

			IncrementCulturePtsForTurn();
			UpdatePollution();
			UpdateBuildableItems();
			CheckHappiness();
		}

		private void CheckHappiness()
		{
			if((this.Status != CityStatus.LoveTheMayor) && SatisfiesWeLoveTheKing())
			{
				CityStatusEventArgs e = new CityStatusEventArgs(CityStatus.LoveTheMayor, this.Status);
				this.Status = CityStatus.LoveTheMayor;
				OnCityStatusChanged(e);				
			}
			else if(this.SadPeople > this.HappyPeople)
			{
				if((this.Status == CityStatus.Normal) || (this.Status == CityStatus.LoveTheMayor))
				{
					CityStatusEventArgs e = new CityStatusEventArgs(CityStatus.Disorder, this.Status);	
					this.Status = CityStatus.Disorder;
					OnCityStatusChanged(e);
				}
			}
			else if(this.HappyPeople > this.SadPeople)
			{
				if(this.Status == CityStatus.Disorder)
				{
					CityStatusEventArgs e = new CityStatusEventArgs(CityStatus.Normal, this.Status);
					this.Status = CityStatus.Normal;
					OnCityStatusChanged(e);
				}
			}
		}

		/// <summary>
		/// Sabotages the production of whatever is currently being produced.
		/// </summary>
		internal void SabotageProduction()
		{
			this.Shields = 0;
		}
	
		private bool SatisfiesWeLoveTheKing()
		{
			if(this.SadPeople > 0 || this.Population < 6) //FIXME: magic number			
				return false;			
			return (this.HappyPeople > this.ContentPeople);
		}

		/// <summary>
		/// Gets a value indicating whether or not the city is currently losing food
		/// each turn.
		/// </summary>
		/// <returns></returns>
		public bool IsLosingFood()
		{
			int available = 0;
            GameRoot root = GameRoot.Instance;
			foreach(Point coordinate in this.UsedCells)
			{
                GridCell cell = root.Grid.GetCell(coordinate);
				available += cell.FoodUnits;
			}

			return available < this.Population; 
		}

		/// <summary>
		/// Updates the amount of food in the city food bins.  This
		/// takes into account how much food the city currently has,
		/// how much will be produced during the turn, and how much
		/// will be eaten during the turn.
		/// </summary>
		protected virtual void UpdateAvailableFood()
		{
            GameRoot root = GameRoot.Instance;
            int foodPerCitizen = root.Ruleset.FoodPerCitizen;
			//update the amount of food available;
			this.AvailableFood = this.AvailableFood - (this.Population * foodPerCitizen);

			if (PopulationGrowthOnTurn())
			{
				//enough food for population growth.
				this.Population = this.Population + 1;
				this.SadPeople++;
				this.AvailableFood = 0;
			}
		}

		/// <summary>
		/// Applys positive and negative effects that the surrounding
		/// cells will have.  This includes shields, gold, and food.
		/// </summary>
		protected virtual void ApplyCellEffects()
		{
			this.AvailableFood += FoodPerTurn;
			this.Shields += ShieldsPerTurn;
			this.ParentCountry.Gold += GoldPerTurn;	
		}

		/// <summary>
		/// Sells the improvement located in the city.  The city will
		/// get a certain percentage of money spent building the improvement, 
		/// however, any positive (or negative) effects the improvement had
		/// on the city will be removed.
		/// </summary>
		/// <param name="improvement"></param>
		public void SellImprovement(Improvement improvement)
		{
			this.Improvements.Remove(improvement);
			RemoveImprovementEffects(improvement);
			UpdateBuildableItems();
		}

		/// <summary>
		/// Saves the city information.
		/// </summary>
		/// <param name="writer"></param>
		public void Save(XmlWriter writer)
		{
			if(writer == null)
				throw new ArgumentNullException("writer");

			writer.WriteStartElement("City");
			writer.WriteElementString("Name", this.Name);
			writer.WriteElementString("Population", this.Population.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Food", this.AvailableFood.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Shields", this.Shields.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Pollution", this.Pollution.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("YearFounded", this.YearFounded.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("IsCapital", this.IsCapitol.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Radius", this.Radius.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("HappyPeople", this.HappyPeople.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("ContentPeople", this.ContentPeople.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("SadPeople", this.SadPeople.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Taxmen", this.TaxCollectors.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Scientists", this.Scientists.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Entertainers", this.Entertainers.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Status", Enum.GetName(typeof(CityStatus), Array.IndexOf(Enum.GetValues(typeof(CityStatus)),this.Status)));
			if(this.NextImprovement != null)
				writer.WriteElementString("NextImprovement", this.NextImprovement.Name);
			writer.WriteStartElement("Location");
			writer.WriteElementString("X", this.Coordinates.X.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Y", this.Coordinates.Y.ToString(CultureInfo.InvariantCulture));
			writer.WriteEndElement();
			writer.WriteStartElement("Improvements");
			foreach(Improvement improvement in this.Improvements)
			{
				writer.WriteElementString("Name", improvement.Name);
			}
			writer.WriteEndElement();
			writer.WriteStartElement("UsedCells");
			foreach(Point coordinates in this.UsedCells)
			{
				writer.WriteStartElement("UsedCell");
				writer.WriteStartElement("Location");
				writer.WriteElementString("X", coordinates.X.ToString(CultureInfo.InvariantCulture));
				writer.WriteElementString("Y", coordinates.Y.ToString(CultureInfo.InvariantCulture));
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		/// <summary>
		/// Loads the city information.
		/// </summary>
		/// <param name="reader"></param>
		public void Load(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			string last = "";
			GameRoot root = GameRoot.Instance;
			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "City")
					break;

				if(reader.NodeType == XmlNodeType.Element)
				{
					last = reader.Name;
					switch(last)
					{
						case "Location":
							LoadLocation(reader);
							break;
						case "Improvements":
							LoadImprovements(reader);
							break;
						case "UsedCells":
							LoadUsedCells(reader);
							break;
					}
				}
				else if(reader.NodeType == XmlNodeType.Text)
				{
					switch(last)
					{
						case "Name":
							this.Name = reader.Value;
							break;
						case "Population":
							this.Population = XmlConvert.ToInt32(reader.Value);
							break;
						case "Food":
							this.AvailableFood = XmlConvert.ToInt32(reader.Value);
							break;
						case "Shields":
							this.Shields = XmlConvert.ToInt32(reader.Value);
							break;
						case "Pollution":
							this.Pollution = XmlConvert.ToInt32(reader.Value);
							break;
						case "YearFounded":
							this.YearFounded = XmlConvert.ToInt32(reader.Value);
							break;
						case "IsCapital":
							this.IsCapitol = Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture);
							break;
						case "Radius":
							this.Radius = XmlConvert.ToInt32(reader.Value);
							break;
						case "HappyPeople":
							this.HappyPeople = XmlConvert.ToInt32(reader.Value);
							break;
						case "ContentPeople":
							this.ContentPeople = XmlConvert.ToInt32(reader.Value);
							break;
						case "SadPeople":
							this.SadPeople = XmlConvert.ToInt32(reader.Value);
							break;
						case "TaxMen":
							this.TaxCollectors = XmlConvert.ToInt32(reader.Value);
							break;
						case "Scientists":
							this.Scientists = XmlConvert.ToInt32(reader.Value);
							break;
						case "Entertainers":
							this.Entertainers = XmlConvert.ToInt32(reader.Value);
							break;
						case "Status":
							this.Status = (CityStatus)Enum.Parse(typeof(CityStatus), reader.Value, true);
							break;
						case "NextImprovement":
							this.NextImprovement = root.Ruleset.Improvements[reader.Value];
							break;
					}
				}
			}
		}

		private void LoadLocation(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

            Point location = new Point();
			string last = "";
		
			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "Location")
					break;
				if(reader.NodeType == XmlNodeType.Element)
					last = reader.Name;
				else if(reader.NodeType == XmlNodeType.Text)
				{
					switch(last)
					{
						case "X":
							location.X = XmlConvert.ToInt32(reader.Value);
							break;
						case "Y":
							location.Y = XmlConvert.ToInt32(reader.Value);
							break;
					}
				}
			}

            this.Coordinates = location;
		}

		private void LoadImprovements(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");
			GameRoot root = GameRoot.Instance;
			string last = "";
			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "Improvements")
					break;
				if(reader.NodeType == XmlNodeType.Element)
					last = reader.Name;
				else if(reader.NodeType == XmlNodeType.Text)
				{
					if(last == "Improvement")
					{
						this.Improvements.Add(root.Ruleset.Improvements[reader.Value]);
					}
				}
			}
		}

		private void LoadUsedCells(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");
			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "UsedCells")
					break;
				if(reader.NodeType == XmlNodeType.Element && reader.Name == "UsedCell")
					LoadUsedCell(reader);
			}
		}

		private void LoadUsedCell(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");
			string last = "";
            Point coords = new Point();
			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "UsedCell")
					break;
				if(reader.NodeType == XmlNodeType.Element)
					last = reader.Name;
				else if(reader.NodeType == XmlNodeType.Text)
				{
					switch(last)
					{
						case "X":
							coords.X = XmlConvert.ToInt32(reader.Value);
							break;
						case "Y":
							coords.Y = XmlConvert.ToInt32(reader.Value);
							break;
					}
				}
			}			
			this.UsedCells.Add(coords);
		}

		private void ApplyImprovementEffects(Improvement improvement)
		{
			if(improvement == null)
				throw new ArgumentNullException("improvement");
			this.ParentCountry.CulturePoints += improvement.CulturePerTurn;
		}

		private void RemoveImprovementEffects(Improvement improvement)
		{
			if(improvement == null)
				throw new ArgumentNullException("improvement");
			this.ParentCountry.CulturePoints -= improvement.CulturePerTurn;
		}

		/// <summary>
		/// Calculates the number of culture points generated during the 
		/// current turn for the city.
		/// </summary>
		/// <returns></returns>
		protected void IncrementCulturePtsForTurn()
		{
			int cultureDelta = 0;
			foreach(Improvement improvement in this.Improvements)			
				cultureDelta += improvement.CulturePerTurn;				
			this.CulturePoints += cultureDelta;
			if(this.CulturePoints >= this.CultureThreshold)			
				OnCulturalInfluenceExpanded();			
		}

        //Determines if there will be a population increase on this turn.
		private bool PopulationGrowthOnTurn()
		{
			bool grow = false;
			if (this.AvailableFood > BinSize)			
				grow = true;			
			return grow;
		}

		private void UpdateBuildableItems()
		{
			bool canHaveIt;
			GameRoot root = GameRoot.Instance;

			this.BuildableItems.Clear();
			
			foreach(UnitBase unit in root.Ruleset.Units)
			{
				canHaveIt = true;
				foreach(Resource resource in unit.NeededResources)
				{
					if(!HasAccessToResource(resource))
					{
						canHaveIt = false;
						break;
					}
				}

				if(!canHaveIt)
				{
					continue;
				}

				if(unit.PrerequisiteTechnology != null)
				{
					if(!ParentCountry.AcquiredTechnologies.Contains(unit.PrerequisiteTechnology))
					{
						canHaveIt = false;
					}
				}
				
				if(canHaveIt)
				{
					this.BuildableItems.Add(unit);
				}
			}

			foreach(Improvement improvement in root.Ruleset.Improvements)
			{
				canHaveIt = true;
				if(this.Improvements.Contains(improvement))
				{
					canHaveIt = false;
				}

				if(!canHaveIt)
				{
					continue;
				}

				foreach(Resource resource in improvement.RequiredResources)
				{
					if(!HasAccessToResource(resource))
					{
						canHaveIt = false;
						break;
					}
				}

				if(!canHaveIt)
				{
					continue;
				}

				foreach(Improvement requiredImprovement in improvement.RequiredImprovements)
				{
					if(!this.Improvements.Contains(requiredImprovement))
					{
						canHaveIt = false;
						break;
					}
				}

				if(!canHaveIt)
				{
					continue;
				}

				foreach(Technology technology in improvement.RequiredTechnologies)
				{
					if(!ParentCountry.AcquiredTechnologies.Contains(technology))
					{
						canHaveIt = false;
						break;
					}
				}

				if(canHaveIt)
				{
					this.BuildableItems.Add(improvement);
				}
			}
		}

		/// <summary>
		/// Recalculates the number of pollution points in the city and determines if (and where) 
		/// pollution will appear during this turn.
		/// </summary>
		private void UpdatePollution()
		{
			double points = 0;
			this.Pollution = 0;

			if(this.Population > 12)
			{
				points = this.Population - 12;
			}

			foreach(Improvement improvement in this.Improvements)
			{
				this.Pollution += improvement.PollutionPoints;
			}

			points += this.Pollution;
            int chance = RandomNumber.UpTo(100);
			if(chance <= points)
			{
				//we have new pollution
				GridCell cell = GetNonPollutedCell();
				cell.IsPolluted = true;
				PollutionEventArgs e = new PollutionEventArgs(cell);
				OnPollutionCreated(e);
			}
		}

		/// <summary>
		/// Gets a random non-polluted cell around the city.
		/// </summary>
		/// <returns></returns>
		private GridCell GetNonPollutedCell()
		{
            throw new NotImplementedException();
		}

		/// <summary>
		/// Returns <i>true</i> if the city has access to a certain resource.
		/// </summary>
		/// <param name="resource">The resource to check access to.</param>
		/// <returns></returns>
		public bool HasAccessToResource(Resource resource)
		{
            GameRoot root = GameRoot.Instance;
            GridCell currentCell = root.Grid.GetCell(this.Coordinates);
			return currentCell.CanGetTo(resource, currentCell);			
		}

		/// <summary>
		/// Gets the next item for the city to build.  This could be a unit
		/// or an improvement (or even a wonder).
		/// </summary>
		/// <returns></returns>
		private BuildableItem GetNextImprovement()
		{
			BuildableItem nextImprovement;

			UpdateBuildableItems();

			//TODO: return a good item.
			nextImprovement = (BuildableItem)this.BuildableItems[0];

			return nextImprovement;
		}
	}
}
