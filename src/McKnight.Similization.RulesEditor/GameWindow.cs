using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using Similization.Objects;
using LJM.Similization.DataObjects;
using LJM.Similization.DataObjects.Relational;

namespace LJM.SimilizationRulesEditor
{
	/// <summary>
	/// Summary description for GameWindow.
	/// </summary>
	public class GameWindow : Control
	{
		private GameRoot _gameRoot;
		private Size _cellSize;
		private event StatusChangedEventHandler _statusChanged;
		private Bitmap _cityBitmap;
		private Bitmap _villageBitmap;
		private Bitmap _roadNSBitmap;
		private Bitmap _roadEWBitmap;
		private Bitmap _activeBitmap;
		private Bitmap _irrigationBitmap;
		private Bitmap _grassBitmap;
		private GridCell _centerCell;
		private const int CELL_WIDTH = 128;
		private const int CELL_HEIGHT = 64;

		public GameWindow()
		{
			_cellSize = new Size(CELL_WIDTH,CELL_HEIGHT);
			SetStyle(ControlStyles.AllPaintingInWmPaint | 
				//ControlStyles.DoubleBuffer | 
				ControlStyles.ResizeRedraw | 
				ControlStyles.UserPaint, true);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(Brushes.White, this.ClientRectangle);
			ControlPaint.DrawBorder3D(e.Graphics, this.DisplayRectangle, Border3DStyle.SunkenInner, Border3DSide.All);
			if(_gameRoot != null)
			{
				DrawGrid();
			}
			base.OnPaint(e);
		}

		/// <summary>
		/// Gets a <c>Point</c> representing the location in screen coordinates
		/// of the upper left pixel to start drawing the center cell.
		/// </summary>
		/// <returns></returns>
		private Point GetOrigin()
		{
			Point center;
			Point origin;

			center = GetCenterPoint();

			origin = new Point(center.X - (CELL_WIDTH/2),
				center.Y - (CELL_HEIGHT/2));

			return origin;
		}

		/// <summary>
		/// Draws the grid onto the screen.
		/// </summary>
		private void DrawGrid()
		{
			if(GameRoot.ActiveUnit != null)
			{
				_centerCell = GameRoot.ActiveUnit.Location;
			}

			GridCell drawnCell = _centerCell;
			Point cellCoordinates = _centerCell.Location;
			Direction currentDirection = Direction.East;
			int timesToDirectionChange=0;
			int timesInDirection=0;

			while(drawnCell != null)
			{
				DrawCell(drawnCell);
				drawnCell = GetNextDrawingCell(drawnCell, currentDirection);

				timesInDirection++;
				if(timesInDirection >= timesToDirectionChange)
				{
					currentDirection = GetNextDirection(currentDirection);
					timesInDirection = 0;

					if(currentDirection == Direction.East || 
						currentDirection == Direction.West)
					{
						timesToDirectionChange++;
					}
				}
			}
		}

		/// <summary>
		/// Gets a <c>GridCell</c> object representing the next cell to draw onto the screen.
		/// </summary>
		/// <param name="currentCell">The cell that was just drawn on the screen.</param>
		/// <param name="nextCellDirection">The direction to search for the next cell.
		/// Maps are drawn center out, going clockwise, so direction will change at each corner.</param>
		/// <returns></returns>
		private GridCell GetNextDrawingCell(GridCell currentCell, Direction nextCellDirection)
		{
			GridCell nextCell = null;

			switch(nextCellDirection)
			{
				case Direction.North:
					nextCell = (GridCell)currentCell.TopCell;
					break;
				case Direction.South:
					nextCell = (GridCell)currentCell.BottomCell;
					break;
				case Direction.East:
					nextCell = (GridCell)currentCell.RightCell;
					break;
				case Direction.West:
					nextCell = (GridCell)currentCell.LeftCell;
					break;
			}

			return nextCell;
		}

		/// <summary>
		/// Gets the next clockwise direction.
		/// </summary>
		/// <param name="currentDirection"></param>
		/// <returns></returns>
		private Direction GetNextDirection(Direction currentDirection)
		{
			Direction nextDirection = Direction.North;
			switch(currentDirection)
			{
				case Direction.North:
					nextDirection = Direction.East;
					break;
				case Direction.South:
					nextDirection = Direction.West;
					break;
				case Direction.East:
					nextDirection = Direction.South;
					break;
				case Direction.West:
					nextDirection = Direction.North;
					break;
			}

			return nextDirection;
		}

		/// <summary>
		/// Draws the cell onto the screen
		/// </summary>
		/// <param name="gridCell"></param>
		private void DrawCell(GridCell gridCell)
		{
			Graphics g = CreateGraphics();
			Point location;
			Rectangle bounds;
			
			if(gridCell == null)
			{
				OnStatusChanged(this, new StatusChangedEventArgs("DrawCell was passed a null cell!", 5));
				return;
			}
			
			location = Translate(gridCell);
			bounds = new Rectangle(location, _cellSize);

			if(!gridCell.HasBeenExploredBy(_gameRoot.PlayerColony))
			{
				Color alphaTransparent = Color.Black;
				if(gridCell.FogOfWarBitmap.Name != GridCellImageKey.FogOfWar)
				{
					gridCell.CellTexture.MakeTransparent(alphaTransparent);
					g.DrawImage(gridCell.CellTexture, bounds);
				}

				g.DrawImage(gridCell.FogOfWarBitmap.Image.Image, bounds);
				return;
			}

			g.DrawImage(gridCell.CellTexture, bounds);

			if(gridCell.City != null)
			{
				g.DrawImage(_cityBitmap, bounds);
			}
			else if(gridCell.Village != null)
			{
				g.DrawImage(_villageBitmap, bounds);
			}

			if(gridCell.IsIrrigated)
			{
				g.DrawImage(_irrigationBitmap, bounds);
			}

			if(gridCell.HasRoad)
			{
				g.DrawImage(GetRoadPic(gridCell), bounds);
			}

			if(gridCell.Resource != null)
			{
				g.DrawImage(gridCell.Resource.EditorImage.Image, bounds);	
			}

			if(gridCell.Units.Count > 0)
			{
				MoveableUnit unit;
				unit = (MoveableUnit)gridCell.Units[0];

				Debug.Assert(unit.EditorImage != null,
					"Null Image!");

				g.DrawImage(unit.EditorImage.Image, bounds);

				if(unit == GameRoot.ActiveUnit)
				{
					g.DrawImage(_activeBitmap, bounds);
				}
			}
		
		}

		private Bitmap GetRoadPic(GridCell cell)
		{
			Bitmap roadBitmap = null;
			switch(cell.RoadLayout)
			{
				case RoadLayout.None:
					roadBitmap = null;
					break;
				case RoadLayout.BiDirectional:
					roadBitmap = _roadNSBitmap;
					break;
				case RoadLayout.EastWest:
					roadBitmap = _roadEWBitmap;
					break;
				case RoadLayout.NorthSouth:
					roadBitmap = _roadNSBitmap;
					break;
			}

			return roadBitmap;
		}

		/// <summary>
		/// Gets a <c>Point</c> structure representing the coordinates of the 
		/// cell requested.  Note that this point will be pixel coordinates, not
		/// grid coordinates.
		/// </summary>
		/// <param name="gridCell"></param>
		/// <returns></returns>
		private Point Translate(RelationalGridCell gridCell)
		{
			int rightOffCenter;
			int topOffCenter;
			int rightDelta;
			int topDelta = 0;
			Point location;
			Point center = GetCenterPoint();

			rightOffCenter = gridCell.Location.X - _centerCell.Location.X;
			topOffCenter = gridCell.Location.Y - _centerCell.Location.Y;
			
			rightDelta = rightOffCenter * (CELL_WIDTH/2);
			topDelta += rightOffCenter * (CELL_HEIGHT/2);

			rightDelta += topOffCenter * (CELL_WIDTH/2); 
			topDelta -= topOffCenter * (CELL_HEIGHT/2);
			
			location = new Point(center.X + rightDelta, center.Y - topDelta);

			return location;
		}

		/// <summary>
		/// Gets the grid cell at the location on the grid matching the x/y coordinats
		/// in the point.
		/// </summary>
		/// <param name="point">A <c>Point</c> structure containing the coordinates to get the
		/// Grid Cell for</param>
		/// <returns>A <c>GridCell</c> object representing the grid cell at the
		/// coordinates requested.</returns>
		public GridCell UnTranslate(Point point)
		{
			int rightOffCenter;
			int topOffCenter;
			int rightDelta;
			int topDelta;
			Point center = GetCenterPoint();
			GridCell cell = _centerCell;
			Direction eastWest;
			Direction northSouth;
			
			rightOffCenter = point.X - center.X;
			topOffCenter = point.Y - center.Y;

			rightDelta = rightOffCenter / (CELL_WIDTH/2);
			topDelta = rightOffCenter / (CELL_HEIGHT/2);

			rightDelta += topOffCenter / (CELL_WIDTH/2);
			topDelta -= topOffCenter / (CELL_HEIGHT/2);

			eastWest = rightDelta >= 0 ? Direction.East : Direction.West;
			northSouth = topDelta >= 0 ? Direction.South : Direction.North;

			rightDelta = Math.Abs(rightDelta);
			topDelta = Math.Abs(topDelta);

			for(int i = 0; i < rightDelta; i++)
			{
				for(int j = 0; j < topDelta; j++)
				{
					if(northSouth == Direction.South)
					{
						cell = (GridCell)cell.BottomCell;
					}
					else
					{
						cell = (GridCell)cell.TopCell;
					}
				}

				if(northSouth == Direction.East)
				{
					cell = (GridCell)cell.RightCell;
				}
				else
				{
					cell = (GridCell)cell.LeftCell;
				}
			}

			return cell;
		}

		/// <summary>
		/// Gets a <c>Point</c> structure representing the center of the GameWindow.
		/// </summary>
		/// <returns></returns>
		public Point GetCenterPoint()
		{
			return new Point(ClientRectangle.Width/2,
				ClientRectangle.Height/2);
		}

		public void StartGame(Civilization playerCivilization, ArrayList civilizations, string leaderName, WorldSize mapSize)
		{
			_cityBitmap = new Bitmap("City.bmp");
			_cityBitmap.MakeTransparent(TransparentColor.Value);
			_gameRoot = GameRoot.GetGameRoot();
			_villageBitmap = new Bitmap("village.bmp");
			_villageBitmap.MakeTransparent(TransparentColor.Value);
			_roadNSBitmap = new Bitmap("road_ns.bmp");
			_roadNSBitmap.MakeTransparent(TransparentColor.Value);
			_roadEWBitmap = new Bitmap("road_ew.bmp");
			_roadEWBitmap.MakeTransparent(TransparentColor.Value);
			_activeBitmap = new Bitmap("active.bmp");
			_activeBitmap.MakeTransparent(TransparentColor.Value);
			_irrigationBitmap = new Bitmap("irrigation.bmp");
			_irrigationBitmap.MakeTransparent(TransparentColor.Value);
			_grassBitmap = new Bitmap(@"images\grass.bmp");
			_grassBitmap.MakeTransparent(TransparentColor.Value);
			
			_gameRoot.StatusChanged += new StatusChangedEventHandler(OnStatusChanged);
			_gameRoot.MapSize = mapSize;
			_gameRoot.Start(playerCivilization, civilizations, leaderName);
		}

		private void OnStatusChanged(object sender, StatusChangedEventArgs e)
		{
			if(_statusChanged != null)
				_statusChanged(this, e);
		}

		public GameRoot GameRoot
		{
			get { return _gameRoot; }
		}

		public event StatusChangedEventHandler StatusChanged
		{
			add
			{
				_statusChanged += value;
			}
			
			remove
			{
				_statusChanged -= value;
			}
		}
	}
}
