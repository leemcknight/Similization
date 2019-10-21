using System;
using System.Collections;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents a strategy used by the AI.  This is the base class for
	/// all of the strategies that the AI uses.
	/// </summary>
	internal abstract class AIStrategy
	{

        private NamedObjectCollection<City> managedCities;

		/// <summary>
		/// Initializes a new instance of the <c>AIStrategy</c> class.
		/// </summary>
		protected AIStrategy()
		{
            managedCities = new NamedObjectCollection<City>();
		}
	
		/// <summary>
		/// Invokes the AI strategy on the Similization item
		/// </summary>
		internal virtual void Invoke(AICity foreignCity)
		{
			if(foreignCity == null)
				throw new ArgumentNullException("foreignCity");
			if(!this.managedCities.Contains(foreignCity))
			{
				this.managedCities.Add(foreignCity);
				foreignCity.ImprovementBuilt +=	new EventHandler<ImprovementBuiltEventArgs>(OnImprovementBuilt);
			}
		}

		/// <summary>
		/// Invokes the AI strategy on the Similization item
		/// </summary>
		internal virtual void Invoke(AIUnit foreignUnit)
		{
		}

		/// <summary>
		/// Event handler for the <i>TradeProposed</i> event of the foreign colony
		/// using the strategy.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void OnTradeProposed(object sender, TradeProposedEventArgs e)
		{
		}

		/// <summary>
		/// Event handler for the <c>ImprovementBuilt</c> event of the city belonging
		/// to the foreign colony using the strategy.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void OnImprovementBuilt(object sender, ImprovementBuiltEventArgs e)
		{
		}
	}
}
