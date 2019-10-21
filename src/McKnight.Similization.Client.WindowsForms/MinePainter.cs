using System;
using System.Collections.Generic;
using System.Text;

namespace McKnight.Similization.Client.WindowsForms
{
    /// <summary>
    /// Class responsible for painting Mines onto the grid.
    /// </summary>
    public class MinePainter : GridCellLayerPainter
    {
        private Tileset tileset;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinePainter"/> class.
        /// </summary>
        /// <param name="tileset"></param>
        public MinePainter(Tileset tileset)
        {
            this.tileset = tileset;
        }

        /// <summary>
        /// Paints a Mine if there is one on the grid cell.
        /// </summary>
        public override void Paint()
        {
            if (!this.GridCell.HasMine)
                return;
            this.Graphics.DrawImage(this.tileset.CellImprovementTiles["Mine"], this.Bounds);
        }
    }
}
