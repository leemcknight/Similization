using System;
using System.Collections.Generic;
using System.Text;
using McKnight.Similization.Core;
using System.Drawing;

namespace McKnight.Similization.Server
{
    /// <summary>
    /// Class responsible for calculating the best possible paths for <see cref="Unit"/> objects
    /// moving to a desired destination.
    /// </summary>
    public class JourneyCalculator : IJourneyCalculator
    {        
        /// <summary>
        /// Creates a <see cref="Journey"/> object that will represent the path the specified 
        /// <see cref="Unit"/> must follow in order to reach the specified <i>destination</i>.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public Journey CreateJourney(UnitBase unit, System.Drawing.Point destination)
        {
            Point[] path = null;
            if (unit.Type == UnitType.Air)
                path = CreateAirJourney(unit, destination);
            else if (unit.Type == UnitType.Land)
                path = CreateLandJourney(unit, destination);
            else if (unit.Type == UnitType.Sea)
                path = CreateWaterJourney(unit, destination);
            Journey journey = new Journey(unit.Coordinates, destination, path);
            return journey;
        }

        private Point[] CreateAirJourney(UnitBase unit, Point destination)
        {
            throw new NotImplementedException();
            
        }

        private Point[] CreateWaterJourney(UnitBase unit, Point destination)
        {
            throw new NotImplementedException();
        }

        private Point[] CreateLandJourney(UnitBase unit, Point destination)
        {
            throw new NotImplementedException();
        }
    }
}
