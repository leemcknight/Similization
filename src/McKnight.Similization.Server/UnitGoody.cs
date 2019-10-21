using System;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents a village "goody" that contains a new unit for the 
	/// country finding the village.
	/// </summary>
	public class UnitGoody : VillageGoody
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UnitGoody"/> class.
		/// </summary>
		/// <param name="tribeName"></param>
		/// <param name="newUnit"></param>
		public UnitGoody(string tribeName, Unit newUnit) : base(tribeName)
		{
			this.unit = newUnit;
		}

		/// <summary>
		/// Adds the unit goody to the finding country.
		/// </summary>
		/// <param name="goodyOwner"></param>
		public override void ApplyGoody(Country goodyOwner)
		{
			if(goodyOwner == null)
				throw new ArgumentNullException("goodyOwner");

			base.ApplyGoody(goodyOwner);
			goodyOwner.Units.Add(this.unit);
			this.unit.ParentCountry = goodyOwner;
		}

		private Unit unit;
		/// <summary>
		/// Gets the unit that was found in the village.
		/// </summary>
		public Unit Unit
		{
			get { return unit; }
		}
	}
}