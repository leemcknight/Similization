using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;

namespace McKnight.Similization.Core
{
	/// <summary>
	/// Represents an era of time in the game.
	/// </summary>
	public class Era : NamedObject
	{
        private int eraId;
        private bool firstEra;        
        private Era nextEra;
        private Collection<string> researchers;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Era"/> class.
		/// </summary>
		public Era(DataRow row)
		{
			if(row == null)
				throw new ArgumentNullException("row");
			this.eraId = Convert.ToInt32(row["EraID"], CultureInfo.InvariantCulture);
			this.firstEra = Convert.ToBoolean(row["IsFirstEra"], CultureInfo.InvariantCulture);
			this.Name = Convert.ToString(row["Name"], CultureInfo.InvariantCulture);
			DataRow[] researcherRows = row.GetChildRows("EraResearcher");
			researchers = new Collection<string>();
            foreach (DataRow researcherRow in researcherRows)
                researchers.Add(Convert.ToString(researcherRow["ResearcherName"], CultureInfo.InvariantCulture));			
		}
				
		/// <summary>
		/// Internal id of the era.
		/// </summary>
		internal int EraId
		{
			get { return this.eraId; }
		}
		
		/// <summary>
		/// Determines whether this is the first era in the game.
		/// </summary>
		public bool FirstEra
		{
			get { return this.firstEra; }
			set { this.firstEra = value; }
		}
				
		/// <summary>
		/// The <see cref="Era"/> that comes next in the game.
		/// </summary>
		public Era NextEra
		{
			get { return this.nextEra; }
			set { this.nextEra = value; }
		}
		
		/// <summary>
		/// The names given to researchers in this era.
		/// </summary>
		public Collection<string> Researchers
		{
			get { return this.researchers; }
		}		
	}	
}