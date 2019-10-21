using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Text.RegularExpressions;

namespace LJM.Similization.Client.DirectX.Controls
{
	/// <summary>
	/// Class representing a DirectX Panel control.
	/// </summary>
	public class DXPanel : DXContainerControl
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="DXPanel"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
		public DXPanel(IDirectXControlHost controlHost, DXControl parent) 
            : base(controlHost, parent)
		{			
			InitializeComponent();
		}

		private void InitializeComponent()
		{									
			this.BackColor = Color.Tan;
            this.BackColor2 = Color.Tan;
			this.ForeColor = Color.Black;			
		}
	}
}
