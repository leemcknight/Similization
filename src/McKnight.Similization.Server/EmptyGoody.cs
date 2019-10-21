using System;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents and empty goody.  Villages that are abandoned will 
	/// have this as a goody. 
	/// </summary>
	public class EmptyGoody : VillageGoody
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EmptyGoody"/> class.
		/// </summary>
		/// <param name="tribeName"></param>
		public EmptyGoody(string tribeName) : base(tribeName)
		{
		}
	}
}