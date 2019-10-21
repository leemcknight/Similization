using System;
using System.Globalization;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{	
	/// <summary>
	/// Represents a command to show the domestic advisor screen.
	/// </summary>
	public class DisplayDomesticAdvisorCommand : Command
	{
		/// <summary>
		/// Initializes a new instance of the <c>DisplayDomesticAdvisorCommand</c> class.
		/// </summary>
		public DisplayDomesticAdvisorCommand()
		{
			this.Name = "DisplayDomesticAdvisorCommand";
		}

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			ClientApplication client = ClientApplication.Instance;
			IDomesticAdvisorControl ctl = client.GetControl(typeof(IDomesticAdvisorControl)) as IDomesticAdvisorControl;
			ctl.AdvisorText = GetAdvisorText();
			ctl.ShowSimilizationControl();
			OnInvoked();
		}

		private static string GetAdvisorText()
		{
			string text = string.Empty;
			ClientApplication client = ClientApplication.Instance;
			Country player = client.Player;
			if(player.Government.Fallback)
				text = ClientResources.GetString("domesticAdvisor_anarchy");
			else
			{
				if(RandomNumber.GetRandomNumber(10) > 5)
				{
					//tell them we need more cities.
					text = ClientResources.GetString("domesticAdvisor_needCities");
				}
				else
				{
					//say something about a city.
					int count = player.Cities.Count;
					int idx = RandomNumber.GetRandomNumber(count-1);
					City city = client.Player.Cities[idx];
					string format = ClientResources.GetString("domesticAdvisor_cityHappiness");
					text = string.Format(CultureInfo.CurrentCulture, format, player.Government.LeaderTitle, city.Name, "");
				}
			}

			return text;
		}
	}
}