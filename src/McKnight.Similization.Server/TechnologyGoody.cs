using System;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents a village "goody" that contains a new technology for the
	/// country finding the village.
	/// </summary>
	public class TechnologyGoody : VillageGoody
	{
		/// <summary>
		/// Initializes a new instance of the <c>TechnologyGoody</c> class.
		/// </summary>
		/// <param name="tribeName"></param>
		/// <param name="newTechnology"></param>
		public TechnologyGoody(string tribeName, Technology newTechnology) : base(tribeName)
		{
			this.technology = newTechnology;

		}

		/// <summary>
		/// Adds the technology to the country.
		/// </summary>
		public override void ApplyGoody(Country goodyOwner)
		{
			if(goodyOwner == null)
				throw new ArgumentNullException("goodyOwner");

			base.ApplyGoody(goodyOwner);
			goodyOwner.LearnTechnology(this.technology);
		}

		private Technology technology;

		/// <summary>
		/// Gets the technology that was found in the village.
		/// </summary>
		public Technology Technology
		{
			get { return this.technology; }
		}
	}
}