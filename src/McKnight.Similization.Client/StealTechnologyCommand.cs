using System;
using System.Collections.Generic;
using System.Text;
using McKnight.Similization.Server;
using System.Globalization;
using McKnight.Similization.Core;
using System.Collections.ObjectModel;

namespace McKnight.Similization.Client
{
    /// <summary>
    /// Class representing the command to steal a technology from 
    /// an opponent.
    /// </summary>
    public class StealTechnologyCommand : EspionageCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StealTechnologyCommand"/> class.
        /// </summary>
        public StealTechnologyCommand()
        {
            this.Name = "StealTechnologyCommand";
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
            IGameWindow gw = ClientApplication.Instance.GameWindow;
            bool canSteal = CheckForStealableTechnologies();
            if (!canSteal)
            {
                string msg = ClientResources.GetString("stealtechnology_noneavailable");
                msg = string.Format(
                    CultureInfo.CurrentCulture,
                    ClientApplication.Instance.Player.Government.LeaderTitle,
                    this.DiplomaticTie.ForeignCountry.Civilization.Noun);
                gw.ShowMessageBox(msg, ClientResources.GetString(StringKey.GameTitle));
                OnCanceled();
            }
            this.City = this.DiplomaticTie.ForeignCountry.CapitalCity;
            EspionageResult result = this.DiplomaticTie.StealTechnology();
            string text = string.Empty;
            switch (result)
            {
                case EspionageResult.Failure:
                    text = ClientResources.GetString("stealtechnology_failure");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.City.Name);
                    break;
                case EspionageResult.ImmuneToEspionage:
                    text = ClientResources.GetString("stealtechnology_immune");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.DiplomaticTie.ForeignCountry.Civilization.Adjective);
                    break;
                case EspionageResult.SpyCaught:
                    text = ClientResources.GetString("stealtechnology_spycaught");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.City.Name, 
                        this.DiplomaticTie.ForeignCountry.Government.LeaderTitle, 
                        this.DiplomaticTie.ForeignCountry.LeaderName);
                    break;
                case EspionageResult.Success:
                    text = ClientResources.GetString("stealtechnology_success");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.DiplomaticTie.ForeignCountry.Civilization.Noun);
                    break;               
            }
            
            gw.ShowMessageBox(text, ClientResources.GetString(StringKey.GameTitle));
            OnInvoked();
        }

        //This method checks the technologies of the foreign country to ensure 
        //that they have at least 1 technology that the parent country does 
        //not.
        private bool CheckForStealableTechnologies()
        {
            bool canSteal = false;
            Country player = ClientApplication.Instance.Player;
            foreach (Technology technology in this.DiplomaticTie.ForeignCountry.AcquiredTechnologies)
            {
                if (!player.AcquiredTechnologies.Contains(technology))
                {
                    canSteal = true;
                    break;
                }

            }

            return canSteal;
        }
    }
}
