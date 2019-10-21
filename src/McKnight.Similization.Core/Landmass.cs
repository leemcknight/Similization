using System;
using System.Collections.Generic;
using System.Text;

namespace McKnight.Similization.Core
{
    /// <summary>
    /// Different land mass configurations
    /// </summary>
    public enum Landmass
    {

        /// <summary>
        /// Pangaea.  1 huge langmass.
        /// </summary>
        Pangaea,

        /// <summary>
        /// Small number of larger continents.
        /// </summary>
        Continents,

        /// <summary>
        /// Large number of smaller continents or islands.
        /// </summary>
        Archipelago
    }
}
