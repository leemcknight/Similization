using System;
using System.Collections.Generic;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// Helper class for getting string keys of different diplomatic actions.
	/// </summary>
	public static class DiplomacyStringKey
	{	
		/// <summary>
		/// String Keys for Exiting negotiations with a foreign 
		/// country.
		/// </summary>
		public static string[] GetExitDiplomacyStrings()
		{
			
			return new string[] {
									"exitDiplomacy1",
									"exitDiplomacy2",
									"exitDiplomacy3"
								};			
		}

		/// <summary>
		/// String Keys for Declaring War on a foreign country.
		/// </summary>
		public static string[] GetDeclareWarStrings()
		{			
			return new string[] {
									"declareWar1",
									"declareWar2",
									"declareWar3"
								};
			
		}

		/// <summary>
		/// String Keys for Auto-Retreat of invading troops.
		/// </summary>        
		public static string[] GetAutoRetreatTroopsStrings()
		{			
				return new string[] {
										"autoRetreatTroops1",
										"autoRetreatTroops2",
										"autoRetreatTroops3"
									};			
		}

		/// <summary>
		/// String Keys for two civilizations trading their world maps
		/// with each other.
		/// </summary>
		public static string[] GetTradeWorldMapsStrings()
		{
			
			return new string[] {
									"tradeWorldMaps1",
									"tradeWorldMaps2",
									"tradeWorldMaps3"
								};
		
		}

		/// <summary>
		/// String Keys for starting the trading process with a foreign country.
		/// </summary>
		public static string[] GetProposeNegotiationStrings()
		{
			
			return new string[] {
									"proposeNegotiation1",
									"proposeNegotiation2",
									"proposeNegotiation3"
								};
		
		}

		/// <summary>
		/// String Keys for quitting the current negotation and/or trade proposals.
		/// </summary>
		public static string[] GetBackOutFromNegotiationStrings()
		{			
			return new string[] {
									"backoutFromNegotiation1",
									"backoutFromNegotiation2",
									"backoutFromNegotiation3"
								};
			
		}


		/// <summary>
		/// String Keys for passively retreating your troops.
		/// </summary>
		public static string[] GetPassiveRetreatTroopsStrings()
		{			
			return new string[] {
									"passiveRetreatTroops1",
									"passiveRetreatTroops2",
									"passiveRetreatTroops3"
								};			
		}


		/// <summary>
		/// String Keys for refusing the give the requested tribute to 
		/// the foreign government.
		/// </summary>
		public static string[] GetRefuseTributeStrings()
		{			
			return new string[] {
									"refuseTribute1",
									"refuseTribute2",
									"refuseTribute3"
								};			
		}

		/// <summary>
		/// String Keys for Giving in to the demands of a foreign country.
		/// </summary>
		public static string[] GetGiveTributeStrings()
		{			
			return new string[] {
									"giveTribute1",
									"giveTribute2",
									"giveTribute3"
								};			
		}

		/// <summary>
		/// String Keys for threatening war on the foreign country unless they 
		/// remove their troops.
		/// </summary>
		public static string[] GetThreatenWarForBorderInvasionStrings()
		{			
			return new string[] {
									"threatenWarForBorderInvasion"
								};			
		}

		/// <summary>
		/// String Keys for warning the foreign country that their troops must 
		/// be withdrawn.
		/// </summary>
		public static string[] GetWarningForBorderInvasionStrings()
		{			
			return new string[] {
									"warningForBorderInvasion1",
									"warningForBorderInvasion2",
									"warningForBorderInvasion3"
								};			
		}


		/// <summary>
		/// String Keys for Offering a Right of Passage Treaty to the foreign country.
		/// </summary>
		public static string[] GetOfferRightOfPassageStrings()
		{			
			return new string[] {
									"offerRightOfPassage1",
									"offerRightOfPassage2",
									"offerRightOfPassage3"
								};			
		}


		/// <summary>
		/// String Keys for offering a peace treaty while at war with a foreign country.
		/// </summary>
		public static string[] GetOfferPeaceTreatyDuringWarStrings()
		{
			
			return new string[] {
									"offerPeaceTreatyDuringWar1",
									"offerPeaceTreatyDuringWar2",
									"offerPeaceTreatyDuringWar3"
								};
			
		}


		/// <summary>
		/// String Keys for offering a peace treat to a foreign country while the human 
		/// player is about to capture a specific foreign city.
		/// </summary>
		public static string[] GetOfferPeaceTreatyCityInvasionStrings()
		{
			
			return new string[] {
									"offerPeaceTreatyCityInvasion1",
									"offerPeaceTreatyCityInvasion2",
									"offerPeaceTreatyCityInvasion3"
								};
			
		}


		/// <summary>
		/// String Keys for asking the foreign country for a loan of gold.
		/// </summary>
		public static string[] GetAskForLoanStrings()
		{			
			return new string[] {
									"askForLoan"
								};
			
		}

		/// <summary>
		/// String Keys for offering a trade to the foreign leader.
		/// </summary>
		public static string[] GetUserOfferStrings()
		{			
			return new string[] {
									"userOffer1",
									"userOffer2",
									"userOffer3"
								};			
		}


		/// <summary>
		/// String Keys for offering a gift to the foreign leader.
		/// </summary>
		public static string[] GetUserGiftStrings()
		{			
			return new string[] {
									"userGift1",
									"userGift2",
									"userGift3"
								};			
		}


		/// <summary>
		/// String Keys for demanding a tribute from the foreign country.
		/// </summary>
		public static string[] GetDemandTributeStrings()
		{			
			return new string[] {
									"demandTribute1",
									"demandTribute2",
									"demandTribute3"
								};			
		}


		/// <summary>
		/// String Keys for asking the foreign leader what they want in exchange 
		/// for a certain item.
		/// </summary>
		public static string[] GetAskForCounterofferStrings()
		{			
			return new string[] {
									"askForCounterOffer1",
									"askForCounterOffer2",
									"askForCounterOffer3"
								};
			
		}


		/// <summary>
		/// String Keys for asking the foreign leader what they will exchange for 
		/// a particular item the player is willing to give in trade.
		/// </summary>
		public static string[] GetAskForExchangeStrings()
		{
			
			return new string[] {
									"askForExchange1",
									"askForExchange2",
									"askForExchange3"
								};
		
		}
	}
}
