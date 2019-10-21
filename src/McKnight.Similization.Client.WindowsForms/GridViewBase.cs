using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using McKnight.Similization.Client;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// Base class for all map viewing controls in the Windows Client.
	/// </summary>
	internal class GridViewBase : Control
	{				
        private Point centerCoordinates;
		private Grid grid;
		private Tileset tileset;
        private GridPainter painter;
        private Font cityNameFont;
        private Color cityNameFontColor;
        private event EventHandler centerCellChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="GridViewBase"/> class.
		/// </summary>
		public GridViewBase()
		{
			SetStyle(
				ControlStyles.UserPaint | 
				ControlStyles.AllPaintingInWmPaint | 
				ControlStyles.OptimizedDoubleBuffer,
				true);
            InitializePainter();
		}

        private void InitializePainter()
        {
            this.grid = ClientApplication.Instance.ServerInstance.Grid;
            this.tileset = ClientApplication.Instance.Tileset;            
            this.painter = new GridPainter(this, this.grid, this.tileset);
            this.CenterCoordinates = new Point(this.grid.Size.Width / 2, this.grid.Size.Height / 2);            
        }

        /// <summary>
        /// Gives an easy reference to the <see cref="McKnight.Similization.Server.Grid"/> object 
        /// currently loaded.
        /// </summary>
        protected Grid Grid
        {
            get { return this.grid; }
        }

        /// <summary>
        /// The <see cref="GridPainter"/> responsible for painting the <see cref="Grid"/> onto 
        /// the screen.
        /// </summary>
        public GridPainter GridPainter
        {
            get { return this.painter; }
        }

		/// <summary>
		/// The <see cref="Tileset"/> used in the game.
		/// </summary>
		protected Tileset Tileset
		{
			get 
			{ 
				if(this.tileset == null)
					this.tileset = ClientApplication.Instance.Tileset;
				return this.tileset; 
			}
		}

		/// <summary>
		/// The <c>System.Drawing.Font</c> to use when displaying City information.
		/// </summary>
		public Font CityNameFont
		{
			get { return this.cityNameFont; }
			set { this.cityNameFont = value; }
		}

		/// <summary>
		/// The <c>System.Drawing.Color</c> to use for the <c>CityNameFont</c> when 
		/// displaying the City information.
		/// </summary>
		public Color CityNameFontColor
		{
			get { return this.cityNameFontColor; }
			set { this.cityNameFontColor = value; }
		}

		/// <summary>
		/// Gets or sets the coordinates to draw in the center of the view.
		/// </summary>
		public Point CenterCoordinates
		{
			get { return this.centerCoordinates; }
			set
			{
				if(this.centerCoordinates != value)
				{
					this.centerCoordinates = value;
                    this.painter.CenterGridCoordinate = this.centerCoordinates;
					OnCenterCellChanged();
					Invalidate();
				}
			}
		}

		
		/// <summary>
		/// Occurs when the <c>CenterCell</c> property changes.
		/// </summary>
		/// <remarks>The <c>CenterCell</c> property can change for many different reasons, including:
		/// <list type="">
		/// <item>The user scrolls the map</item>
		/// <item>The user clicks a cell in the map</item>
		/// <item>The user selectes a different area in the <c>MiniMap</c></item>
		/// <item>The game changes the focused cell (i.e. to bring attention to something in the game).</item>
		/// </list></remarks>
		public event EventHandler CenterCellChanged
		{
			add
			{
				this.centerCellChanged += value; 
			}

			remove
			{
				this.centerCellChanged -= value; 
			}
		}

		/// <summary>
		/// Fires the <i>CenterCellChanged</i> event.
		/// </summary>
		protected virtual void OnCenterCellChanged()
		{
			if(this.centerCellChanged != null)
			{
				this.centerCellChanged(this, EventArgs.Empty);
			}
            Invalidate();
		}

		/// <summary>
		/// Gets a <c>Point</c> structure representing the center of the GameWindow.
		/// </summary>
		/// <returns></returns>
		protected Point CenterPoint
		{
            get
            {
                int x = ClientRectangle.Width / 2;
                int y = ClientRectangle.Height / 2;
                return new Point(x, y);
            }
		}
		
        /// <summary>
        /// Paints the Grid.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.painter.Graphics = e.Graphics;
            this.painter.Paint();
        }		
	}
}
