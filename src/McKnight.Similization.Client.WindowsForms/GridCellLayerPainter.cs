using System;
using System.Collections.Generic;
using System.Text;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
    public abstract class GridCellLayerPainter : Painter
    {
        private GridCell gridCell;

        /// <summary>
        /// The <see cref="GridCell"/> to paint the layer onto.
        /// </summary>
        public GridCell GridCell
        {
            get { return this.gridCell; }
            set { this.gridCell = value; }
        }
    }
}
