using System;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents a strategy for the AI that will have the AI attempting
	/// to expand their empire as quickly as possible.
	/// </summary>
	internal class ExpansionStrategy : AIStrategy
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ExpansionStrategy"/> class.
		/// </summary>
		internal ExpansionStrategy(AICountry foreignCountry)
		{
			if(foreignCountry == null)
				throw new ArgumentNullException("foreignCountry");
			foreignCountry.TradeProposed += new EventHandler<TradeProposedEventArgs>(OnTradeProposed);
		}

		/// <summary>
		/// Event handler for the <i>TradeProposed</i> event of the foreign country
		/// that has the Expansion Strategy.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void OnTradeProposed(object sender, TradeProposedEventArgs e)
		{
			base.OnTradeProposed(sender, e);
		}

		/// <summary>
		/// Starts using this strategy for the city.
		/// </summary>
		/// <param name="foreignCity"></param>
		internal override void Invoke(AICity foreignCity)
		{
			base.Invoke(foreignCity);
		}

		/// <summary>
		/// Event handler for the <i>ImprovementBuilt</i> event of a city using this strategy.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void OnImprovementBuilt(object sender, ImprovementBuiltEventArgs e)
		{
			AICity city = e.City as AICity;
			
			BuildableItem nextImprovement = null;
			GameRoot root = GameRoot.Instance;
			
			//what to do with what we just built?
			if(e.Improvement.GetType() == typeof(AIUnit))
			{
				AIUnit unit = e.Improvement as AIUnit;

				switch(city.PrimaryNeed)
				{
					case AICityNeed.Commerce:
						
						break;
					case AICityNeed.Culture:
						
						break;
					case AICityNeed.Food:
						
						break;
					case AICityNeed.UnitDefense:
						unit.Fortified = true;
						break;
				}
				
			}
			else if(e.Improvement.GetType() == typeof(AISettler))
			{

			}
			else if(e.Improvement.GetType() == typeof(AIWorker))
			{
				AIWorker worker = e.Improvement as AIWorker;
				worker.ParentCity = e.City;
				worker.Automated = true;
			}


			//next item to build
			switch(city.PrimaryNeed)
			{
				case AICityNeed.Commerce:
					if(!city.HasWorkerInFields())
					{
						nextImprovement = UnitFactory.CreateWorker(city);
					}
					else
					{
						nextImprovement = 
							city.GetBuildableItemForNeed(city.PrimaryNeed, 
							typeof(Improvement));
					}
					break;
				case AICityNeed.Culture:
					nextImprovement =
						city.GetBuildableItemForNeed(city.PrimaryNeed,typeof(Improvement));
					break;

				case AICityNeed.Food:
					if(!city.HasWorkerInFields())
					{
						nextImprovement = UnitFactory.CreateWorker(city);
					}
					else
					{
						nextImprovement =
							city.GetBuildableItemForNeed(city.PrimaryNeed,
							typeof(Improvement));
					}
					break;
				case AICityNeed.UnitDefense:
					nextImprovement =
						city.GetBuildableItemForNeed(city.PrimaryNeed,
						typeof(Unit));
					break;
			}

			city.NextImprovement = nextImprovement;
		}
	}
}
