using System;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// This class contains constants that are keys in the name/value pair of
	/// resource files.  We use this class not so much to hide the string keys, 
	/// but to give the programmer a nice intellisense list of possible string resources.
	/// This also reduces the possibility for typos.
	/// </summary>
	public sealed class StringKey
	{

		private StringKey() {}


		/// <summary>
		/// The title of the game.
		/// </summary>
		public const string GameTitle = "gameTitle";

		/// <summary>
		/// Server startup
		/// </summary>
		public const string ServerStartup = "serverStartup";

		/// <summary>
		/// Completed server startup
		/// </summary>
		public const string ServerStartupComplete = "serverStartupComplete";

		/// <summary>
		/// Game Started
		/// </summary>
		public const string GameStart = "gameStart";

		/// <summary>
		/// Unit Killed
		/// </summary>
		public const string UnitKilled = "unitDestroyed";

		/// <summary>
		/// New player registered with the server.
		/// </summary>
		public const string PlayerRegistered = "playerRegistered";

		/// <summary>
		/// Expansionist
		/// </summary>
		public const string ExpansionistAttribute = "expansionistAttribute";

		/// <summary>
		/// Commercial
		/// </summary>
		public const string CommercialAttribute = "commercialAttribute";

		/// <summary>
		/// Religious
		/// </summary>
		public const string ReligiousAttribute = "religiousAttribute";

		/// <summary>
		/// Industrious
		/// </summary>
		public const string IndustriousAttribute = "industriousAttribute";

		/// <summary>
		/// Militaristic
		/// </summary>
		public const string MilitaristicAttribute = "militaristicAttribute";

		/// <summary>
		/// Scientific
		/// </summary>
		public const string ScientificAttribute = "scientificAttribute";

		/// <summary>
		/// City Captured key
		/// </summary>
		public const string CityCaptured = "cityInvaded";

		/// <summary>
		/// City Lost to invasion
		/// </summary>
		public const string CityLostToInvasion = "cityLostToInvasion";

		/// <summary>
		/// Cultural Influence Expanded Key
		/// </summary>
		public const string CulturalInfluenceExpanded = "culturalInfluenceExpanded";

		/// <summary>
		/// City Disorder Key
		/// </summary>
		public const string CityDisorder = "cityDisorder";

		/// <summary>
		/// Order Restored Key
		/// </summary>
		public const string OrderRestored = "orderRestored";

		/// <summary>
		/// Cannot Grow Key
		/// </summary>
		public const string CannotGrow = "cannotGrow";

		/// <summary>
		/// Improvement Built Key
		/// </summary>
		public const string ImprovementBuilt = "improvementBuilt";

		/// <summary>
		/// Technology Acquired Key
		/// </summary>
		public const string TechnologyAcquired = "technologyAcquired";

		/// <summary>
		/// Starvation key
		/// </summary>
		public const string Starved = "starved";

		/// <summary>
		/// Unit acquired from village
		/// </summary>
		public const string VillageUnit = "villageUnit";

		/// <summary>
		/// Gold acquired from village
		/// </summary>
		public const string VillageGold = "villageGold";

		/// <summary>
		/// village joins as a new city
		/// </summary>
		public const string VillageCity = "villageCity";

		/// <summary>
		/// Village is Deserted
		/// </summary>
		public const string VillageDeserted = "villageDeserted";

		/// <summary>
		/// New settler from village
		/// </summary>
		public const string VillageSettler = "villageSettler";

		/// <summary>
		/// Attacked from a barbarian in the village
		/// </summary>
		public const string VillageBarbarian = "villageBarbarian";

		/// <summary>
		/// Map acquired from the village
		/// </summary>
		public const string VillageMap = "villageMap";

		/// <summary>
		/// Technology acquired from the village.
		/// </summary>
		public const string VillageTechnology = "villageTechnology";

		/// <summary>
		/// Scientists need direction on what to research next.
		/// </summary>
		public const string TechDirectionNeeded = "techResearchNeeded";

		/// <summary>
		/// The human player destroys another civilization
		/// </summary>
		public const string PlayerDestroysCivilization = "playerDestroysFoe";

		/// <summary>
		/// A foreign civilization destroys another foreign civilization.
		/// </summary>
		public const string ForeignCivilizationDestroyed = "foreignCivDestroyed";

		/// <summary>
		/// The human player has been defeated.
		/// </summary>
		public const string PlayerDestroyed = "playerDefeated";

		/// <summary>
		/// The human player has won a military victory.
		/// </summary>
		public const string MilitaryVictory = "militaryVictory";

		/// <summary>
		/// We Love The King day celebrated in a city.
		/// </summary>
		public const string WeLoveTheKing = "weLoveTheKing";

		/// <summary>
		/// We Love the King celebration has ended in a city.
		/// </summary>
		public const string WeLoveTheKingEnd = "weLoveTheKingEnd";

		/// <summary>
		/// Message shown to the user telling them their turn is over.
		/// </summary>
		public const string TurnFinished = "turnFinished";

		/// <summary>
		/// Message telling the user that the game is ready for user input.
		/// </summary>
		public const string Ready = "ready";

		/// <summary>
		/// Message telling the user what technology they are currently researching.
		/// </summary>
		public const string Researching = "researching";

		/// <summary>
		/// Message telling the user that they are not currently researching any new technologies.
		/// </summary>
		public const string NoResearch = "noResearch";

		/// <summary>
		/// Message telling the user how much gold they currently have.
		/// </summary>
		public const string GoldAmount = "goldAmount";

		/// <summary>
		/// Message telling the user there is not currently an active unit to play.
		/// </summary>
		public const string NoActiveUnit = "noActiveUnit";

		/// <summary>
		/// Formats the current year in the B.C. format.
		/// </summary>
		public const string NegativeYear = "negativeYear";

		/// <summary>
		/// Formats the current year in the A.D. format.
		/// </summary>
		public const string PositiveYear = "positiveYear";

		/// <summary>
		/// Message telling the user they have entered a new Era.
		/// </summary>
		public const string NextEra = "nextEra";

		/// <summary>
		/// Message telling the user that there is a new type of government available to them 
		/// in the game.  Asks if they would like to switch to this new form of government.
		/// </summary>
		public const string NewGovernmentAvailable = "newGovernmentAvailable";

		/// <summary>
		/// Message telling the user that the government is descending into anarchy from 
		/// the government change.
		/// </summary>
		public const string Revolution = "revolution";

		/// <summary>
		/// Message telling the user to select a new government type.
		/// </summary>
		public const string GovernmentSelection = "governmentSelection";

		/// <summary>
		/// Message telling the user that population growth is needed in a city before
		///  the unit can be finished.
		/// </summary>
		public const string PopulationGrowthNeeded = "populationGrowthNeeded";

		/// <summary>
		/// Message asking the user to confirm that they want to disband a unit.
		/// </summary>
		public const string DisbandConfirmation = "disbandConfirmation";
		
		/// <summary>
		/// Message asking the user if they will accept a request for an audience with 
		/// a foreign leader.
		/// </summary>
		public const string AudienceRequest = "audienceRequest";

		/// <summary>
		/// The text describing a foreign country on the diplomacy screen.
		/// </summary>
		public const string DiplomacyCountryHeader = "diplomacyCountryHeader";

		/// <summary>
		/// The text for a Gracious Attitude.
		/// </summary>
		public const string GraciousAttitude = "graciousAttitude";

		/// <summary>
		/// The text for an Annoyed Attitude.
		/// </summary>
		public const string AnnoyedAttitude = "annoyedAttitude";

		/// <summary>
		/// The text for a Polite Attitude.
		/// </summary>
		public const string PoliteAttitude = "politeAttitude";

		/// <summary>
		/// The text for a Cautious Attitude.
		/// </summary>
		public const string CautiousAttitude = "cautiousAttitude";

		/// <summary>
		/// The text for a Furious Attitude.
		/// </summary>
		public const string FuriousAttitude = "furiousAttitude";

		/// <summary>
		/// The text for telling the player that the deal will probably be acceptable.
		/// </summary>
		public const string AdviseOfDealAcceptance = "adviseDealAccept";

		/// <summary>
		/// The text for telling the user that while the foreign leader will probably reject the deal, they are 
		/// close to accepting.
		/// </summary>
		public const string AdviseOfDealRejectionWeak = "adviseDealRejectWeak";

		/// <summary>
		/// The text for telling the user the deal will be rejected.
		/// </summary>
		public const string AdviseOfDealRejectionNeutral = "adviseDealRejectNeutral";

		/// <summary>
		/// The text for telling the user the foreign country will probably be insulted by the deal.
		/// </summary>
		public const string AdviseOfDealRejectionStrong = "adviseDealRejectStrong";

		/// <summary>
		/// The text for telling the user there is no way the deal will be accepted.
		/// </summary>
		public const string AdviseOfDealRejectTotal = "adviseDealRejectTotal";

		/// <summary>
		/// The text for a Veteran Unit.
		/// </summary>
		public const string RankVeteran = "rankVeteran";

		/// <summary>
		/// The text for an Elite Unit.
		/// </summary>
		public const string RankElite = "rankElite";

		/// <summary>
		/// The text for a Regular Unit.
		/// </summary>
		public const string RankRegular = "rankRegular";

		/// <summary>
		/// The text for a Conscript Unit.
		/// </summary>
		public const string RankConscript = "rankConscript";

		/// <summary>
		/// The text that is displayed above the unit lists on the military advisor screen.
		/// </summary>
		public const string MilitaryAdvisorCountryHeader = "militaryAdvisorCountryHeader";

		/// <summary>
		/// The text displaying an AD year.
		/// </summary>
		public const string YearAD = "year_AD";

		/// <summary>
		/// The text displaying a BC year.
		/// </summary>
		public const string YearBC = "year_BC";


		/// <summary>
		/// Text telling the user how many more turns are required to complete the next improvement.
		/// </summary>
		public const string TurnsUntilComplete = "turnsUntilComplete";
	}
	
}
