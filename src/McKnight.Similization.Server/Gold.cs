using System;
using System.Globalization;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents a lump sum of gold that can be traded or given away during 
	/// negotations and/or trades.
	/// </summary>
	public class GoldLumpSum : ITradable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GoldLumpSum"/> class.
		/// </summary>
		/// <param name="lumpAmount">The amount of gold in the lump sum.</param>
		public GoldLumpSum(int lumpAmount)
		{
			this.lumpSum = lumpAmount;
		}

		/// <summary>
		/// Returns the value of the gold to the specified <see cref="Country"/>.
		/// </summary>
		/// <returns></returns>
		public int CalculateValueForCountry(CountryBase country)
		{
            if (country == null)
                throw new ArgumentNullException("country");

            if (country.Gold == 0 && this.lumpSum >= 100)
                return 100;
            else if (country.Gold == 0 && this.lumpSum < 100)
                return this.lumpSum;

            double tg = Convert.ToDouble(country.Gold);
            double ng = Convert.ToDouble(this.lumpSum);

            double c = ng / tg;
            if (c > 1.0d)
                return 100;     //more than doubling wealth!
            else if ((1.0d > c) && (c >= .75d))
                return 75;
            else if ((.75d > c) && (c >= .5d))
                return 50;
            else if ((.5d > c) && (c >= .25d))
                return 25;
            else
                return 5;            
		}

		/// <summary>
		/// Gets a string representation of the lump sum of gold.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.lumpSum.ToString(CultureInfo.InvariantCulture) + " Gold (Lump Sum)";
		}

		private int lumpSum;

		/// <summary>
		/// Gets the amount of gold in the lump sum.
		/// </summary>
		public int LumpSum
		{
			get { return this.lumpSum; }
			set { this.lumpSum = value; }
		}
	}

	/// <summary>
	/// Represents an amount of gold per turn that will be paid to a foreign country.
	/// </summary>
	public class GoldPerTurn : ITradable
	{
		/// <summary>
		/// Initializes a new instance of the <c>GoldPerTurn</c> class.
		/// </summary>
		/// <param name="amountPerTurn"></param>
		public GoldPerTurn(int amountPerTurn)
		{
			this.amountPerTurn = amountPerTurn;
		}

		/// <summary>
		/// Returns the value of the trade for the specified <see cref="Country"/>
		/// </summary>
		/// <returns></returns>
		public int CalculateValueForCountry(CountryBase country)
		{
            if (country == null)
                throw new ArgumentNullException("country");

			double ng = Convert.ToDouble(this.amountPerTurn * 20);
            double tg = country.Gold;

            double c = ng / tg;

            if (c > 1.0d)
                return 100;     //more than doubling wealth!
            else if ((1.0d > c) && (c >= .75d))
                return 75;
            else if ((.75d > c) && (c >= .5d))
                return 50;
            else if ((.5d > c) && (c >= .25d))
                return 25;
            else
                return 5;
		}


		private int amountPerTurn;

		/// <summary>
		/// Gets the amount of gold per turn that will be given to a foreign country.
		/// </summary>
		public int AmountPerTurn
		{
			get { return this.amountPerTurn; }
			set { this.amountPerTurn = value; }
		}

		/// <summary>
		/// Gets a string representation of the <c>GoldPerTurn</c> class instance.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.amountPerTurn.ToString(CultureInfo.InvariantCulture) + " Per Turn";

		}

	}
}
