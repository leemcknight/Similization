using System;
using System.Drawing;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// Different possible views of the histograph.
	/// </summary>
	public enum HistographView
	{
		/// <summary>
		/// Histograph by culture
		/// </summary>
		Culture,

		/// <summary>
		/// Histograph by power
		/// </summary>
		Power,

		/// <summary>
		/// Histograph by score
		/// </summary>
		Score
	}

	/// <summary>
	/// Different advisors in the game.
	/// </summary>
	public enum Advisor
	{
		/// <summary>
		/// Military Advisor.  Gives advice concerning 
		/// military strength of the players military 
		/// and the opponents military.
		/// </summary>
		MilitaryAdvisor,

		/// <summary>
		/// Cultural Advisor
		/// </summary>
		CulturalAdvisor,

		/// <summary>
		/// Foreign Advisor
		/// </summary>
		ForeignAdvisor,

		/// <summary>
		/// Trade Advisor
		/// </summary>
		TradeAdvisor,

		/// <summary>
		/// Domestic Advisor
		/// </summary>
		DomesticAdvisor,

		/// <summary>
		/// Science Advisor
		/// </summary>
		ScienceAdvisor
	}

	/// <summary>
	/// Possible values for a comparison between a specific aspect of 2 countries.
	/// </summary>
	public enum ComparisonRating
	{
		/// <summary>
		/// Worse than opponent
		/// </summary>
		Worse,

		/// <summary>
		/// Equal to opponent
		/// </summary>
		Equal,

		/// <summary>
		/// Better than opponent
		/// </summary>
		Better
	}

	
}
