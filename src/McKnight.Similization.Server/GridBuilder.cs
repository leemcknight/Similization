using System;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Collections;
using McKnight.Similization.Core;

namespace McKnight.Similization.Server
{
    /// <summary>
    /// Class responsible for creating an instance of <see cref="Grid"/> objects.
    /// </summary>
    public class GridBuilder
    {
        private enum SeedType { TinyIsland, Island, LandMass, Continent, Panganea }
        //TODO: place in xml config file
        private const int Size_Tiny = 60;
        private const int Size_Small = 80;
        private const int Size_Standard = 100;
        private const int Size_Large = 140;
        private const int Size_Huge = 180;
        private Grid grid;
        private Ruleset ruleset;
        
        /// <summary>
        /// Generates a map with the specified parameters.
        /// </summary>
        /// <param name="worldSize"></param>
        /// <param name="worldAge"></param>
        /// <param name="worldTemperature"></param>
        /// <param name="worldClimate"></param>
        /// <param name="worldLandmass"></param>
        /// <param name="worldWaterCoverage"></param>
        /// <param name="ruleset"></param>
        /// <returns></returns>
        public Grid Build(WorldSize worldSize, Age worldAge, Temperature worldTemperature, Climate worldClimate, Landmass worldLandmass, WaterCoverage worldWaterCoverage, Ruleset ruleset)
        {
            Size size = GetDimensions(worldSize);
            this.grid = BuildGrid(size);
            this.ruleset = ruleset;
            List<Point> seeds = GetLandSeeds(grid, worldLandmass);
            GrowLandSeeds(seeds, worldWaterCoverage);
            GenerateHeightMap(worldAge);
            GenerateTemperatureMap(worldTemperature);
            GenerateClimateMap(worldClimate);            
            AddTerrain();
            AddRivers();
            AddLakes();
            AddResources();
            return grid;
        }

        private Size GetDimensions(WorldSize sizeOfWorld)
        {
            switch (sizeOfWorld)
            {
                case WorldSize.Tiny:
                    return new Size(Size_Tiny, Size_Tiny);
                case WorldSize.Small:
                    return new Size(Size_Small, Size_Small);
                case WorldSize.Standard:
                    return new Size(Size_Standard, Size_Standard);
                case WorldSize.Large:
                    return new Size(Size_Large, Size_Large);
                case WorldSize.Huge:
                    return new Size(Size_Huge, Size_Huge);
                default:
                    throw new System.ArgumentException(ServerResources.InvalidWorldSize, "sizeOfWorld");
            }
        }

        
        /// Builds a grid with the specified dimensions.        
        private Grid BuildGrid(Size size)
        {
            int x, y;
            GridCell cell;            
            GridCell[,] gridCells = new GridCell[size.Width, size.Height];
            for (y = 0; y < size.Height; y++)
            {
                for (x = 0; x < size.Width; x++)
                {
                    cell = new GridCell(new Point(x, y));                    
                    cell.IsDry = false;
                    cell.Altitude = -100;
                    gridCells[x, y] = cell;
                }
            }
            Grid grid = new Grid(gridCells);
            return grid;
        }



        //gets an array of points representing the center points 
        //of islands or continents on the map.  These points are 
        //used to generate the continents and islands.
        private List<Point> GetLandSeeds(Grid grid, Landmass worldLandmass)
        {
            int numSeeds = 0;
            switch (worldLandmass)
            {
                case Landmass.Archipelago:
                    numSeeds = 12;
                    break;
                case Landmass.Pangaea:
                    numSeeds = 1;
                    break;
                case Landmass.Continents:
                    numSeeds = 6;
                    break;
            }

            List<Point> seeds = new List<Point>();
            Point center = new Point(grid.Size.Width / 2, grid.Size.Height / 2);
            int x;
            int y;
            for (int i = 0; i < numSeeds; i++)
            {
                x = RandomNumber.Between(0, grid.Size.Width);
                y = RandomNumber.Between(0, grid.Size.Height);
                seeds.Add(new Point(x, y));
            }

            return seeds;
        }

        private void GrowLandSeeds(List<Point> seeds, WaterCoverage worldWaterCoverage)
        {            
            int seedRadius;

            SeedType seedType;
            if (seeds.Count > 1)
                seedType = SeedType.Continent;
            else
                seedType = SeedType.Panganea;

            foreach (Point seed in seeds)
            {
                seedRadius = GetSeedSize(grid.Size, seedType);
                GrowSingleSeed(seed, seedRadius);
                switch (seedType)
                {
                    case SeedType.Continent:
                        seedType = SeedType.LandMass;
                        break;
                    case SeedType.Island:
                        seedType = SeedType.TinyIsland;
                        break;
                    case SeedType.LandMass:
                        seedType = SeedType.Island;
                        break;
                    case SeedType.TinyIsland:
                        seedType = SeedType.Continent;
                        break;
                }
            }
        }

        //Takes a single point on the map and grows an island with the maxRadius.
        private void GrowSingleSeed(Point seed, int maxRadius)
        {
            Point upperLeft;            
            GridCell cell;
            int x, y;

            x = seed.X - (maxRadius / 2);
            y = seed.Y - (maxRadius / 2);

            if (x < 0)
            {
                if (y < 0)
                {
                    int larger = (x >= y ? x : y);
                    maxRadius -= Math.Abs(larger);
                    y = 0;
                }
                else
                {
                    maxRadius -= Math.Abs(x);
                }
                x = 0;
            }

            if (y < 0 && x > 0)
            {
                maxRadius -= Math.Abs(y);
                y = 0;
            }

            upperLeft = new Point(x, y);
            int xCell, yCell;
            for (int i = 0; i <= maxRadius; i++)
            {
                for (int j = 0; j <= maxRadius; j++)
                {
                    xCell = upperLeft.X + i;
                    yCell = upperLeft.Y + j;
                    if (xCell >= grid.Size.Width)
                    {
                        //off the screen in the x direction
                        //not a problem.  just add wrap it around.
                        xCell -= grid.Size.Width;
                    }
                    if (yCell >= grid.Size.Height)
                    {
                        //off the screen in the y direction.
                        yCell = grid.Size.Height - 1;
                    }
                    else if (yCell < 0)
                    {
                        yCell = 0;
                    }
                    cell = grid.GetCell(new Point(xCell, yCell));
                    cell.IsDry = true;
                    grid.DryCells.Add(cell);
                }
            }
        }

        //Generates an alitude for each grid cell on the map.  This method
        //takes an Age as a parameter.  The newer the world, the rockier
        //the general landscape will be.
        private void GenerateHeightMap(Age worldAge)
        {            
            int baseAltitude = 0;

            switch (worldAge)
            {
                case Age.FiveBillion:
                    baseAltitude = 10;
                    break;
                case Age.FourBillion:
                    baseAltitude = 25;
                    break;
                case Age.ThreeBillion:
                    baseAltitude = 50;
                    break;
            }

            foreach (GridCell cell in grid.DryCells)
            {
                if (cell.IsDry)
                    cell.Altitude = CalcLandAltitude(cell, baseAltitude);
                else
                {
                    //water.  how far are we from land?  This will
                    //determine the depth of the water.
                    cell.Altitude = CalcWaterDepth(cell);
                }
            }
        }

        //Calculate the altitude of the cell.  Altitude is determined 
        //by the base altitude (created from the age of the planet), 
        //and by other factors, such as distance from the sea.
        private int CalcLandAltitude(GridCell cell, int baseAltitude)
        {
            return baseAltitude;
        }

        //Calculate the depth of the water on the given cell.  Water 
        //depth is determined by the cells distance to land.  The 
        //farther from land, the deeper the water.
        private int CalcWaterDepth(GridCell cell)
        {            
            Point coords = cell.Coordinates;
            int distToLand = int.MaxValue;
            for (int radius = 0; radius <= this.grid.Size.Width; radius++)
            {
                if (CheckForDryLandOnRadius(cell, radius))
                {
                    distToLand = radius;
                    break;
                }
            }

            if (distToLand >= 10)
                return -100;
            else if (distToLand >= 5)
                return -50;
            else
                return -10;
        }

        private bool CheckForDryLandOnRadius(GridCell cell, int radius)
        {
            Point origin = cell.Coordinates;
            Rectangle bounds = new Rectangle(cell.Coordinates.X - radius, cell.Coordinates.Y - radius, cell.Coordinates.X + radius, cell.Coordinates.Y + radius);
                        
            if (CheckGridLineForDryLand(bounds.Location, bounds.Width, Direction.East))
                return true;
            if (CheckGridLineForDryLand(bounds.Location, bounds.Height, Direction.South))
                return true;
            if(CheckGridLineForDryLand(new Point(bounds.Right, bounds.Top), bounds.Height, Direction.South))
                return true;
            if(CheckGridLineForDryLand(new Point(bounds.Left, bounds.Bottom), bounds.Width, Direction.East))
                return true;
            return false;

        }

        //checks a single strip of terrain (either vertical or horizontal) to see 
        //if any of the cells on the strip contain dry land.
        private bool CheckGridLineForDryLand(Point start, int length, Direction direction)
        {
            Point next = start;
            for (int x = 0; x <= length; x++)
            {                
                if (this.grid.GetCell(next).IsDry)
                    return true;
                if (direction == Direction.East)
                    next = new Point(next.X + 1, next.Y);
                else if (direction == Direction.South)
                    next = new Point(next.X, next.Y + 1);
            }
            return false;
        }

        //Calulates the amount of rainfall for each grid cell on the map.
        //The amount of rainfall a cell gets is first determined by the 
        //overall world climate, and is then tweaked based on factors such 
        //as temperature, location, and altitude.
        private void GenerateClimateMap(Climate worldClimate)
        {
            GridCell cell;
            int baseRainfall = 0;
            int cellRainfall = 0;
            int temperatureEffect = 0;
            int altitudeEffect = 0;
            int locationEffect = 0;

            switch (worldClimate)
            {
                case Climate.Arid:
                    baseRainfall = 25;
                    break;
                case Climate.Normal:
                    baseRainfall = 50;
                    break;
                case Climate.Wet:
                    baseRainfall = 85;
                    break;
            }
            cellRainfall = baseRainfall + altitudeEffect + locationEffect + temperatureEffect;
            for (int i = 0; i < grid.Size.Height; i++)
            {
                for (int j = 0; j < grid.Size.Width; j++)
                {
                    cell = grid.GetCell(new Point(j, i));
                    cell.Rainfall = cellRainfall;
                }
            }
        }

        //Assigns a temperature to each grid cell on the map.  
        private void GenerateTemperatureMap(Temperature worldTemperature)
        {
            GridCell cell;
            int[] map = new int[grid.Size.Width];
            int equator = grid.Size.Height / 2;
            int baseTemp = 0;
            int baseCellTemp;
            int cellTemperature;
            int percentFromEquator;

            switch (worldTemperature)
            {
                case Temperature.Warm:
                    baseTemp = 100;
                    break;
                case Temperature.Temperate:
                    baseTemp = 80;
                    break;
                case Temperature.Cool:
                    baseTemp = 60;
                    break;
            }

            for (int i = 0; i < grid.Size.Height; i++)
            {
                percentFromEquator = CalcPercentFromEquator(equator, i);
                baseCellTemp = baseTemp - percentFromEquator;
                if (baseCellTemp < 0) baseCellTemp = 0;

                //TODO: figure this out.
                cellTemperature = baseCellTemp;

                for (int j = 0; j < grid.Size.Width; j++)
                {
                    cell = grid.GetCell(new Point(j, i));
                    cell.Temperature = cellTemperature;
                }
            }
        }

        private void AddTerrain()
        {
            IList terrains = this.ruleset.Terrains;
            TerrainBuilder terrainBuilder = new TerrainBuilder(grid, terrains);
            terrainBuilder.GenerateTerrain();
        }

        private void AddResources()
        {
            IList resources = this.ruleset.Resources;
            GridResourceBuilder resourceBuilder = new GridResourceBuilder(grid, resources);
            resourceBuilder.GenerateResourcesForGrid();
        }

        private void AddRivers()
        {
            RiverBuilder builder = new RiverBuilder(grid);
            builder.GenerateRivers();
        }

        private void AddLakes()
        {
            //TODO: implement
        }

        private int GetSeedSize(Size gridSize, SeedType seedType)
        {
            switch (gridSize.Width)
            {
                case Size_Tiny:		//60
                    switch (seedType)
                    {
                        case SeedType.Continent: return 20;
                        case SeedType.Panganea: return 45;
                        case SeedType.LandMass: return 10;
                        case SeedType.Island: return 5;
                        case SeedType.TinyIsland: return 2;
                    }
                    break;
                case Size_Small:		//80
                    switch (seedType)
                    {
                        case SeedType.Continent: return 27;
                        case SeedType.Panganea: return 60;
                        case SeedType.LandMass: return 15;
                        case SeedType.Island: return 8;
                        case SeedType.TinyIsland: return 3;
                    }
                    break;
                case Size_Standard:		//100	
                    switch (seedType)
                    {
                        case SeedType.Continent: return 33;
                        case SeedType.Panganea: return 75;
                        case SeedType.LandMass: return 25;
                        case SeedType.Island: return 12;
                        case SeedType.TinyIsland: return 5;
                    }
                    break;
                case Size_Large:		//140
                    switch (seedType)
                    {
                        case SeedType.Continent: return 75;
                        case SeedType.Panganea: return 100;
                        case SeedType.LandMass: return 35;
                        case SeedType.Island: return 18;
                        case SeedType.TinyIsland: return 8;
                    }
                    break;
                case Size_Huge:			//180
                    switch (seedType)
                    {
                        case SeedType.Continent: return 80;
                        case SeedType.Panganea: return 130;
                        case SeedType.LandMass: return 50;
                        case SeedType.Island: return 25;
                        case SeedType.TinyIsland: return 12;
                    }
                    break;
            }

            throw new ArgumentException(ServerResources.UnknownSeedSize, "seedType");
        }

        private int CalcPercentFromEquator(int equatorLocation, int cellLocation)
        {
            int cellsFromEquator = Math.Abs(equatorLocation - cellLocation);

            double d = Convert.ToDouble(cellsFromEquator) / Convert.ToDouble(equatorLocation);
            int percentFromEquator = Convert.ToInt32(d * 100);

            return percentFromEquator;
        }
    }
}
