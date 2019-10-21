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
	/// Dialog asking the user to select a new Government.
	/// </summary>
	public class GovernmentSelectionDialog : System.Windows.Forms.Form, IGovernmentSelectionControl
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initializes a new instance of the <c>GovernmentSelectionDialog</c> class.
		/// </summary>
		public GovernmentSelectionDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			PopulateGovernmentCombo();
		}

		private void PopulateGovernmentCombo()
		{
			ClientApplication client = ClientApplication.Instance;
			GameRoot root = client.ServerInstance;

			foreach(Government gov in root.Ruleset.Governments)
			{
				if(!gov.Fallback)
				{
					cboGovernment.Items.Add(gov);
				}
			}
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

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblInstructions;
		private System.Windows.Forms.ComboBox cboGovernment;
		private System.Windows.Forms.Label lblGovernment;
        private PictureBox pictureBox1;

		/// <summary>
		/// Gets or sets the message to show to the user.
		/// </summary>
		public string Message 
		{
			get { return this.lblInstructions.Text; }
			set { this.lblInstructions.Text = value; }
		}

		private Government nextGovernment;

		/// <summary>
		/// Gets or sets the default Government in the dropdown.
		/// </summary>
		public Government NextGovernment
		{
			get { return this.nextGovernment; }
			set 
			{
				this.nextGovernment = value; 
				this.cboGovernment.SelectedItem = this.nextGovernment;
			}
		}

		/// <summary>
		/// Shows the control.
		/// </summary>
		public void ShowSimilizationControl()
		{
			ShowDialog();
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GovernmentSelectionDialog));
            this.btnOK = new System.Windows.Forms.Button();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.cboGovernment = new System.Windows.Forms.ComboBox();
            this.lblGovernment = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this._okButton_Click);
            // 
            // lblInstructions
            // 
            resources.ApplyResources(this.lblInstructions, "lblInstructions");
            this.lblInstructions.Name = "lblInstructions";
            // 
            // cboGovernment
            // 
            this.cboGovernment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGovernment.FormattingEnabled = true;
            resources.ApplyResources(this.cboGovernment, "cboGovernment");
            this.cboGovernment.Name = "cboGovernment";
            // 
            // lblGovernment
            // 
            resources.ApplyResources(this.lblGovernment, "lblGovernment");
            this.lblGovernment.Name = "lblGovernment";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = McKnight.Similization.Client.WindowsForms.Properties.Resources.woman3;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // GovernmentSelectionDialog
            // 
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblGovernment);
            this.Controls.Add(this.cboGovernment);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GovernmentSelectionDialog";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void _okButton_Click(object sender, System.EventArgs e)
		{
			ClientApplication client = ClientApplication.Instance;

			client.Player.Government = (Government)cboGovernment.SelectedItem;
		}
	}
}
