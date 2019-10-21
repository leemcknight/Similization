using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Drawing;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
    /// <summary>
    /// Class responsible for generating terrain for a grid.
    /// </summary>
    internal class TerrainBuilder
    {
        private Grid grid;
        private IList terrains;

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainBuilder"/> class.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="terrains"></param>
        public TerrainBuilder(Grid grid, IList terrains)
        {
            if (grid == null)
                throw new ArgumentNullException("grid");
            if (terrains == null)
                throw new ArgumentNullException("terrains");

            this.grid = grid;
            this.terrains = terrains;
        }

        /// <summary>
        /// Generates terrain for all of the <see cref="GridCell"/> objects 
        /// within the <see cref="Grid"/>.
        /// </summary>
        internal void GenerateTerrain()
        {
            GridCell cell;
            for (int i = 0; i < grid.Size.Width; i++)
            {
                for (int j = 0; j < grid.Size.Height; j++)
                {
                    cell = grid.GetCell(new Point(i, j));
                    AssignTerrainToCell(cell);
                }
            }
        }
        
        //Assigns an appropriate terrain to the specified cell.
        private void AssignTerrainToCell(GridCell cell)
        {
            if (cell.IsDry)
                AssignDryTerrain(cell);
            else
                AssignWetTerrain(cell);
        }

        //Assigns a dry terrain to the GridCell.  
        private void AssignDryTerrain(GridCell cell)
        {            
            List<Terrain> candidates = new List<Terrain>();

            //these are the properties of a cell that determine the type
            //of terrain that will be assigned.  Each terrain has ranges
            //for each of these properties that it can support.  If a terrain
            //satisfies all of the properties, it is considered a candidate
            //terrain for the cell.
            int elevation = cell.Altitude;
            int temperature = cell.Temperature;
            int rainfall = cell.Rainfall;

            //weed out wet terrains and dry terrains that are of an inappropriate altitude.
            foreach (Terrain terrain in this.terrains)
            {
                if (terrain.Dry)
                {
                    if (elevation >= terrain.LowestElevation && elevation <= terrain.HighestElevation)
                    {
                        if (temperature >= terrain.LowestTemperature && temperature <= terrain.HighestTemperature)
                        {
                            if (rainfall >= terrain.LowestRainfall && rainfall <= terrain.MaximumRainfall)                            
                                candidates.Add(terrain);                            
                        }
                    }
                }
            }

            if (candidates.Count == 0)
            {
                string format = ServerResources.CannotAssignTerrain;
                string param1 = cell.IsDry ? "Dry" : "Wet";

                //format the message
                string msg = string.Format(
                    CultureInfo.InvariantCulture, 
                    format, 
                    param1, 
                    cell.Altitude.ToString(CultureInfo.InvariantCulture), 
                    cell.Temperature.ToString(CultureInfo.InvariantCulture)
                    );

                throw new InvalidOperationException(msg);
            }
            else if (candidates.Count == 1)
            {
                cell.Terrain = candidates[0];
                return;
            }
            else
            {
                //TODO: code for multiple terrain candiates.
                cell.Terrain = candidates[0];
            }
        }

        //Assigns a wet terrain to a gridcell.  The rules for assigning a wet terrain 
        //are much different than a dry terrain.  For wet terrains, we only care 
        //about depth to determine terrain.  Temperature and rainfall are not needed.
        private void AssignWetTerrain(GridCell cell)
        {            
            int elevation = cell.Altitude;
            foreach (Terrain terrain in this.terrains)
            {
                if (!terrain.Dry)
                {
                    if (elevation >= terrain.LowestElevation && elevation <= terrain.HighestElevation)
                    {
                        cell.Terrain = terrain;
                        break;
                    }
                }
            }

            if (cell.Terrain == null)
            {
                string format = ServerResources.CannotAssignTerrain;
                string param1 = cell.IsDry ? "Dry" : "Wet";
                string msg = string.Format(
                    CultureInfo.InvariantCulture, 
                    format, 
                    param1, 
                    cell.Altitude.ToString(CultureInfo.InvariantCulture), 
                    cell.Temperature.ToString(CultureInfo.InvariantCulture)
                    );
                throw new InvalidOperationException(msg);
            }
        }

        //Gets the deepest water based terrain in the ruleset.  
        //This terrain is always used for ocean squares.
        private Terrain GetWetTerrain()
        {            
            Terrain best = null;
            foreach (Terrain terrain in this.terrains)
            {
                if (terrain.Dry == false)
                {
                    if (best == null || best.LowestElevation > terrain.LowestElevation)
                    {
                        best = terrain;
                    }
                }
            }
            return best;
        }
    }
}
