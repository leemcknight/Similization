using System;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	
	/// <summary>
	/// Class the represents the arguments passes to listeners 
	/// of the event fired when an improvement is built. 
    /// </summary>
    /// <remarks>
    /// These arguments
	/// contain properties describing the improvement that was built,
	/// the city that built them, and the "recommended" next improvement.
    ///</remarks>
	public class ImprovementBuiltEventArgs : EventArgs
	{
		private BuildableItem improvement;
		private BuildableItem recommendedImprovement;
		private City city;

		/// <summary>
		/// Initializes a new instance of the <c>ImprovementBuiltEventArgs</c> class.
		/// </summary>
		/// <param name="improvement"></param>
		/// <param name="city"></param>
		/// <param name="recommendedNextImprovement"></param>
		public ImprovementBuiltEventArgs(BuildableItem improvement,	City city, BuildableItem recommendedNextImprovement)
		{
			this.improvement = improvement;
			this.recommendedImprovement = recommendedNextImprovement;
			this.city = city;
		}

		/// <summary>
		/// The city that built the improvement.
		/// </summary>
		public City City
		{
			get { return this.city; }
		}

		/// <summary>
		/// The improvement that was just built.
		/// </summary>
		public BuildableItem Improvement
		{
			get { return this.improvement; }
		}

		/// <summary>
		/// The "recommended" improvement to build next.  If it is not changed,
		/// this will be the improvement that is automatically started on the the
		/// next turn.
		/// </summary>
		public BuildableItem RecommendedImprovement
		{
			get { return this.recommendedImprovement; }
		}
	}

}