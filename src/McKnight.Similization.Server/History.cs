using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Keeps a history of all major events in the game for all civilizations
	/// </summary>
	public class History
	{		
        private Queue<HistoryItem> historyQueue;

		/// <summary>
		/// Initializes a new instance of the <see cref="History"/> class.
		/// </summary>
		public History()
		{            
            this.historyQueue = new Queue<HistoryItem>();
		}

		/// <summary>
		/// Adds a history item to the History of the game.
		/// </summary>
		/// <param name="item"></param>
		public void AddHistoryItem(HistoryItem item)
		{
			this.historyQueue.Enqueue(item);
		}

		/// <summary>
		/// Gets the next history item from the History of the game.
		/// </summary>
		/// <returns></returns>
		public HistoryItem NextItem
		{
			get
			{
				HistoryItem item = null;

				if(this.historyQueue.Count > 0)
				{
					item = this.historyQueue.Dequeue() as HistoryItem;
				}

				return item;
			}
		}

		/// <summary>
		/// Gets a list of all the history items for a particular
		/// country.
		/// </summary>
		/// <param name="country"></param>
		/// <returns></returns>
		public HistoryItem[] GetHistoryFor(Country country)
		{
			if(country == null)
				throw new ArgumentNullException("country");

			HistoryItem[] items = this.historyQueue.ToArray();            
			int count = 0;

			foreach(HistoryItem item in items)
			{
				if(item.Country == country)
				{
					count++;
				}
			}

			HistoryItem[] historyItems = new HistoryItem[count];

			int index = 0;
			foreach(HistoryItem item in items)
			{
				if(item.Country == country)
				{
					historyItems[index++] = item;
				}
			}

			return historyItems;
		}

		/// <summary>
		/// Gets all the history items in the History class.
		/// </summary>
		/// <returns></returns>
		public HistoryItem[] GetAllHistory()
		{			
			return this.historyQueue.ToArray();
		}

		/// <summary>
		/// Gets the total number of history items for all colonies in the game.
		/// </summary>
		public int HistoryItemCount
		{
			get { return this.historyQueue.Count; }
		}

		/// <summary>
		/// Saves the History to the the xml stream.
		/// </summary>
		/// <param name="writer"></param>
		public void Save(XmlWriter writer)
		{
			if(writer == null)
				throw new ArgumentNullException("writer");

			object[] items;
			items = this.historyQueue.ToArray();
			HistoryItem item;
			writer.WriteStartElement("History");
			for(int i = 0; i <= items.GetUpperBound(0); i++)
			{
				item = (HistoryItem)items[i];
				item.Save(writer);
			}
			writer.WriteEndElement();
		}

		/// <summary>
		/// Loads the history.
		/// </summary>
		/// <param name="reader"></param>
		public void Load(XmlReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			while(reader.Read())
			{
				if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "History")
					break;

				if(reader.NodeType == XmlNodeType.Element && reader.Name == "HistoryItem")
				{
					HistoryItem item = new HistoryItem();
					item.Load(reader);
					this.historyQueue.Enqueue(item);
				}
			}
		}
	}

	
}
