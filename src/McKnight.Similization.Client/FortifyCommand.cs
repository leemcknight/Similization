using System;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{	
	/// <summary>
	/// Represents a command to have a unit fortify itself.
	/// </summary>
	public class FortifyCommand : Command
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FortifyCommand"/> class.
		/// </summary>
		public FortifyCommand()
		{
			this.Name = "FortifyCommand";
		}

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			ClientApplication client = ClientApplication.Instance;
			Unit unit = client.ServerInstance.ActiveUnit;
			if(unit == null)
				throw new InvalidOperationException(ClientResources.GetExceptionString("Unit_Null"));

			unit.Fortified = true;
			OnInvoked();
		}
	}
}