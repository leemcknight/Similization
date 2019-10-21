using System;
using System.Collections;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.Direct3D.CustomVertex;

namespace LJM.Similization.Client.DirectX.Engine
{
    /// <summary>
    /// Represents a single piece of textured terrain on the map.  
    /// </summary>
	public class TerrainQuad : ViewableObject, IDisposable
	{
		private bool valid;
		private Texture texture;
		private Vector3 face1Normal;
		private Vector3 face2Normal;
		private PositionNormalTextured[] corners;

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainQuad"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="southWestVector"></param>
        /// <param name="southEastVector"></param>
        /// <param name="northWestVector"></param>
        /// <param name="northEastVector"></param>
		public TerrainQuad(string name, Vector3 southWestVector, Vector3 southEastVector, Vector3 northWestVector, Vector3 northEastVector)
		{
            this.Name = name;
			this.corners = new PositionNormalTextured[6];

			this.corners[0].X = northWestVector.X;
			this.corners[0].Y = northWestVector.Y;
			this.corners[0].Z = northWestVector.Z;
			this.corners[0].U = 0.0f;
			this.corners[0].V = 0.0f;

			this.corners[1].X = southWestVector.X;
			this.corners[1].Y = southWestVector.Y;
			this.corners[1].Z = southWestVector.Z;
			this.corners[1].U = 0.0f;
			this.corners[1].V = 1.0f;

			this.corners[2].X = northEastVector.X;
			this.corners[2].Y = northEastVector.Y;
			this.corners[2].Z = northEastVector.Z;
			this.corners[2].U = 1.0f;
			this.corners[2].V = 0.0f;

			this.corners[3].X = southEastVector.X;
			this.corners[3].Y = southEastVector.Y;
			this.corners[3].Z = southEastVector.Z;
			this.corners[3].U = 1.0f;
			this.corners[3].V = 1.0f;

			this.position.X = (northEastVector.X + northWestVector.X) / 2.0f;
			this.position.Y = (southWestVector.Y + southEastVector.Y + northWestVector.Y + northEastVector.Y) /4.0f;
			this.position.Z = (southWestVector.Z + northWestVector.Z) / 2.0f;

			double dx, dz;
			dx = northEastVector.X - northWestVector.X;
			dz = northWestVector.Z - southWestVector.Z;
			this.BoundingRadius = (float)Math.Sqrt(dx * dx + dz * dz) / 2.0f;

			this.face1Normal = EngineMath.ComputeFaceNormal(
				new Vector3(this.corners[0].X, this.corners[0].Y, this.corners[0].Z),
				new Vector3(this.corners[1].X, this.corners[1].Y, this.corners[1].Z),
				new Vector3(this.corners[2].X, this.corners[2].Y, this.corners[2].Z));

			this.face2Normal = EngineMath.ComputeFaceNormal(
				new Vector3(this.corners[1].X, this.corners[1].Y, this.corners[1].Z),
				new Vector3(this.corners[3].X, this.corners[3].Y, this.corners[3].Z),
				new Vector3(this.corners[2].X, this.corners[2].Y, this.corners[2].Z));

			this.corners[0].Normal = this.face1Normal;
			this.corners[1].Normal = this.FaceNormals;
			this.corners[2].Normal = this.FaceNormals;
			this.corners[3].Normal = this.face2Normal;
			this.corners[4].Normal = this.FaceNormals;
			this.corners[5].Normal = this.FaceNormals;

			this.corners[4].X = this.corners[2].X;
            this.corners[4].Y = this.corners[2].Y;
            this.corners[4].Z = this.corners[2].Z;
            this.corners[4].U = this.corners[2].U;
            this.corners[4].V = this.corners[2].V;
            this.corners[5].X = this.corners[1].X;
            this.corners[5].Y = this.corners[1].Y;
            this.corners[5].Z = this.corners[1].Z;
            this.corners[5].U = this.corners[1].U;
            this.corners[5].V = this.corners[1].V;

			this.valid = true;
		}

        /// <summary>
        /// Calculates the normal vector for the <see cref="TerrainQuad"/>.
        /// </summary>
		public Vector3 FaceNormals 
		{
			get 
			{ 
				Vector3 sum = Vector3.Add(this.face1Normal, this.face2Normal); 
				sum.Normalize(); 
				return sum; 
			} 
		}


        /// <summary>
        /// Sets the assigns the normal vector as the normal vector of the specified
        /// corner of the quad.
        /// </summary>
        /// <param name="corner"></param>
        /// <param name="normalVector"></param>
		public void SetCornerNormal(int corner, Vector3 normalVector)
		{
			normalVector.Normalize();
			this.corners[corner].Normal = normalVector;
		}

        /// <summary>
        /// Determines whether the <see cref="TerrainQuad"/> is valid.
        /// </summary>
		public bool IsValid
		{
			get { return this.valid; }
		}

        /// <summary>
        /// Determines whether the <see cref="TerrainQuad"/> is inside the bounds 
        /// of the specified rectangle.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public override bool IsInsideRectangle(System.Drawing.RectangleF rectangle)
        {
            bool inside = false;

            for (int i = 0; i < 4; i++)
            {
                if(rectangle.Contains(this.corners[i].X, this.corners[i].Z))
                {
                    inside = true;
                    break;
                }
            }

            return inside;
        }

        /// <summary>
        /// The <i>Texture</i> associated with the terrain quad.
        /// </summary>
        public Texture Texture
		{
			get { return this.texture; }
			set { this.texture = value; }
		}

        /// <summary>
        /// Appends the vertex information for the <see cref="TerrainQuad"/> into the 
        /// <i>CustomVertex.PositionNormalTextured</i> array passed in, starting at 
        /// the specified index.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="vertices"></param>
        /// <returns></returns>
		public int AppendVertexData(int offset, PositionNormalTextured[] vertices)
		{
			int newOffset = offset;

			if(this.valid && !this.IsCulled)
			{
				for(int i = 0; i < 6; i++)				
					vertices[offset+i] = this.corners[i];				

				newOffset += 6;
				this.IsCulled = true;
			}

			return newOffset;
		}
	}
}
