using System;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{	
	/// <summary>
	/// Represents a command to save the game.
	/// </summary>
	public class SaveGameCommand : Command
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SaveGameCommand"/> class.
		/// </summary>
		public SaveGameCommand()
		{
			this.Name = "SaveGameCommand";
		}

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			ClientApplication client = ClientApplication.Instance;
			ISaveGameWindow wnd = (ISaveGameWindow)client.GetControl(typeof(ISaveGameWindow));
			wnd.ShowSimilizationControl();
			GameRoot root = client.ServerInstance;
			if(wnd.SavedGameFile.Length > 0)
			{
				root.SaveGame(wnd.SavedGameFile);
				OnInvoked();
			}
			else
			{
				OnCanceled();
			}
		}
	}
}