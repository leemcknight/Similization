using System;
using System.Globalization;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{
	
	/// <summary>
	/// Represents a command to show the military advisor screen.
	/// </summary>
	public class DisplayMilitaryAdvisorCommand : Command
	{
		/// <summary>
        /// Initializes a new instance of the <see cref="DisplayMilitaryAdvisorCommand"/> class.
		/// </summary>
		public DisplayMilitaryAdvisorCommand()
		{
			this.Name = "DisplayMilitaryAdvisorCommand";
		}

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			ClientApplication client = ClientApplication.Instance;
			IMilitaryAdvisorControl ctl = (IMilitaryAdvisorControl)client.GetControl(typeof(IMilitaryAdvisorControl));
			ctl.CountryHeaderText = GetCountryHeaderText(client.Player);
			ctl.ForeignCountryHeaderText = GetCountryHeaderText(ctl.ForeignCountry);
			ctl.ForeignCountryChanged += new EventHandler(OnForeignCountryChanged);
            ctl.AdvisorText = GetAdvisorText(client.Player, ctl.ForeignCountry);
			ctl.ShowSimilizationControl();
			OnInvoked();
		}

		private void OnForeignCountryChanged(object sender, EventArgs e)
		{
		}

		private static string GetCountryHeaderText(Country country)
		{
			if(country == null)
				return string.Empty;

			string s = ClientResources.GetString(StringKey.MilitaryAdvisorCountryHeader);
			string adjective = country.Civilization.Adjective;
			string gov = country.Government.Name;
			string text = string.Format(CultureInfo.InvariantCulture, s, adjective, gov);
			return text;
		}

        private static string GetAdvisorText(Country parent, Country foreign)
        {
            if (foreign == null)
            {
                //there's no foreign country, so the advisor will have to offer 
                //other advice.
                return ClientResources.GetString("militaryAdvisor_needUnits");
                
            }
            else
            {
                //we can talk about anything.
                return GetMilitaryComparisonString(parent, foreign);
            }        
        }

        private static string GetMilitaryComparisonString(Country parent, Country foreign)
        {
            int powerThem = 0;
            int powerUs = 0;

            foreach (Unit unit in foreign.Units)
                powerThem += unit.OffensivePower;
            foreach (Unit unit in parent.Units)
                powerUs += unit.OffensivePower;

            double n, d;
            n = Convert.ToDouble(powerUs);
            d = Convert.ToDouble(powerThem + powerUs);
            if (d == 0 && n > 0)
                return ClientResources.GetString("militaryAdvisor_strongMilitary");
            else if (d == 0 && n == 0)
                return ClientResources.GetString("militaryAdvisor_averageMilitary");
            double div = n / d;
            if (div >= .4 && div <= .6)
                return ClientResources.GetString("militaryAdvisor_averageMilitary");
            else if (div <= .4)
                return ClientResources.GetString("militaryAdvisor_weakMilitary");
            else if (div >= .6)
                return ClientResources.GetString("militaryAdvisor_strongMilitary");
            return string.Empty;
        }
	}
}