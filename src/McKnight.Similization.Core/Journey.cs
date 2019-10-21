using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Drawing;

namespace McKnight.Similization.Core
{
    /// <summary>
    /// Represents a journey that a unit can take.  <see cref="Journey"/>s 
    /// have origins and destinations, and also an optimal path.
    /// </summary>
    public class Journey
    {
        private Point origin;
        private Point destination;
        private Point[] path;
        private int pathIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Journey"/> class.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <param name="path"></param>
        public Journey(Point origin, Point destination, Point[] path)
        {
            this.origin = origin;
            this.destination = destination;
            this.path = path;
        }

        /// <summary>
        /// A <i>System.Drawing.Point</i> representing the coordinates on the map 
        /// of the starting location for the <see cref="Journey"/>.
        /// </summary>
        public Point Origin
        {
            get { return this.origin; }
        }

        /// <summary>
        /// Gets a collection of <i>Point</i> objects representing the coordinates 
        /// of the remaining locations in the path.
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<Point> CalculateExistingPath()
        {
            List<Point> list = new List<Point>();
            int bound = this.path.Length - 1;
            for (int i = this.pathIndex; i <= bound; i++)
                list.Add(this.path[i]);
            return new ReadOnlyCollection<Point>(list);
        }

        /// <summary>
        /// A <i>System.Drawing.Point</i> representing the coordinates on the map 
        /// of the ending location for the <see cref="Journey"/>.
        /// </summary>
        public Point Destination
        {
            get { return this.destination; }
        }

        /// <summary>
        /// Determines whether the <see cref="Journey"/> is at an end.
        /// </summary>
        public bool Finished
        {
            get
            {
                if (this.path == null)
                    return true;
                if (this.path.Length == 0)
                    return true;
                if (this.path.GetUpperBound(0) == this.pathIndex)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// Returns the next <i>Point</i> in the path, without actually moving the 
        /// internal pointer to that point in the <see cref="Journey"/>.
        /// </summary>
        /// <returns></returns>
        public Point PeekNextPoint()
        {
            if (pathIndex == this.path.Length - 1)
                return this.path[this.path.Length - 1];
            return this.path[this.pathIndex + 1];
        }

        /// <summary>
        /// Moves one point along the path along the <see cref="Journey"/>.  
        /// </summary>
        /// <returns>The next coordinate in the path.</returns>
        public Point Continue()
        {
            if (pathIndex == this.path.Length - 1)
                return this.path[this.path.Length - 1];
            return this.path[this.pathIndex++];
        }

        /// <summary>
        /// Moves backwards one point along the <see cref="Journey"/>.
        /// </summary>
        /// <returns></returns>
        public Point Backtrack()
        {
            if (pathIndex == 0)
                return this.path[0];
            return this.path[this.pathIndex--];
        }
    }
}
