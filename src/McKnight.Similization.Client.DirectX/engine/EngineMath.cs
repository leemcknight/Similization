using System;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Engine
{
	/// <summary>
	/// Math functions for the LJM engine
	/// </summary>
	public static class EngineMath
	{
        /// <summary>
        /// Computes the normal vector to the 3 specified vectors.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
		public static Vector3 ComputeFaceNormal(Vector3 p1, Vector3 p2, Vector3 p3)
		{
			Vector3 normalVector;

			Vector3 V1 = Vector3.Subtract(p1,p2);
			Vector3 V2 = Vector3.Subtract(p3,p1);
			normalVector = Vector3.CrossProduct( V1, V2 );
			normalVector.Normalize();

			return normalVector;
		}

        /// <summary>
        /// Determines whether the specified point is inside the bounds of the 
        /// specified rectangle.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
		public static bool InRect(Rectangle rect, Vector3 pt)
		{
			bool inside =  pt.X >= rect.Left && pt.X <= rect.Right && 
				pt.Z >= rect.Bottom && pt.Z <= rect.Top;

			return inside;
		}
	}
}
