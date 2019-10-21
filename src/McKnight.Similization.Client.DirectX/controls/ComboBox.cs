using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Controls
{
	/// <summary>
	/// Class representing a DirectX drop down combobox.
	/// </summary>
	public class DXComboBox : DXListControl
	{
		private object hotItem;
		private bool expanded;
        private bool disposed;     
		private int collapsedHeight = 20;		
		private static int MaxDropDownHeight = 100;
		private DXScrollBar scrollbar;                      
        private VertexBuffer shadeBuffer;
        private VertexBuffer pressedShadeBuffer;
        private VertexBuffer dropDownBuffer;
        private VertexBuffer arrowBuffer;
        private Color lightShade = Color.LightGray;
        private Color darkShade = Color.Gray; 

        /// <summary>
        /// Initializes a new instance of the <see cref="DXComboBox"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
		public DXComboBox(IDirectXControlHost controlHost) 
            : base(controlHost)
		{
            this.ControlHost.MouseActionPerformed += new EventHandler<DXMouseEventArgs>(ControlHost_MouseActionPerformed);
		}

       

        /// <summary>
        /// Initializes a new instance of the <see cref="DXComboBox"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
		public DXComboBox(IDirectXControlHost controlHost, DXControl parent) 
            : base(controlHost, parent)
		{            			
			this.BackColor = Color.White;
			this.ForeColor = Color.Black;
			this.scrollbar = new DXScrollBar(controlHost, parent);	
		    this.ControlHost.MouseActionPerformed += new EventHandler<DXMouseEventArgs>(ControlHost_MouseActionPerformed);
		}

        //have to directly listen to this event because the normal events from the base class 
        //don't include movement of the mouse within the control, only crossing boundaries.
        void ControlHost_MouseActionPerformed(object sender, DXMouseEventArgs e)
        {
            if (!this.Hovered)
                return;
            if (e.MouseAction != MouseAction.Move)
                return;

            object newHot;
            if (this.expanded)
            {
                newHot = GetItemAtPoint(new Point(e.MousePosition.X, e.MousePosition.Y));
                if (newHot != this.hotItem)
                    this.hotItem = newHot;
            }
            else
                this.hotItem = null;
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
                    if(this.scrollbar != null)
                        this.scrollbar.Dispose();
                    if (this.shadeBuffer != null)
                        this.shadeBuffer.Dispose();
                    if (this.pressedShadeBuffer != null)
                        this.pressedShadeBuffer.Dispose();
                    if (this.dropDownBuffer != null)
                        this.dropDownBuffer.Dispose();
                    if (this.arrowBuffer != null)
                        this.arrowBuffer.Dispose();
                }
                this.disposed = true;
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// Pushes data front the source into the control.
        /// </summary>
        /// <param name="data"></param>
        protected override void PushData(object data)
        {
            base.PushData(data);
            if (data == null)
                return;
            IList list = (IList)data;
            if (list.Count > 0)
                this.SelectedItem = list[0];
            else
                this.SelectedItem = null;
        }

        /// <summary>
        /// Handles the mouse button being clicked on the <see cref="DXComboBox"/>.
        /// </summary>
        /// <param name="mea"></param>
		protected override void OnMouseDown(DXMouseEventArgs mea)
		{
			object selected = GetItemAtPoint(new Point(mea.MousePosition.X, mea.MousePosition.Y));
			if(selected == null)
				this.expanded = !this.expanded;
			else
			{
				//they've clicked on an item.
				this.SelectedItem = selected;
				this.expanded = false;
			}
			if(this.expanded)			
				this.Height = CalcDropDownHeight() + this.collapsedHeight;                			
			else			
				this.Height = this.collapsedHeight;			
			base.OnMouseDown(mea);
		}

        private int CalcDropDownHeight()
        {
            Rectangle rect = this.D3DFont.MeasureString(null, this.Text, DrawStringFormat.SingleLine, this.ForeColor);
            int height = this.Items.Count * rect.Height;
            if (height > DXComboBox.MaxDropDownHeight)
                height = DXComboBox.MaxDropDownHeight;
            return height;
        }

        /// <summary>
        /// Handles the <see cref="DXComboBox"/> losing focus.
        /// </summary>
		protected override void OnLostFocus()
		{
			this.expanded = false;
			base.OnLostFocus();
		}

        //Gets the item in the combobox at the selected point.
		private object GetItemAtPoint(Point pt)
		{
			Point absPoint = PointToScreen(new Point(0, collapsedHeight));			
			int yDiff = pt.Y - absPoint.Y;

			if(yDiff < 0)			
				return null;
            int itemHeight = this.D3DFont.MeasureString(null, this.Text, DrawStringFormat.Left, this.ForeColor).Size.Height;

			int index = yDiff/itemHeight;
			index--; //account for zero based array
			if(index >= 0 && index < Items.Count)	//item at point?
				return Items[index];
			else			
				return null;			
		}

        /// <summary>
        /// Handles the size of the <see cref="DXComboBox"/> changing.
        /// </summary>
        protected override void OnSizeChanged()
        {
            if (this.collapsedHeight == 0)
                this.collapsedHeight = base.Size.Height;
            base.OnSizeChanged();
        }

        /// <summary>
        /// Renders the <see cref="DXComboBox"/>.
        /// </summary>
		public override void Render(RenderEventArgs e)
		{
            //have to recalc the size since we might be expanded.  In that case, 
            //we don't want the button to cover the entire size of the control, 
            //just the height as it is when collapsed.
            Size buttonSize = new Size(this.Width, this.collapsedHeight);     

            //create the rectangle for the button portion of the combobox
            Rectangle rect = new Rectangle(PointToScreen(Point.Empty), buttonSize);            

            //shade the rectangle
            ShadeButton(e.ControlHost.Device, rect);            


			if(this.expanded)			
				DrawDropDown(e.ControlHost);

            //draw the bounding rectangle around the button portion
            Color color = this.BorderColor;
            if (this.Hovered || this.Focused)
                color = this.HoveredBorderColor;
            CustomPainters.DrawBoundingRectangle(e.ControlHost, rect, color);

            //draw the arrow
            if (this.arrowBuffer == null)
                this.arrowBuffer = CustomPainters.CreateColoredTriangleBuffer(e.ControlHost.Device);
            Rectangle arrowRect = new Rectangle(this.Location.X + this.Size.Width - 20, this.Location.Y + 5, 15, collapsedHeight - 10);
            CustomPainters.PaintColoredTriangle(e.ControlHost.Device, arrowRect, this.arrowBuffer, color);

			this.D3DFont.DrawString(null, this.Text, rect, DrawStringFormat.Left, this.ForeColor);			
		}

        //Shades the button of the combobox.
        private void ShadeButton(Device device, Rectangle rectangle)
        {            
            if(this.shadeBuffer == null)
                this.shadeBuffer = CustomPainters.CreateColoredBuffer(device);
            if(this.pressedShadeBuffer == null)
                this.pressedShadeBuffer = CustomPainters.CreateColoredBuffer(device);
            
            if (this.expanded)
                CustomPainters.PaintColoredRectangle(device, rectangle, this.pressedShadeBuffer, this.darkShade, this.lightShade, GradientDirection.Vertical);
            else
                CustomPainters.PaintColoredRectangle(device, rectangle, this.shadeBuffer, this.lightShade, this.darkShade, GradientDirection.Vertical);                            
        }

        /// <summary>
        /// Handles the location of the <see cref="DXComboBox"/> being changed.
        /// </summary>
		protected override void OnLocationChanged()
		{
			this.scrollbar.Location = new Point(Location.X - this.scrollbar.Width, 
				Location.Y + this.collapsedHeight);            
			base.OnLocationChanged();
		}

		private void DrawDropDown(IDirectXControlHost controlHost)
		{			
            //draw the box            
            Point origin = PointToScreen(Point.Empty);
            int x = origin.X;
            int y = origin.Y;
            int bottom = origin.Y + this.collapsedHeight;
            int dropDownHeight = CalcDropDownHeight();
            Rectangle rect = new Rectangle(x, bottom, this.Width, dropDownHeight);
            if (this.dropDownBuffer == null)
                this.dropDownBuffer = CustomPainters.CreateColoredBuffer(controlHost.Device);
            CustomPainters.PaintColoredRectangle(controlHost.Device, rect, dropDownBuffer, this.BackColor);
            CustomPainters.DrawBoundingRectangle(controlHost, rect, Color.DimGray);                        
            DrawItems(rect, this.hotItem);			
		}
	}
}
