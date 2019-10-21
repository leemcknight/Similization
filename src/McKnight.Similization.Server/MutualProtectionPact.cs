namespace McKnight.Similization.Server
{
	using System;

	/// <summary>
	/// A mutual proctection pact is an agreement between 2 countries to 
	/// come to each others' aid at time of war.  If one country is attacked,
	/// the other country in the agreement will automatically declare war on 
	/// the attacking country.
	/// </summary>
	public class MutualProtectionPact : DiplomaticAgreement
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MutualProtectionPact"/> class.
		/// </summary>
		/// <param name="parentCountry"></param>
		/// <param name="allyCountry"></param>
		public MutualProtectionPact(Country parentCountry, Country allyCountry)	: base ( parentCountry, allyCountry)
		{
		}
	}
}