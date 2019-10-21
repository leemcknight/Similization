using System;
using System.Collections;
using Microsoft.DirectX.DirectSound;
using Buffer = Microsoft.DirectX.DirectSound.Buffer;
using System.Windows.Forms;

namespace LJM.Similization.Client.DirectX.Sound
{
    /// <summary>
    /// Class representing a sound effect in the game.
    /// </summary>
	public class SoundEffect : IDisposable
	{
		private Device device;
		private Buffer buffer;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundEffect"/> class.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="owner"></param>
		public SoundEffect(string fileName, Control owner)
		{
			BufferDescription desc;

			desc = new BufferDescription();
			this.device = new Device();
			this.device.SetCooperativeLevel(owner.Handle, CooperativeLevel.Normal);            
			this.buffer = new Buffer(this.device, desc, fileName);
		}

        ~SoundEffect()
        {
            Dispose(false);
        }

        /// <summary>
        /// Plays the sound effect.
        /// </summary>
		public void Play()
		{
			this.buffer.Play(0, PlayFlags.Default);
		}
        
        /// <summary>
        /// Cleans up all managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                if(this.buffer != null)
                    this.buffer.Dispose();
                if(this.device != null)
                    this.device.Dispose();                
            }
            this.disposed = true;
        }
        
    }
}
