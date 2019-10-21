using System;
using System.Globalization;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{	
	/// <summary>
	/// Command for merging a unit into a city.
	/// </summary>
	public class MergeUnitCommand : Command
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MergeUnitCommand"/> class.
		/// </summary>
		public MergeUnitCommand()
		{
			this.Name = "MergeUnitCommand";
		}

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			ClientApplication client = ClientApplication.Instance;

			Unit unit =  client.ServerInstance.ActiveUnit;
            GridCell unitCell = client.ServerInstance.Grid.GetCell(unit.Coordinates);

			if(unit == null)
				throw new InvalidOperationException(ClientResources.GetString("Unit_Null"));

			if(unitCell.City == null)
				throw new InvalidOperationException(ClientResources.GetString("Unit_MergeNoCity"));

			if(!unit.CanMergeWithCity)
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ClientResources.GetString("Unit_CannotMerge"), unit.Name));

			unit.MergeWithCity();
			OnInvoked();
		}
	}
}