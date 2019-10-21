using System;
using System.Data;
using System.Collections;
using System.Globalization;
using System.Xml;
using McKnight.Similization.Core;

namespace McKnight.Similization.Core
{
	/// <summary>
	/// Represents a Resource that can be traded by two colonies.
	/// </summary>
	public class Resource : NamedObject, ITradable
	{				
        private int commerceEffect;
        private int foodEffect;
        private int shieldEffect;
        private bool railroadPrerequisite;
        private NamedObjectCollection<Technology> requiredTechnologies;
        private NamedObjectCollection<Terrain> terrains;

		/// <summary>
		/// Initializes a new instance of the <see cref="Resource"/> class.
		/// </summary>
		/// <param name="row">A <c>System.Data.DataRow</c> with the 
		/// information about the Resource.</param>
		public Resource(DataRow row)
		{
			if(row == null)
				throw new ArgumentNullException("row");

			this.commerceEffect = Convert.ToInt32(row["Commerce"], CultureInfo.InvariantCulture);
			this.foodEffect = Convert.ToInt32(row["Food"], CultureInfo.InvariantCulture);
			this.Name = Convert.ToString(row["Name"], CultureInfo.InvariantCulture);
			this.railroadPrerequisite = Convert.ToBoolean(row["IsRailroadPrerequisite"], CultureInfo.InvariantCulture);
			this.shieldEffect = Convert.ToInt32(row["Shields"], CultureInfo.InvariantCulture);
			this.terrains = new NamedObjectCollection<Terrain>();
			this.requiredTechnologies = new NamedObjectCollection<Technology>();
		}
						
		/// <summary>
		/// The effect the resource has on commerce.
		/// </summary>
		public int CommerceEffect
		{
			get { return this.commerceEffect; }
			set { this.commerceEffect = value; }
		}
		
		/// <summary>
		/// The effect the resource has on food production for a 
		/// City.
		/// </summary>
		public int FoodEffect
		{
			get { return this.foodEffect; }
			set { this.foodEffect = value; }
		}
		
		/// <summary>
		/// Determines whether or not this resource is required in order 
		/// to build railroads.
		/// </summary>
		public bool RailroadPrerequisite
		{
			get { return this.railroadPrerequisite; }
			set { this.railroadPrerequisite = value; }
		}
		
		/// <summary>
		/// The list of technologies that must be researched before this resource 
		/// can be used.
		/// </summary>
		public NamedObjectCollection<Technology> RequiredTechnologies
		{
			get { return this.requiredTechnologies; }
		}
		
		/// <summary>
		/// The effect the resource has on shield production for a 
		/// city.
		/// </summary>
		public int ShieldEffect
		{
			get { return this.shieldEffect; }
			set { this.shieldEffect = value; }
		}
		
		/// <summary>
		/// The different terrains this resource can be found on.
		/// </summary>
		public NamedObjectCollection<Terrain> Terrains
		{
			get { return this.terrains; }
		}

		/// <summary>
		/// The name of the Resource.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.Name;
		}
        
        /// <summary>
        /// Calculates the value of the resource to the specified <see cref="CountryBase"/>.
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public int CalculateValueForCountry(CountryBase country)
        {
            throw new NotImplementedException();
        } 
    }
}
