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
	/// This is the form that is shown to the user whenever a new improvement is built
	/// by a city.
	/// </summary>
	public class ImprovementBuiltDialog : System.Windows.Forms.Form, IImprovementBuiltControl
	{
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _goToCityButton;
		private System.Windows.Forms.Label _announcementLabel;
        private City _city;
        private PictureBox _unitPictureBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initializes a new instance of the <c>frmImprovement</c> class.
		/// </summary>
		public ImprovementBuiltDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}
		
		/// <summary>
		/// Gets or sets the city that built the improvement.
		/// </summary>
		public City City
		{
			get { return _city; }
			set { _city = value; }
		}

		/// <summary>
		/// Shows the Similization Control.
		/// </summary>
		public void ShowSimilizationControl()
		{
			ShowDialog();
		}

		/// <summary>
		/// Gets or sets the message to display to the user.
		/// </summary>
		public string Message 
		{
			get { return _announcementLabel.Text; }
			set { _announcementLabel.Text = value; }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImprovementBuiltDialog));
            this._okButton = new System.Windows.Forms.Button();
            this._goToCityButton = new System.Windows.Forms.Button();
            this._announcementLabel = new System.Windows.Forms.Label();
            this._unitPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._unitPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _okButton
            // 
            this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this._okButton, "_okButton");
            this._okButton.Name = "_okButton";
            // 
            // _goToCityButton
            // 
            resources.ApplyResources(this._goToCityButton, "_goToCityButton");
            this._goToCityButton.Name = "_goToCityButton";
            this._goToCityButton.Click += new System.EventHandler(this._goToCityButton_Click);
            // 
            // _announcementLabel
            // 
            this._announcementLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this._announcementLabel, "_announcementLabel");
            this._announcementLabel.Name = "_announcementLabel";
            // 
            // _unitPictureBox
            // 
            this._unitPictureBox.Image = McKnight.Similization.Client.WindowsForms.Properties.Resources.woman4;
            resources.ApplyResources(this._unitPictureBox, "_unitPictureBox");
            this._unitPictureBox.Name = "_unitPictureBox";
            this._unitPictureBox.TabStop = false;
            // 
            // ImprovementBuiltDialog
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this._unitPictureBox);
            this.Controls.Add(this._announcementLabel);
            this.Controls.Add(this._goToCityButton);
            this.Controls.Add(this._okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImprovementBuiltDialog";
            ((System.ComponentModel.ISupportInitialize)(this._unitPictureBox)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void _goToCityButton_Click(object sender, System.EventArgs e)
		{
			CityDialog cityWindow;

			cityWindow = new CityDialog(_city);

			cityWindow.ShowDialog(this.Owner);
			this.Close();
		}
	}
}
