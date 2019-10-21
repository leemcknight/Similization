namespace McKnight.Similization.Server
{
	using System;

	/// <summary>
	/// Class representing a peace treaty between 2 countries.
	/// </summary>
	public class PeaceTreaty : DiplomaticAgreement
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PeaceTreaty"/> class.
		/// </summary>
		/// <param name="parentCountry"></param>
		/// <param name="foreignCountry"></param>
		public PeaceTreaty(Country parentCountry, Country foreignCountry) : base(parentCountry, foreignCountry)
		{
		}
	}
}