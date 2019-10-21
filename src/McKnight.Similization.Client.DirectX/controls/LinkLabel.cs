using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Controls
{
    /// <summary>
    /// Class representing a link that the user can click on.
    /// </summary>
	public class DXLinkLabel : DXLabel
	{
        private Color hoverColor;
        /// <summary>
        /// Initializes a new instance of the <see cref="DXLinkLabel"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
		public DXLinkLabel(IDirectXControlHost controlHost, DXControl parent) 
            : base(controlHost, parent)
		{
			this.ForeColor = Color.Blue;
            this.hoverColor = Color.Red;
		}

        /// <summary>
        /// The <i>color</i> of the <see cref="DXLinkLabel"/> when the mouse is 
        /// hovered over the control.
        /// </summary>
        public Color HoverColor
        {
            get { return this.hoverColor; }
            set { this.hoverColor = value; }
        }

        /// <summary>
        /// Handles the movement of the mouse over the <see cref="DXLinkLabel"/>.
        /// </summary>
        /// <param name="mea"></param>
		protected override void OnMouseMove(DXMouseEventArgs mea)
		{			
			FontStyle style;
			style = FontStyle.Underline;
			if(this.Font.Bold)
				style |= FontStyle.Bold;
			if(this.Font.Italic)
				style |= FontStyle.Italic;
						
            mea.ControlHost.Device.SetCursor(Cursors.Hand.Handle, true);

			System.Drawing.Font newFont = 
				new System.Drawing.Font(this.Font.FontFamily, 
				this.Font.Size, style);
            
			this.Font = newFont;            
            base.OnMouseMove(mea);
		}

        /// <summary>
        /// Handles the mouse cursor leaving the <see cref="DXLinkLabel"/>.
        /// </summary>
		protected override void OnMouseLeave(DXMouseEventArgs e)
		{
			FontStyle style;
			style = FontStyle.Regular;
			if(this.Font.Bold)
				style |= FontStyle.Bold;
			if(this.Font.Italic)
				style |= FontStyle.Italic;

            e.ControlHost.Device.SetCursor(Cursors.Default.Handle, false);						
			
			System.Drawing.Font newFont = 
				new System.Drawing.Font(this.Font.FontFamily, 
				this.Font.Size, style);

			this.Font = newFont;
            base.OnMouseLeave(e);
		}

        /// <summary>
        /// Draws the text for the label onto the control.
        /// </summary>
        /// <param name="rectangle"></param>
        protected override void DrawLabelText(Rectangle rectangle)
        {
            Color textColor = this.Hovered ? this.hoverColor : this.ForeColor;
            this.D3DFont.DrawString(
                null,
                this.Text,
                rectangle,
                DrawStringFormat.SingleLine,
                textColor);
        }
       
	}
}
