using System;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{	
	/// <summary>
	/// Represents a command to have a unit move to a specific cell.
	/// </summary>
	public class MoveToCommand : Command
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MoveToCommand"/> class.
		/// </summary>
		public MoveToCommand()
		{
			this.Name = "MoveToCommand";
		}

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			IGameWindow gameWindow = ClientApplication.Instance.GameWindow;
			gameWindow.GoToCursor = true;
			OnInvoked();
		}
	}
}