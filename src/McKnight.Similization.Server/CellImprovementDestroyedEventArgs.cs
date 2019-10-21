namespace McKnight.Similization.Server
{
	using System;

	/// <summary>
	/// Event Arguments for the <c>CellImprovementDestroyed</c> event.
	/// </summary>
	public class CellImprovementDestroyedEventArgs : System.EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <c>CellImprovementDestroyedEventArgs</c> class.
		/// </summary>
		/// <param name="cell"></param>
		/// <param name="destroyedBy"></param>
		public CellImprovementDestroyedEventArgs(GridCell cell, Unit destroyedBy)
		{
			this.cell = cell;
			this.destroyedBy = destroyedBy;
		}

		private GridCell cell;

		/// <summary>
		/// The <c>GridCell</c> that had an improvement destroyed.
		/// </summary>
		public GridCell Cell
		{
			get { return this.cell; }
		}


		private Unit destroyedBy;

		/// <summary>
		/// The <c>Unit</c> that destroyed the improvement.
		/// </summary>
		public Unit DestroyedBy
		{
			get { return this.destroyedBy; }
		}

	}
}