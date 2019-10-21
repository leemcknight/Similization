using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using McKnight.Similization.Core;
using McKnight.Similization.Server;
using McKnight.Similization.Client;

namespace McKnight.Similization.Client.WindowsForms
{
    /// <summary>
    /// Class responsible for painting an individual <see cref="McKnight.Similization.Server.GridCell"/> 
    /// onto the screen.
    /// </summary>
    public class GridCellPainter : Painter
    {
        private Tileset tileset;        
        private GridCell gridCell;
        private GridCellPaintOption paintOption;
        private List<GridCellLayerPainter> painters;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GridCellPainter"/> class.
        /// </summary>
        /// <param name="tileset"></param>
        public GridCellPainter(Tileset tileset)
        {
            this.tileset = tileset;
            InitializePainters();
        }

        private void InitializePainters()
        {
            this.painters = new List<GridCellLayerPainter>();
            this.painters.Add(new TerrainPainter(this.tileset));
            this.painters.Add(new RoadPainter(this.tileset));
            this.painters.Add(new CityPainter(this.tileset));
            this.painters.Add(new VillagePainter(this.tileset));
            this.painters.Add(new ResourcePainter(this.tileset));
            this.painters.Add(new IrrigationPainter(this.tileset));
            this.painters.Add(new MinePainter(this.tileset));
            this.painters.Add(new UnitPainter(this.tileset));
        }

        /// <summary>
        /// The options for the <see cref="GridCell"/> when painting it.
        /// </summary>
        public GridCellPaintOption PaintOption
        {
            get { return this.paintOption; }
            set { this.paintOption = value; }
        }

        /// <summary>
        /// The <see cref="McKnight.Similization.Server.GridCell"/> to paint.
        /// </summary>
        public GridCell GridCell
        {
            get { return this.gridCell; }
            set { this.gridCell = value; }
        }

        /// <summary>
        /// Paints the GridCell onto the screen.
        /// </summary>        
        public override void Paint()
        {                        
            ClientApplication client = ClientApplication.Instance;           
            Country player = client.Player;

            if (this.paintOption == GridCellPaintOption.UnexploredCell)
            {
                this.Graphics.FillRectangle(Brushes.Black, this.Bounds);
                return;
            }

            foreach (GridCellLayerPainter painter in this.painters)
            {
                painter.Graphics = this.Graphics;
                painter.GridCell = this.gridCell;
                painter.Bounds = this.Bounds;
                painter.Paint();
            }

            if (this.PaintOption == GridCellPaintOption.ExploredUninhabited)
            {
                Brush brush = new SolidBrush(Color.FromArgb(50, Color.Black));
                this.Graphics.FillRectangle(brush, this.Bounds);
            }
        }             
    }
}
