using System;
using McKnight.Similization.Core;
using System.Drawing;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Class representing an artifical intelligence unit.  These are units
	/// used by computer opponents.
	/// </summary>
	public class AIUnit : Unit
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AIUnit"/> class.
		/// </summary>
		public AIUnit() : base() 
		{
		}
		
		/// <summary>
		/// Intializes a new instance of the <see cref="AIUnit"/> class.
		/// </summary>
		/// <param name="coordinates"></param>
		/// <param name="clone"></param>
		public AIUnit(Point coordinates, UnitBase clone): base(coordinates, clone)
		{
		}
		
		
		/// <summary>
		/// Takes a turn for the AI unit.
		/// </summary>
		public override void DoTurn()
		{
			if(!this.Active)
				return;

            GameRoot root = GameRoot.Instance;
            GridCell myCell = root.Grid.GetCell(this.Coordinates);

			if(this.currentBattleCity != null)
			{
				this.mode = AIUnitMode.Attack;
				MoveTo(this.currentBattleCity.Coordinates);
			}
			else if(this.currentBattlePartner != null)
			{
				this.mode = AIUnitMode.Attack;
				MoveTo(this.currentBattlePartner.Coordinates);
			}
			else
			{
				if(this.mode == AIUnitMode.Attack)
				{

				}
				else if (this.mode == AIUnitMode.Explore)
				{
					if(this.Destination == this.Coordinates || this.Destination == Point.Empty)					
                       MoveTo(GetCoordinatesToExplore());															
				}
				else if (this.mode == AIUnitMode.Pillage)
				{
					if( this.currentBattleCountry != null)
					{                        
						Point pillaged = myCell.FindClosestPillagableCoordinates(this.ParentCountry, this.currentBattleCountry);
						if((this.Coordinates != pillaged) && (this.Destination != pillaged))						
							MoveTo(pillaged);						
						else if(this.Coordinates == pillaged)						
							Pillage();						
					}
				}
				else if (this.mode == AIUnitMode.Defend && myCell.City != null)				
					this.Fortified = true;									
			}
		}

		/// <summary>
		/// Gets the cell to explore for the AI unit.
		/// </summary>
		/// <returns></returns>
		private Point GetCoordinatesToExplore()
		{
            throw new NotImplementedException();
		}

		private Country currentBattleCountry;
		/// <summary>
		///Gets the country that the AI opponent is currently battling. 
		/// </summary>
		public Country CurrentBattleCountry
		{
			get 
			{
				return this.currentBattleCountry; 
			}

			set
			{
				Country temp = value;
				if(this.currentBattleCity != null)
				{
					if(this.currentBattleCity.ParentCountry != temp)
						this.currentBattleCity = null;
				}

				if(this.currentBattlePartner != null)
				{
					if(this.currentBattlePartner.ParentCountry != temp)
						this.currentBattlePartner = null;
				}

				this.currentBattleCountry = value;
			}
		}

		private City currentBattleCity;
		/// <summary>
		/// Gets the city that the AI unit is currently attacking.
		/// </summary>
		public City CurrentBattleCity
		{
			get
			{
				return this.currentBattleCity;
			}

			set
			{
				City temp = value;
				Country newCountry = (Country)temp.ParentCountry;
				this.CurrentBattleCountry = newCountry;
				this.currentBattleCity = value;
			}
		}

		private Unit currentBattlePartner;
		/// <summary>
		/// Gets the unit that the AI unit is currently attacking.
		/// </summary>
		public Unit CurrentBattleUnit
		{
			get 
			{
				return this.currentBattlePartner; 
			}

			set
			{
				Unit temp = value;
				Country newCountry = temp.ParentCountry;
				this.CurrentBattleCountry = newCountry;
				this.currentBattlePartner = value;
			}
		}

		private AIUnitMode mode = AIUnitMode.Explore;
		/// <summary>
		/// Gets the mode of the unit.
		/// </summary>
		public AIUnitMode Mode
		{
			get { return this.mode; }
			set { this.mode = value;}
		}
	}
}
