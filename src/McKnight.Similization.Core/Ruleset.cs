using System;
using System.Collections;
using System.Data;
using System.Globalization;

namespace McKnight.Similization.Core
{
	/// <summary>
	/// Class represeting a Similization Ruleset.
	/// </summary>
	public class Ruleset
	{
        private string fullPath;                        
        private NamedObjectCollection<Resource> resources;
        private NamedObjectCollection<Improvement> improvements;
        private NamedObjectCollection<Civilization> civilizations;
        private NamedObjectCollection<Government> governments;
        private NamedObjectCollection<UnitBase> units;
        private NamedObjectCollection<Terrain> terrains;
        private NamedObjectCollection<Era> eras;
        private NamedObjectCollection<Technology> technologies;        
        private int townBinSize = 20;
        private int cityBinSize = 50;
        private int metropolisBinSize = 100;
        private int foodPerCitizen = 2;
        private int roadMovementBonus;

		/// <summary>
		/// Initializes a new instance of the <see cref="Ruleset"/> class.
		/// </summary>
		/// <param name="rulesetDataSet"></param>
        public Ruleset(DataSet rulesetDataSet)
		{
			if(rulesetDataSet == null)
				throw new ArgumentNullException("rulesetDataSet");

            LoadDefaultRules(rulesetDataSet.Tables["DefaultRules"]);
			LoadUnits(rulesetDataSet.Tables["Unit"]);
			LoadResources(rulesetDataSet.Tables["Resource"]);
			LoadCivilizations(rulesetDataSet.Tables["Civilization"]);
			LoadImprovements(rulesetDataSet.Tables["Improvement"]);
			LoadGovernments(rulesetDataSet.Tables["Government"]);
			LoadTerrains(rulesetDataSet.Tables["Terrain"]);
			LoadEras(rulesetDataSet.Tables["Era"]);
			LoadTechnologies(rulesetDataSet.Tables["Technology"]);
			AddStartingTechnologies(rulesetDataSet.Tables["CivilizationStartingTechnology"]);
			AddUnitTechnologies(rulesetDataSet);
			AddCityNames(rulesetDataSet.Tables["CityName"]);
		}		
		
		/// <summary>
		/// The fully qualified path to the Ruleset file.
		/// </summary>
		public string FullPath
		{
			get { return this.fullPath; }
			set { this.fullPath = value; }
		}

        /// <summary>
        /// The effect that a road will have on a units ability to move through terrain.
        /// </summary>
        public int RoadMovementBonus
        {
            get { return this.roadMovementBonus; }
        }

        /// <summary>
        /// The size of the food bins for towns.
        /// </summary>
        public int TownBinSize
        {
            get { return this.townBinSize; }
        }

        /// <summary>
        /// The size of the food bins for cities.
        /// </summary>
        public int CityBinSize
        {
            get { return this.cityBinSize; }
        }

        /// <summary>
        /// The size of the food bins for a metropolis.
        /// </summary>
        public int MetropolisBinSize
        {
            get { return this.metropolisBinSize; }
        }

        /// <summary>
        /// The amount of food each citizen will eat during a turn.
        /// </summary>
        public int FoodPerCitizen
        {
            get { return this.foodPerCitizen; }
        }
		
		/// <summary>
		/// The civilizations in the ruleset.
		/// </summary>
		public NamedObjectCollection<Civilization> Civilizations
		{
			get { return this.civilizations; }
		}
		
		/// <summary>
		/// The eras in the ruleset.
		/// </summary>
		public NamedObjectCollection<Era> Eras
		{
			get { return this.eras; }
		}
		
		/// <summary>
		/// The governments in the ruleset.
		/// </summary>
		public NamedObjectCollection<Government> Governments
		{
			get { return this.governments; }
		}
		
		/// <summary>
		/// The improvements in the ruleset.
		/// </summary>
        public NamedObjectCollection<Improvement> Improvements
		{
			get { return this.improvements; }
		}
		
		/// <summary>
		/// The resources in the ruleset.
		/// </summary>
        public NamedObjectCollection<Resource> Resources
		{
			get { return this.resources; }
		}
		
		/// <summary>
		/// The technologies in the ruleset.
		/// </summary>
		public NamedObjectCollection<Technology> Technologies
		{
			get { return this.technologies; }
		}
		
		/// <summary>
		/// The terrains in the ruleset.
		/// </summary>
		public NamedObjectCollection<Terrain> Terrains
		{
			get { return this.terrains; }
		}
		
		/// <summary>
		/// The Units in the ruleset.
		/// </summary>
		public NamedObjectCollection<UnitBase> Units
		{
			get { return this.units; }
		}

        private void LoadDefaultRules(DataTable ruleTable)
        {
            DataRow row = ruleTable.Rows[0];
            this.foodPerCitizen =  Convert.ToInt32(row["FoodPerCitizen"], CultureInfo.InvariantCulture);
            this.townBinSize = Convert.ToInt32(row["TownBinSize"], CultureInfo.InvariantCulture);
            this.cityBinSize = Convert.ToInt32(row["CityBinSize"], CultureInfo.InvariantCulture);
            this.metropolisBinSize = Convert.ToInt32(row["MetropolisBinSize"], CultureInfo.InvariantCulture);
            this.roadMovementBonus = Convert.ToInt32(row["RoadMovementBonus"], CultureInfo.InvariantCulture);
        }

		private void LoadUnits(DataTable unitsTable)
		{
            this.units = new NamedObjectCollection<UnitBase>();

			UnitBase unit;
			foreach(DataRow row in unitsTable.Rows)
			{
				unit = new UnitBase(row);
				this.units.Add(unit);
			}
		}

		private void LoadResources(DataTable resourcesTable)
		{
			this.resources = new NamedObjectCollection<Resource>();

			Resource resource;
			foreach(DataRow row in resourcesTable.Rows)
			{
				resource = new Resource(row);
				this.resources.Add(resource);
			}
		}

		private void LoadTerrains(DataTable terrainTable)
		{
			this.terrains = new NamedObjectCollection<Terrain>();

			Terrain terrain;
			foreach(DataRow row in terrainTable.Rows)
			{
				terrain = new Terrain(row);
				this.terrains.Add(terrain);
			}
		}

		private void LoadCivilizations(DataTable civilizationTable)
		{
            this.civilizations = new NamedObjectCollection<Civilization>();

			Civilization civilization;
			foreach(DataRow row in civilizationTable.Rows)
			{
				civilization = new Civilization(row);
				this.civilizations.Add(civilization);
			}
		}

		private void LoadTechnologies(DataTable technologyTable)
		{
			this.technologies = new NamedObjectCollection<Technology>();

			Technology technology;
			foreach(DataRow row in technologyTable.Rows)
			{
				technology = new Technology(row);
				technology.Era = GetEra(technology.EraId);
				this.technologies.Add(technology);
			}
		}

		private void LoadImprovements(DataTable improvementTable)
		{
            this.improvements = new NamedObjectCollection<Improvement>();

			Improvement improvement;
			foreach(DataRow row in improvementTable.Rows)
			{
				improvement = new Improvement(row);
				this.improvements.Add(improvement);
			}
		}

		private void LoadGovernments(DataTable governmentTable)
		{
            this.governments = new NamedObjectCollection<Government>();

			Government government;
			foreach(DataRow row in governmentTable.Rows)
			{
				government = new Government(row);
				this.governments.Add(government);
			}
		}

		private void LoadEras(DataTable eraTable)
		{
			this.eras = new NamedObjectCollection<Era>();

			Era era;
			foreach(DataRow row in eraTable.Rows)
			{
				era = new Era(row);
				this.eras.Add(era);
			}
		}

		private void AddStartingTechnologies(DataTable startingTechnologyTable)
		{
			int civilizationID;
			int technologyID;
			Civilization civilization;
			Technology technology;
			foreach(DataRow row in startingTechnologyTable.Rows)
			{
				civilizationID = Convert.ToInt32(row["CivilizationID"], CultureInfo.InvariantCulture);
				technologyID = Convert.ToInt32(row["TechnologyID"], CultureInfo.InvariantCulture);
				civilization = GetCivilization(civilizationID);
				technology = GetTechnology(technologyID);
				civilization.StartingTechnologies.Add(technology);
			}
		}

		private void AddCityNames(DataTable nameTable)
		{			
			foreach(Civilization civ in this.civilizations)
			{			
				DataRow[] rows = nameTable.Select("CivilizationID=" + civ.CivilizationId.ToString(CultureInfo.InvariantCulture));				
				foreach(DataRow row in rows)				
                    civ.CityNames.Add((string)row["Name"]);													
			}
		}

		private Civilization GetCivilization(int civilizationID)
		{
			foreach(Civilization civilization in this.civilizations)
			{
				if(civilization.CivilizationId == civilizationID)
				{
					return civilization;
				}
			}
			return null;
		}

		private Technology GetTechnology(int technologyID)
		{
			foreach(Technology technology in this.technologies)
			{
				if(technology.TechnologyId == technologyID)
				{
					return technology;
				}
			}

			return null;
		}

		private Era GetEra(int eraId)
		{
			foreach(Era era in this.eras)
			{
				if(era.EraId == eraId)				
					return era;				
			}

			return null;
		}

		private void AddUnitTechnologies(DataSet ds)
		{
			DataRow row;
			DataTable units = ds.Tables["Unit"];
			int technologyID;
			string filter;
			foreach(UnitBase unit in this.units)
			{
				filter = "UnitID=" + unit.UnitId.ToString(CultureInfo.InvariantCulture);
				row = (DataRow)units.Select(filter).GetValue(0);
				if(row["TechnologyID"] != DBNull.Value)
				{
					technologyID = Convert.ToInt32(row["TechnologyID"], CultureInfo.InvariantCulture);
					unit.PrerequisiteTechnology = GetTechnology(technologyID);
				}
			}
		}
	}
}