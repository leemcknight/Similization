using System;
using System.Data;
using System.Globalization;
using McKnight.Similization.Core;

namespace McKnight.Similization.Core
{
	/// <summary>
	/// Represents a city improvement.
	/// </summary>
	public class Improvement : BuildableItem
	{        
        private int culturePerTurn;
        private int maintenanceCostPerTurn;
        private int pollutionPoints;
        private NamedObjectCollection<Technology> requiredTechnologies;
        private NamedObjectCollection<Improvement> requiredImprovements;
        private NamedObjectCollection<Resource> requiredResources;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Improvement"/> class.
		/// </summary>
		public Improvement() 
		{
            this.requiredImprovements = new NamedObjectCollection<Improvement>();
			this.requiredResources = new NamedObjectCollection<Resource>();
			this.requiredTechnologies = new NamedObjectCollection<Technology>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Improvement"/> class.
		/// </summary>
		/// <param name="row"></param>
		public Improvement(DataRow row) : this()
		{
			if(row == null)
				throw new ArgumentNullException("row");
			this.culturePerTurn = Convert.ToInt32(row["Culture"], CultureInfo.InvariantCulture);
			this.maintenanceCostPerTurn = Convert.ToInt32(row["MaintenanceCost"], CultureInfo.InvariantCulture);
			this.pollutionPoints = Convert.ToInt32(row["Pollution"], CultureInfo.InvariantCulture);
			this.Cost = Convert.ToInt32(row["BuildCost"], CultureInfo.InvariantCulture);
			this.Name = Convert.ToString(row["Name"], CultureInfo.InvariantCulture);
		}
		
		/// <summary>
		/// The number of culture points generated for a country 
		/// by this Improvement each turn.
		/// </summary>
		public int CulturePerTurn
		{
			get { return this.culturePerTurn; }
		}
		
		/// <summary>
		/// Determines the amount of gold per turn that a <c>Country</c> 
		/// must pay to maintain the <c>Improvement</c>.
		/// </summary>
		public int MaintenanceCostPerTurn
		{
			get { return this.maintenanceCostPerTurn; }
			set { this.maintenanceCostPerTurn = value; }
		}
		
		/// <summary>
		/// The amount of pollution caused by the <c>Improvement</c>.
		/// </summary>
		public int PollutionPoints
		{
			get { return this.pollutionPoints; }
			set { this.pollutionPoints = value; }
		}
		
		/// <summary>
		/// The list of improvements that must be built in a city before 
		/// this improvement can be built.
		/// </summary>
        public NamedObjectCollection<Improvement> RequiredImprovements
		{
			get { return this.requiredImprovements; }
		}
		
		/// <summary>
		/// The list of resources that must be available to build this improvement.
		/// </summary>
        public NamedObjectCollection<Resource> RequiredResources
		{
			get { return this.requiredResources; }
		}
		
		/// <summary>
		/// The list of technologies that must be available to build this improvement.
		/// </summary>
		public NamedObjectCollection<Technology> RequiredTechnologies
		{
			get { return this.requiredTechnologies; }
		}
	}
}