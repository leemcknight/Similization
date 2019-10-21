using System;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents a Settler "goody".  This is a settler that has joined
	/// the country that found the village.
	/// </summary>
	public class SettlerGoody : VillageGoody
	{

		/// <summary>
		/// Initializes a new instance of the <c>SettlerGoody</c> class.
		/// </summary>
		/// <param name="tribeName"></param>
		/// <param name="newSettler"></param>
		public SettlerGoody(string tribeName, Settler newSettler) : base(tribeName)
		{
			this.settler = newSettler;
		}

		/// <summary>
		/// Adds the settler to the parent's units.
		/// </summary>
		/// <param name="goodyOwner"></param>
		public override void ApplyGoody(Country goodyOwner)
		{
			if(goodyOwner == null)
				throw new ArgumentNullException("goodyOwner");

			base.ApplyGoody(goodyOwner);
			this.settler.ParentCountry = goodyOwner;
			goodyOwner.Units.Add(this.settler);
		}

		private Settler settler;
		/// <summary>
		/// Gets the settler that joined the country
		/// from the village.
		/// </summary>
		public Settler Settler
		{
			get { return this.settler; }
		}
	}
}