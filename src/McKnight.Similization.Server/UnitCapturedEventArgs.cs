namespace McKnight.Similization.Server
{
	using System;

    /// <summary>
    /// Event Arguments fot the <i>Captured</i> event of a <see cref="Unit"/>.
    /// </summary>
	public class UnitCapturedEventArgs : System.EventArgs
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitCapturedEventArgs"/> class.
        /// </summary>
        /// <param name="unitCaptured"></param>
        /// <param name="capturedBy"></param>
		public UnitCapturedEventArgs(Unit unitCaptured, Unit capturedBy)
		{
			this.unitCaptured = unitCaptured;
			this.capturedBy = capturedBy;
		}

		private Unit capturedBy;

		/// <summary>
		/// The enemy unit that has captured the unit.
		/// </summary>
		public Unit CapturedBy
		{
			get { return this.capturedBy; }
		}

		private Unit unitCaptured;

        /// <summary>
        /// The <see cref="Unit"/> that has been captured by the enemy.
        /// </summary>
		public Unit UnitCaptured
		{
			get { return this.unitCaptured; }
		}
	}
}