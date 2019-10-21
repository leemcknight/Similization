using System;
using McKnight.Similization.Core;

namespace McKnight.Similization.Client
{
	
	/// <summary>
	/// Represents a command to show help to the user.
	/// </summary>
	public class HelpCommand : Command
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="HelpCommand"/> class.
		/// </summary>
		public HelpCommand()
		{
			this.Name = "HelpCommand";
		}

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			ClientApplication client = ClientApplication.Instance;
			IHelpControl helpControl = (IHelpControl)client.GetControl(typeof(IHelpControl));
			helpControl.ShowSimilizationControl();
			OnInvoked();			
		}
	}
}