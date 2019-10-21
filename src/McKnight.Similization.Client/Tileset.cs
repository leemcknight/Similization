using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Data;
using System.Drawing;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// A Tileset of images used throughout the game on entities, such 
	/// as units, terrains, and resources.
	/// </summary>
	public class Tileset
	{
		private Size tileSize = new Size(32,32);
		private CityTileCollection cityTiles;
		private Dictionary<string, Image> resourceTiles;
		private Dictionary<string, Image> unitTiles;
		private Dictionary<string, TileInfo> terrainTiles;
        private Dictionary<string, Image> cellImprovementTiles;
		private Collection<Image> villageTiles;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Tileset"/> class.
		/// </summary>
		/// <param name="fileName"></param>
		public Tileset(string fileName)
		{
			DataSet ds = new DataSet("Tileset");
            ds.Locale = CultureInfo.InvariantCulture;
			ds.ReadXml(fileName);
			LoadDataSet(ds);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tileset"/> class.
		/// </summary>
		/// <param name="tilesetDataSet"></param>
		public Tileset(DataSet tilesetDataSet)
		{
			if(tilesetDataSet == null)
				throw new ArgumentNullException("tilesetDataSet");

			LoadDataSet(tilesetDataSet);
		}

		/// <summary>
		/// The Size (in pixels) of a tile in the tileset.
		/// </summary>
		public Size TileSize
		{
			get { return this.tileSize; }
		}

		/// <summary>
		/// The tiles used to draw cities.
		/// </summary>
		public CityTileCollection CityTiles
		{
			get { return this.cityTiles; }
		}

		/// <summary>
		/// The tiles used for resources.
		/// </summary>
		public Dictionary<string, Image> ResourceTiles
		{
			get { return this.resourceTiles; }
		}

		/// <summary>
		/// The tiles used for units.
		/// </summary>
		public Dictionary<string, Image> UnitTiles
		{
			get { return this.unitTiles; }
		}

		/// <summary>
		/// The tiles used for terrains.
		/// </summary>
		public Dictionary<string, TileInfo> TerrainTiles
		{
			get { return this.terrainTiles; }
		}

		/// <summary>
		/// The tiles used for villages.
		/// </summary>
		public Collection<Image> VillageTiles
		{
			get { return this.villageTiles; }
		}

		/// <summary>
		/// The tiles used for irrigated cells.
		/// </summary>
		public Dictionary<string, Image> CellImprovementTiles
		{
			get { return this.cellImprovementTiles; }
		}

		private void LoadDataSet(DataSet ds)
		{
			LoadUnitTiles(ds.Tables["UnitTile"]);
			LoadTerrainTiles(ds.Tables["TerrainTile"]);
			LoadResourceTiles(ds.Tables["ResourceTile"]);
			LoadVillageTiles(ds.Tables["VillageTile"]);
			LoadCityTiles(ds.Tables["CityTile"]);
		}

		/// <summary>
		/// Fills the <see cref="CityTiles"/> <c>CityTileCollection</c> with the tile information 
		/// in the <c>System.Data.DataTable</c>
		/// </summary>
		/// <param name="tileTable"></param>
		protected virtual void LoadCityTiles(DataTable tileTable)
		{
			if(tileTable == null)
				throw new ArgumentNullException("tileTable");

			this.cityTiles = new CityTileCollection();
			foreach(DataRow row in tileTable.Rows)			
				cityTiles.Add(new CityTile(row));			
		}

		/// <summary>
		/// Fills the <see cref="UnitTiles"/> collection with the tile information
        /// in the <see cref="System.Data.DataTable"/>.
		/// </summary>
		/// <param name="tileTable"></param>
		protected virtual void LoadUnitTiles(DataTable tileTable)
		{
			this.unitTiles = new Dictionary<string, Image>();
			foreach(DataRow row in tileTable.Rows)
			{
				unitTiles.Add(row["UnitName"].ToString(), Image.FromFile(row["TilePath"].ToString()));
			}
		}

		/// <summary>
		/// Fills the <see cref="Tileset"/> with the tile information
        /// in the <see cref="System.Data.DataTable"/>.
		/// </summary>
		/// <param name="tileTable"></param>
		protected virtual void LoadResourceTiles(DataTable tileTable)
		{
			this.resourceTiles = new Dictionary<string, Image>();
			foreach(DataRow row in tileTable.Rows)
			{
				resourceTiles.Add(row["ResourceName"].ToString(), Image.FromFile(row["TilePath"].ToString()));
			}
		}

		/// <summary>
		/// Fills the <see cref="Tileset"/> with the tile information 
        /// in the <see cref="System.Data.DataTable"/>.
		/// </summary>
		/// <param name="tileTable"></param>
		protected virtual void LoadTerrainTiles(DataTable tileTable)
		{
            this.terrainTiles = new Dictionary<string, TileInfo>();
            TileInfo info;
			foreach(DataRow row in tileTable.Rows)
			{
                info = new TileInfo();
                info.Key = row["TerrainName"].ToString();
                info.TileImagePath = row["TilePath"].ToString();
                this.terrainTiles.Add(info.Key, info);
			}
		}

		/// <summary>
		/// Fills the <see cref="Tileset"/> with tiles used to render villages 
		/// on the screen.
		/// </summary>
		/// <param name="tileTable"></param>
		protected virtual void LoadVillageTiles(DataTable tileTable)
		{
			this.villageTiles = new Collection<Image>();
			foreach(DataRow row in tileTable.Rows)
			{
				villageTiles.Add(Image.FromFile(row["TilePath"].ToString()));
			}
		}

		/// <summary>
		/// Fills the <see cref="Tileset"/> with the tiles used to render 
		/// cell improvements such as irrigation, mines, and roads.s
		/// </summary>
		/// <param name="tileTable"></param>
		protected virtual void LoadCellImprovementTiles(DataTable tileTable)
		{
			this.cellImprovementTiles = new Dictionary<string, Image>();
			foreach(DataRow row in tileTable.Rows)
			{
				cellImprovementTiles.Add(row["TileName"].ToString(), Image.FromFile(row["TilePath"].ToString()));
			}
		}
		
	}
}