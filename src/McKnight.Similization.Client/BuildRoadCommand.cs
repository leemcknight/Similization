using System;
using System.Globalization;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{	
	/// <summary>
	/// Represents a command to build a road.
	/// </summary>
	public class BuildRoadCommand : Command
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BuildRoadCommand"/> class.
		/// </summary>
		public BuildRoadCommand()
		{
			this.Name = "BuildRoadCommand";
		}

		/// <summary>
		/// Invokes the <c>BuildRoadCommand</c>.
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
			
			((Worker)unit).BuildRoad();
			OnInvoked();
		}
	}
}