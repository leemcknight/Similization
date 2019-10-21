using System;
using System.Drawing;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Contains methods to create and return "goodies" that are to 
	/// be placed inside villages.
	/// </summary>
	internal static class GoodyFactory
	{	
		/// <summary>
		/// Gets a <see cref="VillageGoody"/> from the factory.
		/// </summary>
		/// <returns></returns>
		public static VillageGoody GetGoody(Village village, Unit explorer)
		{
			if(village == null)
				throw new ArgumentNullException("village");
			if(explorer == null)
				throw new ArgumentNullException("explorer");

			int r = RandomNumber.UpTo(7);
			VillageGoody goody = new EmptyGoody(village.TribeName);

			switch(r)
			{
				case 0:
					goody = new EmptyGoody(village.TribeName);
					break;
				case 1:
					goody = new GoldGoody(village.TribeName, GetGoldAmount());
					break;
				case 2:
					goody = new UnitGoody(village.TribeName, GetUnit(explorer));
					break;
				case 3:
					goody = new SettlerGoody(village.TribeName, GetSettler(explorer));
					break;
				case 4:
					goody = new TechnologyGoody(village.TribeName, GetTechnology(explorer.ParentCountry));
					break;
				case 5:
					goody = new BarbarianGoody(village.TribeName, GetBarbarian(explorer));
					break;
				case 6:
					goody = new CityGoody(village.TribeName, GetCity(village, explorer));
					break;
				case 7:
					goody = new MapGoody(village.TribeName);
					break;
			}

			return goody;
		}

		private static int GetGoldAmount()
		{
			return 10;
		}

		/// <summary>
		/// Gets a new unit from the village for the exploring units' country.
		/// </summary>
		/// <param name="explorer"></param>
		/// <returns></returns>
		private static Unit GetUnit(Unit explorer)
		{
			GameRoot root = GameRoot.Instance;
			UnitBase unit = root.Ruleset.Units[0];
			Unit newUnit = new Unit(explorer.Coordinates, unit);			
			return newUnit;
		}

		/// <summary>
		/// Gets a new settler from the village for the exploring units' country.
		/// </summary>
		/// <param name="explorer"></param>
		/// <returns></returns>
		private static Settler GetSettler(Unit explorer)
		{			
			Settler settler = UnitFactory.CreateSettler(explorer.Coordinates, explorer.ParentCountry);						
			return settler;
		}

		/// <summary>
		/// Gets a barbarian from the village.
		/// </summary>
		/// <returns></returns>
		private static Barbarian GetBarbarian(Unit explorer)
		{
			GameRoot root = GameRoot.Instance;
			UnitBase baseUnit =  root.Ruleset.Units[0];
            Point coordinates = explorer.Coordinates;
            GridCell cell = root.Grid.GetCell(coordinates);
			GridCell location = cell.FindClosestEmptyLandCell();
			Barbarian b = new Barbarian(location.Coordinates, baseUnit);
			return b;
		}

		/// <summary>
		/// Gets a new technology from the village for the exploring country.
		/// </summary>
		/// <param name="explorer"></param>
		/// <returns></returns>
		private static Technology GetTechnology(Country explorer)
		{
			return explorer.ResearchableTechnologies[0];;
		}

		/// <summary>
		/// Gets a new city from the village for the exploring units' country.
		/// </summary>
		/// <param name="v"></param>
		/// <param name="explorer"></param>
		/// <returns></returns>
		private static City GetCity(Village v, Unit explorer)
		{
			City newCity = null;
			if(explorer.GetType() == typeof(AIUnit) || 
				explorer.GetType() == typeof(AISettler) ||
				explorer.GetType() == typeof(AIWorker))
			{
				newCity = new AICity(explorer.Coordinates, explorer.ParentCountry);
			}
			else
			{
				newCity =  new City(explorer.Coordinates, explorer.ParentCountry);
			}
			
			return newCity;
		}
	}
}