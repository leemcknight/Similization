using System;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Generic;
using Microsoft.DirectX.Direct3D;
using LJM.Similization.Client.DirectX.Util;
using Microsoft.DirectX.Direct3D.CustomVertex;

namespace LJM.Similization.Client.DirectX.Engine
{
	/// <summary>
	/// An individual side to a skybox.
	/// </summary>
	public class SkyFace : IDisposable
	{
		private GraphicsBuffer<PositionNormalTextured> corners;
		private VertexBuffer vertexBuffer;
		private Texture texture;
		private bool valid;
        private bool disposed;

		public SkyFace(string textureName, SkyFaceSide faceSide)
		{
            this.vertexBuffer = VertexBuffer.CreateGeneric<PositionNormalTextured>(
                GameEngine.Device,
                4,
                Usage.WriteOnly,
                PositionNormalTextured.Format,
                Pool.Managed,
               null);
           
            this.corners = this.vertexBuffer.Lock<PositionNormalTextured>(0, 0, LockFlags.None);
            PositionNormalTextured[] data = new PositionNormalTextured[4];

			switch(faceSide)
			{                    
				case SkyFaceSide.Top:
					//Y always 100; 
					//back x -100; front X +100; 
					//back Z -100; front Z +100
					data[0].X = -100f;
                    data[0].Y = 100f;
                    data[0].Z = -100f;
                    data[0].U = 0f;
                    data[0].V = 1f;
                    data[0].Nx = 0f;
                    data[0].Ny = -1f;
                    data[0].Nz = 0f;
                    data[1].X = 100f;
                    data[1].Y = 100f;
                    data[1].Z = -100f;
                    data[1].U = 0f;
                    data[1].V = 0f;
                    data[1].Nx = 0f;
                    data[1].Ny = -1f;
                    data[1].Nz = 0f;
                    data[2].X = -100f;
                    data[2].Y = 100f;
                    data[2].Z = 100f;
                    data[2].U = 1f;
                    data[2].V = 1f;
                    data[2].Nx = 0;
                    data[2].Ny = -1f;
                    data[2].Nz = 0f;
                    data[3].X = 100f;
                    data[2].Y = 100f;
                    data[3].Z = 100f;
                    data[3].U = 1f;
                    data[3].V = 0f;
                    data[3].Nx = 0f;
                    data[3].Ny = -1f;
                    data[3].Nz = 0f;
					break;
				case SkyFaceSide.Bottom:
                    data[0].X = -100.0f; // nw
                    data[0].Y = -100.0f;
                    data[0].Z = 100.0f;
                    data[0].U = 0.0f;
                    data[0].V = 0.0f;
                    data[0].Nx = 0.0f;
                    data[0].Ny = 1.0f;
                    data[0].Nz = 0.0f;
                    data[1].X = 100.0f; // ne
                    data[1].Y = -100.0f;
                    data[1].Z = 100.0f;
                    data[1].U = 1.0f;
                    data[1].V = 1.0f;
                    data[1].Nx = 0.0f;
                    data[1].Ny = 1.0f;
                    data[1].Nz = 0.0f;
                    data[2].X = -100.0f; // sw
                    data[2].Y = -100.0f;
                    data[2].Z = -100.0f;
                    data[2].U = 1.0f;
                    data[2].V = 0.0f;
                    data[2].Nx = 0.0f;
                    data[2].Ny = 1.0f;
                    data[2].Nz = 0.0f;
                    data[3].X = 100.0f; // se
                    data[3].Y = -100.0f;
                    data[3].Z = -100.0f;
                    data[3].U = 0.0f;
                    data[3].V = 1.0f;
                    data[3].Nx = 0.0f;
                    data[3].Ny = 1.0f;
                    data[3].Nz = 0.0f;
					break;
				case SkyFaceSide.Left:
                    data[0].X = -100.0f; // upper south
                    data[0].Y = 100.0f;
                    data[0].Z = -100.0f;
                    data[0].U = 0.0f;
                    data[0].V = 0.0f;
                    data[0].Nx = 1.0f;
                    data[0].Ny = 0.0f;
                    data[0].Nz = 0.0f;
                    data[1].X = -100.0f; // upper north
                    data[1].Y = 100.0f;
                    data[1].Z = 100.0f;
                    data[1].U = 1.0f;
                    data[1].V = 0.0f;
                    data[1].Nx = 1.0f;
                    data[1].Ny = 0.0f;
                    data[1].Nz = 0.0f;
                    data[2].X = -100.0f; // lower south
                    data[2].Y = -100.0f;
                    data[2].Z = -100.0f;
                    data[2].U = 0.0f;
                    data[2].V = 1.0f;
                    data[2].Nx = 1.0f;
                    data[2].Ny = 0.0f;
                    data[2].Nz = 0.0f;
                    data[3].X = -100.0f; // lower north
                    data[3].Y = -100.0f;
                    data[3].Z = 100.0f;
                    data[3].U = 1.0f;
                    data[3].V = 1.0f;
                    data[3].Nx = 1.0f;
                    data[3].Ny = 0.0f;
                    data[3].Nz = 0.0f;
					break;
				case SkyFaceSide.Right:
                    data[0].X = 100.0f; // upper ne
                    data[0].Y = 100.0f;
                    data[0].Z = 100.0f;
                    data[0].U = 0.0f;
                    data[0].V = 0.0f;
                    data[0].Nx = -1.0f;
                    data[0].Ny = 0.0f;
                    data[0].Nz = 0.0f;
                    data[1].X = 100.0f; // upper se
                    data[1].Y = 100.0f;
                    data[1].Z = -100.0f;
                    data[1].U = 1.0f;
                    data[1].V = 0.0f;
                    data[1].Nx = -1.0f;
                    data[1].Ny = 0.0f;
                    data[1].Nz = 0.0f;
                    data[2].X = 100.0f; // lower ne
                    data[2].Y = -100.0f;
                    data[2].Z = 100.0f;
                    data[2].U = 0.0f;
                    data[2].V = 1.0f;
                    data[2].Nx = -1.0f;
                    data[2].Ny = 0.0f;
                    data[2].Nz = 0.0f;
                    data[3].X = 100.0f; // lower se
                    data[3].Y = -100.0f;
                    data[3].Z = -100.0f;
                    data[3].U = 1.0f;
                    data[3].V = 1.0f;
                    data[3].Nx = -1.0f;
                    data[3].Ny = 0.0f;
                    data[3].Nz = 0.0f;
					break;
				case SkyFaceSide.Front:
                    data[0].X = -100.0f; // upper nw
                    data[0].Y = 100.0f;
                    data[0].Z = 100.0f;
                    data[0].U = 0.0f;
                    data[0].V = 0.0f;
                    data[0].Nx = 0.0f;
                    data[0].Ny = 0.0f;
                    data[0].Nz = -1.0f;
                    data[1].X = 100.0f; // upper ne
                    data[1].Y = 100.0f;
                    data[1].Z = 100.0f;
                    data[1].U = 1.0f;
                    data[1].V = 0.0f;
                    data[1].Nx = 0.0f;
                    data[1].Ny = 0.0f;
                    data[1].Nz = -1.0f;
                    data[2].X = -100.0f; // lower nw
                    data[2].Y = -100.0f;
                    data[2].Z = 100.0f;
                    data[2].U = 0.0f;
                    data[2].V = 1.0f;
                    data[2].Nx = 0.0f;
                    data[2].Ny = 0.0f;
                    data[2].Nz = -1.0f;
                    data[3].X = 100.0f; // lower ne
                    data[3].Y = -100.0f;
                    data[3].Z = 100.0f;
                    data[3].U = 1.0f;
                    data[3].V = 1.0f;
                    data[3].Nx = 0.0f;
                    data[3].Ny = 0.0f;
                    data[3].Nz = -1.0f;
					break;
				case SkyFaceSide.Back:
                    data[0].X = 100.0f; // upper se
                    data[0].Y = 100.0f;
                    data[0].Z = -100.0f;
                    data[0].U = 0.0f;
                    data[0].V = 0.0f;
                    data[0].Nx = 0.0f;
                    data[0].Ny = 0.0f;
                    data[0].Nz = -1.0f;
                    data[1].X = -100.0f; // upper sw
                    data[1].Y = 100.0f;
                    data[1].Z = -100.0f;
                    data[1].U = 1.0f;
                    data[1].V = 0.0f;
                    data[1].Nx = 0.0f;
                    data[1].Ny = 0.0f;
                    data[1].Nz = -1.0f;
                    data[2].X = 100.0f; // lower se
                    data[2].Y = -100.0f;
                    data[2].Z = -100.0f;
                    data[2].U = 0.0f;
                    data[2].V = 1.0f;
                    data[2].Nx = 0.0f;
                    data[2].Ny = 0.0f;
                    data[2].Nz = -1.0f;
                    data[3].X = -100.0f; // lower sw
                    data[3].Y = -100.0f;
                    data[3].Z = -100.0f;
                    data[3].U = 1.0f;
                    data[3].V = 1.0f;
                    data[3].Nx = 0.0f;
                    data[3].Ny = 0.0f;
                    data[3].Nz = -1.0f;
					break;
			}

            this.corners.Write(data);
            this.vertexBuffer.Unlock();			
            this.texture = new Texture(GameEngine.Device, textureName);
			this.valid = true;									
		}

        /// <summary>
        /// Releases all resources.
        /// </summary>
		public void Dispose()
		{
            Dispose(true);
            GC.SuppressFinalize(this);
		}

        private void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed && disposing)
                {
                    if (this.texture != null)
                        this.texture.Dispose();

                    if (this.vertexBuffer != null)
                        this.vertexBuffer.Dispose();
                }
            }
            finally
            {
                this.disposed = true;
            }            
        }


        /// <summary>
        /// Determines whether the <see cref="SkyFace"/> is valid.
        /// </summary>
		public bool Valid
		{
			get { return this.valid; }
		}

        /// <summary>
        /// Renders the <see cref="SkyFace"/> onto the screen.
        /// </summary>
		public void Render()
		{
			if(!this.valid)			
				return;			
											
			GameEngine.Device.SetStreamSource(0, this.vertexBuffer, 0);
			GameEngine.Device.VertexFormat = PositionNormalTextured.Format;
			GameEngine.Device.SetTexture(0, this.texture);
			GameEngine.Device.DrawPrimitives(PrimitiveType.TriangleStrip,0,2);			
		}
	}

	public enum SkyFaceSide
	{
		Top,
		Bottom,
		Front,
		Right,
		Back,
		Left
	}
}
