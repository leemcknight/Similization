using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using McKnight.Similization.Client;
using McKnight.Similization.Server;
using McKnight.Similization.Core;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// This is the dialog window that the player is presented with whenever a new 
	/// technology is discovered.  The purpose of this dialog is to inform the player
	/// of the advance, and find out what technology to research next.
	/// </summary>
	public class NewTechnologyDialog : System.Windows.Forms.Form, ITechnologyControl
	{
		private System.Windows.Forms.PictureBox _pictureBox;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblResearch;
		private System.Windows.Forms.ComboBox cboTechnology;
		private System.Windows.Forms.Label lblAdvance;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Intitializes a new instance of the <c>NewTechnologyDialog</c>
		/// </summary>
		public NewTechnologyDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			
		}

		/// <summary>
		/// Gets the Technology that the player chose to research next.
		/// </summary>
		public Technology ChosenTechnology
		{
			get { return this.technology; }
			set { this.technology = value; }
		}

		private Technology technology;

		/// <summary>
		/// Gets or sets the message to show to the user.
		/// </summary>
		public string Message
		{
			get 
			{
				return lblAdvance.Text;
			}

			set
			{
				lblAdvance.Text = value;
			}
		}

		/// <summary>
		/// Shows the Similization Control.
		/// </summary>
		public void ShowSimilizationControl()
		{
			ClientApplication client = ClientApplication.Instance;
			cboTechnology.DataSource = client.Player.ResearchableTechnologies;
			cboTechnology.DisplayMember = "Name";
			cboTechnology.DataBindings.Add("SelectedItem", this, "ChosenTechnology");
			ShowDialog();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewTechnologyDialog));
            this.btnOK = new System.Windows.Forms.Button();
            this.lblResearch = new System.Windows.Forms.Label();
            this.cboTechnology = new System.Windows.Forms.ComboBox();
            this._pictureBox = new System.Windows.Forms.PictureBox();
            this.lblAdvance = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            // 
            // lblResearch
            // 
            resources.ApplyResources(this.lblResearch, "lblResearch");
            this.lblResearch.Name = "lblResearch";
            // 
            // cboTechnology
            // 
            this.cboTechnology.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTechnology.FormattingEnabled = true;
            resources.ApplyResources(this.cboTechnology, "cboTechnology");
            this.cboTechnology.Name = "cboTechnology";
            // 
            // _pictureBox
            // 
            this._pictureBox.BackgroundImage = McKnight.Similization.Client.WindowsForms.Properties.Resources.woman3;
            resources.ApplyResources(this._pictureBox, "_pictureBox");
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.TabStop = false;
            // 
            // lblAdvance
            // 
            resources.ApplyResources(this.lblAdvance, "lblAdvance");
            this.lblAdvance.Name = "lblAdvance";
            // 
            // NewTechnologyDialog
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lblAdvance);
            this.Controls.Add(this._pictureBox);
            this.Controls.Add(this.cboTechnology);
            this.Controls.Add(this.lblResearch);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewTechnologyDialog";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

	}
}
