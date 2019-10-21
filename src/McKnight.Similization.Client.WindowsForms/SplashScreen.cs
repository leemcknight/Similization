using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using McKnight.Similization.Client;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// Splash Screen for the Similization Windows Client.
	/// </summary>
	public class SplashScreen : System.Windows.Forms.Form, ISplashScreen
	{
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Label lblCredits;
		private System.Windows.Forms.Label lblClient;
		private System.Windows.Forms.PictureBox pbDotNet;
		private System.Windows.Forms.Panel pnlTitle;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initializes a new instance of the <c>SplashScreen</c> windows form.
		/// </summary>
		public SplashScreen()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region ISplashScreen implementation

		/// <summary>
		/// Shows the control.
		/// </summary>
		public void ShowSimilizationControl()
		{
			this.Show();
			System.Windows.Forms.Application.DoEvents();
			
		}

		/// <summary>
		/// Closes the control.
		/// </summary>
		public void CloseSplashScreen()
		{
			this.Close();
		}

		private string versionNumber;

		/// <summary>
		/// The Version Number of the Application.
		/// </summary>
		public string VersionNumber
		{
			get { return this.versionNumber; }
			set { this.versionNumber = value; }
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCredits = new System.Windows.Forms.Label();
            this.lblClient = new System.Windows.Forms.Label();
            this.pbDotNet = new System.Windows.Forms.PictureBox();
            this.pnlTitle = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbDotNet)).BeginInit();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblTitle.Name = "lblTitle";
            // 
            // lblCredits
            // 
            resources.ApplyResources(this.lblCredits, "lblCredits");
            this.lblCredits.ForeColor = System.Drawing.Color.White;
            this.lblCredits.Name = "lblCredits";
            // 
            // lblClient
            // 
            resources.ApplyResources(this.lblClient, "lblClient");
            this.lblClient.ForeColor = System.Drawing.Color.White;
            this.lblClient.Name = "lblClient";
            // 
            // pbDotNet
            // 
            this.pbDotNet.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.pbDotNet, "pbDotNet");
            this.pbDotNet.Name = "pbDotNet";
            this.pbDotNet.TabStop = false;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.White;
            this.pnlTitle.Controls.Add(this.pbDotNet);
            this.pnlTitle.Controls.Add(this.lblTitle);
            resources.ApplyResources(this.pnlTitle, "pnlTitle");
            this.pnlTitle.Name = "pnlTitle";
            // 
            // SplashScreen
            // 
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.lblClient);
            this.Controls.Add(this.lblCredits);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashScreen";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.pbDotNet)).EndInit();
            this.pnlTitle.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
	}
}
