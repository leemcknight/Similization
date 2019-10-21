using System;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents a strategy for the AI where the AI will work on building 
	/// new buildings for their cities and improving the surrounding landscape.
	/// </summary>
	internal class BuildStrategy : AIStrategy
	{
		/// <summary>
		/// Initializes a new instance of the <c>BuildStrategy</c> class.
		/// </summary>
		/// <param name="foreignCountry"></param>
		internal BuildStrategy(AICountry foreignCountry)
		{
			if(foreignCountry == null)
				throw new ArgumentNullException("foreignCountry");
			foreignCountry.TradeProposed +=	new EventHandler<TradeProposedEventArgs>(OnTradeProposed);
		}

		/// <summary>
		/// Event handler for a trade proposed to the country using this strategy.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void OnTradeProposed(object sender, TradeProposedEventArgs e)
		{
			base.OnTradeProposed(sender, e);
		}

		/// <summary>
		/// Event handler for an improvement being built in a city belonging to the country
		/// using this strategy.
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
						unit.Mode = AIUnitMode.Explore;
						break;
					case AICityNeed.Culture:
						unit.Mode = AIUnitMode.Explore;
						break;
					case AICityNeed.Food:
						unit.Mode = AIUnitMode.Explore;
						break;
					case AICityNeed.UnitDefense:
						unit.Mode = AIUnitMode.Defend;
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
                        nextImprovement = UnitFactory.CreateWorker(city);					
					else					
						nextImprovement = city.GetBuildableItemForNeed(city.PrimaryNeed, typeof(Improvement));					
					break;
				case AICityNeed.Culture:
					nextImprovement = city.GetBuildableItemForNeed(city.PrimaryNeed,typeof(Improvement));
					break;
				case AICityNeed.Food:
					if(!city.HasWorkerInFields())											
                        nextImprovement = UnitFactory.CreateWorker(city);					
					else					
						nextImprovement = city.GetBuildableItemForNeed(city.PrimaryNeed, typeof(Improvement));					
					break;
				case AICityNeed.UnitDefense:
					nextImprovement = city.GetBuildableItemForNeed(city.PrimaryNeed, typeof(Unit));
					break;
			}

			city.NextImprovement = nextImprovement;
		}
	}
}
