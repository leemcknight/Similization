using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.Generic;
using Microsoft.DirectX.Direct3D.CustomVertex;

namespace LJM.Similization.Client.DirectX.Controls
{
    /// <summary>
    /// Text Aligment options for different <see cref="DXControl"/> objects.
    /// </summary>
	public enum TextAlign
	{
        /// <summary>
        /// Align the text on the right.  The text will end on the rightmost 
        /// portion of the control.
        /// </summary>       
		Right,

        /// <summary>
        /// Align the text on the left.  The text will start on the leftmost 
        /// portion of the control.
        /// </summary>
		Left,

        /// <summary>
        /// Align the text in the center of the control.  
        /// </summary>
		Center
	}

	/// <summary>
	/// Represents a DirecX text label.
	/// </summary>
	public class DXLabel : DXControl
	{
		private DXImage image;
		private TextAlign alignment;
		private bool autoSize;
        private bool disposed;        
        private Rectangle textRectangle;
        private DrawStringFormat textFormat;
        private VertexBuffer iconVertexBuffer;
	
        /// <summary>
        /// Initializes a new instance of the <see cref="DXLabel"/> class.
        /// </summary>
		public DXLabel(IDirectXControlHost controlHost) 
            : base(controlHost)
		{

		}

        /// <summary>
        /// Initializes a new instance of the <see cref="DXLabel"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
		public DXLabel(IDirectXControlHost controlHost, DXControl parent) 
            : base(controlHost, parent)
		{            			
			this.ForeColor = Color.White;			
			this.TextAlignment = TextAlign.Left;			            
		}

        /// <summary>
        /// Releases all resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed && disposing)
                {
                    if (this.image != null)
                        this.image.Dispose();
                    if (this.iconVertexBuffer != null)
                        this.iconVertexBuffer.Dispose();                    
                }
                this.disposed = true;
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// Determines whether the <see cref="DXLabel"/> should automatically 
        /// size itself to accomodate the text being displayed.
        /// </summary>
		public bool AutoSize
		{
			get { return this.autoSize; }
			set 
            { 
                this.autoSize = value;                
                if (this.autoSize)
                {
                    Size size;
                    Rectangle rect = this.D3DFont.MeasureString(null, this.Text, this.textFormat, this.ForeColor);
                    size = new Size(rect.Width, rect.Height);
                    if (this.image != null)
                        size.Width += this.image.Size.Width;
                    this.Size = size;
                }
                CalcTextRectangle();                
            }
		}

        /// <summary>
        /// Determines how the text will be aligned on the <see cref="DXLabel"/>.
        /// </summary>
		public TextAlign TextAlignment
		{
			get { return this.alignment; }
			set 
            { 
                this.alignment = value;
                this.textFormat = DrawStringFormat.WordBreak;
                switch (this.alignment)
                {
                    case TextAlign.Center:
                        this.textFormat |= DrawStringFormat.Center;
                        break;
                    case TextAlign.Left:
                        this.textFormat |= DrawStringFormat.Left;
                        break;
                    case TextAlign.Right:
                        this.textFormat |= DrawStringFormat.Right;
                        break;
                }
            }
		}

        /// <summary>
        /// The <see cref="DXImage"/> to draw next to the text on the <see cref="DXLabel"/>.
        /// </summary>
		public DXImage Icon
		{
			get { return image; }
			set { image = value; }
		}

        //Draws the icon for the label.
		private void DrawIcon()
		{
			if(this.image == null)			
				return;						            
			Rectangle rect = new Rectangle(PointToScreen(Point.Empty), this.image.Size);
            if (this.iconVertexBuffer == null)
                this.iconVertexBuffer = CustomPainters.CreateTexturedBuffer(this.ControlHost.Device);            
            CustomPainters.PaintTexturedRectangle(this.ControlHost.Device, rect, this.iconVertexBuffer, image.Texture);									
		}      

        /// <summary>
        /// Fires the <i>TextChanged</i> event.
        /// </summary>
        protected override void OnTextChanged()
        {
            if (this.autoSize)
            {
                Size size;
                Rectangle rect = this.D3DFont.MeasureString(null, this.Text, this.textFormat, this.ForeColor);
                size = new Size(rect.Width, rect.Height);
                if (this.image != null)
                    size.Width += this.image.Size.Width;
                this.Size = size;
            }            
            base.OnTextChanged();
        }

        /// <summary>
        /// Fires the <i>FontChanged</i> event.
        /// </summary>
        protected override void OnFontChanged()
        {
            if (this.autoSize)
            {
                Size size;
                Rectangle rect = this.D3DFont.MeasureString(null, this.Text, this.textFormat, this.ForeColor);
                size = new Size(rect.Width, rect.Height);
                if (this.image != null)
                    size.Width += this.image.Size.Width;
                this.Size = size;
            }            
            base.OnFontChanged();
        }

        /// <summary>
        /// Renders the <see cref="DXLabel"/> onto the screen.
        /// </summary>
		public override void Render(RenderEventArgs e)
		{            
            DrawIcon();            
            CalcTextRectangle();
            DrawLabelText(this.textRectangle);			
		}

        /// <summary>
        /// Draws the text for the label onto the control.
        /// </summary>
        /// <param name="rectangle"></param>
        protected virtual void DrawLabelText(Rectangle rectangle)
        {
            this.D3DFont.DrawString(
                null, 
                this.Text, 
                rectangle, 
                this.textFormat, 
                this.ForeColor);            
        }

        private void CalcTextRectangle()
        {
            Point origin = PointToScreen(Point.Empty);
            if (this.image != null)
            {
                Rectangle textRect = this.D3DFont.MeasureString(null, this.Text, this.textFormat, this.ForeColor);
                int x = origin.X + this.image.Size.Width + 5;
                int y = origin.Y;
                Point textOrigin = new Point(x, y);
                this.textRectangle = new Rectangle(textOrigin, textRect.Size);                
            }
            else
            {
                this.textRectangle = new Rectangle(origin, this.Size);
            }
        }

	}
}
