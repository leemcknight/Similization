using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Collections.ObjectModel;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Controls
{
	/// <summary>
	/// Class representing a DirectX menu.
	/// </summary>
	public class DXMenu : DXContainerControl
	{
        private bool disposed;
        private Color highlightColor;
		private DXControlCollection menuItems;
		private const int MinimumWidth = 100;

        /// <summary>
        /// Intializes a new instance of the <see cref="DXMenu"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
		public DXMenu(IDirectXControlHost controlHost) : base(controlHost)
		{
			
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="DXMenu"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
		public DXMenu(IDirectXControlHost controlHost, DXControl parent) : base(controlHost, parent)
		{			
            this.menuItems = new DXControlCollection();
            this.menuItems.CollectionChanged += new CollectionChangeEventHandler(MenuItemsCollectionChanged);
            this.BackColor = Color.Tan;
            this.BackColor2 = Color.Tan;
            this.ForeColor = Color.White;
		}

        void MenuItemsCollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            int height = this.menuItems.Count * 32;
            this.Height = height;
            DXControl ctl = (DXControl)e.Element;
            if (e.Action == CollectionChangeAction.Add)
            {
                int width = CalcItemWidth(ctl.Text);
                if (width > this.Width)
                    this.Width = width;
                ctl.Location = new Point(0, this.Height - 32);
                ctl.Size = new Size(this.Width, 32);
                ctl.Parent = this;
                this.Controls.Add(ctl);                
            }
            else
            {
                this.Controls.Remove(ctl);                
            }
        }

        protected override void OnSizeChanged()
        {
            foreach (DXControl ctl in this.menuItems)
                ctl.Width = this.Width;
            base.OnSizeChanged();
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
                    if(this.menuItems != null)
                        foreach (DXMenuItem item in this.menuItems)
                            item.Dispose();
                }
                this.disposed = true;
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// The collection of <see cref="DXMenuItem"/> objects belonging to this <see cref="DXMenu"/>.
        /// </summary>
		public DXControlCollection MenuItems
		{
			get { return this.menuItems; }
		}

        /// <summary>
        /// The <i>Color</i> to use as the background of highlighted menu items.
        /// </summary>
        public Color HighlightColor
        {
            get { return this.highlightColor; }
            set { this.highlightColor = value; }
        }

        private int CalcItemWidth(string itemText)
        {
            int width = this.D3DFont.MeasureString(null, itemText, DrawStringFormat.Left, Color.White).Width;
            width += 40;
            return width;
        }
	}

	
}
