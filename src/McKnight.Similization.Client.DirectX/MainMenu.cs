using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using LJM.Similization.Core;
using LJM.Similization.Client.DirectX.Controls;
using LJM.Similization.Client.DirectX.Sound;
using LJM.Similization.Client;

namespace LJM.Similization.Client.DirectX
{
    /// <summary>
    /// DirectX implementation of the <see cref="IStartingScreen"/> interface.
    /// </summary>
	public class MainMenu : DXWindow, IStartingScreen
	{
        private bool disposed;
		private DXLinkLabel newGameLabel;
		private DXLinkLabel quickStartLabel;
		private DXLinkLabel loadGameLabel;
		private DXLinkLabel tutorialLabel;
		private DXLinkLabel optionsLabel;
		private DXLinkLabel exitLabel;		
		private SoundEffect transitionSound;
        private string rulesetPath;        
		private System.Drawing.Font LabelFont = new System.Drawing.Font("Times New Roman", 12.25F, FontStyle.Bold);

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenu"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
		public MainMenu(IDirectXControlHost controlHost) : base(controlHost)
		{
			InitializeComponent();
            InitializeCommands();
		}

        /// <summary>
        /// Releases all resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (this.disposed && disposing)
                {
                    if (this.loadGameLabel != null)
                        this.loadGameLabel.Dispose();
                    if (this.newGameLabel != null)
                        this.newGameLabel.Dispose();
                    if (this.tutorialLabel != null)
                        this.tutorialLabel.Dispose();
                    if (this.optionsLabel != null)
                        this.optionsLabel.Dispose();
                    if (this.quickStartLabel != null)
                        this.quickStartLabel.Dispose();
                    if (this.exitLabel != null)
                        this.exitLabel.Dispose();
                    if (this.transitionSound != null)
                        this.transitionSound.Dispose();
                    if (this.LabelFont != null)
                        this.LabelFont.Dispose();
                }
            }
            finally
            {
                this.disposed = true;
                base.Dispose(disposing);
            }
        }

        //Initializes the command pattern objects tied to the menu commands.
        private void InitializeCommands()
        {
            NamedObjectCollection<Command> commands = ClientApplication.Instance.Commands;
            this.loadGameLabel.Tag = commands["LoadGameCommand"];
            this.newGameLabel.Tag = commands["StartNewGameCommand"];
            this.optionsLabel.Tag = commands["GameOptionsCommand"];
            this.exitLabel.Tag = commands["ExitGameCommand"];
        }

		private void InitializeComponent()
		{
            Rectangle bounds = this.ControlHost.ScreenBounds;
            int buttonLocationX = bounds.Width / 2;
            buttonLocationX -= 100;

            this.Size = bounds.Size;
            this.Location = bounds.Location;
            this.BackColor = Color.DarkGoldenrod;
            this.BackColor2 = Color.LightGoldenrodYellow;
            this.Text = DirectXClientResources.GameTitle;		
			
            //this.transitionSound = new SoundEffect(@"Sound\select.wav", this.ControlHost.Device);
			this.loadGameLabel = new DXLinkLabel(this.ControlHost, this);
			this.newGameLabel = new DXLinkLabel(this.ControlHost, this);
			this.quickStartLabel = new DXLinkLabel(this.ControlHost, this);
			this.tutorialLabel = new DXLinkLabel(this.ControlHost, this);
			this.optionsLabel = new DXLinkLabel(this.ControlHost, this);
			this.exitLabel = new DXLinkLabel(this.ControlHost, this);

			//New Game Label
			this.newGameLabel.Icon = Icons.NewIcon;
            this.newGameLabel.Text = DirectXClientResources.MainMenuNewGame;
            this.newGameLabel.Font = LabelFont;
            this.newGameLabel.ForeColor = Color.White;
            this.newGameLabel.HoverColor = Color.Yellow;
            this.newGameLabel.Location = new Point(buttonLocationX, 100);
            this.newGameLabel.TextAlignment = TextAlign.Left;
            this.newGameLabel.Click += new EventHandler(LinkClicked);            
            this.newGameLabel.AutoSize = true;
            this.Controls.Add(this.newGameLabel);

			this.quickStartLabel.Icon = Icons.QuickStartIcon;
            this.quickStartLabel.Location = new Point(buttonLocationX, 150);
            this.quickStartLabel.Font = LabelFont;
            this.quickStartLabel.ForeColor = Color.White;
            this.quickStartLabel.HoverColor = Color.Yellow;
            this.quickStartLabel.Text = DirectXClientResources.MainMenuQuickStart;
            this.quickStartLabel.TextAlignment = TextAlign.Left;
            this.quickStartLabel.AutoSize = true;            
            this.Controls.Add(this.quickStartLabel);

			this.tutorialLabel.Icon = Icons.HelpIcon;
            this.tutorialLabel.Text = DirectXClientResources.MainMenuTutorial;
            this.tutorialLabel.Font = LabelFont;
            this.tutorialLabel.TextAlignment = TextAlign.Left;
            this.tutorialLabel.ForeColor = Color.White;
            this.tutorialLabel.HoverColor = Color.Yellow;
            this.tutorialLabel.Location = new Point(buttonLocationX, 200);
            this.tutorialLabel.AutoSize = true;            
            this.Controls.Add(this.tutorialLabel);

			//Load Game Label
			this.loadGameLabel.Location = new Point(buttonLocationX,250);
            this.loadGameLabel.Icon = Icons.LoadIcon;
            this.loadGameLabel.Text = DirectXClientResources.MainMenuLoadGame;
            this.loadGameLabel.Font = LabelFont;
            this.loadGameLabel.HoverColor = Color.Yellow;
            this.loadGameLabel.ForeColor = Color.White;
            this.loadGameLabel.Click += new EventHandler(LinkClicked);
            this.loadGameLabel.TextAlignment = TextAlign.Left;
            this.loadGameLabel.AutoSize = true;            
            this.Controls.Add(this.loadGameLabel);

			//Options Label
			this.optionsLabel.Location = new Point(buttonLocationX, 300);
            this.optionsLabel.Icon = Icons.CreditsIcon;
            this.optionsLabel.Text = DirectXClientResources.MainMenuOptions;
            this.optionsLabel.Font = LabelFont;
            this.optionsLabel.ForeColor = Color.White;
            this.optionsLabel.HoverColor = Color.Yellow;
            this.optionsLabel.TextAlignment = TextAlign.Left;
            this.optionsLabel.AutoSize = true;
            this.optionsLabel.Click += new EventHandler(LinkClicked);
            this.Controls.Add(this.optionsLabel);

			//quit label
			this.exitLabel.Location = new Point(buttonLocationX,350);
            this.exitLabel.Icon = Icons.ExitGameIcon;
            this.exitLabel.Text = DirectXClientResources.MainMenuExitGame;
            this.exitLabel.Font = LabelFont;
            this.exitLabel.ForeColor = Color.White;
            this.exitLabel.HoverColor = Color.Yellow;
            this.exitLabel.TextAlignment = TextAlign.Left;
            this.exitLabel.AutoSize = true;
            this.exitLabel.Click += new EventHandler(LinkClicked);            
            this.Controls.Add(this.exitLabel);			
		}

        private void LinkClicked(object sender, System.EventArgs e)
        {
            DXLinkLabel lbl = (DXLinkLabel)sender;
            Command cmd = lbl.Tag as Command;
            cmd.Invoke();
            Close();
        }
		
        /// <summary>
        /// Shows the <see cref="MainMenu"/> control.
        /// </summary>
		public void ShowSimilizationControl()
		{
			this.Show();
		}

        /// <summary>
        /// The full path the the ruleset to use in the game.
        /// </summary>
		public string RulesetPath
		{
			get { return this.rulesetPath; }
			set { this.rulesetPath = value; }
		}        				
	}
}
