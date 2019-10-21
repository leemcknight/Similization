using System;
using System.Globalization;
using System.Xml;
using System.Drawing;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Class responsible for creating instance of <see cref="Country"/> classes 
    /// from xml metadata.
	/// </summary>
	public static class CountryFactory
	{		
		/// <summary>
		/// Gets the correct subclass of a <see cref="Country"/> object.
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static Country CreateCountry(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			Country c = null;
			bool ai = false;
			while(reader.Read())
			{
				if(reader.Name == "AI")
				{
					ai = Convert.ToBoolean(reader.ReadString(), CultureInfo.InvariantCulture);
					break;
				}
			}
			if(ai)			
				c = new AICountry();
			else
				c = new Country();
			c.Load(reader);
			return c;
		}

        /// <summary>
        /// Creates a <see cref="Country"/> with the specified parameters.
        /// </summary>
        /// <param name="civilization"></param>
        /// <param name="leaderName"></param>
        /// <param name="isAIPlayer"></param>
        /// <param name="playerColor"></param>
        /// <returns></returns>
        public static Country CreateCountry(Civilization civilization, string leaderName, bool isAIPlayer, Color playerColor)
        {
            if (civilization == null)
                throw new ArgumentNullException("civilization");

            GameRoot root = GameRoot.Instance;
            Country newCountry = null;
            GridCell randomCell = root.Grid.FindRandomDryCell();
            Point coordinates = randomCell.Coordinates;
            if (isAIPlayer)            
                newCountry = new AICountry(civilization, leaderName, coordinates);            
            else            
                newCountry = new Country(civilization, leaderName, coordinates);            
            newCountry.Color = playerColor;

            return newCountry;       
        }
	}
}
