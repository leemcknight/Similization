using System;

namespace McKnight.Similization.Core
{
	/// <summary>
	/// Represents a Similization wonder.  Wonders are special improvements that can
	/// affect the civilization as a whole, such as a space program, or a cure for 
	/// cancer.
	/// </summary>
	public class Wonder : BuildableItem
	{
		private bool smallWonder;
        private int cultureEffect;

		/// <summary>
		/// Gets or sets a value indicating if the Wonder is a "small" wonder.
		/// Small wonders can be built by any civilization, even if someone else
		/// has already built it.
		/// </summary>
		public bool IsSmallWonder
		{
			get { return this.smallWonder; }
			set { this.smallWonder = value; }
		}
		
		/// <summary>
		/// Gets or sets the culture effect this wonder has on the city.
		/// </summary>
		public int CultureEffect
		{
			get { return this.cultureEffect; }
			set { this.cultureEffect = value; }
		}
	}
}
