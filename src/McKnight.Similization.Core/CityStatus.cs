using System;

namespace McKnight.Similization.Core
{	
	/// <summary>
	/// Different statuses a city can be in.
	/// </summary>
	public enum CityStatus
	{
		/// <summary>
		/// Normal status
		/// </summary>
		Normal,

		/// <summary>
		/// The city is having a "We Love the mayor" day.
		/// </summary>
		LoveTheMayor,

		/// <summary>
		/// The city is in disorder.
		/// </summary>
		Disorder
	}
}