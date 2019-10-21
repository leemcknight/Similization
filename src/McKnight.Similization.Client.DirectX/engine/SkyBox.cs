using System;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Engine
{
	/// <summary>
	/// A Box providing the boundaries of the 3D environment.
	/// </summary>
	public class SkyBox : ViewableObject
	{
		private SkyFace[] skyFaces;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkyBox"/> class.
        /// </summary>
        /// <param name="frontFace"></param>
        /// <param name="rightFace"></param>
        /// <param name="backFace"></param>
        /// <param name="leftFace"></param>
        /// <param name="topFace"></param>
        /// <param name="bottomFace"></param>
		public SkyBox(string frontFace, string rightFace, string backFace,
			string leftFace, string topFace, string bottomFace) : base()
		{
			this.skyFaces = new SkyFace[6];	
			this.skyFaces[0] = new SkyFace(frontFace,SkyFaceSide.Front);
			this.skyFaces[1] = new SkyFace(leftFace,SkyFaceSide.Left);
			this.skyFaces[2] = new SkyFace(backFace,SkyFaceSide.Back);
			this.skyFaces[3] = new SkyFace(rightFace,SkyFaceSide.Right);
			this.skyFaces[4] = new SkyFace(topFace,SkyFaceSide.Top);
			this.skyFaces[5] = new SkyFace(bottomFace,SkyFaceSide.Bottom);
		}

        /// <summary>
        /// Releases all resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed && disposing)
                {
                    foreach (SkyFace face in this.skyFaces)
                        face.Dispose();
                }
            }
            finally
            {
                this.disposed = true;
                base.Dispose(disposing);
            }
        }

		/// <summary>
		/// Renders the SkyBox to the camera.
		/// </summary>
		/// <param name="camera"></param>
		public override void Render(Camera camera)
		{						
			Matrix worldMatrix = Matrix.Identity;
			Matrix viewMatrix = camera.ViewMatrix;			
			GameEngine.Device.Transform.View = viewMatrix;
            GameEngine.Device.Transform.World = worldMatrix;

			//we need to disable z buffering for the skybox, since 
			//it represents the horizon, and by definition, everything
			//is in front of the horizon.
			GameEngine.Device.RenderState.ZBufferWriteEnable = false;
			GameEngine.Device.RenderState.CullMode = Cull.None;

			float cameraPitch = camera.CalculatePitch();
			float cameraHeading = camera.CalculateHeading();

			if(cameraPitch > 0.0f)			
				this.skyFaces[4].Render();			
			else if(cameraPitch < 0.0f)
				this.skyFaces[5].Render();
			
			if(cameraHeading > 0f && cameraHeading < 180f)			
				this.skyFaces[1].Render();			
			if(cameraHeading > 270f || cameraHeading < 90f)			
				this.skyFaces[0].Render();			
			if(cameraHeading > 180f && cameraHeading < 360f)			
				this.skyFaces[3].Render();			
			if(cameraHeading > 90f && cameraHeading < 270f)			
				this.skyFaces[2].Render();			
			GameEngine.Device.Transform.View = camera.ViewMatrix;
			GameEngine.Device.RenderState.ZBufferWriteEnable = true;
		}
	}
}
