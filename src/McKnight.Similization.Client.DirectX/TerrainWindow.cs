using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using LJM.Similization.Server;
using LJM.Similization.Client.DirectX.Controls;

namespace LJM.Similization.Client.DirectX
{
	/// <summary>
	/// Summary description for TerrainWindow.
	/// </summary>
	public class TerrainWindow : DXWindow
	{
		private DXButton _doneButton;
		private DXLabel _lblTerrain;
		private GridCell _cell;
		private DXLabel _lblFoodHDR;
		private DXLabel _lblFood;
		private DXLabel _lblShieldsHDR;
		private DXLabel _lblShields;
		private DXLabel _lblGoldHDR;
		private DXLabel _lblGold;

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainWindow"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
		public TerrainWindow(IDirectXControlHost controlHost) : base(controlHost)
		{
			
		}

        /// <summary>
        /// Intializes a new instance of the <see cref="TerrainWindow"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        /// <param name="parent"></param>
        /// <param name="cell"></param>
		public TerrainWindow(IDirectXControlHost controlHost, DXControl parent, GridCell cell) : base(controlHost)
		{
			_cell = cell;
			this.Parent = parent;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.Location = new Point(50,50);
			this.Size = new Size(300,200);
			this.BackColor = SystemColors.Control;
			this.ForeColor = SystemColors.ActiveCaption;
			this.Text = "Terrain View";

			_doneButton = new DXButton(this.ControlHost, this);
			_doneButton.Text = "Done";
			_doneButton.Size = new Size(75,25);
			_doneButton.Location = new Point(200, 150);
			_doneButton.Click += new System.EventHandler(this.DonePressed);
			this.Controls.Add(_doneButton);

			_lblTerrain = new DXLabel(this.ControlHost, this);
			_lblTerrain.Text = _cell.Terrain.Name;
			_lblTerrain.Location = new Point(20,50);
			this.Controls.Add(_lblTerrain);

			_lblFoodHDR = new DXLabel(this.ControlHost, this);
			_lblFoodHDR.Text = "Food:";
			_lblFoodHDR.Location = new Point(100,50);
			this.Controls.Add(_lblFoodHDR);

			_lblFood = new DXLabel(this.ControlHost, this);
			_lblFood.Text = _cell.FoodUnits.ToString();
			_lblFood.Location = new Point(150, 50);
			this.Controls.Add(_lblFood);

			_lblGoldHDR = new DXLabel(this.ControlHost, this);
			_lblGoldHDR.Text = "Gold:";
			_lblGoldHDR.Location = new Point(100, 75);
			this.Controls.Add(_lblGoldHDR);

			_lblGold = new DXLabel(this.ControlHost, this);
			_lblGold.Text = _cell.GoldPerTurn.ToString();
			_lblGold.Location = new Point(150, 75);
			this.Controls.Add(_lblGold);

			_lblShieldsHDR = new DXLabel(this.ControlHost, this);
			_lblShieldsHDR.Text = "Shields:";
			_lblShieldsHDR.Location = new Point(100, 100);
			this.Controls.Add(_lblShieldsHDR);

			_lblShields = new DXLabel(this.ControlHost, this);
			_lblShields.Text = _cell.Shields.ToString();
			_lblShields.Location = new Point(150, 100);
			this.Controls.Add(_lblShields);

		}

		private void DonePressed(object sender, System.EventArgs e)
		{
			this.Hide();
		}
	}
}
