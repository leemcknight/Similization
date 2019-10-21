using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using McKnight.Similization.Core;
using McKnight.Similization.Server;
using System.Collections.ObjectModel;

namespace McKnight.Similization.Client
{
    /// <summary>
    /// Class representing a command to plant a spy in the embassy 
    /// of a foreign country.
    /// </summary>
    /// <remarks>In order to invoke this command, an embassy must exist
    /// in the capital city of the foreign country.</remarks>
    public class PlantSpyCommand : Command
    {
        DiplomaticTie tie;
        /// <summary>
        /// Initializes a new instance of the <see cref="PlantSpyCommand"/> class.
        /// </summary>
        public PlantSpyCommand()
        {
            this.Name = "PlantSpyCommand";
        }

        /// <summary>
        /// Invokes the command.
        /// </summary>
        public override void Invoke()
        {
            OnInvoking();
            ClientApplication ca = ClientApplication.Instance;
            bool hasValidTies = false;            
            IDiplomaticTiePicker picker = (IDiplomaticTiePicker)ca.GetControl(typeof(IDiplomaticTiePicker));
            picker.PickerTitle = ClientResources.GetString("plantSpy_tiePickerTitle");
            Collection<DiplomaticTie> ties = new Collection<DiplomaticTie>();
            foreach (DiplomaticTie t in ca.Player.DiplomaticTies)
            {
                //TODO:  account for needed improvements
                if (t.HasEmbassy)
                {
                    hasValidTies = true;
                    if(!t.HasSpy)
                        ties.Add(t);
                }

            }
            if (ties.Count == 0 && hasValidTies)
            {
                //all the valid civs already have spys.
                string msg = ClientResources.GetString("plantSpy_spysExists");
                ca.GameWindow.ShowMessageBox(msg, ClientResources.GetString(StringKey.GameTitle));
                OnCanceled();
                return;
            }
            picker.InitializePicker(ties);
            picker.ShowSimilizationControl();
            this.tie = picker.DiplomaticTie;            
            EspionageResult result = this.DiplomaticTie.PlantSpy();
            string text = string.Empty;
            switch (result)
            {
                case EspionageResult.Success:
                    text = ClientResources.GetString("plantspy_success");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.DiplomaticTie.ForeignCountry.CapitalCity.Name);
                    break;
                case EspionageResult.Failure:
                    text = ClientResources.GetString("plantspy_failure");
                    text = string.Format(
                        CultureInfo.CurrentCulture,
                        text, 
                        this.DiplomaticTie.ForeignCountry.CapitalCity.Name,
                        this.DiplomaticTie.ForeignCountry.Government.LeaderTitle,
                        this.DiplomaticTie.ForeignCountry.LeaderName);
                    break;                
            }
            IGameWindow gw = ClientApplication.Instance.GameWindow;
            gw.ShowMessageBox(text, ClientResources.GetString(StringKey.GameTitle));
            OnInvoked();
        }

        /// <summary>
        /// The <see cref="McKnight.Similization.Server.DiplomaticTie"/> that exists 
        /// between the two countries.
        /// </summary>
        public DiplomaticTie DiplomaticTie
        {
            get { return this.tie; }
            set { this.tie = value; }
        }
    }
}
