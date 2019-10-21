using System;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents a village "goody" that contains a friendly tribe
	/// that shares their maps of the region with the exploring country.
	/// </summary>
	public class MapGoody : VillageGoody
	{
		/// <summary>
		/// Initializes a new instance of the <c>MapGoody</c> class.
		/// </summary>
		/// <param name="tribeName"></param>
		public MapGoody(string tribeName) : base(tribeName)
		{
		
		}
	}
}