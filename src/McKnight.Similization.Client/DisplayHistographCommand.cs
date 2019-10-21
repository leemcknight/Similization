using System;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{	
	/// <summary>
	/// Represents a command to show the histograph.
	/// </summary>
	public class DisplayHistographCommand : Command
	{
		/// <summary>
		/// Initialiazes a new instance of the <see cref="DisplayHistographCommand"/> class.
		/// </summary>
		public DisplayHistographCommand()
		{
			this.Name = "DisplayHistographCommand";
		}

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			ClientApplication client = ClientApplication.Instance;

			IHistograph histograph = 
				client.GetControl(typeof(IHistograph)) as IHistograph;

			histograph.ShowSimilizationControl();
			OnInvoked();
		}
	}
}