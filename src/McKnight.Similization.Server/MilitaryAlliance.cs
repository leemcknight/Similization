namespace McKnight.Similization.Server
{
	using System;

	/// <summary>
	/// A Military Alliance is an agreement between 2 colonies to declare
	/// war on a third.
	/// </summary>
	public class MilitaryAlliance : DiplomaticAgreement
	{
		private Country victim;

		/// <summary>
		/// Initializes a new instance of the <see cref="MilitaryAlliance"/> class.
		/// </summary>
		/// <param name="parentCountry"></param>
		/// <param name="allyCountry"></param>
		/// <param name="victim"></param>
		public MilitaryAlliance(Country parentCountry, Country allyCountry, Country victim)	: base (parentCountry, allyCountry)
		{
			this.victim = victim;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MilitaryAlliance"/> class.
		/// </summary>
		/// <param name="parentCountry"></param>
		/// <param name="allyCountry"></param>
		public MilitaryAlliance(Country parentCountry, Country allyCountry) : base (parentCountry, allyCountry)
		{
		}

		/// <summary>
		/// Gets the <see cref="Country"/> that is the victim of the alliance.
		/// </summary>
		public Country AllianceVictim
		{
			get { return this.victim; }
			set { this.victim = value; }
		}
	}
}