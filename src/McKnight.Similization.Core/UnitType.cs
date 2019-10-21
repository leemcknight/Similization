using System;

namespace McKnight.Similization.Core
{
	
	/// <summary>
	/// Basic unit types.
	/// </summary>
	public enum UnitType
	{
		/// <summary>
		/// The unit is land-based.  It can travel only on land 
		/// (dry) squares.
		/// </summary>
		Land,

		/// <summary>
		/// The unit is sea-based.  It can travel on sea squares
		/// and dock on sea-accessible cities.
		/// </summary>
		Sea,

		/// <summary>
		/// The unit can fly.
		/// </summary>
		Air
	}
}