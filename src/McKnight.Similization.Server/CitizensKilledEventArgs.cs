namespace McKnight.Similization.Server
{
	using System;

	/// <summary>
	/// Event Arguments for the <c>CitizensKilled</c> event of a <c>City</c>.
	/// </summary>
	public class CitizensKilledEventArgs : System.EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <c>CitizensKilledEventArgs</c> class.
		/// </summary>
		/// <param name="attackingCountry"></param>
		public CitizensKilledEventArgs(Country attackingCountry)
		{
			this.attackingCountry = attackingCountry;
		}

		private Country attackingCountry;

		/// <summary>
		/// The <c>Country</c> responsible for killing the City's citizens.
		/// </summary>
		public Country AttackingCountry
		{
			get { return this.attackingCountry; }
		}
	}
}