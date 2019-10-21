using System;
using System.Collections.Generic;
using System.Text;
using McKnight.Similization.Core;
using McKnight.Similization.Server;
using System.Collections.ObjectModel;

namespace McKnight.Similization.Client
{
    /// <summary>
    /// Class representing any particular espionage command.
    /// </summary>
    /// <remarks>This class is abstract and cannot be invoked directly.</remarks>
    public abstract class EspionageCommand : Command
    {
        private City city;
        private DiplomaticTie tie;

        /// <summary>
        /// Gets the <see cref="McKnight.Similization.Server.City"/>, and if necessary, 
        /// the <see cref="McKnight.Similization.Server.DiplomaticTie"/> from the user.
        /// </summary>
        protected virtual void SelectCity(string cityPickerTitle)
        {
            ClientApplication ca = ClientApplication.Instance;
            
            IDiplomaticTiePicker picker = (IDiplomaticTiePicker)ca.GetControl(typeof(IDiplomaticTiePicker));
            picker.PickerTitle = ClientResources.GetString("espionageTiePickerTitle");
            Collection<DiplomaticTie> ties = new Collection<DiplomaticTie>();
            foreach (DiplomaticTie t in ca.Player.DiplomaticTies)
            {
                //TODO:  account for needed improvements
                if (t.HasEmbassy)
                    ties.Add(t);
            }            
            picker.InitializePicker(ties);
            picker.ShowSimilizationControl();
            this.tie = picker.DiplomaticTie;
        
            ICityPicker cityPicker = (ICityPicker)ca.GetControl(typeof(ICityPicker));
            cityPicker.PickerTitle = cityPickerTitle;
            cityPicker.InitializePicker(this.DiplomaticTie.ForeignCountry);
            cityPicker.ShowSimilizationControl();
            this.city = cityPicker.City;
            
        }

        /// <summary>
        /// The <see cref="DiplomaticTie"/> that we are using to perform 
        /// the sabotage.
        /// </summary>
        public DiplomaticTie DiplomaticTie
        {
            get { return this.tie; }
            set { this.tie = value; }
        }

        /// <summary>
        /// The <see cref="McKnight.Similization.Server.City"/> to sabotage 
        /// the production of.
        /// </summary>
        public City City
        {
            get { return this.city; }
            set { this.city = value; }
        }
    }
}
