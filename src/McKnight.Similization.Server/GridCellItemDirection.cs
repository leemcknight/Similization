namespace McKnight.Similization.Server
{
	using System;

	/// <summary>
	/// Describes the layout of items like roads, railroads, and rivers 
	/// on a particular cell.
	/// </summary>
	public enum GridCellItemDirection
	{
		/// <summary>
		/// The item does not exist on this cell.
		/// </summary>
		None,

		/// <summary>
		/// The item goes in a north/south direction.
		/// </summary>
		NorthSouth,

		/// <summary>
		/// The item goes in an east/west direction.
		/// </summary>
		EastWest,

		/// <summary>
		/// The item goes both east/west and north/south (multiple items on the cell)
		/// </summary>
		Bidirectional,

		/// <summary>
		/// The item turns in the cell from east to south.
		/// </summary>
		SoutheastCorner,

		/// <summary>
		/// The item turns in the cell from east to north.
		/// </summary>
		NortheastCorner,

		/// <summary>
		/// The item turns in the cell from south to west.
		/// </summary>
		SouthwestCorner,

		/// <summary>
		/// The item turns in the cell from nort to west.
		/// </summary>
		NorthwestCorner,

		/// <summary>
		/// The item runs from the southeast to the northwest.
		/// </summary>
		SoutheastToNorthwest,

		/// <summary>
		/// The item runs from the southwest to the northeast.
		/// </summary>
		SouthwestToNortheast
	}
}