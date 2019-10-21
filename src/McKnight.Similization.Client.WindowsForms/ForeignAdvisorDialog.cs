using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using McKnight.Similization.Core;
using McKnight.Similization.Client;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// Summary description for ForeignAdvisorDialog.
	/// </summary>
	public class ForeignAdvisorDialog : System.Windows.Forms.UserControl, IForeignAdvisorControl
	{
		private System.Windows.Forms.ImageList _imageList;
		private System.ComponentModel.IContainer components;
		private System.Drawing.Point[] _points;
		private int _nextPointIndex;
        private Color _peaceColor;
		private System.Windows.Forms.Label lblForeignAdvisorAdvice;
        private System.Windows.Forms.LinkLabel lnkMore;
        private PictureBox pbAdvisor;
		private Color _warColor;

		/// <summary>
		/// Initializes a new instance of the <c>ForeignAdvisorDialog</c> class.
		/// </summary>
		public ForeignAdvisorDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			BuildPointArray();
			_peaceColor = Color.Blue;
			_warColor = Color.Red;

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

		private void BuildPointArray()
		{
			_nextPointIndex = 0;
			_points = new Point[] {
									  new Point(100,100),
									  new Point(100,300),
									  new Point(300,300)
								  };
		}

		/// <summary>
		/// Shows the control.
		/// </summary>
		public void ShowSimilizationControl()
		{
			ShowPlayers();	
			ParentForm.ShowDialog();
		}

		/// <summary>
		/// The text that the Foreign Advisor is telling the player.
		/// </summary>
		public string AdvisorText
		{
			get { return lblForeignAdvisorAdvice.Text; }
			set { lblForeignAdvisorAdvice.Text = value; }
		}

		private void ShowPlayers()
		{
			ClientApplication client = ClientApplication.Instance;
			Country player = client.Player;

            NamedObjectCollection<Country> countries = client.ServerInstance.Countries;
			AddPlayer(client.Player);

			foreach(Country country in countries)
			{
				if(country != player)
				{
					AddPlayer(country);
				}
			}

		}

		private void AddPlayer(Country player)
		{
			Label playerLabel = GetPlayerLabel(player);
			this.Controls.Add(playerLabel);
		}

		private Label GetPlayerLabel(Country player)
		{
			DiplomaticTie tie;
			Country localPlayer = ClientApplication.Instance.Player;
			Label playerLabel = new Label();
			string labelText = string.Empty; 
			int width = 0;

			tie = localPlayer.GetDiplomaticTie(player);

			if(tie != null || player == localPlayer)
			{
				labelText = GetPlayerString(player);
				
			}
			else
			{
                labelText = ClientResources.GetString("unknownOpponent");
			}
			
			Graphics g = CreateGraphics();
			width = g.MeasureString(labelText, this.Font).ToSize().Width;
			width += 100;

			playerLabel.Text = labelText;
			playerLabel.ImageList = _imageList;
			playerLabel.ImageIndex = 0;
			playerLabel.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			playerLabel.ImageAlign = ContentAlignment.MiddleLeft;
			playerLabel.TextAlign = ContentAlignment.MiddleCenter;
			playerLabel.Size = new Size(width, 50);
			playerLabel.Location = GetNextPoint();

			return playerLabel;
		}

		private static string GetPlayerString(Country player)
		{
            string format = ClientResources.GetString("civilizationDetail");
			string playerString = string.Format(
                CultureInfo.CurrentCulture,
				format, 
				player.Government.LeaderTitle,
				player.LeaderName, player.Name
				);

			return playerString;			
		}

		private Point GetNextPoint()
		{
			return _points[_nextPointIndex++];
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForeignAdvisorDialog));
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this.lblForeignAdvisorAdvice = new System.Windows.Forms.Label();
            this.lnkMore = new System.Windows.Forms.LinkLabel();
            this.pbAdvisor = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdvisor)).BeginInit();
            this.SuspendLayout();
            // 
            // _imageList
            // 
            this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
            this._imageList.Images.SetKeyName(0, "");
            // 
            // lblForeignAdvisorAdvice
            // 
            resources.ApplyResources(this.lblForeignAdvisorAdvice, "lblForeignAdvisorAdvice");
            this.lblForeignAdvisorAdvice.Name = "lblForeignAdvisorAdvice";
            // 
            // lnkMore
            // 
            resources.ApplyResources(this.lnkMore, "lnkMore");
            this.lnkMore.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkMore.Name = "lnkMore";
            this.lnkMore.TabStop = true;
            // 
            // pbAdvisor
            // 
            this.pbAdvisor.Image = McKnight.Similization.Client.WindowsForms.Properties.Resources.woman2;
            resources.ApplyResources(this.pbAdvisor, "pbAdvisor");
            this.pbAdvisor.Name = "pbAdvisor";
            this.pbAdvisor.TabStop = false;
            // 
            // ForeignAdvisorDialog
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lnkMore);
            this.Controls.Add(this.lblForeignAdvisorAdvice);
            this.Controls.Add(this.pbAdvisor);
            resources.ApplyResources(this, "$this");
            this.Name = "ForeignAdvisorDialog";
            ((System.ComponentModel.ISupportInitialize)(this.pbAdvisor)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
	}
}
