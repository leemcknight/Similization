using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using McKnight.Similization.Core;

namespace McKnight.Similization.Client.WindowsForms
{
    /// <summary>
    /// Class responsible for painting a <see cref="McKnight.Similization.Core.Terrain"/> onto the screen.
    /// </summary>
    public class TerrainPainter : GridCellBorderPainter
    {
        private Tileset tileset;                

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainPainter"/> class.
        /// </summary>
        /// <param name="tileset"></param>
        public TerrainPainter(Tileset tileset)
        {
            this.tileset = tileset;
        }

        /// <summary>
        /// Paints the <see cref="Terrain"/>.
        /// </summary>
        public override void Paint()
        {
            Terrain terrain = this.GridCell.Terrain;
            Image image = (Image)this.tileset.TerrainTiles[terrain.Name].TileImage;
            this.Graphics.DrawImage(image, this.Bounds);
        }      
    }
}
