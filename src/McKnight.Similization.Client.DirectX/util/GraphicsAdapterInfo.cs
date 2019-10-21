using System;
using System.Drawing;
using System.Collections;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Util
{
	/// <summary>
	/// Info about a display adapter
	/// </summary>
	public class GraphicsAdapterInfo
	{
		public int AdapterOrdinal;
		public AdapterDetails AdapterDetails;
		public ArrayList DisplayModeList = new ArrayList(); // List of D3DDISPLAYMODEs
		public ArrayList DeviceInfoList = new ArrayList(); // List of D3DDeviceInfos		
	}
}
