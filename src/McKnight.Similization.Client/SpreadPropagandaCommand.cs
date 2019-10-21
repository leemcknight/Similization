using System;
using System.Collections.Generic;
using System.Text;
using McKnight.Similization.Server;
using System.Globalization;

namespace McKnight.Similization.Client
{
    /// <summary>
    /// Class representing the command to spread propaganda throughout an 
    /// enemy city.
    /// </summary>
    public class SpreadPropagandaCommand : EspionageCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadPropagandaCommand"/> class.
        /// </summary>
        public SpreadPropagandaCommand()
        {
            this.Name = "SpreadPropagandaCommand";
        }

        /// <summary>
        /// Invokes the Command.
        /// </summary>
        public override void Invoke()
        {
            OnInvoking();
            string cityPickerTitle = ClientResources.GetString("propaganda_cityPickerTitle");            
            SelectCity(cityPickerTitle);
            if (this.City == null)
            {
                OnCanceled();
                return;
            }
            EspionageResult result = this.DiplomaticTie.SpreadPropaganda(this.City);
            string text = string.Empty;
            switch (result)
            {
                case EspionageResult.Failure:
                    text = ClientResources.GetString("propaganda_failure");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.City.Name);
                    break;                                
                case EspionageResult.ImmuneToEspionage:
                    text = ClientResources.GetString("propaganda_immune");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.DiplomaticTie.ForeignCountry.Civilization.Adjective);
                    break;                  
                case EspionageResult.Success:
                    text = ClientResources.GetString("propaganda_success");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.City.Name);
                    break;
            }
            IGameWindow gw = ClientApplication.Instance.GameWindow;
            gw.ShowMessageBox(text, ClientResources.GetString(StringKey.GameTitle));
            OnInvoked();
        }        
    }
}
