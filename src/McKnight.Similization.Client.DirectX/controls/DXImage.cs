using System;
using System.Drawing;
using System.Collections;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Controls
{
	/// <summary>
	/// Class representing an image used in DirectX.
	/// </summary>
	public class DXImage : IDisposable
	{
		private Texture texture;
		private ImageInformation imageInfo;		
		private string filename;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="DXImage"/> class.
        /// </summary>
        /// <param name="fileName"></param>
		public DXImage(Device device, string fileName)
		{
			this.filename = fileName;
			Load(device);
		}

        ~DXImage()
        {
            Dispose(false);
        }

        /// <summary>
        /// Cleans up any resources still being used.
        /// </summary>
		public void Dispose()
		{
            Dispose(true);
            GC.SuppressFinalize(this);
		}

        private void Dispose(bool disposing)
        {
            if(!this.disposed && disposing && this.texture != null)
                this.texture.Dispose();
            this.disposed = true;
        }

        /// <summary>
        /// Loads the image.
        /// </summary>
		public void Load(Device device)
		{            
			this.texture = new Texture(device, this.filename);
            this.imageInfo = Texture.GetImageInformationFromFile(this.filename);            
		}

        /// <summary>
        /// The texture behind the image.
        /// </summary>
		public Texture Texture
		{
			get { return this.texture; }
		}

        /// <summary>
        /// Basic information about the <see cref="DXImage"/>.
        /// </summary>
        public ImageInformation ImageInformation
        {
            get { return this.imageInfo; }
        }

        /// <summary>
        /// The size of the image.
        /// </summary>
        public Size Size
        {
            get { return new Size(this.imageInfo.Width, this.imageInfo.Height); }
        }
	}
}
