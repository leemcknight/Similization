using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Controls
{
	/// <summary>
	/// Base class for combo boxes and list boxes.  Includes 
	/// functionality for dataSources and scrollbars.
	/// </summary>
	public abstract class DXListControl : DXControl
	{
        private bool disposed;
		private object dataSource;
        private string displayMember = string.Empty;		
		private event EventHandler selectedIndexChanged;
		private object selectedItem;
        private DXScrollBar horizontalScrollbar;
        private DXScrollBar verticalScrollbar;
		private ArrayList items = new ArrayList();
        private VertexBuffer highlightBuffer;
        private Color highlightColor = Color.LightBlue;
        private Color highlightColor2 = Color.MediumBlue;
        private Color highlightedTextColor = Color.White;
        private Color borderColor = Color.White;
        private Color highlightedBorderColor = Color.SteelBlue;

        /// <summary>
        /// Initializes a new instance of the <see cref="DXListControl"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
		protected DXListControl(IDirectXControlHost controlHost) 
            : base(controlHost)
		{
			
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="DXListControl"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
		protected DXListControl(IDirectXControlHost controlHost, DXControl parent) 
            : base(controlHost, parent)
		{
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
                    if (this.horizontalScrollbar != null)
                        this.horizontalScrollbar.Dispose();
                    if (this.verticalScrollbar != null)
                        this.verticalScrollbar.Dispose();
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
        /// The horizontal scroll bar for the <see cref="DXListControl"/>.
        /// </summary>
		protected DXScrollBar HorizontalScrollBar
		{
            get { return this.horizontalScrollbar; ; }
		}

        /// <summary>
        /// The vertical scroll bar for the <see cref="DXListControl"/>.
        /// </summary>
		protected DXScrollBar VerticalScrollBar
		{
			get { return this.verticalScrollbar; }
		}

        /// <summary>
        /// The source of data to databind to the <see cref="DXListControl"/>.
        /// </summary>
        /// <remarks>The <i>DataSource</i> must implement the <i>IList</i> interface.</remarks>
		public object DataSource
		{
			get { return this.dataSource; }
			set 
            {
                PushData(value);
                this.dataSource = value;                 
            }
		}

        /// <summary>
        /// Pushes data from the datasource into the <see cref="DXListControl"/>.
        /// </summary>
        /// <param name="data"></param>
        protected virtual void PushData(object data)
        {
            IList list;
            IBindingList bindingList = data as IBindingList;
            if(bindingList != null)
            {
                list = bindingList;
                bindingList.ListChanged += new ListChangedEventHandler(BindingListChanged);
            }
            else
            {
                list = data as IList;            
            }
            if (data == null)
                throw new ArgumentException(ControlResources.DataSourceMustImplementIList);
            foreach (object item in list)
                this.items.Add(item);
        }

        //Occurs when the bound list changes; we need to refresh the combobox.
        void BindingListChanged(object sender, ListChangedEventArgs e)
        {
            IBindingList bindingList = sender as IBindingList;
            this.items.Clear();
            foreach (object item in bindingList)
                this.items.Add(item);
        }

        /// <summary>
        /// The name of the property to pull the text from when displaying the
        /// items in the <see cref="DXComboBox"/>.
        /// </summary>
        public string DisplayMember
        {
            get { return this.displayMember; }
            set { this.displayMember = value; }
        }

        /// <summary>
        /// Gets the items in the <see cref="DXComboBox"/>
        /// </summary>
		public ArrayList Items
		{
			get { return this.items; }
		}

        /// <summary>
        /// The item that is currently selected in the <see cref="DXComboBox"/>.
        /// </summary>
		public object SelectedItem
		{
			get 
			{ 
				if(selectedItem == null && items.Count > 0)				
					selectedItem = this.items[0];				
				return selectedItem; 
			}

			set
			{
				if(items.Contains(value))				
					this.selectedItem = value;				
			}
		}

        /// <summary>
        /// The text of the selected item in the <see cref="DXComboBox"/>.
        /// </summary>
		public override string Text
		{
			get 
			{                
                return GetItemText(this.selectedItem);
			}

			set 
            {
                foreach (object item in this.items)
                {
                    if (item.ToString() == value)
                    {
                        this.selectedItem = item;
                        break;
                    }
                }
            }
		}

        /// <summary>
        /// The top color of the highlight gradient.
        /// </summary>
        public Color HighlightColor1
        {
            get { return this.highlightColor; }
            set { this.highlightColor = value; }
        }

        /// <summary>
        /// The bottom color of the highlight gradient.
        /// </summary>
        public Color HighlightColor2
        {
            get { return this.highlightColor2; }
            set { this.highlightColor2 = value; }
        }

        /// <summary>
        /// The color of the text of the items when they are highlighted.
        /// </summary>
        public Color HighlightedTextColor
        {
            get { return this.highlightedTextColor; }
            set { this.highlightedTextColor = value; }
        }

        /// <summary>
        /// The color used to draw the border of the <see cref="DXListControl"/>.
        /// </summary>
        public Color BorderColor
        {
            get { return this.borderColor; }
            set { this.borderColor = value; }
        }

        /// <summary>
        /// The color used to draw the border of the <see cref="DXListControl"/> when 
        /// it is focused or hovered.
        /// </summary>
        public Color HoveredBorderColor
        {
            get { return this.highlightedBorderColor; }
            set { this.highlightedBorderColor = value; }
        }

        /// <summary>
        /// Given an item in the <see cref="ListControl"/>, returns the proper 
        /// string representation.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string GetItemText(object item)
        {
            if (item == null)
                return string.Empty;            
            if (this.displayMember.Length == 0)
                return item.ToString();
            
            Type t = item.GetType();            
            PropertyInfo propInfo = t.GetProperty(this.displayMember);
            return propInfo.GetValue(item, null).ToString();
        }

        /// <summary>
        /// Occurs when the <i>SelectedIndex</i> property changes.
        /// </summary>
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				selectedIndexChanged += value; 
			}
			remove
			{
				selectedIndexChanged -= value; 
			}
		}

        /// <summary>
        /// Fires the <i>SelectedIndexChanged</i> event.
        /// </summary>
		protected virtual void OnSelectedIndexChanged()
		{
			if(this.selectedIndexChanged != null)			
				this.selectedIndexChanged(this, EventArgs.Empty);			
		}

        /// <summary>
        /// Renders all of the items in the <see cref="DXListControl"/>.
        /// </summary>
        /// <param name="destinationRectangle"></param>
        protected virtual void DrawItems(Rectangle destinationRectangle, object hotItem)
        {
            if (this.items.Count == 0)
                return;

            DXDrawItemEventArgs e;
            Rectangle bounds;            
            int index = 0;
            int yOffset = 0;            
            int itemHeight = this.D3DFont.MeasureString(null, this.items[0].ToString(), DrawStringFormat.Left, this.ForeColor).Size.Height;
            foreach (object item in this.Items)
            {
                if (yOffset + itemHeight > destinationRectangle.Height)
                    break;
                bounds = new Rectangle(destinationRectangle.X, destinationRectangle.Y + yOffset, Size.Width, itemHeight);                
                DrawItem(bounds, index, (hotItem == item));
                index++;
                yOffset += itemHeight;
            }
        }

        /// <summary>
        /// Draws a single item in the <see cref="DXListControl"/>.
        /// </summary>
        /// <param name="bounds"></param>
        protected virtual void DrawItem(Rectangle bounds, int itemIndex, bool highlight)
        {
            Color foreColor;

            if (highlight)
            {
                foreColor = this.highlightedTextColor;
                DrawHighlight(bounds);                
            }
            else
            {
                foreColor = this.ForeColor;
            }

            string text = GetItemText(this.Items[itemIndex]);
            this.D3DFont.DrawString(null, text, bounds, DrawStringFormat.Left, foreColor);
        }

        /// <summary>
        /// Draws the highlight over the currently selected item in the <see cref="DXListControl"/>.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void DrawHighlight(Rectangle bounds)
        {
            //create the vertex buffer for the highlight if necessary
            if (this.highlightBuffer == null)
                this.highlightBuffer = CustomPainters.CreateColoredBuffer(this.ControlHost.Device);

            //render the rectangle
            CustomPainters.PaintColoredRectangle(
                this.ControlHost.Device, 
                bounds, 
                this.highlightBuffer, 
                this.highlightColor, 
                this.highlightColor2, 
                GradientDirection.Vertical
                );
        }

        /// <summary>
        /// Gets the text of the currently selected item in the <see cref="DXListControl"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.selectedItem != null)
                return this.selectedItem.ToString();
            else
                return string.Empty;
        }
	}
}
