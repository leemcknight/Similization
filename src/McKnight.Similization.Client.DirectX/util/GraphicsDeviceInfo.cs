using System;
using System.Drawing;
using System.Collections;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Util
{
	/// <summary>
	/// Info about a D3D device, including a list of DeviceCombos (see below) 
	/// that work with the device
	/// </summary>
	public class GraphicsDeviceInfo
	{
		public int AdapterOrdinal;
		public DeviceType DevType;
		public Capabilities Caps;
		public ArrayList DeviceComboList = new ArrayList(); // List of D3DDeviceCombos
		public override string ToString() { return DevType.ToString(); }
	}
}
