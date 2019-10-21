using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using McKnight.Similization.Core;

namespace McKnight.Similization.Core
{
	/// <summary>
	/// Class representing a Civilization.
	/// </summary>
	public class Civilization : NamedObject
	{
        private int civilizationId;
        private string adjective;
        private string noun;
        private string leaderName;
        private bool commercial;
        private bool expansionist;
        private bool industrious;
        private bool militaristic;
        private bool religious;
        private bool scientific;
        private Collection<string> cityNames;
        private NamedObjectCollection<Technology> startingTechnologies;

		/// <summary>
		/// Initializes a new instance of the <see cref="Civilization"/> class.
		/// </summary>
		/// <param name="row">A <see cref="System.Data.DataRow"/> containing the 
		/// record of the Civilization information.</param>
		public Civilization(DataRow row)
		{
			if(row == null)
				throw new ArgumentNullException("row");

			this.civilizationId = Convert.ToInt32(row["CivilizationID"], CultureInfo.InvariantCulture);
			this.adjective = (string)row["Adjective"];
			this.noun = (string)row["Noun"];
			this.expansionist = Convert.ToBoolean(row["Expansionist"], CultureInfo.InvariantCulture);
			this.commercial = Convert.ToBoolean(row["Commercial"], CultureInfo.InvariantCulture);
			this.industrious = Convert.ToBoolean(row["Industrious"], CultureInfo.InvariantCulture);
			this.leaderName = (string)row["LeaderName"];
			this.militaristic = Convert.ToBoolean(row["Militaristic"], CultureInfo.InvariantCulture);
			this.Name = (string)row["Name"];
			this.religious = Convert.ToBoolean(row["Religious"], CultureInfo.InvariantCulture);
			this.scientific = Convert.ToBoolean(row["Scientific"], CultureInfo.InvariantCulture);
			this.startingTechnologies = new NamedObjectCollection<Technology>();
            this.cityNames = new Collection<string>();
		}		
		
		/// <summary>
		/// Internal ID for the <see cref="Civilization"/> object.
		/// </summary>
		internal int CivilizationId
		{
			get { return this.civilizationId; }			
		}
		
		/// <summary>
		/// The word used as an adjective for the <see cref="Civilization"/> in text displayed to the user.
		/// </summary>
		public string Adjective
		{
			get { return this.adjective; }
			set { this.adjective = value; }
		}
		
		/// <summary>
		/// The word used as a noun for the <see cref="Civilization"/> in text displayed to the user.
		/// </summary>
		public string Noun
		{
			get { return this.noun; }
			set { this.noun = value; }
		}
		
		/// <summary>
		/// Determines whether this <see cref="Civilization"/> is considered commercial.
		/// </summary>
		public bool Commercial
		{
			get { return this.commercial; }
			set { this.commercial = value; }
		}
		
		/// <summary>
		/// Determines whether the <see cref="Civilization"/> is considered expansionist.
		/// </summary>
		public bool Expansionist
		{
			get { return this.expansionist; }
			set { this.expansionist = value; }
		}
		
		/// <summary>
		/// Determines whether this <see cref="Civilization"/> is considered industrious.
		/// </summary>
		public bool Industrious
		{
			get { return this.industrious; }
			set { this.industrious = value; }
		}
				
		/// <summary>
		/// Determines whether this <see cref="Civilization"/> is considered militaristic.
		/// </summary>
		public bool Militaristic
		{
			get { return this.militaristic; }
			set { this.militaristic = value; }
		}
		
		/// <summary>
		/// Determines whether this <see cref="Civilization"/> is considered religious.
		/// </summary>
		public bool Religious
		{
			get { return this.religious; }
			set { this.religious = value; }
		}
		
		/// <summary>
		/// Determines whether this <see cref="Civilization"/> is considered scientific.
		/// </summary>
		public bool Scientific
		{
			get { return this.scientific; }
			set { this.scientific = value; }
		}
		
		/// <summary>
		/// The name of the leader of the <see cref="Civilization"/>.
		/// </summary>
		public string LeaderName
		{
			get { return this.leaderName; }
			set { this.leaderName = value; }
		}
		
		/// <summary>
		/// The names of the cities in the civilization.
		/// </summary>
		public Collection<string> CityNames
		{
			get { return this.cityNames; }			
		}
		
		/// <summary>
		/// The list of technologies this civilization starts the game with.
		/// </summary>
		public NamedObjectCollection<Technology> StartingTechnologies
		{
			get { return this.startingTechnologies; }
		}		
	}

	
}
