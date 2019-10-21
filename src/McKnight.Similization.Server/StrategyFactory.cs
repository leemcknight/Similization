using System;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Creates and returns AI strategies for AI opponents.
	/// </summary>
	internal static class StrategyFactory
	{		
		/// <summary>
		/// Gets an appropriate strategy for the opponent.
		/// </summary>
		/// <param name="opponent"></param>
		/// <returns></returns>
		internal static AIStrategy GetStrategy(AICountry opponent)
		{
			//TODO: implement
			AIStrategy strategy = new ExpansionStrategy(opponent);
			return strategy;
		}
	}
}
