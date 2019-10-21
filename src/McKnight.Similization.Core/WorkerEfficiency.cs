using System;

namespace McKnight.Similization.Core
{
	/// <summary>
	/// Different levels of worker efficiency.
	/// </summary>
	public enum WorkerEfficiency
	{
		/// <summary>
		/// 50% less efficient than usual
		/// </summary>
		MinusFiftyPercent,

		/// <summary>
		/// 25% less efficient than usual
		/// </summary>
		MinusTwentyFivePercent,

		/// <summary>
		/// Normal efficiency
		/// </summary>
		Normal,

		/// <summary>
		/// 25% more efficient than usual
		/// </summary>
		PlusTwentyFivePercent,

		/// <summary>
		/// 50% more efficient than usual
		/// </summary>
		PlusFiftyPercent
	}
}