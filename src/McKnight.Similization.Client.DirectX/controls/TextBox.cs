using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Text.RegularExpressions;

namespace LJM.Similization.Client.DirectX.Controls
{
	/// <summary>
	/// Class representing a DirectX TextBox.
	/// </summary>
	public class DXTextBox : DXControl
	{
		private int maxlength;

        /// <summary>
        /// Initializes a new instance of the <see cref="DXTextBox"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
		public DXTextBox(IDirectXControlHost controlHost) 
            : base(controlHost)
		{
            this.BackColor = Color.LightGray;
            this.BackColor2 = Color.DarkGray;
            this.ForeColor = Color.White;            
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="DXTextBox"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
		public DXTextBox(IDirectXControlHost controlHost, DXControl parent) 
            : base(controlHost, parent)
		{
            this.BackColor = Color.LightGray;
            this.BackColor2 = Color.DarkGray;
            this.ForeColor = Color.White;            
		}

        /// <summary>
        /// Gets or sets the maximum number of characters that can be in the <see cref="DXTextBox"/>.
        /// </summary>
		public int MaxLength
		{
			get { return this.maxlength; }
			set { this.maxlength = value; }
		}	

        /// <summary>
        /// Handles a key being pressed while the <see cref="DXTextBox"/> has focus.
        /// </summary>
        /// <param name="e"></param>
		protected override void OnKeyPress(DXKeyboardEventArgs e)
		{
            this.Text += e.Key.ToString();
			base.OnKeyPress(e);
		}

        /// <summary>
        /// Handles the mouse moving over the <see cref="DXTextBox"/>.
        /// </summary>
        /// <param name="mea"></param>
		protected override void OnMouseMove(DXMouseEventArgs mea)
		{
            mea.ControlHost.Device.SetCursor(Cursors.IBeam.Handle, true);            
			base.OnMouseMove(mea);
		}

        /// <summary>
        /// Handles the mouse leaving the <see cref="DXTextBox"/>
        /// </summary>
		protected override void OnMouseLeave(DXMouseEventArgs e)
		{			
            e.ControlHost.Device.SetCursor(Cursors.Default.Handle, true);
			base.OnMouseLeave(e);
		}

        /// <summary>
        /// Renders the <see cref="DXTextBox"/>.
        /// </summary>
		public override void Render(RenderEventArgs e)
		{
            base.Render(e);
            CustomPainters.DrawBoundingRectangle(e.ControlHost, this.ScreenBounds, Color.White);
			D3DFont.DrawString(null, this.Text, this.ScreenBounds, DrawStringFormat.Left, this.ForeColor);			
		}

	}
}
