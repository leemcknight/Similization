using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Text.RegularExpressions;

namespace LJM.Similization.Client.DirectX.Controls
{
	/// <summary>
	/// A DirectX Scroll bar control.  Does both horizontal 
	/// and vertical scroll bars.
	/// </summary>
	public class DXScrollBar : DXControl
	{
		private int scrollValue;
		private int min;
		private int max;

        /// <summary>
        /// Initializes a new instance of the <see cref="DXScrollBar"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
		public DXScrollBar(IDirectXControlHost controlHost, DXControl parent) 
            : base(controlHost, parent)
		{
			
		}
		
        /// <summary>
        /// Gets the value of the scrollbar, indicating where it is between the minimum 
        /// and maximum allowed positions.
        /// </summary>
		public int Value
		{
			get { return this.scrollValue; }
			set { this.scrollValue = value; }
		}

        /// <summary>
        /// The minimum position of the scrollbar.  When <i>Value</i> is set to this, the scroll 
        /// bar will be at the topmost (for veritical) or leftmost (for horizontal) position.
        /// </summary>
		public int Minimum
		{
			get { return this.min; }
			set { this.min = value; }
		}

        /// <summary>
        /// The maximum position of the scrollbar.  When <i>Value</i> is set to this, the scroll
        /// bar will be at the bottommost (for vertical) or rightmost (for horizontal) position.
        /// </summary>
		public int Maximum
		{
			get { return this.max; }
			set { this.max = value; }
		}
	}
}
