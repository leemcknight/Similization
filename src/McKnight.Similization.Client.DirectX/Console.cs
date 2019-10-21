using System;
using LJM.Similization.Client.DirectX.Controls;
using LJM.Similization.Client;


namespace LJM.Similization.Client.DirectX
{
	/// <summary>
	/// DirectX Client implementation of the <see cref="IConsole"/> interface.
	/// </summary>
	public class Console : DXWindow, IConsole
	{
        DXListBox listBox;

        /// <summary>
        /// Initializes a new instance of the <see cref="Console"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
        public Console(IDirectXControlHost controlHost) : base(controlHost)
        {
            this.listBox = new DXListBox(controlHost, this);
        }

        /// <summary>
        /// Writes a single line to the console.
        /// </summary>
        /// <param name="line"></param>
		public void WriteLine(string line)
		{
            this.listBox.Items.Add(line);
		}

        /// <summary>
        /// Toggles the console visible.
        /// </summary>
		public void ShowConsole()
		{
			this.Show();
		}

        /// <summary>
        /// Toggles the console invisible.
        /// </summary>
		public void HideConsole()
		{
			this.Hide();
		}		
	}
}