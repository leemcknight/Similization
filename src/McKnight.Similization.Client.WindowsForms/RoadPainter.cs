using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
    /// <summary>
    /// Class responsible for painting a road onto the screen.
    /// </summary>
    public class RoadPainter : GridCellLayerPainter
    {
        private Tileset tileset;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoadPainter"/> class.
        /// </summary>
        /// <param name="tileset">The <see cref="Tileset"/> containing the images 
        /// used to show the roads.</param>
        public RoadPainter(Tileset tileset)
        {
            this.tileset = tileset;
        }

        /// <summary>
        /// Paints the roads onto the cell.
        /// </summary>
        public override void Paint()
        {
            if (!this.GridCell.HasRoad)
                return;
            Image image = GetRoadImage(this.GridCell);
            this.Graphics.DrawImage(image, this.Bounds);
        }     

        private Image GetRoadImage(GridCell cell)
        {
            Image roadImage = null;
            switch (cell.RoadLayout)
            {
                case GridCellItemDirection.None:
                    roadImage = this.tileset.CellImprovementTiles["road_None"];
                    break;
                case GridCellItemDirection.Bidirectional:
                    roadImage = this.tileset.CellImprovementTiles["road_BiDirectional"];
                    break;
                case GridCellItemDirection.EastWest:
                    roadImage = this.tileset.CellImprovementTiles["road_EastWest"];
                    break;
                case GridCellItemDirection.NorthSouth:
                    roadImage = this.tileset.CellImprovementTiles["road_NorthSouth"];
                    break;
                case GridCellItemDirection.SouthwestToNortheast:
                    roadImage = this.tileset.CellImprovementTiles["road_SouthWestToNorthEast"];
                    break;
            }

            return roadImage;
        }
    }
}
