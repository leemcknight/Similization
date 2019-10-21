using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.Generic;
using LJM.Similization.Client;
using LJM.Similization.Server;
using Microsoft.DirectX.Direct3D.CustomVertex;
using LJM.Similization.Client.DirectX.Util;

namespace LJM.Similization.Client.DirectX.Engine
{
	/// <summary>
	/// Represents the terrain of the 3D world
	/// </summary>
	public class Terrain : IDisposable
	{
        private bool disposed;
		private Vector3[,] elevations;
		private VertexBuffer vertexBuffer;
		private PositionNormalTextured[] vertices;
		private TerrainQuad[,] terrainQuads;
		private int width;
		private int height;
		private float spacing;
		private event EventHandler<StatusChangedEventArgs> _statusLoadEvent;
		private Grid grid;
        private int numVertices = 3000;
        /// <summary>
        /// Initializes a new instance of the <see cref="Terrain"/> class.
        /// </summary>
        /// <param name="grid"></param>
		public Terrain(Grid grid)
		{            
			this.grid = grid;
			this.elevations = new Vector3[grid.Size.Width,grid.Size.Height];
            width = grid.Size.Width - 1;
            height = grid.Size.Height - 1;
            this.numVertices = width * height * 6;
			this.terrainQuads = new TerrainQuad[this.width, this.height];
			this.vertices = new PositionNormalTextured[this.numVertices];
			this.spacing = 10.0f;            
		}

        ~Terrain()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all resources
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
                    for (int x = 0; x < this.width; x++)
                    {
                        for (int y = 0; y < this.height; y++)
                        {
                            if (this.terrainQuads[x, y] != null)
                                this.terrainQuads[x, y].Dispose();
                        }
                    }

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
        /// Builds up and initializes all the texture information for the map.
        /// </summary>
		public void Load(Tileset tileset)
		{
			long totalQuads = this.width * this.height;
			double scaledPercent=0;
			double nonScaledPercent;
            
			nonScaledPercent = 0;
            int alt;
            GridCell cell;
			for(int x=0; x < this.width; x++)
			{
				for(int y = 0; y < this.height; y++)
				{
					this.elevations[x,y].X = x * this.spacing;
        			this.elevations[x,y].Z = y * this.spacing;
                    cell = this.grid.GetCell(new Point(x, y));
                    alt = 0; // cell.Altitude;
                    if (alt < 0 && !cell.IsDry)
                        alt = 0;
                    this.elevations[x, y].Y = alt; // this.grid.GetCell(new Point(x, y)).Altitude;
				}

				nonScaledPercent += (double)this.height/totalQuads;
				scaledPercent = (nonScaledPercent * 33);
				OnLoadProgressChanged(new StatusChangedEventArgs("Loading Elevation Data...",(int)scaledPercent));
			}

			for(int x=0; x < this.width; x++)
			{
				for(int y = 0; y < this.height; y++)
				{
					this.terrainQuads[x,y] = new TerrainQuad(
                        "Quad"+ x + "-" + y,
						this.elevations[x,y], 
                        this.elevations[x+1,y],
						this.elevations[x,y+1], 
                        this.elevations[x+1,y+1]);
                    cell = this.grid.GetCell(new Point(x, y));
                    this.terrainQuads[x, y].Texture = (Texture)tileset.TerrainTiles[cell.Terrain.Name].TileImage;
					GameEngine.TopQuadTreeNode.AddObject(this.terrainQuads[x,y]);
				}
				nonScaledPercent += (double)this.height/totalQuads;
				scaledPercent = nonScaledPercent * 33;
				OnLoadProgressChanged(new StatusChangedEventArgs("Loading Terrain Quads...", (int)scaledPercent));
			}

			for(int x = 1; x < this.width-1; x++)
			{
				for(int y = 1; y < this.height-1; y++)
				{
					Vector3 southWestNormal = 
						this.terrainQuads[x,y].FaceNormals + this.terrainQuads[x-1,y-1].FaceNormals +
						this.terrainQuads[x-1,y].FaceNormals + this.terrainQuads[x,y-1].FaceNormals;
					this.terrainQuads[x,y].SetCornerNormal(0, southWestNormal);

					Vector3 southEastNormal = 
						this.terrainQuads[x,y].FaceNormals + this.terrainQuads[x,y-1].FaceNormals +
						this.terrainQuads[x+1,y].FaceNormals + this.terrainQuads[x+1,y-1].FaceNormals;
					this.terrainQuads[x,y].SetCornerNormal(1, southEastNormal);

					Vector3 northWestNormal =
						terrainQuads[x,y].FaceNormals + this.terrainQuads[x-1,y].FaceNormals +
						this.terrainQuads[x-1,y+1].FaceNormals + this.terrainQuads[x,y+1].FaceNormals;
					this.terrainQuads[x,y].SetCornerNormal(2, northWestNormal);

					Vector3 northEastNormal =
						this.terrainQuads[x,y].FaceNormals + this.terrainQuads[x,y+1].FaceNormals +
						this.terrainQuads[x+1,y+1].FaceNormals + this.terrainQuads[x+1,y].FaceNormals;
					this.terrainQuads[x,y].SetCornerNormal(3, northEastNormal);
				}
				nonScaledPercent += (double)this.height/totalQuads;
				scaledPercent = nonScaledPercent * 33;
				OnLoadProgressChanged(new StatusChangedEventArgs("Setting Normal Vectors...", (int)scaledPercent));
			}

            this.vertexBuffer = VertexBuffer.CreateGeneric<PositionNormalTextured>(
                GameEngine.Device,
                numVertices,
                Usage.WriteOnly,
                PositionNormalTextured.Format,
                Pool.Default,
                null);			
		}

        /// <summary>
        /// Occurs when progress has been made loading the <see cref="Terrain"/>.
        /// </summary>
		public event EventHandler<StatusChangedEventArgs> LoadProgress
		{
			add
			{
				_statusLoadEvent += value; 
			}

			remove
			{
				_statusLoadEvent -= value; 
			}
		}

        /// <summary>
        /// Fires the <see cref="LoadProgress"/> event.
        /// </summary>
        /// <param name="e"></param>
		protected virtual void OnLoadProgressChanged(StatusChangedEventArgs e)
		{
			if(_statusLoadEvent != null)
			{
				_statusLoadEvent(this, e);
			}
		}

        /// <summary>
        /// Renders the terrain.
        /// </summary>
        /// <param name="camera"></param>
		public void Render(Camera camera)
		{						
			int offset = 0;
            int oldOffset;
            TerrainQuad quad;			
            GameEngine.Device.RenderState.CullMode = Cull.Clockwise;
			GameEngine.Device.VertexFormat = PositionNormalTextured.Format;
            GameEngine.Device.Material = GraphicsUtility.InitMaterial(Color.White);					
			GameEngine.Device.Transform.World = Matrix.Identity;            
			GameEngine.Device.Transform.View = camera.ViewMatrix;
            List<TerrainQuad> drawnQuads = new List<TerrainQuad>();

            GraphicsBuffer<PositionNormalTextured> buffer =
                this.vertexBuffer.Lock<PositionNormalTextured>(0, 0, LockFlags.None);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    oldOffset = offset;
                    quad = this.terrainQuads[x, y];
                    offset = this.terrainQuads[x, y].AppendVertexData(offset, this.vertices);
                    if (offset != oldOffset)
                        drawnQuads.Add(quad);
                }
            }
            buffer.Write(this.vertices);
            this.vertexBuffer.Unlock();
            offset = 0;
            foreach (TerrainQuad nextQuad in drawnQuads)
            {
                GameEngine.Device.VertexFormat = PositionNormalTextured.Format;
                GameEngine.Device.SetTexture(0, nextQuad.Texture);
                GameEngine.Device.SetStreamSource(0, this.vertexBuffer, offset);
                GameEngine.Device.DrawPrimitives(PrimitiveType.TriangleList, 0, 6);
                offset += 6;
            }
           
		}		
	}
}
