namespace McKnight.Similization.Client
{
	using System;
	using System.Resources;
	using McKnight.Similization.Server;

	/// <summary>
	/// Class containg functions to generate advice that will be shown on the diplomatic negotiation dialog 
	/// in the game.
	/// </summary>
	public sealed class DiplomacyAdvice
	{
		private DiplomacyAdvice()
		{
		}

		/// <summary>
		/// Gets an array of strings containing tips to display on the negotiation dialog.
		/// </summary>
		/// <param name="tie">The <c>DiplomaticTie</c> that exists between the two negotiating parties.</param>
		/// <returns></returns>
		public static string[] GetAdvice(DiplomaticTie tie)
		{
			if(tie == null)
				throw new ArgumentNullException("tie");

			string[] tips = new string[3];

			InitializeResourceManager();			
			Country them = tie.ForeignCountry;

			ComparisonRating militaryRating = GetMilitaryRating(them);
			string militaryText = "";
			switch(militaryRating)
			{
				case ComparisonRating.Better:
					militaryText = _resMgr.GetString(DiplomacyAdviceStringKey.StrongerForces);
					break;
				case ComparisonRating.Equal:
					militaryText = _resMgr.GetString(DiplomacyAdviceStringKey.EqualForces);
					break;
				case ComparisonRating.Worse:
					militaryText = _resMgr.GetString(DiplomacyAdviceStringKey.WeakerForces);
					break;
			}

		
			ComparisonRating scienceRating = GetScienceRating(them);
			string scienceText1 = "";
			string scienctText2 = "";
			switch(scienceRating)
			{
				case ComparisonRating.Better:
					scienceText1 = _resMgr.GetString(DiplomacyAdviceStringKey.BetterTech1);
					scienctText2 = _resMgr.GetString(DiplomacyAdviceStringKey.BetterTech2);
					break;
				case ComparisonRating.Equal:
					scienceText1 = _resMgr.GetString(DiplomacyAdviceStringKey.EqualTech1);
					scienctText2 = _resMgr.GetString(DiplomacyAdviceStringKey.EqualTech2);
					break;
				case ComparisonRating.Worse:
					scienceText1 = _resMgr.GetString(DiplomacyAdviceStringKey.WorseTech1);
					scienctText2 = _resMgr.GetString(DiplomacyAdviceStringKey.WorseTech2);
					break;
			}

			tips[0] = FormatAdvice(militaryText, them);
			tips[1] = FormatAdvice(scienceText1, them);
			tips[2] = FormatAdvice(scienctText2, them);

			return tips;
		}

		private static string FormatAdvice(string advice, Country foreignCountry)
		{
			advice = advice.Replace("$CIVNOUN0",foreignCountry.Civilization.Noun);
			advice = advice.Replace("$CIVADJ0", foreignCountry.Civilization.Adjective);
			advice = advice.Replace("$LEADER0", foreignCountry.LeaderName);
			return advice;
		}

		private static ComparisonRating GetMilitaryRating(Country foreignCountry)
		{
			Country us = ClientApplication.Instance.Player;
			if(us.Units.Count > foreignCountry.Units.Count)
			{
				return ComparisonRating.Better;
			}
			else if(us.Units.Count < foreignCountry.Units.Count)
			{
				return ComparisonRating.Worse;
			}

			return ComparisonRating.Equal;
		}

		private static ComparisonRating GetScienceRating(Country foreignCountry)
		{
			Country us = ClientApplication.Instance.Player;

			if(us.AcquiredTechnologies.Count > foreignCountry.AcquiredTechnologies.Count)
			{
				return ComparisonRating.Better;
			}
			else if(us.AcquiredTechnologies.Count < foreignCountry.AcquiredTechnologies.Count)
			{
				return ComparisonRating.Worse;
			}

			return ComparisonRating.Equal;
		}

		private static ResourceManager _resMgr;

		private static void InitializeResourceManager()
		{
			if(_resMgr == null)
			{
				_resMgr = new ResourceManager(
					"McKnight.Similization.Client.AdvisorText", 
					typeof(ClientApplication).Assembly);
			}
		}
	}
}