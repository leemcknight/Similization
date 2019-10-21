using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Controls
{
	/// <summary>
	/// Class representing a DirectX button control.
	/// </summary>
	public class DXButton : DXControl
	{
        private bool disposed;     
		private DXImage icon;		                
        private VertexBuffer iconVertexBuffer;        
        private event EventHandler buttonPressed;        

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
		public DXButton(IDirectXControlHost controlHost, DXControl parent) 
            : base(controlHost, parent)
		{            
			this.ForeColor = Color.White;
            this.BackColor = Color.LightGray;
            this.BackColor2 = Color.DarkSlateGray;
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
                    if(this.icon != null)
                        this.icon.Dispose();
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
		/// The icon to draw on the button
		/// </summary>
		public DXImage Icon
		{
			get { return this.icon; }
			set { this.icon = value; }
		}

        //Draws the icon for the button.
		private void DrawIcon(Device device)
		{
			if(this.icon == null)			
				return;			
						
			Point start = PointToScreen(Point.Empty);
			Rectangle rect = new Rectangle(new Point(start.X + 3, start.Y + 3), this.icon.Size);

            if (this.iconVertexBuffer == null)
                this.iconVertexBuffer = CustomPainters.CreateTexturedBuffer(device);
            CustomPainters.PaintTexturedRectangle(device, rect, this.iconVertexBuffer, this.icon.Texture);            			
		}

        /// <summary>
        /// Render the <see cref="DXButton"/> onto the screen.
        /// </summary>
		public override void Render(RenderEventArgs e)
		{
            base.Render(e);
			DrawIcon(e.ControlHost.Device);
            Rectangle rect = new Rectangle(PointToScreen(Point.Empty), this.Size);
            CustomPainters.DrawBoundingRectangle(e.ControlHost, rect, Color.White);            
			D3DFont.DrawString(null, this.Text, rect, DrawStringFormat.VerticalCenter | DrawStringFormat.Center, this.ForeColor);			    
		}
		
	}
}
