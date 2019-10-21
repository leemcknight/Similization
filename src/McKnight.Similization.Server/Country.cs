using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Xml;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents a country in the game.
	/// </summary>
	public class Country : CountryBase
	{
        private int researchUnits;
        private int timesWithoutTechDirection;
        private int revolutionTurnsLeft;
        private bool unitActivatedOnTurn;
        private bool revolution;        
        private City capital;
        private Government nextGovernment;
        private NamedObjectCollection<City> cities;        
        private Collection<DiplomaticTie> diplomaticTies;                
        private event EventHandler<SpyCapturedEventArgs> spyCaptured;
        private event EventHandler<MutualProtectionPactEventArgs> mutualProtectionPactInvoked;
        private event EventHandler<WarDeclaredEventArgs> warDeclared;
        private event EventHandler<AudienceRequestedEventArgs> audienceRequested;
        private event EventHandler<GovernmentAvailableEventArgs> newGovernmentAvailable;
        private event EventHandler<RevolutionEndedEventArgs> revolutionEnded;
        private event EventHandler<DefeatedEventArgs> defeated;
        private event EventHandler started;
        private event EventHandler turnFinished;
        private event EventHandler<TradeProposedEventArgs> tradeProposed;        
        private event EventHandler researchDirectionNeeded;
        private event EventHandler revolutionStarted;
				
		/// <summary>
		/// Initializes a new instance of the <see cref="Country"/> class.
		/// </summary>
		public Country() : base()
		{
            this.cities = new NamedObjectCollection<City>();
            this.diplomaticTies = new Collection<DiplomaticTie>();
		}

		/// <summary>
		/// Intializes a new instance of the <see cref="Country"/> class.
		/// </summary>
		/// <param name="civilization"></param>
		/// <param name="leaderName"></param>
		/// <param name="coordinates"></param>
		public Country(Civilization civilization, string leaderName, Point coordinates) : this()
		{
			this.Civilization = civilization;
			this.LeaderName = leaderName;
			this.Gold = 10;
			AddCivTechs();
			AddFirstSettler(coordinates);
		}		

		/// <summary>
		/// Adds the first settler to the game at the coordinates specified.
		/// </summary>
		/// <param name="coordinates"></param>
		private void AddFirstSettler(Point coordinates)
		{						
			//starting unit
            bool ai = this.GetType() == typeof(AICountry);
			Settler settler = UnitFactory.CreateSettler(coordinates, this);
            GameRoot root = GameRoot.Instance;
            GridCell cell = root.Grid.GetCell(coordinates);
			cell.Explore(this);
			this.Units.Add(settler);
		}

		/// <summary>
		/// Takes a turn for the country.
		/// </summary>
		internal virtual void DoTurn()
		{
			int goldBeforeTurn = this.Gold;

			foreach(City city in this.cities)			
				city.DoTurn();
						
			int expenses = CalculateCorruptionExpensePerTurn() + 
							CalculateTotalUnitExpensePerTurn() + 
							CalculateMaintenanceExpensePerTurn();
			
			this.Gold -= expenses;
			int goldAfterTurn = this.Gold;

			foreach(Unit unit in this.Units)			
				unit.DoTurn();
						
			int profit = goldAfterTurn - goldBeforeTurn;
			DivyGold(profit);
			ApplyCultureEffects();
	
			if(this.ResearchedTechnology != null)
			{
				CheckForTechnologyAdvance();
			}
			else
			{
				this.timesWithoutTechDirection++;
				if(this.timesWithoutTechDirection == 3)
				{
					OnResearchDirectionNeeded();
				}
			}

			IncrementScore();			
			if(this.revolution)
			{
				if(this.revolutionTurnsLeft > 0)
				{
					this.revolutionTurnsLeft--;
				}
				else
				{
					this.revolution = false;
					RevolutionEndedEventArgs e = new RevolutionEndedEventArgs(this.nextGovernment);
					OnRevolutionEnded(e);
				}
			}
			OnTurnFinished();
		}

		private void ApplyCultureEffects()
		{
			foreach(City city in this.cities)			
				this.CulturePoints += city.CulturePerTurn;			
		}

		private void DivyGold(int gold)
		{
			double entertainment = (double)this.EntertainmentPercentage/100;
			double science = (double)this.SciencePercentage/100;
			double scienceUnits = gold * science;
			double entertainmentUnits = gold * entertainment;
			double remainder = gold - (scienceUnits + entertainmentUnits);
			this.researchUnits += Convert.ToInt32(scienceUnits);
			this.Gold += Convert.ToInt32(remainder);
		}

		/// <summary>
		/// Increments the score for the country.
		/// </summary>
		protected virtual void IncrementScore()
		{
			//neighbor relations
			int relationDelta = 0;
			foreach(DiplomaticTie tie in this.diplomaticTies)
			{
				switch(tie.Attitude)
				{
					case Attitude.Annoyed:
						relationDelta += 10;
						break;
					case Attitude.Cautious:
						relationDelta += 20;
						break;
					case Attitude.Furious:
						break;
					case Attitude.Gracious:
						relationDelta += 40;
						break;
					case Attitude.Polite:
						relationDelta += 30;
						break;
				}
			}

			//amount of territory
			GameRoot root = GameRoot.Instance;
			int territory = root.Grid.GetTerritoryAmount(this);

			//# of happy citizens
			int happyPeople = 0;
			foreach(City city in this.cities)
			{
				happyPeople += city.HappyPeople;
			}

			this.Score = happyPeople + relationDelta + territory;
		}

		private void CheckForTechnologyAdvance()
		{
			//technology advance
			if(this.researchUnits >= (this.ResearchedTechnology.RequiredResearchUnits * this.AdvanceCostFactor) )
			{
				LearnTechnology(this.ResearchedTechnology);
			}
		}

		internal void LearnTechnology(Technology newTechnology)
		{
			//add our new technology to the acquired list
			this.AcquiredTechnologies.Add(newTechnology);

			//rebuild the list of researchable technologies
			UpdateResearchableTechnologies();

			//reset our research units
			this.researchUnits = 0;

			//check for era advance
			if(HasNextEraRequirements())
			{
				this.Era = this.Era.NextEra;
			}
		}

		private bool HasNextEraRequirements()
		{
			bool hasIt = true;
			foreach(Technology technology in this.ResearchableTechnologies)
			{
				if(technology.Era == this.Era && technology.RequiredForEraAdvance)
				{
					hasIt = false;
					break;
				}
			}
			return hasIt;
		}

        /// <summary>
        /// Starts a revolution, trying to change to the specified <see cref="Government"/>.
        /// </summary>
        /// <param name="desiredGovernment"></param>
        public void StartRevolution(Government desiredGovernment)
        {
            if (this.nextGovernment != null)
            {
                this.revolution = false;
                this.Government = desiredGovernment;
                UpdateAvailableGovernments();
            }
            else if (this.Government != null)
            {
                this.revolution = true;
                this.nextGovernment = desiredGovernment;
                this.revolutionTurnsLeft = 4; //FIXME: how long does a revolution last?

                GameRoot root = GameRoot.Instance;
                foreach (Government gov in root.Ruleset.Governments)
                {
                    if (gov.Fallback)
                    {
                        this.Government = gov;
                        break;
                    }
                }

                OnRevolutionStarted();
            }
            else
            {
                this.Government = desiredGovernment;
            }
        }
				
		/// <summary>
		/// Determines whether the <see cref="Country"/> can create 
		/// a trade embargo against other countries.
		/// </summary>
		/// <remarks>To be able to have a trade embargo, a country 
		/// must have sucessfully researched the proper technologies, and
		/// must have an embassy.</remarks>
		public bool CanInvokeTradeEmbargoWith(Country ally)
		{
			if(ally == null)
				throw new ArgumentNullException("ally");
			DiplomaticTie tie = GetDiplomaticTie(ally);
			if(tie == null)
				return false;
			return tie.HasEmbassy;
		}

        /// <summary>
        /// The <see cref="City"/> that is acting as the capital for the 
        /// <see cref="Country"/>.
        /// </summary>
        public City CapitalCity
        {
            get { return this.capital; }
            set 
            {
                if (this.cities.Contains(value))
                    this.capital = value;
                else
                    throw new InvalidOperationException(ServerResources.CapitialNotInCountry);
            }
        }
		
		/// <summary>
		/// Gets a list of all the diplomatic ties a country has.
		/// </summary>
		public Collection<DiplomaticTie> DiplomaticTies
		{
			get { return diplomaticTies; }
		}
						
		/// <summary>
		/// Gets a list of all the cities belonging to the country.
		/// </summary>
        public NamedObjectCollection<City> Cities
		{
			get { return this.cities; }
		}				
								
		/// <summary>
		/// Occurs when a foreign leader requests an audience with this country.
		/// </summary>
		public event EventHandler<AudienceRequestedEventArgs> AudienceRequested
		{
			add
			{
				this.audienceRequested += value; 
			}

			remove
			{
				this.audienceRequested -= value; 
			}
		}

		/// <summary>
		/// Fires the <i>AudienceRequested</i> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnAudienceRequested(AudienceRequestedEventArgs e)
		{
			if(this.audienceRequested != null)			
				this.audienceRequested(this, e);			
		}
		
		/// <summary>
		/// Event that fires when the country has been militarily defeated.
		/// </summary>
		public event EventHandler<DefeatedEventArgs> Defeated
		{
			add
			{
				this.defeated += value;
			}

			remove
			{
				this.defeated -= value;
			}
		}

		/// <summary>
		/// Fires the <i>Defeated</i> event.
		/// </summary>
		protected virtual void OnDefeated(DefeatedEventArgs e)
		{
			if(this.defeated != null)			
				this.defeated(this, e);			
		}
				
		/// <summary>
		/// Occurs when a new government becomes available to the country.
		/// </summary>
		public event EventHandler<GovernmentAvailableEventArgs> NewGovernmentAvailable
		{
			add
			{
				this.newGovernmentAvailable += value; 
			}

			remove
			{
				this.newGovernmentAvailable -= value;
			}
		}

		/// <summary>
		/// Fires the <i>NewGovernmentAvailable</i> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnNewGovernmentAvailable(GovernmentAvailableEventArgs e)
		{
			if(this.newGovernmentAvailable != null)			
				this.newGovernmentAvailable(this, null);			
		}
		
		/// <summary>
		/// Event that fires whenever the wise men of the country need direction
		/// on what technology to research next.
		/// </summary>
		public event EventHandler ResearchDirectionNeeded
		{
			add
			{
				this.researchDirectionNeeded += value;
			}

			remove
			{
				this.researchDirectionNeeded -= value;
			}
		}

		/// <summary>
		/// Fires the <i>ResearchDirectionNeeded</i> event.
		/// </summary>
		protected virtual void OnResearchDirectionNeeded()
		{
			if(this.researchDirectionNeeded != null)			
				this.researchDirectionNeeded(this, EventArgs.Empty);			
		}
		
		/// <summary>
		/// Event that fires whenever a revolution has started in the Country.
		/// </summary>
		public event EventHandler RevolutionStarted
		{
			add
			{
				this.revolutionStarted += value;
			}

			remove
			{
				this.revolutionStarted -= value; 
			}
		}

		/// <summary>
		/// Fires the <i>RevolutionStarted</i> event.
		/// </summary>
		protected virtual void OnRevolutionStarted()
		{
			if(this.revolutionStarted != null)			
				this.revolutionStarted(this, null);			
		}
		
		/// <summary>
		/// Occurs when a revolution has ended.
		/// </summary>
		public event EventHandler<RevolutionEndedEventArgs> RevolutionEnded
		{
			add
			{
				this.revolutionEnded += value; 
			}

			remove
			{
				this.revolutionEnded -= value;
			}
		}

		/// <summary>
		/// Fires the <i>RevolutionEnded</i> event.
		/// </summary>
		protected virtual void OnRevolutionEnded(RevolutionEndedEventArgs e)
		{
			if(this.revolutionEnded != null)			
				this.revolutionEnded(this,e);			
		}
		
		/// <summary>
		/// Occurs when the country starts its' first turn in the game.
		/// </summary>
		public event EventHandler Started
		{
			add
			{
				this.started += value; 
			}

			remove
			{
				this.started -= value;
			}
		}

		/// <summary>
		/// Fires the <i>Started</i> event.
		/// </summary>		
		protected virtual void OnStarted()
		{
			if(this.started != null)			
				this.started(this, EventArgs.Empty);			
		}

		/// <summary>
		/// Fires the <i>TradeProposed</i> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnTradeProposed(TradeProposedEventArgs e)
		{
			if(this.tradeProposed != null)			
				this.tradeProposed(this, e);			
		}

		/// <summary>
		/// Occurs when another player proposes a trade.
		/// </summary>
		public event EventHandler<TradeProposedEventArgs> TradeProposed
		{
			add
			{
				this.tradeProposed += value;
			}

			remove
			{
				this.tradeProposed -= value;
			}
		}
		
		/// <summary>
		/// Occurs when a country has reached the end of its' turn.
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
				this.turnFinished(this,null);			
		}
		
		/// <summary>
		/// Occurs when war has been declared with another <see cref="Country"/>.
		/// </summary>
		public event EventHandler<WarDeclaredEventArgs> WarDeclared
		{
			add
			{
				this.warDeclared += value; 
			}

			remove
			{
				this.warDeclared -= value; 
			}
		}

		/// <summary>
		/// Fires the <see cref="WarDeclared"/> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnWarDeclared(WarDeclaredEventArgs e)
		{
			if(this.warDeclared != null)
				this.warDeclared(this, e);
		}

		private void AddCivTechs()
		{
			foreach(Technology technology in this.Civilization.StartingTechnologies)
			{
				this.AcquiredTechnologies.Add(technology);
			}
		}

		private void DiplomaticTieCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if(e.Action == CollectionChangeAction.Add)
			{
				//new diplomatic tie
				DiplomaticTie t = (DiplomaticTie)e.Element;
				t.DiplomaticStateChanged += new EventHandler(DiplomaticStateChanged);
			}
		}

		private void DiplomaticStateChanged(object sender, EventArgs e)
		{
			DiplomaticTie t = (DiplomaticTie)sender;
			if(t.DiplomaticState == DiplomaticState.War)
			{
				//someone just declared war on us.
				//check for mutual protection pacts.
				foreach(DiplomaticTie tie in this.diplomaticTies)
				{
					foreach(DiplomaticAgreement agreement in tie.DiplomaticAgreements)
					{
						if(agreement is MutualProtectionPact)
						{
							tie.ForeignCountry.InvokeMutualProtectionPact(this, t.ForeignCountry);
						}
					}
				}
			}
		}

		private void HandleCityCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if(e.Action == CollectionChangeAction.Remove)
			{
				if((this.cities.Count == 0) && (this.Units.Count == 0))
				{
					GameRoot root = GameRoot.Instance;
					//This country has been destroyed.
					DefeatedEventArgs ed = new DefeatedEventArgs(this, this);	//FIXME: conqueror
					OnDefeated(ed);
					root.Countries.Remove(this);
				}
			}
		}

		private void HandleAcquiredTechnologiesCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			UpdateAvailableGovernments();
		}

		private void HandleUnitCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if(e.Action == CollectionChangeAction.Remove)
			{
				if((this.Units.Count == 0) && (this.cities.Count == 0))
				{
					DefeatedEventArgs de = 
						new DefeatedEventArgs(this,this); //FIXME: conqueror
					OnDefeated(de);
				}
			}
		}

		/// <summary>
		/// Requests an audience with the country.
		/// </summary>
		/// <param name="requester">The <c>Country</c> that is requesting an audience.</param>
		/// <param name="givenItems"></param>
		/// <param name="takenItems"></param>
		/// <remarks>A request for an audience can be either accepted or rejected by 
		/// the asked party.</remarks>
		public virtual void RequestAudience(Country requester, Collection<ITradable> givenItems, Collection<ITradable> takenItems)
		{
			AudienceRequestedEventArgs e = new AudienceRequestedEventArgs(requester, givenItems, takenItems);
			OnAudienceRequested(e);
		}

		/// <summary>
		/// Requests an audience with the country.
		/// </summary>
		/// <param name="requester"></param>
		public virtual void RequestAudience(Country requester)
		{
			RequestAudience(requester, null, null);
		}

		/// <summary>
		/// Invokes the <see cref="MutualProtectionPact"/> between this <see cref="Country"/> and 
		/// the <see cref="Country"/> passed as the <i>friend</i> parameter.  
		/// </summary>
		/// <param name="friend"></param>
		/// <param name="enemy"></param>
		/// <remarks>Invoking the <see cref="MutualProtectionPact"/> will cause this <see cref="Country"/> to
		/// start a war against <i>enemy</i>.</remarks>
		public virtual void InvokeMutualProtectionPact(Country friend, Country enemy)
		{
			if(friend == null)
				throw new ArgumentNullException("friend");

			if(enemy == null)
				throw new ArgumentNullException("enemy");

			DiplomaticTie tie = null;
			foreach(DiplomaticTie t in this.diplomaticTies)
			{
				if(t.ForeignCountry == friend)
				{
					tie = t;
					break;
				}
			}
			
			if(tie == null)
				throw new ArgumentException(ServerResources.NoDiplomaticTie);

			MutualProtectionPact pact = null;
			foreach(DiplomaticAgreement agreement in tie.DiplomaticAgreements)
			{
                MutualProtectionPact p = agreement as MutualProtectionPact;
				if(p != null)
				{					
					if((p.Country1 == friend || p.Country2 == friend))
					{
						pact = p;
						break;
					}
				}
			}

			if(pact == null)
				throw new ArgumentException(ServerResources.NoMutualProtectionPact, "friend");

			OnMutualProtectionPactInvoked(new MutualProtectionPactEventArgs(pact, enemy));
			DeclareWar(enemy);
		}

		/// <summary>
		/// Gets a value indicating if the other country is considered a foe.
		/// </summary>
		/// <param name="foreignCountry"></param>
		/// <returns></returns>
		public bool IsFoeOf(Country foreignCountry)
		{
			bool isFoe = false;

			foreach(DiplomaticTie diplomaticTie in this.diplomaticTies)
			{
				if(diplomaticTie.ForeignCountry == foreignCountry)
				{
					if(diplomaticTie.DiplomaticState == DiplomaticState.War)
					{
						isFoe = true;
					}
					break;
				}
			}
			return isFoe;
		}

		/// <summary>
		/// Gets a value indicating whether or not the other country is considered a friend.
		/// </summary>
		/// <param name="foreignCountry"></param>
		/// <returns></returns>
		public bool IsFriendOf(Country foreignCountry)
		{
			return !IsFoeOf(foreignCountry);
		}

		/// <summary>
		/// Establishes a diplomatic tie with the foreign country.
		/// </summary>
		/// <param name="foreignCountry"></param>
		public DiplomaticTie EstablishDiplomaticTie(Country foreignCountry)
		{
			//make sure we don't already have a tie with this country
			DiplomaticTie tie = GetDiplomaticTie(foreignCountry);
			if(tie != null)
			{
				return tie;
			}

			tie = new DiplomaticTie(this, foreignCountry);
			this.diplomaticTies.Add(tie);

			return tie;
		}

		/// <summary>
		/// Establishes an embassy with the foreign country.  This 
		/// allows investigating cities, stealing technologies, and
		/// contacting leaders.
		/// </summary>
		/// <param name="foreignCountry"></param>
		public void EstablishEmbassy(Country foreignCountry)
		{
			foreach(DiplomaticTie diplomaticTie in this.diplomaticTies)
			{
				if(diplomaticTie.ForeignCountry == foreignCountry)
				{
					diplomaticTie.HasEmbassy = true;
					break;
				}
			}
		}

		/// <summary>
		/// Gets the diplomatic tie that has been established with the 
		/// foreign country.
		/// </summary>
		/// <param name="foreignCountry">The country the tie has been established with</param>
		/// <remarks>If no diplomatic tie has been established with the foreign country, 
		/// a <i>null</i> value will be returned.</remarks>
		public DiplomaticTie GetDiplomaticTie(Country foreignCountry)
		{
			foreach(DiplomaticTie tie in this.diplomaticTies)
			{
				if(tie.ForeignCountry == foreignCountry)
				{
					return tie;
				}
			}

			return null;
		}

        /// <summary>
        /// Captures a spy from the <see cref="Country"/> within the <see cref="DiplomaticTie"/>.
        /// </summary>
        /// <param name="diplomaticTie"></param>
        /// <param name="espionageAction"></param>
        /// <param name="espionageCompleted"></param>
        /// <param name="city"></param>
        internal virtual void CaptureSpy(DiplomaticTie diplomaticTie, EspionageAction espionageAction, bool espionageCompleted, City city)
        {
            SpyCapturedEventArgs args = new SpyCapturedEventArgs(diplomaticTie, espionageAction, espionageCompleted, city);
            OnSpyCaptured(args);
        }

		/// <summary>
		/// Gets a city name for the colony.  Useful in situations where the player 
		/// is creating a new city and you want to give them a suggestion.  Also useful
		/// for AI opponents.
		/// </summary>
		/// <returns>A <c>string</c> representation of the city name.</returns>
		public string CreateNewCityName()
		{
			bool cityNameOK;
			string nextCityName = "";

			foreach(string cityName in this.Civilization.CityNames)
			{
				cityNameOK = true;
				foreach(City city in this.cities)
				{
					if(city.Name == cityName)
					{
						cityNameOK = false;
						break;
					}
				}

				if(cityNameOK)
				{
					nextCityName = cityName;
					break;
				}
			}

			return nextCityName;
		}

		/// <summary>
		/// Declares war on the specified country.
		/// </summary>
		/// <param name="enemy"></param>
		public void DeclareWar(Country enemy)
		{
			if(enemy == null)
				throw new ArgumentNullException("enemy");

			DiplomaticTie tie = null;
			//if there's an existing tie between us, update it.
			foreach(DiplomaticTie t in this.diplomaticTies)
			{
				if(t.ForeignCountry == enemy)
				{
					tie = t;
					break;
				}
			}

			if(tie == null)
			{
				tie = new DiplomaticTie(this, enemy);
				this.diplomaticTies.Add(tie);
			}
		
			tie = null;
			//do the same for them.
			foreach(DiplomaticTie t in enemy.DiplomaticTies)
			{
				if(t.ForeignCountry == this)
				{
					tie = t;
					break;
				}
			}

			if(tie == null)
			{
				tie = new DiplomaticTie(enemy, this);
				enemy.diplomaticTies.Add(tie);
			}
			tie.Attitude = Attitude.Furious;
			tie.DiplomaticState = DiplomaticState.War;
			tie.CurrentTrades.Clear();

			WarDeclaredEventArgs e = new WarDeclaredEventArgs(this, enemy);
			OnWarDeclared(e);
		}

		/// <summary>
		/// Disbands the unit passed into the function.
		/// </summary>
		/// <param name="unit">The unit to disband.</param>
		public void DisbandUnit(Unit unit)
		{
			if(this.Units.Contains(unit))			
				this.Units.Remove(unit);			
		}

		/// <summary>
		/// Gets the amount of gold that would be plundered if the city in the
		/// parameter was invaded by a foreign enemy.
		/// </summary>
		/// <param name="plunderedCity"></param>
		/// <returns></returns>
		public int GetPlunderedAmountForCity(City plunderedCity)
		{
			if(plunderedCity == null)
				throw new ArgumentNullException("plunderedCity");

			double totalPopulation = 0;
			
			foreach(City city in this.cities)			
				totalPopulation += city.Population;			
			double percentage = (double)plunderedCity.Population/totalPopulation;			
			int plunderedAmount = Convert.ToInt32(percentage * this.Gold);
			return plunderedAmount;
		}

		/// <summary>
		/// Proposes a trade to the foreign country with items on each side of the table.
		/// </summary>
		/// <param name="givenItems"></param>
		/// <param name="takenItems"></param>
		/// <param name="foreignCountry"></param>
		/// <returns></returns>
		public virtual TradeResponse ProposeTrade(Collection<ITradable> givenItems, Collection<ITradable> takenItems, Country foreignCountry)
		{
			TradeProposedEventArgs eventArgs;
			eventArgs = new TradeProposedEventArgs(givenItems,takenItems,foreignCountry);
			OnTradeProposed(eventArgs);
			return TradeResponse.Accept;
		}

		/// <summary>
		/// Gets the percentage ( 1 - 100% ) done researching
		/// the next technology.
		/// </summary>
		public int PercentageDoneResearching
		{
			get
			{
				double percentDone;
				double x;
				double y;

				x = Convert.ToDouble(this.researchUnits);
				y = Convert.ToDouble(this.ResearchedTechnology.RequiredResearchUnits);

				percentDone = x/y;
				percentDone *= 100;
				if(percentDone > 100)
				{
					percentDone = 100;
				}
				return Convert.ToInt32(percentDone);
			}
		}

		/// <summary>
		/// Gets the amount of gold per turn generated from all the cities 
		/// belonging to the country.
		/// </summary>
		/// <returns></returns>
		public int CalculateIncomePerTurnFromCities()
		{
			int gold = 0;

			foreach(City city in this.cities)
			{
				gold += city.GoldPerTurn;
			}

			return gold;
		}

		/// <summary>
		/// Gets the amount of gold generated each turn by the tax collectors.
		/// </summary>
		/// <returns></returns>
		public int CalculateIncomePerTurnFromTaxmen()
		{
			//TODO: implement
			return 0;
		}

		/// <summary>
		/// Gets the (estimated) gold per turn produced by this country.
		/// </summary>
		/// <returns></returns>
		public int CalculateTotalIncomePerTurn()
		{
			return CalculateIncomePerTurnFromCities();
		}

		/// <summary>
		/// Gets the amount of money the country spends on science per turn.
		/// </summary>
		/// <returns></returns>
		public int CalculateScienceExpensePerTurn()
		{
            double sp = Convert.ToDouble(this.SciencePercentage);
			double percent = sp/100d;

			int expenditures = 
				CalculateCorruptionExpensePerTurn() +
				CalculateMaintenanceExpensePerTurn() +
				CalculateTotalUnitExpensePerTurn();

			int baseAmt = CalculateTotalIncomePerTurn() - expenditures;
			double amt = percent * baseAmt;
			int final  = Convert.ToInt32(Math.Ceiling(amt));
			return final;
		}

		/// <summary>
		/// Gets the amount of money the country spends on entertainment per turn
		/// </summary>
		/// <returns></returns>
		public int CalculateEntertainmentExpensePerTurn()
		{
            double ep = Convert.ToDouble(this.EntertainmentPercentage);
			double percent = ep/100d;
			int expenditures = 
				CalculateCorruptionExpensePerTurn() +
				CalculateMaintenanceExpensePerTurn() +
				CalculateTotalUnitExpensePerTurn();

			int baseAmt = CalculateTotalIncomePerTurn() - expenditures;
			double amt = percent * baseAmt;
			int final  = Convert.ToInt32(Math.Ceiling(amt));
			return final;
		}

		/// <summary>
		/// Gets the total number of units allowed for free, given the current population of the 
		/// country.
		/// </summary>
		/// <returns>A <see cref="System.Int32"/> representing the number of free units allowed.</returns>
		/// <remarks>A negative number indicates that <i>all</i> units are free, regardless of 
		/// country population.</remarks>
		public int CalculateNumberOfFreeUnitsAllowed()
		{
			int freeUnits = 0;
			foreach(City city in this.cities)
			{
				switch(city.SizeClass)
				{
					case CitySizeClass.City:
						freeUnits += this.Government.FreeCityUnits;
						break;
					case CitySizeClass.Metropolis:
						freeUnits += this.Government.FreeMetropolisUnits;
						break;
					case CitySizeClass.Town:
						freeUnits += this.Government.FreeTownUnits;
						break;
				}
			}

			return freeUnits;
		}

		/// <summary>
		/// Gets the amount of money the country spends on units per turn.
		/// </summary>
		/// <returns></returns>
		public int CalculateTotalUnitExpensePerTurn()
		{
			int freeUnits = CalculateNumberOfFreeUnitsAllowed();
			
			if(freeUnits > 0)
			{
				int totalUnits = this.Units.Count;
				int cost = totalUnits - freeUnits;
				if(cost > 0)
				{
					return cost;
				}
				else
				{
					return 0;
				}
			}
			else
			{
				//negative free units means that the army does not 
				//impose a cost on the unit support.
				return 0;
			}
			
		}

		/// <summary>
		/// Gets the amount of money the country spends on maintenance costs per turn.
		/// </summary>
		/// <returns></returns>
		public int CalculateMaintenanceExpensePerTurn()
		{
			int expense = 0;
			foreach(City city in this.cities)
			{
				expense += city.MaintenanceCostPerTurn;
			}

			return expense;
		}

		/// <summary>
		/// Gets the amount of money that is lost to corruption per turn for the country.
		/// </summary>
		/// <returns></returns>
		public int CalculateCorruptionExpensePerTurn()
		{
			//TODO: implement
			return 0;
		}

		/// <summary>
		/// Gets the total amount of money, per turn, that the country spends.
		/// </summary>
		/// <returns></returns>
		public int CalculateTotalExpensePerTurn()
		{
			int total = 
				CalculateCorruptionExpensePerTurn() +
				CalculateMaintenanceExpensePerTurn() +
				CalculateTotalUnitExpensePerTurn() +
				CalculateEntertainmentExpensePerTurn() +
				CalculateScienceExpensePerTurn();

			return total;
		}

		/// <summary>
		/// Gets the total profit, pet turn, that the country makes.
		/// </summary>
		/// <returns></returns>
		public int CalculateNetProfitPerTurn()
		{
			int profit = 
				CalculateTotalIncomePerTurn() -
				CalculateTotalExpensePerTurn();

			return profit;
		}

		/// <summary>
		/// Gets the number of turns until the country gets its next 
		/// technology advance.
		/// </summary>
		/// <returns></returns>
		public int CalculateTurnsUntilTechnologyAdvance()
		{
			double turns = 0;
			int researchPerTurn = 0;
			foreach(City city in this.cities)
			{
				researchPerTurn += city.ResearchPerTurn;
			}

			if(researchPerTurn <= 0)
			{
				return int.MaxValue;
			}
			turns = this.ResearchedTechnology.RequiredResearchUnits/researchPerTurn;

			int advanceTurns = Convert.ToInt32(Math.Ceiling(turns));

			return advanceTurns;
		}

		/// <summary>
		/// Gets the cultural perception of the foreign country toward this country.
		/// </summary>
		/// <param name="foreignCountry"></param>
		/// <returns></returns>
		public CulturalPerception GetCulturalPerception(Country foreignCountry)
		{	
			if(foreignCountry == null)
				throw new ArgumentNullException("foreignCountry");

			double culturePtRatio;
			CulturalPerception perception;

			culturePtRatio = this.CulturePoints / foreignCountry.CulturePoints;
			
			if ( culturePtRatio >= 3f )
			{
				perception = CulturalPerception.InAwe;
			}
			else if ( culturePtRatio >= 2f )
			{
				perception = CulturalPerception.Admirer;
			}
			else if ( culturePtRatio >= 1f )
			{
				perception = CulturalPerception.Impressed;
			}
			else if ( culturePtRatio >= .75f )
			{
				perception = CulturalPerception.Unimpressed;
			}
			else if ( culturePtRatio >= .5f )
			{
				perception = CulturalPerception.Dismissive;
			}
			else 
			{
				perception = CulturalPerception.Disdainful;
			}

			return perception;
		}
		
		/// <summary>
		/// Gets or sets a value indicating if at least 1 unit was active
		/// during the past turn of the game.
		/// </summary>
		internal bool UnitActivatedOnTurn
		{
			get { return this.unitActivatedOnTurn; }
			set { this.unitActivatedOnTurn = value; }
		}
		
		/// <summary>
		/// Occurs when a <see cref="MutualProtectionPact"/> this country has with an ally 
		/// is invoked by the ally.
		/// </summary>
		public event EventHandler<MutualProtectionPactEventArgs> MutualProtectionPactInvoked
		{
			add
			{
				this.mutualProtectionPactInvoked += value;
			}

			remove
			{
				this.mutualProtectionPactInvoked -= value;
			}
		}

		/// <summary>
		/// Fires the <i>MutualProtectionPactInvoked</i> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMutualProtectionPactInvoked(MutualProtectionPactEventArgs e)
		{
			if(this.mutualProtectionPactInvoked != null)
				mutualProtectionPactInvoked(this, e);
		}


        
        /// <summary>
        /// Occurs when this <see cref="Country"/> captured a foreign spy.
        /// </summary>
        /// <remarks>For more information, see the <see cref="SpyCapturedEventArgs"/> class.</remarks>
        public event EventHandler<SpyCapturedEventArgs> SpyCaptured
        {
            add
            {
                this.spyCaptured += value;
            }

            remove
            {
                this.spyCaptured -= value; 
            }
        }

        /// <summary>
        /// Fires the <i>SpyCaptured</i> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSpyCaptured(SpyCapturedEventArgs e)
        {
            if (this.spyCaptured != null)
                this.spyCaptured(this, e);
        }

		/// <summary>
		/// Saves the Country information.
		/// </summary>
		/// <param name="writer"></param>
		public void Save(XmlWriter writer)
		{
			if(writer == null)
				throw new ArgumentNullException("writer");

			bool isAIColony = (this is AICountry);
			writer.WriteStartElement("Country");
			writer.WriteAttributeString("AI", isAIColony.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("CivilizationName", this.Name);
			writer.WriteElementString("Government", this.Government.Name);
			writer.WriteElementString("LeaderName", this.LeaderName);
			writer.WriteElementString("Era", this.Era.Name);
			writer.WriteElementString("Color", this.Color.Name);
			writer.WriteElementString("Gold", this.Gold.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Culture", this.CulturePoints.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("CommercePercentage", this.CommercePercentage.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("SciencePercentage", this.SciencePercentage.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("EntertainmentPercentage", this.EntertainmentPercentage.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Score", this.Score.ToString(CultureInfo.InvariantCulture));
			if(this.ResearchedTechnology != null)
			{
				writer.WriteElementString("NextTechnology", this.ResearchedTechnology.Name);
				writer.WriteElementString("ResearchUnits", this.researchUnits.ToString(CultureInfo.InvariantCulture));
			}
			writer.WriteStartElement("Cities");
			foreach(City city in this.cities)
			{
				city.Save(writer);
			}
			writer.WriteEndElement();
			writer.WriteStartElement("Units");
			foreach(Unit unit in this.Units)
			{
				unit.Save(writer);
			}
			writer.WriteEndElement();
			writer.WriteStartElement("Technologies");
			foreach(Technology tech in this.AcquiredTechnologies)
			{
				writer.WriteElementString("Technology", tech.Name);
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		/// <summary>
		/// Loads the Country from the xml stream.
		/// </summary>
		/// <param name="reader"></param>
		public void Load(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			GameRoot root = GameRoot.Instance;
			string last = "";
			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "Country")
					break;

                if (reader.NodeType == XmlNodeType.Element)
                {
                    last = reader.Name;
                    switch (last)
                    {
                        case "Cities":
                            LoadCities(reader);
                            break;
                        case "Units":
                            LoadUnits(reader);
                            break;
                        case "Technologies":
                            LoadTechnologies(reader);
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.Text)
                {
                    switch (last)
                    {
                        case "CivilizationName":
                            this.Civilization = root.Ruleset.Civilizations[reader.Value];
                            break;
                        case "Government":
                            this.Government = root.Ruleset.Governments[reader.Value];
                            break;
                        case "LeaderName":
                            this.LeaderName = reader.Value;
                            break;
                        case "Era":
                            this.Era = root.Ruleset.Eras[reader.Value];
                            break;
                        case "Color":
                            this.Color = Color.FromName(reader.Value);
                            break;
                        case "Gold":
                            this.Gold = XmlConvert.ToInt32(reader.Value);
                            break;
                        case "Culture":
                            this.CulturePoints = XmlConvert.ToInt32(reader.Value);
                            break;
                        case "CommercePercentage":
                            this.CommercePercentage = XmlConvert.ToInt32(reader.Value);
                            break;
                        case "SciencePercentage":
                            this.SciencePercentage = XmlConvert.ToInt32(reader.Value);
                            break;
                        case "EntertainmentPercentage":
                            this.EntertainmentPercentage = XmlConvert.ToInt32(reader.Value);
                            break;
                        case "Score":
                            this.Score = XmlConvert.ToInt32(reader.Value);
                            break;
                        case "NextTechnology":
                            this.ResearchedTechnology = root.Ruleset.Technologies[reader.Value];
                            break;
                        case "ResearchUnits":
                            this.researchUnits = XmlConvert.ToInt32(reader.Value);
                            break;
                    }
                }
			}
		}

        //Loads all the countrys' units from the xmlreader.
        private void LoadUnits(XmlReader reader)
        {            
            bool isAiUnit = (this is AICountry);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Units")
                    break;                
                else if (reader.NodeType == XmlNodeType.Element && reader.Name == "Technology")
                {
                    Unit unit = UnitFactory.FromSaveGameUnitNode(reader, isAiUnit);
                    this.Units.Add(unit);
                }
            }
        }

        //loads all of the acquired technologies for the country.
        private void LoadTechnologies(XmlReader reader)
        {
            GameRoot root = GameRoot.Instance;
            string last = string.Empty;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Technologies")
                    break;
                if (reader.NodeType == XmlNodeType.Element)
                    last = reader.Name;
                else if (reader.NodeType == XmlNodeType.Text && last == "Technology")                
                    this.AcquiredTechnologies.Add(root.Ruleset.Technologies[reader.Value]);                
            }
        }

        //loads all the cities from the XmlReader.
        private void LoadCities(XmlReader reader)
        {            
            bool ai = (this is AICountry);
            City city;
            string last = string.Empty;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Cities")
                    break;
                if (reader.NodeType == XmlNodeType.Element)
                    last = reader.Name;
                else if (reader.NodeType == XmlNodeType.Text && last == "City")
                {
                    if (ai)
                        city = new AICity();
                    else
                        city = new City();
                    city.Load(reader);
                    this.cities.Add(city);
                }
            }
        }

		/// <summary>
		/// Rebuilds the internal list of researchable technoliges
		/// for the colony.  This list is based on technologies that
		/// have not been acquired, but have all the technological
		/// requirements met.
		/// </summary>
		public void UpdateResearchableTechnologies()
		{
			//FIXME: This should really be a private function; I'm not real thrilled that it's
			//public for no other reason than it needs to be called after the constructor has finished.
			bool hasRequirements  = false;
			GameRoot root = GameRoot.Instance;

			this.ResearchableTechnologies.Clear();
			foreach(Technology technology in root.Ruleset.Technologies)
			{
				hasRequirements = false;
				if((!this.AcquiredTechnologies.Contains(technology)) && 
					(technology != this.ResearchedTechnology) && 
					(technology.Era == this.Era))
				{
					hasRequirements = true;
					foreach(Technology neededTechnology in technology.RequiredTechnologies)
					{
						hasRequirements = this.AcquiredTechnologies.Contains(neededTechnology);
						if(!hasRequirements)
						{
							break;
						}
					}
				}
				if(hasRequirements)
				{
					this.ResearchableTechnologies.Add(technology);

				}
			}
		}

		/// <summary>
		/// Refreshes the internal list of available governments based upon 
		/// the technologies that the country has researched.
		/// </summary>
		private void UpdateAvailableGovernments()
		{
			GameRoot root = GameRoot.Instance;
			bool avail = false;
			
			foreach(Government gov in root.Ruleset.Governments)
			{
				if(this.AvailableGovernments.Contains(gov))
				{
					continue;
				}
				else if(gov != this.Government)
				{
					avail = true;
					foreach(Technology tech in gov.RequiredTechnologies)
					{
						if(!this.AcquiredTechnologies.Contains(tech))
						{
							avail = false;
							break;
						}
					}
				}	
				if(avail)
				{
					this.AvailableGovernments.Add(gov);
				}
			}
		}

		/// <summary>
		/// Refreshes
		/// </summary>
		public void UpdateAvailableResources()
		{
			GameRoot root = GameRoot.Instance;
			bool avail = false;

			foreach(Resource resource in root.Ruleset.Resources)
			{
				if(this.AvailableResources.Contains(resource))
				{
					continue;
				}
				else
				{
					avail = true;
					foreach(Technology tech in resource.RequiredTechnologies)
					{
						if(!this.AcquiredTechnologies.Contains(tech))
						{
							avail = false;
							break;
						}
					}
				}
				if(avail)
				{
					this.AvailableResources.Add(resource);
				}
			}
		}
	}
}
