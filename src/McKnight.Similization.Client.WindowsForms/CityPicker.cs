using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using McKnight.Similization.Client;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
    /// <summary>
    /// Windows Forms implementation of the <see cref="McKnight.Similization.Client.ICityPicker"/> 
    /// interface.
    /// </summary>
    public partial class CityPicker : Form, ICityPicker
    {      
        /// <summary>
        /// Initializes a new instance of the <see cref="CityPicker"/> class.
        /// </summary>
        public CityPicker()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes the city list with the cities of the specified <see cref="McKnight.Similization.Server.Country"/>.
        /// </summary>
        /// <param name="country"></param>
        public void InitializePicker(Country country)
        {
            if (country == null)
                throw new ArgumentNullException("country");
            this.cboCity.DataSource = country.Cities;
            this.cboCity.DisplayMember = "Name";
        }

        /// <summary>
        /// The <see cref="McKnight.Similization.Server.City"/> that the user has selected.
        /// </summary>
        public City City
        {
            get
            {
                return (City)this.cboCity.SelectedItem;
            }            
        }

        /// <summary>
        /// Shows the control.
        /// </summary>
        public void ShowSimilizationControl()
        {
            this.ShowDialog();
        }

        /// <summary>
        /// The instructions telling the user to select a <see cref="McKnight.Similization.Server.City"/> from the list.
        /// </summary>
        public string PickerTitle
        {
            get { return this.lblInstructions.Text; }
            set { this.lblInstructions.Text = value; }
        }
    }
}