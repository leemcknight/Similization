using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using McKnight.Similization.Client;
using McKnight.Similization.Core;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// The Welcome Screen for the Windows Client version of Similization.
	/// </summary>
	public class WelcomeControl : UserControl, IStartingScreen
	{		
		private System.Windows.Forms.PictureBox pbMain;
		private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnNewGame;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Button btnQuit;
		private System.Windows.Forms.Button btnOptions;
		private System.Windows.Forms.Button btnLoadGame;
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="WelcomeControl"/> class.
		/// </summary>
		public WelcomeControl()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            InitializeCommands();
		}

        private void InitializeCommands()
        {
            NamedObjectCollection<Command> commands = ClientApplication.Instance.Commands;
            this.btnLoadGame.Tag = commands["LoadGameCommand"];
            this.btnNewGame.Tag = commands["StartNewGameCommand"];
            this.btnOptions.Tag = commands["GameOptionsCommand"];
            this.btnQuit.Tag = commands["QuitGameCommand"];
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

		/// <summary>
		/// Shows the Similization control
		/// </summary>
		public void ShowSimilizationControl()
		{
			ClientApplication client = ClientApplication.Instance;
			((WindowsClientApplication)client).LoadGameControl(this);
		}

        private void ButtonClicked(object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            Command cmd = (Command)button.Tag;
            cmd.Invoke();
        }

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeControl));
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.btnLoadGame = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMain
            // 
            resources.ApplyResources(this.pbMain, "pbMain");
            this.pbMain.Name = "pbMain";
            this.pbMain.TabStop = false;
            // 
            // lblHeader
            // 
            resources.ApplyResources(this.lblHeader, "lblHeader");
            this.lblHeader.Name = "lblHeader";
            // 
            // btnNewGame
            // 
            this.btnNewGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnNewGame, "btnNewGame");
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Click += new System.EventHandler(ButtonClicked);
            // 
            // btnLoadGame
            // 
            this.btnLoadGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnLoadGame, "btnLoadGame");
            this.btnLoadGame.Name = "btnLoadGame";
            this.btnLoadGame.Click += new System.EventHandler(ButtonClicked);
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // btnQuit
            // 
            this.btnQuit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnQuit, "btnQuit");
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Click += new System.EventHandler(ButtonClicked);
            // 
            // btnOptions
            // 
            this.btnOptions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnOptions, "btnOptions");
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Click += new System.EventHandler(ButtonClicked);
            // 
            // WelcomeControl
            // 
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnLoadGame);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.pbMain);
            resources.ApplyResources(this, "$this");
            this.Name = "WelcomeControl";
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

	}
}
