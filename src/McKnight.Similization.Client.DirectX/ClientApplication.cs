using System;
using System.Collections;
using System.Windows.Forms;
using LJM.Similization.Client;
using LJM.Similization.Client.DirectX.Engine;
using LJM.Similization.Client.DirectX.Controls;
using LJM.Similization.Client.DirectX.Sound;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX
{

	/// <summary>
	/// DirectX Client implementation of the <see cref="LJM.Similization.Client.ClientApplication"/> class.
	/// </summary>
	public class DirectXClientApplication : ClientApplication
	{
		private GameWindow gameWindow;
		private HostForm hostForm;
		private ControlHost controlHost;
		private Jukebox jukeBox;
       
		/// <summary>
		/// Entry point of the application.
		/// </summary>
		public static void Main()
		{
			client = new DirectXClientApplication();
			client.Start();
		}

		private void DoMainLoop()
		{
			while(this.hostForm != null && this.hostForm.Created)
			{
				this.controlHost.Refresh();
				Application.DoEvents();
			}
		}

        /// <summary>
        /// Gets an instance of the <see cref="GraphicsMain"/> class.
        /// </summary>
        /// <returns></returns>
		public ControlHost CreateGraphics()
		{
			return this.controlHost;
		}

        /// <summary>
        /// The <see cref="JukeBox"/> responsible for playing songs in the game.
        /// </summary>
		public Jukebox JukeBox
		{
			get { return this.jukeBox; }
		}

        /// <summary>
        /// The <i>Device</i> used to render graphics.
        /// </summary>
        public Device Device
        {
            get { return this.controlHost.Device; }
        }

		/// <summary>
		/// Starts the DirectX Client.
		/// </summary>
		public override void Start()
		{
			this.hostForm = new HostForm();
            this.controlHost = new ControlHost(this.hostForm);                                 
			this.jukeBox = new Jukebox();
            InitializeClient();
			this.MiniMap = new MiniMap(this.controlHost);            
            this.controlHost.Refresh();
            InitializeConsole(new Console(this.controlHost));			
			base.Start();
			DoMainLoop();
		}

        /// <summary>
        /// Loads the DirectX textures into the <see cref="Tileset"/>.
        /// </summary>
        protected override void LoadTileSetImageData()
        {
            foreach (TileInfo tile in this.Tileset.TerrainTiles.Values)
            {
                tile.TileImage = new Texture(this.controlHost.Device, tile.TileImagePath);
            }
        }

		/// <summary>
		/// Gets the DirectX control that implements the interface with the type supplied.
		/// </summary>
		/// <param name="controlType"></param>
		/// <returns></returns>
		public override ISimilizationControl GetControl(System.Type controlType)
		{
			ISimilizationControl ctl = null;
			
			if( typeof(ILoadGameWindow) == controlType)
			{
				ctl = new LoadGameWindow(this.controlHost);
			}
			else if(controlType == typeof(ISaveGameWindow))
			{
			
			}
            else if (controlType == typeof(ICityControl))
            {
                return new CityDetailWindow(this.controlHost);
            }
            else if (controlType == typeof(IOptionsControl))
            {
                return new OptionsWindow(this.controlHost);
            }
            else if (controlType == typeof(INewCityControl))
            {
                ctl = new NewCityWindow(this.controlHost, this.gameWindow);
            }
            else if (controlType == typeof(INewGameControl))
            {
                ctl = new NewGameControl(this.controlHost);
            }
            else if (controlType == typeof(IHistograph))
            {

            }
            else if (controlType == typeof(ISplashScreen))
            {
                ctl = new SplashScreen(this.controlHost);
            }
            else if (controlType == typeof(IStartingScreen))
            {
                ctl = new MainMenu(this.controlHost);
            }
            else if (controlType == typeof(IImprovementBuiltControl))
            {

            }
            else if (controlType == typeof(IResearchNeededControl))
            {

            }
            else if (controlType == typeof(ITechnologyControl))
            {

            }
            else if (controlType == typeof(IDomesticAdvisorControl))
            {

            }
            else if (controlType == typeof(IDiplomacyControl))
            {

            }
            else if (controlType == typeof(IForeignAdvisorControl))
            {

            }
            else if (controlType == typeof(IHelpControl))
            {

            }
            else if (controlType == typeof(IGameWindow))
            {
                if (this.gameWindow == null)
                    this.gameWindow = new GameWindow(this.controlHost);
                this.StatusView = this.gameWindow.TaskBar;
                return this.gameWindow;
            }
			return ctl;
		}
	}
}