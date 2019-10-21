using System;
using System.Collections;
using System.Collections.ObjectModel;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	#region CannotGrow
	/// <summary>
	/// Event Arguments for the <i>CannotGrow</i> event.
	/// </summary>
	public class CannotGrowEventArgs : EventArgs
	{
		private Improvement neededImprovement;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="CannotGrowEventArgs"/> class.
		/// </summary>
		/// <param name="neededImprovement"></param>
		public CannotGrowEventArgs(Improvement neededImprovement)
		{
			this.neededImprovement = neededImprovement;
		}

		/// <summary>
		/// Gets the improvement that is needed by the city
		/// in order to grow beyond its' current size.
		/// </summary>
		public Improvement NeededImprovement
		{
			get { return this.neededImprovement; }
		}
	}	
	#endregion

	#region Combat
	/// <summary>
	/// Event Arguments for the <c>Combat</c> event handler.
	/// </summary>
	public class CombatEventArgs : EventArgs
	{
		private Unit foe;

		/// <summary>
		/// Initializes a new instance of the <c>CombatEventArgs</c> class.
		/// </summary>
		/// <param name="foe"></param>
		public CombatEventArgs(Unit foe)
		{
			this.foe = foe;
		}

		/// <summary>
		/// The unit that is being attacked.
		/// </summary>
		public Unit Foe
		{
			get { return this.foe; }
		}
	}

	#endregion

	#region Defeated
	/// <summary>
	/// Event arguments for the <c>Defeated</c> event of a country.
	/// </summary>
	public class DefeatedEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <c>DefeatedEventArgs</c> class.
		/// </summary>
		/// <param name="conqueror"></param>
		/// <param name="victim"></param>
		public DefeatedEventArgs(Country conqueror, Country victim)
		{
			_conqueror = conqueror;
			_victim = victim;
		}

		private Country _conqueror;
		
		/// <summary>
		/// Gets the <c>Country</c> that defeated the (now) defunct 
		/// civilization.
		/// </summary>
		public Country Conqueror
		{
			get { return _conqueror; }
		}

		private Country _victim;

		/// <summary>
		/// Gets the <c>Country</c> that was defeated.
		/// </summary>
		public Country Victim
		{
			get { return _victim; }
		}
	}

	#endregion

	#region GameEnded

	/// <summary>
	/// Event Arguments for the <c>GameEnded</c> event of the <c>GameRoot</c> class.
	/// </summary>
	public class GameEndedEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <c>GameEndedEventArgs</c> class.
		/// </summary>
		/// <param name="victor"></param>
		/// <param name="typeOfVictory"></param>
		public GameEndedEventArgs(Country victor, VictoryType typeOfVictory)
		{
			_victoryType = typeOfVictory;
			_victor = victor;
		}

		private VictoryType _victoryType;

		/// <summary>
		/// Gets the type of victory that was achieved.
		/// </summary>
		public VictoryType VictoryType
		{
			get { return _victoryType; }
		}

		private Country _victor;

		/// <summary>
		/// Gets the <c>Country</c> that has won the game.
		/// </summary>
		public Country Victor
		{
			get { return _victor; }
		}
	}
	#endregion

	#region TradeProposed

	/// <summary>
	/// Event Args for the <i>TradeProposed</i> event.
	/// </summary>
	public class TradeProposedEventArgs : EventArgs
	{
		private Collection<ITradable> givenItems;
		private Collection<ITradable> takenItems;
		private Country foreignCountry;

		/// <summary>
		/// Initializes a new instance of the <see cref="TradeProposedEventArgs"/> class.
		/// </summary>
		/// <param name="givenItems"></param>
		/// <param name="takenItems"></param>
		/// <param name="foreignCountry"></param>
		public TradeProposedEventArgs(Collection<ITradable> givenItems, Collection<ITradable> takenItems,	Country foreignCountry)
		{
			this.givenItems = givenItems;
			this.takenItems = takenItems;
			this.foreignCountry = foreignCountry;
		}

		/// <summary>
		/// The items the proposer is giving.
		/// </summary>
		public Collection<ITradable> GivenItems
		{
			get { return givenItems; }
		}

		/// <summary>
		/// The items the proposer wants in return.
		/// </summary>
		public Collection<ITradable> TakenItems
		{
			get { return takenItems; }
		}

		/// <summary>
		/// The country the proposer is offering the trade to.
		/// </summary>
		public Country ForeignCountry
		{
			get { return foreignCountry; }
		}
	}
	#endregion

	#region CityCaptured


	/// <summary>
	/// Event Args for the <c>CityCaptured</c> event.
	/// </summary>
	public class CapturedEventArgs : EventArgs
	{
		private CountryBase _invader;
		private CountryBase _loser;
		private int _goldPlundered;

		/// <summary>
		/// Initializes a new instance of the <see cref="CapturedEventArgs"/> class.
		/// </summary>
		/// <param name="invader"></param>
		/// <param name="loser"></param>
		/// <param name="goldPlundered"></param>
		public CapturedEventArgs(CountryBase invader, CountryBase loser, int goldPlundered)
		{
			_invader = invader;
			_loser = loser;
			_goldPlundered = goldPlundered;
		}

		/// <summary>
		/// Gets the country that invaded the city.
		/// </summary>
		public CountryBase Invader
		{
			get { return _invader; }
		}

		/// <summary>
		/// Gets the country that once owned this city.
		/// </summary>
		public CountryBase Loser
		{
			get { return _loser; }
		}

		/// <summary>
		/// Gets the amount of gold plundered from the city.
		/// </summary>
		public int GoldPlundered
		{
			get { return _goldPlundered; }
		}
	}
	#endregion

	#region AudienceRequested


	/// <summary>
	/// Event arguments for the <c>AudienceRequested</c> event.
	/// </summary>
	public class AudienceRequestedEventArgs : System.EventArgs
	{
        private Country requester;
        private Collection<ITradable> givenItems;
        private Collection<ITradable> takenItems;

		/// <summary>
		/// Initializes a new instance of the <c>AudienceRequestedEventArgs</c> class.
		/// </summary>
		/// <param name="requester"></param>
		/// <param name="givenItems"></param>
		/// <param name="takenItems"></param>
		public AudienceRequestedEventArgs(Country requester, Collection<ITradable> givenItems, Collection<ITradable> takenItems)
		{
            if (requester == null)
                throw new ArgumentNullException("requester");
            if (givenItems == null)
                throw new ArgumentNullException("givenItems");
            if (takenItems == null)
                throw new ArgumentNullException("takenItems");
			this.requester = requester;			
			this.givenItems = givenItems;					
			this.takenItems = takenItems;			
		}

		/// <summary>
		/// The country requesting an audience.
		/// </summary>
		public Country Requester
		{
			get { return this.requester; }
		}
		
		/// <summary>
		/// An collection of tradeable items that the foreign country is offering.
		/// </summary>
		public Collection<ITradable> GivenItems
		{
			get { return this.givenItems; }
		}
		
		/// <summary>
		/// An collection of tradeable items that the foreign country wants from the 
		/// player.
		/// </summary>
		public Collection<ITradable> TakenItems
		{
			get { return this.takenItems; }
		}
	}
	#endregion

	#region CityStatusChanged


	/// <summary>
	/// Event arguments for the <c>CityStatusChanged</c> event.
	/// </summary>
	public class CityStatusEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <c>CityStatusEventArgs</c> class.
		/// </summary>
		/// <param name="newStatus"></param>
		/// <param name="previousStatus"></param>
		public CityStatusEventArgs(CityStatus newStatus, CityStatus previousStatus)
		{
			_status = newStatus; 
			_previousStatus = previousStatus;
		}

		private CityStatus _status;

		/// <summary>
		/// The new status of the city.
		/// </summary>
		public CityStatus CityStatus
		{
			get { return _status; }
		}

		private CityStatus _previousStatus; 

		/// <summary>
		/// The previous status of the city.
		/// </summary>
		public CityStatus PreviousStatus
		{
			get { return _previousStatus; }
		}
	}
	#endregion

	#region PollutionCreated
	/// <summary>
	/// Event Arguments for the <c>PollutionCreated</c> event of a City.
	/// </summary>
	public class PollutionEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <c>PollutionEventArgs</c> class.
		/// </summary>
		/// <param name="cell"></param>
		public PollutionEventArgs(GridCell cell)
		{
			_cell = cell;
		}

		private GridCell _cell;

		/// <summary>
		/// Gets a reference to the cell that now has the pollution.
		/// </summary>
		public GridCell GridCell
		{
			get { return _cell; }
		}
	}
	#endregion

	#region NewGovernmentAvailable
	/// <summary>
	/// Event arguments for the <c>NewGovernmentAvailable</c> event of a <c>Country</c> object.
	/// </summary>
	public class GovernmentAvailableEventArgs : EventArgs
	{
		private Government government;

		/// <summary>
		/// Initializes a new instance of the <c>GovernmentAvailableEventArgs</c> class.
		/// </summary>
		/// <param name="government"></param>
		public GovernmentAvailableEventArgs(Government government)
		{
			this.government = government;
		}
		/// <summary>
		/// Gets the <c>Government</c> that is now available.
		/// </summary>
		public Government Government
		{
			get { return this.government; }
		}
	}
	#endregion

	#region RevolutionEnded
	/// <summary>
	/// Event Arguments for the <c>RevolutionEnded</c> event.
	/// </summary>
	public class RevolutionEndedEventArgs : EventArgs
	{

		/// <summary>
		/// Initializes a new instance of the <c>RevolutionEndedEventArgs</c> class.
		/// </summary>
		/// <param name="nextGovernment"></param>
		public RevolutionEndedEventArgs(Government nextGovernment)
		{
			this.nextGovernment = nextGovernment;
		}

		private Government nextGovernment;

		/// <summary>
		/// Gets the recommended government to switch to.
		/// </summary>
		/// <remarks>For revolutions started from government changes, this will be the
		/// government the player tried to change to that resulted in the revolution.</remarks>
		public Government NextGovernment
		{
			get { return this.nextGovernment; }
		}
	}
	#endregion

	#region StrategyChanged	
	/// <summary>
	/// Event Args for the <c>StrategyChanged</c> event of an AI Country.
	/// </summary>
	internal class StrategyChangedEventArgs : EventArgs
	{
		private AIStrategy _oldStrategy;
		private AIStrategy _newStrategy;

		/// <summary>
		/// Initializes a new instance of the <c>StrategyChangedEventArgs</c> class.
		/// </summary>
		/// <param name="oldStrategy"></param>
		/// <param name="newStrategy"></param>
		internal StrategyChangedEventArgs(AIStrategy oldStrategy, AIStrategy newStrategy)
		{
			_oldStrategy = oldStrategy;
			_newStrategy = newStrategy;
		}

		/// <summary>
		/// Gets the old strategy of the country.
		/// </summary>
		internal AIStrategy OldStrategy
		{
			get { return _oldStrategy; }
		}

		/// <summary>
		/// Gets the new strategy of the country.
		/// </summary>
		internal AIStrategy NewStrategy
		{
			get { return _newStrategy; }
		}
	}
	#endregion

	#region StatusChanged
	/// <summary>
	/// Event Args for the StatusChanged Event Handler.  This class
	/// exposes properties for the status message, and the percent 
	/// complete for the operation giving the status.
	/// </summary>
	public class StatusChangedEventArgs : EventArgs
	{
        private string message;
		private int percentDone;

		/// <summary>
		/// Intializes a new instance of the <c>StatusChangedEventArgs</c> class.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="percentDone"></param>
		public StatusChangedEventArgs(string message, int percentDone)
		{
            this.message = message;
			this.percentDone = percentDone;
		}

		/// <summary>
		/// The overall percent complete of the operation this status change is
		/// a part of.
		/// </summary>
		public int PercentDone
		{
			get { return this.percentDone; }
		}

        /// <summary>
        /// The message associated with the new status.
        /// </summary>
        public string Message
        {
            get { return this.message; }
        }
	}
	#endregion

	#region VillageEncountered
	/// <summary>
	/// Event Arguments for the <c>VillageEncountered</c> event of a unit.
	/// </summary>
	public class VillageEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <c>VillageEventArgs</c> class.
		/// </summary>
		/// <param name="villageEncountered"></param>
		/// <param name="goody"></param>
		public VillageEventArgs(Village villageEncountered, VillageGoody goody)
		{
			_village = villageEncountered;
			_goody = goody;
		}

		private Village _village;

		/// <summary>
		/// Gets the village encountered.
		/// </summary>
		public Village Village
		{
			get { return _village; }
		}

		private VillageGoody _goody; 

		/// <summary>
		/// Gets the <c>VillageGoody</c> that was found in the village.
		/// </summary>
		public VillageGoody Goody
		{
			get { return _goody; }
		}
	}
	#endregion

}
