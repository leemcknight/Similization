using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Similization.Objects;
using LJM.Similization.DataObjects;
using LJM.Similization.DataObjects.Relational;

namespace LJM.SimilizationRulesEditor
{
	/// <summary>
	/// Summary description for frmGameWindow.
	/// </summary>
	public class frmGameWindow : System.Windows.Forms.Form
	{
		private System.Windows.Forms.StatusBar _statusBar;
		private System.Windows.Forms.StatusBarPanel _statusPanel;
		private System.Windows.Forms.StatusBarPanel _yearPanel;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.ToolBar _toolBar;
		private System.Windows.Forms.MainMenu _menu;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem _saveGameMenuItem;
		private System.Windows.Forms.MenuItem _quickSaveMenuItem;
		private LJM.SimilizationRulesEditor.GameWindow _gameWindow;
		private System.Windows.Forms.Splitter _splitter;
		private System.Windows.Forms.TextBox _consoleTextBox;
		private GameRoot _gameRoot;
		private System.Windows.Forms.StatusBarPanel _movesLeftPanel;
		private System.Windows.Forms.StatusBarPanel _governmentPanel;
		private System.Windows.Forms.StatusBarPanel _technologyPanel;
		private System.Windows.Forms.ImageList _imageList;
		private System.Windows.Forms.ToolBarButton _saveButton;
		private System.Windows.Forms.StatusBarPanel _unitPanel;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.ComponentModel.IContainer components;

		public frmGameWindow()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			GameRoot root = GameRoot.GetGameRoot();

			root.ActiveUnitChanged += new System.EventHandler(ActiveUnitChanged);

		}

		public void StartGame(Civilization playerCivilization, ArrayList enemies, string leaderName, string worldSizeString)
		{
			WorldSize worldSize;

			worldSize = WorldSize.Tiny;
			_gameWindow.StatusChanged += new StatusChangedEventHandler(OnGameStatusChanged);

			switch(worldSizeString)
			{
				case "Huge":
					worldSize= WorldSize.Huge;
					break;
				case "Large":
					worldSize = WorldSize.Large;
					break;
				case "Small":
					worldSize = WorldSize.Small;
					break;
				case "Standard":
					worldSize = WorldSize.Standard;
					break;
				case "Tiny":
					worldSize= WorldSize.Tiny;
					break;
			}
			_gameWindow.StartGame(playerCivilization, enemies, leaderName, worldSize);
			_gameRoot = _gameWindow.GameRoot;
			_gameRoot.PlayerColony.TechnologyAcquired += new EventHandler(TechnologyAcquired);
			_gameRoot.PlayerColony.TradeProposed += new TradeProposedEventHandler(TradeProposed);
			_yearPanel.Text = _gameRoot.Year.ToString();
			_governmentPanel.Text = _gameRoot.PlayerColony.Government.Name;
			_technologyPanel.Text = "Researching " + _gameRoot.PlayerColony.ResearchedTech.Name;
			
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		private void TechnologyAcquired(object sender, System.EventArgs e)
		{
			_technologyPanel.Text = "Researching " + _gameRoot.PlayerColony.ResearchedTech.Name;
		}

		private void TradeProposed(object sender, TradeProposedEventArgs e)
		{
			MessageBox.Show("Trade Proposed...");
		}

		private void OnGameStatusChanged(object sender, StatusChangedEventArgs e)
		{
			_consoleTextBox.Text = _consoleTextBox.Text + e.Message + System.Environment.NewLine;
			_consoleTextBox.SelectionStart = _consoleTextBox.Text.Length;
			_consoleTextBox.ScrollToCaret();
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmGameWindow));
			this._statusBar = new System.Windows.Forms.StatusBar();
			this._statusPanel = new System.Windows.Forms.StatusBarPanel();
			this._movesLeftPanel = new System.Windows.Forms.StatusBarPanel();
			this._governmentPanel = new System.Windows.Forms.StatusBarPanel();
			this._technologyPanel = new System.Windows.Forms.StatusBarPanel();
			this._unitPanel = new System.Windows.Forms.StatusBarPanel();
			this._yearPanel = new System.Windows.Forms.StatusBarPanel();
			this._menu = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this._saveGameMenuItem = new System.Windows.Forms.MenuItem();
			this._quickSaveMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this._toolBar = new System.Windows.Forms.ToolBar();
			this._saveButton = new System.Windows.Forms.ToolBarButton();
			this._imageList = new System.Windows.Forms.ImageList(this.components);
			this._gameWindow = new LJM.SimilizationRulesEditor.GameWindow();
			this._splitter = new System.Windows.Forms.Splitter();
			this._consoleTextBox = new System.Windows.Forms.TextBox();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this._statusPanel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._movesLeftPanel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._governmentPanel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._technologyPanel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._unitPanel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._yearPanel)).BeginInit();
			this.SuspendLayout();
			// 
			// _statusBar
			// 
			this._statusBar.Location = new System.Drawing.Point(0, 473);
			this._statusBar.Name = "_statusBar";
			this._statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						  this._statusPanel,
																						  this._movesLeftPanel,
																						  this._governmentPanel,
																						  this._technologyPanel,
																						  this._unitPanel,
																						  this._yearPanel});
			this._statusBar.ShowPanels = true;
			this._statusBar.Size = new System.Drawing.Size(736, 24);
			this._statusBar.TabIndex = 0;
			this._statusBar.Text = "statusBar1";
			// 
			// _statusPanel
			// 
			this._statusPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this._statusPanel.Text = "Ready...";
			this._statusPanel.Width = 220;
			// 
			// _movesLeftPanel
			// 
			this._movesLeftPanel.Text = "0 Moves Left";
			// 
			// _technologyPanel
			// 
			this._technologyPanel.Text = "Researching:";
			// 
			// _yearPanel
			// 
			this._yearPanel.Text = "5000 B.C.";
			// 
			// _menu
			// 
			this._menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				  this.menuItem1,
																				  this.menuItem8});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem3,
																					  this.menuItem2,
																					  this._saveGameMenuItem,
																					  this._quickSaveMenuItem,
																					  this.menuItem5,
																					  this.menuItem4});
			this.menuItem1.Text = "&File";
			// 
			// _saveGameMenuItem
			// 
			this._saveGameMenuItem.Index = 2;
			this._saveGameMenuItem.Text = "&Save Game...";
			// 
			// _quickSaveMenuItem
			// 
			this._quickSaveMenuItem.Index = 3;
			this._quickSaveMenuItem.Text = "&Quick Save";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 4;
			this.menuItem5.Text = "-";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 5;
			this.menuItem4.Text = "&Exit";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 1;
			this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem9,
																					  this.menuItem10,
																					  this.menuItem11});
			this.menuItem8.Text = "&View";
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 0;
			this.menuItem9.Text = "&Toolbar";
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 1;
			this.menuItem10.Text = "&Status Bar";
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 2;
			this.menuItem11.Text = "&Game Console";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = -1;
			this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem7});
			this.menuItem6.Text = "&Help";
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 0;
			this.menuItem7.Text = "&Help";
			// 
			// _toolBar
			// 
			this._toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this._saveButton});
			this._toolBar.DropDownArrows = true;
			this._toolBar.ImageList = this._imageList;
			this._toolBar.Name = "_toolBar";
			this._toolBar.ShowToolTips = true;
			this._toolBar.Size = new System.Drawing.Size(736, 25);
			this._toolBar.TabIndex = 1;
			this._toolBar.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this._toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this._toolBar_ButtonClick);
			// 
			// _saveButton
			// 
			this._saveButton.ImageIndex = 0;
			// 
			// _imageList
			// 
			this._imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this._imageList.ImageSize = new System.Drawing.Size(16, 16);
			this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
			this._imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// _gameWindow
			// 
			this._gameWindow.Dock = System.Windows.Forms.DockStyle.Top;
			this._gameWindow.Location = new System.Drawing.Point(0, 25);
			this._gameWindow.Name = "_gameWindow";
			this._gameWindow.Size = new System.Drawing.Size(736, 371);
			this._gameWindow.TabIndex = 6;
			this._gameWindow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmGameWindow_KeyDown);
			this._gameWindow.DoubleClick += new System.EventHandler(this._gameWindow_DoubleClick);
			// 
			// _splitter
			// 
			this._splitter.Dock = System.Windows.Forms.DockStyle.Top;
			this._splitter.Location = new System.Drawing.Point(0, 396);
			this._splitter.Name = "_splitter";
			this._splitter.Size = new System.Drawing.Size(736, 5);
			this._splitter.TabIndex = 7;
			this._splitter.TabStop = false;
			// 
			// _consoleTextBox
			// 
			this._consoleTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._consoleTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._consoleTextBox.Location = new System.Drawing.Point(0, 401);
			this._consoleTextBox.Multiline = true;
			this._consoleTextBox.Name = "_consoleTextBox";
			this._consoleTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._consoleTextBox.Size = new System.Drawing.Size(736, 72);
			this._consoleTextBox.TabIndex = 8;
			this._consoleTextBox.Text = "";
			this._consoleTextBox.WordWrap = false;
			this._consoleTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmGameWindow_KeyDown);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "&Load Game...";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 0;
			this.menuItem3.Text = "&New Game...";
			// 
			// frmGameWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(736, 497);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this._consoleTextBox,
																		  this._splitter,
																		  this._gameWindow,
																		  this._statusBar,
																		  this._toolBar});
			this.Menu = this._menu;
			this.Name = "frmGameWindow";
			this.Text = "Similization Game";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmGameWindow_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this._statusPanel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._movesLeftPanel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._governmentPanel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._technologyPanel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._unitPanel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._yearPanel)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void frmGameWindow_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			GameRoot root = GameRoot.GetGameRoot();
			e.Handled = true;
			switch(e.KeyCode)
			{
				case Keys.NumPad3:
					if(root.ActiveUnit != null)
					{
						root.ActiveUnit.MoveTo((GridCell)root.ActiveUnit.Location.BottomCell);
					}
					break;
				case Keys.NumPad7:
					if(root.ActiveUnit != null)
					{
						root.ActiveUnit.MoveTo((GridCell)root.ActiveUnit.Location.TopCell);
					}
					break;
				case Keys.NumPad1:
					if(root.ActiveUnit != null)
					{
						root.ActiveUnit.MoveTo((GridCell)root.ActiveUnit.Location.LeftCell);
					}
					break;
				case Keys.NumPad9:
					if(root.ActiveUnit != null)
					{
						root.ActiveUnit.MoveTo((GridCell)root.ActiveUnit.Location.RightCell);
					}
					break;
				case Keys.NumPad4:
					if(root.ActiveUnit != null)
					{
						root.ActiveUnit.MoveTo((GridCell)root.ActiveUnit.Location.TopCell.LeftCell);
					}
					break;
				case Keys.NumPad6:
					if(root.ActiveUnit != null)
					{
						root.ActiveUnit.MoveTo((GridCell)root.ActiveUnit.Location.RightCell.BottomCell);
					}
					break;
				case Keys.NumPad2:
					if(root.ActiveUnit != null)
					{
						root.ActiveUnit.MoveTo((GridCell)root.ActiveUnit.Location.BottomCell.LeftCell);
					}
					break;
				case Keys.NumPad8:
					if(root.ActiveUnit != null)
					{
						root.ActiveUnit.MoveTo((GridCell)root.ActiveUnit.Location.TopCell.RightCell);
					}
					break;
				case Keys.B:
					BuildNewCity();
					break;
				case Keys.I:
					Irrigate();
					break;
				case Keys.Space:
					_gameRoot.DoTurn();
					break;
				case Keys.R:
					BuildNewRoad();
					break;
				case Keys.M:
					BuildMine();
					break;
			}

			if(_gameRoot.Year > 0)
			{
				_yearPanel.Text = 
					Convert.ToInt32(Math.Abs(_gameRoot.Year)).ToString() + " A.D.";
			}
			else
			{
				_yearPanel.Text = 
					Convert.ToInt32(Math.Abs(_gameRoot.Year)).ToString() + " B.C.";
			}
			
			if(root.ActiveUnit != null)
			{
				_movesLeftPanel.Text = 
					root.ActiveUnit.MovesLeft.ToString() + " moves left";

				if(root.ActiveUnit.MovesLeft == 0)
				{
					if(root.GetNextUnit() == null)
					{
						_gameRoot.DoTurn();
					}
				}
			}
			else if (e.KeyCode != Keys.Space)
			{
				_gameRoot.DoTurn();
			}
			
			_gameWindow.Refresh();
			e.Handled = true;
		}

		private void BuildNewRoad()
		{
			if(_gameRoot.ActiveUnit != null)
			{
				if(_gameRoot.ActiveUnit.GetType() == typeof(Worker))
				{
					((Worker)_gameRoot.ActiveUnit).BuildRoad();
				}
			}
		}

		private void Irrigate()
		{
			if(_gameRoot.UnitIsWorker(_gameRoot.ActiveUnit))
			{
				((Worker)_gameRoot.ActiveUnit).Irrigate();
			}
		}

		private void BuildMine()
		{
			if(_gameRoot.UnitIsWorker(_gameRoot.ActiveUnit))
			{
				((Worker)_gameRoot.ActiveUnit).Mine();
			}
		}

		private void BuildNewCity()
		{
			frmNewCityWindow newCityWindow;
			Settler settler;
			City newCity;
			string newCityName;
			
			if(_gameRoot.ActiveUnit != null)
			{
				if(_gameRoot.ActiveUnit.GetType() == typeof(Settler))
				{
					newCityWindow = new frmNewCityWindow();
					settler = (Settler)_gameRoot.ActiveUnit;
					if(newCityWindow.ShowDialog(this) == DialogResult.OK)
					{
						newCityName = newCityWindow.CityName;
						newCity = settler.Settle(newCityName);
						newCity.ImprovementBuilt += 
							new ImprovementBuiltEventHandler(this.NewImprovementBuilt);
						newCity.Starved +=
							new EventHandler(this.CityStarvation);
					}
				}
			}
		}

		private void NewImprovementBuilt(object sender, ImprovementBuiltEventArgs e)
		{
			frmImprovementBuilt improvementBuiltWindow;

			improvementBuiltWindow = new frmImprovementBuilt(e);
			improvementBuiltWindow.ShowDialog(this);
		}

		private void CityStarvation(object sender, System.EventArgs e)
		{
			City starvedCity = (City)sender;
			string message = String.Format("Starvation at {0}!", starvedCity.Name);
			MessageBox.Show(message,"Similization",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		private void _gameWindow_DoubleClick(object sender, System.EventArgs e)
		{
			GridCell gridCell;
			Point absolute;
			Point relative;

			absolute = Cursor.Position;
			relative = _gameWindow.PointToClient(absolute);
			gridCell = _gameWindow.UnTranslate(relative);	
			if(gridCell == null)
			{
				return;
			}

			if(gridCell.City != null)
			{
				frmCityDetails cityDetailWindow;

				cityDetailWindow = new frmCityDetails(gridCell.City);
				cityDetailWindow.ShowDialog(this);
			}
		}

		private void _toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button == _saveButton)
			{
				string fileName;
				SaveFileDialog dialog;
				
				dialog = new SaveFileDialog();
				dialog.Title = "Save Game";
				
				if(dialog.ShowDialog(this) == DialogResult.OK)
				{
					fileName = dialog.FileName;
					_gameRoot.SaveGame(fileName);
				}
			}
		}

		private void ActiveUnitChanged(object sender, System.EventArgs e)
		{
			if(_gameRoot.ActiveUnit != null)
			{
				_unitPanel.Text = _gameRoot.ActiveUnit.Name;
			}
			else
			{
				_unitPanel.Text = "No Unit";
			}
		}
	}
}
