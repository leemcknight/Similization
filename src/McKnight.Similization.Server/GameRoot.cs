using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.IO;
using System.Text;
using System.Xml;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// This is the main "game" object.
	/// </summary>
	/// <remarks>The <see cref="GameRoot"/> class implements the Singleton pattern.  Only one 
	/// instance of the <see cref="GameRoot"/> class can ever exist.  Furthermore, the <see cref="GameRoot"/> 
	/// class cannot be instantiated directly, rather an instance must be retrieved from the 
	/// static <i>Instance</i> property.</remarks>
	public sealed class GameRoot
	{
        private int year;        
		private Grid grid;
		private Country activeCountry;
        private Unit activeUnit;
        private NamedObjectCollection<Country> countries;        
		private Rules rules;
        private Ruleset ruleset;
        private History history;
		private static GameRoot gameRoot;
        private event EventHandler activeUnitChanged;
        private event EventHandler gameStarted;
        private event EventHandler loadedGameStarted;
        private event EventHandler countriesLoaded;
        private event EventHandler serverStarted;
        private event EventHandler serverStarting;
        private event EventHandler turnFinished;
        private event EventHandler<GameEndedEventArgs> gameEnded;
        private event EventHandler<StatusChangedEventArgs> statusChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="GameRoot"/> class.
		/// </summary>
		private GameRoot()
		{
            this.history = new History();
            this.countries = new NamedObjectCollection<Country>();            
			this.countries.CollectionChanged += new CollectionChangeEventHandler(HandleCountryCollectionChange);
		}
		
		/// <summary>
		/// The unit currently active in the game
		/// </summary>
		public Unit ActiveUnit
		{
			get { return this.activeUnit; }
		}

		/// <summary>
		/// The country that is currently taking it's turn
		/// in the game.
		/// </summary>
		public Country ActiveCountry
		{
			get { return this.activeCountry; }
		}
		
		/// <summary>
		/// Gets a list of countries in the game
		/// </summary>
        public NamedObjectCollection<Country> Countries
		{
			get { return this.countries; }
		}

		/// <summary>
		/// Gets the grid being used in the game.
		/// </summary>
		public Grid Grid
		{
			get { return this.grid; }
		}
		
		/// <summary>
		/// Gets the current year for the game
		/// </summary>
		public int Year
		{
			get { return this.year; }
		}
		
		/// <summary>
		/// Gets the game history.
		/// </summary>
		public History History
		{
			get { return this.history; }
		}

		/// <summary>
		/// Gets or sets the rules of the game.
		/// </summary>
		public Rules Rules
		{
			get { return this.rules; }
			set { this.rules = value; }
		}

		/// <summary>
		/// The <see cref="Ruleset"/> the game is currently playing on.
		/// </summary>
		public Ruleset Ruleset
		{
			get { return ruleset; }
		}

		/// <summary>
		/// Gets an instance of the Game Root object.  This is a singleton implementation,
		/// and will always return the same instance.
		/// </summary>
		/// <returns></returns>
		public static GameRoot Instance
		{
			get
			{
				if(gameRoot == null)				
					gameRoot = new GameRoot();				
				return gameRoot;
			}
		}		
		
		/// <summary>
		/// Event that fires whenever the active unit in the game is changed.  This 
		/// happens whenever the turn for a unit is finished.
		/// </summary>
		public event EventHandler ActiveUnitChanged
		{
			add
			{
				this.activeUnitChanged += value;
			}

			remove
			{
				this.activeUnitChanged -= value;
			}
		}

		/// <summary>
		/// Fires the <i>ActiveUnitChanged</i> event.
		/// </summary>
		private void OnActiveUnitChanged()
		{
			if(this.activeUnitChanged != null)
			{
				this.activeUnitChanged(null, null);
			}
		}
		
		/// <summary>
		/// Event that fires when the game begins.
		/// </summary>
		public event EventHandler GameStarted
		{
			add
			{
				this.gameStarted += value;
			}

			remove
			{
				this.gameStarted -= value;
			}
		}

		/// <summary>
		/// Fires the <i>GameStarted</i> event.
		/// </summary>
		private void OnGameStarted()
		{
			if(this.gameStarted != null)
			{
				this.gameStarted(this, EventArgs.Empty);
			}
		}
		
		/// <summary>
		/// Occurs when the game is successfully loaded by the server and started.
		/// </summary>
		public event EventHandler LoadedGameStarted
		{
			add
			{
				this.loadedGameStarted += value;
			}

			remove
			{
				this.loadedGameStarted -= value;
			}
		}

		private void OnLoadedGameStarted()
		{
			if(this.loadedGameStarted != null)
			{
				this.loadedGameStarted(this, EventArgs.Empty);
			}
		}
		
		/// <summary>
		/// Occurs whenever a player has won the game.
		/// </summary>
		public event EventHandler<GameEndedEventArgs> GameEnded
		{
			add
			{
				this.gameEnded += value;
			}

			remove
			{
				this.gameEnded -= value;
			}
		}

		/// <summary>
		/// Fires the <i>GameEnded</i> event.
		/// </summary>
		/// <param name="e"></param>
		private void OnGameEnded(GameEndedEventArgs e)
		{
			if(this.gameEnded != null)
			{
				this.gameEnded(this,e);
			}
		}
		
		/// <summary>
		/// Event that fires whent the GameRoot object is finished loading countries
		/// from the save game file.
		/// </summary>
		public event EventHandler CountriesLoaded
		{
			add
			{
				this.countriesLoaded += value;
			}

			remove
			{
				this.countriesLoaded -= value;
			}
		}

		private void OnCountriesLoaded()
		{
			if(this.countriesLoaded != null)
			{
				this.countriesLoaded(this, EventArgs.Empty);
			}
		}
		
		/// <summary>
		/// Event that fires after the server has been successfully started.
		/// </summary>
		public event EventHandler ServerStarted
		{
			add
			{
				this.serverStarted += value; 
			}

			remove
			{
				this.serverStarted -= value;
			}
		}

		/// <summary>
		/// Fires the <i>ServerStarted</i> event.
		/// </summary>
		private void OnServerStarted()
		{
			if(this.serverStarted != null)
			{
				this.serverStarted(this, EventArgs.Empty);
			}
		}
		
		/// <summary>
		/// Event that fires when someone starts the server.
		/// </summary>
		public event EventHandler ServerStarting
		{
			add
			{
				this.serverStarting += value;
			}

			remove
			{
				this.serverStarting -= value;
			}
		}

		/// <summary>
		/// Fires the <i>ServerStarting</i> event.
		/// </summary>
		private void OnServerStarting()
		{
			if(this.serverStarting != null)
			{
				this.serverStarting(this, EventArgs.Empty);
			}
		}
		
		/// <summary>
		/// Occurs when a Server Status changes.
		/// </summary>
		public event EventHandler<StatusChangedEventArgs> StatusChanged
		{
			add
			{
				this.statusChanged += value; 
			}
			remove
			{
				this.statusChanged -= value;
			}
		}

		/// <summary>
		/// Fires the <i>StatusChanged</i> event.
		/// </summary>
		/// <param name="e"></param>
		private void OnStatusChanged(StatusChangedEventArgs e)
		{
			if(this.statusChanged != null)
			{
				this.statusChanged(this, e);
			}
		}
		
		/// <summary>
		/// Event that fires whenever a round of turns is finished in the game.
		/// </summary>
		public event EventHandler TurnFinished
		{
			add
			{
				this.turnFinished += value;
			}

			remove
			{
				this.turnFinished -= value;
			}
		}

		/// <summary>
		/// Fires the <i>TurnFinished</i> event.
		/// </summary>
		private void OnTurnFinished()
		{
			if(this.turnFinished != null)
			{
				this.turnFinished(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Loads the Ruleset from the file.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public DataSet LoadRuleset(string path)
		{
            DataSet rulesetDataSet = new DataSet();
            rulesetDataSet.Locale = CultureInfo.InvariantCulture;
			rulesetDataSet.ReadXmlSchema("Ruleset.xsd");
			rulesetDataSet.ReadXml(path);
			this.ruleset = new Ruleset(rulesetDataSet);
			this.ruleset.FullPath = path;
			return rulesetDataSet;
		}

		private void HandleCountryCollectionChange(object sender, CollectionChangeEventArgs e)
		{
			if(e.Action == CollectionChangeAction.Remove)
			{
				if(this.countries.Count == 1)
				{
					GameEndedEventArgs ge = 
						new GameEndedEventArgs(this.countries[0], 
						VictoryType.MilitaryVictory);

					OnGameEnded(ge);
				}
			}
		}

		/// <summary>
		/// Starts a new server session.
		/// </summary>
		/// <param name="worldSize"></param>
		/// <param name="age"></param>
		/// <param name="climate"></param>
		/// <param name="landmass"></param>
		/// <param name="temperature"></param>
		/// <param name="waterCoverage"></param>
        /// <param name="barbarianAggressiveness"></param>
        /// <param name="difficulty"></param>
		/// <param name="rules"></param>
		public void StartServer(WorldSize worldSize, Age age, Temperature temperature, Climate climate, Landmass landmass, WaterCoverage waterCoverage, BarbarianAggressiveness barbarianAggressiveness, Difficulty difficulty, Rules rules)
		{
			OnServerStarting();
			this.rules = rules;
			OnStatusChanged(new StatusChangedEventArgs(ServerResources.CreatingMap, 5));
            GridBuilder builder = new GridBuilder();
            this.grid = builder.Build(worldSize, age, temperature, climate, landmass, waterCoverage, this.ruleset);						
			OnStatusChanged(new StatusChangedEventArgs(ServerResources.AddingVillages, 10));
			AddVillages(worldSize);
			this.year = -4000;			
			OnServerStarted();
		}

		/// <summary>
		/// Starts the game.
		/// </summary>
		public void StartGame()
		{
			this.activeCountry = this.countries[0];
			this.activeUnit = (Unit)this.activeCountry.Units[0];
			SetInitialGovernment();
			SetInitialEra();
			OnGameStarted();
		}

		/// <summary>
		/// Starts a pre-loaded game.
		/// </summary>
		public void StartLoadedGame()
		{
			OnLoadedGameStarted();
		}

		
		// Sets the era of all the countries in the game to the starting era.		
		private void SetInitialEra()
		{
			Era first = null;

			foreach(Era era in this.ruleset.Eras)
			{
				if(era.FirstEra)
				{
					first = era;
					break;
				}
			}

			foreach(Country c in this.countries)
			{
				c.Era = first;
				c.UpdateResearchableTechnologies();
				c.UpdateAvailableResources();
			}
		}
		
		// Sets the government of all the countries in the game to the starting government.		
		private void SetInitialGovernment()
		{
			Government first = null;
			
			foreach(Government government in this.ruleset.Governments)
			{
				if(government.Primary)
				{
					first = government;
					break;
				}
			}

			if(first == null)
				throw new InvalidOperationException(ServerResources.RulesetDoesNotHavePrimaryGovernment);
		
			foreach(Country country in this.countries)
			{
				country.Government = first;
			}
		}


		// Gets a GridCell object for the starting cell of the player.
		private GridCell GetStartingCell()
		{
			int cellCount = this.grid.DryCells.Count;
			int index = RandomNumber.Between(0, cellCount);
			return this.grid.DryCells[index];
		}

		
		// Executes the end-of-turn processes related to the game,
		// such as resetting the active unit, incrementing the year,
		// and updating shields, gold, and other ResourceType for all 
		// the cities in the game.		
		private void DoTurn()
		{
			HistoryItem historyItem;

			foreach(Country colony in this.countries)
			{
				colony.DoTurn();
				historyItem = new HistoryItem();
				historyItem.Country = colony;
				historyItem.CulturePoints = colony.CulturePoints;
				historyItem.Power = colony.PowerFactor;
				historyItem.Score = colony.Score;
				historyItem.Year = this.year;
				this.history.AddHistoryItem(historyItem);
				colony.NotifyEndOfTurn = false;
			}

			//TODO: correctly increment year based on current year
			this.year++;

			OnTurnFinished();
		}

		/// <summary>
		/// Finds the next <see cref="Unit"/> that should be active and activates the <see cref="Unit"/>
		/// </summary>
		/// <returns>The <see cref="Unit"/> that is now active.</returns>
		public Unit ActivateNextUnit()
		{
			int idx = 0;
			Unit nextUnit = null;
			bool moveToNextCountry = false;
			if(this.activeUnit != null)
			{
				idx = this.activeCountry.Units.IndexOf(this.activeUnit);
				idx++;
			}

			if(idx >= this.activeCountry.Units.Count)
			{
				//need to move to the next country.
				moveToNextCountry = true;
				this.activeUnit = null;
			}
			else
			{
				do
				{
					if(idx < this.activeCountry.Units.Count)					
						nextUnit = (Unit)this.activeCountry.Units[idx];					
				} while(!nextUnit.Active && idx++ < this.activeCountry.Units.Count);
			}
			
			//still possible we have an inactive unit at this point, if the last 
			//unit checked was inactive.
			if(nextUnit != null && nextUnit.Active)
			{
				this.activeUnit = nextUnit;
				if(!this.activeCountry.UnitActivatedOnTurn)
				{
					this.activeCountry.UnitActivatedOnTurn = true;
				}
			}
			else
			{
				this.activeUnit = null;
			}

			if(this.activeUnit == null && !this.activeCountry.UnitActivatedOnTurn)
			{
				this.activeCountry.NotifyEndOfTurn = true;
			}
			else if(moveToNextCountry)
			{
				ActivateNextCountry();
			}

			OnActiveUnitChanged();

			return this.activeUnit;
		}

		
		// Event handler for a unit finishing its' turn.		
		private void OnUnitTurnFinished(object sender, System.EventArgs e)
		{
			ActivateNextUnit();
		}


		/// <summary>
		/// Activates the <see cref="Country"/> whose turn is next.
		/// </summary>
		/// <returns>The <see cref="Country"/> that is now active.</returns>
		public Country ActivateNextCountry()
		{
			int idx = 0;
			if(this.activeCountry != null)
			{
				idx = this.countries.IndexOf(this.activeCountry);
				idx++;
			}

			if(idx >= this.countries.Count)
			{
				
				//	Past the last country.  Do end of turn processing.
				this.activeCountry = this.countries[0];
				if(this.activeCountry.Units.Count > 0)				
					this.activeUnit = (Unit)this.activeCountry.Units[0];				
				else				
					this.activeUnit = null;				

				DoTurn();
				OnActiveUnitChanged();

				if(this.activeUnit == null)
				{
					this.activeCountry.NotifyEndOfTurn = true;
				}
			}
			else
			{
				
				//	Get the new player.  Set the active unit to null before
				//	getting the new unit.
				this.activeCountry = this.countries[idx];
				this.activeUnit = null;
				if(ActivateNextUnit() != null)
				{
					this.activeCountry.NotifyEndOfTurn = false;
				}
				else
				{
					this.activeCountry.NotifyEndOfTurn = true;
				}
			}
			return this.activeCountry;
		}

		/// <summary>
		/// Saves the current game to disk
		/// </summary>
		/// <returns></returns>
		public void SaveGame(string fileName)
		{	
			XmlTextWriter writer = new XmlTextWriter(fileName, Encoding.Unicode);
			writer.Formatting = Formatting.Indented;
			writer.Indentation = 4;
			writer.WriteStartDocument();
			writer.WriteStartElement("Game");
			writer.WriteElementString("RulesetPath", this.ruleset.FullPath);
			writer.WriteElementString("Year", this.year.ToString(CultureInfo.InvariantCulture));
			writer.WriteStartElement("Countries");

			//save the individual countries
			foreach(Country country in this.countries)
			{
				country.Save(writer);
			}

			writer.WriteEndElement();
			this.rules.Save(writer);
			this.history.Save(writer);
			this.grid.Save(writer);
			writer.WriteEndElement();
			writer.WriteEndDocument();
			writer.Flush();
			writer.Close();
		}

		/// <summary>
		/// Loads a game from the filename.
		/// </summary>
		/// <param name="fileName">Path to an xml save game file.</param>
		public void LoadGame(string fileName)
		{
			OnServerStarting();
			XmlTextReader reader = new XmlTextReader(fileName);
			reader.WhitespaceHandling = WhitespaceHandling.None;
			string last = "";

			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.Element)
				{
					last = reader.Name;
					switch(last)
					{
						case "Grid":
							this.grid = new Grid();
							this.grid.Load(reader);
							break;
						case "Rules":
							this.rules = new Rules();
							this.rules.Load(reader);
							break;
						case "Countries":
							LoadCountries(reader);
							break;
						case "History":
							this.history = new History();
							this.history.Load(reader);
							break;
					}
				}
				else if (reader.NodeType == XmlNodeType.Text)
				{
					switch(last)
					{
						case "RulesetPath":
							LoadRuleset(reader.Value);
							break;
						case "Year":
							this.year = XmlConvert.ToInt32(reader.Value);
							break;
					}
				}
			}
			reader.Close();
			OnCountriesLoaded();	
			OnServerStarted();
		}

		//Loads all of the countries saved in the game.
		private void LoadCountries(XmlReader reader)
		{
			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "Countries")
					break;
				if(reader.NodeType == XmlNodeType.Element && reader.Name == "Country")
				{
					Country player = CountryFactory.CreateCountry(reader);
					this.countries.Add(player);
				}
			}
		}

		
		/// Adds the villages to the grid.		
		private void AddVillages(WorldSize worldSize)
		{
			int numVillages = 0;
			GridCell nextVillageCell;
			Village nextVillage;

			switch (worldSize)
			{
				case WorldSize.Tiny:
					numVillages = 10;
					break;
				case WorldSize.Small:
					numVillages = 20;
					break;
				case WorldSize.Standard:
					numVillages = 30;
					break;
				case WorldSize.Large:
					numVillages = 40;
					break;
				case WorldSize.Huge:
					numVillages = 50;
					break;
			}

			for(int i = 1; i <= numVillages; i++)
			{
				do 
				{
					nextVillageCell = this.grid.FindRandomDryCell();
				} while(nextVillageCell.Village != null);

				//FIXME: get village names from Xml.
				nextVillage = new Village("Sioux");
				nextVillageCell.Village = nextVillage;		  
			}
		}
	}
}
