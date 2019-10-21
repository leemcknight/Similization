using System;
using System.Collections.Generic;
using System.Text;

namespace McKnight.Similization.Server
{
    internal class RiverBuilder
    {
        private Grid grid;

        internal RiverBuilder(Grid grid)
        {
            if (grid == null)
                throw new ArgumentNullException("grid");

            this.grid = grid;
        }

        internal void GenerateRivers()
        {

        }

        private void AddRiver(GridCell source, GridCell destination)
        {
            
        }
    }
}
