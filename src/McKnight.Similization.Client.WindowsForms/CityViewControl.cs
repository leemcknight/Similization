using System;
using System.Drawing;
using System.Windows.Forms;
using McKnight.Similization.Client;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// This is the control used in the city detail screen to show the city 
	/// and surrounding cells.
	/// </summary>
	internal class CityViewControl : GridViewBase
	{
		private System.Windows.Forms.ImageList imageList;
		private System.ComponentModel.IContainer components;

        private readonly int Gold = 0;
		private readonly int Shield = 1;
		private readonly int Food = 2;		
		private readonly int ContentPerson = 4;
		private readonly int HappyPerson = 5;
		private readonly int SadPerson = 6;
		private readonly int Entertainer = 7;
		private readonly int Scientist = 8;
		private readonly int TaxMan = 9;
		
	
		/// <summary>
		/// Initializes a new instance of the <c>CityViewControl</c>.
		/// </summary>
		public CityViewControl()
		{
			SetStyle(
				ControlStyles.UserPaint | 
				ControlStyles.AllPaintingInWmPaint | 
				ControlStyles.OptimizedDoubleBuffer,
				true);
			this.UpdateStyles();
			InitializeComponent();
		}
	
		private City city;

		/// <summary>
		/// Gets or sets the city drawn on the control.
		/// </summary>
		public City City
		{
			get { return this.city; }
			set 
			{
				this.city = value; 
				if(this.city != null)
				{
					this.CenterCoordinates = this.city.Coordinates;
					Invalidate();
				}
			}
		}

		/// <summary>
		/// Draws the contents of the control.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{						
			if(this.city == null)			
				return;						
			DrawPopulation(e.Graphics);
		}

		/// <summary>
		/// Handles the clicking of the mouse on the control.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			Point coordinates = this.GridPainter.TranslateScreenCoordinatesToGridCoordinates(new Point(e.X, e.Y));
			if(this.city.UsedCells.Contains(coordinates))			
				this.city.RemoveWorkedCell(coordinates);			
			else			
				this.city.AddWorkedCell(coordinates);			
			Refresh();
			base.OnMouseDown(e);
		}


		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CityViewControl));
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Fuchsia;

		}

		private void DrawPopulation(Graphics g)
		{									
			int x = 10;
			int y = Height - 35;

			for(int i = 1; i <= this.city.HappyPeople; i++)
			{
				g.DrawImage(this.imageList.Images[HappyPerson], x, y);
				x += this.imageList.ImageSize.Width;
			}

			for(int j = 1; j <= this.city.ContentPeople; j++)
			{
				g.DrawImage(this.imageList.Images[ContentPerson], x,y);
                x += this.imageList.ImageSize.Width;
			}

			for(int k = 1; k <= this.city.SadPeople; k++)
			{
				g.DrawImage(this.imageList.Images[SadPerson], x,y);
                x += this.imageList.ImageSize.Width;
			}

			for(int l = 1; l <= this.city.TaxCollectors; l++)
			{
				g.DrawImage(this.imageList.Images[TaxMan], x,y);
                x += this.imageList.ImageSize.Width;
			}

			for(int m = 1; m <= this.city.Entertainers; m++)
			{
				g.DrawImage(this.imageList.Images[Entertainer], x,y);
                x += this.imageList.ImageSize.Width;
			}

			for(int n = 1; n <= this.city.Scientists; n++)
			{
				g.DrawImage(this.imageList.Images[Scientist], x,y);
                x += this.imageList.ImageSize.Width;
			}
		}
	}
}
