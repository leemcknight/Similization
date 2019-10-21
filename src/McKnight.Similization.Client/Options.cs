using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Globalization;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// Represents the options for the game.
	/// </summary>
	public class Options : IDisposable
	{
		private readonly string OPTIONS_SCHEMA = "options.xsd";
		private readonly string OPTIONS_FILE = "options.xml";
        private bool showKilledMessage;
        private bool waitAfterTurn;
        private bool disposed;
        private string rulesetPath;
        private string tilesetPath;
        private Color cityNameFontColor;
        private Font cityNameFont;
		DataSet dataSet;

		/// <summary>
		/// Initializes a new instance of the <see cref="Options"/> class.
		/// </summary>
		public Options()
		{
			this.dataSet = new DataSet("Options");
            this.dataSet.Locale = CultureInfo.InvariantCulture;
			this.dataSet.ReadXmlSchema(OPTIONS_SCHEMA);
			this.dataSet.ReadXml(OPTIONS_FILE);
			SetProperties();	
		}

        /// <summary>
        /// Finalizer for the <see cref="Options"/> class.
        /// </summary>
        ~Options()
        {
            Dispose(false);
        }

		private void SetProperties()
		{
			DataTable table = this.dataSet.Tables[0];
			DataRow row;
			if(table.Rows.Count == 0)
			{
				row = table.NewRow();
				row["ShowKilledMessage"] = true;
				row["StartingRulesetPath"] = string.Empty;
				row["StartingTilesetPath"] = string.Empty;
				row["CityNameFont"] = "Tahoma";
				row["CityNameFontColor"] = "White";
				row["WaitAfterTurn"] = true;
				table.Rows.Add(row);
			}
			else
			{
				row = this.dataSet.Tables[0].Rows[0];
			}

			this.showKilledMessage = Convert.ToBoolean(row["ShowKilledMessage"], CultureInfo.InvariantCulture);
			this.waitAfterTurn = Convert.ToBoolean(row["WaitAfterTurn"], CultureInfo.InvariantCulture);
			this.tilesetPath = Convert.ToString(row["StartingTilesetPath"], CultureInfo.InvariantCulture);
			this.rulesetPath = Convert.ToString(row["StartingRulesetPath"], CultureInfo.InvariantCulture);
			this.cityNameFont = new Font(Convert.ToString(row["CityNameFont"], CultureInfo.InvariantCulture), 8.25F);
			this.cityNameFontColor = Color.FromName(Convert.ToString(row["CityNameFontColor"], CultureInfo.InvariantCulture));
		}

		private void GetProperties()
		{
			DataTable table = this.dataSet.Tables[0];
			DataRow row = table.Rows[0];

			row["ShowKilledMessage"] = this.showKilledMessage;
			row["StartingRulesetPath"] = this.StartingRulesetPath;
			row["StartingTilesetPath"] = this.tilesetPath;
			row["CityNameFont"] = this.cityNameFont.Name;
			row["CityNameFontColor"] = this.cityNameFontColor.Name;
			row["WaitAfterTurn"] = this.waitAfterTurn;            
		}

		
		/// <summary>
		/// Gets or sets a value indicating if the user will see a 
		/// notification when a unit is killed.
		/// </summary>
		public bool ShowKilledMessage
		{
			get { return this.showKilledMessage; }
			set { this.showKilledMessage = value; }
		}

		
		/// <summary>
		/// Gets or sets a value indicating whether the user has 
		/// to manually end their turn before the computer players
		/// take their turn.
		/// </summary>
		public bool WaitAfterTurn
		{
			get { return this.waitAfterTurn; }
			set { this.waitAfterTurn = value; }
		}
		
		/// <summary>
		/// Gets or sets the path of the ruleset to initially default to
		/// when starting new games.
		/// </summary>
		public string StartingRulesetPath
		{
			get { return this.rulesetPath; }
			set { this.rulesetPath = value; }
		}
		
		/// <summary>
		/// The full path to the tileset to use in the game.
		/// </summary>
		public string TilesetPath
		{
			get { return this.tilesetPath; }
			set { this.tilesetPath = value; }
		}
        
		/// <summary>
		/// The <c>System.Drawing.Font</c> to use to display the city information.
		/// </summary>
		public Font CityNameFont
		{
			get { return this.cityNameFont; }
			set { this.cityNameFont = value; }
		}
		
		/// <summary>
		/// The <c>System.Drawing.Color</c> to use when displaying the city information.
		/// </summary>
		public Color CityNameFontColor
		{
			get { return this.cityNameFontColor; }
			set { this.cityNameFontColor = value; }
		}

		/// <summary>
		/// Serializes the options to disk in Xml format.
		/// </summary>
		public void Serialize()
		{
			GetProperties();
			this.dataSet.WriteXml(OPTIONS_FILE);
		}
        
        /// <summary>
        /// Releases all resources used by the <see cref="Options"/> class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.dataSet.Dispose();
                    this.cityNameFont.Dispose();
                }
            }
            this.disposed = true;
        }
        
    }
}
