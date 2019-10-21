using System;
using System.Collections.Generic;
using System.Text;

namespace McKnight.Similization.Client
{
    /// <summary>
    /// Class containing information about a single tile in a <see cref="Tileset"/>
    /// </summary>
    public class TileInfo
    {
        private string tileImagePath;
        private object tileImage;
        private string key;

        /// <summary>
        /// Initializes a new instance of the <see cref="TileInfo"/> class.
        /// </summary>
        public TileInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TileInfo"/> class.
        /// </summary>
        /// <param name="tileImagePath"></param>
        /// <param name="tileImage"></param>
        /// <param name="key"></param>
        public TileInfo(string tileImagePath, object tileImage, string key)
        {
            this.tileImagePath = tileImagePath;
            this.tileImage = tileImage;
            this.key = key;
        }

        /// <summary>
        /// The full path to the image for this <see cref="TileInfo"/>.
        /// </summary>
        public string TileImagePath
        {
            get { return this.tileImagePath; }
            set { this.tileImagePath = value; }
        }

        /// <summary>
        /// The image used for this <see cref="TileInfo"/>
        /// </summary>
        public object TileImage
        {
            get { return this.tileImage; }
            set { this.tileImage = value; }
        }

        /// <summary>
        /// The unique key identifying this tile.
        /// </summary>
        public string Key
        {
            get { return this.key; }
            set { this.key = value; }
        }
    }
}
