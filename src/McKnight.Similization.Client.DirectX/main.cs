using System;
using System.Drawing;
using Microsoft.DirectX;
using Container = System.ComponentModel.Container;
using System.Windows.Forms;

/// <summary>
/// The main windows form for the application.
/// </summary>
namespace LJM.Similization.Client.DirectX
{
    /// <summary>
    /// This class is the main Windows Form hosting the DirectX screen.
    /// </summary>
	public class HostForm : System.Windows.Forms.Form
	{
		private Container components = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainClass"/> class.
        /// </summary>
		public HostForm()
		{
			InitializeComponent();
			Show();
		}

		/// <summary>
		/// Clean up any ResourceType being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
					
				}
			}
			base.Dispose( disposing );
			System.Windows.Forms.Application.Exit();
		}

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HostForm));
            this.SuspendLayout();
            // 
            // MainClass
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(856, 613);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainClass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Similization";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

		}
	}
}
