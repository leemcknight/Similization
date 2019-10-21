using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
    /// <summary>
    /// Class responsible for drawing <see cref="Unit"/> objects onto the screen.
    /// </summary>
    public class UnitPainter : GridCellLayerPainter
    {
        private Tileset tileset;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitPainter"/> class.
        /// </summary>
        /// <param name="tileset"></param>
        public UnitPainter(Tileset tileset)
        {
            this.tileset = tileset;
        }

        /// <summary>
        /// Paints any units that are on the current cell.
        /// </summary>
        public override void Paint()
        {
            if (this.GridCell.Units.Count == 0)
                return;

            Graphics g = this.Graphics;
            Unit unit = this.GridCell.Units[0];
            Image unitImage = this.tileset.UnitTiles[unit.Name];
            g.DrawImage(unitImage, this.Bounds);
        }
    }
}
