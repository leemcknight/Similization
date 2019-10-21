using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
    internal class GridResourceBuilder
    {
        private Grid grid;
        private IList resources;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridResourceBuilder"/> class.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="resources"></param>
        internal GridResourceBuilder(Grid grid, IList resources)
        {
            if (grid == null)
                throw new ArgumentNullException("grid");

            this.grid = grid;
            this.resources = resources;
        }


        internal void GenerateResourcesForGrid()
        {
            GridCell cell;

            for (int i = 0; i < this.grid.Size.Height; i++)
            {
                for (int j = 0; j < this.grid.Size.Width; j++)
                {
                    cell = grid.GetCell(new Point(j, i));
                    cell.Resource = FindResourceForCell(cell);
                }
            }
        }

        //Returns a Resource reference for the given cell.  Chances are,
        //this will be null.
        private Resource FindResourceForCell(GridCell cell)
        {
            int randResult = RandomNumber.UpTo(10);
            Resource resouce = null;

            if (randResult == 5)    //10% chance of this happening.
            {
                List<Resource> possibleResources = FindPossibleResourcesForTerrain(cell.Terrain);

                if (possibleResources.Count > 0)
                {
                    int index = RandomNumber.Between(0, possibleResources.Count - 1);
                    resouce = possibleResources[index];
                }
            }
            return resouce;
        }

        private List<Resource> FindPossibleResourcesForTerrain(Terrain terrain)
        {            
            List<Resource> possibleResources = new List<Resource>();
            foreach (Resource resource in this.resources)
            {
                if (resource.Terrains.Contains(terrain))
                    possibleResources.Add(resource);
            }
            return possibleResources;
        }
    }
}
