using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using McKnight.Similization.Client;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// Summary description for WindowsClientConsole.
	/// </summary>
	public class WindowsClientConsole : System.Windows.Forms.UserControl, IConsole
	{
        private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.TextBox txtCurrentStatus;
		private System.Windows.Forms.Button btnToggleDetailedConsole;
        private ListBox lstStatusHistory;
        private MiniMap miniMap;
        private WinClientStatusView statusView;     
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Initializes a new instance of the <c>WindowsClientConsole</c> class.
		/// </summary>
		public WindowsClientConsole()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

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

		/// <summary>
		/// Writes a line to the console.
		/// </summary>
		/// <param name="message"></param>
		public void WriteLine(string message)
		{
            lstStatusHistory.Items.Add(message);
			txtCurrentStatus.Text = message;
			Application.DoEvents();
		}

		/// <summary>
		/// Shows the console.
		/// </summary>
		public void ShowConsole()
		{
			Show();
		}

		/// <summary>
		/// Hides the console.
		/// </summary>
		public void HideConsole()
		{
			Hide();
		}

		/// <summary>
		/// The Miniature map displaying the entire grid.
		/// </summary>
		public MiniMap MiniMap
		{
			get { return this.miniMap; }
		}

		/// <summary>
		/// Windows client implementation of the <c>ISimilizationStatusView</c> control.
		/// </summary>
		public WinClientStatusView StatusView
		{
            get { return this.statusView; }
		}

		private void btnToggleDetailedConsole_Click(object sender, System.EventArgs e)
		{
            this.lstStatusHistory.Visible = !this.lstStatusHistory.Visible;
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowsClientConsole));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.txtCurrentStatus = new System.Windows.Forms.TextBox();
            this.btnToggleDetailedConsole = new System.Windows.Forms.Button();
            this.lstStatusHistory = new System.Windows.Forms.ListBox();
            this.miniMap = new McKnight.Similization.Client.WindowsForms.MiniMap();
            this.statusView = new McKnight.Similization.Client.WindowsForms.WinClientStatusView();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.Images.SetKeyName(0, "green_next.ico");
            // 
            // txtCurrentStatus
            // 
            resources.ApplyResources(this.txtCurrentStatus, "txtCurrentStatus");
            this.txtCurrentStatus.BackColor = System.Drawing.Color.White;
            this.txtCurrentStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCurrentStatus.Name = "txtCurrentStatus";
            this.txtCurrentStatus.ReadOnly = true;
            // 
            // btnToggleDetailedConsole
            // 
            resources.ApplyResources(this.btnToggleDetailedConsole, "btnToggleDetailedConsole");
            this.btnToggleDetailedConsole.FlatAppearance.BorderSize = 0;
            this.btnToggleDetailedConsole.ImageList = this.imageList;
            this.btnToggleDetailedConsole.Name = "btnToggleDetailedConsole";
            this.btnToggleDetailedConsole.Click += new System.EventHandler(this.btnToggleDetailedConsole_Click);
            // 
            // lstStatusHistory
            // 
            resources.ApplyResources(this.lstStatusHistory, "lstStatusHistory");
            this.lstStatusHistory.FormattingEnabled = true;
            this.lstStatusHistory.Name = "lstStatusHistory";
            // 
            // miniMap
            // 
            this.miniMap.CenterCell = null;
            resources.ApplyResources(this.miniMap, "miniMap");
            this.miniMap.Name = "miniMap";
            this.miniMap.VisibleBounds = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // statusView
            // 
            this.statusView.ActiveUnit = null;
            resources.ApplyResources(this.statusView, "statusView");
            this.statusView.Gold = null;
            this.statusView.Government = null;
            this.statusView.MovesLeft = null;
            this.statusView.Name = "statusView";
            this.statusView.Status = null;
            this.statusView.Technology = null;
            this.statusView.Terrain = null;
            this.statusView.UnitImage = null;
            this.statusView.Year = null;
            // 
            // WindowsClientConsole
            // 
            this.Controls.Add(this.statusView);
            this.Controls.Add(this.miniMap);
            this.Controls.Add(this.lstStatusHistory);
            this.Controls.Add(this.btnToggleDetailedConsole);
            this.Controls.Add(this.txtCurrentStatus);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "WindowsClientConsole";
            resources.ApplyResources(this, "$this");
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion



	}
}
