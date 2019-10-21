using System;
using System.Collections.Generic;
using System.Text;
using McKnight.Similization.Server;
using System.Globalization;
using System.Collections.ObjectModel;

namespace McKnight.Similization.Client
{
    /// <summary>
    /// Class representing the command to steal an opponents' world map.
    /// </summary>
    public class StealWorldMapCommand : EspionageCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StealWorldMapCommand"/> class.
        /// </summary>
        public StealWorldMapCommand()
        {
            this.Name  = "StealWorldMapCommand";
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
            string text = string.Empty;
            EspionageResult result = this.DiplomaticTie.StealWorldMap();
            switch (result)
            {
                case EspionageResult.ImmuneToEspionage:
                    text = ClientResources.GetString("stealworldmap_immune");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.DiplomaticTie.ForeignCountry.Civilization.Adjective);
                    break;
                case EspionageResult.Success:
                    text = ClientResources.GetString("stealworldmap_success");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.DiplomaticTie.ForeignCountry.Civilization.Noun);
                    break;
                case EspionageResult.Failure:
                    text = ClientResources.GetString("stealworldmap_failure");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.City.Name);
                    break;
                case EspionageResult.SpyCaught:
                    text = ClientResources.GetString("stealworldmap_spycaught");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.City.Name, 
                        this.DiplomaticTie.ForeignCountry.Government.LeaderTitle, 
                        this.DiplomaticTie.ForeignCountry.LeaderName);
                    break;
            }
            OnInvoked();
        }
    }
}
