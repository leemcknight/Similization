using System;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{	
	/// <summary>
	/// A Command to bombard a <see cref="GridCell"/>.
	/// </summary>
	public class BombardCommand : Command
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BombardCommand"/> class.
		/// </summary>
		public BombardCommand()
		{
			this.Name = "BombardCommand";
		}

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			ClientApplication client = ClientApplication.Instance;
			client.GameWindow.BeginBombardProcess();
			OnInvoked();
		}
	}
}