using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using McKnight.Similization.Client;
using McKnight.Similization.Server;
using System.Globalization;
using McKnight.Similization.Core;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// Dialog asking the user if they wish to revolt and form a newly discovered government.
	/// </summary>
	/// <remarks>This class is the Windows Forms Client implementation of the <see cref="McKnight.Similization.Client.INewGovernmentControl"/> 
	/// interface.</remarks>
	public class NewGovernmentControl : System.Windows.Forms.Form, INewGovernmentControl
	{
        private System.Windows.Forms.Label lblMessage;
        private Button btnYes;
        private Button btnNo;
        private PictureBox pbAdvisor;
        private Government government;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="NewGovernmentControl"/> class.
		/// </summary>
		public NewGovernmentControl()
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

		/// <summary>
		/// Gets or sets the message to show to the user.
		/// </summary>
		public string Message
		{
			get { return lblMessage.Text; }
			set { lblMessage.Text = value; }
		}
		
		/// <summary>
		/// Gets or sets the new government that is available.
		/// </summary>
		public Government Government
		{
			get { return this.government; }
			set 
			{ 
				this.government = value; 
				RefreshGovernment();
			}
		}

		/// <summary>
		/// Shows the control.
		/// </summary>
		public void ShowSimilizationControl()
		{
			ShowDialog();
		}

		private void RefreshGovernment()
		{
            string format = ClientResources.GetString("rejectGovernmentChange");
			ClientApplication client = ClientApplication.Instance;
			Government gov = client.Player.Government;
            btnNo.Text = string.Format(CultureInfo.CurrentCulture, format, gov.Name);			
		}

        private void btnYes_Click(object sender, EventArgs e)
        {
            Country player = ClientApplication.Instance.Player;
            player.Government = this.government;
        }

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewGovernmentControl));
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.pbAdvisor = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdvisor)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            resources.ApplyResources(this.lblMessage, "lblMessage");
            this.lblMessage.Name = "lblMessage";
            // 
            // btnYes
            // 
            this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            resources.ApplyResources(this.btnYes, "btnYes");
            this.btnYes.Name = "btnYes";
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            resources.ApplyResources(this.btnNo, "btnNo");
            this.btnNo.Name = "btnNo";
            // 
            // pbAdvisor
            // 
            this.pbAdvisor.Image = McKnight.Similization.Client.WindowsForms.Properties.Resources.woman3;
            resources.ApplyResources(this.pbAdvisor, "pbAdvisor");
            this.pbAdvisor.Name = "pbAdvisor";
            this.pbAdvisor.TabStop = false;
            // 
            // NewGovernmentControl
            // 
            this.AcceptButton = this.btnYes;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnNo;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pbAdvisor);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.lblMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewGovernmentControl";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.pbAdvisor)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
        
	}
}
