using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Xml;
using McKnight.Similization.Core;
using System.Drawing;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// A computer controlled country.  These countries will have the AI determine next moves,
	/// barganing decisions, and all game controlled aspects.
	/// </summary>
	public class AICountry : Country
	{
		private Dictionary<Resource, double> resourceNeeds = new Dictionary<Resource, double>();
		private Dictionary<Technology, double> technologyNeeds = new Dictionary<Technology, double>();

		/// <summary>
		/// Initializes a new instance of the <see cref="AICountry"/> class.
		/// </summary>
		public AICountry() : base()
		{
			this.Strategy = StrategyFactory.GetStrategy(this);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AICountry"/> class.
		/// </summary>
		/// <param name="civilization"></param>
		/// <param name="leaderName"></param>
		/// <param name="coordinates"></param>
		public AICountry(Civilization civilization, string leaderName, Point coordinates) :base(civilization, leaderName, coordinates)
		{
			GameRoot root = GameRoot.Instance;
			foreach(Resource resource in root.Ruleset.Resources)			
				this.resourceNeeds.Add(resource, 0d);			
			this.Strategy = StrategyFactory.GetStrategy(this);
		}

		/// <summary>
		/// Takes a turn for the AI Country.
		/// </summary>
		internal override void DoTurn()
		{
			UpdateResourceHashtable();
			DoTurnBasedNegotiations();

			Unit unit;
			for(int i = 0; i < Units.Count; i++)
			{
				if(Units.Count < (i+1))
					return;
				unit = (Unit)this.Units[i];
				unit.DoTurn();
			}

			foreach(AICity city in Cities)
				city.DoTurn();
			this.Strategy = StrategyFactory.GetStrategy(this);
			IncrementScore();
		}

		/// <summary>
		/// Requests an audience with the AI Country.
		/// </summary>
		/// <param name="requester">The country requesting the audience.</param>
		public override void RequestAudience(Country requester)
		{
		}

		/// <summary>
		/// Notifies the <c>AICountry</c> that a unit has invaded their territory.
		/// </summary>
		/// <param name="invader">The <c>Unit</c> that has invaded the territory of 
		/// the <c>AICountry</c>.</param>
		/// <param name="location">The <c>GridCell</c> that was invaded.</param>
		/// <remarks>The <c>AICountry</c> will not always respond to such an invasion, 
		/// especially if the invader has a right of passage treaty with the AI Country.  
		/// However, if the two countries have diplomatic ties, the host country will 
		/// usually issue a warning to the invader.</remarks>
		public void NotifyOfInvasion(Unit invader, GridCell location)
		{
			if(invader == null)
				throw new ArgumentNullException("invader");
			if(location == null)
				throw new ArgumentNullException("location");

			Country parent = invader.ParentCountry;
			DiplomaticTie tie = GetDiplomaticTie(parent);
			if(tie == null)
			{
				parent.RequestAudience(this);
			}
		}

		/// <summary>
		/// Refreshes the internal hashtable of resources for the AI country.
		/// </summary>
		protected virtual void UpdateResourceHashtable()
		{
			//TODO: implement
		}

		/// <summary>
		/// This function looks for negotiation options per turn
		/// for the AI country.  This includes trade proposals, 
		/// threats to foreign colonies, and requests for peace treaties.
		/// </summary>
		protected virtual void DoTurnBasedNegotiations()
		{
			foreach(DiplomaticTie tie in DiplomaticTies)
			{
				switch(tie.DiplomaticState)
				{
					case DiplomaticState.Peace:
						DoPeacetimeNegotiation(tie.ForeignCountry);
						break;
					case DiplomaticState.War:
						DoWartimeNegotiation(tie.ForeignCountry);
						break;
				}
			}
		}

		/// <summary>
		/// Does peacetime negotations with the foreign country passed in.
		/// </summary>
		/// <param name="foreignCountry"></param>
		protected virtual void DoPeacetimeNegotiation(Country foreignCountry)
		{
			if(foreignCountry == null)
				throw new ArgumentNullException("foreignCountry");

			//determine needs	
			double techNeedFactor;
			Resource topNeededResource = null;
			double resourceFactor = 0.0;
			const double threshold = .5d;
			Collection<ITradable> givenItems = null;
			Collection<ITradable> takenItems = null;
		
			int foreignTechs = foreignCountry.AcquiredTechnologies.Count;
			int localTechs = this.AcquiredTechnologies.Count;

			if(localTechs == 0)
			{
				//we don't have _any_ technologies.  We definitely want
				//to bargain for those.
				techNeedFactor = int.MaxValue;
			}
			else
				techNeedFactor = foreignTechs / localTechs;

			foreach(Resource resource in this.resourceNeeds.Keys)
			{
				if((double)this.resourceNeeds[resource] > resourceFactor)
				{
					resourceFactor = (double)this.resourceNeeds[resource];
					topNeededResource = resource;
				}
			}

			if(resourceFactor > techNeedFactor)
			{
				if(resourceFactor > threshold)
				{
                    int v = topNeededResource.CalculateValueForCountry(this);
					givenItems = GetTradeableItems(v,foreignCountry);
					takenItems = new Collection<ITradable>();
					takenItems.Add(topNeededResource);
					foreignCountry.ProposeTrade(givenItems,takenItems, this);
				}
			}
			else
			{
				if(techNeedFactor > threshold)
				{
					
				}
			}
		
		}

		private Collection<ITradable> GetTradeableItems(int tradedValue, Country foreignCountry)
		{
			if(foreignCountry == null)
				throw new ArgumentNullException("foreignCountry");
			DiplomaticTie foreignTie;
			Collection<ITradable> items = new Collection<ITradable>();
			foreach(DiplomaticTie diplomaticTie in DiplomaticTies)
			{
				if(diplomaticTie.ForeignCountry == foreignCountry)
				{
					foreignTie = diplomaticTie;
                    items.Add(new GoldLumpSum(tradedValue));
					break;
				}
			}			
			return items;
		}

		/// <summary>
		/// Does any needed wartime negotiation.  Calling this function 
		/// will not neccessairily result in wartime negotiation, it depends
		/// on country types, country needs, etc...
		/// </summary>
		/// <param name="foreignCountry">The foreign colony to negotiate with</param>
		protected virtual void DoWartimeNegotiation(Country foreignCountry)
		{
			if(foreignCountry == null)
				throw new ArgumentNullException("foreignCountry");

			DiplomaticTie diplomaticTie = null;

			foreach(DiplomaticTie tie in DiplomaticTies)
			{
				if(tie.ForeignCountry == foreignCountry)
				{
					diplomaticTie = tie;
					break;
				}
			}

			if(diplomaticTie == null)
				return;

			if(diplomaticTie.DiplomaticState != DiplomaticState.War)
				return;

			CulturalPerception perception = GetCulturalPerception(foreignCountry);
			
			switch (perception)
			{
				case CulturalPerception.Admirer:
					break;
				case CulturalPerception.Disdainful:
					break;
				case CulturalPerception.Dismissive:
					break;
				case CulturalPerception.Impressed:
					break;
				case CulturalPerception.InAwe:
					break;
				case CulturalPerception.Unimpressed:
					break;
			}
		}

		/// <summary>
		/// Propose a trade with a foreign country.  The foreign country will
		/// examine the items they are giving up, and the items they are 
		/// receiving, and determine a response.
		/// </summary>        
        /// <param name="givenItems"></param>
        /// <param name="takenItems"></param>
        /// <param name="foreignCountry"></param>
		/// <returns></returns>
		public override TradeResponse ProposeTrade(Collection<ITradable> givenItems, Collection<ITradable> takenItems, Country foreignCountry)
		{
			if(takenItems == null)
				throw new ArgumentNullException("takenItems");

			if(givenItems == null)
				throw new ArgumentNullException("givenItems");

			if(foreignCountry == null)
				throw new ArgumentNullException("foreignCountry");

			int givenValue = 0;
			int takenValue = 0;
			TradeResponse retval;

			foreach(ITradable tradeable in givenItems)			
                givenValue += tradeable.CalculateValueForCountry(foreignCountry);			

			foreach(ITradable tradeable in takenItems)
                takenValue += tradeable.CalculateValueForCountry(this);

			if (takenValue >= givenValue)			
				retval = TradeResponse.Accept;			
			else			
				retval = TradeResponse.NeutralDecline;			

			return retval;
		}

        internal override void CaptureSpy(DiplomaticTie diplomaticTie, EspionageAction espionageAction, bool espionageCompleted, City city)
        {
            base.CaptureSpy(diplomaticTie, espionageAction, espionageCompleted, city);

            //find the associated diplomaticTie
            Country enemy = diplomaticTie.ParentCountry;
            DiplomaticTie tie = GetDiplomaticTie(enemy);
            tie.Attitude = Attitude.Furious;

            //we may want to declare war here.  
            double us = Convert.ToDouble(this.PowerFactor);
            double them = Convert.ToDouble(enemy.PowerFactor);

            if (them == 0.0d)
            {
                DeclareWar(enemy);
                return;
            }

            double ratio = us / them;
            if (ratio >= .75d)
                DeclareWar(enemy);
        }

		private AIStrategy strategy;

		/// <summary>
		/// Gets the Strategy that the AI Country is currently using.
		/// </summary>
		internal AIStrategy Strategy
		{
			get { return this.strategy; }
			set
			{
				if(this.strategy == null || this.strategy.GetType() != value.GetType())
				{
					AIStrategy oldStrategy = this.strategy;
					this.strategy = value;
					OnStrategyChanged(new StrategyChangedEventArgs(oldStrategy, value));
				}
			}
		}

		private event EventHandler<StrategyChangedEventArgs> strategyChanged;

		/// <summary>
		/// Occurs when the strategy of the opponent changes.
		/// </summary>
		internal event EventHandler<StrategyChangedEventArgs> StrategyChanged
		{
			add
			{
				this.strategyChanged += value; 
			}

			remove
			{
				this.strategyChanged -= value;
			}
		}

		/// <summary>
		/// Fires the <i>StrategyChanged</i> event.
		/// </summary>
		/// <param name="e"></param>
		private void OnStrategyChanged(StrategyChangedEventArgs e)
		{
			if(this.strategyChanged != null)
			{
				this.strategyChanged(this, e);
			}
		}
	}
}
