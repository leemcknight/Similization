using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace McKnight.Similization.Core
{
    /// <summary>
    /// Interface used to create <see cref="Journey"/> objects for units.
    /// </summary>
    public interface IJourneyCalculator
    {
        /// <summary>
        /// Creates a <see cref="Journey"/> that will move the specified <see cref="UnitBase"/> to the 
        /// desired <i>destination</i>.        
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        Journey CreateJourney(UnitBase unit, Point destination);
    }
}
