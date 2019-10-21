using System;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.Direct3D.CustomVertex;

namespace LJM.Similization.Client.DirectX.Controls
{
	/// <summary>
	/// Class representing a DirectX progress bar.
	/// </summary>
	public class DXProgressBar : DXControl
	{
		private int percentComplete;
        private bool disposed;
		private VertexBuffer vertexBuffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DXProgressBar"/> control.
        /// </summary>
        /// <param name="controlHost"></param>
		public DXProgressBar(IDirectXControlHost controlHost) : base(controlHost)
		{
            
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="DXProgreeBar"/> control.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
		public DXProgressBar(IDirectXControlHost controlHost, DXContainerControl parent) : base(controlHost, parent)
		{
            this.BackColor = Color.DarkGray;
            this.BackColor2 = Color.White;
		}

        /// <summary>
        /// Disposes of all resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed && disposing)
                {
                    if(this.vertexBuffer != null)
                        this.vertexBuffer.Dispose();
                }
                this.disposed = true;
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// Gets or sets the percentage complete of the operation using this progress bar.
        /// </summary>
		public int PercentComplete
		{
			get { return this.percentComplete; }
			set { this.percentComplete = value; }
		}

        private void UpdateVertexBuffer(Device device)
        {
            if (this.vertexBuffer == null)
                this.vertexBuffer = CustomPainters.CreateColoredBuffer(device);
            Point origin = PointToScreen(Point.Empty);
            float pct = ((float)this.percentComplete) / 100f;
            float x = pct * (float)this.Width;
            float y = this.Height;
            Rectangle rect = new Rectangle(origin, new Size((int)x, (int)y));                                                
            CustomPainters.PaintColoredRectangle(device, rect, this.vertexBuffer, this.ForeColor, Color.DarkSlateBlue, GradientDirection.Vertical);
        }

        /// <summary>
        /// Renders the <see cref="DXProgressBar"/>.
        /// </summary>
		public override void Render(RenderEventArgs e)
		{            
            if (this.vertexBuffer == null)
                return;            
            UpdateVertexBuffer(e.ControlHost.Device);
            base.Render(e);                        
            e.ControlHost.Device.VertexFormat = TransformedColored.Format;
            e.ControlHost.Device.SetStreamSource(0, this.vertexBuffer, 0);
            e.ControlHost.Device.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
		}
	}
}
