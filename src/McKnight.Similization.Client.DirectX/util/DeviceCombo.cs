using System;
using System.Drawing;
using System.Collections;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Util
{
	/// <summary>
	/// A combination of adapter format, back buffer format, and windowed/fullscreen 
	/// that is compatible with a particular D3D device (and the app)
	/// </summary>
	public class DeviceCombo
	{
		public int AdapterOrdinal;
		public DeviceType DevType;
		public Format AdapterFormat;
		public Format BackBufferFormat;
		public bool IsWindowed;
		public ArrayList DepthStencilFormatList = new ArrayList(); // List of D3DFORMATs
		public ArrayList MultiSampleTypeList = new ArrayList(); // List of D3DMULTISAMPLE_TYPEs
		public ArrayList MultiSampleQualityList = new ArrayList(); // List of ints (maxQuality per multisample type)
		public ArrayList DepthStencilMultiSampleConflictList = new ArrayList(); // List of DepthStencilMultiSampleConflicts
		public ArrayList VertexProcessingTypeList = new ArrayList(); // List of VertexProcessingTypes
		public ArrayList PresentIntervalList = new ArrayList(); // List of D3DPRESENT_INTERVALs
	}
}
