using System;
using System.Drawing;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents a settler for a computer opponent.
	/// </summary>
	public class AISettler : Settler
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AISettler"/> class.
		/// </summary>
		public AISettler() : base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AISettler"/> class.
		/// </summary>
		/// <param name="coordinates"></param>
		/// <param name="unitClone"></param>
		public AISettler(Point coordinates, UnitBase unitClone) : base(coordinates, unitClone)
		{
			
		}

		/// <summary>
		/// Takes a turn for the AI Settler.
		/// </summary>
		public override void DoTurn()
		{
			if(IsSuitableCityLocation())
			{
				string cityName = ParentCountry.CreateNewCityName();
				Settle(cityName);
			}
			else
			{
                Point coordinates = FindBestCityLocation();
                MoveTo(coordinates);				
			}

			base.DoTurn();
		}

        private Point FindBestCityLocation()
        {
            throw new NotImplementedException();
        }

		/// <summary>
		/// Settles a new city.  This will create a new city (with the 
		/// given name), and remove the settler from the map.
		/// </summary>
		/// <param name="cityName">The name of the city to create.</param>
		/// <returns>A <c>City</c> object for the city just created.</returns>
		public override City Settle(string cityName)
		{
			GameRoot root = GameRoot.Instance;
            GridCell cell = root.Grid.GetCell(this.Coordinates);

			if(cell.City == null)
			{
				AICity newCity = new AICity(this.Coordinates, this.ParentCountry);
				newCity.Name = cityName;
				ParentCountry.Cities.Add(newCity);				
				//have to move to the next unit, since this unit no longer exists.
				root.ActivateNextUnit();
				return newCity;
			}
			else
			{
				throw new InvalidOperationException(ServerResources.GridCellHasCity);
			}
		}

		/// <summary>
		/// Determines whether the current position of the 
        /// <see cref="AISettler"/> is considered suitable for building a city.
		/// </summary>
		/// <returns></returns>
		private bool IsSuitableCityLocation()
		{
            Grid grid = GameRoot.Instance.Grid;
            GridCell cell = grid.GetCell(this.Coordinates);
            if (cell.HasCityInRadius(4))
				return false;			
            double desirability = grid.GetCityLocationDesirability(cell);
			return (desirability >= 75d);
		}
	}
}
