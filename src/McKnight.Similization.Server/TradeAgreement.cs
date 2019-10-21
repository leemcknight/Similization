namespace McKnight.Similization.Server
{
	using System;
	using System.Collections;

	/// <summary>
	/// Represents a trade agreement between two countries.
	/// </summary>
	public class TradeAgreement : DiplomaticAgreement
	{
		private ArrayList _parentItems;
		private ArrayList _allyItems;

		/// <summary>
		/// Initializes a new instance of the <c>TradeAgreement</c> class.
		/// </summary>
		/// <param name="parentCountry"></param>
		/// <param name="allyCountry"></param>
		/// <param name="parentItems"></param>
		/// <param name="allyItems"></param>
		public TradeAgreement( Country parentCountry, Country allyCountry, ArrayList parentItems, ArrayList allyItems )
			: base ( parentCountry, allyCountry)
		{
			_parentItems = parentItems;
			_allyItems = allyItems;
		}

		/// <summary>
		/// Gets the items theat the country is trading.
		/// </summary>
		/// <param name="tradingPartner"></param>
		/// <returns></returns>
		public ArrayList GetTradingItemsForCountry( Country tradingPartner)
		{
			ArrayList items = null;
			if ( tradingPartner == Country1 )
			{
				items = _parentItems;
			}
			else if (tradingPartner == Country2 )
			{
				items = _allyItems; 
			}

			return items;
		}
	}
}