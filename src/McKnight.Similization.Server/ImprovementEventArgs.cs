using System;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{	
	/// <summary>
	/// Event Arguments for the <i>ImprovementDestroyed</i> event of a city.
	/// </summary>
	public class ImprovementDestroyedEventArgs : EventArgs
	{
		/// <summary>
		/// Initialzies a new instance of the <c>ImprovementEventArgs</c> class.
		/// </summary>
		/// <param name="improvement"></param>
        /// <param name="attackingCountry"></param>
		public ImprovementDestroyedEventArgs(Improvement improvement, Country attackingCountry)
		{
			this.improvement = improvement;
			this.attackingCountry = attackingCountry;
		}

		private Improvement improvement;

		/// <summary>
		/// The Improvement the event is tied to.
		/// </summary>
		public Improvement Improvement
		{
			get { return this.improvement; }
		}


		private Country attackingCountry;


		/// <summary>
		/// The <c>Country</c> that attacked the city to destroy the improvement.
		/// </summary>
		public Country AttackingCountry
		{
			get { return this.attackingCountry; }
		}
	}
}