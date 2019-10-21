using System;
using System.ComponentModel;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// A command to propose a trade or start negotiations with the foreign country.
	/// </summary>
	public class ProposeNegotiationCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			DiplomaticTie tie = DiplomacyControl.DiplomaticTie;
			DiplomacyControl.StartNegotiations();	
			DiplomacyControl.ForeignLeaderPhrase = AIDiplomacyPhraseHelper.GetProposalResponse(tie);
			DiplomacyControl.TaskLinks.Clear();
			//DiplomacyControl.GivenItems.CollectionChanged += new CollectionChangeEventHandler(HandleTradeProposalChange);
			//DiplomacyControl.TakenItems.CollectionChanged += new CollectionChangeEventHandler(HandleTradeProposalChange);
			
			IDiplomacyTaskLinkFactory factory = DiplomacyControl.GetTaskLinkFactory();
			DiplomacyTask[] tasks = DiplomacyHelper.GetTradingTasks(tie);

			DiplomacyCommand taskCommand;
			string taskText;
			IDiplomacyTaskLink taskLink;

			foreach(DiplomacyTask task in tasks)
			{
				taskCommand = DiplomacyHelper.GetTaskCommand(task, DiplomacyControl);
				taskText = DiplomacyHelper.GetTaskString(task,tie);
				taskLink = factory.CreateTaskLink(taskText, taskCommand);
				DiplomacyControl.TaskLinks.Add(taskLink);
			}
		}
		
		private void HandleTradeProposalChange(object sender, CollectionChangeEventArgs e)
		{
			TradeResponse probableResponse = GetProbableResponse();
			DiplomacyControl.AdvisorPhrase = DiplomacyHelper.GetProbableTradeResponseString(probableResponse);

			DiplomacyControl.TaskLinks.Clear();

			bool gift = (DiplomacyControl.GivenItems.Count > 0) && (DiplomacyControl.TakenItems.Count == 0);

			IDiplomacyTaskLinkFactory factory = DiplomacyControl.GetTaskLinkFactory();
			IDiplomacyTaskLink taskLink;
			string taskText;
			int randIndex;

			//add the backout task
            string[] backoutStrings = DiplomacyStringKey.GetBackOutFromNegotiationStrings();
			randIndex = RandomNumber.GetRandomNumber(backoutStrings.GetUpperBound(0));
			taskText = DiplomacyHelper.GetString(backoutStrings.GetValue(randIndex).ToString());
			taskLink = factory.CreateTaskLink(taskText, DiplomacyHelper.GetTaskCommand(DiplomacyTask.BackOutFromNegotiation, DiplomacyControl));
			DiplomacyControl.TaskLinks.Add(taskLink);

			if(gift)
			{
				//gift
                string[] giftStrings = DiplomacyStringKey.GetUserGiftStrings();
				randIndex = RandomNumber.GetRandomNumber(giftStrings.GetUpperBound(0));
				taskText = DiplomacyHelper.GetString(giftStrings.GetValue(randIndex).ToString());
				taskLink = factory.CreateTaskLink(taskText,new OfferGiftCommand());
				DiplomacyControl.TaskLinks.Add(taskLink);

				//trade for item
                string[] tradeStrings = DiplomacyStringKey.GetAskForExchangeStrings();
				randIndex = RandomNumber.GetRandomNumber(tradeStrings.GetUpperBound(0));
				taskText = DiplomacyHelper.GetString(tradeStrings.GetValue(randIndex).ToString());
				taskLink = factory.CreateTaskLink(taskText,new AskForExchangeCommand());
				DiplomacyControl.TaskLinks.Add(taskLink);
				
				return;
			}

			bool evenUp = (DiplomacyControl.GivenItems.Count > 0) && (DiplomacyControl.TakenItems.Count > 0);
			if(evenUp)
			{
				//offer trade
                string[] offerStrings = DiplomacyStringKey.GetUserOfferStrings();
				randIndex = RandomNumber.GetRandomNumber(offerStrings.GetUpperBound(0));
				taskText = DiplomacyHelper.GetString(offerStrings.GetValue(randIndex).ToString());
				taskLink = factory.CreateTaskLink(taskText,new OfferEvenUpTradeCommand());
				DiplomacyControl.TaskLinks.Add(taskLink);
				return;
			}

			//demand tribute
            string[] tributeStrings = DiplomacyStringKey.GetDemandTributeStrings();
			randIndex = RandomNumber.GetRandomNumber(tributeStrings.GetUpperBound(0));
			taskText = DiplomacyHelper.GetString(tributeStrings.GetValue(randIndex).ToString());
			taskLink = factory.CreateTaskLink(taskText,new DemandTributeCommand());
			DiplomacyControl.TaskLinks.Add(taskLink);

			//ask for counter
            string[] counterOfferStrings = DiplomacyStringKey.GetAskForExchangeStrings();
			randIndex = RandomNumber.GetRandomNumber(counterOfferStrings.GetUpperBound(0));
			taskText = DiplomacyHelper.GetString(counterOfferStrings.GetValue(randIndex).ToString());
			taskLink = factory.CreateTaskLink(taskText,new AskForCounterofferCommand());
			DiplomacyControl.TaskLinks.Add(taskLink);

		}

		private TradeResponse GetProbableResponse()
		{
			return TradeResponse.WeakDecline;
		}
	}
}
