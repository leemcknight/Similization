using System;
using System.Drawing;
using System.Windows.Forms;
using McKnight.Similization.Client;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// Windows client implementation of the Similization Status View.
	/// </summary>
	public class WinClientStatusView : Control, ISimilizationStatusView
	{
		private string status;
		private string year;
		private string technology;
		private string movesLeft;
		private string gold;
		private string government;
		private string activeUnit;
		private string terrain;
		private Image unitImage;
		private System.ComponentModel.IContainer components = null;
				
		/// <summary>
		/// Initializes a new instance of the <see cref="WinClientStatusView"/> class.
		/// </summary>
		public WinClientStatusView()
		{
			SetStyle(
				ControlStyles.UserPaint | 
				ControlStyles.AllPaintingInWmPaint | 
				ControlStyles.OptimizedDoubleBuffer,
				true);
			this.UpdateStyles();
		}

		/// <summary>
		/// Paints the control.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
            int yOffset = CalcVerticalTextOffset(g, activeUnit);
            int yBase = 10;
			g.DrawString(activeUnit, this.Font, SystemBrushes.ControlText, new Point(10,yBase), StringFormat.GenericDefault);
            g.DrawString(movesLeft, this.Font, SystemBrushes.ControlText, new Point(10, yBase + yOffset), StringFormat.GenericDefault);
            g.DrawString(terrain, this.Font, SystemBrushes.ControlText, new Point(10, yBase + (2*yOffset)), StringFormat.GenericDefault);
            g.DrawString(year, this.Font, SystemBrushes.ControlText, new Point(10, yBase + (3 * yOffset)), StringFormat.GenericDefault);
            g.DrawString(gold, this.Font, SystemBrushes.ControlText, new Point(10, yBase + (4 * yOffset)), StringFormat.GenericDefault);
            g.DrawString(technology, this.Font, SystemBrushes.ControlText, new Point(10, yBase + (5 * yOffset)), StringFormat.GenericDefault);
            g.DrawString(government, this.Font, SystemBrushes.ControlText, new Point(10, yBase + (6*yOffset)), StringFormat.GenericDefault);
			
			if(this.unitImage != null)
				g.DrawImage(this.unitImage, new Point(150,10));
		}

        private int CalcVerticalTextOffset(Graphics g, string text)
        {
            return g.MeasureString(text, this.Font).ToSize().Height;
        }

		/// <summary>
		/// Gets or sets the value to show as the status in the game.
		/// </summary>
		public string Status
		{
			get { return this.status; }
			set 
			{ 
				this.status = value; 
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the value to show in the <i>Year</i> field of the <see cref="WinClientStatusView"/>.
		/// </summary>
		public string Year
		{
			get { return this.year; }
			set 
			{ 
				this.year = value; 
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the name of the terrain the active unit is on.
		/// </summary>
		public string Terrain
		{
			get { return this.terrain; }
			set
			{
				this.terrain = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the value to show in the <i>Technolgy</i> field of the <c>WinClientStatusView</c>.
		/// </summary>
		public string Technology
		{
			get { return this.technology; }
			set 
			{ 
				this.technology = value; 
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the value to show in the <i>MovesLeft</i> field of the <c>WinClientStatusView</c>.
		/// </summary>
		public string MovesLeft
		{
			get { return this.movesLeft; }
			set 
			{ 
				this.movesLeft = value; 
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the value 
		/// </summary>
		public string Gold
		{
			get { return this.gold; }
			set 
			{ 
				this.gold = value; 
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the value to show in the <i>Government</i> field of the <c>WinClientStatusView</c>.
		/// </summary>
		public string Government
		{
			get { return this.government; }
			set 
			{ 
				this.government = value; 
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets a string representation of the active unit in the game.
		/// </summary>
		public string ActiveUnit
		{
			get { return this.activeUnit; }
			set 
			{ 
				this.activeUnit = value; 
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="System.Drawing.Image"/> to display for the active unit.
		/// </summary>
		public Image UnitImage
		{
			get { return this.unitImage; }
			set 
			{
				this.unitImage = value;
				Invalidate();
			}
		}
	}
}
