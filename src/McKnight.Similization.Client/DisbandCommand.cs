using System;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{	
	/// <summary>
	/// Command for disbanding a unit.
	/// </summary>
	public class DisbandCommand : Command
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DisbandCommand"/> class.
		/// </summary>
		public DisbandCommand()
		{
			this.Name = "DisbandCommand";
		}

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			ClientApplication client = ClientApplication.Instance;
			string message, title;

			message = ClientResources.GetString(StringKey.DisbandConfirmation);
			title = ClientResources.GetString(StringKey.GameTitle);

			bool confirmed = client.GameWindow.GetUserConfirmation(message, title);

			if(confirmed)
			{
				Country player = client.Player;
				Unit unit = client.ServerInstance.ActiveUnit;
				player.DisbandUnit(unit);
			}
			OnInvoked();
		}

	}
}