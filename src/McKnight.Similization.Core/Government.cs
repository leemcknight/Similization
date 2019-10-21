using System;
using System.Data;
using System.Globalization;
using McKnight.Similization.Core;

namespace McKnight.Similization.Core
{
	/// <summary>
	/// Represents a single government in the game. 
	/// </summary>
	public class Government : NamedObject
	{        
        private int freeCityUnits;
        private int freeMetropolisUnits;
        private int freeTownUnits;
        private string leaderTitle;
        private bool primary;
        private bool fallback;
        private NamedObjectCollection<Technology> requiredTechnologies;
        private WorkerEfficiency workerEfficiency;

		/// <summary>
		/// Initializes a new instance of the <see cref="Government"/> class.
		/// </summary>
		/// <param name="row">A <c>System.Data.DataRow</c> containing the information 
		/// about the Government.</param>
		public Government(DataRow row)
		{
			if(row == null)
				throw new ArgumentNullException("row");

			this.Name = Convert.ToString(row["Name"], CultureInfo.InvariantCulture);
			this.fallback = Convert.ToBoolean(row["IsFallback"], CultureInfo.InvariantCulture);
			this.freeCityUnits = Convert.ToInt32(row["FreeCityUnits"], CultureInfo.InvariantCulture);
			this.freeMetropolisUnits = Convert.ToInt32(row["FreeMetropolisUnits"], CultureInfo.InvariantCulture);
			this.freeTownUnits = Convert.ToInt32(row["FreeTownUnits"], CultureInfo.InvariantCulture);
			this.leaderTitle = Convert.ToString(row["LeaderTitle"], CultureInfo.InvariantCulture);
			this.primary = Convert.ToBoolean(row["IsPrimary"], CultureInfo.InvariantCulture);
			this.requiredTechnologies = new NamedObjectCollection<Technology>();

			int efficiency = Convert.ToInt32(row["WorkerEfficiencyFactor"], CultureInfo.InvariantCulture);
			switch(efficiency)
			{
				case 1:
					this.workerEfficiency = WorkerEfficiency.Normal;
					break;
				case 2:
					this.workerEfficiency = WorkerEfficiency.PlusTwentyFivePercent;
					break;
				case 3:
					this.workerEfficiency = WorkerEfficiency.PlusFiftyPercent;
					break;
				case -2:
					this.workerEfficiency = WorkerEfficiency.MinusTwentyFivePercent;
					break;
				case -3:
					this.workerEfficiency = WorkerEfficiency.MinusFiftyPercent;
					break;
				default:
					throw new InvalidOperationException(McKnight_Similization_Core.UnknownWorkerEfficiency);
			}
		}		
				
		/// <summary>
		/// Determines whether this government is the fallback government.
		/// </summary>
		/// <remarks>FallBack governments take power when the Country is in anarchy.</remarks>
		public bool Fallback
		{
			get { return this.fallback; }
			set { this.fallback = value; }
		}
		
		/// <summary>
		/// The number of free units allowed by cities under this 
		/// government.
		/// </summary>
		public int FreeCityUnits
		{
			get { return this.freeCityUnits; }
			set { this.freeCityUnits = value; }
		}
		
		/// <summary>
		/// The number of free units allowed by metropolises under this 
		/// government.
		/// </summary>
		public int FreeMetropolisUnits
		{
			get { return this.freeMetropolisUnits; }
			set { this.freeMetropolisUnits = value; }
		}
		
		/// <summary>
		/// The number of free units allowed by towns under this 
		/// government.
		/// </summary>
		public int FreeTownUnits
		{
			get { return this.freeTownUnits; }
			set { this.freeTownUnits = value; }
		}
		
		/// <summary>
		/// The title of the leader for this goverment.
		/// </summary>
		public string LeaderTitle
		{
			get { return this.leaderTitle; }
			set { this.leaderTitle = value; }
		}
		
		/// <summary>
		/// Determines whether this is the primary government in the game.
		/// </summary>
		public bool Primary
		{
			get { return this.primary; }
			set { this.primary = value; }
		}
		
		/// <summary>
		/// The list of technologies that must be obtained before this government can be implemented 
		/// by a country.
		/// </summary>
		public NamedObjectCollection<Technology> RequiredTechnologies
		{
			get { return this.requiredTechnologies; }
		}
		
		/// <summary>
		/// The efficency of workers in this government.
		/// </summary>
		public WorkerEfficiency WorkerEfficiency
		{
			get { return this.workerEfficiency; }
		}
	}
}