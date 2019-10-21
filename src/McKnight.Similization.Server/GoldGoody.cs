using System;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents a "goody" found in a village.  This goody is gold.
	/// </summary>
	public class GoldGoody : VillageGoody
	{
		
		/// <summary>
		/// Initializes a new instance of the <c>GoldGoody</c> class.
		/// </summary>
		/// <param name="tribeName"></param>
		/// <param name="gold"></param>
		public GoldGoody(string tribeName, int gold) : base(tribeName)
		{
			this.gold = gold;
		}

		private int gold;
		/// <summary>
		/// Gets the amount of gold found in the village.
		/// </summary>
		public int Gold
		{
			get { return this.gold; }
		}

		/// <summary>
		/// Applies the goody.
		/// </summary>
		/// <param name="goodyOwner"></param>
		public override void ApplyGoody(Country goodyOwner)
		{
			if(goodyOwner == null)
				throw new ArgumentNullException("goodyOwner");

			base.ApplyGoody(goodyOwner);
			goodyOwner.Gold += this.gold;
		}
	}
}