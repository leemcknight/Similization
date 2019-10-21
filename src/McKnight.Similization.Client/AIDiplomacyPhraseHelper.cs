using System;
using System.Resources;
using System.Globalization;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{

	/// <summary>
	/// Helper class to manage phrases that the AI will say during 
	/// diplomacy.
	/// </summary>
	public static class AIDiplomacyPhraseHelper
	{		
		private static ResourceManager _resMgr;

		//GetForeignLeaderGreeting() will examine the relationship (diplomatic tie) that exists 
		//between the 2 countries and return an appropriate greeting spoken by the foreign country
		//to the player.  Basically, there 4 greeting types:
		//	* First Contact greeting
		//	* First Contact, and the foreign leader is offering a deal
		//  * Greeting when the foreign leader is demanding a tribute of some kind
		//	* "Greetings" when the 2 parties are at war.
		//	*  Greetings when the 2 parties have a current military alliance against a 3rd country
		//	*
		/// <summary>
		/// Gets the greeting that the foreign leader will say to the user.
		/// </summary>
		/// <param name="tie">The <see cref="DiplomaticTie"/> that exists between the players</param>
		/// <returns>The (culture-specific) text of the greeting.</returns>
		public static string GetForeignLeaderGreeting(DiplomaticTie tie)
		{
			if(tie == null)
				throw new ArgumentNullException("tie");

			string greeting = string.Empty;

			if(tie.DiplomaticState == DiplomaticState.War)
			{
				greeting = GetWarGreeting(tie);
			}
			else
			{
				bool hasAlliance = false;
				
				foreach(DiplomaticAgreement agreement in tie.DiplomaticAgreements)
				{
					if(agreement is MilitaryAlliance)
					{
						hasAlliance = true;
					}
				}

				if(hasAlliance)
				{
					greeting = GetAllianceGreeting(tie);
				}
				else
				{
					greeting = GetPeaceGreeting(tie);
				}
			}

			return greeting;
		}

		private static string GetWarGreeting(DiplomaticTie tie)
		{
			if(tie == null)
				throw new ArgumentNullException("tie");

			int index = RandomNumber.GetRandomNumber(144);
			index++; //make it 1 based.
			
			string key = "warGreeting" + index.ToString(CultureInfo.InvariantCulture);
			InitializeResourceManager();
			string phrase = _resMgr.GetString(key);

			Country me = ClientApplication.Instance.Player;
			Country you = tie.ForeignCountry;

			phrase = phrase.Replace("$PLAYER0", me.LeaderName);
			phrase = phrase.Replace("$CIVNAME1", me.Civilization.Name);
			phrase = phrase.Replace("$CIVADJ2", me.Civilization.Adjective);
			phrase = phrase.Replace("$CIVNOUN3", me.Civilization.Noun);
			phrase = phrase.Replace("$AI4", you.LeaderName);
			phrase = phrase.Replace("$CIVNAME5", you.Civilization.Name);
			phrase = phrase.Replace("$CIVADJ6", you.Civilization.Adjective);
			phrase = phrase.Replace("$CIVADJ7", you.Civilization.Noun);

			return phrase;
		}

		private static string GetPeaceGreeting(DiplomaticTie tie)
		{
			if(tie == null)
				throw new ArgumentNullException("tie");

			int index = RandomNumber.GetRandomNumber(47);
			index++; //make it 1 based.

			string key = "peaceGreeting" + index.ToString(CultureInfo.InvariantCulture);
			InitializeResourceManager();
			string phrase = _resMgr.GetString(key);
		
			Country me = ClientApplication.Instance.Player;
			Country you = tie.ForeignCountry;

			phrase = phrase.Replace("$PLAYER0", me.LeaderName);
			phrase = phrase.Replace("$CIVNAME1", me.Civilization.Name);
			phrase = phrase.Replace("$CIVADJ2", me.Civilization.Adjective);
			phrase = phrase.Replace("$AI3", you.LeaderName);
			phrase = phrase.Replace("$CIVNAME4", you.Civilization.Name);
			phrase = phrase.Replace("$CIVADJ5", you.Civilization.Adjective);
			phrase = phrase.Replace("$CIVADJ6", you.Civilization.Noun);

			return phrase;
		}

		private static string GetAllianceGreeting(DiplomaticTie tie)
		{
			if(tie == null)
				throw new ArgumentNullException("tie");

			int index = RandomNumber.GetRandomNumber(47);
			index++; //make it 1 based.

			string key = "allianceGreeting" + index.ToString(CultureInfo.InvariantCulture);
			InitializeResourceManager();
			string phrase = _resMgr.GetString(key);

			Country me = ClientApplication.Instance.Player;
			Country you = tie.ForeignCountry;

			//grab the correct alliance treaty from the tie.
			MilitaryAlliance alliance = null;
			foreach(DiplomaticAgreement agreement in tie.DiplomaticAgreements)
			{
                MilitaryAlliance militaryAlliance = agreement as MilitaryAlliance;
				if(militaryAlliance != null)
				{
                    alliance = militaryAlliance;
					break;
				}
			}
			phrase = phrase.Replace("$PLAYER0", me.LeaderName);
			phrase = phrase.Replace("$CIVNAME1", me.Civilization.Name);
			phrase = phrase.Replace("$CIVADJ2", me.Civilization.Adjective);
			phrase = phrase.Replace("$CIVNAME3", alliance.AllianceVictim.Civilization.Name);
			phrase = phrase.Replace("$CIVADJ4", alliance.AllianceVictim.Civilization.Adjective);
			phrase = phrase.Replace("CIVNOUN5", me.Civilization.Noun);
			phrase = phrase.Replace("$AI6", you.LeaderName);
			phrase = phrase.Replace("$CIVNAME7", you.Civilization.Name);
			phrase = phrase.Replace("$CIVADJ8", you.Civilization.Adjective);
			phrase = phrase.Replace("$CIVADJ9", you.Civilization.Noun);

			return phrase;
		}

		/// <summary>
		/// Gets the phrase that the Ai will speak during first contact.
		/// </summary>
		/// <param name="tie">The newly created diplomatic tie.</param>
		/// <returns>A <c>string</c> containing the spoken phrase.</returns>
		public static string GetFirstContactPhrase(DiplomaticTie tie)
		{
			if(tie == null)
				throw new ArgumentNullException("tie");

			int index = RandomNumber.GetRandomNumber(15);
			index++; //make it 1 based

			string key = "firstContact" + index.ToString(CultureInfo.InvariantCulture);

			InitializeResourceManager();
			string phrase = _resMgr.GetString(key);
			
			phrase = phrase.Replace("$AI0", tie.ForeignCountry.LeaderName);
			phrase = phrase.Replace("$CIVNAME1", tie.ForeignCountry.Civilization.Name);
			phrase = phrase.Replace("$CIVADJ2", tie.ForeignCountry.Civilization.Adjective);
			phrase = phrase.Replace("$CIVADJ3", tie.ForeignCountry.Civilization.Noun);

			return phrase;
		}

		/// <summary>
		/// Gets a phrase that the AI player will say when the human player backs out from proposing 
		/// a trade.
		/// </summary>
		/// <returns>The ai phrase.</returns>
		public static string GenerateBackOutResponse()
		{
			int index = RandomNumber.GetRandomNumber(42);
			InitializeResourceManager();
			string key = "whatever" + index.ToString(CultureInfo.InvariantCulture);
			string phrase = _resMgr.GetString(key);
			return phrase;
		}


		/// <summary>
		/// Gets a phrase that the AI player will say when a trade is initially proposed to them.
		/// </summary>
		/// <returns>The ai phrase.</returns>
		public static string GetProposalResponse(DiplomaticTie tie)
		{
			if(tie == null)
				throw new ArgumentNullException("tie");

			int index = RandomNumber.GetRandomNumber(22);
			index++; //make it 1 based

			string key = "proposalResponse" + index.ToString(CultureInfo.InvariantCulture);
			Country us = ClientApplication.Instance.Player;			
			InitializeResourceManager();
			string phrase = _resMgr.GetString(key);

			phrase = phrase.Replace("$PLAYER0", us.LeaderName);
			phrase = phrase.Replace("$CIVNAME1", us.Civilization.Name);
			phrase = phrase.Replace("$CIVADJ2", us.Civilization.Adjective);

			return phrase;
		}

		private static void InitializeResourceManager()
		{
			if(_resMgr == null)
			{
				_resMgr = new ResourceManager(
					"McKnight.Similization.Client.AiDiplomacyPhrases", 
					typeof(ClientApplication).Assembly);
			}
		}
	}
}
