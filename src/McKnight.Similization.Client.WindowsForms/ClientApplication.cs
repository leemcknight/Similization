using System;
using System.Windows.Forms;
using McKnight.Similization.Client;
using McKnight.Similization.Server;
using System.Drawing;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// The main Windows Client class.
	/// </summary>
	public class WindowsClientApplication : ClientApplication
	{
		private static bool started;
		private MainForm mainForm;
		/// <summary>
		/// Entry point of the application.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			WindowsClientApplication.client = new WindowsClientApplication();
            Application.EnableVisualStyles();
			client.Start();
		}

		/// <summary>
		/// Initializes a new instance of the <c>ClientApplication</c> class.
		/// </summary>
		internal WindowsClientApplication() : base()
		{
			
		}

		/// <summary>
		/// The Main Form
		/// </summary>
		public MainForm HostForm
		{
			get { return this.mainForm; }
		}


		/// <summary>
		/// Starts the game.
		/// </summary>
		public override void Start()
		{
			if(started)
				return;
            InitializeClient();
			started = true;
			this.mainForm = new MainForm();
			InitializeConsole(this.mainForm.Console);
			this.StatusView = this.mainForm.StatusView;
			this.MiniMap = ((WindowsClientConsole)this.mainForm.Console).MiniMap;
			this.mainForm.Show();
			base.Start();
			Application.Run(this.mainForm);
		}

        /// <summary>
        /// Loads the Windows Client Image information into the <see cref="Tileset"/>.
        /// </summary>
        protected override void LoadTileSetImageData()
        {
            foreach (TileInfo tileInfo in this.Tileset.TerrainTiles.Values)
                tileInfo.TileImage = Image.FromFile(tileInfo.TileImagePath);
        }

		/// <summary>
		/// Gets an <c>ISimilizationControl</c> that is of the specified type.
		/// </summary>
		/// <param name="controlType">A <c>System.Type</c> that refers to the 
		/// type of interface that the control should implement.</param>
		/// <returns>A reference to an Windows Forms control that implements the 
		/// specified interface</returns>
		/// <remarks>This method controls the mapping between the generic form 
		/// and dialog interfaces declared in the Similization Client and the 
		/// concrete windows forms classes that implement those interfaces.  This 
		/// method is an override of the same function in the more generic Similization 
		/// Client, so this method is mostly called from code in the SimilizationClient.dll 
		/// assembly.</remarks>
		public override ISimilizationControl GetControl(System.Type controlType)
		{
			ISimilizationControl ctl = null;
			
			if( typeof(ILoadGameWindow) == controlType)
			{
				ctl = new LoadGameWindow();
			}
			else if(controlType == typeof(ISaveGameWindow))
			{
				ctl = new SaveGameWindow();
			}
            else if (controlType == typeof(IAboutBox))
            {
                ctl = new AboutBox();
            }
            else if (controlType == typeof(IOptionsControl))
            {
                ctl = new OptionsDialog();
            }
            else if (controlType == typeof(INewCityControl))
            {
                ctl = new NewCityDialog();
            }
            else if (controlType == typeof(INewGameControl))
            {
                ctl = new NewGameWizard();
            }
            else if (controlType == typeof(IHistograph))
            {
                ctl = new HistographDialog(ClientApplication.Instance.ServerInstance.History);
            }
            else if (controlType == typeof(IStartingScreen))
            {
                ctl = new WelcomeControl();
            }
            else if (controlType == typeof(IImprovementBuiltControl))
            {
                ctl = new ImprovementBuiltDialog();
            }
            else if (controlType == typeof(IResearchNeededControl))
            {
                ctl = new ResearchNeededDialog();
            }
            else if (controlType == typeof(ITechnologyControl))
            {
                ctl = new NewTechnologyDialog();
            }
            else if (controlType == typeof(IDomesticAdvisorControl))
            {
                AdvisorDialog dlg = new AdvisorDialog(Advisor.DomesticAdvisor);
                ctl = dlg.ActiveAdvisorControl;
            }
            else if (controlType == typeof(IDiplomacyControl))
            {
                ctl = new NegotiationDialog();
            }
            else if (controlType == typeof(IDiplomaticTiePicker))
            {
                ctl = new DiplomaticTiePicker();
            }
            else if (controlType == typeof(IForeignAdvisorControl))
            {
                AdvisorDialog dlg = new AdvisorDialog(Advisor.ForeignAdvisor);
                ctl = dlg.ActiveAdvisorControl;
            }
            else if (controlType == typeof(IMilitaryAdvisorControl))
            {
                AdvisorDialog dlg = new AdvisorDialog(Advisor.MilitaryAdvisor);
                ctl = dlg.ActiveAdvisorControl;
            }
            else if (controlType == typeof(IHelpControl))
            {
                ctl = new HelpControl();
                ((HelpControl)ctl).Parent = this.mainForm;
            }
            else if (controlType == typeof(IGameWindow))
            {
                if (this.GameWindow == null)
                    ctl = new GameWindow();
                else
                    ctl = this.GameWindow;
            }
            else if (controlType == typeof(ISplashScreen))
            {
                ctl = new SplashScreen();
            }
			return ctl;
		}

		internal void LoadGameControl(Control control)
		{
			this.mainForm.SwapControl(control);
		}
	}
}
