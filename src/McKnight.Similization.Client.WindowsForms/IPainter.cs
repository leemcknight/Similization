using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace McKnight.Similization.Client.WindowsForms
{
    /// <summary>
    /// Interface to a class that is responsible for painting a layer of the Grid.
    /// </summary>
    public interface IPainter
    {
        /// <summary>
        /// Paints the layer.
        /// </summary>
        void Paint();

        /// <summary>
        /// The <i>Graphics</i> object to paint onto.
        /// </summary>
        Graphics Graphics { get; set; }
    }
}
