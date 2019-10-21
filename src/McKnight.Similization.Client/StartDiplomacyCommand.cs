using System;
using System.Globalization;
using McKnight.Similization.Server;
using McKnight.Similization.Core;

namespace McKnight.Similization.Client
{
		
	/// <summary>
	/// Represents a command to start diplomacy with one of the foreign countries in the game.
	/// </summary>
	public class StartDiplomacyCommand : DiplomacyCommand
	{
		private string[] _advice;
		private int _adviceIndex;

		/// <summary>
		/// Initializes a new instance of the <c>StartDiplomacyCommand</c> class.
		/// </summary>
		public StartDiplomacyCommand()
		{
			this.Name = "StartDiplomacyCommand";
		}

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			ClientApplication client = ClientApplication.Instance;
			IDiplomaticTiePicker picker = (IDiplomaticTiePicker)client.GetControl(typeof(IDiplomaticTiePicker));
			picker.ShowSimilizationControl();
			DiplomaticTie tie = picker.DiplomaticTie;
			if(tie == null)
				return;
			
			//get an instance of the diplomacy control
			DiplomacyControl = (IDiplomacyControl)client.GetControl(typeof(IDiplomacyControl));
			IDiplomacyTaskLinkFactory factory = DiplomacyControl.GetTaskLinkFactory();

			//initialize the properties of the control
			DiplomacyControl.DiplomaticTie = tie;

			DiplomacyControl.ForeignLeaderHeaderText = string.Format(
				CultureInfo.InvariantCulture,
				ClientResources.GetString(StringKey.DiplomacyCountryHeader),
				tie.ForeignCountry.Name, 
				ClientApplication.GetAttitudeString(tie.Attitude));

			DiplomacyControl.ForeignLeaderPhrase = AIDiplomacyPhraseHelper.GetForeignLeaderGreeting(tie);
			DiplomacyControl.AdvisorHelpRequested += new EventHandler(HandleAdvisorHelpRequested);
			_advice = DiplomacyAdvice.GetAdvice(tie);
			DiplomacyControl.AdvisorPhrase = _advice[_adviceIndex];

			//get the tasks
			DiplomacyTask[] tasks = DiplomacyHelper.GetDiplomacyTasks(tie,null,null);

			string taskText;
			DiplomacyCommand command;
			IDiplomacyTaskLink taskLink;

			//add the tasks to the diplomacy control.
			foreach(DiplomacyTask task in tasks)
			{
				taskText = DiplomacyHelper.GetTaskString(task,tie);
				command = DiplomacyHelper.GetTaskCommand(task, DiplomacyControl);
				taskLink = factory.CreateTaskLink(taskText,command);
				DiplomacyControl.TaskLinks.Add(taskLink);
			}
		
			DiplomacyControl.ShowSimilizationControl();
			OnInvoked();
		}

		private void HandleAdvisorHelpRequested(object sender, System.EventArgs e)
		{
			if(_adviceIndex > _advice.GetUpperBound(0))
				_adviceIndex = 0;
			DiplomacyControl.AdvisorPhrase = _advice[_adviceIndex++];
		}

	}
}