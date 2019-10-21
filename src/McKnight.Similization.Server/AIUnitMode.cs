using System;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Different modes an AI unit can be in.
	/// </summary>
	public enum AIUnitMode
	{
		/// <summary>
		/// The unit is currently attacking, or trying to attack 
		/// another unit.
		/// </summary>
		Attack,

		/// <summary>
		/// The unit is currently taking defensive positions.
		/// </summary>
		Defend,

		/// <summary>
		/// The unit is pillaging or trying to pillage a cell.
		/// </summary>
		Pillage,

		/// <summary>
		/// The unit is exploring territory.
		/// </summary>
		Explore,
	}
}