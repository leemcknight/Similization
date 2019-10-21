using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Text.RegularExpressions; 
using LJM.Similization.Client.DirectX.Controls;
using LJM.Similization.Server;

namespace LJM.Similization.Client.DirectX
{
	/// <summary>
	/// Class representing DirectX TaskBar
	/// </summary>
	public class DXTaskbar : DXContainerControl, ISimilizationStatusView
	{
        private string year;
        private string movesLeft;
        private string technology;
        private string terrain;
        private string gold;
        private string activeUnit;
        private string government;
        private Image unitImage;
		private DXButton startButton;
		private DXMenu startMenu;
		private DXMenuItem quitItem;
		private DXMenuItem quickSaveItem;
		private DXMenuItem saveItem;
		private DXMenuItem helpItem;
		private DXPanel panel;		
		private event EventHandler quitGame;

        /// <summary>
        /// Initializes a new instance of the <see cref="DXTaskbar"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
		public DXTaskbar(IDirectXControlHost controlHost) : base(controlHost)
		{
			
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="DXTaskbar"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
		public DXTaskbar(IDirectXControlHost controlHost, DXControl parent) : base(controlHost)
		{						
			this.Parent = parent;			
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			//internal properties			            
			this.ForeColor = Color.White;						
            this.BackColor = Color.FromArgb(100, Color.LightGoldenrodYellow);
            this.BackColor2 = Color.FromArgb(100, Color.DarkGoldenrod);
            this.Size = new Size(800, 150);
            this.Location = new Point(0, 575);

			//start button
			this.startButton = new DXButton(this.ControlHost, this);
            this.startButton.Text = DirectXClientResources.StartButtonText;
            this.startButton.Size = new Size(50, 20);            
            this.startButton.Location = new Point(10, 10);
            this.startButton.Click += new System.EventHandler(this.StartMenuPressed);
            this.Controls.Add(this.startButton);

            //start menu
            this.startMenu = new DXMenu(this.ControlHost, this.Parent);
            this.startMenu.Location = new Point(2, this.Parent.Size.Height - this.startMenu.Size.Height - this.Size.Height - 5);
            this.startMenu.HighlightColor = Color.White;
            this.startMenu.BackColor = Color.Tan;
            this.startMenu.BackColor2 = Color.Tan;
            this.startMenu.Hide();
            ((DXContainerControl)Parent).Controls.Add(this.startMenu);
			
			//start panel
			this.panel = new DXPanel(this.ControlHost, this);
            this.panel.Location = new Point(200, 2);			
			this.panel.Size = new Size(100, 24);
			this.Controls.Add(this.panel);
			
			//quick save item
			this.quickSaveItem = new DXMenuItem(this.ControlHost, this.startMenu);
            this.quickSaveItem.Text = DirectXClientResources.MenuItemQuickSave;
            this.quickSaveItem.Clicked += new System.EventHandler(this.OnQuickSave);
            this.quickSaveItem.Icon = Icons.SaveIcon;
            this.startMenu.MenuItems.Add(this.quickSaveItem);

			//save Item
			this.saveItem = new DXMenuItem(this.ControlHost, this.startMenu);
            this.saveItem.Text = DirectXClientResources.MenuItemSaveGame;
            this.saveItem.Clicked += new System.EventHandler(this.OnSave);
            this.saveItem.Icon = Icons.SaveIcon;
            this.startMenu.MenuItems.Add(this.saveItem);

			//help Item
			this.helpItem = new DXMenuItem(this.ControlHost, this.startMenu);
            this.helpItem.Text = DirectXClientResources.MenuItemHelp;
            this.helpItem.Clicked += new System.EventHandler(this.OnHelp);
            this.helpItem.Icon = Icons.HelpIcon;
            this.startMenu.MenuItems.Add(this.helpItem);

			//quitItem
			this.quitItem = new DXMenuItem(this.ControlHost, this.startMenu);
            this.quitItem.Text = DirectXClientResources.MenuItemQuitToDesktop;
			this.quitItem.Clicked += new System.EventHandler(this.OnMenuQuit);
			this.quitItem.Icon = Icons.ExitGameIcon;
			this.startMenu.MenuItems.Add(this.quitItem);            
		}

        /// <summary>
        /// Renders the <see cref="DXTaskBar"/>
        /// </summary>
        public override void Render(RenderEventArgs e)
        {
            base.Render(e);
            CustomPainters.DrawBoundingRectangle(e.ControlHost, this.ScreenBounds, Color.White);
        }

		private void OnMenuQuit(object sender, System.EventArgs e)
		{
			if(this.quitGame != null)			
				this.quitGame(this, EventArgs.Empty);									
		}

        /// <summary>
        /// Occurs when the user selects to quit the game from the menu 
        /// on the <see cref="Taskbar"/>.
        /// </summary>
		public event EventHandler QuitGame
		{
			add
			{
				this.quitGame += value; 
			}

			remove
			{
				this.quitGame -= value; 
			}
		}

		private void OnSave(object sender, System.EventArgs e)
		{
            throw new NotImplementedException();
		}

		private void OnQuickSave(object sender, System.EventArgs e)
		{
            throw new NotImplementedException();
		}

		private void OnHelp(object sender, System.EventArgs e)
		{
            throw new NotImplementedException();
		}

		private void StartMenuPressed(object sender, System.EventArgs e)
		{
            if (this.startMenu.Visible)
                this.startMenu.Hide();
            else
            {
                this.startMenu.Location = new Point(0, this.Parent.Height - this.startMenu.Height - this.Height);
                this.startMenu.Show();
            }
		}

        /// <summary>
        /// The <see cref="DXPanel"/> that is used for the start button.
        /// </summary>
		public DXPanel StartPanel
		{
			get { return this.panel; }
		}

        /// <summary>
        /// Shows the <see cref="Taskbar"/>.
        /// </summary>
		public override void Show()
		{
			base.Show();
			this.startMenu.Hide();
		}
        
        public string Year
        {
            get
            {
                return this.year;
            }
            set
            {
                this.year = value;
            }
        }

        public string MovesLeft
        {
            get
            {
                return this.movesLeft;
            }
            set
            {
                this.movesLeft = value;
            }
        }

        public string Technology
        {
            get
            {
                return this.technology;
            }
            set
            {
                this.technology = value;
            }
        }

        public string Terrain
        {
            get
            {
                return this.terrain;
            }
            set
            {
                this.terrain = value;
            }
        }

        public string Gold
        {
            get
            {
                return this.gold;
            }
            set
            {
                this.gold = value;
            }
        }

        public string ActiveUnit
        {
            get
            {
                return this.activeUnit;
            }
            set
            {
                this.activeUnit = value;
            }
        }

        public string Government
        {
            get
            {
                return this.government;
            }
            set
            {
                this.government = value;
            }
        }

        public Image UnitImage
        {
            get
            {
                return this.unitImage;
            }
            set
            {
                this.unitImage = value;
            }
        }        
    }
}
