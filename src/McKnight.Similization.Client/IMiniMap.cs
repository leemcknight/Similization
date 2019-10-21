using System;
using System.Drawing;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// Interface representing the little map showing the entire grid.
	/// </summary>
	public interface IMiniMap
	{
		/// <summary>
		/// Event that fires whenever the user clicks on the mini-map.
		/// </summary>
		event EventHandler<MiniMapClickedEventArgs> MiniMapClicked;

		/// <summary>
		/// The <see cref="GridCell"/> that is centered.  
		/// </summary>
		/// <remarks>
		/// Changing this value will 
		/// reposistion the overlay showing the area of the map that is displayed 
		/// on the screen.</remarks>
		GridCell CenterCell { get; set; }

		/// <summary>
		/// Initializes the <c>Grid</c> with the IMiniMap interface.
		/// </summary>
		/// <param name="grid"></param>
		void InitializeMap(Grid grid);

		/// <summary>
		/// A <c>System.Drawing.Rectangle</c> structure describing the area of the map that the user is currently viewing.
		/// </summary>
		/// <remarks>The <c>IMiniMap</c> interface uses this to determine where to center and 
		/// how to draw the rectangle showing the user the are of the map they are viewing.</remarks>
		Rectangle VisibleBounds { get; set;	}
	}

	/// <summary>
	/// Event arguments for the <i>MiniMapClicked</i> event.
	/// </summary>
	public class MiniMapClickedEventArgs : EventArgs
	{
		private Point _center;

		/// <summary>
		/// Initializes a new instance of the <see cref="MiniMapClickedEventArgs"/> class.
		/// </summary>
		/// <param name="center"></param>
		public MiniMapClickedEventArgs(Point center)
		{
			_center = center;
		}

		/// <summary>
		/// Gets the coordinates representing the center of the screen where they clicked on the map.
		/// </summary>
		public Point CenterCell
		{
			get { return _center; }
		}
	}
}
