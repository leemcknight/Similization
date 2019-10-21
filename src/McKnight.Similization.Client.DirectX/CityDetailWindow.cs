using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using LJM.Similization.Server;
using LJM.Similization.Client.DirectX.Controls;

namespace LJM.Similization.Client.DirectX
{
	/// <summary>
	/// DirectX Window showing the user detailed information about 
    /// a <see cref="City"/>.
	/// </summary>
	public class CityDetailWindow : DXWindow, ICityControl
	{
		private City city;		
		private DXLabel lblCity;
		private DXButton btnDone;
		private DXLabel lblNextProjectHDR;
		private DXLinkLabel lblNextProject;
		private DXLabel lblProgressHDR;
		private DXLabel lblProgress;
		private DXLabel lblImprovements;
		private DXListBox lstImprovements;

        /// <summary>
        /// Initializes a new instance of the <see cref="CityDetailWindow"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
		public CityDetailWindow(IDirectXControlHost controlHost) 
            :base(controlHost)
		{
			
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="CityDetailWindow"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
        /// <param name="city"></param>
		public CityDetailWindow(IDirectXControlHost controlHost, DXWindow parent, City city)
            :base(controlHost)
		{
			this.city = city;
			this.Parent = parent;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.Size = new Size(500,500);
			this.Font = SystemInformation.MenuFont;
			this.BackColor = SystemColors.Control;
			this.ForeColor = SystemColors.ActiveCaption;
			this.Text = "City Details";			

			lblCity = new DXLabel(this.ControlHost, this);
			lblCity.Text = city.Name;
			lblCity.Location = new Point(50,50);
			this.Controls.Add(lblCity);

			lblNextProjectHDR = new DXLabel(this.ControlHost, this);
			lblNextProjectHDR.Text = "Producing:";
			lblNextProjectHDR.Location = new Point(10, 75);
			this.Controls.Add(lblNextProjectHDR);

			lblNextProject = new DXLinkLabel(this.ControlHost, this);
			lblNextProject.Text = city.NextImprovement.Name;
			lblNextProject.Location = new Point(100, 75);
			this.Controls.Add(lblNextProject);

			lblProgressHDR = new DXLabel(this.ControlHost, this);
			lblProgressHDR.Text = "Progress:";
			lblProgressHDR.Location = new Point(10, 110);
			this.Controls.Add(lblProgressHDR);

			lblProgress = new DXLabel(this.ControlHost, this);
			lblProgress.Text = city.Shields.ToString() + "/" + city.NextImprovement.Cost.ToString();
			lblProgress.Location = new Point(100, 110);
			this.Controls.Add(lblProgress);

			lblImprovements = new DXLabel(this.ControlHost, this);
			lblImprovements.Text = "Improvements:";
			lblImprovements.Location = new Point(10, 145);
			this.Controls.Add(lblImprovements);

			lstImprovements = new DXListBox(this.ControlHost, this);
			lstImprovements.Location = new Point(100, 145);
			lstImprovements.Size = new Size(200, 300);
			this.Controls.Add(lstImprovements);

			btnDone = new DXButton(this.ControlHost, this);
			btnDone.Text = "Done";
			btnDone.Location = new Point(400,450);
			btnDone.Size = new Size(75,25);
			btnDone.Click += new EventHandler(this.DonePressed);
			this.Controls.Add(btnDone);
		}

		private void DonePressed(object sender, System.EventArgs e)
		{
			this.Parent.SetFocus();
			this.Hide();
		}

        /// <summary>
        /// The <see cref="City"/> being displayed to the user.
        /// </summary>
		public City City
		{
			get { return this.city; }
			set { this.city = value; }
		}

		private void InternalUpdate()
		{
			if(city != null)			
				lblCity.Text = city.Name;						
		}
        
        public bool Editable
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
        
        

        public void ShowSimilizationControl()
        {
            this.Show();
        }
      
    }
}
