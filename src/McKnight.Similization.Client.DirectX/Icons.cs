using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using LJM.Similization.Client.DirectX.Controls;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX
{
	public class Icons
	{
		public static DXImage StartMenuIcon 
		{
			get { return GetIcon(@"images\startIcon.bmp"); }
		}

		public static DXImage SaveIcon
		{
			get { return GetIcon(@"images\save.bmp"); }
		}

		public static DXImage NewIcon
		{
			get { return GetIcon(@"images\new.bmp"); }
		}

		public static DXImage LoadIcon
		{
			get { return GetIcon(@"images\open.bmp"); }
		}

		public static DXImage NetworkIcon
		{
			get { return GetIcon("network.bmp"); }
		}

		public static DXImage ExitGameIcon
		{
			get { return GetIcon(@"images\exit.bmp"); }
		}

		public static DXImage LoadGameIcon
		{
			get { return GetIcon(@"images\open.bmp"); }
		}

		public static DXImage HallOfFameIcon
		{
			get
			{ 
				return GetIcon(@"images\credits.bmp"); 
			}
		}

		public static DXImage QuickStartIcon
		{
			get { return GetIcon(@"images\quick.bmp"); }
		}

		public static DXImage CreditsIcon
		{
			get { return GetIcon(@"images\credits.bmp"); }
		}

		public static DXImage HelpIcon
		{
			get { return GetIcon(@"images\help.bmp"); }
		}

		private static DXImage GetIcon(string name)
		{
			DXImage image;
            Device d = ((DirectXClientApplication)DirectXClientApplication.Instance).Device;
			image = new DXImage(d, name);
			return image;
		}
	}
}
