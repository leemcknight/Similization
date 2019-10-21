using System;
using System.Drawing;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// A barbarian is an AI unit that does not have a parent 
	/// country, and just attacks anything close.
	/// </summary>
	public class Barbarian : AIUnit
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Barbarian"/> class.
		/// </summary>
		/// <param name="coordinates"></param>
		/// <param name="clone"></param>
		public Barbarian(Point coordinates, UnitBase clone) : base(coordinates, clone)
		{
			this.Mode = AIUnitMode.Attack;
		}

		/// <summary>
		/// Takes a turn for the barbarian.
		/// </summary>
		public override void DoTurn()
		{
            GameRoot root = GameRoot.Instance;
            GridCell myCell = root.Grid.GetCell(this.Coordinates);
            Point destination = myCell.FindClosestUnit().Coordinates;
			MoveTo(destination);
		}
	}
}
