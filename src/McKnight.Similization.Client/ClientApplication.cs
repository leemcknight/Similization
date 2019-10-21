using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{
#if DEBUG
	/// <summary>
	/// Contains classes and interfaces that describe and control the basic user interface 
	/// functionality of the game.
	/// </summary>
	public class NamespaceDoc {}
#endif

	/// <summary>
	/// Abstract base class for a Similization client application.  This class
	/// cannot be instantiated directly.  This class uses the Singleton pattern, 
	/// assuring that only one instance can ever be created.
	/// </summary>
	public abstract class ClientApplication : IDisposable
	{
        private bool disposed;
        private ISplashScreen splashScreen;		
		private ISimilizationStatusView statusView;		
		private IMiniMap miniMap;
        private IGameWindow gameWindow;
		private NamedObjectCollection<Command> commands;		
		private Tileset tileset;		
		private GameRoot root;		
		private Country player;		
		private Options options;
        private IConsole messageConsole;
	
		/// <summary>
		/// Initializes a new instance of the <see cref="ClientApplication"/> class.
		/// </summary>
		protected ClientApplication()
		{

		}

        /// <summary>
        /// Finalizer for the <see cref="ClientApplication"/> class.
        /// </summary>
        ~ClientApplication()
        {
            Dispose(false);
        }

        /// <summary>
        /// Initializes the <see cref="ClientApplication"/> class instance.
        /// </summary>
        protected void InitializeClient()
        {
            this.splashScreen = (ISplashScreen)GetControl(typeof(ISplashScreen));
            this.splashScreen.ShowSimilizationControl();
            this.root = GameRoot.Instance;
            this.options = new Options();
            RegisterGameRoot();
            InitializeCommands();
        }

		/// <summary>
		/// Represents the instance of the client application.
		/// </summary>
		protected static ClientApplication client;

		/// <summary>
		/// Gets an instance of the <c>ClientApplication</c>.
		/// </summary>
		public static ClientApplication Instance
		{
			get{ return client; }
		}
		
		/// <summary>
		/// Gets the commands used in the game.
		/// </summary>
		public NamedObjectCollection<Command> Commands
		{
			get { return this.commands; }
		}
				
		/// <summary>
		/// Gets or sets the console to write messages to.
		/// </summary>
		public  IConsole Console
		{
			get { return this.messageConsole; }
		}

        /// <summary>
        /// Sets the <see cref="IConsole"/> to be used in the game.
        /// </summary>
        /// <param name="console"></param>
        protected void InitializeConsole(IConsole console)
        {
            this.messageConsole = console;
        }

		/// <summary>
		/// Gets or sets the status view to show to the user.
		/// </summary>
		protected ISimilizationStatusView StatusView
		{
			get { return this.statusView; }
			set { this.statusView = value; }
		}

		/// <summary>
		/// Gets or sets the mini map to show to the user.
		/// </summary>
		public IMiniMap MiniMap
		{
			get { return this.miniMap; }
			set { this.miniMap = value; }
		}
		
		/// <summary>
		/// Gets the main game window for the client.
		/// </summary>
		public IGameWindow GameWindow
		{
			get { return this.gameWindow; }
		}

		/// <summary>
		/// The tileset that is used for the graphics in the game.
		/// </summary>
		public Tileset Tileset
		{
			get { return this.tileset; }
		}

		internal void LoadTileset(string path)
		{
			this.tileset = new Tileset(path);
            LoadTileSetImageData();
		}

        /// <summary>
        /// Loads the images for the <see cref="Tileset"/> from disk into the client-specific 
        /// image type.
        /// </summary>
        protected virtual void LoadTileSetImageData()
        {
        }

		/// <summary>
		/// Starts the client.
		/// </summary>
		public virtual void Start()
		{
			IStartingScreen dialog = GetControl(typeof(IStartingScreen)) as IStartingScreen;
			dialog.ShowSimilizationControl();
			splashScreen.CloseSplashScreen();			
			this.miniMap.MiniMapClicked += new EventHandler<MiniMapClickedEventArgs>(HandleMiniMapClicked);
		}
		
		private void HandleWarDeclared(object sender, WarDeclaredEventArgs e)
		{
			string message;
			if(e.Agressor == this.player)
				return;
			else if(e.Victim == this.player)
			{
				message = string.Format(CultureInfo.CurrentCulture, ClientResources.GetString("warDeclaredOnUs"), e.Agressor.Name);
			}
			else
			{
				message = string.Format(CultureInfo.CurrentCulture, ClientResources.GetString("warDeclared_foreign"), e.Agressor.Name, e.Victim.Name);
			}
			this.gameWindow.ShowMessageBox(message, ClientResources.GetString(StringKey.GameTitle));
		}

		private void HandleDiplomaticTiesCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if(sender == player.DiplomaticTies)
			{
				this.commands["StartDiplomacyCommand"].Enabled = (player.DiplomaticTies.Count > 0);
			}
		}
		
		/// <summary>
		/// Gets an instance of the <see cref="McKnight.Similization.Server.GameRoot"/> on the server.
		/// </summary>
		public GameRoot ServerInstance 
		{
			get { return this.root; }
		}

		/// <summary>
		/// Gets the <see cref="McKnight.Similization.Server.Country"/> representing the player.
		/// </summary>
		public Country Player 
		{
			get { return this.player; }
			set { this.player = value; }
		}
		
		/// <summary>
		/// Gets the game options.
		/// </summary>
		public Options Options 
		{
			get 
			{ 
				return this.options; 
			}
		}

		private void RegisterPlayer(Country player)
		{
			player.AcquiredTechnologies.CollectionChanged += new CollectionChangeEventHandler(HandleTechnologyCollectionChanged);
			player.TradeProposed += new EventHandler<TradeProposedEventArgs>(HandleTradeProposed);
			player.Defeated += new EventHandler<DefeatedEventArgs>(HandleDefeated);
			player.ResearchDirectionNeeded += new EventHandler(HandleResearchDirectionNeeded);
			player.ResearchedTechnologyChanged += new EventHandler(HandleResearchedTechnologyChanged);
			player.Cities.CollectionChanged += new CollectionChangeEventHandler(HandleCityCollectionChanged);
			player.Units.CollectionChanged += new CollectionChangeEventHandler(HandleUnitCollectionChanged);
			player.TurnFinished += new EventHandler(HandleCountryTurnFinished);
			player.NotifyEndOfTurnChanged += new EventHandler(HandleNotifyEndOfTurnChanged);
			player.EraChanged += new EventHandler(HandleEraChanged);
			player.NewGovernmentAvailable += new EventHandler<GovernmentAvailableEventArgs>(HandleNewGovernmentAvailable);
			player.RevolutionStarted +=	new EventHandler(HandleRevolutionStarted);
			player.RevolutionEnded += new EventHandler<RevolutionEndedEventArgs>(HandleRevolutionEnded);
			player.AudienceRequested += new EventHandler<AudienceRequestedEventArgs>(HandleAudienceRequested);
			player.WarDeclared += new EventHandler<WarDeclaredEventArgs>(HandleWarDeclared);
			//player.DiplomaticTies.CollectionChanged += new CollectionChangeEventHandler(HandleDiplomaticTiesCollectionChanged);
			player.MutualProtectionPactInvoked += new EventHandler<MutualProtectionPactEventArgs>(HandleMutualProtectionPactInvoked);
            player.SpyCaptured += new EventHandler<SpyCapturedEventArgs>(HandleSpyCaptured);
		}

        private void HandleSpyCaptured(object sender, SpyCapturedEventArgs e)
        {
            if (sender == this.player)
            {
                string message = string.Empty;
                Country enemy = e.DiplomaticTie.ForeignCountry;
                switch (e.EspionageAction)
                {
                    case EspionageAction.ExposeSpy:
                        if (e.EspionageCompleted)
                        {
                            message = ClientResources.GetString("spyCaught_exposeSpySuccess");
                            message = string.Format(CultureInfo.CurrentCulture, message, e.City.Name, enemy.Government.LeaderTitle, enemy.LeaderName, enemy.Civilization.Noun);
                        }
                        else
                        {
                            message = ClientResources.GetString("spyCaught_exposeSpyFailure");
                            message = string.Format(CultureInfo.CurrentCulture, message, enemy.Government.LeaderTitle, enemy.LeaderName, enemy.Civilization.Noun);
                        }
                        break;
                    case EspionageAction.PlantDisease:
                        break;
                    case EspionageAction.PlantSpy:
                        message = ClientResources.GetString("spyCaught_plantingSpyFailure");
                        message = string.Format(CultureInfo.CurrentCulture, message, enemy.Government.LeaderTitle, enemy.LeaderName, enemy.Civilization.Noun);
                        break;
                    case EspionageAction.SabotageProduction:
                        if (e.EspionageCompleted)
                        {
                            message = ClientResources.GetString("spyCaught_sabotageProductionSuccess");
                            message = string.Format(CultureInfo.CurrentCulture, message, e.City.Name, enemy.Government.LeaderTitle, enemy.LeaderName, enemy.Civilization.Noun);
                        }
                        else
                        {
                            message = ClientResources.GetString("spyCaught_sabotageProductionFailure");
                            message = string.Format(CultureInfo.CurrentCulture, message, enemy.Government.LeaderTitle, enemy.LeaderName, enemy.Civilization.Noun, e.City.Name);
                        }
                        break;
                    case EspionageAction.SpreadPropaganda:
                        break;
                    case EspionageAction.StealPlans:
                        if (e.EspionageCompleted)
                        {
                            message = ClientResources.GetString("spyCaught_stealPlansSuccess");
                            message = string.Format(CultureInfo.CurrentCulture, message, enemy.Government.LeaderTitle, enemy.LeaderName, enemy.Civilization.Noun);
                        }
                        else
                        {
                            message = ClientResources.GetString("spyCaught_stealPlansFailure");
                            message = string.Format(CultureInfo.CurrentCulture, message, enemy.Government.LeaderTitle, enemy.LeaderName, enemy.Civilization.Noun);
                        }
                        break;
                    case EspionageAction.StealTechnology:
                        message = ClientResources.GetString("spyCaught_stealTechnologyFailure");
                        message = string.Format(CultureInfo.CurrentCulture, message, enemy.Government.LeaderTitle, enemy.LeaderName, enemy.Civilization.Noun);
                        break;
                    case EspionageAction.StealWorldMap:
                        message = ClientResources.GetString("spyCaught_stealWorldMapFailure");
                        message = string.Format(CultureInfo.CurrentCulture, message, enemy.Government.LeaderTitle, enemy.LeaderName, enemy.Civilization.Noun);
                        break;
                }

                this.gameWindow.ShowMessageBox(message, ClientResources.GetString(StringKey.GameTitle));
            }

        }

		private void RegisterCity(City newCity)
		{
			newCity.ImprovementBuilt += new EventHandler<ImprovementBuiltEventArgs>(HandleImprovementBuilt);
			newCity.ImprovementDestroyed +=	new EventHandler<ImprovementDestroyedEventArgs>(HandleImprovementDestroyed);
			newCity.CitizensKilled += new EventHandler<CitizensKilledEventArgs>(HandleCitizensKilled);
			newCity.Starved += new EventHandler(HandleCityStarvation);
			newCity.CityStatusChanged += new EventHandler<CityStatusEventArgs>(HandleCityStatusChanged);
			newCity.AvailableResources.CollectionChanged += new CollectionChangeEventHandler(HandleResourceCollectionChanged);
			newCity.Captured += new EventHandler<CapturedEventArgs>(HandleCityCaptured);
			newCity.GrowthNeededForUnit += new EventHandler(HandleGrowthNeededForUnit);
			this.tileset.CityTiles.Refresh(newCity);

			if(newCity.ParentCountry == player)
			{
				string message = string.Format(CultureInfo.CurrentCulture, ClientResources.GetString("city_founded"), newCity.Name, GetYearString());
				this.gameWindow.ShowMessageBox(message, ClientResources.GetString(StringKey.GameTitle));
			}
		}

		private void RegisterUnit(Unit unit)
		{
			unit.CombatStarted +=new EventHandler<CombatEventArgs>(HandleCombatStart);
			unit.HitPointLost += new EventHandler(HandleHitPointLost);
			unit.Killed += new EventHandler(HandleUnitKilled);
			unit.Captured += new EventHandler<UnitCapturedEventArgs>(HandleUnitCaptured);
			unit.Moved += new EventHandler(HandleMoved);
			if(unit.ParentCountry == this.player)
			{
				unit.VillageEncountered	+= new EventHandler<VillageEventArgs>(HandleVillageEncounter);
			}
		}

		//registers event handlers with all of the server events.
		private void RegisterGameRoot()
		{
			GameRoot root = GameRoot.Instance;
			root.Countries.CollectionChanged += new CollectionChangeEventHandler(HandleCountryCollectionChanged);
			root.ActiveUnitChanged += new EventHandler(HandleActiveUnitChanged);
			root.StatusChanged += new EventHandler<StatusChangedEventArgs>(HandleStatusChanged);
			root.TurnFinished += new EventHandler(HandleTurnFinished);
			root.GameStarted += new EventHandler(HandleGameStart);
			root.GameEnded += new EventHandler<GameEndedEventArgs>(HandleGameEnded);
			root.ServerStarting += new EventHandler(HandleServerStarting);
			root.ServerStarted += new EventHandler(HandleServerStarted);
			root.LoadedGameStarted += new EventHandler(HandleLoadedGameStarted);
		}

		private void HandleServerStarting(object sender, System.EventArgs e)
		{
			this.messageConsole.WriteLine(ClientResources.GetString("server_Starting"));
		}

		private void HandleServerStarted(object sender, System.EventArgs e)
		{
			this.messageConsole.WriteLine(ClientResources.GetString("server_Started"));
			this.gameWindow = (IGameWindow)GetControl(typeof(IGameWindow));
			this.gameWindow.CityNameFont = this.options.CityNameFont;
			this.gameWindow.CityNameFontColor = this.options.CityNameFontColor;
		}

		private void HandleMiniMapClicked(object sender, MiniMapClickedEventArgs e)
		{
			this.gameWindow.CenterCoordinates = e.CenterCell;
		}

		private void HandleServerMessage(object sender, StatusChangedEventArgs e)
		{
			this.messageConsole.WriteLine(e.Message);
		}

		private void HandleCountryCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if(e.Action == CollectionChangeAction.Add)
			{
				Country country = (Country)e.Element;
				string message = string.Format(
					CultureInfo.CurrentCulture,
					ClientResources.GetString(StringKey.PlayerRegistered),
					country.LeaderName,
					country.Civilization.Noun);

				this.messageConsole.WriteLine(message);
				RegisterPlayer(country);
			}
		}

		private void HandleCountryTurnFinished(object sender, System.EventArgs e)
		{
			Country ctry = sender as Country;

			if(ctry == this.player)
			{
				string message = string.Empty;
				if(ctry.NotifyEndOfTurn)
				{
					message = ClientResources.GetString(StringKey.TurnFinished);
				}
				else
				{
					message = ClientResources.GetString(StringKey.Ready);
				}
				this.messageConsole.WriteLine(message);
			}
		}

		private void HandleMutualProtectionPactInvoked(object sender, MutualProtectionPactEventArgs e)
		{
			string message = string.Empty;
			if(e.CommonEnemy == this.player)
			{
				//someone declared war on us in response to our war with another
				//player.
				Country firstEnemy;
				Country newEnemy = (Country)sender;
				if(newEnemy == e.MutualProtectionPact.Country1)
					firstEnemy = e.MutualProtectionPact.Country2;
				else
					firstEnemy = e.MutualProtectionPact.Country1;
				message = string.Format(CultureInfo.CurrentCulture, ClientResources.GetString("warDeclared_mutualProtection"),firstEnemy.Name, newEnemy);
				
			}
			else if(e.MutualProtectionPact.Country2 == this.player)
			{
				//we are declaring war because an ally is invoking their 
				//mutual protection pact with us
				message = string.Format(CultureInfo.CurrentCulture, ClientResources.GetString("mutualProtectionInvoked"), e.CommonEnemy.Name, e.CommonEnemy.Name);
			}
			else
				return;

			this.gameWindow.ShowMessageBox(message, ClientResources.GetString(StringKey.GameTitle));
		}

		private void HandleNotifyEndOfTurnChanged(object sender, System.EventArgs e)
		{
			Country ctry = sender as Country;

			if(ctry == this.player)
			{
				string message = string.Empty;
				if(ctry.NotifyEndOfTurn)
				{
					message = ClientResources.GetString(StringKey.TurnFinished);
				}
				else
				{
					message = ClientResources.GetString(StringKey.Ready);
				}

				this.statusView.ActiveUnit = message;
			}
		}

		private void HandleImprovementBuilt(object sender, ImprovementBuiltEventArgs e)
		{
			if(e.City.ParentCountry != this.player)
				return;
			
			IImprovementBuiltControl ctl = 	(IImprovementBuiltControl)GetControl(typeof(IImprovementBuiltControl));

			string message = string.Format(
								CultureInfo.CurrentCulture, 
								ClientResources.GetString(StringKey.ImprovementBuilt), 
								e.City.Name, 
								e.Improvement.Name);

			ctl.Message = message;
			ctl.City = e.City;
			ctl.ShowSimilizationControl();
		}

		private void HandleImprovementDestroyed(object sender, ImprovementDestroyedEventArgs e)
		{
			City city = (City)sender;

			if(city.ParentCountry == this.player || e.AttackingCountry == this.player)
			{
				string text = ClientResources.GetString("city_improvementdestroyed");
				text = string.Format(CultureInfo.CurrentCulture, text, e.Improvement.Name, city.Name);
				this.gameWindow.ShowMessageBox(text, ClientResources.GetString(StringKey.GameTitle));
			}
		}

		private void HandleCitizensKilled(object sender, CitizensKilledEventArgs e)
		{
			City city = (City)sender;
			if(city.ParentCountry == this.player || e.AttackingCountry == this.player)
			{
				string text = ClientResources.GetString("city_citizenskilled");
				text = string.Format(CultureInfo.CurrentCulture, text, city.Name);
				this.gameWindow.ShowMessageBox(text, ClientResources.GetString(StringKey.GameTitle));
			}
		}

		private void HandleGrowthNeededForUnit(object sender, System.EventArgs e)
		{
			City city = sender as City;

			if(city.ParentCountry != this.player)
				return;

			string message = string.Format(
								CultureInfo.CurrentCulture, 
								ClientResources.GetString(StringKey.PopulationGrowthNeeded),
								city.Name, 
								city.NextImprovement.Name);
			this.gameWindow.ShowMessageBox(message, ClientResources.GetString(StringKey.GameTitle));
		}

		private void HandleAudienceRequested(object sender, AudienceRequestedEventArgs e)
		{
			string message = string.Format(CultureInfo.CurrentCulture, ClientResources.GetString(StringKey.AudienceRequest), e.Requester.Civilization.Noun);
			string title = ClientResources.GetString(StringKey.GameTitle);
			bool accepted = this.gameWindow.GetUserConfirmation(message,title);
			Country country = (Country)sender;
			if(accepted)
			{
				bool initialContact = false;
				DiplomaticTie tie = country.GetDiplomaticTie(e.Requester);

				if(tie == null)
				{
					initialContact = true;
					tie = country.EstablishDiplomaticTie(e.Requester);
					e.Requester.EstablishDiplomaticTie(country);
				}

				IDiplomacyControl ctl = GetControl(typeof(IDiplomacyControl)) as IDiplomacyControl;
				ctl.DiplomaticTie = tie;

				ctl.ForeignLeaderHeaderText = string.Format(
											CultureInfo.CurrentCulture,
											ClientResources.GetString(StringKey.DiplomacyCountryHeader), 
											tie.ForeignCountry.Name, 
											GetAttitudeString(tie.Attitude));

				if(initialContact)
					ctl.ForeignLeaderPhrase = AIDiplomacyPhraseHelper.GetFirstContactPhrase(tie);
				else
					ctl.ForeignLeaderHeaderText = AIDiplomacyPhraseHelper.GetForeignLeaderGreeting(tie);
				
				IDiplomacyTaskLinkFactory factory = ctl.GetTaskLinkFactory();

				IDiplomacyTaskLink taskLink;
				Command taskCommand;
				string taskText;
				DiplomacyTask[] tasks = DiplomacyHelper.GetDiplomacyTasks(tie, e.GivenItems, e.TakenItems);

				foreach(DiplomacyTask task in tasks)
				{
					taskCommand = DiplomacyHelper.GetTaskCommand(task, ctl);
					taskText = DiplomacyHelper.GetTaskString(task,tie);
					taskLink = factory.CreateTaskLink(taskText, taskCommand);
					ctl.TaskLinks.Add(taskLink);
				}
				
				ctl.ShowSimilizationControl();
			}
		}

		internal static string GetAttitudeString(Attitude attitude)
		{
			string attitudeText = string.Empty;

			switch(attitude)
			{
				case Attitude.Annoyed:
					attitudeText = ClientResources.GetString(StringKey.AnnoyedAttitude);
					break;
				case Attitude.Cautious:
					attitudeText = ClientResources.GetString(StringKey.CautiousAttitude);
					break;
				case Attitude.Furious:
					attitudeText = ClientResources.GetString(StringKey.FuriousAttitude);
					break;
				case Attitude.Gracious:
					attitudeText = ClientResources.GetString(StringKey.GraciousAttitude);
					break;
				case Attitude.Polite:
					attitudeText = ClientResources.GetString(StringKey.PoliteAttitude);
					break;
			}

			return attitudeText;
		}

		private void HandleResourceCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
		}

		private void HandleTechnologyCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if(e.Action == CollectionChangeAction.Add)
			{
				Technology technology = (Technology)e.Element;
				string message = string.Format(
					CultureInfo.CurrentCulture,
					ClientResources.GetString(StringKey.TechnologyAcquired),
					this.player.Government.LeaderTitle,
					GetResearcher(this.player),
					technology.Name);

				ITechnologyControl ctl = (ITechnologyControl)GetControl(typeof(ITechnologyControl));
				ctl.ShowSimilizationControl();
                ctl.Message = message;
				this.player.ResearchedTechnology = ctl.ChosenTechnology;
			}
		}

		private void HandleEraChanged(object sender, System.EventArgs e)
		{
			Country country = (Country)sender;

			if(country == this.player && player.Era.FirstEra == false)
			{
				string message = string.Format(CultureInfo.CurrentCulture, ClientResources.GetString(StringKey.NextEra), this.player.Government.LeaderTitle);
				this.gameWindow.ShowMessageBox(message, ClientResources.GetString(StringKey.GameTitle));
			}
			this.tileset.CityTiles.Refresh(country);
		}

		private void HandleRevolutionStarted(object sender, System.EventArgs e)
		{
			if(sender != this.player)
				return;
			string message = string.Format(
								CultureInfo.CurrentCulture, 
								ClientResources.GetString(StringKey.Revolution), 
								this.player.Government.Name);
			this.gameWindow.ShowMessageBox(message, ClientResources.GetString(StringKey.GameTitle));
		}

		private void HandleRevolutionEnded(object sender, RevolutionEndedEventArgs e)
		{
			if(sender != this.player)
				return;

			IGovernmentSelectionControl ctl = (IGovernmentSelectionControl)GetControl(typeof(IGovernmentSelectionControl));
			string message = ClientResources.GetString(StringKey.GovernmentSelection);
			ctl.Message = message;
			ctl.ShowSimilizationControl();
		}

		private void HandleVillageEncounter(object sender, VillageEventArgs e)
		{
			VillageGoody goody = e.Goody;

			string message = string.Empty;
			
			if(goody.GetType() == typeof(UnitGoody))
			{
				message = string.Format(
					CultureInfo.CurrentCulture,
					ClientResources.GetString(StringKey.VillageUnit),
					e.Village.TribeName, 
					((UnitGoody)goody).Unit.Name);
			}
			else if(goody.GetType() == typeof(SettlerGoody))
			{
				message = string.Format(
					CultureInfo.CurrentCulture,
					ClientResources.GetString(StringKey.VillageSettler),
					e.Village.TribeName,
					((SettlerGoody)goody).Settler.Name,
					this.player.Government.Name);
			}
			else if(goody.GetType() == typeof(CityGoody))
			{
				message = string.Format(
					CultureInfo.CurrentCulture,
					ClientResources.GetString(StringKey.VillageCity),
					e.Village.TribeName);

			}
			else if(goody.GetType() == typeof(BarbarianGoody))
			{
				message = string.Format(
					CultureInfo.CurrentCulture,
					ClientResources.GetString(StringKey.VillageBarbarian),
					e.Village.TribeName,
					((BarbarianGoody)goody).Barbarian.Name);
			}
			else if(goody.GetType() == typeof(TechnologyGoody))
			{
				message = string.Format(
					CultureInfo.CurrentCulture,
					ClientResources.GetString(StringKey.VillageTechnology),
					e.Village.TribeName,
					((TechnologyGoody)goody).Technology.Name);

			}
			else if(goody.GetType() == typeof(MapGoody))
			{
				message = string.Format(
					CultureInfo.CurrentCulture,
					ClientResources.GetString(StringKey.VillageMap),
					e.Village.TribeName);
			}
			else if(goody.GetType() == typeof(EmptyGoody))
			{
				message = string.Format(
					CultureInfo.CurrentCulture,
					ClientResources.GetString(StringKey.VillageDeserted),
					e.Village.TribeName);
			}
			else if(goody.GetType() == typeof(GoldGoody))
			{
				message = string.Format(
					CultureInfo.CurrentCulture,
					ClientResources.GetString(StringKey.VillageGold),
					((GoldGoody)goody).Gold.ToString(CultureInfo.CurrentCulture),
					e.Village.TribeName);
						
			}
			
			this.gameWindow.ShowMessageBox(message, ClientResources.GetString(StringKey.GameTitle));
		}

		private void HandleMoved(object sender, System.EventArgs e)
		{
			RefreshStatusView();
		}

		private void HandleTurnFinished(object sender, System.EventArgs e)
		{
			RefreshStatusView();
		}

		private string GetYearString()
		{
			string year = Math.Abs(this.root.Year).ToString(CultureInfo.CurrentCulture);
			string formatted = string.Empty;
			if(this.root.Year >= 0)
			{
				formatted = String.Format(CultureInfo.CurrentCulture, ClientResources.GetString(StringKey.PositiveYear), year);
			}
			else
			{
				formatted = String.Format(CultureInfo.CurrentCulture, ClientResources.GetString(StringKey.NegativeYear), year);
			}

			return formatted;
		}

		private string GetTechMessage()
		{
			string message = string.Empty;
			if(this.player.ResearchedTechnology != null)
			{
				message = string.Format(
					CultureInfo.CurrentCulture,
					ClientResources.GetString(StringKey.Researching), 
					this.player.ResearchedTechnology.Name);
			}
			else
			{
				message = ClientResources.GetString(StringKey.NoResearch);
			}

			return message;
		}

		private void HandleTradeProposed(object sender, TradeProposedEventArgs e)
		{
			//STUB
		}

		private void HandleResearchDirectionNeeded(object sender, System.EventArgs e)
		{
			if(sender != this.player)
				return;
			IResearchNeededControl ctl = (IResearchNeededControl)GetControl(typeof(IResearchNeededControl));
			string message = string.Format(
								CultureInfo.CurrentCulture,
								ClientResources.GetString(StringKey.TechDirectionNeeded),
								this.player.Government.LeaderTitle,
								GetResearcher(this.player));
			ctl.Message = message;
			ctl.ShowSimilizationControl();
			this.player.ResearchedTechnology = ctl.ChosenTechnology;
		}

		private void HandleResearchedTechnologyChanged(object sender, System.EventArgs e)
		{
			RefreshStatusView();
		}

		private void HandleNewGovernmentAvailable(object sender, GovernmentAvailableEventArgs e)
		{
			if(sender != this.player)
				return;
		
			string message = string.Format(
								CultureInfo.CurrentCulture,
								ClientResources.GetString(StringKey.NewGovernmentAvailable), 
								this.player.Government.LeaderTitle, 
								e.Government.Name);
			INewGovernmentControl ctl = (INewGovernmentControl)GetControl(typeof(INewGovernmentControl));
			ctl.Government = e.Government;
			ctl.Message = message;
			ctl.ShowSimilizationControl();
		}

		private void HandleDefeated(object sender, DefeatedEventArgs e)
		{
			string message = string.Empty;
			if(e.Conqueror == this.player)
			{
				message = string.Format(
							CultureInfo.CurrentCulture,
							ClientResources.GetString(StringKey.PlayerDestroysCivilization),
							string.Empty,
							e.Victim.Name,
							this.player.Government.LeaderTitle);
			}
			else if(e.Victim == this.player)
			{
				message = string.Format(
								CultureInfo.CurrentCulture,
								ClientResources.GetString(StringKey.PlayerDestroyed),
								e.Conqueror.Name);
			}
			else
			{
				message = string.Format(
								CultureInfo.CurrentCulture,
								ClientResources.GetString(StringKey.ForeignCivilizationDestroyed),
								this.player.Government.LeaderTitle,
								string.Empty,
								e.Victim.Name, 
								e.Conqueror.Name);
			}

			this.gameWindow.ShowMessageBox(message, ClientResources.GetString(StringKey.GameTitle));
		}

		private void HandleActiveUnitChanged(object sender, System.EventArgs e)
		{
			this.gameWindow.ActiveUnit = this.root.ActiveUnit;
			RefreshStatusView();
			UpdateCommandAvailability();
		}

		private void RefreshStatusView()
		{
			Unit unit = this.root.ActiveUnit;
			this.statusView.Year = GetYearString();
			if(unit != null)
			{
				this.gameWindow.CenterCoordinates = unit.Coordinates;
				if(unit.ParentCountry == this.player)
				{
					this.statusView.ActiveUnit = unit.Name;
					this.statusView.Gold = string.Format(
										CultureInfo.CurrentCulture, 
										ClientResources.GetString(StringKey.GoldAmount), 
										Convert.ToString(this.player.Gold, CultureInfo.CurrentCulture));

					this.statusView.MovesLeft = string.Format(
							CultureInfo.CurrentCulture,
							ClientResources.GetString("statusView_MovesLeft"), 
							Convert.ToString(unit.OffensivePower, CultureInfo.CurrentCulture), 
							Convert.ToString(unit.Defense, CultureInfo.CurrentCulture), 
							Convert.ToString(unit.MovesLeft, CultureInfo.CurrentCulture), 
							Convert.ToString(unit.MovesPerTurn, CultureInfo.CurrentCulture));
                    
                    GridCell unitCell = this.root.Grid.GetCell(unit.Coordinates);
					this.statusView.UnitImage = (Image)Tileset.UnitTiles[unit.Name];
					this.statusView.Terrain = unitCell.Terrain.Name;
					this.statusView.Government = player.Civilization.Adjective + " " + player.Government.Name;
					if(this.player.ResearchedTechnology != null)
						this.statusView.Technology = player.ResearchedTechnology.Name;
					else
						this.statusView.Technology = ClientResources.GetString("statusView_NoResearch");
				}
			}
			else
			{
				this.statusView.ActiveUnit = ClientResources.GetString(StringKey.NoActiveUnit);
				this.statusView.MovesLeft = string.Empty;
			}
		}

		private void HandleHitPointLost(object sender, System.EventArgs e)
		{
			//STUB
		}

		private void HandleCombatStart(object sender, CombatEventArgs e)
		{
			//STUB
		}

		private void HandleCityStarvation(object sender, System.EventArgs e)
		{
			City city = sender as City;

			if(city.ParentCountry == this.player)
			{
				string message = String.Format(CultureInfo.CurrentCulture, ClientResources.GetString(StringKey.Starved), city.Name);
				this.gameWindow.ShowMessageBox(message, ClientResources.GetString(StringKey.GameTitle), city.Coordinates);
			}
		}

		private void HandleUnitKilled(object sender, System.EventArgs e)
		{
			Unit unit = sender as Unit;
			if(unit.ParentCountry == this.player && this.Options.ShowKilledMessage)
			{
				string message = string.Format(
									CultureInfo.CurrentCulture, 
									ClientResources.GetString(StringKey.UnitKilled), 
									unit.Name);
				this.gameWindow.ShowMessageBox(message, ClientResources.GetString(StringKey.GameTitle));
			}
		}

		private void HandleUnitCaptured(object sender, UnitCapturedEventArgs e)
		{
			if(e.UnitCaptured.ParentCountry == this.player)
			{
				//we lost the unit
				string text = string.Format(
								CultureInfo.InvariantCulture, 
								ClientResources.GetString("unit_Captured"), 
								e.UnitCaptured.Name);
				this.gameWindow.ShowMessageBox(text, ClientResources.GetString(StringKey.GameTitle));
			}
			else if(e.CapturedBy.ParentCountry == this.player)
			{
				//we got the unit
				string text = string.Format(
								CultureInfo.InvariantCulture, 
								ClientResources.GetString("player_capturesUnit"), 
								e.UnitCaptured.Name);
				this.gameWindow.ShowMessageBox(text, ClientResources.GetString(StringKey.GameTitle));
			}
		}

		private void HandleCityStatusChanged(object sender, CityStatusEventArgs e)
		{            
            City city = (City)sender;
			if(city.ParentCountry != this.player)
				return;
			
			string message = string.Empty;
			switch(e.CityStatus)
			{
				case CityStatus.Normal:
					if(e.PreviousStatus == CityStatus.LoveTheMayor)
					{
						message = string.Format(CultureInfo.CurrentCulture, ClientResources.GetString(StringKey.WeLoveTheKingEnd), city.Name);
					}
					else
					{
						message = string.Format(CultureInfo.CurrentCulture, ClientResources.GetString(StringKey.OrderRestored), city.Name);
					}
					break;
				case CityStatus.Disorder:
					message = string.Format(CultureInfo.CurrentCulture, ClientResources.GetString(StringKey.CityDisorder), city.Name);
					break;
				case CityStatus.LoveTheMayor:
					message = string.Format(CultureInfo.CurrentCulture, ClientResources.GetString(StringKey.WeLoveTheKing), city.Name);
					break;
			}
			this.gameWindow.ShowMessageBox(message, ClientResources.GetString(StringKey.GameTitle), city.Coordinates);
		}
	
		private void HandleCityCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if(e.Action == CollectionChangeAction.Add)
			{
				City city = (City)e.Element;
				RegisterCity(city);
			}
		}

		private void HandleUnitCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if(e.Action == CollectionChangeAction.Add)
			{
				Unit unit = (Unit)e.Element;
				RegisterUnit(unit);
			}
		}

		private void HandleNewGame(object sender, System.EventArgs e)
		{
			StartNewGameCommand cmd = (StartNewGameCommand)this.commands["StartNewGameCommand"];
			//	Register the human player with the server.
			
            Country country = CountryFactory.CreateCountry(cmd.Civilization, cmd.LeaderName, false, cmd.PlayerColor);
            this.root.Countries.Add(country);
            this.player = country;
			int idx = 0;
			System.Drawing.Color opponentColor;
			//	Register the AI opponents.
			foreach(Civilization civ in cmd.Opponents)
			{
				do
				{
					opponentColor = PlayerColor.FromIndex(idx++);

				} while(opponentColor == cmd.PlayerColor);
                country = CountryFactory.CreateCountry(civ, civ.LeaderName, true, opponentColor);
				this.root.Countries.Add(country);
			}
			
			this.root.Grid.CellImprovementDestroyed += new EventHandler<CellImprovementDestroyedEventArgs>(HandleCellImprovementDestroyed);
			this.root.StartGame();
			//Enable the user to save the game now that it's started.
			this.commands["SaveGameCommand"].Enabled = true;
			RefreshStatusView();
		}

		private void HandleLoadedGame(object sender, EventArgs e)
		{
			int count = 0;
			foreach(Country country in this.root.Countries)
			{
				if(country.GetType() == typeof(Country))
				{
					this.player = country;
					count++;
				}
			}

			if(count > 1)
			{
				//what to do here?
			}

			this.root.Grid.CellImprovementDestroyed += new EventHandler<CellImprovementDestroyedEventArgs>(HandleCellImprovementDestroyed);
			string message = string.Format(CultureInfo.CurrentCulture, ClientResources.GetString("item_loading"), this.options.TilesetPath);
			this.messageConsole.WriteLine(message);
			this.LoadTileset(this.options.TilesetPath);
			this.root.StartLoadedGame();
			//Enable the user to save the game now that it's started.
			this.commands["SaveGameCommand"].Enabled = true;
			this.messageConsole.WriteLine(ClientResources.GetString("game_loaded"));
			RefreshStatusView();
		}

		private void HandleCellImprovementDestroyed(object sender, CellImprovementDestroyedEventArgs e)
		{
			if(e.Cell.Owner == this.player)
			{
				City city = e.Cell.FindClosestDomesticCity(this.player);
				string text = ClientResources.GetString("cell_enemydestroysimprovements");
				text = string.Format(CultureInfo.CurrentCulture, text, e.DestroyedBy.ParentCountry.Civilization.Noun, city.Name);
				this.gameWindow.ShowMessageBox(text, ClientResources.GetString(StringKey.GameTitle));
			}
		}

		private void HandleCityCaptured(object sender, CapturedEventArgs e)
		{
			string message = string.Empty;
			City city = (City)sender;

			if(e.Loser == this.player)
			{
				message = String.Format(
								CultureInfo.CurrentCulture,
								ClientResources.GetString(StringKey.CityLostToInvasion),
								this.player.Government.LeaderTitle, 
								city.Name, 
								e.Invader.Name, 
								e.GoldPlundered.ToString(CultureInfo.CurrentCulture));
			}
			else if(e.Invader == this.player)
			{
				message = String.Format(
								CultureInfo.CurrentCulture,
								ClientResources.GetString(StringKey.CityCaptured), 
								city.Name, 
								e.GoldPlundered.ToString(CultureInfo.CurrentCulture));
			}
			else
			{
				return;
			}

			this.gameWindow.ShowMessageBox(message, ClientResources.GetString(StringKey.GameTitle), city.Coordinates);
		}

		private void HandleStatusChanged(object sender, StatusChangedEventArgs e)
		{
			this.messageConsole.WriteLine(e.Message);
		}

		private void HandleGameStart(object sender, System.EventArgs e)
		{
			string attribute1 = string.Empty;
			string attribute2 = string.Empty;
			GameRoot root = GameRoot.Instance;
			string t = string.Empty;
			Civilization civ = this.player.Civilization;
			if(civ.Commercial)
			{
				attribute1 = ClientResources.GetString(StringKey.CommercialAttribute); 
			}
			if(civ.Expansionist)
			{
				t = ClientResources.GetString(StringKey.ExpansionistAttribute);
				if(attribute1.Length == 0)
				{
					attribute1 = t;
				}
				else if(attribute2.Length == 0)
				{
					attribute2 = t;
				}
			}
			if(civ.Industrious)
			{
				t = ClientResources.GetString(StringKey.IndustriousAttribute);
				if(attribute1.Length == 0)
				{
					attribute1 = t;
				}
				else if(attribute2.Length == 0)
				{
					attribute2 = t;
				}
			}
			if(civ.Militaristic)
			{
				t = ClientResources.GetString(StringKey.MilitaristicAttribute);
				if(attribute1.Length == 0)
				{
					attribute1 = t;
				}
				else if(attribute2.Length == 0)
				{
					attribute2 = t;
				}
			}
			if(civ.Religious)
			{
				t = ClientResources.GetString(StringKey.ReligiousAttribute);
				if(attribute1.Length == 0)
				{
					attribute1 = t;
				}
				else if(attribute2.Length == 0)
				{
					attribute2 = t;
				}
			}
			if(civ.Scientific)
			{
				t = ClientResources.GetString(StringKey.ScientificAttribute);
				if(attribute1.Length == 0)
				{
					attribute1 = t;
				}
				else if(attribute2.Length == 0)
				{
					attribute2 = t;
				}
			}

			string techName;
			techName = this.player.AcquiredTechnologies[0].Name;

			string message = string.Format(
								CultureInfo.CurrentCulture,
								ClientResources.GetString(StringKey.GameStart),
								this.player.LeaderName, 
								attribute1, 
								attribute2,
								techName);

			this.gameWindow.ShowSimilizationControl();
			this.miniMap.InitializeMap(root.Grid);
			this.gameWindow.ShowMessageBox(message, ClientResources.GetString(StringKey.GameTitle));
			this.commands["DisplayMilitaryAdvisorCommand"].Enabled = true;
			this.commands["DisplayForeignAdvisorCommand"].Enabled = true;
			this.commands["DisplayDomesticAdvisorCommand"].Enabled = true;
		}

		private void HandleLoadedGameStarted(object sender, EventArgs e)
		{
			this.gameWindow.ShowSimilizationControl();
			this.miniMap.InitializeMap(root.Grid);
		}

		private void HandleGameEnded(object sender, GameEndedEventArgs e)
		{
			string message = string.Empty;
			switch(e.VictoryType)
			{
				case VictoryType.CulturalVictory:
					break;
				case VictoryType.DiplomaticVictory:
					break;
				case VictoryType.DominationVictory:
					break;
				case VictoryType.MilitaryVictory:
					message = string.Format(
						CultureInfo.CurrentCulture,
						ClientResources.GetString(StringKey.MilitaryVictory),
						this.player.Government.LeaderTitle,
						this.player.Name);
					break;
				case VictoryType.SpaceVictory:
					break;
			}
			this.gameWindow.ShowMessageBox(message, ClientResources.GetString(StringKey.GameTitle));
		}

		/// <summary>
		/// Gets a control from the client based on the name.  The control must 
		/// implement the <c>ISimilizationControl</c> interface.
		/// </summary>
		/// <param name="controlType"></param>
		/// <returns></returns>
		public abstract ISimilizationControl GetControl(System.Type controlType);

		private void UpdateCommandAvailability()
		{
			Unit unit = root.ActiveUnit;

			if(unit == null)
			{
				this.commands["IrrigateCommand"].Enabled = false;
				this.commands["MoveToCommand"].Enabled = false;
				this.commands["BuildNewCityCommand"].Enabled = false;
				this.commands["BuildRoadCommand"].Enabled = false;
				this.commands["FortifyCommand"].Enabled = false;
				this.commands["BuildMineCommand"].Enabled = false;
				this.commands["DisbandCommand"].Enabled = false;
				this.commands["MergeUnitCommand"].Enabled = false;
				this.commands["BombardCommand"].Enabled = false;
			}
			else
			{
				this.commands["IrrigateCommand"].Enabled = unit.CanWork;
				this.commands["MoveToCommand"].Enabled = true;
				this.commands["BuildNewCityCommand"].Enabled = unit.CanSettle;
				this.commands["BuildRoadCommand"].Enabled = unit.CanWork;
				this.commands["FortifyCommand"].Enabled = true;
				this.commands["BuildMineCommand"].Enabled = unit.CanWork;
				this.commands["DisbandCommand"].Enabled = true;
				this.commands["MergeUnitCommand"].Enabled = unit.CanMergeWithCity;
				this.commands["BombardCommand"].Enabled = unit.CanBombard;
			}
		}

		private static string GetResearcher(Country player)
		{
			Collection<string> researchers = player.Era.Researchers;
			int idx = RandomNumber.GetRandomNumber(researchers.Count-1);
			return researchers[idx];
		}

		private void InitializeCommands()
		{
			this.commands = new NamedObjectCollection<Command>();

			StartNewGameCommand newGameCmd = new StartNewGameCommand();
			newGameCmd.Invoked += new EventHandler(HandleNewGame);
			LoadGameCommand loadGameCmd = new LoadGameCommand();
			loadGameCmd.Invoked += new EventHandler(HandleLoadedGame);

			//
			//	GAME COMMANDS
			//
			this.commands.Add(newGameCmd);
			this.commands.Add(loadGameCmd);
            this.commands.Add(new AboutCommand());
			this.commands.Add(new SaveGameCommand());
			this.commands.Add(new StartNewGameCommand());
			this.commands.Add(new DisplayDomesticAdvisorCommand());
            this.commands.Add(new EstablishEmbassyCommand());
			this.commands.Add(new GameOptionsCommand());
			this.commands.Add(new DisplayHistographCommand());
			this.commands.Add(new DisplayDomesticAdvisorCommand());
			this.commands.Add(new DisplayForeignAdvisorCommand());
			this.commands.Add(new DisplayMilitaryAdvisorCommand());
			this.commands.Add(new StartDiplomacyCommand());
            this.commands.Add(new InvestigateCityCommand());
            this.commands.Add(new SabotageCommand());
            this.commands.Add(new SpreadPropagandaCommand());
            this.commands.Add(new StealPlansCommand());
            this.commands.Add(new StealTechnologyCommand());
            this.commands.Add(new StealWorldMapCommand());
            this.commands.Add(new PlantDiseaseCommand());
            this.commands.Add(new PlantSpyCommand());
            this.commands.Add(new ExposeSpyCommand());
			this.commands.Add(new HelpCommand());

			//
			//	UNIT COMMANDS
			//
			this.commands.Add(new IrrigateCommand());
			this.commands.Add(new MoveToCommand());
			this.commands.Add(new BuildNewCityCommand());
			this.commands.Add(new BuildRoadCommand());
			this.commands.Add(new FortifyCommand());
			this.commands.Add(new BuildMineCommand());
			this.commands.Add(new DisbandCommand());
			this.commands.Add(new MergeUnitCommand());
			this.commands.Add(new BombardCommand());
			
			//Disable commands that require an active game.         
			this.commands["SaveGameCommand"].Enabled = false;
			this.commands["DisplayDomesticAdvisorCommand"].Enabled = false;
			this.commands["DisplayHistographCommand"].Enabled = false;
			this.commands["DisplayDomesticAdvisorCommand"].Enabled = false;
			this.commands["DisplayForeignAdvisorCommand"].Enabled = false;
			this.commands["DisplayMilitaryAdvisorCommand"].Enabled = false;
			this.commands["StartDiplomacyCommand"].Enabled = false;
			this.commands["IrrigateCommand"].Enabled = false;
			this.commands["MoveToCommand"].Enabled = false;
			this.commands["BuildNewCityCommand"].Enabled = false;
			this.commands["BuildRoadCommand"].Enabled = false;
			this.commands["FortifyCommand"].Enabled = false;
			this.commands["BuildMineCommand"].Enabled = false;
			this.commands["DisbandCommand"].Enabled = false;
			this.commands["MergeUnitCommand"].Enabled = false;
			this.commands["BombardCommand"].Enabled = false;
            this.commands["InvestigateCityCommand"].Enabled = false;
            this.commands["SabotageCommand"].Enabled = false;
            this.commands["SpreadPropagandaCommand"].Enabled = false;
            this.commands["StealPlansCommand"].Enabled = false;
            this.commands["StealTechnologyCommand"].Enabled = false;
            this.commands["StealWorldMapCommand"].Enabled = false;
            this.commands["PlantDiseaseCommand"].Enabled = false;
            this.commands["PlantSpyCommand"].Enabled = false;
            this.commands["ExposeSpyCommand"].Enabled = false;
            this.commands["EstablishEmbassyCommand"].Enabled = false;

            //other command initialization
            StartNewGameCommand cmd = (StartNewGameCommand)this.commands["StartNewGameCommand"];
            cmd.RulesetPath = this.options.StartingRulesetPath;
            cmd.TilesetPath = this.options.TilesetPath;
		}
        
        /// <summary>
        /// Releases all resources used by the <see cref="ClientApplication"/> class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);    
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                if (this.options != null)
                    this.options.Dispose();                
                this.disposed = true;
            }
        }
    }
}
