using System;
using System.Drawing;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
    /// <summary>
    /// Class representing a unit to settle new cities.
    /// </summary>
	/// <remarks>
	/// Settlers are special units that can form new cities, 
	/// or join existing cities and increase their population.
    /// </remarks>
	public class Settler : Unit
	{		
		/// <summary>
		/// Initializes a new instance of the <see cref="Settler"/> object.
		/// </summary>
		public Settler() : base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Settler"/> object.
		/// </summary>
		/// <param name="coordinates"></param>
		/// <param name="unitClone"></param>
		public Settler(Point coordinates, UnitBase unitClone) : base(coordinates, unitClone)
		{
			
		}		

		/// <summary>
		/// Settles a new city.  This will create a new city (with the 
		/// given name), and remove the settler from the map.
		/// </summary>
		/// <param name="cityName">The name of the city to create.</param>
		/// <returns>A <c>City</c> object for the city just created.</returns>
		public virtual City Settle(string cityName)
		{
			GameRoot root = GameRoot.Instance;
            GridCell currentCell = root.Grid.GetCell(this.Coordinates);
			if(currentCell.City == null)
			{
				City newCity = new City(this.Coordinates, this.ParentCountry);
				newCity.Name = cityName;
				this.ParentCountry.Cities.Add(newCity);
                this.ParentCountry.Units.Remove(this);
                currentCell.Units.Remove(this);
                currentCell.City = newCity;
				root.ActivateNextUnit();
				return newCity;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Joins the given city.  When this happens, the city's population 
		/// will increase by 2 points, and the settler will be removed from
		/// the map.
		/// </summary>
		/// <param name="city"></param>
		public void JoinCity(City city)
		{
			if(city == null)
				throw new ArgumentNullException("city");
			city.Population += 2;
			ParentCountry.Units.Remove(this);
		}
	}
}
