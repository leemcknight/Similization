using System;
using System.Drawing;

namespace LJM.SimilizationRulesEditor
{
	/// <summary>
	/// Summary description for GoodyHutImage.
	/// </summary>
	public class IsometricTileSet
	{
		private Bitmap _bitmap;

		public IsometricTileSet(string fileName)
		{
			_bitmap = new Bitmap(fileName);
		}

		public Bitmap GetIsometricTile(Point location)
		{
			
			
			return null;
		}
	}
}
