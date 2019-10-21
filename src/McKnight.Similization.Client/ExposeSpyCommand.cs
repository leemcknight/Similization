using System;
using System.Collections.Generic;
using System.Text;
using McKnight.Similization.Server;
using System.Globalization;
using System.Collections.ObjectModel;

namespace McKnight.Similization.Client
{
    /// <summary>
    /// Class representing a command to expose a spy from a foreign country.
    /// </summary>
    public class ExposeSpyCommand : EspionageCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExposeSpyCommand"/> class.
        /// </summary>
        public ExposeSpyCommand()
        {
            this.Name = "ExposeSpyCommand";
        }

        /// <summary>
        /// Invokes the command.
        /// </summary>
        public override void Invoke()
        {            
            ClientApplication ca = ClientApplication.Instance;
            IDiplomaticTiePicker picker = (IDiplomaticTiePicker)ca.GetControl(typeof(IDiplomaticTiePicker));
            picker.PickerTitle = ClientResources.GetString("exposespy_tiePickerTitle");
            Collection<DiplomaticTie> ties = new Collection<DiplomaticTie>();
            foreach (DiplomaticTie t in ca.Player.DiplomaticTies)
            {
                //TODO:  account for needed improvements
                if (t.HasEmbassy)
                    ties.Add(t);

            }
            picker.InitializePicker(ties);
            picker.ShowSimilizationControl();
            this.DiplomaticTie = picker.DiplomaticTie;            
            IGameWindow gw = ClientApplication.Instance.GameWindow;            
            this.City = this.DiplomaticTie.ForeignCountry.CapitalCity;
            EspionageResult result = this.DiplomaticTie.ExposeSpy();
            string text = string.Empty;
            switch (result)
            {                
                case EspionageResult.ImmuneToEspionage:
                    text = ClientResources.GetString("exposespy_immune");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.DiplomaticTie.ForeignCountry.Government.LeaderTitle,
                        this.DiplomaticTie.ForeignCountry.LeaderName,
                        ClientApplication.Instance.Player.CapitalCity.Name,
                        this.DiplomaticTie.ForeignCountry.Government.LeaderTitle,
                        this.DiplomaticTie.ForeignCountry.LeaderName);
                    break;
                case EspionageResult.SpyCaught:
                    text = ClientResources.GetString("exposespy_spycaught");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.City.Name, 
                        this.DiplomaticTie.ForeignCountry.Government.LeaderTitle, 
                        this.DiplomaticTie.ForeignCountry.LeaderName);
                    break;
                case EspionageResult.Success:
                    text = ClientResources.GetString("exposespy_success");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, this.DiplomaticTie.ForeignCountry.Government.LeaderTitle,
                        this.DiplomaticTie.ForeignCountry.LeaderName,
                        ClientApplication.Instance.Player.CapitalCity.Name);
                    break;
                case EspionageResult.SuccessWithCapturedSpy:
                    text = ClientResources.GetString("exposespy_success_spycaught");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text,
                        ClientApplication.Instance.Player.CapitalCity.Name,
                        this.DiplomaticTie.ForeignCountry.Government.LeaderTitle,
                        this.DiplomaticTie.ForeignCountry.LeaderName);
                    break;  
            }

            gw.ShowMessageBox(text, ClientResources.GetString(StringKey.GameTitle));
            OnInvoked();
        }
    }
}
