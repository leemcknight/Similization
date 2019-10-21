using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
    /// <summary>
    /// Class responsible for painting borders onto a <see cref="GridCell"/>.
    /// </summary>
    public class GridCellBorderPainter : GridCellLayerPainter
    {        
        private Grid grid;
        

        /// <summary>
        /// The <see cref="Grid"/> to paint the border onto.
        /// </summary>
        public Grid Grid
        {
            get { return this.grid; }
            set { this.grid = value; }
        }

   
        /// <summary>
        /// Paints the borders on the <see cref="GridCell"/> specified in the <i>GridCell</i> property.
        /// </summary>
        public override void Paint()
        {
            if (this.GridCell.Owner == null)
                return;

            CountryBase owner = this.GridCell.Owner;
            GridCell leftCell = this.grid.GetCell(new Point(this.GridCell.Coordinates.X - 1, this.GridCell.Coordinates.Y));
            GridCell topCell = this.grid.GetCell(new Point(this.GridCell.Coordinates.X, this.GridCell.Coordinates.Y - 1));
            GridCell bottomCell = this.grid.GetCell(new Point(this.GridCell.Coordinates.X, this.GridCell.Coordinates.Y + 1));
            GridCell rightCell = this.grid.GetCell(new Point(this.GridCell.Coordinates.X + 1, this.GridCell.Coordinates.Y));
            Pen pen = new Pen(Color.FromArgb(75, owner.Color), 3.0f);

            if (leftCell.Owner != this.GridCell.Owner)
                this.Graphics.DrawLine(pen, this.Bounds.Location, new Point(this.Bounds.Left, this.Bounds.Bottom));

            if (topCell != null && topCell.Owner != this.GridCell.Owner)
                this.Graphics.DrawLine(pen, this.Bounds.Location, new Point(this.Bounds.Right, this.Bounds.Top));

            if (bottomCell != null && bottomCell.Owner != this.GridCell.Owner)
                this.Graphics.DrawLine(pen, new Point(this.Bounds.Left, this.Bounds.Bottom - 3), new Point(this.Bounds.Right, this.Bounds.Bottom - 3));

            if (rightCell.Owner != this.GridCell.Owner)
                this.Graphics.DrawLine(pen, new Point(this.Bounds.Right - 3, this.Bounds.Top), new Point(this.Bounds.Right - 3, this.Bounds.Bottom));

        }
    }
}
