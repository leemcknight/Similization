using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Controls
{
	/// <summary>
	/// Class representing a DirectX window.
	/// </summary>
	public class DXWindow : DXContainerControl
	{		
		private Microsoft.DirectX.Direct3D.Font titleFont;
		private DXWindow childDialog;
        private VertexBuffer headerBuffer;
        private bool shadeHeader = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="DXWindow"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
		public DXWindow(IDirectXControlHost controlHost) 
            : base(controlHost)
		{
            this.Font = new System.Drawing.Font("Veranda", 24F, FontStyle.Bold);
            this.titleFont = new Microsoft.DirectX.Direct3D.Font(controlHost.Device, this.Font);
		}


        /// <summary>
        /// Determines whether the header of the window will be shaded when drawn.
        /// </summary>
        public bool ShadeHeader
        {
            get { return this.shadeHeader; }
            set { this.shadeHeader = value; }
        }

        /// <summary>
        /// Closes the window and disposes of the resources.
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// Renders the <see cref="DXWindow"/>.
        /// </summary>
		public override void Render(RenderEventArgs e)
		{
            base.Render(e);
			DrawTitleBar(e.ControlHost.Device);
            if (this.childDialog != null)
                this.childDialog.Render(e);
		}
		
		protected DXWindow ChildDialog
		{
			get { return this.childDialog; }
			set { this.childDialog = value; }
		}

		private void DrawTitleBar(Device device)
		{
			Point origin;
			origin = PointToScreen(new Point(10, 10));
            if(this.shadeHeader)
                DrawHeaderShade(device);
            this.titleFont.DrawString(null, this.Text, new Rectangle(origin, new Size(this.Size.Width, 50)), DrawStringFormat.Left, Color.White);			
		}

        private void DrawHeaderShade(Device device)
        {            
            if (this.headerBuffer == null)
                this.headerBuffer = CustomPainters.CreateColoredBuffer(device);
            Rectangle rect = new Rectangle(PointToScreen(Point.Empty), new Size(this.Width, 50));
            CustomPainters.PaintColoredRectangle(device, rect, this.headerBuffer, Color.DarkGoldenrod, Color.LightGoldenrodYellow, GradientDirection.Horizontal);            
        }
	}	
}
