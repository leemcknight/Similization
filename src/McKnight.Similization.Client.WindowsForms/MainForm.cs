using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using McKnight.Similization.Server;
using McKnight.Similization.Client;
using McKnight.Similization.Core;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// The main game windows form.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components = null;		

		/// <summary>
		/// Initializes a new instance of the <seealso cref="MainForm"/> windows form.
		/// </summary>
		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			//	Build the commands and associate them with the
			//	proper controls.
			//
			ApplyCommandPattern();
			
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

		private void ApplyCommandPattern()
		{
			ClientApplication client = ClientApplication.Instance;
          
			irrigateToolStripMenuItem.Tag = client.Commands["IrrigateCommand"];
            irrigateToolStripMenuItem.Enabled = client.Commands["IrrigateCommand"].Enabled;
            buildRoadToolStripMenuItem.Tag = client.Commands["BuildRoadCommand"];
            buildRoadToolStripMenuItem.Enabled = client.Commands["BuildRoadCommand"].Enabled;
            fortifyToolStripMenuItem.Tag = client.Commands["FortifyCommand"];
			fortifyToolStripMenuItem.Enabled = client.Commands["FortifyCommand"].Enabled;
            mineToolStripMenuItem.Tag = client.Commands["BuildMineCommand"];
			mineToolStripMenuItem.Enabled = client.Commands["BuildMineCommand"].Enabled;
            optionsToolStripMenuItem.Tag = client.Commands["GameOptionsCommand"];
			optionsToolStripMenuItem.Enabled = client.Commands["GameOptionsCommand"].Enabled;
            histographToolStripMenuItem.Tag = client.Commands["DisplayHistographCommand"];
			histographToolStripMenuItem.Enabled = client.Commands["DisplayHistographCommand"].Enabled;
            domesticAdvisorToolStripMenuItem.Tag = client.Commands["DisplayDomesticAdvisorCommand"];
			domesticAdvisorToolStripMenuItem.Enabled = client.Commands["DisplayDomesticAdvisorCommand"].Enabled;
            foreignAdvisorToolStripMenuItem.Tag = client.Commands["DisplayForeignAdvisorCommand"];
			foreignAdvisorToolStripMenuItem.Enabled = client.Commands["DisplayForeignAdvisorCommand"].Enabled;
            militaryAdvisorToolStripMenuItem.Tag = client.Commands["DisplayMilitaryAdvisorCommand"];
			militaryAdvisorToolStripMenuItem.Enabled = client.Commands["DisplayMilitaryAdvisorCommand"].Enabled;
            disbandUnitToolStripMenuItem.Tag = client.Commands["DisbandCommand"];
			disbandUnitToolStripMenuItem.Enabled = client.Commands["DisbandCommand"].Enabled;
            mergeWithCityToolStripMenuItem.Tag = client.Commands["MergeUnitCommand"];
			mergeWithCityToolStripMenuItem.Enabled = client.Commands["MergeUnitCommand"].Enabled;
            bombardToolStripMenuItem.Tag = client.Commands["BombardCommand"];
			bombardToolStripMenuItem.Enabled = client.Commands["BombardCommand"].Enabled;
            startDiplomacyToolStripMenuItem.Tag = client.Commands["StartDiplomacyCommand"];
			startDiplomacyToolStripMenuItem.Enabled = client.Commands["StartDiplomacyCommand"].Enabled;
            investigateCityToolStripMenuItem.Tag = client.Commands["InvestigateCityCommand"];
            investigateCityToolStripMenuItem.Enabled = client.Commands["InvestigateCityCommand"].Enabled;
            helpToolStripMenuItem.Tag = client.Commands["HelpCommand"];
			helpToolStripMenuItem.Enabled = client.Commands["HelpCommand"].Enabled;
            helpToolStripButton.Tag = helpToolStripMenuItem.Tag;
			helpToolStripButton.Enabled = helpToolStripMenuItem.Enabled;
            settleToolStripMenuItem.Tag = client.Commands["BuildNewCityCommand"];
			settleToolStripMenuItem.Enabled = client.Commands["BuildNewCityCommand"].Enabled;
            settleToolStripButton.Tag = client.Commands["BuildNewCityCommand"];
            settleToolStripButton.Enabled = client.Commands["BuildNewCityCommand"].Enabled;       
			saveToolStripButton.Tag = client.Commands["SaveGameCommand"];
			saveToolStripButton.Enabled = client.Commands["SaveGameCommand"].Enabled;
            saveToolStripMenuItem.Tag = client.Commands["SaveGameCommand"];
			saveToolStripMenuItem.Enabled = client.Commands["SaveGameCommand"].Enabled;
			openToolStripButton.Tag = client.Commands["LoadGameCommand"];
			openToolStripButton.Enabled = client.Commands["LoadGameCommand"].Enabled;
            openToolStripMenuItem.Tag = client.Commands["LoadGameCommand"];	
			openToolStripMenuItem.Enabled= client.Commands["LoadGameCommand"].Enabled;
			newToolStripButton.Tag = client.Commands["StartNewGameCommand"];
			newToolStripButton.Enabled = client.Commands["StartNewGameCommand"].Enabled;
            newToolStripMenuItem.Tag = client.Commands["StartNewGameCommand"];
			newToolStripMenuItem.Enabled = client.Commands["StartNewGameCommand"].Enabled;
            sabotageToolStripMenuItem.Tag = client.Commands["SabotageCommand"];
            sabotageToolStripMenuItem.Enabled = client.Commands["SabotageCommand"].Enabled;
            spreadPropagandaToolStripMenuItem.Tag = client.Commands["SpreadPropagandaCommand"];
            spreadPropagandaToolStripMenuItem.Enabled = client.Commands["SpreadPropagandaCommand"].Enabled;
            stealPlansToolStripMenuItem.Tag = client.Commands["StealPlansCommand"];
            stealPlansToolStripMenuItem.Enabled = client.Commands["StealPlansCommand"].Enabled;
            stealTechnologyToolStripMenuItem.Tag = client.Commands["StealTechnologyCommand"];
            stealTechnologyToolStripMenuItem.Enabled = client.Commands["StealTechnologyCommand"].Enabled;
            stealWorldMapToolStripMenuItem.Tag = client.Commands["StealWorldMapCommand"];
            stealWorldMapToolStripMenuItem.Enabled = client.Commands["StealWorldMapCommand"].Enabled;
            plantDiseaseToolStripMenuItem.Tag = client.Commands["PlantDiseaseCommand"];
            plantDiseaseToolStripMenuItem.Enabled = client.Commands["PlantDiseaseCommand"].Enabled;
            plantSpyToolStripMenuItem.Tag = client.Commands["PlantSpyCommand"];
            plantSpyToolStripMenuItem.Enabled = client.Commands["PlantSpyCommand"].Enabled;
            exposeSpyToolStripMenuItem.Tag = client.Commands["ExposeSpyCommand"];
            exposeSpyToolStripMenuItem.Enabled = client.Commands["ExposeSpyCommand"].Enabled;
            moveToToolStripMenuItem.Tag = client.Commands["MoveToCommand"];	
			moveToToolStripMenuItem.Enabled = client.Commands["MoveToCommand"].Enabled;
            establishEmbassyToolStripMenuItem.Tag = client.Commands["EstablishEmbassyCommand"];
            establishEmbassyToolStripMenuItem.Enabled = client.Commands["EstablishEmbassyCommand"].Enabled;
            aboutToolStripMenuItem.Tag = client.Commands["AboutCommand"];
            aboutToolStripMenuItem.Enabled = client.Commands["AboutCommand"].Enabled;

			client.Commands["SaveGameCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
			client.Commands["IrrigateCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
			client.Commands["BuildRoadCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
			client.Commands["FortifyCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
			client.Commands["BuildMineCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
			client.Commands["DisbandCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
            client.Commands["StartDiplomacyCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
			client.Commands["MergeUnitCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
			client.Commands["BuildNewCityCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
			client.Commands["MoveToCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
			client.Commands["BombardCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
			client.Commands["DisplayMilitaryAdvisorCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
			client.Commands["DisplayDomesticAdvisorCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
			client.Commands["DisplayForeignAdvisorCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
            client.Commands["EstablishEmbassyCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
            client.Commands["InvestigateCityCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
            client.Commands["SabotageCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
            client.Commands["SpreadPropagandaCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
            client.Commands["StealPlansCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
            client.Commands["StealTechnologyCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
            client.Commands["StealWorldMapCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
            client.Commands["PlantDiseaseCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
            client.Commands["PlantSpyCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
            client.Commands["ExposeSpyCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
            client.Commands["AboutCommand"].EnabledChanged += new EventHandler(CommandEnabledChanged);
		}

		private void CommandEnabledChanged(object sender, System.EventArgs e)
		{
			Command cmd = (Command)sender;
			bool enabled = cmd.Enabled;
			string name = cmd.Name;

			switch(name)
			{
                    
				case "IrrigateCommand":
					this.irrigateToolStripMenuItem.Enabled = enabled;
					break;
				case "BuildRoadCommand":
					this.buildRoadToolStripMenuItem.Enabled = enabled;
					break;
				case "FortifyCommand":
					this.fortifyToolStripMenuItem.Enabled = enabled;
					break;
				case "BuildMineCommand":
					this.mineToolStripMenuItem.Enabled = enabled;
					break;
				case "DisbandCommand":
					this.disbandUnitToolStripMenuItem.Enabled = enabled;
					break;
				case "MergeUnitCommand":
					this.mergeWithCityToolStripMenuItem.Enabled = enabled;
					break;
				case "BuildNewCityCommand":
					this.settleToolStripMenuItem.Enabled = enabled;
					this.settleToolStripButton.Enabled = enabled;
					break;
				case "MoveToCommand":
					this.moveToToolStripMenuItem.Enabled = enabled;					
					break;
                case "StartDiplomacyCommand":
                    this.startDiplomacyToolStripMenuItem.Enabled = enabled;
                    break;
                case "InvestigateCityCommand":
                    this.investigateCityToolStripMenuItem.Enabled = enabled;
                    break;
                case "SabotageCommand":
                    this.sabotageToolStripMenuItem.Enabled = enabled;
                    break;                
                case "SpreadPropagandaCommand":
                    this.spreadPropagandaToolStripMenuItem.Enabled = enabled;
                    break;
                case "StealPlansCommand":
                    this.stealPlansToolStripMenuItem.Enabled = enabled;
                    break;
                case "StealTechnologyCommand":
                    this.stealTechnologyToolStripMenuItem.Enabled = enabled;
                    break;
                case "StealWorldMapCommand":
                    this.stealWorldMapToolStripMenuItem.Enabled = enabled;
                    break;
                case "PlantDiseaseCommand":
                    this.plantDiseaseToolStripMenuItem.Enabled = enabled;
                    break;
                case "PlantSpyCommand":
                    this.plantSpyToolStripMenuItem.Enabled = enabled;
                    break;
                case "ExposeSpyCommand":
                    this.exposeSpyToolStripMenuItem.Enabled = enabled;
                    break;
				case "BombardCommand":
					this.bombardToolStripMenuItem.Enabled = enabled;
					break;
				case "SaveGameCommand":
					this.saveToolStripButton.Enabled = enabled;
					this.saveToolStripMenuItem.Enabled = enabled;
					break;
				case "DisplayMilitaryAdvisorCommand":
					this.militaryAdvisorToolStripMenuItem.Enabled = enabled;
					break;
				case "DisplayForeignAdvisorCommand":
					this.foreignAdvisorToolStripMenuItem.Enabled = enabled;
					break;
				case "DisplayDomesticAdvisorCommand":
					this.domesticAdvisorToolStripMenuItem.Enabled = enabled;
					break;
                case "AboutCommand":
                    this.aboutToolStripMenuItem.Enabled = enabled;
                    break;
                     
			}
		}

		#region Event Handlers
		private void HandleMenuItemClick(object sender, System.EventArgs e)
		{
            Command cmd = (Command)((ToolStripMenuItem)sender).Tag;
            cmd.Invoke();
		}

		private void _toolbarMenuItem_Click(object sender, System.EventArgs e)
		{
            ToolStripButton btn = (ToolStripButton)sender;
            Command cmd = (Command)btn.Tag;
            cmd.Invoke();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		#endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem contentsToolStripMenuItem;
        private ToolStripMenuItem indexToolStripMenuItem;
        private ToolStripMenuItem searchToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripMenuItem histographToolStripMenuItem;
        private ToolStripButton newToolStripButton;
        private ToolStripButton openToolStripButton;
        private ToolStripButton saveToolStripButton;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripButton helpToolStripButton;
        private ToolStripMenuItem actionsToolStripMenuItem;
        private ToolStripMenuItem automateWorkerToolStripMenuItem;
        private ToolStripMenuItem moveToToolStripMenuItem;
        private ToolStripMenuItem settleToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem irrigateToolStripMenuItem;
        private ToolStripMenuItem buildRoadToolStripMenuItem;
        private ToolStripMenuItem mineToolStripMenuItem;
        private ToolStripMenuItem fortifyToolStripMenuItem;
        private ToolStripMenuItem cleanPollutionToolStripMenuItem;
        private ToolStripMenuItem clearJungleToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem airliftToolStripMenuItem;
        private ToolStripMenuItem bombardToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem disbandUnitToolStripMenuItem;
        private ToolStripMenuItem mergeWithCityToolStripMenuItem;
        private ToolStripMenuItem diplomacyToolStripMenuItem;
        private ToolStripMenuItem startDiplomacyToolStripMenuItem;
        private ToolStripMenuItem establishEmbassyToolStripMenuItem;
        private ToolStripMenuItem investigateCityToolStripMenuItem;
        private ToolStripMenuItem plantSpyToolStripMenuItem;
        private ToolStripMenuItem advisorsToolStripMenuItem;
        private ToolStripMenuItem domesticAdvisorToolStripMenuItem;
        private ToolStripMenuItem tradeAdvisorToolStripMenuItem;
        private ToolStripMenuItem foreignAdvisorToolStripMenuItem;
        private ToolStripMenuItem militaryAdvisorToolStripMenuItem;
        private ToolStripMenuItem scienceAdvisorToolStripMenuItem;
        private ToolStripMenuItem culturalAdvisorToolStripMenuItem;
        private ToolStripMenuItem espionageToolStripMenuItem;
        private ToolStripMenuItem sabotageToolStripMenuItem;
        private ToolStripMenuItem spreadPropagandaToolStripMenuItem;
        private ToolStripMenuItem plantDiseaseToolStripMenuItem;
        private ToolStripMenuItem stealPlansToolStripMenuItem;
        private ToolStripMenuItem stealWorldMapToolStripMenuItem;
        private ToolStripMenuItem exposeSpyToolStripMenuItem;
        private ToolStripButton settleToolStripButton;
        private ToolStrip toolStrip1;
        private WindowsClientConsole console;
        private ToolStripMenuItem stealTechnologyToolStripMenuItem;

		#region Properties
		/// <summary>
		/// Gets the console.
		/// </summary>
		public IConsole Console
		{
			get { return this.console; }
		}

		/// <summary>
		/// Gets the Status View.
		/// </summary>
		public ISimilizationStatusView StatusView
		{
			get { return this.console.StatusView; }
		}

		#endregion

		private Control activeControl;

		internal void SwapControl(Control control)
		{
			if(this.activeControl != null)
			{
				this.activeControl.Hide();
			}

			if(!splitContainer1.Panel1.Controls.Contains(control))
			{
				splitContainer1.Panel1.Controls.Add(control);
				control.Dock = DockStyle.Fill;
			}

			this.activeControl = control;	
		}

        private void ToolStripButtonClicked(object sender, EventArgs e)
        {
            ((Command)((ToolStripButton)sender).Tag).Invoke();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histographToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.automateWorkerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.irrigateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildRoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fortifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanPollutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearJungleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.airliftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bombardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.disbandUnitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeWithCityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diplomacyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startDiplomacyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.establishEmbassyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.investigateCityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plantSpyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.espionageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sabotageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spreadPropagandaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plantDiseaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stealPlansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stealTechnologyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stealWorldMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exposeSpyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advisorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.domesticAdvisorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tradeAdvisorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.foreignAdvisorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.militaryAdvisorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scienceAdvisorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.culturalAdvisorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.settleToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.console = new McKnight.Similization.Client.WindowsForms.WindowsClientConsole();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.diplomacyToolStripMenuItem,
            this.espionageToolStripMenuItem,
            this.advisorsToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // newToolStripMenuItem
            // 
            resources.ApplyResources(this.newToolStripMenuItem, "newToolStripMenuItem");
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // openToolStripMenuItem
            // 
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // saveToolStripMenuItem
            // 
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.histographToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            // 
            // histographToolStripMenuItem
            // 
            this.histographToolStripMenuItem.Name = "histographToolStripMenuItem";
            resources.ApplyResources(this.histographToolStripMenuItem, "histographToolStripMenuItem");
            this.histographToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.automateWorkerToolStripMenuItem,
            this.moveToToolStripMenuItem,
            this.settleToolStripMenuItem,
            this.toolStripSeparator1,
            this.irrigateToolStripMenuItem,
            this.buildRoadToolStripMenuItem,
            this.mineToolStripMenuItem,
            this.fortifyToolStripMenuItem,
            this.cleanPollutionToolStripMenuItem,
            this.clearJungleToolStripMenuItem,
            this.toolStripSeparator3,
            this.airliftToolStripMenuItem,
            this.bombardToolStripMenuItem,
            this.toolStripSeparator4,
            this.disbandUnitToolStripMenuItem,
            this.mergeWithCityToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            resources.ApplyResources(this.actionsToolStripMenuItem, "actionsToolStripMenuItem");
            // 
            // automateWorkerToolStripMenuItem
            // 
            this.automateWorkerToolStripMenuItem.Name = "automateWorkerToolStripMenuItem";
            resources.ApplyResources(this.automateWorkerToolStripMenuItem, "automateWorkerToolStripMenuItem");
            this.automateWorkerToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // moveToToolStripMenuItem
            // 
            this.moveToToolStripMenuItem.Name = "moveToToolStripMenuItem";
            resources.ApplyResources(this.moveToToolStripMenuItem, "moveToToolStripMenuItem");
            this.moveToToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // settleToolStripMenuItem
            // 
            this.settleToolStripMenuItem.Image = McKnight.Similization.Client.WindowsForms.Properties.Resources.wmploc_dll_14;
            this.settleToolStripMenuItem.Name = "settleToolStripMenuItem";
            resources.ApplyResources(this.settleToolStripMenuItem, "settleToolStripMenuItem");
            this.settleToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // irrigateToolStripMenuItem
            // 
            this.irrigateToolStripMenuItem.Name = "irrigateToolStripMenuItem";
            resources.ApplyResources(this.irrigateToolStripMenuItem, "irrigateToolStripMenuItem");
            this.irrigateToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // buildRoadToolStripMenuItem
            // 
            this.buildRoadToolStripMenuItem.Name = "buildRoadToolStripMenuItem";
            resources.ApplyResources(this.buildRoadToolStripMenuItem, "buildRoadToolStripMenuItem");
            this.buildRoadToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // mineToolStripMenuItem
            // 
            this.mineToolStripMenuItem.Name = "mineToolStripMenuItem";
            resources.ApplyResources(this.mineToolStripMenuItem, "mineToolStripMenuItem");
            this.mineToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // fortifyToolStripMenuItem
            // 
            this.fortifyToolStripMenuItem.Name = "fortifyToolStripMenuItem";
            resources.ApplyResources(this.fortifyToolStripMenuItem, "fortifyToolStripMenuItem");
            this.fortifyToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // cleanPollutionToolStripMenuItem
            // 
            this.cleanPollutionToolStripMenuItem.Name = "cleanPollutionToolStripMenuItem";
            resources.ApplyResources(this.cleanPollutionToolStripMenuItem, "cleanPollutionToolStripMenuItem");
            this.cleanPollutionToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // clearJungleToolStripMenuItem
            // 
            this.clearJungleToolStripMenuItem.Name = "clearJungleToolStripMenuItem";
            resources.ApplyResources(this.clearJungleToolStripMenuItem, "clearJungleToolStripMenuItem");
            this.clearJungleToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // airliftToolStripMenuItem
            // 
            this.airliftToolStripMenuItem.Name = "airliftToolStripMenuItem";
            resources.ApplyResources(this.airliftToolStripMenuItem, "airliftToolStripMenuItem");
            this.airliftToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // bombardToolStripMenuItem
            // 
            this.bombardToolStripMenuItem.Name = "bombardToolStripMenuItem";
            resources.ApplyResources(this.bombardToolStripMenuItem, "bombardToolStripMenuItem");
            this.bombardToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // disbandUnitToolStripMenuItem
            // 
            this.disbandUnitToolStripMenuItem.Image = McKnight.Similization.Client.WindowsForms.Properties.Resources.del;
            this.disbandUnitToolStripMenuItem.Name = "disbandUnitToolStripMenuItem";
            resources.ApplyResources(this.disbandUnitToolStripMenuItem, "disbandUnitToolStripMenuItem");
            this.disbandUnitToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // mergeWithCityToolStripMenuItem
            // 
            this.mergeWithCityToolStripMenuItem.Name = "mergeWithCityToolStripMenuItem";
            resources.ApplyResources(this.mergeWithCityToolStripMenuItem, "mergeWithCityToolStripMenuItem");
            this.mergeWithCityToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // diplomacyToolStripMenuItem
            // 
            this.diplomacyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startDiplomacyToolStripMenuItem,
            this.establishEmbassyToolStripMenuItem,
            this.investigateCityToolStripMenuItem,
            this.plantSpyToolStripMenuItem});
            this.diplomacyToolStripMenuItem.Name = "diplomacyToolStripMenuItem";
            resources.ApplyResources(this.diplomacyToolStripMenuItem, "diplomacyToolStripMenuItem");
            // 
            // startDiplomacyToolStripMenuItem
            // 
            this.startDiplomacyToolStripMenuItem.Name = "startDiplomacyToolStripMenuItem";
            resources.ApplyResources(this.startDiplomacyToolStripMenuItem, "startDiplomacyToolStripMenuItem");
            this.startDiplomacyToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // establishEmbassyToolStripMenuItem
            // 
            this.establishEmbassyToolStripMenuItem.Name = "establishEmbassyToolStripMenuItem";
            resources.ApplyResources(this.establishEmbassyToolStripMenuItem, "establishEmbassyToolStripMenuItem");
            this.establishEmbassyToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // investigateCityToolStripMenuItem
            // 
            this.investigateCityToolStripMenuItem.Name = "investigateCityToolStripMenuItem";
            resources.ApplyResources(this.investigateCityToolStripMenuItem, "investigateCityToolStripMenuItem");
            this.investigateCityToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // plantSpyToolStripMenuItem
            // 
            this.plantSpyToolStripMenuItem.Name = "plantSpyToolStripMenuItem";
            resources.ApplyResources(this.plantSpyToolStripMenuItem, "plantSpyToolStripMenuItem");
            this.plantSpyToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // espionageToolStripMenuItem
            // 
            this.espionageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sabotageToolStripMenuItem,
            this.spreadPropagandaToolStripMenuItem,
            this.plantDiseaseToolStripMenuItem,
            this.stealPlansToolStripMenuItem,
            this.stealTechnologyToolStripMenuItem,
            this.stealWorldMapToolStripMenuItem,
            this.exposeSpyToolStripMenuItem});
            this.espionageToolStripMenuItem.Name = "espionageToolStripMenuItem";
            resources.ApplyResources(this.espionageToolStripMenuItem, "espionageToolStripMenuItem");
            // 
            // sabotageToolStripMenuItem
            // 
            this.sabotageToolStripMenuItem.Name = "sabotageToolStripMenuItem";
            resources.ApplyResources(this.sabotageToolStripMenuItem, "sabotageToolStripMenuItem");
            this.sabotageToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // spreadPropagandaToolStripMenuItem
            // 
            this.spreadPropagandaToolStripMenuItem.Name = "spreadPropagandaToolStripMenuItem";
            resources.ApplyResources(this.spreadPropagandaToolStripMenuItem, "spreadPropagandaToolStripMenuItem");
            this.spreadPropagandaToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // plantDiseaseToolStripMenuItem
            // 
            this.plantDiseaseToolStripMenuItem.Name = "plantDiseaseToolStripMenuItem";
            resources.ApplyResources(this.plantDiseaseToolStripMenuItem, "plantDiseaseToolStripMenuItem");
            this.plantDiseaseToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // stealPlansToolStripMenuItem
            // 
            this.stealPlansToolStripMenuItem.Name = "stealPlansToolStripMenuItem";
            resources.ApplyResources(this.stealPlansToolStripMenuItem, "stealPlansToolStripMenuItem");
            this.stealPlansToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // stealTechnologyToolStripMenuItem
            // 
            this.stealTechnologyToolStripMenuItem.Name = "stealTechnologyToolStripMenuItem";
            resources.ApplyResources(this.stealTechnologyToolStripMenuItem, "stealTechnologyToolStripMenuItem");
            // 
            // stealWorldMapToolStripMenuItem
            // 
            this.stealWorldMapToolStripMenuItem.Name = "stealWorldMapToolStripMenuItem";
            resources.ApplyResources(this.stealWorldMapToolStripMenuItem, "stealWorldMapToolStripMenuItem");
            this.stealWorldMapToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // exposeSpyToolStripMenuItem
            // 
            this.exposeSpyToolStripMenuItem.Name = "exposeSpyToolStripMenuItem";
            resources.ApplyResources(this.exposeSpyToolStripMenuItem, "exposeSpyToolStripMenuItem");
            this.exposeSpyToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // advisorsToolStripMenuItem
            // 
            this.advisorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.domesticAdvisorToolStripMenuItem,
            this.tradeAdvisorToolStripMenuItem,
            this.foreignAdvisorToolStripMenuItem,
            this.militaryAdvisorToolStripMenuItem,
            this.scienceAdvisorToolStripMenuItem,
            this.culturalAdvisorToolStripMenuItem});
            this.advisorsToolStripMenuItem.Name = "advisorsToolStripMenuItem";
            resources.ApplyResources(this.advisorsToolStripMenuItem, "advisorsToolStripMenuItem");
            // 
            // domesticAdvisorToolStripMenuItem
            // 
            this.domesticAdvisorToolStripMenuItem.Name = "domesticAdvisorToolStripMenuItem";
            resources.ApplyResources(this.domesticAdvisorToolStripMenuItem, "domesticAdvisorToolStripMenuItem");
            this.domesticAdvisorToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // tradeAdvisorToolStripMenuItem
            // 
            this.tradeAdvisorToolStripMenuItem.Name = "tradeAdvisorToolStripMenuItem";
            resources.ApplyResources(this.tradeAdvisorToolStripMenuItem, "tradeAdvisorToolStripMenuItem");
            this.tradeAdvisorToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // foreignAdvisorToolStripMenuItem
            // 
            this.foreignAdvisorToolStripMenuItem.Name = "foreignAdvisorToolStripMenuItem";
            resources.ApplyResources(this.foreignAdvisorToolStripMenuItem, "foreignAdvisorToolStripMenuItem");
            this.foreignAdvisorToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // militaryAdvisorToolStripMenuItem
            // 
            this.militaryAdvisorToolStripMenuItem.Name = "militaryAdvisorToolStripMenuItem";
            resources.ApplyResources(this.militaryAdvisorToolStripMenuItem, "militaryAdvisorToolStripMenuItem");
            this.militaryAdvisorToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // scienceAdvisorToolStripMenuItem
            // 
            this.scienceAdvisorToolStripMenuItem.Name = "scienceAdvisorToolStripMenuItem";
            resources.ApplyResources(this.scienceAdvisorToolStripMenuItem, "scienceAdvisorToolStripMenuItem");
            this.scienceAdvisorToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // culturalAdvisorToolStripMenuItem
            // 
            this.culturalAdvisorToolStripMenuItem.Name = "culturalAdvisorToolStripMenuItem";
            resources.ApplyResources(this.culturalAdvisorToolStripMenuItem, "culturalAdvisorToolStripMenuItem");
            this.culturalAdvisorToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            resources.ApplyResources(this.contentsToolStripMenuItem, "contentsToolStripMenuItem");
            this.contentsToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            resources.ApplyResources(this.indexToolStripMenuItem, "indexToolStripMenuItem");
            this.indexToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            resources.ApplyResources(this.searchToolStripMenuItem, "searchToolStripMenuItem");
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.HandleMenuItemClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator6,
            this.settleToolStripButton,
            this.toolStripSeparator7,
            this.helpToolStripButton});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.newToolStripButton, "newToolStripButton");
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Click += new System.EventHandler(this._toolbarMenuItem_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.openToolStripButton, "openToolStripButton");
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Click += new System.EventHandler(this._toolbarMenuItem_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.saveToolStripButton, "saveToolStripButton");
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Click += new System.EventHandler(this._toolbarMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Click += new System.EventHandler(this._toolbarMenuItem_Click);
            // 
            // settleToolStripButton
            // 
            this.settleToolStripButton.Image = McKnight.Similization.Client.WindowsForms.Properties.Resources.wmploc_dll_14;
            resources.ApplyResources(this.settleToolStripButton, "settleToolStripButton");
            this.settleToolStripButton.Name = "settleToolStripButton";
            this.settleToolStripButton.Click += new System.EventHandler(this._toolbarMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Click += new System.EventHandler(this._toolbarMenuItem_Click);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.helpToolStripButton, "helpToolStripButton");
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Click += new System.EventHandler(this._toolbarMenuItem_Click);
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.console);
            // 
            // console
            // 
            resources.ApplyResources(this.console, "console");
            this.console.ForeColor = System.Drawing.Color.White;
            this.console.Name = "console";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private SplitContainer splitContainer1;
	}
}
