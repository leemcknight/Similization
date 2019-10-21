using System;
using System.Collections.Generic;
using System.Text;
using McKnight.Similization.Core;
using McKnight.Similization.Server;
using System.Drawing;
using System.Windows.Forms;

namespace McKnight.Similization.Client.WindowsForms
{
    /// <summary>
    /// Class responsible for painting a <see cref="Grid"/> onto the screen.
    /// </summary>
    public class GridPainter : Painter
    {
        private Point centerGridCoordinate;
        private Grid grid;
        private GridCellPainter cellPainter;        
        private Tileset tileset;
        private Control control;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GridPainter"/> class.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="grid"></param>
        /// <param name="tileset"></param>
        public GridPainter(Control control, Grid grid, Tileset tileset)
        {
            this.control = control;
            this.grid = grid;
            this.tileset = tileset;
            this.cellPainter = new GridCellPainter(tileset);
            this.Graphics = control.CreateGraphics();            
            Paint();
        }

        /// <summary>
        /// The coordinates of the <see cref="GridCell"/> that will be in the exact 
        /// middle of the screen.
        /// </summary>
        public Point CenterGridCoordinate
        {
            get { return this.centerGridCoordinate; }
            set { this.centerGridCoordinate = value; }
        }

        /// <summary>
        /// Paints the grid.
        /// </summary>
        public override void Paint()
        {
            this.cellPainter.Graphics = this.Graphics;
            this.Graphics.Clear(Color.Black);
            
            Point coords = new Point();
            int width = CalcWidthInCellUnits();
            int height = CalcHeightInCellUnits();
            Point startingGridCoordinate = GetFirstDrawnCoordinate();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //this is required for x map wrap around.  If 
                    //we're off the edge of the screen, we need to draw
                    //from the other side.
                    if (x + startingGridCoordinate.X >= grid.Size.Width)
                        coords.X = (x + startingGridCoordinate.X) - (grid.Size.Width);
                    else
                        coords.X = x + startingGridCoordinate.X;
                    coords.Y = y + startingGridCoordinate.Y;

                    //Draw the cell at the specified indicies    
                    Point location = TranslateGridCoordinatesToScreenCoordinates(coords);
                    Rectangle bounds = new Rectangle(location, this.tileset.TileSize);
                    this.cellPainter.Bounds = bounds;
                    this.cellPainter.GridCell = grid.GetCell(coords);
                    this.cellPainter.PaintOption = GridCellPaintOption.ExploredInhabited;
                    this.cellPainter.Paint();
                }
            }
        }

        /// <summary>
        /// Translates the given grid coordinates into pixel coordinates on the screen.
        /// </summary>
        /// <param name="gridCoordinates"></param>
        /// <returns></returns>
        public Point TranslateGridCoordinatesToScreenCoordinates(Point gridCoordinates)
        {
            //Translating a GridCell to a pixel location can be a little complicated.
            //We have to deal with map wrap-around in both directions on the x plane.
            //To achieve this, we must first determine if there is wrap around, and 
            //if so, in what direction.
            int xOffset;
            int yOffset = gridCoordinates.Y - this.centerGridCoordinate.Y;
            Point firstCoordinate = GetFirstDrawnCoordinate();
            //required for map wrap-around.
            if (gridCoordinates.X < firstCoordinate.X)
            {
                //how many x cells from the origin is this?
                xOffset = grid.Size.Width - firstCoordinate.X;
                xOffset += gridCoordinates.X;
            }
            else
            {
                //gridcell is to the right of the first cell.  we're ok.
                xOffset = gridCoordinates.X - firstCoordinate.X;
            }

            //number of pixels from the center point.
            int xPixelOffset = this.tileset.TileSize.Width * xOffset;
            int yPixelOffset = this.tileset.TileSize.Height * yOffset;

            int x = xPixelOffset;
            int y = CalcCenterScreenCoords().Y + yPixelOffset;
            Point translated = new Point(x, y);
            return translated;
        }

        /// <summary>
        /// Translates the screen coordinates to the coordinates of the <see cref="GridCell"/> 
        /// at those screen coordinates.
        /// </summary>
        /// <param name="screenCoordinates"></param>
        /// <returns></returns>
        public Point TranslateScreenCoordinatesToGridCoordinates(Point screenCoordinates)
        {
            Point center = CalcCenterScreenCoords();
            int xPixelOffset = screenCoordinates.X - center.X;
            int yPixelOffset = screenCoordinates.Y - center.Y;

            int xOffset = (xPixelOffset / this.tileset.TileSize.Width);
            int yOffset = (yPixelOffset / this.tileset.TileSize.Height);

            int x = this.centerGridCoordinate.X + xOffset;
            int y = this.centerGridCoordinate.Y + yOffset;

            if (x < 0 || y < 0 || x >= this.grid.Size.Width || y >= this.grid.Size.Height)
                return Point.Empty;
            return new Point(x, y);
        }

        private Point GetFirstDrawnCoordinate()
        {
            int height = CalcHeightInCellUnits();
            int width = CalcWidthInCellUnits();
            int x = this.centerGridCoordinate.X - (width / 2);
            int y = this.centerGridCoordinate.Y - (height / 2);
            if (x < 0)            
                x = (this.grid.Size.Width - 1) - Math.Abs(x);            
            y = y < 0 ? 0 : y;

            //the first cell is inside the bounds of the screen.
            return new Point(x, y);
        }


        private int CalcHeightInCellUnits()
        {
            int height = this.control.Bounds.Height;
            int cells = height / this.tileset.TileSize.Height;
            return cells;
        }

        private int CalcWidthInCellUnits()
        {
            int width = this.control.Bounds.Width;
            int cells = width / this.tileset.TileSize.Width;
            return cells;
        }

        private Point CalcCenterScreenCoords()
        {
            Rectangle r = this.control.Bounds;
            int hX = (int)r.Width / 2;
            int hY = (int)r.Height / 2;
            return new Point(r.Location.X + hX, r.Location.Y + hY);
        }
    }
}
