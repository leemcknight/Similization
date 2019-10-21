namespace McKnight.Similization.Server
{
	using System;

	/// <summary>
	/// A trade embargo is an agreement with two colonies
	/// not to trade resources or luxuries with a third 
	/// colony.
	/// </summary>
	public class TradeEmbargo : DiplomaticAgreement
	{
		private Country _victim;

		/// <summary>
		/// Initializes a new instance of the <c>TradeEmbargo</c> class.
		/// </summary>
		/// <param name="parentCountry"></param>
		/// <param name="allyCountry"></param>
		/// <param name="victim"></param>
		public TradeEmbargo(Country parentCountry, Country allyCountry, Country victim)
			: base ( parentCountry, allyCountry)
		{
			_victim = victim;
		}

		/// <summary>
		/// The victim of the trade embargo.  The allies
		/// agree not to trade with the victim.
		/// </summary>
		public Country EmbargoVictim
		{
			get { return _victim; }
		}
	}
}