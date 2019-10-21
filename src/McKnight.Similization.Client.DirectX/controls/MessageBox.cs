using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Controls
{
    /// <summary>
    /// Class representing a DirectX MessageBox
    /// </summary>
    public class MessageBox : DXWindow
    {
        private bool disposed;
        private DXLabel label;
        private DXButton okButton;
        private VertexBuffer headerBuffer;
        private VertexBuffer backgroundBuffer;
        private Color headerColor1;
        private Color headerColor2;
        
        private MessageBox(IDirectXControlHost controlHost, string message) 
            : base(controlHost)
        {
            this.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label = new DXLabel(controlHost, this);
            this.label.Location = new Point(50, 50);
            this.label.Text = message;
            this.label.Size = new Size(425, 200);

            this.okButton = new DXButton(controlHost, this);
            this.okButton.Location = new Point(150, 255);
            this.okButton.Text = ControlResources.OkButtonText;
            this.okButton.Size = new Size(75, 25);
            this.okButton.Click += new EventHandler(OkButtonPressed);

            this.Controls.Add(this.label);
            this.Controls.Add(this.okButton);

            this.headerColor1 = Color.DarkGoldenrod;
            this.headerColor2 = Color.LightGoldenrodYellow;
        }

        /// <summary>
        /// Renders the <see cref="DXMessageBox"/>
        /// </summary>
        /// <param name="e"></param>
        public override void Render(RenderEventArgs e)
        {
            DrawHeader(e.ControlHost.Device);
            DrawBackGround(e.ControlHost);
        }

        protected override void OnSizeChanged()
        {
            base.OnSizeChanged();
            int space = this.Size.Width - this.okButton.Width;
            this.okButton.Location = new Point(space / 2, this.Height - this.okButton.Height - 5);
        }

        private void DrawBackGround(IDirectXControlHost controlHost)
        {            
            if (this.backgroundBuffer == null)
                this.backgroundBuffer = CustomPainters.CreateColoredBuffer(controlHost.Device);
            Point pt = PointToScreen(Point.Empty);
            Rectangle rect = new Rectangle(pt.X, pt.Y + 25, this.Width, this.Height - 25);
            CustomPainters.PaintColoredRectangle(controlHost.Device, rect, this.backgroundBuffer, this.BackColor, this.BackColor2, GradientDirection.Vertical);
            CustomPainters.DrawBoundingRectangle(controlHost, new Rectangle(pt, this.Size), Color.White);
        }

        private void DrawHeader(Device device)
        {            
            if (this.headerBuffer == null)
                this.headerBuffer = CustomPainters.CreateColoredBuffer(device);

            Point pt = PointToScreen(Point.Empty);
            Rectangle rect = new Rectangle(PointToScreen(Point.Empty), new Size(this.Width, 25));
            CustomPainters.PaintColoredRectangle(device, rect, this.headerBuffer, this.headerColor1, this.headerColor2, GradientDirection.Horizontal);                                    
            pt.Offset(3,3);
            this.D3DFont.DrawString(null, this.Text, pt, Color.White);            
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
                    if (this.okButton != null)
                        this.okButton.Dispose();
                    if (this.label != null)
                        this.label.Dispose();
                    if (this.headerBuffer != null)
                        this.headerBuffer.Dispose();                    
                }
            }
            finally
            {
                this.disposed = true;
                base.Dispose(disposing);
            }
        }

        private void OkButtonPressed(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Shows the <see cref="MessageBox"/>.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public static void Show(IDirectXControlHost controlHost, string message, string title)
        {
            //minimum size of a messagebox.            
            MessageBox m = new MessageBox(controlHost, message);            
            m.BackColor = Color.LightGray;
            m.BackColor2 = Color.Gray;
            m.Size = CalcSize(message, m.D3DFont);
            m.Location = new Point(300, 300);
            m.Text = title;
            m.Show();
        }

        private static Size CalcSize(string message, Microsoft.DirectX.Direct3D.Font font)
        {            
            Size size = font.MeasureString(null, message, DrawStringFormat.Left, Color.White).Size;
            int maxWidth = 500;
            if (size.Width <= maxWidth)
                return new Size(500, 200);
            else
            {
                int height = size.Height;
                int rem;
                int lines = Math.DivRem(size.Width, maxWidth, out rem);
                if (rem > 0)
                    lines++;
                height *= lines;
                return new Size(500, height + 50);

            }
        }
    }
}
