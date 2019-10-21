using System;
using System.Collections.Generic;
using System.Text;

namespace McKnight.Similization.Core
{
    /// <summary>
    /// Defines an interface for things that can be traded among players.
    /// </summary>
    public interface ITradable
    {
        /// <summary>
        /// Gets the value of the tradable item.
        /// </summary>
        /// <returns></returns>
        int CalculateValueForCountry(CountryBase country);
    }
}
