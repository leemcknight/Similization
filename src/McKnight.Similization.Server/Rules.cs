namespace McKnight.Similization.Server
{
	using System;
	using System.Globalization;
	using System.Xml;

	/// <summary>
	/// Represents the rules of the game.
	/// </summary>
	public class Rules
	{
        private bool allowDominationVictory;
        private bool allowDiplomaticVictory;
        private bool allowCulturalVictory;
        private bool allowSpaceVictory;
        private bool allowMilitaryVictory;
        private bool allowCivilizationSpecificAbilities;

		/// <summary>
		/// Initializes a new instance of the <see cref="Rules"/> class.
		/// </summary>
		public Rules()
		{
			this.allowMilitaryVictory = true;
			this.allowCivilizationSpecificAbilities = true;
			this.allowSpaceVictory = true;
		}
				
		/// <summary>
		/// Gets or sets value indicating whether or not domination victories are allowed.
		/// </summary>
		public bool AllowDominationVictory
		{
			get { return this.allowDominationVictory; }
			set { this.allowDominationVictory = value; }
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether or not diplomatic victories are allowed.
		/// </summary>
		public bool AllowDiplomaticVictory
		{
			get { return this.allowDiplomaticVictory; }
			set { this.allowDiplomaticVictory = value; }
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether or not cultural victories are allowed.
		/// </summary>
		public bool AllowCulturalVictory
		{
			get { return this.allowCulturalVictory; }
			set { this.allowCulturalVictory = value; }
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether or not space victories are allowed.
		/// </summary>
		public bool AllowSpaceVictory
		{
			get { return this.allowSpaceVictory; }
			set { this.allowSpaceVictory = value; }
		}

		
		/// <summary>
		/// Gets or sets a value indicating whether or not military victories are allowed.
		/// </summary>
		public bool AllowMilitaryVictory
		{
			get { return this.allowMilitaryVictory; }
			set { this.allowMilitaryVictory = value; }
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether or not to allow civilization-specific abilities.
		/// </summary>
		public bool AllowCivilizationSpecificAbilities
		{
			get { return this.allowCivilizationSpecificAbilities; }
			set { this.allowCivilizationSpecificAbilities = value; }
		}

		/// <summary>
		/// Loads the rules from the XmlReader.
		/// </summary>
		/// <param name="reader"></param>
		public void Load(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			string last = "";
			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "Rules")
					break;

				if(reader.NodeType == XmlNodeType.Element)
					last = reader.Name;
				else if(reader.NodeType == XmlNodeType.Text)
				{
					switch(last)
					{
						case "AllowCivSpecificAbilities":
							this.allowCivilizationSpecificAbilities = Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture);
							break;
						case "AllowCulturalVictory":
							this.allowCulturalVictory = Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture);
							break;
						case "AllowDiplomaticVictory":
							this.AllowDiplomaticVictory = Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture);
							break;
						case "AllowDominationVictory":
							this.AllowDominationVictory = Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture);
							break;
						case "AllowSpaceVictory":
							this.AllowSpaceVictory = Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture);
							break;
					}
				}
			}
		}

		/// <summary>
		/// Saves the rules to the xml writer.
		/// </summary>
		/// <param name="writer">The <c>XmlWriter</c> to write the rules to.</param>
		public void Save(XmlWriter writer)
		{
			if(writer == null)
				throw new ArgumentNullException("writer");

			writer.WriteStartElement("Rules");
			writer.WriteElementString("AllowCivSpecificAbilities", this.allowCivilizationSpecificAbilities.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("AllowCulturalVictory", this.allowCulturalVictory.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("AllowDiplomaticVictory", this.allowDiplomaticVictory.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("AllowDominationVictory", this.allowDominationVictory.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("AllowMilitaryVictory", this.allowMilitaryVictory.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("AllowSpaceVictory", this.allowSpaceVictory.ToString(CultureInfo.InvariantCulture));
			writer.WriteEndElement();
		}

	}
}
