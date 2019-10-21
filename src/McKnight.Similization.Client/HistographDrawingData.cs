using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// Contains properties and methods that are used to represent drawing information 
	/// for the Histograph in Similization.
	/// </summary>
	public class HistographDrawingData
	{
		private History _hist;
		private Size _viewSize;
        private HistographView _view;

		/// <summary>
		/// Initializes a new instance of the <see cref="HistographDrawingData"/> class.
		/// </summary>
		/// <param name="history"></param>
		/// <param name="viewSize"></param>
		public HistographDrawingData(History history, Size viewSize)
		{
			_hist = history;
			_viewSize = viewSize;
		}
	
        /// <summary>
        /// Gets or sets the <see cref="HistographView"/> detailing the information 
        /// being displayed to the user.
        /// </summary>
        public HistographView View
        {
            get { return _view; }
            set
            {
                if (_view != value)
                {
                    _view = value;
                    LoadData();
                }
            }
        }

		private void LoadData()
		{
			HistoryItem[] items = _hist.GetAllHistory();
			HistographViewDataPoint hdp;
			int year = -4000;

			ClientApplication client = ClientApplication.Instance;
			int numCountries = client.ServerInstance.Countries.Count;
			int numYears = items.GetLength(0) / numCountries;
			float yDelta;
			
			if(numYears == 1)
			{
				yDelta = _viewSize.Height;
			}
			else
			{
                float h = Convert.ToSingle(_viewSize.Height);
                float years = Convert.ToSingle(numYears - 1);
				yDelta = h/years;
			}

			//y position of the data point
			float y = 0;		

			//x position of the data point
			float x = 0;

			//total value of all item values up to and including the country being processed
			int total = 0;

			//value of just the item value of the country being processed
			int single = 0;

			//index into the array of data points
			int idx = 0;

			int dataPointIdx = 0;

			HistographViewDataPoint[] viewDataPoints = new HistographViewDataPoint[numCountries];

			HistographViewDataPoint[] countryDataPoints;

			foreach(HistoryItem item in items)
			{
				if(year != item.Year)
				{
					//new year in the history data.  
					//increment the year counter and 
					//move the y pointer down delta amt.
					year = item.Year;
					x = 0f;
					idx = 0;
				
					foreach(HistographViewDataPoint pt in viewDataPoints)
					{
						if(total > 0)
						{
							pt.Width = (((float)pt.Value/(float)total)*((float)_viewSize.Width));
						}
						else
						{
							pt.Width = 0f;
						}

						pt.StartingPoint = new PointF(x,y);
						x += pt.Width;

						if(_countryDataPoints.ContainsKey(pt.Country))
						{
							countryDataPoints = _countryDataPoints[pt.Country]; 								
							countryDataPoints[dataPointIdx] = pt;
						}
						else
						{
							countryDataPoints = new HistographViewDataPoint[numYears];
							countryDataPoints[dataPointIdx] = pt;
							_countryDataPoints.Add(pt.Country, countryDataPoints);
						}
					}

					dataPointIdx++;
					total = 0;
					y += yDelta;
				}
				
				single = GetDataValue(item);

				hdp = new HistographViewDataPoint();
				hdp.Value = single;
				hdp.Country = item.Country;

				viewDataPoints[idx++] = hdp;
				total += single;
			
			}

			//take care of the last year
			x=0;
			foreach(HistographViewDataPoint pt in viewDataPoints)
			{
				if(total > 0)
				{
					pt.Width = (int)(((double)pt.Value/(double)total)*((double)_viewSize.Width));
				}
				else
				{
					pt.Width = 0;
				}

				pt.StartingPoint = new PointF(x,y);
				x += pt.Width;

				if(_countryDataPoints.ContainsKey(pt.Country))
				{
					countryDataPoints = _countryDataPoints[pt.Country] 
						as HistographViewDataPoint[];

						if(dataPointIdx <= countryDataPoints.GetUpperBound(0))
						{
							countryDataPoints[dataPointIdx] = pt;
						}
				}
				else
				{
					countryDataPoints = new HistographViewDataPoint[numYears];
					countryDataPoints[dataPointIdx] = pt;
					_countryDataPoints.Add(pt.Country, countryDataPoints);
				}
			}

			BuildRegions();
		}

		private Dictionary<Country, Region> _countryRegionData = new Dictionary<Country, Region>();
		private Dictionary<Country, HistographViewDataPoint[]> _countryDataPoints = new Dictionary<Country, HistographViewDataPoint[]>();

		private void BuildRegions()
		{
			ClientApplication client = ClientApplication.Instance;
			int bound;
			HistographViewDataPoint[] dataPoints;
			Region reg;
			GraphicsPath path;
			PointF[] pts;
			PointF pt;
			HistographViewDataPoint dataPoint;
			_countryRegionData = new Dictionary<Country, Region>();

			foreach(Country ctry in client.ServerInstance.Countries)
			{
				dataPoints = _countryDataPoints[ctry];
			
				bound = dataPoints.GetUpperBound(0);

				pts = new PointF[(bound+1)*2];
				int ptsBound = pts.GetUpperBound(0);
				path = new GraphicsPath();
				
				path.StartFigure();
				for(int i = 0; i <= bound; i++)
				{
					dataPoint = dataPoints[i];
					pt = 
						new PointF(dataPoint.StartingPoint.X + dataPoint.Width, 
						dataPoint.StartingPoint.Y);

					pts[i] = pt;
					pts[ptsBound-i] = dataPoint.StartingPoint;
				}

				path.AddPolygon(pts);
				path.CloseFigure();
				reg = new Region(path);
				_countryRegionData.Add(ctry, reg);
			}
		}

		/// <summary>
		/// Gets the histograph region for the specified country.
		/// </summary>
		/// <param name="country"></param>
		/// <returns></returns>
		public Region GetRegion(Country country)
		{
			return _countryRegionData[country];
		}

		/// <summary>
		/// Gets the value of the data point for the history item, based on the 
		/// current view of the histograph.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		private int GetDataValue(HistoryItem item)
		{
			int total = 0;

			switch(_view)
			{
				case HistographView.Culture:
					total = item.CulturePoints;
					break;
				case HistographView.Power:
					total = item.Power;
					break;
				case HistographView.Score:
					total = item.Score;
					break;
			}

			return total;
		}

		private class HistographViewDataPoint
		{
			private Country _country;

			public Country Country
			{
				get { return _country; }
				set { _country = value; }
			}

			private int _value; 

			public int Value
			{
				get { return _value; }
				set { _value = value; }
			}

			private PointF _startingPoint;

			public PointF StartingPoint
			{
				get { return _startingPoint; }
				set { _startingPoint = value; }
			}

			private float _width;

			public float Width
			{
				get { return _width; }
				set { _width = value; }
			}
		}
	}	

}
