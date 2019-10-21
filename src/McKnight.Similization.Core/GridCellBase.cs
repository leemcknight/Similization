using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace McKnight.Similization.Core
{
    /// <summary>
    /// Class representing an individual cell on a grid.
    /// </summary>
    public class GridCellBase
    {
        private int altitude;
        private int temperature;
        private int rainfall;
        private bool isBorder;
        private bool isPolluted;
        private bool isDry;
        private bool isIrrigated;
        private bool hasRoad;
        private bool hasRiver;
        private bool hasRailroad;
        private bool hasMine;
        private bool hasFortress;
        private Resource resource;
        private Terrain terrain;
        private CountryBase owner;
        private Point coordinates;


        /// <summary>
        /// The Altitude of the <see cref="GridCellBase"/>.
        /// </summary>
        public int Altitude
        {
            get { return this.altitude; }
            set { this.altitude = value; }
        }

        /// <summary>
        /// The temperature of the <see cref="GridCellBase"/>.
        /// </summary>
        /// <remarks>The temperature of a terrain can range from 0-100.</remarks>
        public int Temperature
        {
            get { return this.temperature; }
            set { this.temperature = value; }
        }

        /// <summary>
        /// The amount of rainfall per year the <see cref="GridCellBase"/> gets.
        /// </summary>
        /// <remarks>The rainfall amount will be in a range from 0-100.</remarks>
        public int Rainfall
        {
            get { return this.rainfall; }
            set { this.rainfall = value; }
        }

        /// <summary>
        /// A <c>System.Drawing.Point</c> structure representing the 
        /// coordinates of the <see cref="GridCell"/> within the <see cref="Grid"/>.
        /// </summary>
        public Point Coordinates
        {
            get { return this.coordinates; }
            set { this.coordinates = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating wheter or 
        /// not there is pollution in this cell.
        /// </summary>
        public bool IsPolluted
        {
            get { return this.isPolluted; }
            set { this.isPolluted = value; }
        }

        /// <summary>
        /// Gets or sets whether or not the cell can be considred "dry" (i.e land)
        /// or not.
        /// </summary>
        public bool IsDry
        {
            get { return this.isDry; }
            set { this.isDry = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not
        /// the cell has irrigation.
        /// </summary>
        public bool IsIrrigated
        {
            get { return this.isIrrigated; }
            set { this.isIrrigated = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not 
        /// the cell has a road on it.
        /// </summary>
        /// <remarks>
        /// This will return <i>true</i> if there is a railroad also.
        /// </remarks>
        public bool HasRoad
        {
            get { return this.hasRoad; }
            set { this.hasRoad = value; }
        }

        /// <summary>
        /// Determines whether a river is located on this cell.
        /// </summary>
        public bool HasRiver
        {
            get { return this.hasRiver; }
            set { this.hasRiver = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not
        /// the cell has a railroad on it.
        /// </summary>
        public bool HasRailroad
        {
            get { return this.hasRailroad; }
            set { this.hasRailroad = value; }
        }

        /// <summary>
        /// Gets a value indicating whether or not
        /// there is a mine located in this cell.
        /// </summary>
        public bool HasMine
        {
            get { return this.hasMine; }
            set { this.hasMine = value; }
        }

        /// <summary>
        /// Determines whether the cell is fortified.
        /// </summary>
        public bool HasFortress
        {
            get { return this.hasFortress; }
            set { this.hasFortress = value; }
        }

        /// <summary>
        /// The <see cref="Resource"/> that exists in the GridCell.
        /// </summary>
        public Resource Resource
        {
            get { return this.resource; }
            set { this.resource = value; }
        }

        /// <summary>
        /// The <see cref="Terrain"/> that is on the GridCell.
        /// </summary>
        public Terrain Terrain
        {
            get { return this.terrain; }
            set { this.terrain = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating wheter or 
        /// not this cell is located on the border of
        /// "cultural influence" of the parent colony.
        /// </summary>
        public bool IsBorder
        {
            get { return this.isBorder; }
            set { this.isBorder = value; }
        }

        /// <summary>
        /// Gets or sets the country the cell belongs to. Unclaimed cells will return <i>null</i>.
        /// </summary>
        public CountryBase Owner
        {
            get { return this.owner; }
            set { this.owner = value; }
        }
    }
}
