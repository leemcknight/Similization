namespace McKnight.Similization.Server
{
	using System;

	/// <summary>
	/// A Right of passage agreement between 2 countries allows the
	/// countries to freely pass each others land, use their roads and
	/// rail, without fear of retaliation.
	/// </summary>
	public class RightOfPassage : DiplomaticAgreement
	{
		/// <summary>
		/// Initializes a new instance of the <c>RightOfPassage</c> class.
		/// </summary>
		/// <param name="parentCountry"></param>
		/// <param name="allyCountry"></param>
		public RightOfPassage(Country parentCountry, Country allyCountry)
			: base ( parentCountry, allyCountry )
		{
		}
			 
	}
}