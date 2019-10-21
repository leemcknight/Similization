using System;
using System.Drawing;
using System.Collections;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Util
{
	/// <summary>
	/// Used to sort Displaymodes
	/// </summary>
	class DisplayModeComparer : System.Collections.IComparer
	{


		/// <summary>
		/// Compare two display modes
		/// </summary>
		public int Compare(object x, object y)
		{
			DisplayMode dx = (DisplayMode)x;
			DisplayMode dy = (DisplayMode)y;

			if (dx.Width > dy.Width)
				return 1;
			if (dx.Width < dy.Width)
				return -1;
			if (dx.Height > dy.Height)
				return 1;
			if (dx.Height < dy.Height)
				return -1;
			if (dx.Format > dy.Format)
				return 1;
			if (dx.Format < dy.Format)
				return -1;
			if (dx.RefreshRate > dy.RefreshRate)
				return 1;
			if (dx.RefreshRate < dy.RefreshRate)
				return -1;
			return 0;
		}
	}
}
