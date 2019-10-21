using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace McKnight.Similization.Core
{
    /// <summary>
    /// Class representing an individual country in the game.
    /// </summary>
    public class CountryBase : NamedObject
    {
        private int advanceCostFactor = 10;
        private int commercePercentage = 80;
        private int gold;
        private int score;
        private int sciencePercent = 20;                	
        private int entertainmentPercentage;
        private int culturePoints;
        private bool notifyEndOfTurn;        
        private string leaderName;
        private Color color;
        private Civilization civilization;        
        private Government government;
        private Technology researchedTechnology;
        private Era era;        
        private NamedObjectCollection<Government> availableGovernments;
        private NamedObjectCollection<Resource> availableResources;
        private NamedObjectCollection<UnitBase> units;        
        private NamedObjectCollection<Technology> researchableTechnologies;
        private NamedObjectCollection<Technology> acquiredTechnologies;
        private event EventHandler eraChanged;
        private event EventHandler notifyEndOfTurnChanged;
        private event EventHandler researchedTechnologyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryBase"/> class.
        /// </summary>
        public CountryBase()
        {
            this.availableGovernments = new NamedObjectCollection<Government>();
            this.availableResources = new NamedObjectCollection<Resource>();
            this.units = new NamedObjectCollection<UnitBase>();
            this.acquiredTechnologies = new NamedObjectCollection<Technology>();
            this.researchableTechnologies = new NamedObjectCollection<Technology>();
        }

        /// <summary>
        /// Gets or sets the cost factor of a civilization advance. 
        /// For human players, this is always 10.  For AI players, 
        /// this can range from 6 to 20, depending on the difficulty.
        /// </summary>
        public int AdvanceCostFactor
        {
            get { return this.advanceCostFactor; }
            set { this.advanceCostFactor = value; }
        }

        /// <summary>
        /// The percentage of overall resources that are converted to gold and 
        /// added to the coffers of the <see cref="CountryBase"/>.
        /// </summary>
        public int CommercePercentage
        {
            get { return this.commercePercentage; }
            set { this.commercePercentage = value; }
        }

        /// <summary>
        /// Gets the amount of gold in the countries coffers.
        /// </summary>
        public int Gold
        {
            get { return this.gold; }
            set { this.gold = value; }
        }

        /// <summary>
        /// Gets the name of the leader of the <see cref="CountryBase"/>
        /// </summary>
        /// <remarks>This property is read-only.</remarks>
        public string LeaderName
        {
            get { return this.leaderName; }
            set { this.leaderName = value; }
        }

        /// <summary>
        /// The percentage of overall resources that are dedicated to the advance 
        /// of science.
        /// </summary>
        public int SciencePercentage
        {
            get { return this.sciencePercent; }
            set { this.sciencePercent = value; }
        }

        /// <summary>
        /// Gets the current score for the <see cref="CountryBase"/> in the game.
        /// </summary>
        public int Score
        {
            get { return this.score; }
            set { this.score = value; }
        }

        /// <summary>
        /// Gets the power factor for a country.
        /// </summary>
        public int PowerFactor
        {
            get
            {
                int powerFactor = 0;
                foreach (UnitBase unit in this.units)
                    powerFactor += unit.OffensivePower;
                return powerFactor;
            }
        }

        /// <summary>
        /// Gets or sets the percentage of commerce dedicated to entertainment.
        /// </summary>
        public int EntertainmentPercentage
        {
            get { return this.entertainmentPercentage; }
            set { this.entertainmentPercentage = value; }
        }

        /// <summary>
        /// Gets the <see cref="Civilization"/> associated with the country.
        /// </summary>
        public Civilization Civilization
        {
            get { return this.civilization; }
            set { this.civilization = value; }
        }

        /// <summary>
        /// The type of <see cref="Government"/> the <see cref="CountryBase"/> is currently using.		
        /// </summary>
        public Government Government
        {
            get { return this.government; }
            set { this.government = value; }
        }

        /// <summary>
        /// The list of <see cref="Technology"/> objects that can be researched by this 
        /// <see cref="CountryBas"/>.
        /// </summary>
        public NamedObjectCollection<Technology> ResearchableTechnologies
        {
            get { return this.researchableTechnologies; }
        }

        /// <summary>
        /// The list of <see cref="Technology"/> objects that have been researched by the <see cref="CountryBase"/>.
        /// </summary>
        public NamedObjectCollection<Technology> AcquiredTechnologies
        {
            get { return this.acquiredTechnologies; }
        }

        /// <summary>
        /// Gets or sets a color for the <see cref="Country"/>.
        /// </summary>
        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        /// <summary>
        /// The total number of culture points for the colony
        /// </summary>
        public int CulturePoints
        {
            get { return this.culturePoints; }
            set { this.culturePoints = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not to prompt the user 
        /// somehow that their turn is over.
        /// </summary>
        /// <remarks>This is useful in situations when the user does not have
        /// any activated units in a turn.  In these situations, the users turn 
        /// will appear to be skipped.  Setting this property to <i>true</i> will
        /// prevent that.</remarks>
        public bool NotifyEndOfTurn
        {
            get { return this.notifyEndOfTurn; }
            set
            {
                if (value != this.notifyEndOfTurn)
                {
                    this.notifyEndOfTurn = value;
                    OnNotifyEndOfTurnChanged();
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="Era"/> the country is currently in.
        /// </summary>
        public Era Era
        {
            get { return this.era; }
            set
            {
                if (this.era != value)
                {
                    this.era = value;
                    OnEraChanged();
                }
            }
        }

        /// <summary>
        /// The current <see cref="Technology"/> being researched by the <see cref="CountryBase"/>.
        /// </summary>
        public Technology ResearchedTechnology
        {
            get { return this.researchedTechnology; }
            set
            {
                if (this.researchedTechnology != value)
                {
                    this.researchedTechnology = value;
                    OnResearchedTechnologyChanged();
                }
            }
        }

        /// <summary>
        /// The list of governements that are available to the country.
        /// </summary>
        /// <remarks>This list will not contain the current governement.</remarks>
        public NamedObjectCollection<Government> AvailableGovernments
        {
            get { return this.availableGovernments; }
        }

        /// <summary>
        /// The list of resources that are available to the country.
        /// </summary>
        public NamedObjectCollection<Resource> AvailableResources
        {
            get { return this.availableResources; }
        }

        /// <summary>
        /// All the units belonging to the colony
        /// </summary>
        public NamedObjectCollection<UnitBase> Units
        {
            get { return this.units; }
        }

        /// <summary>
        /// Occurs when the <i>Era</i> property changes.
        /// </summary>
        public event EventHandler EraChanged
        {
            add
            {
                this.eraChanged += value;
            }

            remove
            {
                this.eraChanged -= value;
            }
        }

        /// <summary>
        /// Fires the <i>EraChanged</i> event.
        /// </summary>
        protected virtual void OnEraChanged()
        {
            if (this.eraChanged != null)
            {
                this.eraChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Occurs when the <c>NotifyEndOfTurn</c> property changes.
        /// </summary>
        public event EventHandler NotifyEndOfTurnChanged
        {
            add
            {
                this.notifyEndOfTurnChanged += value;
            }

            remove
            {
                this.notifyEndOfTurnChanged -= value;
            }
        }

        /// <summary>
        /// Fires the <i>NotifyEndOfTurnChanged</i> event.
        /// </summary>
        protected virtual void OnNotifyEndOfTurnChanged()
        {
            if (this.notifyEndOfTurnChanged != null)
            {
                this.notifyEndOfTurnChanged(this, null);
            }
        }

        /// <summary>
        /// Occurs when the value of the <i>ResearchTechnology</i> 
        /// property changes.
        /// </summary>
        public event EventHandler ResearchedTechnologyChanged
        {
            add
            {
                this.researchedTechnologyChanged += value;
            }

            remove
            {
                this.researchedTechnologyChanged -= value;
            }
        }

        /// <summary>
        /// Fires the <i>ResearchedTechnologyChanged</i> event.
        /// </summary>
        protected virtual void OnResearchedTechnologyChanged()
        {
            if (this.researchedTechnologyChanged != null)
                this.researchedTechnologyChanged(this, EventArgs.Empty);
        }
    }
}
