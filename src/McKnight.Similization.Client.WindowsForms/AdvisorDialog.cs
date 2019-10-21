using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using McKnight.Similization.Client;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// The Similization Advisor Dialog.  This dialog hosts the different controls 
	/// containing the advisors in the game, such as the domestic advisor and 
	/// military advisor.
	/// </summary>
	public class AdvisorDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label _advisorsHeaderLabel;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button _doneButton;
		private System.Windows.Forms.LinkLabel _domesticAdvisorLink;
		private System.Windows.Forms.LinkLabel _foreignAdvisorLink;
		private System.Windows.Forms.LinkLabel _scienceAdvisorLink;
		private System.Windows.Forms.LinkLabel _militaryAdvisorLink;
		private System.Windows.Forms.LinkLabel _culturalAdvisorLink;
		private System.Windows.Forms.LinkLabel _tradeAdvisorLink;
		private System.Windows.Forms.ImageList _imageList;
		private System.Windows.Forms.Panel _mainPanel;
		private System.Windows.Forms.Label _advisorNameLabel;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Initializes a new instance of the <c>AdvisorDialog</c> class.
		/// </summary>
		/// <param name="advisor"></param>
		public AdvisorDialog(Advisor advisor)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitializeAdvisorControls();

			InitializeAdvisorLinks();

			LoadAdvisor(advisor);
		}

		private DomesticAdvisorDialog _domesticControl;
		private ForeignAdvisorDialog _foreignControl;
		private MilitaryAdvisorControl _militaryControl;

		private void InitializeAdvisorControls()
		{
			_domesticControl = new DomesticAdvisorDialog();
			_foreignControl = new ForeignAdvisorDialog();
			_militaryControl = new MilitaryAdvisorControl();
		}

		private void InitializeAdvisorLinks()
		{
			_domesticAdvisorLink.Tag = Advisor.DomesticAdvisor;
			_culturalAdvisorLink.Tag = Advisor.CulturalAdvisor;
			_scienceAdvisorLink.Tag = Advisor.ScienceAdvisor;
			_militaryAdvisorLink.Tag = Advisor.MilitaryAdvisor;
			_tradeAdvisorLink.Tag = Advisor.TradeAdvisor;
			_foreignAdvisorLink.Tag = Advisor.ForeignAdvisor;
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

		private Advisor _advisor;

		private void LoadAdvisor(Advisor advisor)
		{
			UserControl newControl = null;
			_advisor = advisor;

			switch(advisor)
			{
				case Advisor.CulturalAdvisor:
					_advisorNameLabel.Text = ClientResources.GetString("advisor_cultural");
					break;
				case Advisor.DomesticAdvisor:
					_advisorNameLabel.Text = ClientResources.GetString("advisor_domestic");
					newControl = _domesticControl;
					break;
				case Advisor.ForeignAdvisor:
					_advisorNameLabel.Text = ClientResources.GetString("advisor_foreign");
					newControl = _foreignControl;
					break;
				case Advisor.MilitaryAdvisor:
					_advisorNameLabel.Text = ClientResources.GetString("advisor_military");
					newControl = _militaryControl;
					break;
				case Advisor.ScienceAdvisor:
					_advisorNameLabel.Text = ClientResources.GetString("advisor_scientific");
					break;
				case Advisor.TradeAdvisor:
					_advisorNameLabel.Text = ClientResources.GetString("advisor_trade");
					break;
			}

			SwapControl(newControl);
		}

		private void SwapControl(UserControl newControl)
		{
			if(_activeControl != null)
				_activeControl.Hide();

			_activeControl = newControl;
			if(!_mainPanel.Contains(newControl))
			{
				_mainPanel.Controls.Add(newControl);
				newControl.Dock = DockStyle.Fill;
			}
			_activeControl.Show();
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvisorDialog));
            this._mainPanel = new System.Windows.Forms.Panel();
            this._advisorsHeaderLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this._tradeAdvisorLink = new System.Windows.Forms.LinkLabel();
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this._culturalAdvisorLink = new System.Windows.Forms.LinkLabel();
            this._militaryAdvisorLink = new System.Windows.Forms.LinkLabel();
            this._scienceAdvisorLink = new System.Windows.Forms.LinkLabel();
            this._foreignAdvisorLink = new System.Windows.Forms.LinkLabel();
            this._domesticAdvisorLink = new System.Windows.Forms.LinkLabel();
            this._advisorNameLabel = new System.Windows.Forms.Label();
            this._doneButton = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainPanel
            // 
            resources.ApplyResources(this._mainPanel, "_mainPanel");
            this._mainPanel.Name = "_mainPanel";
            // 
            // _advisorsHeaderLabel
            // 
            resources.ApplyResources(this._advisorsHeaderLabel, "_advisorsHeaderLabel");
            this._advisorsHeaderLabel.Name = "_advisorsHeaderLabel";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._tradeAdvisorLink);
            this.panel2.Controls.Add(this._culturalAdvisorLink);
            this.panel2.Controls.Add(this._militaryAdvisorLink);
            this.panel2.Controls.Add(this._scienceAdvisorLink);
            this.panel2.Controls.Add(this._foreignAdvisorLink);
            this.panel2.Controls.Add(this._domesticAdvisorLink);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // _tradeAdvisorLink
            // 
            resources.ApplyResources(this._tradeAdvisorLink, "_tradeAdvisorLink");
            this._tradeAdvisorLink.ImageList = this._imageList;
            this._tradeAdvisorLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._tradeAdvisorLink.Name = "_tradeAdvisorLink";
            this._tradeAdvisorLink.TabStop = true;
            this._tradeAdvisorLink.VisitedLinkColor = System.Drawing.Color.White;
            this._tradeAdvisorLink.Click += new System.EventHandler(this.HandleLinkClicked);
            // 
            // _imageList
            // 
            this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
            this._imageList.Images.SetKeyName(0, "");
            this._imageList.Images.SetKeyName(1, "");
            this._imageList.Images.SetKeyName(2, "");
            this._imageList.Images.SetKeyName(3, "");
            this._imageList.Images.SetKeyName(4, "");
            // 
            // _culturalAdvisorLink
            // 
            resources.ApplyResources(this._culturalAdvisorLink, "_culturalAdvisorLink");
            this._culturalAdvisorLink.ImageList = this._imageList;
            this._culturalAdvisorLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._culturalAdvisorLink.Name = "_culturalAdvisorLink";
            this._culturalAdvisorLink.TabStop = true;
            this._culturalAdvisorLink.VisitedLinkColor = System.Drawing.Color.White;
            this._culturalAdvisorLink.Click += new System.EventHandler(this.HandleLinkClicked);
            // 
            // _militaryAdvisorLink
            // 
            resources.ApplyResources(this._militaryAdvisorLink, "_militaryAdvisorLink");
            this._militaryAdvisorLink.ImageList = this._imageList;
            this._militaryAdvisorLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._militaryAdvisorLink.Name = "_militaryAdvisorLink";
            this._militaryAdvisorLink.TabStop = true;
            this._militaryAdvisorLink.VisitedLinkColor = System.Drawing.Color.White;
            this._militaryAdvisorLink.Click += new System.EventHandler(this.HandleLinkClicked);
            // 
            // _scienceAdvisorLink
            // 
            resources.ApplyResources(this._scienceAdvisorLink, "_scienceAdvisorLink");
            this._scienceAdvisorLink.ImageList = this._imageList;
            this._scienceAdvisorLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._scienceAdvisorLink.Name = "_scienceAdvisorLink";
            this._scienceAdvisorLink.TabStop = true;
            this._scienceAdvisorLink.VisitedLinkColor = System.Drawing.Color.White;
            this._scienceAdvisorLink.Click += new System.EventHandler(this.HandleLinkClicked);
            // 
            // _foreignAdvisorLink
            // 
            resources.ApplyResources(this._foreignAdvisorLink, "_foreignAdvisorLink");
            this._foreignAdvisorLink.ImageList = this._imageList;
            this._foreignAdvisorLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._foreignAdvisorLink.Name = "_foreignAdvisorLink";
            this._foreignAdvisorLink.TabStop = true;
            this._foreignAdvisorLink.VisitedLinkColor = System.Drawing.Color.White;
            this._foreignAdvisorLink.Click += new System.EventHandler(this.HandleLinkClicked);
            // 
            // _domesticAdvisorLink
            // 
            resources.ApplyResources(this._domesticAdvisorLink, "_domesticAdvisorLink");
            this._domesticAdvisorLink.ImageList = this._imageList;
            this._domesticAdvisorLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._domesticAdvisorLink.Name = "_domesticAdvisorLink";
            this._domesticAdvisorLink.TabStop = true;
            this._domesticAdvisorLink.VisitedLinkColor = System.Drawing.Color.White;
            this._domesticAdvisorLink.Click += new System.EventHandler(this.HandleLinkClicked);
            // 
            // _advisorNameLabel
            // 
            resources.ApplyResources(this._advisorNameLabel, "_advisorNameLabel");
            this._advisorNameLabel.Name = "_advisorNameLabel";
            // 
            // _doneButton
            // 
            this._doneButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this._doneButton, "_doneButton");
            this._doneButton.Name = "_doneButton";
            // 
            // AdvisorDialog
            // 
            this.AcceptButton = this._doneButton;
            this.CancelButton = this._doneButton;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this._doneButton);
            this.Controls.Add(this._advisorNameLabel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this._advisorsHeaderLabel);
            this.Controls.Add(this._mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvisorDialog";
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void HandleLinkClicked(object sender, System.EventArgs e)
		{
			LinkLabel link = (LinkLabel)sender;
			Advisor advisor = (Advisor)link.Tag;
			if(advisor != _advisor)
				LoadAdvisor(advisor);
		}

		private UserControl _activeControl;


		/// <summary>
		/// Gets the active advisor control on the form.
		/// </summary>
		public ISimilizationControl ActiveAdvisorControl
		{
			get
			{
				return (ISimilizationControl)_activeControl;
			}
		}

		
	}
}
