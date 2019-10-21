using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.DirectInput;

namespace LJM.Similization.Client.DirectX.Controls
{
	/// <summary>
	/// DirectX ListBox control.
	/// </summary>
	public class DXListBox : DXListControl
	{        
        /// <summary>
        /// Initializes a new instance of the <see cref="DXListBox"/> control.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
		public DXListBox(IDirectXControlHost controlHost, DXControl parent) 
            : base(controlHost, parent)
		{						
			this.BackColor = Color.White;
            this.BackColor2 = Color.LightGray;
			this.ForeColor = Color.Black;			
		}

		private object GetItemAtPoint(Point point)
		{
			Point translated = PointToScreen(Point.Empty);
            Rectangle textRect = this.D3DFont.MeasureString(null, "W", DrawStringFormat.SingleLine, this.ForeColor);
			int yOffset = (point.Y - translated.Y) / textRect.Height;
            if(yOffset >= 0)
			    if(yOffset < this.Items.Count)			
				    return this.Items[yOffset];			
			return null;
		}

        /// <summary>
        /// Handles the mouse button being held down over the <see cref="DXListBox"/>.
        /// </summary>
        /// <param name="e"></param>
		protected override void OnMouseDown(DXMouseEventArgs mea)
		{
			Point point = new Point(mea.MouseState.X, mea.MouseState.Y);
			object item = GetItemAtPoint(point);
			if(item != null && item != SelectedItem)
			{
				SelectedItem = item;
				OnSelectedIndexChanged();
			}

			base.OnMouseDown(mea);
		}

        /// <summary>
        /// Handles a key being held down while the <see cref="DXListBox"/> has focus.
        /// </summary>
        /// <param name="e"></param>
		protected override void OnKeyPress(DXKeyboardEventArgs e)
		{
			int index = 0;

			if(this.SelectedItem != null)			
				index = Items.IndexOf(this.SelectedItem);

            if (e.KeyboardState[Key.UpArrow] && index > 0)
            {
                index--;
                this.SelectedItem = this.Items[index];
                OnSelectedIndexChanged();
            }
            else if (e.KeyboardState[Key.DownArrow] && index < (this.Items.Count - 1))
            {
                index++;
                this.SelectedItem = this.Items[index];
                OnSelectedIndexChanged();
            }

			base.OnKeyPress(e);
		}

        /// <summary>
        /// Renders the <see cref="DXListBox"/>.
        /// </summary>
		public override void Render(RenderEventArgs e)
		{
            if (e == null)
                throw new ArgumentNullException("e");
            base.Render(e);
            Color color = this.BorderColor;
            if (this.Focused || this.Hovered)
                color = this.HoveredBorderColor;
            CustomPainters.DrawBoundingRectangle(e.ControlHost, this.ScreenBounds, color);
            DrawItems(this.ScreenBounds, this.SelectedItem);            
		}
	}
}
