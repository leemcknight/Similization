using System;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// Represents a command to show the game options to the user.
	/// </summary>
	public class GameOptionsCommand : Command
	{
        private IOptionsControl optionsControl;
		/// <summary>
		/// Initializes a new instance of the <see cref="GameOptionsCommand"/> class.
		/// </summary>
		public GameOptionsCommand()
		{
			this.Name = "GameOptionsCommand";
		}

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			ClientApplication client = ClientApplication.Instance;
			this.optionsControl = client.GetControl(typeof(IOptionsControl)) as IOptionsControl;
            this.optionsControl.Closed += new EventHandler<ControlClosedEventArgs>(OptionsControlClosed);
            this.optionsControl.ShowSimilizationControl();            
		}

        void OptionsControlClosed(object sender, ControlClosedEventArgs e)
        {            
            if (e.CloseResult == ControlCloseResult.Ok)
            {
                ClientApplication client = ClientApplication.Instance;
                client.Options.ShowKilledMessage = this.optionsControl.ShowKilledMessage;
                client.Options.WaitAfterTurn = this.optionsControl.WaitAfterTurn;
                client.Options.StartingRulesetPath = this.optionsControl.StartingRulesetPath;
                client.Options.TilesetPath = this.optionsControl.TilesetPath;
                client.Options.CityNameFont = this.optionsControl.CityNameFont;
                client.Options.CityNameFontColor = this.optionsControl.CityNameFontColor;
                client.Options.Serialize();
                OnInvoked();
            }
            else
            {
                OnCanceled();
            }
        }
	}

}