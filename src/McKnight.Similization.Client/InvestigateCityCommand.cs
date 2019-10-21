using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{
    /// <summary>
    /// Class representing a command to investigate a city from another
    /// civilization.
    /// </summary>
    public class InvestigateCityCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvestigateCityCommand"/> class.
        /// </summary>
        public InvestigateCityCommand()
        {
            this.Name = "InvestigateCityCommand";
        }

        /// <summary>
        /// Invokes the command.
        /// </summary>
        public override void Invoke()
        {
            OnInvoking();
            ClientApplication ca = ClientApplication.Instance;
            IDiplomaticTiePicker picker = (IDiplomaticTiePicker)ca.GetControl(typeof(IDiplomaticTiePicker));
            picker.ShowSimilizationControl();
            DiplomaticTie tie = picker.DiplomaticTie;
            if(tie == null)
            {
                OnCanceled();
                return;
            }
            ICityPicker cityPicker = (ICityPicker)ca.GetControl(typeof(ICityPicker));
            cityPicker.PickerTitle = ClientResources.GetString("investigateCity_cityPickerTitle");
            cityPicker.ShowSimilizationControl();
            City city = cityPicker.City;
            if (city == null)
            {
                OnCanceled();
                return;
            }
            bool success = tie.InvestigateCity(city);
            string text = string.Empty;
            if (success)
            {
                text = ClientResources.GetString("investigateCity_success");
                text = string.Format(CultureInfo.CurrentCulture, text, city.Name);
                ca.GameWindow.ShowMessageBox(text, ClientResources.GetString(StringKey.GameTitle));
                ICityControl cityControl = (ICityControl)ca.GetControl(typeof(ICityControl));
                cityControl.Editable = false;
                cityControl.City = city;
                cityControl.ShowSimilizationControl();
            }
            else
            {
                text = ClientResources.GetString("investigateCity_immune");
                text = string.Format(
                    CultureInfo.CurrentCulture,
                    text, 
                    tie.ForeignCountry.Civilization.Adjective);
                ca.GameWindow.ShowMessageBox(text, ClientResources.GetString(StringKey.GameTitle));
            }
            OnInvoked();
        }
    }
}
