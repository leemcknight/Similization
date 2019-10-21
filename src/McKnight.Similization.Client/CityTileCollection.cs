using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{    	
	/// <summary>
	/// A strongly-typed collection of <see cref="CityTile"/> objects.
	/// </summary>
	public class CityTileCollection : Collection<CityTile>
	{
		private Dictionary<CityBase, CityTile> cityHash;

		/// <summary>
		/// Initializes a new instance of the <see cref="CityTileCollection"/> class.
		/// </summary>
		public CityTileCollection()
		{
            cityHash = new Dictionary<CityBase, CityTile>();
		}


		/// <summary>
		/// Gets the <see cref="CityTile"/> for the specified Era, Civilization, and City size.
		/// </summary>
        /// <param name="era"></param>
        /// <param name="civilization"></param>
        /// <param name="sizeClass"></param>
		public CityTile GetTile(Era era, Civilization civilization, CitySizeClass sizeClass)
		{			
			foreach(CityTile tile in this.Items)
			{
				if(tile.Civilization == civilization 
					&& tile.Era == era 
					&& tile.SizeClass == sizeClass)
				{
					return tile;
				}
			}

			return null;			
		}

		/// <summary>
		/// Refreshes the tiles for the civilization.
		/// </summary>
		/// <param name="country">The <see cref="Country"/> to refresh the tiles for.</param>
		/// <remarks>This method is useful when all the tiles in a Civilization need to change, 
		/// for instance when the era changes.</remarks>
		public void Refresh(Country country)
		{
			if(country == null)
				throw new ArgumentNullException("country");

			foreach(CityBase city in country.Cities)
			{
				Refresh(city);
			}
			
		}

		/// <summary>
		/// Refreshes the tile for the city.
		/// </summary>
		/// <param name="city"></param>
		public void Refresh(CityBase city)
		{
			if(city == null)
				throw new ArgumentNullException("city");

			this.cityHash.Remove(city);
			CityTile tile = GetCityTile(city);
			this.cityHash.Add(city, tile);
		}


		/// <summary>
		/// Gets the image to use for the specified city.
		/// </summary>
		/// <param name="city"></param>
		/// <returns></returns>
		public Image GetImage(CityBase city)
		{
			if(city == null)
				throw new ArgumentNullException("city");

			return this.cityHash[city].Image;
		}


		private CityTile GetCityTile(CityBase city)
		{
			CitySizeClass sizeClass = city.SizeClass;
			Civilization civ = city.ParentCountry.Civilization;
			Era era = city.ParentCountry.Era;

			foreach(CityTile tile in this.Items)
			{
				if(tile.Civilization == civ && tile.Era == era && tile.SizeClass == sizeClass)
				{
					return tile;
				}
			}

			return null;
		}
	}     
}