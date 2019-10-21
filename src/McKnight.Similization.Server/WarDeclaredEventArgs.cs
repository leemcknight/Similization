namespace McKnight.Similization.Server
{
	using System;

	/// <summary>
	/// Event Arguments for the <i>WarDeclared</i> event of a <see cref="Country"/>.
	/// </summary>
	public class WarDeclaredEventArgs : EventArgs
	{
		private Country agressor;
		private Country victim;

		/// <summary>
		/// Initializes a new instance of the <see cref="WarDeclaredEventArgs"/> class.
		/// </summary>
		/// <param name="agressor"></param>
		/// <param name="victim"></param>
		public WarDeclaredEventArgs(Country agressor, Country victim)
		{
			this.agressor = agressor;
			this.victim = victim;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WarDeclaredEventArgs"/> class.
		/// </summary>
		public WarDeclaredEventArgs()
		{
		}

		/// <summary>
		/// The <see cref="Country"/> initiating the war.
		/// </summary>
		public Country Agressor
		{
			get { return this.agressor; }
			set { this.agressor = value; }
		}

		/// <summary>
		/// The <see cref="Country"/> having war declared upon them.
		/// </summary>
		public Country Victim
		{
			get { return this.victim; }
			set { this.victim = value; }
		}
	}
}