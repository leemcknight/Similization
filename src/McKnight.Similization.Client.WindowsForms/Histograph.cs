using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using McKnight.Similization.Client;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
	
	/// <summary>
	/// Control for displaying Similization Historgraph information.
	/// </summary>
	public class Histograph : Control
	{
		private HistographDrawingData _data;
		private bool _initialized;
        private HistographView _view = HistographView.Score;

		/// <summary>
		/// Loads the history into the Histograph control.
		/// </summary>
		/// <param name="history"></param>
		public void LoadHistory(History history)
		{
			_data = new HistographDrawingData(history, Size);
            _data.View = _view;
			_initialized = true;
		}

		/// <summary>
		/// Override of the <i>Paint</i> event handler.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if(!_initialized)			
				return;		
			Graphics g = e.Graphics;			
			ClientApplication client = ClientApplication.Instance;
			GameRoot root = client.ServerInstance;
			
			int x = 0;
			int xDelta = Width/root.Countries.Count;
			
			foreach(Country player in root.Countries)
			{
				g.FillRegion(new SolidBrush(player.Color), _data.GetRegion(player));				
				DrawPlayerName(g, player,x);
				x += xDelta;
			}
		}
		
		// Draws the player name at the specified x position on the control.		
		private void DrawPlayerName(Graphics g, Country player, int x)
		{			
			string s = player.Name;
			Size textSize = g.MeasureString(s, Font).ToSize();
			Rectangle rect = new Rectangle(new Point(x,0), textSize);
			g.FillRectangle(new SolidBrush(player.Color), rect);			
			g.DrawString(s, Font, Brushes.White, rect);			
		}

		
		/// <summary>
		/// Gets or sets the view of the Histograph.
		/// </summary>
		public HistographView View
		{
			get { return _view; }
			set 
			{
				_view = value;
				if(_data == null)				
					return;
                _data.View = _view;
				Invalidate();
			}
		}

	}
}
