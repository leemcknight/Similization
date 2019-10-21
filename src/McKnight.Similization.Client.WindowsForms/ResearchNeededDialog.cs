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
	/// This is the dialog that is shown to the user when they go for 
	/// a couple of turns without a technology being researched.  This
	/// dialog alerts them that they are not currently researching anyting
	/// and prompts them to select a technology to research.
	/// </summary>
	public class ResearchNeededDialog : System.Windows.Forms.Form, IResearchNeededControl
	{
		private System.Windows.Forms.Label _instructionsLabel;
		private System.Windows.Forms.ComboBox _technologyCombo;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button _okButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="ResearchNeededDialog"/> class.
		/// </summary>
		public ResearchNeededDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			FillTechCombo();
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

		
		// Databinds the players' researchable technologies to the 
		// technology combobox.
		private void FillTechCombo()
		{
			ClientApplication client = ClientApplication.Instance;

			Country c = client.Player;

			_technologyCombo.DataSource = c.ResearchableTechnologies;
		}

		/// <summary>
		/// Gets the technology that the player has chosen to research next.
		/// </summary>
		public Technology ChosenTechnology
		{
			get { return _technologyCombo.SelectedItem as Technology; }
		}

		/// <summary>
		/// Gets or sets the message to display to the user.
		/// </summary>
		public string Message 
		{
			get { return _instructionsLabel.Text; }
			set { _instructionsLabel.Text = value; }
		}

		/// <summary>
		/// Shows the Similization control.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResearchNeededDialog));
            this._okButton = new System.Windows.Forms.Button();
            this._instructionsLabel = new System.Windows.Forms.Label();
            this._technologyCombo = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // _okButton
            // 
            this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this._okButton, "_okButton");
            this._okButton.Name = "_okButton";
            // 
            // _instructionsLabel
            // 
            resources.ApplyResources(this._instructionsLabel, "_instructionsLabel");
            this._instructionsLabel.Name = "_instructionsLabel";
            // 
            // _technologyCombo
            // 
            this._technologyCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._technologyCombo.FormattingEnabled = true;
            resources.ApplyResources(this._technologyCombo, "_technologyCombo");
            this._technologyCombo.Name = "_technologyCombo";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = McKnight.Similization.Client.WindowsForms.Properties.Resources.woman3;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // ResearchNeededDialog
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this._technologyCombo);
            this.Controls.Add(this._instructionsLabel);
            this.Controls.Add(this._okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResearchNeededDialog";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
	}
}
