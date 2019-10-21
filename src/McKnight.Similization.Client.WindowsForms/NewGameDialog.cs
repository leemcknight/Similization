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
	/// Wizard responsible for walking the user through setting up a new game.
	/// </summary>
	public class NewGameWizard : System.Windows.Forms.Form, INewGameControl
	{
		private Icon icon;

		private enum WizardStep
		{
			WorldSetup,
			PlayerSetup
		}

		private WorldSetupControl worldSetup = new WorldSetupControl();
		private PlayerSetupControl playerSetup = new PlayerSetupControl();
		private UserControl currentControl;

		private WizardStep wizardStep = WizardStep.WorldSetup;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Panel pnlTop;
		private System.Windows.Forms.Panel pnlIcon;
		private System.Windows.Forms.Label lblInstructions;
		private System.Windows.Forms.Label lblHeader;
		private System.Windows.Forms.Button btnBack;
		private System.Windows.Forms.Panel pnlMain;
        private Label label1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initializes a new instance of the <c>NewGameWizard</c> windows form.
		/// </summary>
		public NewGameWizard()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			this.worldSetup.Parent = this.pnlMain;
			this.playerSetup.Parent = this.pnlMain;
			this.playerSetup.Hide();
			SwapControl(this.worldSetup);
			this.icon = new Icon(@"images\55.ico");
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewGameWizard));
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlIcon = new System.Windows.Forms.Panel();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNext
            // 
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.btnNext.Name = "btnNext";
            this.btnNext.Click += new System.EventHandler(this._okButton_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.btnCancel.Name = "btnCancel";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.pnlIcon);
            this.pnlTop.Controls.Add(this.lblInstructions);
            this.pnlTop.Controls.Add(this.lblHeader);
            resources.ApplyResources(this.pnlTop, "pnlTop");
            this.pnlTop.Name = "pnlTop";
            // 
            // pnlIcon
            // 
            resources.ApplyResources(this.pnlIcon, "pnlIcon");
            this.pnlIcon.Name = "pnlIcon";
            this.pnlIcon.Paint += new System.Windows.Forms.PaintEventHandler(this._iconPanel_Paint);
            // 
            // lblInstructions
            // 
            resources.ApplyResources(this.lblInstructions, "lblInstructions");
            this.lblInstructions.Name = "lblInstructions";
            // 
            // lblHeader
            // 
            resources.ApplyResources(this.lblHeader, "lblHeader");
            this.lblHeader.Name = "lblHeader";
            // 
            // btnBack
            // 
            resources.ApplyResources(this.btnBack, "btnBack");
            this.btnBack.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.btnBack.Name = "btnBack";
            this.btnBack.Click += new System.EventHandler(this._backButton_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.label1);
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.pnlMain.Name = "pnlMain";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // NewGameWizard
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewGameWizard";
            this.pnlTop.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void _okButton_Click(object sender, System.EventArgs e)
		{
			switch(this.wizardStep)
			{
				case WizardStep.PlayerSetup:
					OnResultChosen();
					DialogResult = DialogResult.OK;
					break;
				case WizardStep.WorldSetup:
					this.wizardStep = WizardStep.PlayerSetup;
					SwapControl(this.playerSetup);
					this.btnBack.Enabled = true;
					break;
			}
		}

		private void SwapControl(UserControl newControl)
		{
			if(this.currentControl != null)
			{
				this.currentControl.Hide();
			}

			this.currentControl = newControl;

			if(this.currentControl != null)
			{
				this.currentControl.Show();
			}
		}

		
		/// <summary>
		/// Gets the civilization the the player chose to play as.
		/// </summary>
		public Civilization ChosenCivilization
		{
			get { return this.playerSetup.ChosenCivilization; }
		}
		

		/// <summary>
		/// Gets a list of computer opponents to play against.
		/// </summary>
		public NamedObjectCollection<Civilization> ChosenOpponents
		{
			get { return this.playerSetup.ChosenOpponents; }
		}

		/// <summary>
		/// Gets the color the player chose for their country.
		/// </summary>
		public Color PlayerColor
		{
			get { return this.playerSetup.PlayerColor; }
		}

		/// <summary>
		/// Gets the name of the human player.
		/// </summary>
		public string LeaderName
		{
			get { return this.playerSetup.LeaderName; }
		}

		/// <summary>
		/// Gets the size of the world to use in the game.
		/// </summary>
		public WorldSize WorldSize
		{
			get { return this.worldSetup.WorldSize; }
		}

        public Difficulty Difficulty
        {
            get { return Difficulty.Chieftain; }
        }

        public BarbarianAggressiveness BarbarianAggressiveness
        {
            get { return BarbarianAggressiveness.Raging; }
        }

		/// <summary>
		/// Gets the Age of the world to use in the game.
		/// </summary>
		public Age Age
		{
			get { return this.worldSetup.Age; }
		}

		/// <summary>
		/// Gets the Climate for the world to use in the game.
		/// </summary>
		public Climate Climate
		{
			get { return this.worldSetup.Climate; }
		}

		/// <summary>
		/// Gets the size of the land masses on the world to use in the game.
		/// </summary>
		public Landmass Landmass
		{
			get { return this.worldSetup.Landmass; }
		}

		/// <summary>
		/// Gets the average temperature of the world to use in the game.
		/// </summary>
		public Temperature Temperature
		{
			get { return this.worldSetup.Temperature; }
		}

		/// <summary>
		/// Gets the amount of water on the world to use in the game.
		/// </summary>
		public WaterCoverage WaterCoverage
		{
			get { return this.worldSetup.WaterCoverage; }
		}

		/// <summary>
		/// Gets the rules the player wants to use on the game.
		/// </summary>
		public Rules Rules
		{
			get { return this.playerSetup.Rules; }
		}

		/// <summary>
		/// Shows the control.
		/// </summary>
		public void ShowSimilizationControl()
		{
			ShowDialog();
		}

		private event EventHandler resultChosen;

		/// <summary>
		/// Occurs when the player has chosen all of the game parameters.
		/// </summary>
		public event EventHandler ResultChosen
		{
			add
			{
				this.resultChosen += value;
			}

			remove
			{
				this.resultChosen -= value;
			}
		}

		/// <summary>
		/// Fires the <c>ResultChosen</c> event.
		/// </summary>
		protected virtual void OnResultChosen()
		{
			if(this.resultChosen != null)
			{
				this.resultChosen(this,EventArgs.Empty);
			}
		}

		private void _backButton_Click(object sender, System.EventArgs e)
		{
			if(this.wizardStep == WizardStep.PlayerSetup)
			{
				this.wizardStep = WizardStep.WorldSetup;
				SwapControl(this.worldSetup);
				this.btnBack.Enabled = false;
			}
		}

		private void _iconPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.DrawIcon(this.icon,0,0);
		}
	}
}
