using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using McKnight.Similization.Core;

namespace McKnight.Similization.Client
{	
	/// <summary>
	/// An image tile used to draw cities on the screen.
	/// </summary>
	/// <remarks></remarks>
	public class CityTile
	{
        private Era era;
        private Civilization civilization;
        private CitySizeClass sizeClass;
        private Image image;

		/// <summary>
		/// Initializes a new instance of the <see cref="CityTile"/> class.
		/// </summary>
		/// <param name="tileRow"></param>
		public CityTile(DataRow tileRow)
		{
			//City tile names contain information regarding the civilization
			//the image belongs to, the size class,  and also the era the tile belongs to.
			//we parse this apart with underscore characters "_".  So, for 
			//example, a city tile for America in industrial times for a metropolis-sized
			//city would look like "america_industrial_metropolis".  Note that these
			//are case-insensitive.

			if(tileRow == null)
				throw new ArgumentNullException("tileRow");

			//here is the full, unsplit version of the tilename.
			string unsplit = Convert.ToString(tileRow["CityTileName"], CultureInfo.InvariantCulture);

			string[] split = unsplit.Split("_".ToCharArray());

            if (split.Length != 3)
            {
                string error = ClientResources.GetExceptionString("error_invalidCityTileName");
                error = string.Format(CultureInfo.CurrentCulture, error, unsplit);
                throw new ArgumentException(error, "tileRow");
            }

			string civ = split[0];
			string eraName = split[1];
			string sizeClassName = split[2];

			Ruleset rs = ClientApplication.Instance.ServerInstance.Ruleset;

			foreach(Civilization c in rs.Civilizations)
			{
				if(string.Compare(c.Name, civ, true, CultureInfo.InvariantCulture) == 0)
				{
					this.civilization = c;
					break;
				}
			}

			if(this.civilization == null)
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, ClientResources.GetExceptionString("error_invalidCivilization"),civ), "tileRow");


			foreach(Era e in rs.Eras)
			{
				if(string.Compare(e.Name, eraName, true, CultureInfo.InvariantCulture) == 0)
				{
					this.era = e;
					break;
				}
			}

			if(this.era == null)
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, ClientResources.GetExceptionString("error_invalidEra"), eraName), "tileRow");

			try
			{
				this.sizeClass = (CitySizeClass)Enum.Parse(typeof(CitySizeClass), sizeClassName, true);
			}
			catch(System.Exception ex)
			{
				throw new ArgumentException(sizeClassName + string.Format(CultureInfo.InvariantCulture, ClientResources.GetExceptionString("error_invalidCitySize"),sizeClassName), "tileRow", ex);
			}

			string path = string.Empty;			
			path = Convert.ToString(tileRow["TilePath"], CultureInfo.InvariantCulture);
			this.image = Image.FromFile(path);			
		}
		
		/// <summary>
		/// The <see cref="Era"/> this tile will be drawn in.
		/// </summary>
		public Era Era
		{
			get { return this.era; }
		}

		/// <summary>
		/// The <see cref="Civilization"/> this tile will drawn for.
		/// </summary>
		public Civilization Civilization
		{
			get { return this.civilization; }
		}
			
		/// <summary>
		/// The <see cref="CitySizeClass"/> the tile will be drawn for.
		/// </summary>
		public CitySizeClass SizeClass
		{
			get { return this.sizeClass; }
		}
		
		/// <summary>
		/// The image for the city.
		/// </summary>
		public Image Image
		{
			get { return image; }
		}
	}
}