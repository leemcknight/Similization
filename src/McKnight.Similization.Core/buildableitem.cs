using System;

namespace McKnight.Similization.Core
{
	/// <summary>
	/// Represents an item that can be built by a city.
	/// </summary>
	public abstract class BuildableItem : NamedObject
	{
		private int cost;

		/// <summary>
		/// The Cost (in gold) to build the item.
		/// </summary>
		public int Cost
		{
			get { return this.cost; }
			set { this.cost = value; }
		}
	}
}