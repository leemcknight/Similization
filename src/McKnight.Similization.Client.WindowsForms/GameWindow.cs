using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using McKnight.Similization.Core;
using McKnight.Similization.Client;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// This is the control used to render the game onto the screen.
	/// </summary>
	internal class GameWindow : GridViewBase, IGameWindow
	{
		private Dictionary<City, Rectangle> cityLocations = new Dictionary<City, Rectangle>();
		private ContextMenu contextMenu;
		private MenuItem terrainInfoMenuItem;
		private Rectangle visibleBounds;		
        private bool goToCursor;
        private Point hotCoordinates;
        private Unit activeUnit;
		private bool bombardFlag;

		/// <summary>
		/// Initializes a new instance of the <see cref="GameWindow"/> class.
		/// </summary>
		public GameWindow() : base()
		{			
			LoadContextMenu();						
		}

		/// <summary>
		/// Override of the base class paint handler.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{			
			base.OnPaint(e);            						
			if(this.goToCursor && this.activeUnit != null)			
				PaintMoveToLine(e.Graphics, this.activeUnit.Coordinates, this.hotCoordinates);			
            if (this.bombardFlag && this.activeUnit != null)            
                PaintMoveToLine(e.Graphics, this.activeUnit.Coordinates, this.hotCoordinates);                        
		}

		/// <summary>
		/// Shows a message box with the desired information.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="title"></param>
		public void ShowMessageBox(string message, string title)
		{
			MessageBox.Show(
                message,
                title, 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information                                
                );
		}

		/// <summary>
		/// Shows a message box with the desired information.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="title"></param>
		/// <param name="newCenter"></param>
		public void ShowMessageBox(string message, string title, Point newCenter)
		{
			this.CenterCoordinates = newCenter;
			ShowMessageBox(message,title);
		}

		/// <summary>
		/// Handles the clicking on the control.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClick(System.EventArgs e)
		{
			if(this.goToCursor)
			{
				this.goToCursor = false;
				GameRoot root = GameRoot.Instance;
				if(root.ActiveUnit != null)				
					root.ActiveUnit.MoveTo(this.hotCoordinates);				
			}
			else if(this.bombardFlag)			
				Bombard();			
			base.OnClick(e);
		}


		private void Bombard()
		{			
			GameRoot root = GameRoot.Instance;
			if(root.ActiveUnit == null)
				throw new InvalidOperationException(ClientResources.GetString("error_bombardNoUnit"));

			bool airUnit = root.ActiveUnit.Type == UnitType.Air;
			string text = string.Empty;
            GridCell cell = this.Grid.GetCell(this.hotCoordinates);
			if(root.ActiveUnit.PrecisionBombardment)
			{                
				ImprovementPicker picker = new ImprovementPicker(cell.City);
				if(picker.ShowDialog() == DialogResult.OK)
				{
					bool success = root.ActiveUnit.Bombard(cell,picker.Improvement);
					if(!success)
					{
						text = ClientResources.GetString("bombingrun_failed");
					}
					this.bombardFlag = false;
				}
			}
			else
			{
				BombardmentResult br = root.ActiveUnit.Bombard(cell);
				switch(br)
				{
					case BombardmentResult.AttackerKilled:
						text = ClientResources.GetString("bombingrun_failed_lostunit");
						break;
					case BombardmentResult.Failed:
						if(airUnit)
							text = ClientResources.GetString("bombingrun_failed");
						else
							text = ClientResources.GetString("bombardment_failed");
						break;
					case BombardmentResult.SucceededDestroyingCellImprovement:
						if(airUnit)
							text = ClientResources.GetString("bombingrun_success_improvement");
						else
							text = ClientResources.GetString("bombardment_success_improvement");
						break;
					case BombardmentResult.SucceededInjuredUnit:
						if(airUnit)
							text = ClientResources.GetString("bombingrun_success_unit");
						else
							text = ClientResources.GetString("bombardment_success_unit");
						break;
				}
				this.bombardFlag = false;
			}

			if(text.Length > 0)
				this.ShowMessageBox(text, ClientResources.GetString(StringKey.GameTitle));
			Invalidate();
		}

		/// <summary>
		/// Handles the typing in the control.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			ClientApplication client = ClientApplication.Instance;
			GameRoot root = client.ServerInstance;

			if(e.KeyCode == Keys.Space)
			{
				if(root.ActiveCountry.NotifyEndOfTurn)				
					root.ActivateNextCountry();				
				else				
					root.ActivateNextUnit();				
				return;
			}

			Unit unit = root.ActiveUnit;
            if (unit == null) 
                return;
						
            Point newCoordinates = Point.Empty;            
			switch(e.KeyCode)
			{
				case Keys.NumPad3:
                    newCoordinates = new Point(unit.Coordinates.X + 1, unit.Coordinates.Y + 1);
					break;
				case Keys.NumPad7:
                    newCoordinates = new Point(unit.Coordinates.X - 1, unit.Coordinates.Y - 1);
					break;
				case Keys.NumPad1:
                    newCoordinates = new Point(unit.Coordinates.X - 1, unit.Coordinates.Y + 1);
					break;
				case Keys.NumPad9:
                    newCoordinates = new Point(unit.Coordinates.X + 1, unit.Coordinates.Y - 1);
					break;
				case Keys.NumPad4:
                    newCoordinates = new Point(unit.Coordinates.X - 1, unit.Coordinates.Y);
					break;
				case Keys.NumPad6:
                    newCoordinates = new Point(unit.Coordinates.X + 1, unit.Coordinates.Y);
					break;
				case Keys.NumPad2:
                    newCoordinates = new Point(unit.Coordinates.X, unit.Coordinates.Y + 1);
					break;
				case Keys.NumPad8:
					newCoordinates = new Point(unit.Coordinates.X, unit.Coordinates.Y - 1);
					break;
			}

			if(newCoordinates == Point.Empty)  return;
			unit.MoveTo(newCoordinates);						
			Invalidate();			
		}

		/// <summary>
		/// Handles the double clicking on the control.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDoubleClick(System.EventArgs e)
		{
			Point absolute = Cursor.Position;
			Point relative = PointToClient(absolute);
			Point gridCoords = this.GridPainter.TranslateScreenCoordinatesToGridCoordinates(relative);	

			if(gridCoords == Point.Empty)			
				return;

            GridCell gridCell = this.Grid.GetCell(gridCoords);

			if(gridCell.City != null)
			{
				CityDialog cityDetailWindow;

				cityDetailWindow = new CityDialog(gridCell.City);
				cityDetailWindow.ShowDialog(this);
			}
			else
			{
				TerrainDialog dlg = new TerrainDialog(gridCell);
				dlg.ShowDialog(this);
			}

			base.OnDoubleClick(e);
		}

		/// <summary>
		/// Handles the mouse movement.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseMove(MouseEventArgs e)
		{			            
			Point pt = new Point(e.X, e.Y);
			Point gridCoords = this.GridPainter.TranslateScreenCoordinatesToGridCoordinates(pt);
            
			if(gridCoords != this.hotCoordinates)
			{
				this.hotCoordinates = gridCoords;
				Invalidate();
			}

			base.OnMouseMove(e);
			
		}

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
        }

		/// <summary>
		/// Fires the <i>CenterCellChanged</i> event.
		/// </summary>
		/// <remarks>This method is overriden to ensure that the <c>MiniMap</c> is 
		/// redrawn with new and accurate graphics reflecting the visible area of the map.</remarks>
		protected override void OnCenterCellChanged()
		{
			ClientApplication client = ClientApplication.Instance;			
			client.MiniMap.VisibleBounds = this.visibleBounds;
			base.OnCenterCellChanged();
		}

		private bool CanBeBombarded()
		{
			if(!this.bombardFlag)
				return false;

			Point source = this.activeUnit.Coordinates;
			Point target = this.hotCoordinates;		
            int dist = Grid.CalculateDistanceBetweenCoordinates(source, target);
			int range = this.activeUnit.BombardmentRange;
			return (range >= dist);
		}


		


		//Draw all of the city names, populations, and current city statuses on
		//the screen.
		private void DrawInformationForCities(Graphics g)
		{
			GameRoot root = GameRoot.Instance;
			RectangleF cityRectangle;

			foreach(Country player in root.Countries)
			{
				foreach(City foeCity in player.Cities)
				{
					cityRectangle = GetCityBounds(foeCity);

					if(cityRectangle == Rectangle.Empty)
					{
						continue;
					}

					cityRectangle.Offset(0, 30);
					if(cityRectangle != RectangleF.Empty)
					{
						DrawCityInformation(foeCity, cityRectangle, g);
					}
				}
			}
		}

		private void PaintMoveToLine(Graphics g, Point origin, Point destination)
		{				
			GraphicsPath path = new GraphicsPath(FillMode.Winding);
					
			PointF pt1, pt2;
			int moves = 0;
			Point subOrigin = origin;
			Point subDestination = origin;

			do
			{
				if(subDestination.Y != destination.Y)
				{
                    if (subDestination.Y > destination.Y)
                        subDestination = new Point(subOrigin.X, subOrigin.Y -1);
                    else
                        subDestination = new Point(subOrigin.X, subOrigin.Y + 1);
				}
				else if(subDestination.X != destination.X)
				{
					if(subDestination.X > destination.X)					
                        subDestination = new Point(subOrigin.X - 1, subOrigin.Y);
					else					
                        subDestination = new Point(subOrigin.X + 1, subOrigin.Y);					
				}
				
				pt1 = this.GridPainter.TranslateGridCoordinatesToScreenCoordinates(subOrigin);
				pt2 = this.GridPainter.TranslateGridCoordinatesToScreenCoordinates(subDestination);			
				
				//offset the points to get the center of the cells.
				pt1.X += this.Tileset.TileSize.Width/2;
				pt1.Y += this.Tileset.TileSize.Height/2;
				pt2.X += this.Tileset.TileSize.Width/2;
				pt2.Y += this.Tileset.TileSize.Height/2;
				

				path.AddLine(pt1, pt2);
				subOrigin = subDestination;
				moves++;

			} while(subDestination != destination);


			Pen pen = new Pen(Color.FromArgb(100, Color.Red),5f);
			g.DrawPath(pen, path);

			g.DrawString(moves.ToString(CultureInfo.CurrentCulture), 
                this.Font, 
                Brushes.White, 
                pt2, 
                StringFormat.GenericDefault);
		}

		private Rectangle GetCityBounds(City cityToFind)
		{
			Rectangle cityBounds = Rectangle.Empty;            
			if(this.cityLocations.ContainsKey(cityToFind))
			{
				cityBounds = this.cityLocations[cityToFind];
			}

			return cityBounds;
		}

		private void DrawCityInformation(City drawnCity, RectangleF informationRect, Graphics g)
		{
			Color backColor = Color.FromArgb(100, 0,0,0);						
			int maxStringWidth;
            string cityLine = ClientResources.GetString("cityLine");
            int fontHeight = g.MeasureString(cityLine, this.CityNameFont).ToSize().Height;            
            string cityText = string.Format(
                CultureInfo.CurrentCulture, 
                cityLine, 
                drawnCity.Name,
                drawnCity.TurnsUntilGrowth.ToString(CultureInfo.CurrentCulture));

			string improvementText = string.Format(
                CultureInfo.CurrentCulture,
                drawnCity.NextImprovement.Name,
                drawnCity.TurnsUntilComplete.ToString(CultureInfo.CurrentCulture));

			maxStringWidth = g.MeasureString(cityText, this.CityNameFont).ToSize().Width;

			if(maxStringWidth < g.MeasureString(improvementText, this.CityNameFont).ToSize().Width)
			{
				maxStringWidth = g.MeasureString(improvementText, this.CityNameFont).ToSize().Width;
			}


			RectangleF rect = new RectangleF(
				informationRect.X + 30, 
				informationRect.Bottom - 30,
				maxStringWidth, 
				30);
			
			//	Border
			g.FillRectangle(new SolidBrush(backColor), rect);

			//	Country color background behind population
			g.FillRectangle(new SolidBrush(drawnCity.ParentCountry.Color),
					informationRect.X, informationRect.Bottom - 30, 30, 30);

			//	Draw the population of the city
			g.DrawString(
                drawnCity.Population.ToString(CultureInfo.CurrentCulture),
				new Font(this.CityNameFont.FontFamily, 15F,FontStyle.Bold), 
				Brushes.White, 
				new PointF(informationRect.X + 5, rect.Y + 2), 
				StringFormat.GenericTypographic);

			//	City name and turns until growth
			g.DrawString(                
                cityText, this.CityNameFont, new SolidBrush(this.CityNameFontColor),
				new PointF(informationRect.X + 30, rect.Y));

			
			//	Line separator between city name and next improvement
			g.DrawLine(new Pen(drawnCity.ParentCountry.Color),
				new PointF(rect.X, rect.Y + fontHeight), 
				new PointF(rect.Right, rect.Y + fontHeight));

			
			//	Next improvement and turns until achieved.
			g.DrawString(improvementText, this.CityNameFont, 
				new SolidBrush(this.CityNameFontColor),	
				new PointF(informationRect.X + 30, rect.Y + fontHeight));

			
			g.DrawRectangle(Pens.Gray,rect.X,rect.Y,rect.Width,rect.Height);
		}


		/// <summary>
		/// Shows the Game Window.
		/// </summary>
		public void ShowSimilizationControl()
		{
			WindowsClientApplication app = (WindowsClientApplication)ClientApplication.Instance;
			app.HostForm.SwapControl(this);
		}
		
		/// <summary>
		/// Gets or sets a value indicating if the game control is in "go to"
		/// mode.  When in this mode, the active unit will go to the grid cell
		/// selected on the map.  When moving the cursor around the screen, 
		/// the map will show the path the unit will take and the number of 
		/// turns it will take to reach the destination.
		/// </summary>
		public bool GoToCursor
		{
			get { return this.goToCursor; }
			set
			{
				this.goToCursor = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets the grid coordinates that is currently under the mouse cursor.
		/// </summary>
		public Point HotCoordinates
		{
			get { return this.hotCoordinates; }
		}
		
		/// <summary>
		/// Gets or sets the unit to focus on when drawing the screen.
		/// </summary>
		public Unit ActiveUnit
		{
			get { return this.activeUnit; }
			set { this.activeUnit = value; }
		}

		/// <summary>
		/// Gets the user's confirmation.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="title"></param>
		/// <returns></returns>
		public bool GetUserConfirmation(string message, string title)
		{
            DialogResult dr = MessageBox.Show(
                message,
                title,
                MessageBoxButtons.YesNo, 
				MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);

			if(dr == DialogResult.Yes)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Switches the <see cref="GameWindow"/> to a mode where the use must select 
		/// a <see cref="GridCell"/> to bombard.
		/// </summary>
		public void BeginBombardProcess()
		{
			bombardFlag = true;
			Invalidate();
		}

		private void LoadContextMenu()
		{
			this.contextMenu = new ContextMenu();
			this.ContextMenu = this.contextMenu;

			this.terrainInfoMenuItem = new MenuItem(ClientResources.GetString("terrainInfo"));
			this.terrainInfoMenuItem.Click += new EventHandler(HandleTerrainInfoClick);

			MenuItem splitter = new MenuItem("-");
	
			this.contextMenu.MenuItems.AddRange( new MenuItem[] {
																this.terrainInfoMenuItem,
																splitter,
										
															});
		}


		private void HandleTerrainInfoClick(object sender, System.EventArgs e)
		{
			Point absolute = Cursor.Position;
			Point relative = PointToClient(absolute);
			Point gridCoords = this.GridPainter.TranslateScreenCoordinatesToGridCoordinates(relative);	

			if(gridCoords == Point.Empty)			
				return;

            GridCell gridCell = this.Grid.GetCell(gridCoords);
			TerrainDialog dlg = new TerrainDialog(gridCell);
			dlg.ShowDialog(this);
		}
	}

	
}
