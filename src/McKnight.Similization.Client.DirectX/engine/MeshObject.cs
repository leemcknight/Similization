using System;
using System.Collections;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Engine
{
	/// <summary>
	/// Object representing a 3d world object.
	/// </summary>
	public class MeshObject : ViewableObject, IDisposable
	{
		private Mesh _mesh;        
		private Material[] _meshMaterials;
		private Texture[] _meshTextures;
		private Vector3 _offset;
		private Attitude _attitudeOffset;

		//private ProgressiveMesh[] _progressiveMeshes;
		private int _progressiveMeshIndex;
		private int _levelsOfDetail;
		private float _maxLevelOfDetailRange;
		private GraphicsBuffer _adjacency;
		private Vector3 _positiveExtents;
		private Vector3 _negativeExtents;
		private Vector3[] _corners;


		public MeshObject(string fileName, Vector3 offset, Attitude adjust) : base()
		{
			_levelsOfDetail = 1;
			_maxLevelOfDetailRange = 1.0f;
			_positiveExtents = new Vector3(-1.0f,-1.0f,-1.0f);
			_negativeExtents = new Vector3(1.0f,1.0f,1.0f);
			_corners = new Vector3[8];

			Mesh tempMesh = null;
			WeldEpsilons epsilons = new WeldEpsilons();

			Vector3 center;
			_offset = offset;
			_attitudeOffset = adjust;
			Position = new Vector3(100.0f,100.0f,0.0f);

			MaterialList materials = null;

			_mesh = new Mesh(GameEngine.Device, fileName, MeshFlags.SystemMemory, 
				_adjacency, materials, new EffectInstanceList());

			VertexBuffer vertexBuffer = _mesh.VertexBuffer;

			GraphicsBuffer vertexData = vertexBuffer.Lock(0,0,LockFlags.NoSystemLock);

			BoundingRadius = Geometry.ComputeBoundingSphere(vertexData,
				_mesh.NumberVertices,_mesh.VertexFormat).Radius;

            BoundingBox box = Geometry.ComputeBoundingBox(vertexData, _mesh.NumberVertices, _mesh.VertexFormat);
            

			vertexBuffer.Unlock();
			vertexBuffer.Dispose();

			//calculate the corners of the bounding box based on the extents obtained
			//from the mesh.
			_offset.Y = -_negativeExtents.Y;

			_corners[0].X = _negativeExtents.X;
			_corners[0].Y = _negativeExtents.Y + _offset.Y;
			_corners[0].Z = _negativeExtents.Z;

			_corners[1].X = _positiveExtents.X;
			_corners[1].Y = _negativeExtents.Y + _offset.Y;
			_corners[1].Z = _negativeExtents.Z;

			_corners[2].X = _negativeExtents.X;
			_corners[2].Y = _positiveExtents.Y + _offset.Y;
			_corners[2].Z = _negativeExtents.Z;

			_corners[3].X = _positiveExtents.X;
			_corners[3].Y = _positiveExtents.Y + _offset.Y;
			_corners[3].Z = _negativeExtents.Z;

			_corners[4].X = _negativeExtents.X;
			_corners[4].Y = _negativeExtents.Y;
			_corners[4].Z = _positiveExtents.Z;

			_corners[5].X = _positiveExtents.X;
			_corners[5].Y = _negativeExtents.Y + _offset.Y;
			_corners[5].Z = _positiveExtents.Z;

			_corners[6].X = _positiveExtents.X;
			_corners[6].Y = _positiveExtents.Y + _offset.Y;
			_corners[6].Z = _positiveExtents.Z;

			//TODO: IS THIS CORRECT?
			_corners[7].X = _positiveExtents.X;
			_corners[7].Y = _positiveExtents.Y + _offset.Y;
			_corners[7].Z = _positiveExtents.Z;

			tempMesh = Mesh.Clean(CleanType.BackFacing, _mesh, _adjacency, _adjacency);
			_mesh.Dispose();
			_mesh = tempMesh;

			_mesh.WeldVertices(0, epsilons, _adjacency);
			_mesh.Validate(_adjacency);

			//CreateLevelOfDetail();

			if(_meshTextures == null && materials != null)
			{
				_meshTextures = new Texture[materials.Count];
				_meshMaterials = new Material[materials.Count];

				for(int i = 0; i < materials.Count; i++)
				{
					_meshMaterials[i] = materials[i].Material;
					_meshMaterials[i].AmbientColor = _meshMaterials[i].DiffuseColor;

					if(materials[i].TextureFileName != null)
					{
                        _meshTextures[i] = new Texture(GameEngine.Device, materials[i].TextureFileName);							
					}
				}
			}

		}
        /*
		private void CreateLevelOfDetail()
		{
			ProgressiveMesh tempMesh = null;
			int mininumVertices = 0;
			int maximumVertices = 0;
			int verticesPerMesh = 0;

			tempMesh = 
				new ProgressiveMesh(_mesh, _adjacency, 
				null, 1, MeshFlags.SimplifyVertex);

			mininumVertices = tempMesh.MinVertices;
			maximumVertices = tempMesh.MaxVertices;

			if(_progressiveMeshes != null)
			{
				for(int i = 0; i < _progressiveMeshes.Length; i++)
				{
					_progressiveMeshes[i].Dispose();
				}
			}

			verticesPerMesh = 
				(maximumVertices - mininumVertices) / _levelsOfDetail;

			_progressiveMeshes = new ProgressiveMesh[_levelsOfDetail];

			for(int i = 0; i < _progressiveMeshes.Length; i++)
			{
				_progressiveMeshes[_progressiveMeshes.Length - 1 - i] =
					tempMesh.Clone(MeshFlags.Managed | MeshFlags.VbShare,
					tempMesh.VertexFormat, GameEngine.Device);

				if(_levelsOfDetail > 1)
				{
					_progressiveMeshes[_progressiveMeshes.Length - 1 - i].TrimByVertices(
						mininumVertices + verticesPerMesh * i, 
						mininumVertices + verticesPerMesh * (i+1));
				}

				_progressiveMeshes[_progressiveMeshes.Length - 1 - i].OptimizeBaseLevelOfDetail(
					MeshFlags.OptimizeVertexCache);
			}

			_progressiveMeshIndex = 0;
			_progressiveMeshes[_progressiveMeshIndex].NumberVertices = maximumVertices;
			tempMesh.Dispose();
		}
         */

		public void Dispose()
		{
		}

		public Vector3 Offset
		{
			get { return _offset; }
		}

		/// <summary>
		/// Renders the Meshed object to the camera that is passed in as a parameter.
		/// </summary>
		/// <param name="camera">The camera to render to</param>
		public override void Render(Camera camera)
		{
			Matrix worldMatrix;

			GameEngine.Device.RenderState.CullMode = 
				Cull.CounterClockwise;

			if(Parent != null)
			{
				worldMatrix = Matrix.Multiply(WorldMatrix, Parent.WorldMatrix);
			}
			else
			{
				worldMatrix = WorldMatrix;
			}

			GameEngine.Device.Transform.World = worldMatrix;

			for(int i = 0; i < _meshMaterials.Length; i++)
			{
				GameEngine.Device.Material = _meshMaterials[i];
				GameEngine.Device.SetTexture(0,_meshTextures[i]);

				//_progressiveMeshes[i].DrawSubset(i);
			}

			foreach(ViewableObject viewableObject in Children)
			{
				viewableObject.Render(camera);
			}

			this.IsCulled = true;
		}
	}
}
