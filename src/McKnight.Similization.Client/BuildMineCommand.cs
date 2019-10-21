using System;
using System.Globalization;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{	
	/// <summary>
	/// Represents a command to have the worker build a mine.
	/// </summary>
	public class BuildMineCommand : Command
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BuildMineCommand"/> class.
		/// </summary>
		public BuildMineCommand()
		{
			this.Name = "BuildMineCommand";
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

			if(!unit.CanWork)
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ClientResources.GetExceptionString("Unit_CannotWork"),unit.Name));
			
			((Worker)unit).Mine();
			
			OnInvoked();
		}
	}
}