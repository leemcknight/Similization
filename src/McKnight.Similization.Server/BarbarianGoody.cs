using System;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents an unfriendly barbarian that was in the village.  
	/// </summary>
	public class BarbarianGoody : VillageGoody
	{
		/// <summary>
		/// Initializes a new instance of the <c>BarbarianGoody</c> class.
		/// </summary>
		/// <param name="tribeName"></param>
		/// <param name="enemy"></param>
		public BarbarianGoody(string tribeName, Barbarian enemy) : base(tribeName)
		{
			this.barbarian = enemy;
		}

		private Barbarian barbarian;

		/// <summary>
		/// Gets the barbarian that was unleashed.
		/// </summary>
		public Barbarian Barbarian
		{
			get { return this.barbarian; }
		}
	}
}