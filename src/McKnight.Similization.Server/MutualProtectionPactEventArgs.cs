namespace McKnight.Similization.Server
{
	using System;

	/// <summary>
	/// Event Arguments for the <i>MutualProtectionPactInvoked</i> event of a <see cref="Country"/>.
	/// </summary>
	public class MutualProtectionPactEventArgs : EventArgs
	{
		private MutualProtectionPact pact;
		private Country commonEnemy;

		/// <summary>
		/// Initializes a new instance of the <see cref="McKnight.Similization.Server.MutualProtectionPactEventArgs"/> 
		/// class.
		/// </summary>
		/// <param name="pact"></param>
        /// <param name="commonEnemy"></param>
		public MutualProtectionPactEventArgs(MutualProtectionPact pact, Country commonEnemy)
		{
			this.pact = pact;
			this.commonEnemy = commonEnemy;
		}

		/// <summary>
		/// The <see cref="McKnight.Similization.Server.Country"/> that has attacked our ally, causing 
		/// the partner in the <see cref="McKnight.Similization.Server.MutualProtectionPact"/> to delcare 
		/// war on them.
		/// </summary>
		public Country CommonEnemy
		{
			get { return this.commonEnemy; }
		}

		/// <summary>
		/// The <see cref="McKnight.Similization.Server.MutualProtectionPact"/> associated 
		/// with the event.
		/// </summary>
		public MutualProtectionPact MutualProtectionPact
		{
			get { return this.pact; }
		}
	}
}