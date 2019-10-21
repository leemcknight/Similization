using System;
using System.Drawing;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents a city owned by an AI opponent.
	/// </summary>
	public class AICity : City
	{
		private AICityNeed primaryNeed = AICityNeed.UnitDefense;
		private int defenseNeed = 2;

        /// <summary>
        /// Initializes a new instance of the <see cref="AICity"/> class.
        /// </summary>
        public AICity()  : base()
        {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="AICity"/> class.
		/// </summary>
		/// <param name="coordinates"></param>
		/// <param name="parentCountry"></param>
		public AICity(Point coordinates, Country parentCountry) : base(coordinates, parentCountry)
		{
			AICountry parent = parentCountry as AICountry;
			parent.Strategy.Invoke(this);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AICity"/> class.  This constructor is
		///  useful when the programmer needs to convert a regular <see cref="City"/> to an 
		///  <see cref="AICity"/> (e.g. when an AI opponent captures a human players' city.).
		/// </summary>
		/// <param name="coordinates"></param>
		/// <param name="parentCountry"></param>
		/// <param name="capturedCity"></param>
		public AICity(Point coordinates, Country parentCountry, City capturedCity) : this(coordinates, parentCountry)
		{
			if(capturedCity == null)
				throw new ArgumentNullException("capturedCity");
			this.Population = capturedCity.Population;
			InitializeCityRadius(capturedCity.Radius);
		}

		/// <summary>
		/// Fires the <i>ImprovementBuilt</i> event.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnImprovementBuilt(ImprovementBuiltEventArgs e)
		{
			base.OnImprovementBuilt(e);
		}

		/// <summary>
		/// Gets the primary need of the city.
		/// </summary>
		public AICityNeed PrimaryNeed
		{
			get { return this.primaryNeed; }
		}

		/// <summary>
		/// Takes a turn for the AI City.
		/// </summary>
		public override void DoTurn()
		{
			base.DoTurn();
			RefreshPrimaryNeed();
		}

		private void RefreshPrimaryNeed()
		{
			if(IsLosingFood())
			{
				this.primaryNeed = AICityNeed.Food;
				return;
			}
			else if(CalcCityDefense() <= this.defenseNeed)
			{
				this.primaryNeed = AICityNeed.UnitDefense;
			}
			else if(RetrieveWorkItem() != null)
			{
				this.primaryNeed = AICityNeed.Commerce;
			}
			else
			{
				this.primaryNeed = AICityNeed.Culture;
			}
		}

	}
}
