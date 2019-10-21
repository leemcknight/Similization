using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace McKnight.Similization.Client.WindowsForms
{
    /// <summary>
    /// Class responsible for painting irrigation onto the screen.
    /// </summary>
    public class IrrigationPainter : GridCellLayerPainter
    {
        private Tileset tileset;

        /// <summary>
        /// Initializes a new instance of the <see cref="IrrigationPainter"/> class.
        /// </summary>
        /// <param name="tileset"></param>
        public IrrigationPainter(Tileset tileset)
        {
            this.tileset = tileset;
        }


        /// <summary>
        /// Paints the irrigation onto the screen.
        /// </summary>
        public override void Paint()
        {
            if (!this.GridCell.IsIrrigated)
                return;
            Image image = this.tileset.CellImprovementTiles["Irrigation"];
            this.Graphics.DrawImage(image, this.Bounds);
        }
    }
}
