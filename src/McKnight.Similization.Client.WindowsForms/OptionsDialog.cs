using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using McKnight.Similization.Client;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// This is the options dialog for the Similization windows client.
	/// </summary>
	public class OptionsDialog : System.Windows.Forms.Form, IOptionsControl
	{
		private System.Windows.Forms.HelpProvider _helpProvider;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage generalTab;
		private System.Windows.Forms.TextBox txtRuleset;
		private System.Windows.Forms.Button btnRuleset;
		private System.Windows.Forms.Label lblTileset;
		private System.Windows.Forms.TextBox txtTileset;
		private System.Windows.Forms.Button btnTileset;
		private System.Windows.Forms.TabPage tbConsole;
		private System.Windows.Forms.Label lblCityFont;
		private System.Windows.Forms.Button btnFont;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox chkNotification;
		private System.Windows.Forms.CheckBox chkWaitAfterTurn;
		private System.Windows.Forms.Label lblFontValue;
        private new event EventHandler<ControlClosedEventArgs> closed;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="OptionsDialog"/> class.
		/// </summary>
		public OptionsDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			ClientApplication client = ClientApplication.Instance;
			Options opt = client.Options;
			chkNotification.Checked = opt.ShowKilledMessage;
			chkWaitAfterTurn.Checked = opt.WaitAfterTurn;
			txtRuleset.Text = opt.StartingRulesetPath;
			txtTileset.Text = opt.TilesetPath;
			lblFontValue.Text = opt.CityNameFont.Name;
			lblFontValue.ForeColor = opt.CityNameFontColor;
			lblFontValue.Font = opt.CityNameFont;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsDialog));
            this.chkNotification = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this._helpProvider = new System.Windows.Forms.HelpProvider();
            this.chkWaitAfterTurn = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.generalTab = new System.Windows.Forms.TabPage();
            this.lblFontValue = new System.Windows.Forms.Label();
            this.btnFont = new System.Windows.Forms.Button();
            this.lblCityFont = new System.Windows.Forms.Label();
            this.btnTileset = new System.Windows.Forms.Button();
            this.txtTileset = new System.Windows.Forms.TextBox();
            this.lblTileset = new System.Windows.Forms.Label();
            this.btnRuleset = new System.Windows.Forms.Button();
            this.txtRuleset = new System.Windows.Forms.TextBox();
            this.tbConsole = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.generalTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkNotification
            // 
            this._helpProvider.SetHelpString(this.chkNotification, resources.GetString("chkNotification.HelpString"));
            resources.ApplyResources(this.chkNotification, "chkNotification");
            this.chkNotification.Name = "chkNotification";
            this._helpProvider.SetShowHelp(this.chkNotification, ((bool)(resources.GetObject("chkNotification.ShowHelp"))));
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this._helpProvider.SetShowHelp(this.btnOK, ((bool)(resources.GetObject("btnOK.ShowHelp"))));
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this._helpProvider.SetShowHelp(this.btnCancel, ((bool)(resources.GetObject("btnCancel.ShowHelp"))));
            // 
            // chkWaitAfterTurn
            // 
            this._helpProvider.SetHelpString(this.chkWaitAfterTurn, resources.GetString("chkWaitAfterTurn.HelpString"));
            resources.ApplyResources(this.chkWaitAfterTurn, "chkWaitAfterTurn");
            this.chkWaitAfterTurn.Name = "chkWaitAfterTurn";
            this._helpProvider.SetShowHelp(this.chkWaitAfterTurn, ((bool)(resources.GetObject("chkWaitAfterTurn.ShowHelp"))));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this._helpProvider.SetShowHelp(this.label1, ((bool)(resources.GetObject("label1.ShowHelp"))));
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.generalTab);
            this.tabControl.Controls.Add(this.tbConsole);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this._helpProvider.SetShowHelp(this.tabControl, ((bool)(resources.GetObject("tabControl.ShowHelp"))));
            // 
            // generalTab
            // 
            this.generalTab.Controls.Add(this.lblFontValue);
            this.generalTab.Controls.Add(this.btnFont);
            this.generalTab.Controls.Add(this.lblCityFont);
            this.generalTab.Controls.Add(this.btnTileset);
            this.generalTab.Controls.Add(this.txtTileset);
            this.generalTab.Controls.Add(this.lblTileset);
            this.generalTab.Controls.Add(this.btnRuleset);
            this.generalTab.Controls.Add(this.txtRuleset);
            this.generalTab.Controls.Add(this.chkWaitAfterTurn);
            this.generalTab.Controls.Add(this.chkNotification);
            this.generalTab.Controls.Add(this.label1);
            resources.ApplyResources(this.generalTab, "generalTab");
            this.generalTab.Name = "generalTab";
            this._helpProvider.SetShowHelp(this.generalTab, ((bool)(resources.GetObject("generalTab.ShowHelp"))));
            // 
            // lblFontValue
            // 
            resources.ApplyResources(this.lblFontValue, "lblFontValue");
            this.lblFontValue.Name = "lblFontValue";
            this._helpProvider.SetShowHelp(this.lblFontValue, ((bool)(resources.GetObject("lblFontValue.ShowHelp"))));
            // 
            // btnFont
            // 
            resources.ApplyResources(this.btnFont, "btnFont");
            this.btnFont.Name = "btnFont";
            this._helpProvider.SetShowHelp(this.btnFont, ((bool)(resources.GetObject("btnFont.ShowHelp"))));
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // lblCityFont
            // 
            resources.ApplyResources(this.lblCityFont, "lblCityFont");
            this.lblCityFont.Name = "lblCityFont";
            this._helpProvider.SetShowHelp(this.lblCityFont, ((bool)(resources.GetObject("lblCityFont.ShowHelp"))));
            // 
            // btnTileset
            // 
            resources.ApplyResources(this.btnTileset, "btnTileset");
            this.btnTileset.Name = "btnTileset";
            this._helpProvider.SetShowHelp(this.btnTileset, ((bool)(resources.GetObject("btnTileset.ShowHelp"))));
            this.btnTileset.Click += new System.EventHandler(this.btnTileset_Click);
            // 
            // txtTileset
            // 
            resources.ApplyResources(this.txtTileset, "txtTileset");
            this.txtTileset.Name = "txtTileset";
            this._helpProvider.SetShowHelp(this.txtTileset, ((bool)(resources.GetObject("txtTileset.ShowHelp"))));
            // 
            // lblTileset
            // 
            resources.ApplyResources(this.lblTileset, "lblTileset");
            this.lblTileset.Name = "lblTileset";
            this._helpProvider.SetShowHelp(this.lblTileset, ((bool)(resources.GetObject("lblTileset.ShowHelp"))));
            // 
            // btnRuleset
            // 
            resources.ApplyResources(this.btnRuleset, "btnRuleset");
            this.btnRuleset.Name = "btnRuleset";
            this._helpProvider.SetShowHelp(this.btnRuleset, ((bool)(resources.GetObject("btnRuleset.ShowHelp"))));
            this.btnRuleset.Click += new System.EventHandler(this.btnRuleset_Click);
            // 
            // txtRuleset
            // 
            resources.ApplyResources(this.txtRuleset, "txtRuleset");
            this.txtRuleset.Name = "txtRuleset";
            this._helpProvider.SetShowHelp(this.txtRuleset, ((bool)(resources.GetObject("txtRuleset.ShowHelp"))));
            // 
            // tbConsole
            // 
            resources.ApplyResources(this.tbConsole, "tbConsole");
            this.tbConsole.Name = "tbConsole";
            this._helpProvider.SetShowHelp(this.tbConsole, ((bool)(resources.GetObject("tbConsole.ShowHelp"))));
            // 
            // OptionsDialog
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDialog";
            this._helpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.tabControl.ResumeLayout(false);
            this.generalTab.ResumeLayout(false);
            this.generalTab.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion


		/// <summary>
		/// Gets a value indicating whether or not to show a message 
		/// to the user when one of their units is destroyed.
		/// </summary>
		public bool ShowKilledMessage
		{
			get { return chkNotification.Checked; }
		}

		/// <summary>
		/// Gets a value indicating whether or not to force the user
		/// to wait after their turn.
		/// </summary>
		public bool WaitAfterTurn
		{
			get { return chkWaitAfterTurn.Checked; }
		}

		/// <summary>
		/// Gets the <c>System.Drawing.Font</c> to use when display city information.
		/// </summary>
		public Font CityNameFont
		{
			get { return this.lblFontValue.Font; }
		}

		/// <summary>
		/// Gets the <c>Color</c> to use on the <c>CityNameFont</c> Font.
		/// </summary>
		public Color CityNameFontColor
		{
			get { return this.lblFontValue.ForeColor; }
		}

		/// <summary>
		/// Gets the path of the ruleset to start the game with by default.
		/// </summary>
		public string StartingRulesetPath
		{
			get { return this.txtRuleset.Text;	}
			set { this.txtRuleset.Text = value; }
		}


		/// <summary>
		/// The path to the tileset used in the game.
		/// </summary>
		public string TilesetPath
		{
			get { return this.txtTileset.Text; }
			set { this.txtTileset.Text = value; }
		}

		/// <summary>
		/// Shows the control.
		/// </summary>
		public void ShowSimilizationControl()
		{
			ShowDialog();
		}

		private void btnRuleset_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Ruleset Files (*.ruleset) | *.ruleset";
			dlg.Title = "Choose a default ruleset";
			dlg.Multiselect = false;
			dlg.RestoreDirectory = true;
			if(dlg.ShowDialog(this) == DialogResult.OK)
			{
				this.StartingRulesetPath = dlg.FileName;
			}
		}

		private void btnTileset_Click(object sender, System.EventArgs e)
		{

			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Tileset Files (*.tileset) | *.tileset";
			dlg.Title = "Choose a tileset";
			dlg.Multiselect = false;
			dlg.RestoreDirectory = true;
			if(dlg.ShowDialog(this) == DialogResult.OK)
			{
				this.TilesetPath = dlg.FileName;
			}
		
		}

		private void btnFont_Click(object sender, System.EventArgs e)
		{
			FontDialog fd = new FontDialog();
			fd.ShowColor = true;
			
			DialogResult dr = fd.ShowDialog(this);
			if(dr == DialogResult.OK)
			{
				this.lblFontValue.Font = fd.Font;
				this.lblFontValue.ForeColor = fd.Color;
				this.lblFontValue.Text = fd.Font.Name;
			}
		}

        /// <summary>
        /// Occurs when the <see cref="OptionsDialog"/> is closed.
        /// </summary>
        public event EventHandler<ControlClosedEventArgs> Closed
        {
            add
            {
                this.closed += value;
            }

            remove
            {
                this.closed -= value;
            }
        }
    }
}
