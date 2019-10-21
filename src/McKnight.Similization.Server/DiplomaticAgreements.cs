using System;
using System.Collections;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Base class for all diplomatic agreements.  This class
	/// cannot be directly created.
	/// </summary>
	public abstract class DiplomaticAgreement : ITradable
	{
		private Country _country1;
		private Country _country2;
		private int _turnsLeft;

		/// <summary>
		/// Initializes a new instance of the <c>DiplomaticAgreement</c> class.
		/// </summary>
		/// <param name="parentCountry"></param>
		/// <param name="allyCountry"></param>
		protected DiplomaticAgreement(Country parentCountry, Country allyCountry)
		{
			_country1 = parentCountry;
			_country2 = allyCountry;
			_turnsLeft = 20;
		}

		/// <summary>
		/// The first country in the agreement.
		/// </summary>
		public Country Country1
		{
			get { return _country1; }
		}

		/// <summary>
		/// The second country in the agreement.
		/// </summary>
		public Country Country2
		{
			get { return _country2; }
		}

		/// <summary>
		/// The number of turns left in the agreement.
		/// </summary>
		public int TurnsLeft
		{
			get { return _turnsLeft; }
		}

		/// <summary>
		/// Gets or sets a bool indicating whether or not the 
		/// agreement can be peacefully canceled.
		/// </summary>
		public bool CanPeacefullyCancel
		{
			get { return ( _turnsLeft == 0 ); }
		}

		/// <summary>
		/// Calculates the value of the <see cref="DiplomaticAgreement"/> to the 
        /// specified <see cref="Country"/>
		/// </summary>
		/// <returns></returns>
		public virtual int CalculateValueForCountry(CountryBase country)
		{
            if (country == null)
                throw new ArgumentNullException("country");

            throw new NotImplementedException();
		}
	}
}
