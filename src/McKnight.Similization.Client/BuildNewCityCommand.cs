using System;
using System.Globalization;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{	
	/// <summary>
	/// Command for building a new city.
	/// </summary>
	public class BuildNewCityCommand : Command
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="BuildNewCityCommand"/> class.
		/// </summary>
		public BuildNewCityCommand()
		{
			this.Name = "BuildNewCityCommand";
		}

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			Settler settler;
			string newCityName;

			OnInvoking();

			ClientApplication client = ClientApplication.Instance;
			GameRoot root = client.ServerInstance;

			if(root.ActiveUnit == null)
				throw new InvalidOperationException(ClientResources.GetExceptionString("Unit_Null"));
			
			if(!root.ActiveUnit.CanSettle)
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ClientResources.GetExceptionString("Unit_CannotSettle"),root.ActiveUnit.Name));
				
			INewCityControl newCityWindow = (INewCityControl)client.GetControl(typeof(INewCityControl));

			settler = (Settler)root.ActiveUnit;
			newCityWindow.ShowSimilizationControl();
			
			newCityName = newCityWindow.CityName;
			_city = settler.Settle(newCityName);

			OnInvoked();
		}

		private City _city;

		/// <summary>
		/// Gets the last city created with this command.
		/// </summary>
		public City LastCityBuilt
		{
			get { return _city; }
		}
	}

}