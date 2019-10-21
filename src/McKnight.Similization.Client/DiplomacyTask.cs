using System;
using McKnight.Similization.Core;

namespace McKnight.Similization.Client
{

	/// <summary>
	/// Inteface representing a physical link a user can click on to invoke a task.
	/// </summary>
	public interface IDiplomacyTaskLink
	{
		/// <summary>
		/// The text that will be displayed to the user in the link.
		/// </summary>
		string LinkText { get; set; }

		/// <summary>
		/// The <see cref="Command"/> object to invoke when the user clicks the link.
		/// </summary>
		Command DiplomacyCommand { get; set; }
	}

	/// <summary>
	/// Different tasks that can be performed whilst in diplomacy.
	/// </summary>
	public enum DiplomacyTask
	{
		/// <summary>
		/// Quit the current round of diplomacy
		/// </summary>
		ExitDiplomacy,

		/// <summary>
		/// Declare War on the foreign country
		/// </summary>
		DeclareWar,

		/// <summary>
		/// Apologize for getting too close with troops.  This task 
		/// will result in the auto-withdrawl of the troops.
		/// </summary>
		AutoRetreatTroops,

		/// <summary>
		/// Promise to remove your troops from the foreign territory as 
		/// soon as possible.
		/// </summary>
		PassiveRetreatTroops,

		/// <summary>
		/// Refuse to give the tribute that was requested by the foreign 
		/// country.
		/// </summary>
		RefuseTribute,

		/// <summary>
		/// Agree to give the foreign country the requested tribute.
		/// </summary>
		GiveTribute,

		/// <summary>
		/// Tell the foreign country to remove their troops with the 
		/// threat of war.
		/// </summary>
		ThreatenWarForBorderInvasion,

		/// <summary>
		/// Tell the foreign country that they have troops within your 
		/// borders.
		/// </summary>
		WarningForBorderInvasion,

		/// <summary>
		/// Offer a right of passage agreement with the foreign country.
		/// </summary>
		OfferRightOfPassage,

		/// <summary>
		/// Offer a peace treaty while at war with the foreign country.
		/// </summary>
		OfferPeaceTreatyDuringWar,

		/// <summary>
		/// Offer a peace treaty under the threat of capturing a specific city.
		/// </summary>
		OfferPeaceTreatyCityInvasion,

		/// <summary>
		/// Offer a peace treaty under the threat of a larger invasion.
		/// </summary>
		OfferPeaceTreatyGeneralInvasion,

		/// <summary>
		/// Offer to trade world maps.
		/// </summary>
		TradeWorldMaps,

		/// <summary>
		/// Ask the foreign country for a loan of gold.
		/// </summary>
		AskForLoan,

		/// <summary>
		/// Propose a negotiation
		/// </summary>
		ProposeNegotiation,

		/// <summary>
		/// Back out of the diplomacy.
		/// </summary>
		BackOutFromNegotiation,

		/// <summary>
		/// Accept the proposal (trade, treaty, etc...) that was proposed 
		/// by the foreign country.
		/// </summary>
		AcceptForeignProposal,

		/// <summary>
		/// Reject the proposal that was made by the foreign country.
		/// </summary>
		RejectForeignProposal,


		/// <summary>
		/// Offer a counter proposal to the one made by the foreign country.
		/// </summary>
		Counterproposal,

		/// <summary>
		/// Present the an even-up trade to the foreign leader.  Even-up trades are 
		/// trades where both sides give something up.
		/// </summary>
		PresentEvenUpTrade,

		/// <summary>
		/// Present the trade to the foreign leader in the form of a gift.  
		/// </summary>
		PresentGiftTrade,

		/// <summary>
		/// Present the trade to the foreign leader as a threat.  In these trades, 
		/// the user is asking the opponent to give something up with nothing in 
		/// return.
		/// </summary>
		DemandTribute,

		/// <summary>
		/// Ask the foreign leader to tell us what they want in exchange for an item we 
		/// want from them.
		/// </summary>
		AskForCounter,

		/// <summary>
		/// Ask the foreign leader if there is anything they will give us in exchange for 
		/// a particular item we are willing to give up.
		/// </summary>
		AskForExchange
	}
}
