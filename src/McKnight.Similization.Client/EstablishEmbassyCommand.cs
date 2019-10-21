using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using McKnight.Similization.Core;
using McKnight.Similization.Server;
using System.Collections.ObjectModel;

namespace McKnight.Similization.Client
{
    /// <summary>
    /// Class representing a command to establish an embassy within the 
    /// capital of another country.
    /// </summary>
    public class EstablishEmbassyCommand : Command
    {
        private DiplomaticTie tie;

        /// <summary>
        /// Initializes a new instance of the <see cref="EstablishEmbassyCommand"/> class.
        /// </summary>
        public EstablishEmbassyCommand()
        {
            this.Name = "EstablishEmbassyCommand";
        }   

        /// <summary>
        /// Invokes the command.
        /// </summary>
        public override void Invoke()
        {
            OnInvoking();
            ClientApplication ca = ClientApplication.Instance;
            IDiplomaticTiePicker picker = (IDiplomaticTiePicker)ca.GetControl(typeof(IDiplomaticTiePicker));
            picker.PickerTitle = ClientResources.GetString("establishEmbassy_tiePickerTitle");
            Collection<DiplomaticTie> ties = new Collection<DiplomaticTie>();
            foreach (DiplomaticTie t in ca.Player.DiplomaticTies)
            {
                //TODO:  account for needed improvements
                if (!t.HasEmbassy)
                    ties.Add(t);
            }
            if (ties.Count == 0)
            {
                string msg = ClientResources.GetString("establishEmbassy_noTies");
                ca.GameWindow.ShowMessageBox(msg, ClientResources.GetString(StringKey.GameTitle));
                OnCanceled();
            }
            picker.InitializePicker(ties);
            picker.ShowSimilizationControl();
            this.tie = picker.DiplomaticTie;
            this.tie.HasEmbassy = true;
            string text = ClientResources.GetString("establishEmbassy_success");

            text = string.Format(
                        CultureInfo.CurrentCulture, 
                        text, 
                        ca.Player.Government.LeaderTitle,
                        this.tie.ForeignCountry.Civilization.Adjective,
                        ClientResources.GetCitySizeString(this.tie.ForeignCountry.CapitalCity.SizeClass),
                        this.tie.ForeignCountry.CapitalCity.Name
                        );


            ca.GameWindow.ShowMessageBox(text, ClientResources.GetString(StringKey.GameTitle));
            OnInvoked();
        }
        
    }
}
