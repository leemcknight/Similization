using System;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{	
	/// <summary>
	/// Represents a command to show the foreign advisor screen.
	/// </summary>
	public class DisplayForeignAdvisorCommand : Command
	{
		/// <summary>
		/// Intializes a new instance of the <see cref="DisplayForeignAdvisorCommand"/> class.
		/// </summary>
		public DisplayForeignAdvisorCommand()
		{
			this.Name = "DisplayForeignAdvisorCommand";
		}

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			ClientApplication client = ClientApplication.Instance;

			IForeignAdvisorControl ctl =
				client.GetControl(typeof(IForeignAdvisorControl)) as IForeignAdvisorControl;

			ctl.ShowSimilizationControl();
			OnInvoked();
		}
	}
}