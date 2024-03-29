using System;
using System.Data;
using System.Globalization;

namespace McKnight.Similization.Core
{
	/// <summary>
	/// A Terrain for a grid cell.
	/// </summary>
	public class Terrain : NamedObject
	{
        private int commerce;
        private int defense;
        private int food;
        private int shields;
        private int irrigationBonus;
        private int miningBonus;
        private int roadBonus;
        private int movementCost;
        private int lowestElevation;
        private int highestElevation;
        private int lowestTemperature;
        private int highestTemperature;
        private int lowestRainfall;
        private int maximumRainfall;        
        private int visibility;
        private bool dry;
        private bool mustBorderRiver;

		/// <summary>
		/// Initializes a new instance of the <see cref="Terrain"/> class.
		/// </summary>
		public Terrain() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="Terrain"/> class.
		/// </summary>
		/// <param name="row"></param>
		public Terrain(DataRow row)
		{
			if(row == null)
				throw new ArgumentNullException("row");

			this.commerce = Convert.ToInt32(row["Commerce"], CultureInfo.InvariantCulture);
			this.defense = Convert.ToInt32(row["Defense"], CultureInfo.InvariantCulture);
			this.dry = Convert.ToBoolean(row["IsDry"], CultureInfo.InvariantCulture);
			this.food = Convert.ToInt32(row["Food"], CultureInfo.InvariantCulture);
			this.irrigationBonus = Convert.ToInt32(row["IrrigationBonus"], CultureInfo.InvariantCulture);
			this.miningBonus = Convert.ToInt32(row["MiningBonus"], CultureInfo.InvariantCulture);
			this.movementCost = Convert.ToInt32(row["MoveCost"], CultureInfo.InvariantCulture);
			this.Name = Convert.ToString(row["Name"], CultureInfo.InvariantCulture);
			this.roadBonus = Convert.ToInt32(row["RoadBonus"], CultureInfo.InvariantCulture);
			this.shields = Convert.ToInt32(row["Shields"], CultureInfo.InvariantCulture);
			this.lowestTemperature = Convert.ToInt32(row["MinimumTemperature"], CultureInfo.InvariantCulture);
			this.highestTemperature = Convert.ToInt32(row["MaximumTemperature"], CultureInfo.InvariantCulture);
			this.lowestElevation = Convert.ToInt32(row["MinimumElevation"], CultureInfo.InvariantCulture);
			this.highestElevation = Convert.ToInt32(row["MaximumElevation"], CultureInfo.InvariantCulture);
			if(row["MinimumRainfall"] != DBNull.Value)
				this.lowestRainfall = Convert.ToInt32(row["MinimumRainfall"], CultureInfo.InvariantCulture);
			if(row["MaximumRainfall"] != DBNull.Value)
				this.maximumRainfall = Convert.ToInt32(row["MaximumRainfall"], CultureInfo.InvariantCulture);
			if(row["MustBorderRiver"] != DBNull.Value)
				this.mustBorderRiver = Convert.ToBoolean(row["MustBorderRiver"], CultureInfo.InvariantCulture);
		}		
		
		/// <summary>
		/// Determines the amount of commerce generated by the terrain each turn.
		/// </summary>
		public int Commerce
		{
			get { return this.commerce; }
			set { this.commerce = value; }
		}
		
		/// <summary>
		/// Determines the number of defense points the terrain offers defending 
		/// units in this terrain.
		/// </summary>
		public int Defense
		{
			get { return this.defense; }
			set { this.defense = value; }
		}
		
		/// <summary>
		/// Determines whether the terrain is considered dry.
		/// </summary>
		public bool Dry
		{
			get { return this.dry; }
			set { this.dry = value; }
		}
		
		/// <summary>
		/// Determines the amount of food generated by this terrain.
		/// </summary>
		public int Food
		{
			get { return this.food; }
			set { this.food = value; }
		}
		
		/// <summary>
		/// Determines the additional amount of food generated by 
		/// this terrain if it is irrigated.
		/// </summary>
		public int IrrigationBonus
		{
			get { return this.irrigationBonus; }
			set { this.irrigationBonus = value; }
		}
		
		/// <summary>
		/// Determines the additional amount of commerce generated by 
		/// the terrain each turn when the cell is mined.
		/// </summary>
		public int MiningBonus
		{
			get { return this.miningBonus; }
			set { this.miningBonus = value; }
		}
		
		/// <summary>
		/// The number of moves it takes for a unit to cross this terrain.
		/// </summary>
		public int MovementCost
		{
			get { return this.movementCost; }
			set { this.movementCost = value; }
		}
		
		/// <summary>
		/// Determines the additional amount of commerce generated by 
		/// the terrain each turn when the cell has a road on it.
		/// </summary>
		public int RoadBonus
		{
			get { return this.roadBonus; }
			set { this.roadBonus = value; }
		}
		
		/// <summary>
		/// The number of shields generated by this terrain each turn.
		/// </summary>
		public int Shields
		{
			get { return this.shields; }
			set { this.shields = value; }
		}
		
		/// <summary>
		/// The lowest elevation this terrain can exist at.
		/// </summary>
		public int LowestElevation
		{
			get { return this.lowestElevation; }
		}
		
		/// <summary>
		/// The highest elevation this terrain can exist at.
		/// </summary>
		public int HighestElevation
		{
			get { return this.highestElevation; }
		}
		
		/// <summary>
		/// The lowest temperature this terrain can exist at.
		/// </summary>
		public int LowestTemperature
		{
			get { return this.lowestTemperature; }
		}
		
		/// <summary>
		/// The highest temperature this terrain can exist at.
		/// </summary>
		public int HighestTemperature
		{
			get { return this.highestTemperature; }
		}

		/// <summary>
		/// The minimum amount of rainfall this terrain requires.
		/// </summary>
		public int LowestRainfall
		{
			get { return this.lowestRainfall; }
		}
		
		/// <summary>
		/// The maximum amount of rainfall this terrain can support.
		/// </summary>
		public int MaximumRainfall
		{
			get { return this.maximumRainfall; }
		}
		
		/// <summary>
		/// Determines whether this particular terrain can only exist next to a river 
		/// square.
		/// </summary>
		public bool MustBorderRiver
		{
			get { return this.mustBorderRiver; }
		}
		
		/// <summary>
		/// The distance that units can see while on this terrain.
		/// </summary>
		public int Visibility
		{
			get { return this.visibility; }
			set { this.visibility = value; }
		}

	}
}