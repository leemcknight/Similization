using System;
using System.Collections.ObjectModel;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// A abstract command invoked during diplomacy.
	/// </summary>
	public abstract class DiplomacyCommand : Command
	{
		private IDiplomacyControl _diplomacyControl;

        /// <summary>
        /// Initalizes a new instance of the <see cref="DiplomacyCommand"/> class.
        /// </summary>
		protected DiplomacyCommand()
		{
		}

		/// <summary>
		/// The control that this command is displayed on.
		/// </summary>
		public IDiplomacyControl DiplomacyControl
		{
			get { return _diplomacyControl; }
			set { _diplomacyControl = value; }
		}
	}
	

    
    /// <summary>
    /// Class representing an action to apply the results of a trade.
    /// </summary>
	public abstract class ApplyTradeCommand : DiplomacyCommand
	{
        /// <summary>
        /// Invokes the <see cref="ApplyTradeCommand"/>.
        /// </summary>
		public override void Invoke()
		{
			OnInvoking();
			Collection<ITradable> givenItems = this.DiplomacyControl.GivenItems;
            Collection<ITradable> takenItems = this.DiplomacyControl.TakenItems;
			DiplomaticTie tie = this.DiplomacyControl.DiplomaticTie;
			
			foreach(ITradable given in givenItems)
			{
                Type type = given.GetType();
				if(type == typeof(DiplomaticAgreement))
				{
					tie.DiplomaticAgreements.Add((DiplomaticAgreement)given);
				}
				else if(type == typeof(GoldLumpSum))
				{
					GoldLumpSum gls = (GoldLumpSum)given;
					tie.ForeignCountry.Gold += gls.LumpSum;
				}
				else if(type == typeof(GoldPerTurn))
				{
				}
				else if(type == typeof(Technology))
				{
					tie.ForeignCountry.AcquiredTechnologies.Add((Technology)given);
				}
				else if(type == typeof(City))
				{
                    City city = (City)given;
					tie.ForeignCountry.Cities.Add(city);
					ClientApplication.Instance.Player.Cities.Remove(city);
				}
				else if(type == typeof(Resource))
				{
				}
			}

			foreach(ITradable taken in takenItems)
			{
                Type type = taken.GetType();
                if (type == typeof(DiplomaticAgreement))
                {
                }
                else if (type == typeof(GoldLumpSum))
                {
                    tie.ParentCountry.Gold += ((GoldLumpSum)taken).LumpSum;
                }
                else if (type == typeof(GoldPerTurn))
                {
                }
                else if (type == typeof(Technology))
                {
                    tie.ParentCountry.AcquiredTechnologies.Add((Technology)taken);
                }
                else if (type == typeof(City))
                {
                    City city = (City)taken;
                    tie.ParentCountry.Cities.Add(city);
                    tie.ForeignCountry.Cities.Remove(city);
                }
                else if (type == typeof(Resource))
                {
                }

			}
			OnInvoked();
		}
	}


	/// <summary>
	/// A command to exit the negotiations with the foreign country.
	/// </summary>
	public class ExitNegotiationsCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			this.DiplomacyControl.EndDiplomacy();
			OnInvoked();
		}

	}

	/// <summary>
	/// A command to have the offending troops automatically retreat to the nearest 
	/// neutral cell and apologize for the intrusion.
	/// </summary>
	public class AutoRetreatTroopsCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{

		}
	}


	/// <summary>
	/// A command to passively retreat troops that are invading on foreign ground.  
	/// </summary>
	/// <remarks>This command will not automatically retreat troops.  It will 
	/// be up to the user to retreat the troops.  This command will basically 
	/// buy time for the user.</remarks>
	public class PassiveRetreatTroopsCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
		}

	}

	/// <summary>
	/// A command to have the two negotiating parties trade world maps.
	/// </summary>
	public class TradeWorldMapsCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			
		}
	}

	/// <summary>
	/// A command to quit the current negotiations and/or trade proposals.
	/// </summary>
	public class BackOutFromNegotiationCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			DiplomacyControl.EndNegotiations();
			DiplomacyControl.TaskLinks.Clear();

			DiplomaticTie tie = DiplomacyControl.DiplomaticTie;

			IDiplomacyTaskLinkFactory factory = DiplomacyControl.GetTaskLinkFactory();
			DiplomacyTask[] tasks = DiplomacyHelper.GetDiplomacyTasks(tie, null, null);

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

			DiplomacyControl.ForeignLeaderPhrase = AIDiplomacyPhraseHelper.GenerateBackOutResponse();
			OnInvoked();
		}
	}

	/// <summary>
	/// A command to refuse to give the requested tribute to the foreign country.
	/// </summary>
	public class RefuseTributeCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
		}
	}


	/// <summary>
	/// A command to give in to the demands of the foreign country.
	/// </summary>
	public class GiveTributeCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
		}
	}

	/// <summary>
	/// A command for threatening the foreign country with war unless they 
	/// remove their troops.
	/// </summary>
	public class ThreatenWarForBorderInvasionCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
		}
	}


	/// <summary>
	/// A command for warning the foreign country to remove their troops.
	/// </summary>
	public class WarningForBorderInvasionCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
		}
	}


	/// <summary>
	/// A command for offering a right of passage agreement to the foreign country.
	/// </summary>
	public class OfferRightOfPassageCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
		}
	}

	/// <summary>
	/// A command to offer a peace treaty to the foreign country during a time of war.
	/// </summary>
	public class OfferPeaceTreatyDuringWarCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
		}
	}


	/// <summary>
	/// A command to ask the foreign country for a loan of gold.
	/// </summary>
	public class RequestLoanCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
		}
	}


	/// <summary>
	/// A command to offer an even-up trade to the foreign country.
	/// </summary>
	/// <remarks>Even-up trades are trades where both sides give something 
	/// to the other party.  It may still be a one-sided deal, but both 
	/// sides are giving something up.</remarks>
	public class OfferEvenUpTradeCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
		}
	}


	/// <summary>
	/// A command to offer a gift to the foreign country.
	/// </summary>
	public class OfferGiftCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
		}
	}


	/// <summary>
	/// A command to demand a tribute from the foreign leader.
	/// </summary>
	public class DemandTributeCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
		}
	}


	/// <summary>
	/// A command to ask the foreign leader to tell the player what they want 
	/// in exchange for a certain item.
	/// </summary>
	public class AskForCounterofferCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
		}
	}


	/// <summary>
	/// A command to asks the foreign leader if there is anything they are willing to trade
	/// in exchange for a particular item the player is offering for trade.
	/// </summary>
	public class AskForExchangeCommand : DiplomacyCommand
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
		}
	}
}
