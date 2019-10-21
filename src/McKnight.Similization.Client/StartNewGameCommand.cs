using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Globalization;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// Represents a command to start a new game.
	/// </summary>
	public class StartNewGameCommand : Command
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StartNewGameCommand"/> class.
		/// </summary>
		public StartNewGameCommand()
		{
			this.Name = "StartNewGameCommand";
		}

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			ClientApplication client = ClientApplication.Instance;
			GameRoot root = client.ServerInstance;

			//load the ruleset
            string loadingMessage = ClientResources.GetString("item_loading");
            string message = string.Format(CultureInfo.CurrentCulture, loadingMessage, this.rulesetPath);
			client.Console.WriteLine(message);
			root.LoadRuleset(this.rulesetPath);
			
			//load the tileset
            message = string.Format(CultureInfo.CurrentCulture, loadingMessage, this.tilesetPath);
			client.Console.WriteLine(message);
			client.LoadTileset(this.tilesetPath);

			INewGameControl ctl= (INewGameControl)client.GetControl(typeof(INewGameControl));
			ctl.ResultChosen += new EventHandler(HandleResultChosen);            
			ctl.ShowSimilizationControl();			
		}

		#region Properties

		private Color playerColor;

		/// <summary>
		/// Gets the color the palyer chose for themselves.
		/// </summary>
		public Color PlayerColor
		{
			get { return this.playerColor; }
		}

		private Civilization civilization;

		/// <summary>
		/// Gets the civilization the player chose to play as.
		/// </summary>
		public Civilization Civilization
		{
			get { return this.civilization; }
		}

		private NamedObjectCollection<Civilization> opponents;

		/// <summary>
		/// Gets the list of civilizations the player is playing against.
		/// </summary>
		public NamedObjectCollection<Civilization> Opponents
		{
			get { return this.opponents; }
		}

		private string leaderName;

		/// <summary>
		/// Gets the name the player chose for his leader.
		/// </summary>
		public string LeaderName
		{
			get { return this.leaderName; }
		}

		private string rulesetPath;

		/// <summary>
		/// The path to the <see cref="Ruleset"/> to use for this game.
		/// </summary>
		public string RulesetPath
		{
			get { return this.rulesetPath; }
			set { this.rulesetPath = value; }
		}

		private string tilesetPath;

		/// <summary>
		/// The path to the <see cref="Tileset"/> to use for this game.
		/// </summary>
		public string TilesetPath
		{
			get { return this.tilesetPath; }
			set { this.tilesetPath = value; }
		}

		#endregion

		#region Event Handlers

		private void HandleResultChosen(object sender, System.EventArgs e)
		{
			INewGameControl ctl = (INewGameControl)sender;
			this.playerColor = ctl.PlayerColor;
			this.civilization = ctl.ChosenCivilization;
			this.opponents = ctl.ChosenOpponents;
			this.leaderName = ctl.LeaderName;
			GameRoot root = ClientApplication.Instance.ServerInstance;
			root.StartServer(ctl.WorldSize, ctl.Age, ctl.Temperature, ctl.Climate, ctl.Landmass, ctl.WaterCoverage, ctl.BarbarianAggressiveness, ctl.Difficulty, ctl.Rules);
            OnInvoked();
		}
		#endregion
	}
}