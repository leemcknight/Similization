using System;
using System.Drawing;
using System.Xml;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Villages are small tribes that can be found by different civilizations.
	/// When these villages are found, different things can happen.  For example,
	/// the people of the village may be happy and offer to join the civilization 
	/// or give them a technology or maps of their area.  Alternatively, the 
	/// tribe may contain barbarians who attack when encoutered.
	/// </summary>
	public class Village
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Village"/> object.
		/// </summary>
		public Village()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Village"/> object.
		/// </summary>
		public Village(string tribeName)
		{
			this.tribeName = tribeName;
		}

		private string tribeName;
		/// <summary>
		/// Gets or sets the name of the tribe the village belongs to.
		/// </summary>
		public string TribeName
		{
			get { return this.tribeName; }
			set { this.tribeName = value; }
		}
	
		/// <summary>
		/// Enters the village.
		/// </summary>
		/// <param name="discoveringUnit"></param>
		/// <returns>The <see cref="VillageGoody"/> awarded to the discovering country.</returns>
		public VillageGoody Discover(Unit discoveringUnit)
		{
			if(discoveringUnit == null)
				throw new ArgumentNullException("discoveringUnit");
			VillageGoody goody = GoodyFactory.GetGoody(this, discoveringUnit);
			goody.ApplyGoody(discoveringUnit.ParentCountry);
			return goody;
		}

		/// <summary>
		/// Saves the village information.
		/// </summary>
		/// <param name="writer"></param>
		public void Save(XmlWriter writer)
		{
			if(writer == null)
				throw new ArgumentNullException("writer");

			writer.WriteStartElement("Village");
			writer.WriteElementString("TribeName", this.tribeName);
			writer.WriteEndElement();
		}

		/// <summary>
		/// Loads the village information.
		/// </summary>
		/// <param name="reader"></param>
		public void Load(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");
			string last = "";
			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "Village")
					break;
				if(reader.NodeType == XmlNodeType.Element)
					last = reader.Name;
				else if(reader.NodeType == XmlNodeType.Text && last == "TribeName")
					this.tribeName = reader.Value;
			}
		}
		
	}
}
