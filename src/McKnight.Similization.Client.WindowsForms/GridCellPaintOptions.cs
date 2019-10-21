using System;
using System.Collections.Generic;
using System.Text;

namespace McKnight.Similization.Client.WindowsForms
{
    /// <summary>
    /// Determines how visible a <see cref="McKnight.Similization.Server.GridCell"/> is.  This will affect 
    /// how it is drawn on the screen.
    /// </summary>
    public enum GridCellPaintOption
    {
        /// <summary>
        /// The cell has not been explored at all.
        /// </summary>
        UnexploredCell,

        /// <summary>
        /// The cell has been expolored, but there is no unit currently 
        /// within viewing distance of it.  
        /// </summary>
        ExploredUninhabited,

        /// <summary>
        /// The cell is visible by a least 1 unit on the screen.
        /// </summary>
        ExploredInhabited
    }
}
