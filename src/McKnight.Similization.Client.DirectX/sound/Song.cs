using System;
using System.Collections;
using Microsoft.DirectX.DirectSound;
using Microsoft.DirectX.Generic;
using Buffer = Microsoft.DirectX.DirectSound.Buffer;

namespace LJM.Similization.Client.DirectX.Sound
{
	/// <summary>
	/// Class representing a song in the game.
	/// </summary>
	public class Song : IDisposable
	{        
		//private Audio audioPlayer;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Song"/> class.
        /// </summary>
        /// <param name="fileName"></param>
		public Song(string fileName)
		{
			//this.audioPlayer = new Audio(fileName, true);
		}

        ~Song()
        {
            Dispose(false);
        }

        /// <summary>
        /// Plays the song.
        /// </summary>
		public void Play()
		{
			//this.audioPlayer.Play();
		}

        /// <summary>
        /// Stops the song.
        /// </summary>
		public void Stop()
		{
			//this.audioPlayer.Stop();
		}
        
        /// <summary>
        /// Releases all managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            //if (!this.disposed && disposing && this.audioPlayer != null)            
            //    this.audioPlayer.Dispose();                            
            this.disposed = true;
        }
    }

	
}
