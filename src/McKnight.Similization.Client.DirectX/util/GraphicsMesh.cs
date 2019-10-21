using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Direct3D = Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Util
{
    /// <summary>
    /// Handles our meshes
    /// </summary>
    public class GraphicsMesh : IDisposable
    {
        private string fileName = null;
        private Mesh systemMemoryMesh = null; // SysMem mesh, lives through resize
        private Mesh localMemoryMesh = null; // Local mesh, rebuilt on resize
        private Direct3D.Material[] materials = null;
        private Texture[] textures = null;
        private bool isUsingMeshMaterials = true;
        private VertexBuffer systemMemoryVertexBuffer = null;
        private VertexBuffer localMemoryVertexBuffer = null;
        private IndexBuffer systemMemoryIndexBuffer = null;
        private IndexBuffer localMemoryIndexBuffer = null;




        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filename">The initial filename</param>
        public GraphicsMesh(string filename)
        {
            fileName = filename;
        }
        public GraphicsMesh() : this("CD3DFile_Mesh") { }




        /// <summary>
        /// The system memory mesh
        /// </summary>
        public Mesh SystemMesh
        {
            get { return systemMemoryMesh; }
        }




        /// <summary>
        /// The local memory mesh
        /// </summary>
        public Mesh LocalMesh
        {
            get { return localMemoryMesh; }
        }




        /// <summary>
        /// Should we use the mesh materials
        /// </summary>
        public bool IsUsingMaterials
        {
            set { isUsingMeshMaterials = value; }
        }



       

        /// <summary>
        /// Set the flexible vertex format
        /// </summary>
        public void SetVertexFormat(Device device, VertexFormats format)
        {
            Mesh pTempSysMemMesh = null;
            Mesh pTempLocalMesh = null;

            if (systemMemoryMesh != null)
            {
                pTempSysMemMesh = systemMemoryMesh.Clone(device, MeshFlags.SystemMemory, format);
            }
            if (localMemoryMesh != null)
            {
                try
                {
                    pTempLocalMesh = localMemoryMesh.Clone(device, 0, format);
                }
                catch (Exception e)
                {
                    pTempSysMemMesh.Dispose();
                    pTempSysMemMesh = null;
                    throw e;
                }
            }

            if (systemMemoryMesh != null)
                systemMemoryMesh.Dispose();
            systemMemoryMesh = null;

            if (localMemoryMesh != null)
                localMemoryMesh.Dispose();
            localMemoryMesh = null;

            // Clean up any vertex/index buffers
            DisposeLocalBuffers(true, true);

            if (pTempSysMemMesh != null) systemMemoryMesh = pTempSysMemMesh;
            if (pTempLocalMesh != null) localMemoryMesh = pTempLocalMesh;

            // Compute normals in case the meshes have them
            if (systemMemoryMesh != null)
                systemMemoryMesh.ComputeNormals();
            if (localMemoryMesh != null)
                localMemoryMesh.ComputeNormals();
        }




        /// <summary>
        /// Restore the device objects after the device was reset
        /// </summary>
        public void RestoreDeviceObjects(object sender, EventArgs e)
        {
            if (null == systemMemoryMesh)
                throw new ArgumentException();

            Device device = (Device)sender;
            // Make a local memory version of the mesh. Note: because we are passing in
            // no flags, the default behavior is to clone into local memory.
            localMemoryMesh = systemMemoryMesh.Clone(device, 0, systemMemoryMesh.VertexFormat);
            // Clean up any vertex/index buffers
            DisposeLocalBuffers(false, true);

        }




        /// <summary>
        /// Invalidate our local mesh
        /// </summary>
        public void InvalidateDeviceObjects(object sender, EventArgs e)
        {
            if (localMemoryMesh != null)
                localMemoryMesh.Dispose();
            localMemoryMesh = null;
            // Clean up any vertex/index buffers
            DisposeLocalBuffers(false, true);
        }




        /// <summary>
        /// Get the vertex buffer assigned to the system mesh
        /// </summary>
        public VertexBuffer SystemVertexBuffer
        {
            get
            {
                if (systemMemoryVertexBuffer != null)
                    return systemMemoryVertexBuffer;

                if (systemMemoryMesh == null)
                    return null;

                systemMemoryVertexBuffer = systemMemoryMesh.VertexBuffer;
                return systemMemoryVertexBuffer;
            }
        }




        /// <summary>
        /// Get the vertex buffer assigned to the Local mesh
        /// </summary>
        public VertexBuffer LocalVertexBuffer
        {
            get
            {
                if (localMemoryVertexBuffer != null)
                    return localMemoryVertexBuffer;

                if (localMemoryMesh == null)
                    return null;

                localMemoryVertexBuffer = localMemoryMesh.VertexBuffer;
                return localMemoryVertexBuffer;
            }
        }




        /// <summary>
        /// Get the Index buffer assigned to the system mesh
        /// </summary>
        public IndexBuffer SystemIndexBuffer
        {
            get
            {
                if (systemMemoryIndexBuffer != null)
                    return systemMemoryIndexBuffer;

                if (systemMemoryMesh == null)
                    return null;

                systemMemoryIndexBuffer = systemMemoryMesh.IndexBuffer;
                return systemMemoryIndexBuffer;
            }
        }




        /// <summary>
        /// Get the Index buffer assigned to the Local mesh
        /// </summary>
        public IndexBuffer LocalIndexBuffer
        {
            get
            {
                if (localMemoryIndexBuffer != null)
                    return localMemoryIndexBuffer;

                if (localMemoryMesh == null)
                    return null;

                localMemoryIndexBuffer = localMemoryMesh.IndexBuffer;
                return localMemoryIndexBuffer;
            }
        }




        /// <summary>
        /// Clean up any resources
        /// </summary>
        public void Dispose()
        {
            if (textures != null)
            {
                for (int i = 0; i < textures.Length; i++)
                {
                    if (textures[i] != null)
                        textures[i].Dispose();
                    textures[i] = null;
                }
                textures = null;
            }

            // Clean up any vertex/index buffers
            DisposeLocalBuffers(true, true);

            // Clean up any memory
            if (systemMemoryMesh != null)
                systemMemoryMesh.Dispose();
            systemMemoryMesh = null;

            // In case the finalizer hasn't been called yet.
            GC.SuppressFinalize(this);
        }




        /// <summary>
        /// Actually draw the mesh
        /// </summary>
        /// <param name="device">The device used to draw</param>
        /// <param name="canDrawOpaque">Can draw the opaque parts of the mesh</param>
        /// <param name="canDrawAlpha">Can draw the alpha parts of the mesh</param>
        public void Render(Device device, bool canDrawOpaque, bool canDrawAlpha)
        {
            if (null == localMemoryMesh)
                throw new ArgumentException();

            RenderStateManager rs = device.RenderState;
            // Frist, draw the subsets without alpha
            if (canDrawOpaque)
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    if (isUsingMeshMaterials)
                    {
                        if (canDrawAlpha)
                        {
                            if (materials[i].DiffuseColor.Alpha < 0xff)
                                continue;
                        }
                        device.Material = materials[i];
                        device.SetTexture(0, textures[i]);
                    }
                    localMemoryMesh.DrawSubset(i);
                }
            }

            // Then, draw the subsets with alpha
            if (canDrawAlpha && isUsingMeshMaterials)
            {
                // Enable alpha blending
                rs.AlphaBlendEnable = true;
                rs.SourceBlend = Blend.SourceAlpha;
                rs.DestinationBlend = Blend.InvSourceAlpha;
                for (int i = 0; i < materials.Length; i++)
                {
                    if (materials[i].DiffuseColor.Alpha == 0xff)
                        continue;

                    // Set the material and texture
                    device.Material = materials[i];
                    device.SetTexture(0, textures[i]);
                    localMemoryMesh.DrawSubset(i);
                }
                // Restore state
                rs.AlphaBlendEnable = false;
            }
        }




        /// <summary>
        /// Draw the mesh with opaque and alpha 
        /// </summary>
        public void Render(Device device)
        {
            Render(device, true, true);
        }




        /// <summary>
        /// Cleans up the local vertex buffers/index buffers
        /// </summary>
        /// <param name="systemBuffers"></param>
        /// <param name="localBuffers"></param>
        private void DisposeLocalBuffers(bool systemBuffers, bool localBuffers)
        {
            if (systemBuffers)
            {
                if (systemMemoryIndexBuffer != null)
                    systemMemoryIndexBuffer.Dispose();
                systemMemoryIndexBuffer = null;

                if (systemMemoryVertexBuffer != null)
                    systemMemoryVertexBuffer.Dispose();
                systemMemoryVertexBuffer = null;
            }
            if (localBuffers)
            {
                if (localMemoryIndexBuffer != null)
                    localMemoryIndexBuffer.Dispose();
                localMemoryIndexBuffer = null;

                if (localMemoryVertexBuffer != null)
                    localMemoryVertexBuffer.Dispose();
                localMemoryVertexBuffer = null;
            }
        }
    }
}
