using System;
using System.Resources;
using McKnight.Similization.Server;
using System.Collections.ObjectModel;
using McKnight.Similization.Core;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// Contains methods to help with the diplomacy process.
	/// </summary>
	public static class DiplomacyHelper
	{
		private static ResourceManager _resMgr;
		
		/// <summary>
		/// Gets a culture-specific string appropriate for the given diplomatic 
		/// task.
		/// </summary>
		/// <param name="task">The <c>DiplomacyTask</c> the description is for.</param>
		/// <param name="tie">The <c>DiplomaticTie</c> between the two negotiating parties.</param>
		/// <returns>The description of the task.</returns>
		/// <remarks>This will return a random string for the specified task.  Most 
		/// tasks have several different strings approprate to the task to make the game 
		/// seem less repetitive.</remarks>
		public static string GetTaskString(DiplomacyTask task, DiplomaticTie tie)
		{
			if(tie == null)
				throw new ArgumentNullException("tie");

			if(_resMgr == null)
			{
				InitializeResourceManager();
			}

			string[] taskTexts = null;

			switch(task)
			{
				case DiplomacyTask.ExitDiplomacy:
					taskTexts = DiplomacyStringKey.GetExitDiplomacyStrings();
					break;
				case DiplomacyTask.DeclareWar:
					taskTexts = DiplomacyStringKey.GetDeclareWarStrings();
					break;
				case DiplomacyTask.AutoRetreatTroops:
					taskTexts = DiplomacyStringKey.GetAutoRetreatTroopsStrings();
					break;
				case DiplomacyTask.ProposeNegotiation:
					taskTexts = DiplomacyStringKey.GetProposeNegotiationStrings();
					break;
				case DiplomacyTask.TradeWorldMaps:
					taskTexts = DiplomacyStringKey.GetTradeWorldMapsStrings();
					break;
				case DiplomacyTask.BackOutFromNegotiation:
					taskTexts = DiplomacyStringKey.GetBackOutFromNegotiationStrings();
					break;
				case DiplomacyTask.PassiveRetreatTroops:
					taskTexts = DiplomacyStringKey.GetPassiveRetreatTroopsStrings();
					break;
				case DiplomacyTask.RefuseTribute:
					taskTexts = DiplomacyStringKey.GetRefuseTributeStrings();
					break;
				case DiplomacyTask.GiveTribute:
					taskTexts = DiplomacyStringKey.GetGiveTributeStrings();
					break;
				case DiplomacyTask.ThreatenWarForBorderInvasion:
					taskTexts = DiplomacyStringKey.GetThreatenWarForBorderInvasionStrings();
					break;
				case DiplomacyTask.WarningForBorderInvasion:
					taskTexts = DiplomacyStringKey.GetWarningForBorderInvasionStrings();
					break;
				case DiplomacyTask.OfferRightOfPassage:
					taskTexts = DiplomacyStringKey.GetOfferRightOfPassageStrings();
					break;
				case DiplomacyTask.OfferPeaceTreatyDuringWar:
					taskTexts = DiplomacyStringKey.GetOfferPeaceTreatyDuringWarStrings();
					break;
				case DiplomacyTask.OfferPeaceTreatyCityInvasion:
					taskTexts = DiplomacyStringKey.GetOfferPeaceTreatyCityInvasionStrings();
					break;
				case DiplomacyTask.AskForLoan:
					taskTexts = DiplomacyStringKey.GetAskForLoanStrings();
					break;
				case DiplomacyTask.PresentEvenUpTrade:
					taskTexts = DiplomacyStringKey.GetUserOfferStrings();
					break;
				case DiplomacyTask.PresentGiftTrade:
					taskTexts = DiplomacyStringKey.GetUserGiftStrings();
					break;
				case DiplomacyTask.DemandTribute:
					taskTexts = DiplomacyStringKey.GetDemandTributeStrings();
					break;
				case DiplomacyTask.AskForCounter:
					taskTexts = DiplomacyStringKey.GetAskForCounterofferStrings();
					break;
				case DiplomacyTask.AskForExchange:
					taskTexts = DiplomacyStringKey.GetAskForExchangeStrings();
					break;
					
			}

			int idx = RandomNumber.GetRandomNumber(taskTexts.GetUpperBound(0));
			string phrase =  GetString(taskTexts[idx]);

			phrase = phrase.Replace("$LEADER0", tie.ForeignCountry.LeaderName);

			return phrase;
		}

		/// <summary>
		/// Gets the text that your advisor will tell the player when they believe the response 
		/// to the trade proposal will be that which is passed in.
		/// </summary>
		/// <param name="response">The response to get the advisor text for.</param>
		/// <returns>The text for the advisors' phrase telling you of the probable response from 
		/// the foreign country to the proposed trade.</returns>
		public static string GetProbableTradeResponseString(TradeResponse response)
		{
			string text = string.Empty;

			switch(response)
			{
				case TradeResponse.Accept:
					text = ClientResources.GetString(StringKey.AdviseOfDealAcceptance);
					break;
				case TradeResponse.NeutralDecline:
					text = ClientResources.GetString(StringKey.AdviseOfDealRejectionNeutral);
					break;
				case TradeResponse.StrongDecline:
					text = ClientResources.GetString(StringKey.AdviseOfDealRejectionStrong);
					break;
				case TradeResponse.TotalDecline:
					text = ClientResources.GetString(StringKey.AdviseOfDealRejectTotal);
					break;
				case TradeResponse.WeakDecline:
					text = ClientResources.GetString(StringKey.AdviseOfDealRejectionWeak);
					break;
			}

			return text;
		}

		/// <summary>
		/// Gets a culture-specific string from the resource file with the specified key.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetString(string key)
		{
			InitializeResourceManager();
			return _resMgr.GetString(key);
		}

		private static void InitializeResourceManager()
		{
			if(_resMgr == null)
			{
				_resMgr = new ResourceManager(
					"McKnight.Similization.Client.DiplomacyTaskStrings", 
					typeof(ClientApplication).Assembly);
			}
		}


		/// <summary>
		/// Gets a command object for the specified <c>DiplomacyTask</c>.
		/// </summary>
		/// <param name="task">The task to get the command for.</param>
		/// <param name="diplomacyControl">The control containing the tasks.</param>
		/// <returns>A reference to a <c>Command</c> object that can be invoked 
		/// when the task is chosen.  For more information, see <see cref="Command"/>.</returns>
		public static DiplomacyCommand GetTaskCommand(DiplomacyTask task, IDiplomacyControl diplomacyControl)
		{
			DiplomacyCommand command = null;
			switch(task)
			{
				case DiplomacyTask.DeclareWar:
					command = new DeclareWarCommand();
					break;
				case DiplomacyTask.ExitDiplomacy:
					command = new ExitNegotiationsCommand();
					break;
				case DiplomacyTask.AutoRetreatTroops:
					command = new AutoRetreatTroopsCommand();
					break;
				case DiplomacyTask.ProposeNegotiation:
					command = new ProposeNegotiationCommand();
					break;
				case DiplomacyTask.TradeWorldMaps:
					command = new TradeWorldMapsCommand();
					break;
				case DiplomacyTask.BackOutFromNegotiation:
					command = new BackOutFromNegotiationCommand();
					break;
				case DiplomacyTask.PassiveRetreatTroops:
					command = new PassiveRetreatTroopsCommand();
					break;
				case DiplomacyTask.RefuseTribute:
					command = new RefuseTributeCommand();
					break;
				case DiplomacyTask.GiveTribute:
					command = new GiveTributeCommand();
					break;
				case DiplomacyTask.ThreatenWarForBorderInvasion:
					command = new ThreatenWarForBorderInvasionCommand();
					break;
				case DiplomacyTask.WarningForBorderInvasion:
					command = new WarningForBorderInvasionCommand();
					break;
				case DiplomacyTask.OfferRightOfPassage:
					command = new OfferRightOfPassageCommand();
					break;
				case DiplomacyTask.OfferPeaceTreatyDuringWar:
					command = new OfferPeaceTreatyDuringWarCommand();
					break;
				case DiplomacyTask.OfferPeaceTreatyCityInvasion:
					command = new OfferPeaceTreatyDuringWarCommand();
					break;
				case DiplomacyTask.OfferPeaceTreatyGeneralInvasion:
					command = new OfferPeaceTreatyDuringWarCommand();
					break;
				case DiplomacyTask.AskForLoan:
					command = new RequestLoanCommand();
					break;
				case DiplomacyTask.PresentEvenUpTrade:
					command = new OfferEvenUpTradeCommand();
					break;
				case DiplomacyTask.PresentGiftTrade:
					command = new OfferGiftCommand();
					break;
				case DiplomacyTask.DemandTribute:
					command = new DemandTributeCommand();
					break;
				case DiplomacyTask.AskForCounter:
					command = new AskForCounterofferCommand();
					break;
				case DiplomacyTask.AskForExchange:
					command = new AskForExchangeCommand();
					break;
			}

			command.DiplomacyControl = diplomacyControl;

			return command;
		}

		/// <summary>
		/// Gets an array of tasks appropriate for the human player to choose from when 
		/// negotiating with a particular opponent.
		/// </summary>
		/// <param name="tie"></param>
		/// <param name="givenItems"></param>
		/// <param name="takenItems"></param>
		/// <returns></returns>
		public static DiplomacyTask[] GetDiplomacyTasks(DiplomaticTie tie, Collection<ITradable> givenItems, Collection<ITradable> takenItems)
		{
            if (tie == null)
                throw new ArgumentNullException("tie");

			DiplomacyTask[] tasks = new DiplomacyTask[5];
			tasks[0] = DiplomacyTask.ExitDiplomacy;		//the last task is always an exit task.

            int numGiven = givenItems == null ? 0 : givenItems.Count;
            int numTaken = takenItems == null ? 0 : takenItems.Count;

			if(numGiven > 0 && numTaken > 0)
			{
				//AI offering straight up trade
				tasks[2] = DiplomacyTask.AcceptForeignProposal;
				tasks[1] = DiplomacyTask.Counterproposal;
				
			}
			else if(numGiven == 0 && numTaken > 0)
			{
				//AI demanding tribute
				tasks[2] = DiplomacyTask.GiveTribute;
				tasks[1] = DiplomacyTask.RefuseTribute;
			}
			else if(numGiven == 0 && numTaken == 0)
			{
				tasks[4] = DiplomacyTask.ProposeNegotiation;
				tasks[3] = DiplomacyTask.AskForLoan;
				tasks[2] = DiplomacyTask.TradeWorldMaps;
				tasks[1] = DiplomacyTask.DeclareWar;
			}

			return tasks;
		}

		/// <summary>
		/// Gets an array of tasks that the user can perform whilst trading with a foreign leader.
		/// </summary>
		/// <param name="tie">The <c>DiplomaticTie</c> that exists between the two countries.</param>
		/// <returns>An array of <c>DiplomaticTask</c> enums representing tasks that can 
		/// be performed by the user during the trade negotiations.</returns>
		/// <remarks>These tasks will be different than those returned from <c>GetDiplomacyTasks()</c>.  
		/// These tasks will be specific to the actual process of trading items with the foreign country, while 
		/// the <c>GetDiplomacyTasks()</c> covered other tasks such as declaring war, offering peace 
		/// treaties, etc...</remarks>
		public static DiplomacyTask[] GetTradingTasks(DiplomaticTie tie)
		{
            if (tie == null)
                throw new ArgumentNullException("tie");

			DiplomacyTask[] tasks = new DiplomacyTask[] {
															DiplomacyTask.BackOutFromNegotiation
														};

			return tasks;
		}

	}
	
	/// <summary>
	/// Interface to a task link factory.  Concrete client implementations will implement this 
	/// factory and create the logic for building and returning client-specific task links.
	/// </summary>
	public interface IDiplomacyTaskLinkFactory
	{
		/// <summary>
		/// Creates a task link and returns it.
		/// </summary>
		/// <param name="text">The text to display to the user in the task link.</param>
		/// <param name="taskCommand">The command for task.</param>
		/// <returns></returns>
		IDiplomacyTaskLink CreateTaskLink(string text, Command taskCommand);
	}
}
