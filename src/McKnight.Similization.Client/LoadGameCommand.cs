using System;
using System.Globalization;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{	
	/// <summary>
	/// Represents a command to load a saved game.
	/// </summary>
	public class LoadGameCommand : Command
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LoadGameCommand"/> class.
		/// </summary>
		public LoadGameCommand()
		{
			this.Name = "LoadGameCommand";
		}

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			ClientApplication client = ClientApplication.Instance;
			ILoadGameWindow window = (ILoadGameWindow)client.GetControl(typeof(ILoadGameWindow));
			window.ShowSimilizationControl();
			if(window.LoadedGameFile == null || window.LoadedGameFile.Length == 0)
			{
				OnCanceled();
				return;
			}
			client.Console.WriteLine(string.Format(CultureInfo.InvariantCulture, ClientResources.GetString("item_loading"), window.LoadedGameFile));
			client.ServerInstance.LoadGame(window.LoadedGameFile);
			client.Console.WriteLine(ClientResources.GetString("game_loaded"));
			OnInvoked();
		}
	}
}