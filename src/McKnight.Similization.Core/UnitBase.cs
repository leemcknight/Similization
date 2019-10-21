using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;

namespace McKnight.Similization.Core
{
    /// <summary>
    /// Class representing an individual unit in the game.
    /// </summary>
    public class UnitBase : BuildableItem
    {
        private int unitId;
        private int defense;
        private int movesPerTurn;
        private int movesLeft;
        private int hitPoints;
        private int bombardmentRange;
        private int bombardmentPower;
        private int rateOfFire;
        private int offensivePower;
        private int populationPoints;
        private bool precisionBombardment;
        private bool canMergeWithCity;
        private bool canSettle;
        private bool canWork;
        private bool active;
        private bool fast;
        private bool fortified;        
        private Point coordinates;
        private UnitRank rank;
        private UnitType type;
        private Journey journey;
        private Technology prerequisiteTechnology;
        private NamedObjectCollection<Resource> neededResources;                

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitBase"/> class.
		/// </summary>
		public UnitBase() 
        {
            this.neededResources = new NamedObjectCollection<Resource>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitBase"/> class.
        /// </summary>
        /// <param name="startingCoordinates">The coordinates the <see cref="UnitBase"/> 
        /// will be placed at on the grid.</param>
        public UnitBase(Point startingCoordinates)
        {
            this.coordinates = startingCoordinates;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitBase"/> class.
        /// </summary>
        /// <param name="startingCoordinates"></param>
        /// <param name="clone"></param>
        public UnitBase(Point startingCoordinates, UnitBase clone) : this(startingCoordinates)
        {
            if (clone == null)
                throw new ArgumentNullException("clone");
            this.offensivePower = clone.OffensivePower;
            this.bombardmentRange = clone.BombardmentRange;
            this.bombardmentPower = clone.bombardmentPower;
            this.canSettle = clone.CanSettle;
            this.canWork = clone.CanWork;
            this.Cost = clone.Cost;
            this.defense = clone.Defense;
            this.hitPoints = clone.HitPoints;
            this.Name = clone.Name;
            this.rateOfFire = clone.RateOfFire;
            //this.upgrade = clone.Upgrade;
            this.canMergeWithCity = clone.CanMergeWithCity;
            this.populationPoints = clone.PopulationPoints;
            this.PrerequisiteTechnology = clone.PrerequisiteTechnology;
            this.movesPerTurn = clone.MovesPerTurn;
            this.movesLeft = this.MovesPerTurn;
        }

		/// <summary>
        /// Initializes a new instance of the <see cref="UnitBase"/> class.
		/// </summary>
		/// <param name="row"></param>
		public UnitBase(DataRow row)
		{
			if(row == null)
				throw new ArgumentNullException("row");

			this.unitId = Convert.ToInt32(row["UnitID"], CultureInfo.InvariantCulture);
			this.Name = Convert.ToString(row["Name"], CultureInfo.InvariantCulture);
			this.bombardmentRange = Convert.ToInt32(row["BombardmentRange"], CultureInfo.InvariantCulture);
			this.bombardmentPower = Convert.ToInt32(row["Bombardment"], CultureInfo.InvariantCulture);
			this.canMergeWithCity = Convert.ToBoolean(row["CanMergeWithCity"], CultureInfo.InvariantCulture);
			this.canSettle = Convert.ToBoolean(row["CanSettle"], CultureInfo.InvariantCulture);
			this.canWork = Convert.ToBoolean(row["CanWork"], CultureInfo.InvariantCulture);
			this.Cost = Convert.ToInt32(row["Cost"], CultureInfo.InvariantCulture);
			this.defense = Convert.ToInt32(row["Defense"], CultureInfo.InvariantCulture);
			this.rank = UnitRank.Conscript;
			this.type = (UnitType)Enum.Parse(typeof(UnitType), row["Type"].ToString(), true);
			this.hitPoints = this.MaxHitPoints;
			this.movesPerTurn = Convert.ToInt32(row["Movement"], CultureInfo.InvariantCulture);
			this.movesLeft = this.movesPerTurn;
			this.offensivePower = Convert.ToInt32(row["Attack"], CultureInfo.InvariantCulture);
			this.populationPoints = Convert.ToInt32(row["PopulationPoints"], CultureInfo.InvariantCulture);
			this.rateOfFire = Convert.ToInt32(row["RateOfFire"], CultureInfo.InvariantCulture);
			this.neededResources = new NamedObjectCollection<Resource>();
		}

        internal int UnitId
        {
            get { return this.unitId; }
        }

        /// <summary>
        /// The <see cref="Journey"/> the <see cref="Unit"/> is currently on.
        /// </summary>
        protected Journey Journey
        {
            get { return this.journey; }
            set { this.journey = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the <see cref="UnitBase"/> is active.
        /// Active units are allowed to move when it is their turn.
        /// </summary>
        public bool Active
        {
            get { return this.active; }
            set { this.active = value; }
        }

        /// <summary>
        /// Determines the range of attack for bombardment-capable units.
        /// </summary>
        public int BombardmentRange
        {
            get { return this.bombardmentRange; }
            set { this.bombardmentRange = value; }
        }

        /// <summary>
        /// Determines the amount of bombardment power the <see cref="UnitBase"/> has.
        /// </summary>
        public int BombardmentPower
        {
            get { return this.bombardmentPower; }
            set { this.bombardmentPower = value; }
        }

        /// <summary>
        /// Determines whether or not this <see cref="UnitBase"/> has precision strike capability
        /// when performing bombardments.
        /// </summary>
        /// <remarks>Precision-strikes allow bombarding units to choose a specific 
        /// city improvement to attack when bombarding a city.</remarks>
        public bool PrecisionBombardment
        {
            get { return this.precisionBombardment; }
            set { this.precisionBombardment = value; }
        }

        /// <summary>
        /// Determines whether the <see cref="UnitBase"/> is capable of bombarding targets.
        /// </summary>
        public bool CanBombard
        {
            get { return this.bombardmentPower > 0; }
        }

        /// <summary>
        /// Determines whether the <see cref="UnitBase"/> is capable of merging with a host <see cref="City"/>.
        /// </summary>
        public bool CanMergeWithCity
        {
            get { return this.canMergeWithCity; }
            set { this.canMergeWithCity = value; }
        }

        /// <summary>
        /// Determines whether the <see cref="UnitBase"/> can settle new cities.
        /// </summary>
        public bool CanSettle
        {
            get { return this.canSettle; }
            set { this.canSettle = value; }
        }

        /// <summary>
        /// Determines whether the <see cref="UnitBase"/> can perform work in and 
        /// around a <see cref="City"/>.
        /// </summary>
        public bool CanWork
        {
            get { return this.canWork; }
            set { this.canWork = value; }
        }

        /// <summary>
        /// Determines the number of defensive points the <see cref="UnitBase"/> has.
        /// </summary>
        public int Defense
        {
            get { return this.defense; }
            set { this.defense = value; }
        }

        /// <summary>
        /// Determines whether the <see cref="UnitBase"/> is considered a fast unit.
        /// </summary>
        /// <remarks>
        /// Fast units have the ability to retreat fromcombat when their health 
        /// gets to 1 hit point.
        /// </remarks>
        public bool Fast
        {
            get { return this.fast; }
            set { this.fast = value; }
        }

        /// <summary>
        /// Gets a value indicating whether or not the <see cref="UnitBase"/> is fortified.
        /// </summary>
        /// <remarks>Fortified units have stronger defensive capabilities, but are limited 
        /// in movement.  Units cannot move while they are fortified.</remarks>
        public bool Fortified
        {
            get { return this.fortified; }
            set
            {
                this.fortified = value;
                if (this.fortified)
                {
                    this.active = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the destination of the <see cref="UnitBase"/>.
        /// </summary>
        public Point Destination
        {
            get 
            {
                if (this.journey == null)
                    return Point.Empty;
                return this.journey.Destination; 
            }            
        }

        /// <summary>
        /// Creates a <see cref="Journey"/> to the specified <i>destination</i>.
        /// </summary>
        /// <param name="destination"></param>
        protected Journey CreateJourney(Point destination) 
        {
            IJourneyCalculator calculator = CreateJourneyCalculator();
            return calculator.CreateJourney(this, destination);
        }

        /// <summary>
        /// Creates and returns an instance implementation of the <see cref="IJourneyCalculator"/> interface.
        /// </summary>
        /// <returns></returns>
        protected virtual IJourneyCalculator CreateJourneyCalculator()
        {
            return null;
        }

        /// <summary>
        /// Gets the current number of hit points the <see cref="UnitBase"/> has.
        /// </summary>
        public int HitPoints
        {
            get { return this.hitPoints; }
            set { this.hitPoints = value; }
        }

        /// <summary>
        /// Gets a <see cref="Point"/> representing the location of the <see cref="UnitBase"/>.
        /// </summary>
        public virtual Point Coordinates
        {
            get { return this.coordinates; }
            set { this.coordinates = value; }
        }

        /// <summary>
        /// Gets the maximum hit points available to the <see cref="UnitBase"/>.  This is the
        /// number of hit points the unit would have if it was at full strength.
        /// </summary>
        public int MaxHitPoints
        {
            get
            {
                int hitPoints = 2;
                switch (this.rank)
                {
                    case UnitRank.Conscript:
                        hitPoints = 2;
                        break;
                    case UnitRank.Regular:
                        hitPoints = 3;
                        break;
                    case UnitRank.Veteran:
                        hitPoints = 4;
                        break;
                    case UnitRank.Elite:
                        hitPoints = 5;
                        break;

                }
                return hitPoints;
            }
        }

        /// <summary>
        /// Gets or sets the number of moves the <see cref="UnitBase"/> has left on the current turn.
        /// </summary>
        public virtual int MovesLeft
        {
            get { return this.movesLeft; }
            set 
            {
                if (value < 0)
                    this.movesLeft = 0;
                else
                    this.movesLeft = value; 
            }
        }

        /// <summary>
        /// Determines the number of moves per turn the <see cref="UnitBase"/> can take.
        /// </summary>
        /// <remarks>This number represents the number of moves the <see cref="UnitBase"/> 
        /// can take on normal <see cref="Terrain"/>.  Some <see cref="Terrain"/> 
        /// have additional movement costs associated with them.</remarks>
        public int MovesPerTurn
        {
            get { return this.movesPerTurn; }
            set { this.movesPerTurn = value; }
        }

        /// <summary>
        /// Determines the number of population points a city will 
        /// be reduced by when this unit is produced.
        /// </summary>
        public int PopulationPoints
        {
            get { return this.populationPoints; }
            set { this.populationPoints = value; }
        }

        /// <summary>
        /// Determines the amount of offensive power the <see cref="UnitBase"/> has.
        /// </summary>
        public int OffensivePower
        {
            get { return this.offensivePower; }
            set { this.offensivePower = value; }
        }

        /// <summary>
        /// Determines the <see cref="Technology"/> that is required before this 
        /// unit can be produced.
        /// </summary>
        public Technology PrerequisiteTechnology
        {
            get { return this.prerequisiteTechnology; }
            set { this.prerequisiteTechnology = value; }
        }

        /// <summary>
        /// The rate at which the <see cref="UnitBase"/> can bombard targets.
        /// </summary>
        public int RateOfFire
        {
            get { return this.rateOfFire; }
            set { this.rateOfFire = value; }
        }

        /// <summary>
        /// Gets or sets the rank of the <see cref="UnitBase"/>.
        /// </summary>
        /// <remarks>Units with higher ranks are stronger, faster, 
        /// and able to heal quicker.</remarks>
        public UnitRank Rank
        {
            get { return this.rank; }
            set { this.rank = value; }
        }

        /// <summary>
        /// Gets or sets the type of the <see cref="UnitBase"/>.
        /// </summary>
        /// <remarks>For more information on unit types, see the <see cref="UnitType"/> enumeration.</remarks>
        public UnitType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <summary>
        /// The resources required in order to build this unit.
        /// </summary>
        public NamedObjectCollection<Resource> NeededResources
        {
            get { return this.neededResources; }
        }
    }
}
