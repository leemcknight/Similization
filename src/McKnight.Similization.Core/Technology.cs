using System;
using System.Data;
using System.Globalization;
using McKnight.Similization.Core;

namespace McKnight.Similization.Core
{
	/// <summary>
	/// Represents a technology that can be traded between countries.
	/// </summary>
	public class Technology : NamedObject
	{
        private int technologyID;
        private int eraId;
        private Era era;        
        private bool requiredForEraAdvance;
        private bool requiredForEspionage;
        private int requiredResearchUnits;
        private NamedObjectCollection<Technology> requiredTechnologies;

		/// <summary>
		/// Initializes a new instance of the <see cref="Technology"/> class.
		/// </summary>
		/// <param name="row">A <c>System.Data.DataRow</c> object containing the 
		/// information about the technology.</param>
		public Technology(DataRow row)
		{
			if(row == null)
				throw new ArgumentNullException("row");

			this.technologyID = Convert.ToInt32(row["TechnologyID"], CultureInfo.InvariantCulture);
			this.eraId = Convert.ToInt32(row["EraID"], CultureInfo.InvariantCulture);
			this.Name = Convert.ToString(row["Name"], CultureInfo.InvariantCulture);
            if(!row.IsNull("RequiredForNextEra"))
			    this.requiredForEraAdvance = Convert.ToBoolean(row["RequiredForNextEra"], CultureInfo.InvariantCulture);
            if(!row.IsNull("RequiredForEspionage"))
                this.requiredForEspionage = Convert.ToBoolean(row["RequiredForEspionage"], CultureInfo.InvariantCulture);
			this.requiredResearchUnits = Convert.ToInt32(row["ResearchUnits"], CultureInfo.InvariantCulture);
			this.requiredTechnologies = new NamedObjectCollection<Technology>();
		}
				
		/// <summary>
		/// Internal ID for the <see cref="Technology"/> object.
		/// </summary>
		internal int TechnologyId
		{
			get { return this.technologyID; }
		}
		
		/// <summary>
		/// Internal ID for the Era.
		/// </summary>
		internal int EraId
		{
			get { return this.eraId; }
		}
		
		/// <summary>
		/// The <see cref="Era"/> the technology is a part of.
		/// </summary>
		public Era Era
		{
			get { return this.era; }
			set { this.era = value; }
		}	
		
		/// <summary>
		/// Determines whether this <see cref="Technology"/> must be obtained by a 
		/// <see cref="Country"/> before the country can move to the next <see cref="Era"/>.
		/// </summary>
		public bool RequiredForEraAdvance
		{
			get { return this.requiredForEraAdvance; }
			set { this.requiredForEraAdvance = value; }
		}
        
        /// <summary>
        /// Determines whether this <see cref="Technology"/> is required before 
        /// a country can perform espionage.
        /// </summary>
        public bool RequiredForEspionage
        {
            get { return this.requiredForEspionage; }
            set { this.requiredForEspionage = value; }
        }
		
		/// <summary>
		/// The number of research units required to obtain this technology.
		/// </summary>
		public int RequiredResearchUnits
		{
			get { return this.requiredResearchUnits; }
			set { this.requiredResearchUnits = value; }
		}
		
		/// <summary>
		/// The list of technologies that must be researched before this technology can be researched.
		/// </summary>
		public NamedObjectCollection<Technology> RequiredTechnologies
		{
			get { return this.requiredTechnologies; }
		}
		
        /*
		/// <summary>
		/// Gets the value of the <see cref="Technology"/> to the specified <see cref="Country"/>.
		/// </summary>
		/// <returns></returns>
		public int CalculateValueForCountry(Country country)
		{
            if (country == null)
                throw new ArgumentNullException("country");

            if (country.AcquiredTechnologies.Contains(this))
                return 0;

            if (country.ResearchedTechnology == this)
            {
                //how far along are we?
                return 100 - country.PercentageDoneResearching;
            }
            return 100;
		}
         * */

        /// <summary>
        /// Gets a string representation of the <see cref="Technology"/>.
        /// </summary>
        /// <returns></returns>
		public override string ToString()
		{
			return this.Name; 
		}
	}
}
