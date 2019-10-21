using System;
using System.Drawing;

namespace McKnight.Similization.Server
{


	/// <summary>
	/// Defines an interface for things that can be owned.
	/// </summary>
	public interface IOwnable
	{
		/// <summary>
		/// The colony that owns the item.
		/// </summary>
		Country ParentCountry { get; set; }
	}

}
