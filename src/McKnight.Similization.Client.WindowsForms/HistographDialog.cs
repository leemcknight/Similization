using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using McKnight.Similization.Client;
using McKnight.Similization.Server;
using System.Globalization;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// Windows Forms implementation of the <see cref="McKnight.Similization.Client.IHistograph"/> 
    /// interface.
	/// </summary>
	public class HistographDialog : System.Windows.Forms.Form, IHistograph
	{
		private System.Windows.Forms.Button btnDone;
		private McKnight.Similization.Client.WindowsForms.Histograph histograph1;
		private System.Windows.Forms.GroupBox grpScore;
		private System.Windows.Forms.Label lblWorldRanking;
		private System.Windows.Forms.Label lblScoreHeader;
		private System.Windows.Forms.Label lblScore;
		private System.Windows.Forms.Label lblView;
		private System.Windows.Forms.ComboBox cboView;
		private Dictionary<Country, Label> _countryHashTable = new Dictionary<Country, Label>();

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="HistographDialog"/> class.
		/// </summary>
		/// <param name="history"></param>
		public HistographDialog(History history)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			histograph1.LoadHistory(history);
			
			Country player = ClientApplication.Instance.Player;
			lblScore.Text = player.Score.ToString(CultureInfo.CurrentCulture);
			cboView.Text = "Score";
			LoadWorldRanking();
		}

		private void LoadWorldRanking()
		{
			ClientApplication client = ClientApplication.Instance;

			Label playerLabel;
			Label scoreLabel;
			int y = 45;
			
			foreach(Country player in client.ServerInstance.Countries)
			{
				if(!_countryHashTable.ContainsKey(player))
				{
					playerLabel = new Label();
					playerLabel.Text = player.Name;
					playerLabel.Location = new Point(8, y);
					playerLabel.AutoSize = true;

					scoreLabel = new Label();
					scoreLabel.AutoSize = true;
					scoreLabel.Location = new Point(120, y);
					this.grpScore.Controls.AddRange( 
						new Control[] { playerLabel, scoreLabel } );

					y += scoreLabel.Height;

					_countryHashTable.Add(player, scoreLabel);
				}
				else
				{
					scoreLabel = _countryHashTable[player];
				}
				
				
				switch(histograph1.View)
				{
					case HistographView.Culture:
						scoreLabel.Text = player.CulturePoints.ToString(CultureInfo.CurrentCulture);
						break;
					case HistographView.Power:
						scoreLabel.Text = player.PowerFactor.ToString(CultureInfo.CurrentCulture);
						break;
					case HistographView.Score:
						scoreLabel.Text = player.Score.ToString(CultureInfo.CurrentCulture);
						break;
				}				
			}
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistographDialog));
            this.btnDone = new System.Windows.Forms.Button();
            this.grpScore = new System.Windows.Forms.GroupBox();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblScoreHeader = new System.Windows.Forms.Label();
            this.lblWorldRanking = new System.Windows.Forms.Label();
            this.lblView = new System.Windows.Forms.Label();
            this.cboView = new System.Windows.Forms.ComboBox();
            this.histograph1 = new McKnight.Similization.Client.WindowsForms.Histograph();
            this.grpScore.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDone
            // 
            this.btnDone.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnDone, "btnDone");
            this.btnDone.Name = "btnDone";
            // 
            // grpScore
            // 
            this.grpScore.Controls.Add(this.lblScore);
            this.grpScore.Controls.Add(this.lblScoreHeader);
            this.grpScore.Controls.Add(this.lblWorldRanking);
            this.grpScore.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.grpScore, "grpScore");
            this.grpScore.Name = "grpScore";
            this.grpScore.TabStop = false;
            // 
            // lblScore
            // 
            resources.ApplyResources(this.lblScore, "lblScore");
            this.lblScore.Name = "lblScore";
            // 
            // lblScoreHeader
            // 
            resources.ApplyResources(this.lblScoreHeader, "lblScoreHeader");
            this.lblScoreHeader.Name = "lblScoreHeader";
            // 
            // lblWorldRanking
            // 
            resources.ApplyResources(this.lblWorldRanking, "lblWorldRanking");
            this.lblWorldRanking.Name = "lblWorldRanking";
            // 
            // lblView
            // 
            resources.ApplyResources(this.lblView, "lblView");
            this.lblView.Name = "lblView";
            // 
            // cboView
            // 
            this.cboView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboView.FormattingEnabled = true;
            resources.ApplyResources(this.cboView, "cboView");
            this.cboView.Items.AddRange(new object[] {
            resources.GetString("cboView.Items"),
            resources.GetString("cboView.Items1"),
            resources.GetString("cboView.Items2")});
            this.cboView.Name = "cboView";
            this.cboView.SelectedIndexChanged += new System.EventHandler(this._viewCombo_SelectedIndexChanged);
            // 
            // histograph1
            // 
            this.histograph1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.histograph1, "histograph1");
            this.histograph1.Name = "histograph1";
            this.histograph1.View = McKnight.Similization.Client.HistographView.Score;
            // 
            // HistographDialog
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.histograph1);
            this.Controls.Add(this.cboView);
            this.Controls.Add(this.lblView);
            this.Controls.Add(this.grpScore);
            this.Controls.Add(this.btnDone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HistographDialog";
            this.ShowInTaskbar = false;
            this.grpScore.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void _viewCombo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			HistographView view = HistographView.Score;


			switch(cboView.Text)
			{
				case "Score":
					view = HistographView.Score;
					break;
				case "Culture":
					view = HistographView.Culture;
					break;
				case "Power":
					view = HistographView.Power;
					break;
			}
			
			histograph1.View = view;
			LoadWorldRanking();
		}

		/// <summary>
		/// Shows the control.
		/// </summary>
		public void ShowSimilizationControl()
		{
			ShowDialog();
		}
	}
}
