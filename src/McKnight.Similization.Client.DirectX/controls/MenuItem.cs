using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Controls
{
    /// <summary>
    /// Class representing a DirectX Menu Item.
    /// </summary>
	public class DXMenuItem : DXControl
	{
        private bool disposed;
		private DXImage icon;				
		private event EventHandler clicked;
        private VertexBuffer iconVertexBuffer;
        private VertexBuffer highlightBuffer;
        private DXMenu parentMenu;

        /// <summary>
        /// Initializes a new instance of the <see cref="DXMenuItem"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
		public DXMenuItem(IDirectXControlHost controlHost) 
            : base(controlHost)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="DXMenuItem"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
		public DXMenuItem(IDirectXControlHost controlHost, DXControl parent) 
            : base(controlHost, parent)
		{						
            this.parentMenu = (DXMenu)parent;
			this.BackColor = Color.DarkGray;
            this.BackColor2 = Color.DarkGray;
			this.ForeColor = Color.White;						
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
                    if (this.icon != null)
                        this.icon.Dispose();
                    if (this.iconVertexBuffer != null)
                        this.iconVertexBuffer.Dispose();
                    if (this.highlightBuffer != null)
                        this.highlightBuffer.Dispose();
                }
                this.disposed = true;
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// Handles the mouse being pressed down on the <see cref="DXMenuItem"/>.
        /// </summary>
        /// <param name="mea"></param>
		protected override void OnMouseDown(DXMouseEventArgs mea)
		{
            bool[] buttons = mea.MouseState.GetButtons();
			if(buttons[0])
			{
				if(this.clicked != null)
					this.clicked(this, EventArgs.Empty);
			}
			base.OnMouseDown(mea);
		}

        /// <summary>
        /// The <see cref="DXImage"/> to use for the icon of the <see cref="DXMenuItem"/>.
        /// </summary>
		public DXImage Icon
		{
			get { return this.icon; }
			set { this.icon = value; }
		}

        /// <summary>
        /// Occurs when the user selects the <see cref="DXMenuItem"/>.
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

		private void DrawIcon(Device device)
		{
			if(this.icon == null)			
				return;
            
            if (this.iconVertexBuffer == null)
                this.iconVertexBuffer = CustomPainters.CreateTexturedBuffer(device);
			
			Point start = PointToScreen(Point.Empty);
			Rectangle rect = new Rectangle(new Point(start.X + 3, start.Y + 3), new Size(24,24));
            CustomPainters.PaintTexturedRectangle(device, rect, this.iconVertexBuffer, this.icon.Texture);			
		}

        /// <summary>
        /// Renders the <see cref="DXMenuItem"/>.
        /// </summary>
		public override void Render(RenderEventArgs e)
		{
            base.Render(e);
			if(this.Hovered)			
				DrawHighlight(e.ControlHost.Device);			
			DrawIcon(e.ControlHost.Device);						
			Point origin = PointToScreen(Point.Empty);
			Point start = new Point(origin.X + 35, origin.Y + 10);
			Rectangle rect = new Rectangle(start, new Size(this.Width - 35, this.Height));
			D3DFont.DrawString(null, this.Text, rect,0, this.ForeColor);
		}

		private void DrawHighlight(Device device)
		{            
            if (this.highlightBuffer == null)
                this.highlightBuffer = CustomPainters.CreateColoredBuffer(device);
            Point origin = PointToScreen(Point.Empty);
            Rectangle rect = new Rectangle(origin.X + 2, origin.Y + 2, this.Width - 4, this.Height - 4);
            CustomPainters.PaintColoredRectangle(device, rect, this.highlightBuffer, this.parentMenu.HighlightColor);            
		}

	}
}
