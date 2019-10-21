using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents a diplomatic tie between two governments.  
	/// </summary>
	public class DiplomaticTie
	{
        private bool hasSpy;
        private bool hasEmbassy;
        private bool continuedPropaganda;
		private Country parentCountry;
        private Country foreignCountry;
		private ArrayList currentTrades;
		private Attitude attitude;				
		private DiplomaticState diplomaticState;
		private event EventHandler diplomaticStateChanged;
        private Collection<DiplomaticAgreement> diplomaticAgreements;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DiplomaticTie"/> class.
		/// </summary>
		/// <param name="parentCountry"></param>
		/// <param name="foreignCountry"></param>
		public DiplomaticTie(Country parentCountry, Country foreignCountry)
		{
			this.parentCountry = parentCountry;
			this.foreignCountry = foreignCountry; 
			this.attitude = Attitude.Cautious;
			this.currentTrades = new ArrayList();
			this.diplomaticAgreements = new Collection<DiplomaticAgreement>();			
			this.diplomaticState = DiplomaticState.Peace;
		}
		
		/// <summary>
		/// Gets a list of trades between the two countries.
		/// </summary>
		public ArrayList CurrentTrades
		{
			get { return this.currentTrades; }
		}

		/// <summary>
		/// Gets the attitude of the foreign country toward the parent country.
		/// </summary>
		public Attitude Attitude
		{
			get { return this.attitude; }
			set { this.attitude = value; }
		}

        /// <summary>
        /// Gets the owner of the <see cref="DiplomaticTie"/>.
        /// </summary>
        public Country ParentCountry
        {
            get { return this.parentCountry; }
        }

		/// <summary>
		/// Gets the foreign country in the tie.
		/// </summary>
		public Country ForeignCountry
		{
			get { return this.foreignCountry; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether or not there is an embassy established 
		/// in the foreign country.
		/// </summary>
		public bool HasEmbassy
		{
			get { return this.hasEmbassy; }
			set { this.hasEmbassy = value; }
		}

        /// <summary>
        /// Determines whether the parent country has a spy in the embassy 
        /// of the country declared in the <i>ForeignCountry</i> property.
        /// </summary>
        /// <remarks>This property is read-only.  To attempt to add a spy, 
        /// call the <i>PlantSpy</i> method.</remarks>
        public bool HasSpy
        {
            get { return this.hasSpy; }
        }

		/// <summary>
		/// Gets the official diplomatic state of the two countries.
		/// </summary>
		public DiplomaticState DiplomaticState
		{
			get { return this.diplomaticState; }
			set 
			{ 
				if(this.diplomaticState != value)
				{
					this.diplomaticState = value; 
					OnDiplomaticStateChanged();
				}
			}
		}
		
		/// <summary>
		/// Gets a list of diplomatic agreements in the diplomatic tie.
		/// </summary>
		public Collection<DiplomaticAgreement> DiplomaticAgreements
		{
			get { return this.diplomaticAgreements; }
		}

		/// <summary>
		/// Attempts to steal a random technolgy from the foreign country. 
		/// </summary>				
		/// <returns></returns>
		public EspionageResult StealTechnology()
		{
            if (!this.hasEmbassy)
                throw new InvalidOperationException(ServerResources.EmbassyRequired);
            if (!this.hasSpy)
                throw new InvalidOperationException(ServerResources.SpyRequired);
            if (this.foreignCountry.Government.Fallback)
                return EspionageResult.ImmuneToEspionage;
			NamedObjectCollection<Technology> candidateTechs = new NamedObjectCollection<Technology>();
			Technology stolenTech; 			
			int randIdx = RandomNumber.UpTo(75);
            if (randIdx >= 75)
            {
                //failure.
                bool spyCaught;
                //50 percent chance of spy being caught
                randIdx = RandomNumber.UpTo(50);
                spyCaught = (randIdx >= 50);
                if (spyCaught)
                {
                    this.hasSpy = false;
                    this.foreignCountry.CaptureSpy(this, EspionageAction.StealTechnology, false, null);
                    return EspionageResult.SpyCaught;
                }
                else
                {
                    return EspionageResult.Failure;
                }
            }

			foreach(Technology foreignTech in this.foreignCountry.AcquiredTechnologies)
			{
				if(!this.parentCountry.AcquiredTechnologies.Contains(foreignTech))
				{
					candidateTechs.Add(foreignTech);
				}
			}
			randIdx = RandomNumber.UpTo(candidateTechs.Count - 1);
			stolenTech = candidateTechs[randIdx];
            this.parentCountry.AcquiredTechnologies.Add(stolenTech);                  
            return EspionageResult.Success;           
		}

		/// <summary>
		/// Attempts to investigate a city in the foreign country.
		/// </summary>
		/// <param name="foreignCity"></param>
		/// <returns></returns>
		public bool InvestigateCity(City foreignCity)
		{
            if (!this.hasEmbassy)
                throw new InvalidOperationException(ServerResources.EmbassyRequired);
			if(this.hasEmbassy)
				return false;
			else
				return true;
		}

		/// <summary>
		/// Attempts to plant a spy within the foreign country.  
		/// </summary>
        /// <remarks>
        /// There's a 50% chance this will succeed.  If it fails, however, 
        /// the foreign country will immediately become furious with your
        /// country.  If it succeeds, your spy can sabotage production and
        /// spread propaganda.</remarks>
		/// <returns></returns>
		public EspionageResult PlantSpy()
		{
            if (!this.hasEmbassy)
                throw new InvalidOperationException(ServerResources.EmbassyRequired);
			int randomNum = RandomNumber.Between(1, 100);
			bool success = (randomNum >= 50);

			if(success)
			{
				this.hasSpy = true;
                return EspionageResult.Success;
			}

            this.foreignCountry.CaptureSpy(this, EspionageAction.PlantSpy, false, this.foreignCountry.CapitalCity);
			return EspionageResult.SpyCaught;
		}

		/// <summary>
		/// Attempt to sabotage the current production item in the foreign city.
		/// This can only take place if there is a spy within the foreign country.
		/// </summary>
		/// <returns></returns>
		public EspionageResult Sabotage(City foreignCity)
		{
            if (foreignCity == null)
                throw new ArgumentNullException("foreignCity");
            if (!this.hasEmbassy)
                throw new InvalidOperationException(ServerResources.EmbassyRequired);
            if (!this.hasSpy)
                throw new InvalidOperationException(ServerResources.SpyRequired);
			int randNum;
			EspionageResult result;

            if (this.foreignCountry.Government.Fallback)
                return EspionageResult.ImmuneToEspionage;
			
			randNum = RandomNumber.Between(1,100);
			if(randNum >= 25)
			{
                //success, but is the spy captured?  25% chance of this happening.				
                randNum = RandomNumber.Between(1, 100);
                result = randNum > 25 ? EspionageResult.Success : EspionageResult.SuccessWithCapturedSpy;
                foreignCity.SabotageProduction();
			}
			else
			{
				//failure, but is the spy captured?  50/50 chance of this happening.
				randNum = RandomNumber.Between(1,100);
				result = randNum > 50 ? EspionageResult.Failure : EspionageResult.SpyCaught;
			}

            if (result == EspionageResult.SpyCaught)
            {
                this.foreignCountry.CaptureSpy(this, EspionageAction.SabotageProduction, false, foreignCity);
                this.hasSpy = false;
            }
            else if (result == EspionageResult.SuccessWithCapturedSpy)
            {
                this.foreignCountry.CaptureSpy(this, EspionageAction.SabotageProduction, true, foreignCity);
                this.hasSpy = false;
            }

			return result;
		}

		/// <summary>
		/// Attempts to get a foreign city to defect to the parent country by
		/// spreading propaganda.  Cities in anarchy are immune to proaganda.
		/// </summary>
		/// <param name="foreignCity"></param>
		/// <returns></returns>
		public EspionageResult SpreadPropaganda(City foreignCity)
		{
            if (!this.hasEmbassy)
                throw new InvalidOperationException(ServerResources.EmbassyRequired);
            if (!this.hasSpy)
                throw new InvalidOperationException(ServerResources.SpyRequired);
			//TODO: add code to account for continued resistance.
			//TODO: add code to account for more advanced governments.			
			if(foreignCity == null)
				throw new ArgumentNullException("foreignCity");
			CulturalPerception perception;
			EspionageResult result = EspionageResult.Failure;
			bool propogandaSpread;
			int chanceForPropaganda = 0;
			int chanceForResistance = 0;
			int chanceForContinuedResistance = 0;

			//a quick check: if the foreign city is in anarchy, 
			//the proganda campaign will automatically fail 
			//(although there is no chance the spy will be caught).
			if(this.foreignCountry.Government.Fallback)
			{
				result = EspionageResult.Failure;
				return result;
			}

			//there are 2 steps to taking over a city with propaganda.  The 
			//first step is successfully spreading propaganda, and the second 
			//step is not having that proaganda resisted.  These two taken 
			//together make it very difficult for overall proaganda to succeed.
			perception = this.foreignCountry.GetCulturalPerception( this.parentCountry );

			switch(perception)
			{
				case CulturalPerception.InAwe:
					chanceForPropaganda = 30;
					chanceForResistance = 40;
					chanceForContinuedResistance = 30;
					break;
				case CulturalPerception.Admirer:
					chanceForPropaganda = 25;
					chanceForResistance = 50;
					chanceForContinuedResistance = 40;
					break;
				case CulturalPerception.Impressed:
					chanceForPropaganda = 20;
					chanceForResistance = 60;
					chanceForContinuedResistance = 50;
					break;
				case CulturalPerception.Unimpressed:
					chanceForPropaganda = 10;
					chanceForResistance = 70;
					chanceForContinuedResistance = 60;
					break;
				case CulturalPerception.Dismissive:
					chanceForPropaganda = 5;
					chanceForResistance = 80;
					chanceForContinuedResistance = 70;
					break;
				case CulturalPerception.Disdainful:
					chanceForPropaganda = 3;
					chanceForResistance = 90;
					chanceForContinuedResistance = 80;
					break;
			}

			int randNum = RandomNumber.Between(1,100);

			if(randNum <= chanceForPropaganda)
			{
				propogandaSpread = true;
			}
			else
			{
				propogandaSpread = false;
				result = EspionageResult.Failure;
			}

			if(propogandaSpread)
			{
				randNum = RandomNumber.Between(1,100);
				if(randNum <= chanceForResistance)
				{
					result = EspionageResult.Failure;
				}
				else
				{
					//the propaganda was successfull.  The city now belongs
					//to the parent.
					result = EspionageResult.Success;
					this.parentCountry.Cities.Add(foreignCity);
					this.foreignCountry.Cities.Remove(foreignCity);
					foreignCity.ParentCountry = this.parentCountry;
				}
			}

			if(result == EspionageResult.Failure)
			{
				//spy caught?
				randNum = RandomNumber.Between(1,100);
				if(randNum >= 50)
				{
					result = EspionageResult.SpyCaught;
                    this.foreignCountry.CaptureSpy(this, EspionageAction.SpreadPropaganda, false, foreignCity);
					this.hasSpy = false;
				}
			}

			return result;
		}

		/// <summary>
		/// If successful, Stealing foreign plans will reveal the positions
		/// of all the foreign colonys' military units.
		/// </summary>
        /// <remarks>Unlike other espionage activities, when stealing plans 
        /// from enemy countries, failure always means the spy is caught.</remarks>
		/// <returns></returns>
		public EspionageResult StealPlans()
		{
            if (!this.hasEmbassy)
                throw new InvalidOperationException(ServerResources.EmbassyRequired);
            if (!this.hasSpy)
                throw new InvalidOperationException(ServerResources.SpyRequired);
			int randNum;
			EspionageResult result;

            if (this.foreignCountry.Government.Fallback)
                return EspionageResult.ImmuneToEspionage;
			
			randNum = RandomNumber.Between(1,100);
			if(randNum <= 75)
			{
                //success.  There is still a chance, however, that the
                //spy was caught in the process.
                randNum = RandomNumber.Between(1, 100);
                if (randNum <= 35)
                {                    
                    result = EspionageResult.SuccessWithCapturedSpy;
                    this.foreignCountry.CaptureSpy(this, EspionageAction.StealPlans, true, this.foreignCountry.CapitalCity);
                    this.hasSpy = false;
                }
                else
                {
                    result = EspionageResult.Success;
                }
			}
			else
			{
				//failure.  spy caught.									
				result = EspionageResult.SpyCaught;
                this.foreignCountry.CaptureSpy(this, EspionageAction.StealPlans, false, this.foreignCountry.CapitalCity);
				this.hasSpy = false;					
			}

			return result;
		}

		/// <summary>
		/// Attempt to steal the world map of the foreign colony.
		/// If this is successfull, all the territory the rival 
		/// colony has explored will become visible to the parent 
		/// colony.
		/// </summary>
		/// <returns></returns>
		public EspionageResult StealWorldMap()
		{
            if (!this.hasEmbassy)
                throw new InvalidOperationException(ServerResources.EmbassyRequired);
            if (!this.hasSpy)
                throw new InvalidOperationException(ServerResources.SpyRequired);
            if (this.foreignCountry.Government.Fallback)
                return EspionageResult.ImmuneToEspionage;			
			int randNum;
			EspionageResult result;			
			randNum = RandomNumber.Between(1,100);
			if(randNum <= 75)
			{
				result = EspionageResult.Success;
			}
			else
			{
				//failure.  spy caught?
				randNum = RandomNumber.Between(1,100);
				if(randNum <= 50)
				{
					result = EspionageResult.SpyCaught;
                    this.foreignCountry.CaptureSpy(this, EspionageAction.StealWorldMap, false, this.foreignCountry.CapitalCity);
					this.hasSpy = false;
				}
				else
				{
					result = EspionageResult.Failure;
				}
			}
			return result;
		}

		/// <summary>
		/// Attempts to expose a spy in the embassy.
		/// </summary>
		/// <returns></returns>
		public EspionageResult ExposeSpy()
		{
            if (!this.hasEmbassy)
                throw new InvalidOperationException(ServerResources.EmbassyRequired);

            if (this.foreignCountry.Government.Fallback)
                return EspionageResult.ImmuneToEspionage;

            EspionageResult result = EspionageResult.Failure;
            int randNum = RandomNumber.Between(1, 100);
            if (randNum <= 75)
            {
                //success.  spy captured?  35% chance.
                randNum = RandomNumber.Between(1, 100);
                if (randNum < 35)
                {
                    //spy captured.
                    result = EspionageResult.SuccessWithCapturedSpy;
                    this.foreignCountry.CaptureSpy(this, EspionageAction.ExposeSpy, true, this.foreignCountry.CapitalCity);
                    this.hasSpy = false;
                }
                else
                {
                    result = EspionageResult.Success;
                }
            }
            else
            {
                //failure.  spy captured? 50/50 chance.
                randNum = RandomNumber.Between(1, 100);
                if (randNum < 50)
                {
                    //spy caught
                    result = EspionageResult.SpyCaught;
                    this.foreignCountry.CaptureSpy(this, EspionageAction.ExposeSpy, false, this.foreignCountry.CapitalCity);
                    this.hasSpy = false;
                }
                else
                {
                    result = EspionageResult.Failure;
                }
            }
            return result;
		}

		
		/// <summary>
		/// Occurs when the <i>DiplomaticState</i> property changes.
		/// </summary>
		public event EventHandler DiplomaticStateChanged
		{
			add
			{
				this.diplomaticStateChanged += value;
			}

			remove
			{
				this.DiplomaticStateChanged -= value;
			}
		}

		/// <summary>
		/// Fires the <i>DiplomaticStateChanged</i> event.
		/// </summary>
		protected virtual void OnDiplomaticStateChanged()
		{
			if(this.diplomaticStateChanged != null)
				this.diplomaticStateChanged(this, EventArgs.Empty);
		}

		private void OnDiplomaticAgreementCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if(e.Action == CollectionChangeAction.Add)
			{
				//what type of agreement was just struck?
				DiplomaticAgreement agreement = (DiplomaticAgreement)e.Element;
				if(agreement is MilitaryAlliance)
				{
					//a military alliance agains a 3rd country.  invoke it.
					this.parentCountry.DeclareWar(((MilitaryAlliance)agreement).AllianceVictim);
				}
			}
		}
	}
}
