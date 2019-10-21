using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using LJM.Similization.Client.DirectX.Controls;
using LJM.Similization.Client;

namespace LJM.Similization.Client.DirectX
{
	/// <summary>
	/// This is the dialog window used to show the status of loading the
	/// 3D engine.
	/// </summary>
	public class EngineLoadingDialog : DXWindow
	{
        private bool disposed;
		private DXLabel instructionsLabel;
		private DXProgressBar progressBar;
		private DXLabel progressLabel;

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineLoadingDialog"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
		public EngineLoadingDialog(IDirectXControlHost controlHost) : base(controlHost)
		{            
            this.ShadeHeader = false;

			//instructions
			instructionsLabel = new DXLabel(this.ControlHost, this);
			instructionsLabel.AutoSize = true;
			instructionsLabel.Location = new Point(50, 50);
			instructionsLabel.ForeColor = Color.White;			
			instructionsLabel.Text = "Please wait while the 3D engine is initialized.";
			this.Controls.Add(instructionsLabel);

			//progress bar
			progressBar = new DXProgressBar(this.ControlHost, this);
			progressBar.Size = new Size(450, 15);
			progressBar.Location = new Point(20,100);
            progressBar.ForeColor = Color.Blue;
			this.Controls.Add(progressBar);

			//progress status label
			progressLabel = new DXLabel(this.ControlHost, this);
			progressLabel.Location = new Point(20, 80);
			progressLabel.AutoSize = true;			
			progressLabel.ForeColor = Color.White;
			this.Controls.Add(progressLabel);
			
		}

        /// <summary>
        /// Releases all resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed && disposing)
                {
                    if (this.progressBar != null)
                        this.progressBar.Dispose();
                    if (this.progressLabel != null)
                        this.progressLabel.Dispose();
                    if (this.instructionsLabel != null)
                        this.instructionsLabel.Dispose();
                }
            }
            finally
            {
                this.disposed = true;
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// Updates the dialog with the new percentage complete of the loading 
        /// operation and shows the new status message to the user.
        /// </summary>
        /// <param name="percent"></param>
        /// <param name="status"></param>
		public void UpdatePercent(int percent, string status)
		{
			progressBar.PercentComplete = percent;
			progressLabel.Text = status;

			//force a refresh here, since it's likely nothing is 
			//being drawn if the cpu is busy with other things...
			DirectXClientApplication app =  (DirectXClientApplication)ClientApplication.Instance;
			ControlHost graphics = app.CreateGraphics();
			graphics.Refresh();
            if (percent == 100)
                Close();			
		}
	}
}
