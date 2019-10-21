using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using McKnight.Similization.Client;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// The is the form the user is prompted with anytime they attempt to settle a 
	/// new city.  This simple for asks the user for the name of the new city.
	/// </summary>
	public class NewCityDialog : System.Windows.Forms.Form, INewCityControl
	{
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.Label _cityNameLabel;
        private System.Windows.Forms.TextBox _cityNameTextBox;
		private System.Windows.Forms.Label _textLabel;
        private PictureBox _pictureBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="NewCityDialog"/> windows form.
		/// </summary>
		public NewCityDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			//Set the recommended city name.
			//
			Country player = ClientApplication.Instance.Player;
			_cityNameTextBox.Text = player.CreateNewCityName();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewCityDialog));
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._cityNameLabel = new System.Windows.Forms.Label();
            this._cityNameTextBox = new System.Windows.Forms.TextBox();
            this._textLabel = new System.Windows.Forms.Label();
            this._pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _okButton
            // 
            this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this._okButton, "_okButton");
            this._okButton.Name = "_okButton";
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this._cancelButton, "_cancelButton");
            this._cancelButton.Name = "_cancelButton";
            // 
            // _cityNameLabel
            // 
            this._cityNameLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this._cityNameLabel, "_cityNameLabel");
            this._cityNameLabel.Name = "_cityNameLabel";
            // 
            // _cityNameTextBox
            // 
            resources.ApplyResources(this._cityNameTextBox, "_cityNameTextBox");
            this._cityNameTextBox.Name = "_cityNameTextBox";
            // 
            // _textLabel
            // 
            resources.ApplyResources(this._textLabel, "_textLabel");
            this._textLabel.Name = "_textLabel";
            // 
            // _pictureBox
            // 
            this._pictureBox.Image = McKnight.Similization.Client.WindowsForms.Properties.Resources.worker;
            resources.ApplyResources(this._pictureBox, "_pictureBox");
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.TabStop = false;
            // 
            // NewCityDialog
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this._textLabel);
            this.Controls.Add(this._pictureBox);
            this.Controls.Add(this._cityNameTextBox);
            this.Controls.Add(this._cityNameLabel);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewCityDialog";
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// Gets the name of the new city.
		/// </summary>
		public string CityName
		{
			get { return _cityNameTextBox.Text; }
		}

		/// <summary>
		/// Shows the control.
		/// </summary>
		public void ShowSimilizationControl()
		{
			ShowDialog();
		}
	}
}
