using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Drawing;
using LJM.Similization.Client.DirectX.Controls;
using LJM.Similization.Client;
using LJM.Similization.Server;
using LJM.Similization.Core;

namespace LJM.Similization.Client.DirectX
{
	/// <summary>
	/// DirectX Client implementation of the <see cref="INewGameControl"/> interface.
	/// </summary>
	public class NewGameControl : DXWindow, INewGameControl
	{
		private DXButton nextButton;
		private DXButton backButton;
		private WizardStep wizardStep = WizardStep.WorldSetup;
		private PlayerSetupWindow playerSetup;
		private WorldSetupControl worldSetup;
		private DXWindow activeWindow;
        private event EventHandler resultChosen;    

		private enum WizardStep
		{
			WorldSetup,
			PlayerSetup
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NewGameControl"/> class.
		/// </summary>
        /// <param name="controlHost"></param>
		public NewGameControl(IDirectXControlHost controlHost) : base(controlHost)
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
            this.Size = this.ControlHost.ScreenBounds.Size;
			this.Location = new Point(0,0);
            
            this.BackColor = Color.DarkGoldenrod;
            this.BackColor2 = Color.LightGoldenrodYellow;

			//
			//worldSetup
			//
			this.worldSetup = new WorldSetupControl(this.ControlHost);
            this.worldSetup.Size = new Size(this.Size.Width, 600);

			//
			//playerSetup
			//
			this.playerSetup = new PlayerSetupWindow(this.ControlHost);
			this.playerSetup.Size = this.Size;
            this.playerSetup.Size = new Size(this.Size.Width, 600);

			this.nextButton = new DXButton(this.ControlHost, this);
			this.backButton = new DXButton(this.ControlHost, this);

			this.nextButton.Text = "Next >";
			this.nextButton.Size = new Size(75,25);
			this.nextButton.Click += new System.EventHandler(HandleNextButtonPressed);
			this.nextButton.Location = new Point(400,650);
			this.Controls.Add(this.nextButton);
			
			this.backButton.Text = "< Back";
			this.backButton.Size = new Size(75,25);
            this.backButton.Click += new System.EventHandler(HandleBackButtonPressed);
            this.backButton.Location = new Point(300, 650);
            this.Controls.Add(this.backButton);

			SwapControl(this.worldSetup);
		}

		private void HandleNextButtonPressed(object sender, System.EventArgs e)
		{
			switch(this.wizardStep)
			{
				case WizardStep.WorldSetup:
					this.wizardStep = WizardStep.PlayerSetup;					
					SwapControl(this.playerSetup);
					break;
				case WizardStep.PlayerSetup:					
                    Dispose();
                    OnResultChosen();
					break;
			}
		}

        private void OnResultChosen()
        {
            if (this.resultChosen != null)
                this.resultChosen(this, EventArgs.Empty);
        }

		private void HandleBackButtonPressed(object sender, System.EventArgs e)
		{
			switch(this.wizardStep)
			{
				case WizardStep.PlayerSetup:
					this.wizardStep = WizardStep.WorldSetup;
					SwapControl(this.worldSetup);
					break;
			}
		}

		private void SwapControl(DXWindow control)
		{
			if(this.activeWindow != null)			
				this.activeWindow.Hide();			

			this.activeWindow = control;

			if(!this.Controls.Contains(control))
			{
				this.activeWindow.Parent = this;
				this.Controls.Add(this.activeWindow);
			}

			this.activeWindow.Show();
		}

        /// <summary>
        /// Occurs when the user is finished setting up the parameters for a new game.
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
		/// The <see cref="Civilization"/> the player has chosen to represent.
		/// </summary>
		public Civilization ChosenCivilization
		{
			get { return this.playerSetup.Civilization; }
		}
		
		/// <summary>
		/// The Size of the Map to play on.
		/// </summary>
        public WorldSize WorldSize
        {
            get { return this.worldSetup.WorldSize; }
        }
                                      
		/// <summary>
		/// The name of the player.
		/// </summary>
		public string LeaderName
		{
            get { return this.playerSetup.LeaderName; }
		}
		
		/// <summary>
		/// The Color the player wants to be.
		/// </summary>
		public Color PlayerColor
		{
			get { return Color.Blue; }
		}
		
		/// <summary>
		/// The Civilizations the player wants to play against.
		/// </summary>
        public NamedObjectCollection<Civilization> ChosenOpponents
		{
			get { return this.playerSetup.Opponents; }
		}

        /// <summary>
        /// The size of the land masses on the world.
        /// </summary>
		public Landmass Landmass
		{
			get { return this.worldSetup.Landmass; }
		}

        /// <summary>
        /// Indicates how aggressive the barbarians will be during gameplay.
        /// </summary>
        public BarbarianAggressiveness BarbarianAggressiveness
        {
            get { return this.worldSetup.BarbarianAggressiveness; }
        }

        /// <summary>
        /// The overall temperature of the world the game will play on.
        /// </summary>
		public Temperature Temperature
		{
			get 
            {
                return this.worldSetup.Temperature;
            }
		}

        /// <summary>
        /// Amount of water on the map.
        /// </summary>
		public WaterCoverage WaterCoverage
		{
			get { return this.worldSetup.WaterCoverage; }
		}

        /// <summary>
        /// Age of the world to play on.
        /// </summary>
		public Age Age
		{
			get { return this.worldSetup.Age; }
		}

        /// <summary>
        /// Rules governing the way the game is played.
        /// </summary>
		public Rules Rules
		{
			get { return this.playerSetup.Rules; }
		}

        /// <summary>
        /// Overall climate of the world to play on.
        /// </summary>
		public Climate Climate
		{
			get { return this.worldSetup.Climate; }
		}

        /// <summary>
        /// The level of difficulty to use during gameplay.
        /// </summary>
        public Difficulty Difficulty
        {
            get { return this.playerSetup.Difficulty; }
        }

		/// <summary>
		/// Shows the Control.
		/// </summary>
		public void ShowSimilizationControl()
		{
			this.Show();
		}	
          
    }
}