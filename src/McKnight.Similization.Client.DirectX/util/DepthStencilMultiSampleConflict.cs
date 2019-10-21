using System;
using System.Drawing;
using System.Collections;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Util
{
	/// <summary>
	/// Info about a depth/stencil buffer format that is incompatible with a
	/// multisample type
	/// </summary>
	public class DepthStencilMultiSampleConflict
	{
		public DepthFormat DepthStencilFormat;
		public MultiSampleType MultiSampleType;
	}
}
