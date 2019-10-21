using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Microsoft.DirectX.DirectSound;
using Buffer = Microsoft.DirectX.DirectSound.Buffer;
using LJM.Similization.Core;

namespace LJM.Similization.Client.DirectX.Sound
{
    /// <summary>
    /// Class representing a list of <see cref="Song"/> objects played in the game.
    /// </summary>
	public class Jukebox : IDisposable
	{
		private Collection<Song> songs;
		private Song currentSong;
		private bool random;
		private bool repeat;
		private int index;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Jukebox"/> class.
        /// </summary>
        /// <param name="owner"></param>
		public Jukebox()
		{
            this.songs = new Collection<Song>();
		}

        ~Jukebox()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases any managed resources.
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
                foreach (Song song in this.songs)
                    song.Dispose();
                if(this.currentSong != null)
                    this.currentSong.Dispose();                
            }
            this.disposed = true;
        }

        /// <summary>
        /// The list of <see cref="Song"/> objects in the <see cref="Jukebox"/>.
        /// </summary>
		public Collection<Song> Songs
		{
			get { return songs; }
		}

        /// <summary>
        /// Determines whether the songs will be randomly played.
        /// </summary>
		public bool Random
		{
			get { return random; }
			set { random = value; }
		}

        /// <summary>
        /// Determines whether the songs will repeat after the last 
        /// song in the list is played.
        /// </summary>
		public bool Repeat
		{
			get { return repeat; }
			set { repeat = value; }
		}


        /// <summary>
        /// Plays a single song in the <see cref="Jukebox"/>.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="addToJukebox"></param>
		public void PlaySingleSong(string fileName, bool addToJukebox)
		{
			if(this.currentSong != null)			
				currentSong.Stop();
			
			this.currentSong = new Song(fileName);
			if(addToJukebox)			
				this.songs.Add(currentSong);			
			this.currentSong.Play();
		}

        /// <summary>
        /// Plays the next <see cref="Song"/> in the list.
        /// </summary>
		public void PlayNextSong()
		{			
			if(this.currentSong != null)			
				this.currentSong.Stop();			

			if(this.random)
			{
				Random rand;
				int next;
                
				rand = new Random();
				do
				{
					next = rand.Next(this.songs.Count-1);

				} while(next == index);

				index = next;
			}
			else
			{
				if(index == this.songs.Count-1)				
					index = 0;				
				else				
					index++;				
			}
			
			songs[index].Play();			
		}
	}

}
