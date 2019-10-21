using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using LJM.Similization.Client;
using LJM.Similization.Server;
using LJM.Similization.Client.DirectX.Controls;
using LJM.Similization.Client.DirectX.Engine;
using Microsoft.DirectX.DirectInput;

namespace LJM.Similization.Client.DirectX
{
	/// <summary>
	/// DirectX Client implementation of the <see cref="IGameWindow"/> interface.
	/// </summary>
	public class GameWindow : DXWindow, IGameWindow
	{		
		private DXTaskbar taskBar;		
        private Point centerCoordinates;
		private const int CellWidth = 50;
		private const int CellHeight = 50;
		private GameEngine engine;		
		private float percentLoadedScaleFactor = 0;
		private EngineLoadingDialog waitDialog;        
        private System.Drawing.Font cityFont;
        private Color cityFontColor;
        private Unit activeUnit;      

        /// <summary>
        /// Intializes a new instance of the <see cref="GameWindow"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
		public GameWindow(IDirectXControlHost controlHost) : base(controlHost)
		{                                    
            InitializeComponent();
            GameRoot root = ClientApplication.Instance.ServerInstance;            
            root.StatusChanged += new EventHandler<StatusChangedEventArgs>(this.StatusChanged);
            this.ShadeHeader = false;   
		}

		private void InitializeComponent()
		{
            Rectangle bounds = this.ControlHost.ScreenBounds;
			this.Location = new Point(0,0);
            this.Size = bounds.Size;
			this.BackColor = Color.Black;
			this.ForeColor = Color.White;			
            
			//start menu
			this.taskBar = new DXTaskbar(this.ControlHost, this);
            this.taskBar.Location = new Point(0, bounds.Bottom - 100);
            this.taskBar.Size = new Size(bounds.Width, 100);			
			this.Controls.Add(this.taskBar);            
		}

		private void StatusChanged(object sender, StatusChangedEventArgs e)
		{
			int percent = 0;

            if (this.waitDialog == null)
            {
                this.waitDialog = new EngineLoadingDialog(this.ControlHost);
                this.waitDialog.Show();
            }

			if(this.percentLoadedScaleFactor < 1.0f)
				percent = Convert.ToInt32(e.PercentDone * this.percentLoadedScaleFactor);
			else
				percent = 25 + ((75*e.PercentDone)/100);

			this.waitDialog.UpdatePercent(percent, e.Message);
		}

        internal DXTaskbar TaskBar
        {
            get { return this.taskBar; }
        }

        /// <summary>
        /// Handles the user pressing a key in the control.
        /// </summary>
        /// <param name="kea"></param>
		protected override void OnKeyPress(DXKeyboardEventArgs e)
		{
			GameRoot root = GameRoot.Instance;
			base.OnKeyPress(e);
			MoveResult result = MoveResult.MoveSuccess;
			
			if(e.KeyboardState[Key.Up])
				result = root.ActiveUnit.MoveTo(root.ActiveUnit.Coordinates);				
			else if(e.KeyboardState[Key.Down])
				result = root.ActiveUnit.MoveTo(root.ActiveUnit.Coordinates);					
            else if(e.KeyboardState[Key.Left])
				result = root.ActiveUnit.MoveTo(root.ActiveUnit.Coordinates);				
			else if(e.KeyboardState[Key.Right])
				result = root.ActiveUnit.MoveTo(root.ActiveUnit.Coordinates);				
            else if(e.KeyboardState[Key.NumPad7])
				result = root.ActiveUnit.MoveTo(root.ActiveUnit.Coordinates);				
			else if(e.KeyboardState[Key.Home])
				result = root.ActiveUnit.MoveTo(root.ActiveUnit.Coordinates);				
			else if(e.KeyboardState[Key.PageDown])
				result = root.ActiveUnit.MoveTo(root.ActiveUnit.Coordinates);				
			else if(e.KeyboardState[Key.End])
				result = root.ActiveUnit.MoveTo(root.ActiveUnit.Coordinates);				
			else if(e.KeyboardState[Key.S])					
				((Settler)root.ActiveUnit).Settle(string.Empty);
			else if(e.KeyboardState[Key.Space])
                 root.ActivateNextUnit();				

			switch(result)
			{
				case MoveResult.CellTaken:
					break;
				case MoveResult.Killed:
					break;
				case MoveResult.MoveSuccess:
					if(root.ActiveUnit != null)
					{
						if(root.ActiveUnit.MovesLeft == 0)
							root.ActivateNextUnit();
					}
					break;
				case MoveResult.UnreachableTerrain:
					break;
				case MoveResult.UnresolvedCombat:
					break;
			}
		}

		public override void Render(RenderEventArgs e)
		{            
			if(this.engine != null && this.engine.Ready)			
				this.engine.Render();
            foreach (DXControl ctl in this.Controls)
                ctl.Render(e);
		} 

        /// <summary>
        /// Loads the 3D rendering engine into memory and initializes it.
        /// </summary>
		private void LoadEngine()
		{			
			this.waitDialog = new EngineLoadingDialog(this.ControlHost);
            this.waitDialog.Size = new Size(500, 200);
            this.waitDialog.Location = new Point(100, 100);
            this.waitDialog.BackgroundImage = new DXImage(this.ControlHost.Device, @"images\dialog.jpg");
			this.ChildDialog = this.waitDialog;
			this.waitDialog.Show();
			this.percentLoadedScaleFactor = .25F;            
            Grid grid = ClientApplication.Instance.ServerInstance.Grid;
			this.percentLoadedScaleFactor = 1.0F;
			this.engine = new GameEngine(this.ControlHost.Device, grid);
			this.engine.StatusChanged += new EventHandler<StatusChangedEventArgs>(this.StatusChanged);
			this.engine.InitializeEngine(ClientApplication.Instance.Tileset);			
            this.waitDialog.Close();
			this.ChildDialog = null;
		}

		private void ShowDate()
		{
            GameRoot root = ClientApplication.Instance.ServerInstance;
			this.taskBar.StartPanel.Text = Math.Abs(root.Year).ToString() + " " + (root.Year > 0 ? "A.D." : "B.C.");
		}

		/// <summary>
		/// Confirms the specified question or statement with the user.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="title"></param>
		/// <returns></returns>
		public bool GetUserConfirmation(string message, string title)
		{			
            throw new NotImplementedException();
		}

		public void ShowSimilizationControl()
		{
			this.Show();
            LoadEngine();
		}
		        
        public void BeginBombardProcess()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public Point HotCoordinates
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public void ShowMessageBox(string message, string title, Point newCenter)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// The coordinates of the <see cref="GridCell"/> that is currently 
        /// centered on the map.
        /// </summary>
        public Point CenterCoordinates
        {
            get
            {
                return this.centerCoordinates;
            }
            set
            {
                this.centerCoordinates = value;
            }
        }
        
        public bool GoToCursor
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// The font to use when drawing <see cref="City"/> information onto the screen.
        /// </summary>
        public System.Drawing.Font CityNameFont
        {
            get
            {
                return this.cityFont;
            }
            set
            {
                this.cityFont = value;
            }
        }

        /// <summary>
        /// The color of the font when drawing information for cities.
        /// </summary>
        public Color CityNameFontColor
        {
            get
            {
                return this.cityFontColor; 
            }
            set
            {
                this.cityFontColor = value;
            }
        }

        /// <summary>
        /// Shows a message to the user with the specified text and title.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public void ShowMessageBox(string message, string title)
        {
            LJM.Similization.Client.DirectX.Controls.MessageBox.Show(this.ControlHost, message, title);
        }

        /// <summary>
        /// The <see cref="Unit"/> whose turn it currently is.
        /// </summary>
        public Unit ActiveUnit
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
    }
}
