using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using LJM.Similization.Client.DirectX.Controls;
using LJM.Similization.Client;

namespace LJM.Similization.Client.DirectX
{
	/// <summary>
	/// Summary description for newCityWindow.
	/// </summary>
	public class NewCityWindow : DXWindow, INewCityControl
	{
		private DXLabel _lblCityName;
		private DXTextBox _txtCityName;
		private DXButton _btnOK;
		private DXButton _btnCancel;
		private DialogResult _res;

		public event EventHandler Canceled;
		public event EventHandler Confirmed;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewCityWindow"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
		public NewCityWindow(IDirectXControlHost controlHost, DXWindow parent) : base(controlHost)
		{
			this.Parent = parent;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.Location = new Point(50,50);
			this.Size = new Size(500,200);
			this.BackColor = SystemColors.Control;
			this.ForeColor = SystemColors.ActiveCaption;
			this.Text = "Name your new city...";

			_lblCityName = new DXLabel(this.ControlHost, this);
			_lblCityName.Text = "City Name";
			_lblCityName.Font = new System.Drawing.Font("Arial", 10.25F, FontStyle.Bold);
			_lblCityName.Location = new Point(20,60);
			this.Controls.Add(_lblCityName);
			
			_txtCityName = new DXTextBox(this.ControlHost, this);
			_txtCityName.Location = new Point(80,60);
			_txtCityName.Size = new Size(200,20);
			this.Controls.Add(_txtCityName);

			_btnOK = new DXButton(this.ControlHost, this);
			_btnOK.Text = "OK";
			_btnOK.Location = new Point(285, 160);
			_btnOK.Size = new Size(75, 25);
			_btnOK.Click += new EventHandler(this.OnOK);
			this.Controls.Add(_btnOK);

			_btnCancel = new DXButton(this.ControlHost, this);
			_btnCancel.Text = "Cancel";
			_btnCancel.Location = new Point(375, 160);
			_btnCancel.Size = new Size(75,25);
			_btnCancel.Click += new EventHandler(OnCancel);
			this.Controls.Add(_btnCancel);
		}

		private void OnCancel(object sender, System.EventArgs e)
		{
			_res = DialogResult.Cancel;
			if(Canceled != null)
				Canceled(this,null);
		}

		private void OnOK(object sender, System.EventArgs e)
		{
			_res = DialogResult.OK;
			if(Confirmed != null)
				Confirmed(this, null);
		}

		public string CityName
		{
			get { return _txtCityName.Text; }
		}

		public void ShowSimilizationControl()
		{
			this.Show();
		}
	}
}
