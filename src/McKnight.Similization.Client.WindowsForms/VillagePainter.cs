using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace McKnight.Similization.Client.WindowsForms
{
    /// <summary>
    /// Class responsible for painting <see cref="McKnight.Similization.Server.Village"/> objects onto the screen.
    /// </summary>
    public class VillagePainter : GridCellLayerPainter
    {
        private Tileset tileset;

        /// <summary>
        /// Initializes a new instance of the <see cref="VillagePainter"/> class.
        /// </summary>
        /// <param name="tileset"></param>
        public VillagePainter(Tileset tileset)
        {
            this.tileset = tileset;
        }

        /// <summary>
        /// Paints any villages on the cell.
        /// </summary>
        public override void Paint()
        {
            if (this.GridCell.Village == null)
                return;
            Image image = this.tileset.VillageTiles[0];
            this.Graphics.DrawImage(image, this.Bounds);
        }
    }
}
