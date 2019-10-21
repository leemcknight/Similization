using System;
using System.Drawing;
using LJM.Similization.Client.DirectX.Controls;
using LJM.Similization.Client;
using LJM.Similization.Server;

namespace LJM.Similization.Client.DirectX
{
    /// <summary>
    /// DirectX implementation of the <see cref="IMiniMap"/> interface.
    /// </summary>
	public class MiniMap : DXControl, IMiniMap
	{
		private event EventHandler<MiniMapClickedEventArgs> clicked;
        private Grid grid;
        private GridCell centerCell;
        private Rectangle bounds;

        /// <summary>
        /// Initializes a new instance of the <see cref="MiniMap"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        public MiniMap(IDirectXControlHost controlHost)
            : base(controlHost)
        {
        }

        /// <summary>
        /// Occurs when the user clicks on the <see cref="MiniMap"/> with the
        /// mouse.
        /// </summary>
        public event EventHandler<MiniMapClickedEventArgs> MiniMapClicked
		{
			add
			{
				this.clicked += value;
			}

			remove
			{
				this.clicked -= value;
			}
		}

        /// <summary>
        /// Initializes the mini-map with the specified <see cref="Grid"/>.
        /// </summary>
        /// <param name="grid"></param>
		public void InitializeMap(Grid grid)
		{
			this.grid = grid;
		}
		
        /// <summary>
        /// Gets or sets the <see cref="GridCell"/> that is centered on the map.
        /// </summary>
		public GridCell CenterCell
		{
			get { return this.centerCell; }
			set { this.centerCell = value; }
		}
   
        /// <summary>
        /// A the rectangle of grid coordinates that determines the area of 
        /// the map that is visible to the user.
        /// </summary>
        public Rectangle VisibleBounds
        {
            get
            {
                return this.bounds;
            }
            set
            {
                this.bounds = value;
            }
        }

    }
}