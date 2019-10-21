using System;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// The tradable world map of a particular <see cref="Country"/>. 
	/// </summary>
    /// <remarks>The world map represents all of the area on the map 
    /// the <i>Owner</i> has explored.</remarks>
	public class WorldMap : ITradable
	{
        private Country owner;

		/// <summary>
		/// Gets the value of the world map.
		/// </summary>
		/// <returns></returns>
		public int CalculateValueForCountry(CountryBase country)
		{
            if (country == null)
                throw new ArgumentNullException("country");            
            throw new NotImplementedException();            
		}

        /// <summary>
        /// The owner of the <see cref="WorldMap"/>
        /// </summary>
        public Country Owner
        {
            get { return this.owner; }
        }
		
	}

	/// <summary>
	/// The territory map of a particular civilization
	/// </summary>
	public class TerritoryMap : ITradable
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="TerritoryMap"/> class.
        /// </summary>
        /// <param name="owner"></param>
        public TerritoryMap(Country owner)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");
            this.owner = owner;
        }

        private Country owner;
		/// <summary>
		/// Gets the value of the territory map.
		/// </summary>
		/// <returns></returns>
		public int CalculateValueForCountry(CountryBase country)
		{
            if (country == null)
                throw new ArgumentNullException("country");
            throw new NotImplementedException();
		}

        /// <summary>
        /// The owner of the <see cref="TerritoryMap"/>.
        /// </summary>
        public Country Owner
        {
            get { return this.owner; }
        }
	}
}
