using System;
using System.Collections.Generic;
using System.Text;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
    /// <summary>
    /// Class responsible for painting <see cref="McKnight.Similization.Server.City"/> objects onto the screen.
    /// </summary>
    public class CityPainter : GridCellLayerPainter
    {
        private Tileset tileset;

        /// <summary>
        /// Initializes a new instance of the <see cref="CityPainter"/> class.
        /// </summary>
        /// <param name="tileset"></param>
        public CityPainter(Tileset tileset)
        {
            this.tileset = tileset;
        }

        /// <summary>
        /// Paints the city onto the screen.
        /// </summary>
        public override void Paint()
        {
            if (this.GridCell.City == null)
                return;
            City city = this.GridCell.City;
            CountryBase country = city.ParentCountry;
            Era era = country.Era;
            Civilization civilization = country.Civilization;
            CitySizeClass sizeClass = city.SizeClass;
            CityTile tile = this.tileset.CityTiles.GetTile(era, civilization, sizeClass);
            this.Graphics.DrawImage(tile.Image, this.Bounds);
        }
    }
}
