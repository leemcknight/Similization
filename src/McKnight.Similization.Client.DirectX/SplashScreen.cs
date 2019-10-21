using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using LJM.Similization.Client.DirectX.Controls;
using LJM.Similization.Client.DirectX.Sound;
using LJM.Similization.Client;

namespace LJM.Similization.Client.DirectX
{
    /// <summary>
    /// DirectX Client implementation of the <see cref="ISplashScreen"/> interface.
    /// </summary>
	public class SplashScreen : DXWindow, ISplashScreen
	{		
		private string version;                

        /// <summary>
        /// Initializes a new instance of the <see cref="SplashScreen"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
		public SplashScreen(IDirectXControlHost controlHost) : base(controlHost)
		{
			InitializeComponent();            
		}        
	
		private void InitializeComponent()
		{
            this.ShadeHeader = false;			
            this.BackColor = Color.White;            
            
		}

        /// <summary>
        /// Closes the splash screen.
        /// </summary>
        public void CloseSplashScreen()
        {
            this.Close();
        }

        /// <summary>
        /// Gets or sets the version number to display to the user.
        /// </summary>
        public string VersionNumber
        {
            get
            {
                return this.version;
            }
            set
            {
                this.version = value;
            }
        }

        /// <summary>
        /// Shows the splash screen.
        /// </summary>
        public void ShowSimilizationControl()
        {                        
            this.BackgroundImage = new DXImage(this.ControlHost.Device, @"images\title.jpg");
            this.Size = this.ControlHost.ScreenBounds.Size;
            this.Show();
        }        
    }
}
