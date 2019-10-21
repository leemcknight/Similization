using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace McKnight.Similization.Client.WindowsForms
{
    /// <summary>
    /// Class responsible for painting <see cref="McKnight.Similization.Core.Resource"/> objects onto the screen.
    /// </summary>
    public class ResourcePainter : GridCellLayerPainter
    {
        private Tileset tileset;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcePainter"/> class.
        /// </summary>
        /// <param name="tileset"></param>
        public ResourcePainter(Tileset tileset)
        {
            this.tileset = tileset;
        }

        /// <summary>
        /// Paints any resources on the cell.
        /// </summary>
        public override void Paint()
        {
            if (this.GridCell.Resource == null)
                return;
            Image image = this.tileset.ResourceTiles[this.GridCell.Resource.Name];
            this.Graphics.DrawImage(image, this.Bounds);
        }
    }
}
