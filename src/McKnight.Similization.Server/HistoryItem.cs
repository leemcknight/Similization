using System;
using System.Globalization;
using System.Xml;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents an individual event for a colony in the history of the game.
	/// </summary>
	public class HistoryItem
	{
		private int year;
        private int power;
        private Country country;
        private int culture;
        private int score;

		/// <summary>
		/// Gets or sets the year the event happened.
		/// </summary>
		public int Year
		{
			get { return this.year; }
			set { this.year = value; }
		}

		
		/// <summary>
		/// Gets or sets the Country the event happened for.
		/// </summary>
		public Country Country
		{
			get { return this.country; }
			set { this.country = value; }
		}

		
		/// <summary>
		/// Gets or sets the total number of Culture Points the 
		/// colony had AFTER the event took place.
		/// </summary>
		public int CulturePoints
		{
			get { return this.culture; }
			set { this.culture = value; }
		}

		
		/// <summary>
		/// Gets or sets the total number of Game points the 
		/// colony had AFTER the event took place.
		/// </summary>
		public int Score
		{
			get { return this.score; }
			set { this.score = value; }
		}

		
		/// <summary>
		/// Gets or sets how many "power" points (military strength)
		/// the colony had AFTER the event took place.
		/// </summary>
		public int Power
		{
			get { return this.power; }
			set { this.power = value; }
		}

		/// <summary>
		/// Saves the history item data to XML.
		/// </summary>
		/// <param name="writer">The <c>XmlWriter</c> to write this history item to.</param>
		public void Save(XmlWriter writer)
		{
			if(writer == null)
				throw new ArgumentNullException("writer");

			writer.WriteStartElement("HistoryItem");
			writer.WriteElementString("Year", this.year.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Country", this.country.Name);
			writer.WriteElementString("Culture", this.culture.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Score", this.score.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Power", this.power.ToString(CultureInfo.InvariantCulture));
			writer.WriteEndElement();
		}

		/// <summary>
		/// Loads the History Item.
		/// </summary>
		/// <param name="reader"></param>
		public void Load(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			string last = "";
			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "HistoryItem")
					break;
				if(reader.NodeType == XmlNodeType.Element)
					last = reader.Name;
				else if(reader.NodeType == XmlNodeType.Text)
				{
					switch(last)
					{
						case "Year":
							this.year = XmlConvert.ToInt32(reader.Value);
							break;
						case "Country":
							this.country = GameRoot.Instance.Countries[reader.Value];
							break;
						case "Culture":
							this.culture = XmlConvert.ToInt32(reader.Value);
							break;
						case "Score":
							this.score = XmlConvert.ToInt32(reader.Value);
							break;
						case "Power":
							this.power = XmlConvert.ToInt32(reader.Value);
							break;
					}
				}
			}
		}
	}
}