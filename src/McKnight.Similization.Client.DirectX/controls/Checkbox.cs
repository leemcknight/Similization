using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Controls
{
	/// <summary>
	/// Class representing a DirectX Checkbox control.
	/// </summary>
	public class DXCheckBox : DXControl
	{
		private bool isChecked;
        private bool disposed;
		private event EventHandler clicked;		
        private DrawStringFormat textFormat;
        private VertexBuffer shadeBuffer;        

        /// <summary>
        /// Initialzes a new instance of the <see cref="DXCheckBox"/> control.
        /// </summary>
        /// <param name="controlHost"></param>
		public DXCheckBox(IDirectXControlHost controlHost) : base(controlHost)
		{

		}

        /// <summary>
        /// Initializes a new instance of the <see cref="DXCheckBox"/> control.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>        
		public DXCheckBox(IDirectXControlHost controlHost, DXControl parent) : base(controlHost, parent)
		{			
			this.ForeColor = SystemColors.ControlText;
            this.textFormat = DrawStringFormat.Left;			
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
                    if (this.shadeBuffer != null)
                        this.shadeBuffer.Dispose();
                }
                this.disposed = true;
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// Handles the mouse button being clicked on the <see cref="DXCheckBox"/> control.
        /// </summary>
        /// <param name="mea"></param>
		protected override void OnMouseDown(DXMouseEventArgs mea)
		{
            bool[] buttons = mea.MouseState.GetButtons();
			if(buttons[0])
			{
				this.isChecked = !this.isChecked;
				if(this.isChecked && this.clicked != null)
					this.clicked(this, EventArgs.Empty);
				base.OnMouseDown(mea);
			}
		}

        /// <summary>
        /// Determines whether the <see cref="DXCheckBox"/> is checked.
        /// </summary>
		public bool Checked
		{
			get { return this.isChecked; }
			set { this.isChecked = value; }
		}

        /// <summary>
        /// Occurs when the <see cref="DXCheckBox"/> is clicked.
        /// </summary>
        public event EventHandler Clicked
        {
            add
            {
                this.clicked += value;
            }

            remove
            {
                this.clicked -= value;
            }
        }

		/// <summary>
		/// Handles the text of the <see cref="DXCheckBox"/> changing.
		/// </summary>
        protected override void OnTextChanged()
        {
            Size size = this.D3DFont.MeasureString(null, this.Text, DrawStringFormat.SingleLine, this.ForeColor).Size;
            size.Width += 20;
            this.Size = size;            
            base.OnTextChanged();
        } 

        private void ShadeBox(Device device)
        {            
            if (this.shadeBuffer == null)
                this.shadeBuffer = CustomPainters.CreateColoredBuffer(device);
            Point start = PointToScreen(Point.Empty);
            Rectangle rect = new Rectangle(start.X, start.Y, 14, 14);
            CustomPainters.PaintColoredRectangle(device, rect, this.shadeBuffer, Color.Gray, Color.White, GradientDirection.Vertical);
        }

        /// <summary>
        /// Renders the <see cref="DXCheckBox"/>.
        /// </summary>
		public override void Render(RenderEventArgs e)
		{                      
            Rectangle boxRect = new Rectangle(PointToScreen(Point.Empty), new Size(14, 14));
            Rectangle textRect = new Rectangle(boxRect.X + 25, boxRect.Y, this.Size.Width - 25, this.Size.Height);
            ShadeBox(e.ControlHost.Device);
            if (this.isChecked)
            {
                Vector2[] checkedVerts = new Vector2[] {
                    new Vector2(boxRect.Right - 2, boxRect.Top + 2),
                    new Vector2(boxRect.Left + 5, boxRect.Bottom - 2),
                    new Vector2(boxRect.Left + 2, boxRect.Bottom - 6)
                };

                e.ControlHost.DrawLine(checkedVerts, Color.White);
            }
            CustomPainters.DrawBoundingRectangle(e.ControlHost, boxRect, Color.White);
			D3DFont.DrawString(null, this.Text, textRect, this.textFormat, this.ForeColor);			
		}
	}
}
