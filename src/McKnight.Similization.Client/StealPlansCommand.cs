using System;
using System.Collections.Generic;
using System.Text;
using McKnight.Similization.Server;
using System.Globalization;
using System.Collections.ObjectModel;

namespace McKnight.Similization.Client
{
    /// <summary>
    /// Class representing the command to steal the plans of another country.
    /// </summary>
    public class StealPlansCommand : EspionageCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StealPlansCommand"/> class.
        /// </summary>
        public StealPlansCommand()
        {
            this.Name = "StealPlansCommand";
        }

        /// <summary>
        /// Invokes the command.
        /// </summary>
        public override void Invoke()
        {
            OnInvoking();            
            ClientApplication ca = ClientApplication.Instance;
            IDiplomaticTiePicker picker = (IDiplomaticTiePicker)ca.GetControl(typeof(IDiplomaticTiePicker));
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
            this.City = this.DiplomaticTie.ForeignCountry.CapitalCity;
            EspionageResult result = this.DiplomaticTie.StealPlans();
            string text = string.Empty;
            switch (result)
            {                
                case EspionageResult.ImmuneToEspionage:
                    text = ClientResources.GetString("stealplans_immune");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.DiplomaticTie.ForeignCountry.Civilization.Adjective);
                    break;                
                case EspionageResult.SpyCaught:
                    text = ClientResources.GetString("stealplans_spycaught");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.City.Name, 
                        this.DiplomaticTie.ForeignCountry.Government.LeaderTitle, 
                        this.DiplomaticTie.ForeignCountry.LeaderName);
                    break;
                case EspionageResult.SuccessWithCapturedSpy:
                    text = ClientResources.GetString("stealplans_success_spycaught");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text,
                        this.City.Name,
                        this.DiplomaticTie.ForeignCountry.Government.LeaderTitle,
                        this.DiplomaticTie.ForeignCountry.LeaderName,
                        this.DiplomaticTie.ForeignCountry.Government.LeaderTitle,
                        this.DiplomaticTie.ForeignCountry.LeaderName);
                    break;
                case EspionageResult.Success:
                    text = ClientResources.GetString("stealplans_success");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.City.Name, 
                        this.DiplomaticTie.ForeignCountry.Government.LeaderTitle, 
                        this.DiplomaticTie.ForeignCountry.LeaderName);
                    break;
            }
            IGameWindow gw = ClientApplication.Instance.GameWindow;
            gw.ShowMessageBox(text, ClientResources.GetString(StringKey.GameTitle));
            OnInvoked();
        }
    }
}
