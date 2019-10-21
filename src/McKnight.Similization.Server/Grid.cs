using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml;
using System.IO;
using System.Reflection;

namespace McKnight.Similization.Server
{

	/// <summary>
	/// Represents a Grid for the game.
	/// </summary>
	public class Grid
	{		
        private Size size;
        private GridCell[,] gridCells;
        private Collection<GridCell> dryCells;

        internal Grid()
        {
            this.dryCells = new Collection<GridCell>();
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="Grid"/> class.
		/// </summary>
		/// <param name="gridCells"></param>
		internal Grid(GridCell[,] gridCells) : this()
		{
            this.gridCells = gridCells;            
			this.size = new Size(gridCells.GetLength(0), gridCells.GetLength(1));
			

            for(int j = 0; j < this.size.Width; j++)
                for(int i = 0; i < this.size.Height; i++)
                    gridCells[j,i].CellImprovementDestroyed += new EventHandler<CellImprovementDestroyedEventArgs>(HandleCellImprovementDestroyed);          
		}

        /// <summary>
        /// Gets the <see cref="GridCell"/> with the specifed coordinates.
        /// </summary>
        public GridCell GetCell(Point coordinates)
		{                        
    		return this.gridCells[coordinates.X,coordinates.Y];			
		}
		
		/// <summary>
		/// The Size of the Grid.
		/// </summary>
		public Size Size
		{
			get { return this.size; }
		}

        /// <summary>
        /// Returns the distance, in cell units to the cell passed into the method.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static int CalculateDistanceBetweenCoordinates(Point source, Point destination)
        {                        
            int xOffset = source.X - destination.X;
            int yOffset = source.Y - destination.Y;
            int dist = (int)Math.Sqrt((xOffset * xOffset) + (yOffset * yOffset));
            return dist;
        }
				
		private void HandleCellImprovementDestroyed(object sender, CellImprovementDestroyedEventArgs e)
		{
			if(this.cellImprovementDestroyed != null)
				this.cellImprovementDestroyed(this, e);
		}
		
		private event EventHandler<CellImprovementDestroyedEventArgs> cellImprovementDestroyed;

        /// <summary>
        /// Occurs when a <see cref="GridCell"/> on the grid has an improvement destroyed.
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
		/// Gets a number between 1 and 100 for the city desirability.
		/// </summary>
		/// <param name="possibleCityLocation"></param>
		/// <returns></returns>
		public double GetCityLocationDesirability(GridCell possibleCityLocation)
		{
            throw new NotImplementedException();
		}

		/// <summary>
		/// Explores the cell for the colony.
		/// </summary>
		/// <param name="coordinates"></param>
		/// <param name="explorer"></param>
		public void ExploreArea(Point coordinates, Country explorer)
		{
			if(explorer == null)
				throw new ArgumentNullException("explorer");

            GridCell cell = GetCell(coordinates);
            int visibility = cell.Terrain.Visibility;			
			int rows = visibility * 2;            
			if (visibility % 2 > 0)
				rows++;

            Point start = new Point(coordinates.X - visibility, coordinates.Y - visibility);			
			for(int x = 0; x < rows; x++)
			{								
				for(int y = 0; y < rows; y++)
				{
                    cell = GetCell(new Point(start.X + x, start.Y + y));
					cell.Explore(explorer);
				}				
			}
		}

		/// <summary>
		/// Gets a random <see cref="GridCell"/> object in the current map.
		/// </summary>
		/// <returns>a random grid cell</returns>
		public GridCell FindRandomCell()
		{
			int x = RandomNumber.UpTo(Size.Width - 1);
			int y = RandomNumber.UpTo(Size.Height - 1);
			return this.gridCells[x,y];
		}

		/// <summary>
		///	Gets a random land-based cell on the map.
		/// </summary>
		/// <returns></returns>
		public GridCell FindRandomDryCell()
		{
            int idx = RandomNumber.UpTo(this.dryCells.Count - 1);
			return this.dryCells[idx];
		}
		
		/// <summary>
		/// Gets a list of GridCells that have "dry" terrain types (i.e. not sea/lake/ocean)
		/// </summary>
		public Collection<GridCell> DryCells
		{
			get { return this.dryCells; }
		}

		/// <summary>
		/// Gets the number of cells on the map under the control of the player.
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		public int GetTerritoryAmount(Country player)
		{
			if(player == null)
				throw new ArgumentNullException("player");

			GridCell cell = this.gridCells[0,0];
			int territory = 0;

			for(int x = 0; x < this.size.Width; x++)
			{
				for(int y = 0; y < this.size.Height; y++)
				{
					cell = this.gridCells[x,y];
					if(cell.Owner == player)
					{
						territory++;
					}
				}
			}

			return territory;
		}

		/// <summary>
		/// Saves the Grid information.
		/// </summary>
		/// <param name="writer">The <c>XmlWriter</c> to writer the grid information to.</param>
		public void Save(XmlWriter writer)
		{
			if(writer == null)
				throw new ArgumentNullException("writer");

			GridCell cell;
			writer.WriteStartElement("Grid");
			writer.WriteStartElement("Size");
			writer.WriteElementString("Width", this.size.Width.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Height", this.size.Height.ToString(CultureInfo.InvariantCulture));
			writer.WriteEndElement();
			writer.WriteStartElement("GridCells");
			for(int i = 0; i < this.size.Width; i++)
			{
				for(int y = 0; y < this.size.Height; y++)
				{
					cell = this.gridCells[i,y];
					cell.Save(writer);
				}
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		/// <summary>
		/// Loads the Grid information.
		/// </summary>
		/// <param name="reader"></param>
		public void Load(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "Grid")
					break;

				if(reader.NodeType == XmlNodeType.Element)
				{
					switch(reader.Name)
					{
						case "Size":
							LoadSize(reader);
							break;
						case "GridCells":
							LoadCells(reader);
							break;
					}
				}
			}
		}

		private void LoadSize(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			int width = 0;
			int height = 0;
			string last = "";
			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "Size")
					break;
				if(reader.NodeType == XmlNodeType.Element)
					last = reader.Name;
				else if(reader.NodeType == XmlNodeType.Text)
				{
					switch(last)
					{
						case "Width":
							width = XmlConvert.ToInt32(reader.Value);
							break;
						case "Height":
							height = XmlConvert.ToInt32(reader.Value);
							break;
					}	
				}
				
			}
			if(width == 0 || height == 0)
				throw new InvalidOperationException(ServerResources.InvalidMapSize);
			this.size = new Size(width, height);
			this.gridCells = new GridCell[width, height];
		}

		//Loads each grid cell from the xml reader.  The "Size" xml element has 
		//to come before the "Cells" list for this to work.  We make an explicit
		//check to alert cases of bad xml.
		private void LoadCells(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");
			if(this.gridCells == null)
				throw new InvalidOperationException(ServerResources.InvalidSizeNode);
			GridCell cell;
			int x,y;
			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "GridCells")
					break;
				if(reader.NodeType == XmlNodeType.Element && reader.Name == "GridCell")
				{
					//instantiate the cell
					cell = new GridCell();
					//have the cell read in the saved info
					cell.Load(reader);
					//add it to the cell array.  the cell has its' coordinates
					//from the xml stream.
					x = cell.Coordinates.X;
					y = cell.Coordinates.Y;
					this.gridCells[x,y] = cell;
				}
			}
		}
	}
}
