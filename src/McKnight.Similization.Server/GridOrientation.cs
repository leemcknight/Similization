using System;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Different directions a player can be facing/moving on the grid.
	/// </summary>
	public enum GridOrientation
	{
		/// <summary>
		/// The object is facing a northerly direction.
		/// </summary>
		FacingNorth,

		/// <summary>
		/// The object is facing a southerly direction.
		/// </summary>
		FacingSouth,

		/// <summary>
		/// The object is facing east.
		/// </summary>
		FacingEast,

		/// <summary>
		/// The object is facing west.
		/// </summary>
		FacingWest,

		/// <summary>
		/// The object is facing northeast.
		/// </summary>
		FacingNortheast,

		/// <summary>
		/// The object is facing northwest.
		/// </summary>
		FacingNorthwest,

		/// <summary>
		/// The object is facing southeast.
		/// </summary>
		FacingSoutheast,

		/// <summary>
		/// The object is facing southwest.
		/// </summary>
		FacingSouthwest
	}

}