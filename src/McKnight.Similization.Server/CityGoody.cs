using System;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents a village "goody" that contains a new city that will join
	/// the country finding the village.
	/// </summary>
	public class CityGoody : VillageGoody
	{		
		/// <summary>
		/// Initializes a new instance of the <see cref="CityGoody"/> class.
		/// </summary>
		/// <param name="tribeName"></param>
		/// <param name="newCity"></param>
		public CityGoody(string tribeName, City newCity) : base(tribeName)
		{
			this.city = newCity;
		}

		/// <summary>
		/// Adds the new city to the countries city list.
		/// </summary>
		/// <param name="goodyOwner"></param>
		public override void ApplyGoody(Country goodyOwner)
		{
			if(goodyOwner == null)
				throw new ArgumentNullException("goodyOwner");

			base.ApplyGoody(goodyOwner);
			this.city.ParentCountry = goodyOwner;
			goodyOwner.Cities.Add(this.city);
		}

		private City city;
		/// <summary>
		/// Gets the city that was found in the village.
		/// </summary>
		public City City
		{
			get { return this.city; }
		}

	}
}