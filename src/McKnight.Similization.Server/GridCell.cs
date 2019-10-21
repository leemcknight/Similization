using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Drawing;
using System.Xml;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	
	/// <summary>
	/// Represents a single grid cell on the map.
	/// </summary>
	public class GridCell : GridCellBase
	{            
        private BorderTypes borderType = BorderTypes.None;
        private Village village;
        private City city;        
        private NamedObjectCollection<Unit> units;
        private NamedObjectCollection<CountryBase> exploredCountries;
        private GridCellItemDirection roadLayout;
        private GridCellItemDirection riverLayout;        		
        private event EventHandler<CellImprovementDestroyedEventArgs> cellImprovementDestroyed;
						
		/// <summary>
		/// Initializes a new instance of the <see cref="GridCell"/> class.
		/// </summary>
		public GridCell()
		{
            exploredCountries = new NamedObjectCollection<CountryBase>();
			InitializeUnitCollection();
			this.roadLayout = GridCellItemDirection.None;
			this.riverLayout = GridCellItemDirection.None;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GridCell"/> class.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public GridCell(int x, int y) : this()
		{
			this.Coordinates = new Point(x,y);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GridCell"/> class.
		/// </summary>
		/// <param name="location"></param>
		public GridCell(Point location) : this(location.X, location.Y)
		{
		}
		
		private void InitializeUnitCollection()
		{
            this.units = new NamedObjectCollection<Unit>();
			this.units.CollectionChanged += new CollectionChangeEventHandler(HandleUnitCollectionChange);
		}
		
		// Adds a road to the cell.  This will look at the surrounding
		// cells to see which type of road should be placed (direction).
		private void SetRoad()
		{
			GridCell cell;
			this.HasRoad = true;
            Grid grid = GameRoot.Instance.Grid;
            GridCell topCell = grid.GetCell(new Point(this.Coordinates.X, this.Coordinates.Y - 1));
            GridCell bottomCell = grid.GetCell(new Point(this.Coordinates.X, this.Coordinates.Y + 1));
            GridCell leftCell = grid.GetCell(new Point(this.Coordinates.X - 1, this.Coordinates.Y));
            GridCell rightCell = grid.GetCell(new Point(this.Coordinates.X + 1, this.Coordinates.Y));
            GridCell bottomRightCell = grid.GetCell(new Point(this.Coordinates.X + 1, this.Coordinates.Y + 1));

			if(topCell != null)
			{
				cell = topCell;
				if(cell.HasRoad || cell.City != null)				
					this.roadLayout = GridCellItemDirection.NorthSouth;				
			}

			if(bottomCell != null)
			{
				cell = bottomCell;
				if(cell.HasRoad || cell.City != null)				
					this.roadLayout = GridCellItemDirection.NorthSouth;				
				else if(bottomRightCell != null)
				{					
					if(bottomRightCell.HasRoad || bottomRightCell.City != null)					
						this.roadLayout = GridCellItemDirection.SouthwestToNortheast;					
				}
			}

			if(rightCell != null)
			{
				cell = rightCell;
				if(cell.HasRoad || cell.City != null)
				{
					this.roadLayout = 
						this.roadLayout == GridCellItemDirection.None ? 
						GridCellItemDirection.EastWest : GridCellItemDirection.Bidirectional;
				}
			}

			if(leftCell != null)
			{
				cell = leftCell;
				if(cell.HasRoad || cell.City != null)
				{
					this.roadLayout = 
						this.roadLayout == GridCellItemDirection.None ?
						GridCellItemDirection.EastWest : GridCellItemDirection.Bidirectional;
				}
			}
		}




        /// <summary>
        /// Gets the number of food units located in the square.  
        /// </summary>
        /// <remarks>
        /// This takes into account any cell improvements, such as roads or railroads,
        /// and any "natural" improvements, such as game, or wheat.
        /// </remarks>
        public int FoodUnits
        {
            get
            {
                int food = this.Terrain.Food;
                if ((this.IsIrrigated) || (this.city != null))
                {
                    food += this.Terrain.IrrigationBonus;
                    if (this.HasRailroad)
                        food++;
                }

                return food;
            }
        }

        /// <summary>
        /// Gets shields this cell will produce each turn.  
        /// </summary>
        /// <remarks>
        /// This takes into account any cell improvements (i.e. a mine).
        /// </remarks>
        public int Shields
        {
            get
            {
                int shields = this.Terrain.Shields;
                if ((this.HasMine) || (this.city != null))
                {
                    shields += this.Terrain.MiningBonus;
                    if (this.HasRailroad)                    
                        shields++;                    
                }

                return shields;
            }

        }


		/// <summary>
		/// Gets the amount of gold that is produced by
		/// the cell during each turn.  
        /// </summary>
        /// <remarks>
        /// This takes into account any cell improvements, such as a mine or road.
        /// </remarks>
		public int GoldPerTurn
		{
			get 
			{ 
				int gold = this.Terrain.Commerce;
				if((this.HasRoad) || (this.city != null))				
					gold += this.Terrain.RoadBonus;				
				return gold;
			}			
		}
				
		/// <summary>
		/// Gets or sets the city located in this cell.
		/// </summary>
		public City City
		{
			get { return this.city; }
			set { this.city = value; }
		}
        		

		
		
		/// <summary>
		/// Gets the layout of the road on the cell.
		/// </summary>
		public GridCellItemDirection RoadLayout
		{
			get { return this.roadLayout; }
		}
		
		/// <summary>
		/// Gets the layour of the river on the cell.
		/// </summary>
		public GridCellItemDirection RiverLayout
		{
			get { return this.riverLayout; }
		}
		

		
		/// <summary>
		/// Gets the border type of the cell.
		/// </summary>
		public BorderTypes BorderType
		{
			get { return this.borderType; }
		}
				

		
		/// <summary>
		/// Gets or sets the a <see cref="Village"/> object
		/// that is located in this cell.
		/// </summary>
		public Village Village
		{
			get { return this.village; }
			set { this.village = value; }
		}
		
				
		/// <summary>
		/// Gets a list of units currently stationed at this cell.
		/// </summary>
        public NamedObjectCollection<Unit> Units
		{
			get	{ return this.units; }
		}

        /// <summary>
        /// Determines whether there is a <see cref="City"/> on this <see cref="GridCell"/>.
        /// </summary>
        public bool HasCity
        {
            get { return this.city != null; }
        }
				
		/// <summary>
		/// Occurs when an attacking unit destroys an improvement on the cell, such
		/// as irrigation or roads.
		/// </summary>
		public event EventHandler<CellImprovementDestroyedEventArgs> CellImprovementDestroyed
		{
			add
			{
				this.cellImprovementDestroyed += value;
			}

			remove
			{
				this.cellImprovementDestroyed -= value;
			}
		}

		/// <summary>
		/// Raises the <i>CellImprovementDestroyed</i> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnCellImprovementDestroyed(CellImprovementDestroyedEventArgs e)
		{
			if(this.cellImprovementDestroyed != null)
			{
				this.cellImprovementDestroyed(this, e);
			}
		}

		/// <summary>
		/// Queries the cell to see if it has been explored by the country passed as a parameter.
		/// </summary>
		/// <param name="explorer"></param>
		/// <returns><c>true</c> if the cell has been explored by <c>Country</c>, <c>false</c>
		/// otherwise.</returns>
		public bool HasBeenExploredBy(Country explorer)
		{
			if(explorer == null)
				throw new ArgumentNullException("explorer");

			return this.exploredCountries.IndexOf(explorer) >= 0; 
		}

		/// <summary>
		/// Queries the cell to see if it has access to a fresh water supply.
		/// </summary>
		/// <returns></returns>
		private bool HasAccessToFreshWater()
		{
			//TODO: implement
			return true;
		}

		/// <summary>
		/// Explores the cell for the colony.
		/// </summary>
		/// <param name="explorer"></param>
		public void Explore(CountryBase explorer)
		{
			if(explorer == null)
				throw new ArgumentNullException("explorer");
			bool exploredBy = false;
			foreach(Country expColony in this.exploredCountries)
			{
				if(explorer == expColony)
				{
					exploredBy = true;
					break;
				}

			}
			if(!exploredBy)			
				this.exploredCountries.Add(explorer);							
		}

		/// <summary>
		/// Gets the best suited work to be performed on the cell.  
		/// </summary>
		/// <returns></returns>
		public WorkerActionInfo FindWorkerAction()
		{
			WorkerActionInfo info = new WorkerActionInfo(WorkerAction.None, this);
			if(this.IsPolluted)
			{
				info.WorkerAction = WorkerAction.CleanPollution;
				info.Priority = 10;
			}
			else if(!this.HasRoad && this.IsDry)
			{
				info.WorkerAction = WorkerAction.BuildRoad;
				info.Priority = 8;
			}
			else if(!this.IsIrrigated && this.IsDry)
			{
				info.WorkerAction = WorkerAction.Irrigate;
				info.Priority = 6;
			}

			return info;
		}

		/// <summary>
		/// Gets a value indicating if a resource is reachable from this cell
		/// by road or railroad.
		/// </summary>
		/// <param name="resource">The resource to check for.</param>
		/// <param name="source">The starting point.</param>
		/// <returns><c>true</c> if the resource is accessible by road, <c>false</c> otherwise.</returns>
		public bool CanGetTo(Resource resource, GridCell source)
		{
			if(resource == null)
				throw new ArgumentNullException("resource");
			if(source == null)
				throw new ArgumentNullException("source");

			if(this.Resource == resource)			
				return true;

            throw new NotImplementedException();
		}


		/// <summary>
		/// Gets the closest unit.
		/// </summary>
		/// <returns></returns>
		public Unit FindClosestUnit()
		{
			return FindClosestUnit(null);
		}

		/// <summary>
		/// Gets the closest unit belonging the the parent country on the grid.
		/// </summary>
		/// <param name="parentCountry"></param>
		/// <returns></returns>
		public Unit FindClosestUnit(Country parentCountry)
		{
			//TODO: implement
			return null;
		}

		/// <summary>
		/// Returns the closest cell of the foreign country that 
		/// can be pillaged (has improvements).
		/// </summary>
		/// <param name="parentCountry">The country doing the pillaging</param>
		/// <param name="foreignCountry">The country being pillaged</param>
		/// <returns>Coordinates to pillage</returns>
		public Point FindClosestPillagableCoordinates(Country parentCountry, Country foreignCountry)
		{
			if(parentCountry == null)
				throw new ArgumentNullException("parentCountry");
			if(foreignCountry == null)
				throw new ArgumentNullException("foreignCountry");

			//the city that will end up having its' land
			//pillaged.
			City pillagedCity;		
			Point closestCell;
			
			pillagedCity = FindClosestForeignCity(parentCountry, foreignCountry);
			closestCell = FindClosestCoordinateFromList(pillagedCity.UsedCells);

			return closestCell;
		}


		// Given a list of grid cells and an origin, returns the grid cell from the list
		// that is closes to the origin.
		private Point FindClosestCoordinateFromList(IList<Point> coordinateList)
		{			
			Point closest = Point.Empty;
			int closestDistance = 0;
			
			foreach(Point coord in coordinateList)
			{
				int dx = Math.Abs(coord.X - this.Coordinates.X);
				int dy = Math.Abs(coord.Y - this.Coordinates.Y);
				int distance = (int)Math.Sqrt((dx * dx) + (dy * dy));
				if((closest == Point.Empty) ||(distance < closestDistance))
				{
					closestDistance = distance;
					closest = coord;
				}
			}
			return closest;
		}

		/// <summary>
		/// Returns the closest explored foreign city of that has been explored
		/// by the parent country.
		/// </summary>
		/// <param name="parentCountry">The parent country.  Foreign cities will only be 
		/// returned if they have been explored by the parent country</param>
		/// <param name="foreignCountry">The country to search for the forieign cities</param>
		/// <returns></returns>
		public City FindClosestForeignCity(Country parentCountry, Country foreignCountry)
		{
			if(parentCountry == null)
				throw new ArgumentNullException("parentCountry");
			if(foreignCountry == null)
				throw new ArgumentNullException("foreignCountry");

            NamedObjectCollection<City> discoveredCities = new NamedObjectCollection<City>();
			City closest = null;
			int closestDistance = 0;
			int deltaX, deltaY;
			int distance;
            GameRoot root = GameRoot.Instance;

			//what cities do they know about?
			foreach(City city in foreignCountry.Cities)
			{
                GridCell cityCell = root.Grid.GetCell(city.Coordinates);
				if(cityCell.HasBeenExploredBy(parentCountry))				
				    discoveredCities.Add(city);				
			}

			//of the "known" cities, which one is closest?
			foreach(City city in discoveredCities)
			{
				deltaX = Math.Abs(city.Coordinates.X - this.Coordinates.X);
				deltaY = Math.Abs(city.Coordinates.Y - this.Coordinates.Y);
				distance = (int)Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));

				if((closest == null) ||(distance < closestDistance))
				{
					closestDistance = distance;
					closest = city;
				}
			}

			return closest;
		}

		/// <summary>
		/// Gets the closest <see cref="City"/> belonging to the <see cref="Country"/> that is passed to 
		/// the method.
		/// </summary>
		/// <param name="parentCountry">The parent country of the <see cref="City"/> to find.</param>
		/// <returns>A <see cref="City"/> object representing the city that is closest.</returns>
		/// <remarks>This method will ignore any <see cref="City"/> objects within the local 
		/// <see cref="GridCell"/>.</remarks>
		public City FindClosestDomesticCity(Country parentCountry)
		{
			if(parentCountry == null)
				throw new ArgumentNullException("parentCountry");

			int deltaX, deltaY;
			City closest = null;
			int closestDistance = 0;
			int distance;

			foreach(City city in parentCountry.Cities)
			{
				deltaX = Math.Abs(city.Coordinates.X - this.Coordinates.X);
				deltaY = Math.Abs(city.Coordinates.Y - this.Coordinates.Y);
				distance = (int)Math.Sqrt( (deltaX * deltaX) + (deltaY * deltaY) );

				if((closest == null) ||(distance < closestDistance))
				{
					closestDistance = distance;
					closest = city;
				}
			}

			return closest;
		}

		/// <summary>
		/// Gets the closest <see cref="City"/> belonging to the <see cref="Country"/> that is passed to 
		/// the method.  The <see cref="City"/> cannot be in the array of excluded cities.
		/// </summary>
		/// <param name="parentCountry"></param>
		/// <param name="exclusions"></param>
		/// <returns></returns>
        public City FindClosestDomesticCity(Country parentCountry, NamedObjectCollection<City> exclusions)
		{
			if(parentCountry == null)
				throw new ArgumentNullException("parentCountry");

			int deltaX, deltaY;
			City closest = null;
			int closestDistance = 0;
			bool exclude = false;
			int distance;

			foreach(City city in parentCountry.Cities)
			{
				exclude = false;
				foreach(City exclusion in exclusions)
				{
					if(exclusion == city)
					{
						exclude = true;
						break;
					}
				}
				
				if(exclude)
				{
					continue;
				}

				deltaX = Math.Abs(city.Coordinates.X - this.Coordinates.X);
				deltaY = Math.Abs(city.Coordinates.Y - this.Coordinates.Y);
				distance = (int)Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));

				if((closest == null) ||(distance < closestDistance))
				{
					closestDistance = distance;
					closest = city;
				}
			}

			return closest;
		}

		/// <summary>
		/// Gets the closest "dry" cell that is unoccupied.
		/// </summary>
		/// <returns></returns>
		public GridCell FindClosestEmptyLandCell()
		{
            throw new NotImplementedException();
		}		

		private void HandleUnitCollectionChange(object sender, CollectionChangeEventArgs e)
		{
			if(e.Action != CollectionChangeAction.Add)
			{
				return;
			}

			Unit unit = (Unit)e.Element;
			bool invasion = false;
			
			if(this.Owner != null)
			{
				if(unit.ParentCountry != this.Owner)
				{
                    Country owner = (Country)this.Owner;
					DiplomaticTie tie = owner.GetDiplomaticTie(unit.ParentCountry);
					if(tie != null)
					{
						invasion = true;
						foreach(DiplomaticAgreement agreement in tie.DiplomaticAgreements)
						{
							if(agreement.GetType() == typeof(RightOfPassage))
							{
								invasion = false;
								break;
							}
						}
					}
					else
					{
						invasion = true;
					}
				}
			}

			if(invasion)
			{
				//TODO: should we notify non-ai players at all?
				if(this.Owner.GetType() == typeof(AICountry))
				{
					((AICountry)this.Owner).NotifyOfInvasion(unit, this);
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether or not there is a city within the radius
		/// of the value passed in.
		/// </summary>
		/// <param name="radius"></param>
		/// <returns></returns>
		public bool HasCityInRadius(int radius)
		{
            throw new NotImplementedException();
		}

		/// <summary>
		/// Gets a value indicating if the grid cell has a border
		/// with the specified border type.
		/// </summary>
		/// <param name="borderType"></param>
		/// <returns></returns>
		public bool HasBorderOfType(BorderTypes borderType)
		{
			BorderTypes composite;
			if(borderType != BorderTypes.None)
			{
				composite = this.borderType & borderType;
				return composite != BorderTypes.None;
			}
			else
			{
				return this.borderType == BorderTypes.None;
			}
		}
		
		/// <summary>
		/// Refreshes the border information.
		/// </summary>
		public void RefreshBorders()
		{
			this.IsBorder = false;
			this.borderType = BorderTypes.None;

            GameRoot root = GameRoot.Instance;
            GridCell cell = root.Grid.GetCell(new Point(this.Coordinates.X, this.Coordinates.Y - 1));			
			if(cell != null && cell.Owner != this.Owner)
			{
				if(!cell.HasBorderOfType(BorderTypes.Bottom))
				{
					this.IsBorder = true;
					this.borderType |= BorderTypes.Top;
				}
			}

            cell = root.Grid.GetCell(new Point(this.Coordinates.X-1, this.Coordinates.Y));			

			if(cell != null && cell.Owner != this.Owner)
			{
				if(!cell.HasBorderOfType(BorderTypes.Right))
				{
					this.IsBorder = true;
					this.borderType |= BorderTypes.Left;
				}
			}

            cell = root.Grid.GetCell(new Point(this.Coordinates.X+1, this.Coordinates.Y));			

			if(cell != null && cell.Owner != this.Owner)
			{
				if(!cell.HasBorderOfType(BorderTypes.Left))
				{
					this.IsBorder = true;
					this.borderType |= BorderTypes.Right;
				}
			}

            cell = root.Grid.GetCell(new Point(this.Coordinates.X, this.Coordinates.Y + 1));			

			if(cell != null && cell.Owner != this.Owner)
			{
				if(!cell.HasBorderOfType(BorderTypes.Top))
				{
					this.IsBorder = true;
					this.borderType |= BorderTypes.Bottom;
				}
			}

		}

		/// <summary>
		/// Saves the GridCell information.
		/// </summary>
		/// <param name="writer"></param>
		public void Save(XmlWriter writer)
		{
			if(writer == null)
				throw new ArgumentNullException("writer");

			writer.WriteStartElement("GridCell");
			writer.WriteStartElement("Coordinates");
			writer.WriteElementString("X", this.Coordinates.X.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Y", this.Coordinates.Y.ToString(CultureInfo.InvariantCulture));
			writer.WriteEndElement();
			writer.WriteElementString("Terrain", this.Terrain.Name);
			writer.WriteElementString("Road", this.HasRoad.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Railroad", this.HasRailroad.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Irrigated", this.IsIrrigated.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Polluted", this.IsPolluted.ToString(CultureInfo.InvariantCulture));
			if(this.Resource != null)
				writer.WriteElementString("Resource", this.Resource.Name);
			if(this.village != null)
				this.village.Save(writer);
			writer.WriteStartElement("Explorers");
			foreach(Country country in this.exploredCountries)
			{
				writer.WriteElementString("Explorer", country.Name);
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		private string[] explorerNames;

		/// <summary>
		/// Loads the list of countries that have explored this cell.
		/// </summary>
		/// <param name="reader"></param>
		private void LoadExplorers(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			GameRoot root = GameRoot.Instance;
			
			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "Explorers")
					break;
			}
		}

		private void CountriesLoaded(object sender, System.EventArgs e)
		{
			Country country;
			GameRoot root = GameRoot.Instance;
			foreach(string name in this.explorerNames)
			{
				country = root.Countries[name];
				this.exploredCountries.Add(country);
			}

			this.explorerNames = null;
		}

		/// <summary>
		/// Loads the grid cell information into the grid cell object.
		/// </summary>
		/// <param name="reader"></param>
		public void Load(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			GameRoot root = GameRoot.Instance;
			string last = "";

			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "GridCell")
					break;
				if(reader.NodeType == XmlNodeType.Element)
				{
					last = reader.Name;
					switch(last)
					{
						case "Coordinates":
							LoadCoordinates(reader);
							break;
						case "Explorers":
							if(!reader.IsEmptyElement)
								LoadExplorers(reader);
							break;
					}
				}
				else if (reader.NodeType == XmlNodeType.Text)
				{
					switch(last)
					{
						case "Terrain":
							this.Terrain = root.Ruleset.Terrains[reader.Value];
							break;
						case "Road":
							this.HasRoad = Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture);
							break;
						case "Railroad":
							this.HasRailroad = Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture);
							break;
						case "Irrigated":
							this.IsIrrigated = Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture);
							break;
						case "Polluted":
							this.IsPolluted = Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture);
							break;
						case "Resource":
							this.Resource = root.Ruleset.Resources[reader.Value];
							break;
					}
				}
			}
		}

		private void LoadCoordinates(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			int x = 0;
			int y = 0;
			string last = "";
		
			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "Coordinates")
					break;
				if(reader.NodeType == XmlNodeType.Element)
					last = reader.Name;
				else if(reader.NodeType == XmlNodeType.Text)
				{
					switch(last)
					{
						case "X":
							x = XmlConvert.ToInt32(reader.Value);
							break;
						case "Y":
							y = XmlConvert.ToInt32(reader.Value);
							break;
					}
				}
			}
			this.Coordinates = new Point(x,y);
		}
	}
}
