using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using McKnight.Similization.Client;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// Mini Map implementation for the Similization Windows Client.
	/// </summary>
	public class MiniMap : Control, IMiniMap
	{
		private Grid grid;
		private const int CellSize = 2;

		/// <summary>
		/// Initializes a new instance of the <c>MiniMap</c> control.
		/// </summary>
		public MiniMap()
		{
			SetStyle(
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint,
				true);


			this.UpdateStyles();
		}

		/// <summary>
		/// Draws the mini-map.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			
			if(this.grid != null)
			{
				DrawGrid(e.Graphics);
				DrawBoundsRect(e.Graphics);
				
			}
			else
			{
				
				g.FillRectangle(Brushes.Black, this.ClientRectangle);
				g.DrawString("Waiting for map...", this.Font, Brushes.White, 0, 10);
			}

			base.OnPaint(e);
		}

		/// <summary>
		/// Internal Handler for the <c>MouseUp</c> event.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if(this.grid != null)
			{

				if(e.Button == MouseButtons.Left)
				{
					ClientApplication client = ClientApplication.Instance;
                    client.GameWindow.CenterCoordinates = TranslateControlCoordinatesToGridCooordinates(new Point(e.X, e.Y));
				}
			}
			base.OnMouseUp(e);
		}

		private Point TranslateControlCoordinatesToGridCooordinates(Point point)
		{            
            int rem;
            int x = Math.DivRem(point.X, CellSize, out rem);
            int y = Math.DivRem(point.X, CellSize, out rem);
            return new Point(x, y);			
		}

		private void DrawGrid(Graphics g)
		{
			GridCell cell;
			int width = this.grid.Size.Width;
			int height = this.grid.Size.Height;
			for(int x = 0; x < width; x++)
			{
				for(int y = 0; y < height; y++)
				{
					cell = this.grid.GetCell(new Point(x,y));
					DrawCell(cell, g);
				}
			}
		}



		private static void DrawCell(GridCell cell, Graphics g)
		{
			Rectangle rect;
			Color color;
            ClientApplication client = ClientApplication.Instance;
			if(cell.HasBeenExploredBy(client.Player))
			{
				if(cell.City == null)
				{
					if(cell.Owner == null)
					{
						color = cell.IsDry ? Color.Green : Color.Blue;
					}
					else
					{
						color = cell.Owner.Color;
					}
				}
				else
				{
					color = Color.White;
				}
			}
			else
			{
				color = Color.Black;
			}

			rect = new Rectangle(new Point(cell.Coordinates.X * CellSize, 
				cell.Coordinates.Y * CellSize), new Size(CellSize,CellSize));

			g.FillRectangle(new SolidBrush(color), rect);	
		}


		//Draws the white rectangle showing the user the area of the map that is currently 
		//being displayed.  This is a little tricky in circumstances where the map 
		//"wraps-around", since technically they are viewing both the rightmost and 
		//leftmost areas of the map at the same time while not viewing the middle.
		private void DrawBoundsRect(Graphics g)
		{
			if(this.visibleBounds != Rectangle.Empty)
			{
				Pen pen = new Pen(Brushes.White, 1);				
				Point origin2 = new Point(this.visibleBounds.X * CellSize, this.visibleBounds.Y * CellSize);
				if(this.visibleBounds.Right > grid.Size.Width-1)
				{
					//off the end of the screen.  The rectangle will be broken into 
					//the "left parts" and "right parts" since we're viewing a wrapped map.

					int endY = origin2.Y + (CellSize * this.visibleBounds.Height);
					//This is the area for the right part of the viewed bounds.
					g.DrawLine(pen,origin2, new Point(origin2.X, this.visibleBounds.Bottom * CellSize));
					g.DrawLine(pen, origin2, new Point(CellSize * grid.Size.Width, origin2.Y));
					g.DrawLine(pen, origin2.X, endY , CellSize * grid.Size.Width,endY  );


					int leftEndX = (this.visibleBounds.Width * 2) - ( (CellSize * grid.Size.Width) - origin2.X );
					//This is the area for the left part of the viewed bounds.
					g.DrawLine(pen, 0, origin2.Y, leftEndX, origin2.Y);
					g.DrawLine(pen, 0, endY, leftEndX, endY);
					g.DrawLine(pen, leftEndX, origin2.Y, leftEndX, endY);

				}
				else
				{
					Rectangle rect = Rectangle.Empty;
					rect.Location = origin2;
					rect.Width = this.visibleBounds.Width * CellSize;
					rect.Height = this.visibleBounds.Height * CellSize;
					g.DrawRectangle(pen, rect);					
				}
			}
		}


		private event EventHandler<MiniMapClickedEventArgs> miniMapClicked;

		/// <summary>
		/// Event that fires whenever the mini-map is clicked.
		/// </summary>
		public event EventHandler<MiniMapClickedEventArgs> MiniMapClicked
		{
			add
			{
				this.miniMapClicked += value;
			}

			remove
			{
				this.miniMapClicked -= value; 
			}
		}

		/// <summary>
		/// Initializes the MiniMap with the grid.
		/// </summary>
		/// <param name="grid"></param>
		public void InitializeMap(Grid grid)
		{
			this.grid = grid;
			Invalidate();
		}

		private GridCell centerCell;

		/// <summary>
		/// Gets or sets the center cell.
		/// </summary>
		public GridCell CenterCell
		{
			get { return this.centerCell; }
			set { this.centerCell = value; }
		}

		private Rectangle visibleBounds;

		/// <summary>
		/// The <c>System.Drawing.Rectangle</c> structure describing the area of the map that the user is currently viewing.
		/// </summary>
		/// <remarks>The <c>IMiniMap</c> interface uses this to determine where to center and 
		/// how to draw the rectangle showing the user the are of the map they are viewing.</remarks>
		public Rectangle VisibleBounds
		{
			get { return this.visibleBounds; }
			set 
			{ 
				this.visibleBounds = value; 
				Invalidate();
			}
		}

		/// <summary>
		/// Fires the <i>MiniMapClicked</i> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMiniMapClicked(MiniMapClickedEventArgs e)
		{
			if(this.miniMapClicked != null)
			{
				this.miniMapClicked(this, e);
			}
		}

	}
}
