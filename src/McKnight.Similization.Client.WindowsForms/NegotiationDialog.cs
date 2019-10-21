using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using McKnight.Similization.Core;
using McKnight.Similization.Server;
using McKnight.Similization.Client;
using System.Collections.ObjectModel;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// This is the Windows Forms implementation of the Similization Diplomacy Control.  
	/// This is the control used to conduct all negotations, including those for trade, 
	/// peace treaties, and all other types of diplomacy.
	/// </summary>
	public class NegotiationDialog : System.Windows.Forms.Form, IDiplomacyControl
	{
		private System.ComponentModel.IContainer components;
        private Point nextTaskLocation;
		private System.Windows.Forms.Label lblCountry;
		private System.Windows.Forms.TreeView tvwLocal;
		private System.Windows.Forms.TreeView tvwForeign;
		private System.Windows.Forms.Panel pnlOffer;
		private System.Windows.Forms.Button btnRemoveForeignItem;
		private System.Windows.Forms.Button btnAddForeignItem;
		private System.Windows.Forms.Button btnRemoveLocalItem;
		private System.Windows.Forms.Button btnAddLocalItem;
		private System.Windows.Forms.ListBox lbLocal;
		private System.Windows.Forms.ListBox lbForeign;
		private System.Windows.Forms.Label lblForeignLeaderPhrase;
		private System.Windows.Forms.Label lblAdvisorHeader;
		private System.Windows.Forms.Label lblAdvisorText;
		private System.Windows.Forms.LinkLabel lnkMoreHelp;
		private System.Windows.Forms.ImageList imageList;
        private PictureBox pbForeignLeader;
		private DiplomacyTaskLinkCollection internalLinkCollection; 


		/// <summary>
		/// Initializes a new instance of the <see cref="NegotiationDialog"/> class.
		/// </summary>
		public NegotiationDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			InitializeCollection();
			InitializeTradeCollections();
			this.nextTaskLocation = new Point( 308,570 );
			this.internalLinkCollection = new DiplomacyTaskLinkCollection();
		}

		private void InitializeCollection()
		{
			this.taskLinks = new DiplomacyTaskLinkCollection();
			this.TaskLinks.CollectionChanged += new CollectionChangeEventHandler(HandleTaskLinkCollectionChanged);
		}

		private void InitializeTradeCollections()
		{
			this.givenItems = new Collection<ITradable>();
			this.takenItems = new Collection<ITradable>();
			this.lbForeign.DataSource = this.takenItems;
			this.lbLocal.DataSource = this.givenItems;
		}

		private void Rebind()
		{
			this.lbForeign.DataSource = null;
			this.lbLocal.DataSource = null;
			this.lbForeign.DataSource = this.takenItems;
			this.lbLocal.DataSource = this.givenItems;
			this.lbForeign.DisplayMember = "Name";
			this.lbLocal.DisplayMember = "Name";
		}


		private Point GetNextTaskLocation()
		{
			this.nextTaskLocation = new Point(this.nextTaskLocation.X, this.nextTaskLocation.Y - 20);

			return this.nextTaskLocation;
		}



		private void HandleTaskLinkCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			switch(e.Action)
			{
				case CollectionChangeAction.Add:
					DiplomacyTaskLink taskLink = (DiplomacyTaskLink)e.Element;
					taskLink.Location = GetNextTaskLocation();
					Controls.Add(taskLink);
					taskLink.Click += new EventHandler(HandleLinkClick);
					this.internalLinkCollection.Add(taskLink);
					break;
				case CollectionChangeAction.Refresh:
					foreach(DiplomacyTaskLink link in this.internalLinkCollection)
					{
						Controls.Remove(link);
					}
					this.internalLinkCollection.Clear();
					this.nextTaskLocation = new Point(308, 570);
					break;
				case CollectionChangeAction.Remove:
					break;
			}
		}


		private void HandleLinkClick(object sender, EventArgs e)
		{
			DiplomacyTaskLink taskLink = (DiplomacyTaskLink)sender;

			taskLink.DiplomacyCommand.Invoke();
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

		private DiplomaticTie diplomaticTie;

		/// <summary>
		/// The <c>DiplomaticTie</c> representing the tie between the two governments 
		/// doing the negotiating.
		/// </summary>
		public DiplomaticTie DiplomaticTie
		{
			get { return this.diplomaticTie; }
			set { this.diplomaticTie = value; }
		}

		private DiplomacyTaskLinkCollection taskLinks;

		/// <summary>
		/// A collection of <c>IDiplomacyTaskLink</c> objects to display on the screen.
		/// </summary>
		public DiplomacyTaskLinkCollection TaskLinks
		{
			get { return this.taskLinks; }
		}


		/// <summary>
		/// The text describing the foreign country and/or leader.
		/// </summary>
		public string ForeignLeaderHeaderText
		{
			get { return lblCountry.Text; }
			set { lblCountry.Text = value; }
		}

		/// <summary>
		/// The phrase that the foreign leader is speaking.
		/// </summary>
		public string ForeignLeaderPhrase
		{
			get { return this.lblForeignLeaderPhrase.Text; }
			set { this.lblForeignLeaderPhrase.Text = value; }
		}


		/// <summary>
		/// The phrase that the advisor is currently telling the user.
		/// </summary>
		public string AdvisorPhrase
		{
			get { return this.lblAdvisorText.Text; }
			set { this.lblAdvisorText.Text = value; }
		}

		/// <summary>
		/// Shows the control.
		/// </summary>
		public void ShowSimilizationControl()
		{
			if(this.diplomaticTie == null)
				throw new InvalidOperationException("The negotiation dialog cannot be shown unless there is a valid diplomatic tie.");
		
			FillLocalTreeView();
			FillForeignTreeView();
			ShowDialog();
			
		}

		/// <summary>
		/// Ends the current diplomacy meeting and closes the diplomacy dialog.
		/// </summary>
		public void EndDiplomacy()
		{
			this.Close();
		}

		/// <summary>
		/// Begins the trading process with the foreign country.
		/// </summary>
		public void StartNegotiations()
		{
			this.tvwForeign.Visible = true;
			this.tvwLocal.Visible = true;
			this.pnlOffer.Visible = true;
		}

		/// <summary>
		/// Ends the trading process without coming to an agreement.
		/// </summary>
		public void EndNegotiations()
		{
			this.tvwForeign.Visible = false;
			this.tvwLocal.Visible = false;
			this.pnlOffer.Visible = false;
		}

		/// <summary>
		/// Occurs when the user asks for more help from the domestic advisor.
		/// </summary>
		public event EventHandler AdvisorHelpRequested;

		private IDiplomacyTaskLinkFactory taskLinkFactory;

		/// <summary>
		/// Gets a WindowsForms implementation of the <c>IDiplomacyTaskLinkFactory</c> interface.
		/// </summary>
		/// <returns></returns>
		public IDiplomacyTaskLinkFactory GetTaskLinkFactory()
		{
			if(this.taskLinkFactory == null)
			{
				this.taskLinkFactory  = new DiplomacyTaskLinkFactory();
			}

			return this.taskLinkFactory;
		}

		private Collection<ITradable> givenItems;

		/// <summary>
		/// A Collection of items that the human player is offering in trade.
		/// </summary>
        public Collection<ITradable> GivenItems
		{
			get { return this.givenItems; }
		}

        private Collection<ITradable> takenItems;

		/// <summary>
		/// A Collection of items that the human player will be receiving in trade.
		/// </summary>
        public Collection<ITradable> TakenItems
		{
			get { return this.takenItems; }
		}
		
		private void FillLocalTreeView()
		{
			ClientApplication client = ClientApplication.Instance;
			Country foe = this.diplomaticTie.ForeignCountry;
			TreeNode child;

			//create the parent nodes
			TreeNode agreementRoot = new TreeNode(ClientResources.GetString("tradableItem_diplomaticAgreement"));
			TreeNode allianceRoot = new TreeNode(ClientResources.GetString("tradableItem_militaryAlliance"));
			TreeNode embargoRoot = new TreeNode(ClientResources.GetString("tradableItem_tradeEmbargo"));
			TreeNode mapRoot = new TreeNode(ClientResources.GetString("tradableItem_map"));
			TreeNode communicationRoot = new TreeNode(ClientResources.GetString("tradableItem_communication"));
			TreeNode resourceRoot = new TreeNode(ClientResources.GetString("tradableItem_resource"));
			TreeNode luxuryRoot = new TreeNode(ClientResources.GetString("tradableItem_luxury"));
			TreeNode goldRoot = new TreeNode(string.Format(CultureInfo.CurrentCulture, ClientResources.GetString("tradableItem_gold"), client.Player.Gold));
			TreeNode technologyRoot = new TreeNode(ClientResources.GetString("tradableItem_technology"));
			TreeNode cityRoot = new TreeNode(ClientResources.GetString("tradableItem_city"));
			TreeNode workerRoot = new TreeNode(ClientResources.GetString("tradableItem_worker"));

			//add the nodes to the tree
			this.tvwLocal.Nodes.Add(agreementRoot);
			this.tvwLocal.Nodes.Add(allianceRoot);
			this.tvwLocal.Nodes.Add(mapRoot);
			this.tvwLocal.Nodes.Add(communicationRoot);
			this.tvwLocal.Nodes.Add(resourceRoot);
			this.tvwLocal.Nodes.Add(luxuryRoot);
			this.tvwLocal.Nodes.Add(goldRoot);
			this.tvwLocal.Nodes.Add(technologyRoot);
			this.tvwLocal.Nodes.Add(cityRoot);
			this.tvwLocal.Nodes.Add(workerRoot);

			//diplomatic agreements
			child = agreementRoot.Nodes.Add(ClientResources.GetString("agreement_peaceTreaty"));
			child.Tag = new PeaceTreaty(client.Player, foe);
			child = agreementRoot.Nodes.Add(ClientResources.GetString("agreement_mutualProtectionPact"));
			child.Tag = new MutualProtectionPact(client.Player, foe);
			child = agreementRoot.Nodes.Add(ClientResources.GetString("agreement_rightOfPassage"));
			child.Tag = new RightOfPassage(client.Player, foe);

			//alliances
			child = allianceRoot.Nodes.Add(ClientResources.GetString("alliance"));
			child.Tag = new MilitaryAlliance(client.Player, foe);

			//embargos
			if(client.Player.CanInvokeTradeEmbargoWith(foe))
			{
				foreach(Country embargoVictim in client.ServerInstance.Countries)
				{
					if(embargoVictim != client.Player && embargoVictim != foe)
					{
						child = embargoRoot.Nodes.Add(ClientResources.GetString("embargo"));
						child.Tag = new TradeEmbargo(client.Player, foe, embargoVictim);
					}
				}
			}

			//maps
			mapRoot.ImageIndex = mapRoot.SelectedImageIndex = 6;
			child = mapRoot.Nodes.Add(ClientResources.GetString("map_world"));
			child.Tag = new WorldMap();
			child = mapRoot.Nodes.Add(ClientResources.GetString("map_territory"));
			child.Tag = new TerritoryMap(client.Player);

			//communications
			foreach(DiplomaticTie tie in client.Player.DiplomaticTies)
			{
				if(tie.ForeignCountry != foe)
				{
					if(foe.GetDiplomaticTie(tie.ForeignCountry) == null)
					{
						communicationRoot.Nodes.Add(string.Format(CultureInfo.CurrentCulture, ClientResources.GetString("communication"), tie.ForeignCountry.Civilization.Name));
						communicationRoot.Tag = new DiplomaticTie(foe, tie.ForeignCountry);	
					}
				}
			}
			
			//resources
			NamedObjectCollection<Resource> resources = client.Player.AvailableResources;
			foreach(Resource resource in resources)
			{
				//don't need to check to see if the foe already has this 
				//resource.  maybe they want more of it...
				child = resourceRoot.Nodes.Add(resource.Name);
				child.Tag = resource;
			}

			//luxuries
			//TODO: implement
			
			//gold
			child = goldRoot.Nodes.Add(ClientResources.GetString("gold_perTurn"));
			child.Tag = new GoldPerTurn(0);
			child = goldRoot.Nodes.Add(ClientResources.GetString("gold_lumpSum"));
			child.Tag = new GoldLumpSum(0);
			
			//technologies
			foreach(Technology technology in client.Player.AcquiredTechnologies)
			{
				if(!foe.AcquiredTechnologies.Contains(technology))
				{
					child = technologyRoot.Nodes.Add(technology.Name);
					child.Tag = technology;
				}
			}

			//cities
			foreach(City city in client.Player.Cities)
			{
				child = cityRoot.Nodes.Add(city.Name);
				child.Tag = city;
			}
		}

		private void FillForeignTreeView()
		{
			ClientApplication client = ClientApplication.Instance;
			Country player = this.diplomaticTie.ForeignCountry;
			Country foe = client.Player;
			TreeNode child;

			//create the parent nodes
			TreeNode agreementRoot = new TreeNode(ClientResources.GetString("tradableItem_diplomaticAgreement"));
			TreeNode allianceRoot = new TreeNode(ClientResources.GetString("tradableItem_militaryAlliance"));
			TreeNode embargoRoot = new TreeNode(ClientResources.GetString("tradableItem_tradeEmbargo"));
			TreeNode mapRoot = new TreeNode(ClientResources.GetString("tradableItem_map"));
			TreeNode communicationRoot = new TreeNode(ClientResources.GetString("tradableItem_communication"));
			TreeNode resourceRoot = new TreeNode(ClientResources.GetString("tradableItem_resource"));
			TreeNode luxuryRoot = new TreeNode(ClientResources.GetString("tradableItem_luxury"));
			TreeNode goldRoot = new TreeNode(string.Format(CultureInfo.CurrentCulture, ClientResources.GetString("tradableItem_gold"), player.Gold));
			TreeNode technologyRoot = new TreeNode(ClientResources.GetString("tradableItem_technology"));
			TreeNode cityRoot = new TreeNode(ClientResources.GetString("tradableItem_city"));
			TreeNode workerRoot = new TreeNode(ClientResources.GetString("tradableItem_worker"));

			//add the nodes to the tree
			this.tvwForeign.Nodes.Add(agreementRoot);
			this.tvwForeign.Nodes.Add(allianceRoot);
			this.tvwForeign.Nodes.Add(mapRoot);
			this.tvwForeign.Nodes.Add(communicationRoot);
			this.tvwForeign.Nodes.Add(resourceRoot);
			this.tvwForeign.Nodes.Add(luxuryRoot);
			this.tvwForeign.Nodes.Add(goldRoot);
			this.tvwForeign.Nodes.Add(technologyRoot);
			this.tvwForeign.Nodes.Add(cityRoot);
			this.tvwForeign.Nodes.Add(workerRoot);

			//diplomatic agreements
			child = agreementRoot.Nodes.Add(ClientResources.GetString("agreement_peaceTreaty"));
			child.Tag = new PeaceTreaty(player, foe);
			child = agreementRoot.Nodes.Add(ClientResources.GetString("agreement_mutualProtectionPact"));
			child.Tag = new MutualProtectionPact(player, foe);
			child = agreementRoot.Nodes.Add(ClientResources.GetString("agreement_rightOfPassage"));
			child.Tag = new RightOfPassage(player, foe);

			//alliances
			child = allianceRoot.Nodes.Add(ClientResources.GetString("alliance"));
			child.Tag = new MilitaryAlliance(player, foe);

			//embargos
			if(player.CanInvokeTradeEmbargoWith(foe))
			{
				foreach(Country embargoVictim in client.ServerInstance.Countries)
				{
					if(embargoVictim != player && embargoVictim != foe)
					{
						child = embargoRoot.Nodes.Add(ClientResources.GetString("embargo"));
						child.Tag = new TradeEmbargo(player, foe, embargoVictim);
					}
				}
			}

			//maps
			mapRoot.ImageIndex = mapRoot.SelectedImageIndex = 6;
			child = mapRoot.Nodes.Add(ClientResources.GetString("map_world"));
			child.Tag = new WorldMap();
			child = mapRoot.Nodes.Add(ClientResources.GetString("map_territory"));
			child.Tag = new TerritoryMap(foe);

			//communications
			foreach(DiplomaticTie tie in player.DiplomaticTies)
			{
				if(tie.ForeignCountry != foe)
				{
					if(foe.GetDiplomaticTie(tie.ForeignCountry) == null)
					{
						communicationRoot.Nodes.Add(string.Format(CultureInfo.CurrentCulture, ClientResources.GetString("communication"), tie.ForeignCountry.Civilization.Name));
						communicationRoot.Tag = new DiplomaticTie(foe, tie.ForeignCountry);	
					}
				}
			}
			
			//resources
            NamedObjectCollection<Resource> resources = player.AvailableResources;
			foreach(Resource resource in resources)
			{
				//don't need to check to see if the foe already has this 
				//resource.  maybe they want more of it...
				child = resourceRoot.Nodes.Add(resource.Name);
				child.Tag = resource;

			}

			//luxuries
			//TODO: implement
			
			//gold
			child = goldRoot.Nodes.Add(ClientResources.GetString("gold_perTurn"));
			child.Tag = new GoldPerTurn(0);
			child = goldRoot.Nodes.Add(ClientResources.GetString("gold_lumpSum"));
			child.Tag = new GoldLumpSum(0);
			
			//technologies
			foreach(Technology technology in player.AcquiredTechnologies)
			{
				if(!foe.AcquiredTechnologies.Contains(technology))
				{
					child = technologyRoot.Nodes.Add(technology.Name);
					child.Tag = technology;
				}
			}

			//cities
			foreach(City city in player.Cities)
			{
				child = cityRoot.Nodes.Add(city.Name);
				child.Tag = city;
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NegotiationDialog));
            this.lblCountry = new System.Windows.Forms.Label();
            this.tvwLocal = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.tvwForeign = new System.Windows.Forms.TreeView();
            this.pnlOffer = new System.Windows.Forms.Panel();
            this.btnRemoveForeignItem = new System.Windows.Forms.Button();
            this.btnAddForeignItem = new System.Windows.Forms.Button();
            this.btnRemoveLocalItem = new System.Windows.Forms.Button();
            this.btnAddLocalItem = new System.Windows.Forms.Button();
            this.lbLocal = new System.Windows.Forms.ListBox();
            this.lbForeign = new System.Windows.Forms.ListBox();
            this.lblForeignLeaderPhrase = new System.Windows.Forms.Label();
            this.lblAdvisorHeader = new System.Windows.Forms.Label();
            this.lblAdvisorText = new System.Windows.Forms.Label();
            this.lnkMoreHelp = new System.Windows.Forms.LinkLabel();
            this.pbForeignLeader = new System.Windows.Forms.PictureBox();
            this.pnlOffer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbForeignLeader)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCountry
            // 
            resources.ApplyResources(this.lblCountry, "lblCountry");
            this.lblCountry.Name = "lblCountry";
            // 
            // tvwLocal
            // 
            this.tvwLocal.BackColor = System.Drawing.Color.White;
            this.tvwLocal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvwLocal.FullRowSelect = true;
            this.tvwLocal.HideSelection = false;
            this.tvwLocal.HotTracking = true;
            resources.ApplyResources(this.tvwLocal, "tvwLocal");
            this.tvwLocal.ImageList = this.imageList;
            this.tvwLocal.Name = "tvwLocal";
            this.tvwLocal.ShowLines = false;
            this.tvwLocal.ShowPlusMinus = false;
            this.tvwLocal.ShowRootLines = false;
            this.tvwLocal.DoubleClick += new System.EventHandler(this._localTreeView_DoubleClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "");
            this.imageList.Images.SetKeyName(3, "");
            this.imageList.Images.SetKeyName(4, "");
            this.imageList.Images.SetKeyName(5, "");
            this.imageList.Images.SetKeyName(6, "");
            this.imageList.Images.SetKeyName(7, "");
            // 
            // tvwForeign
            // 
            this.tvwForeign.BackColor = System.Drawing.Color.White;
            this.tvwForeign.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvwForeign.FullRowSelect = true;
            this.tvwForeign.HideSelection = false;
            this.tvwForeign.HotTracking = true;
            resources.ApplyResources(this.tvwForeign, "tvwForeign");
            this.tvwForeign.ImageList = this.imageList;
            this.tvwForeign.Name = "tvwForeign";
            this.tvwForeign.ShowLines = false;
            this.tvwForeign.ShowPlusMinus = false;
            this.tvwForeign.ShowRootLines = false;
            this.tvwForeign.DoubleClick += new System.EventHandler(this._foreignTreeView_DoubleClick);
            // 
            // pnlOffer
            // 
            resources.ApplyResources(this.pnlOffer, "pnlOffer");
            this.pnlOffer.BackColor = System.Drawing.Color.White;
            this.pnlOffer.Controls.Add(this.btnRemoveForeignItem);
            this.pnlOffer.Controls.Add(this.btnAddForeignItem);
            this.pnlOffer.Controls.Add(this.btnRemoveLocalItem);
            this.pnlOffer.Controls.Add(this.btnAddLocalItem);
            this.pnlOffer.Controls.Add(this.lbLocal);
            this.pnlOffer.Controls.Add(this.lbForeign);
            this.pnlOffer.Name = "pnlOffer";
            // 
            // btnRemoveForeignItem
            // 
            resources.ApplyResources(this.btnRemoveForeignItem, "btnRemoveForeignItem");
            this.btnRemoveForeignItem.Name = "btnRemoveForeignItem";
            this.btnRemoveForeignItem.Click += new System.EventHandler(this._removeForeignItemButton_Click);
            // 
            // btnAddForeignItem
            // 
            resources.ApplyResources(this.btnAddForeignItem, "btnAddForeignItem");
            this.btnAddForeignItem.Name = "btnAddForeignItem";
            this.btnAddForeignItem.Click += new System.EventHandler(this._addForeignItemButton_Click);
            // 
            // btnRemoveLocalItem
            // 
            resources.ApplyResources(this.btnRemoveLocalItem, "btnRemoveLocalItem");
            this.btnRemoveLocalItem.Name = "btnRemoveLocalItem";
            this.btnRemoveLocalItem.Click += new System.EventHandler(this._removeLocalItemButton_Click);
            // 
            // btnAddLocalItem
            // 
            resources.ApplyResources(this.btnAddLocalItem, "btnAddLocalItem");
            this.btnAddLocalItem.Name = "btnAddLocalItem";
            this.btnAddLocalItem.Click += new System.EventHandler(this._addLocalItemButton_Click);
            // 
            // lbLocal
            // 
            this.lbLocal.BackColor = System.Drawing.Color.White;
            this.lbLocal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbLocal.FormattingEnabled = true;
            resources.ApplyResources(this.lbLocal, "lbLocal");
            this.lbLocal.Name = "lbLocal";
            // 
            // lbForeign
            // 
            this.lbForeign.BackColor = System.Drawing.Color.White;
            this.lbForeign.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbForeign.FormattingEnabled = true;
            resources.ApplyResources(this.lbForeign, "lbForeign");
            this.lbForeign.Name = "lbForeign";
            // 
            // lblForeignLeaderPhrase
            // 
            resources.ApplyResources(this.lblForeignLeaderPhrase, "lblForeignLeaderPhrase");
            this.lblForeignLeaderPhrase.Name = "lblForeignLeaderPhrase";
            // 
            // lblAdvisorHeader
            // 
            resources.ApplyResources(this.lblAdvisorHeader, "lblAdvisorHeader");
            this.lblAdvisorHeader.Name = "lblAdvisorHeader";
            // 
            // lblAdvisorText
            // 
            resources.ApplyResources(this.lblAdvisorText, "lblAdvisorText");
            this.lblAdvisorText.Name = "lblAdvisorText";
            // 
            // lnkMoreHelp
            // 
            this.lnkMoreHelp.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            resources.ApplyResources(this.lnkMoreHelp, "lnkMoreHelp");
            this.lnkMoreHelp.Name = "lnkMoreHelp";
            this.lnkMoreHelp.TabStop = true;
            this.lnkMoreHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._moreHelpLink_LinkClicked);
            // 
            // pbForeignLeader
            // 
            resources.ApplyResources(this.pbForeignLeader, "pbForeignLeader");
            this.pbForeignLeader.Name = "pbForeignLeader";
            this.pbForeignLeader.TabStop = false;
            // 
            // NegotiationDialog
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lnkMoreHelp);
            this.Controls.Add(this.lblAdvisorText);
            this.Controls.Add(this.lblAdvisorHeader);
            this.Controls.Add(this.lblForeignLeaderPhrase);
            this.Controls.Add(this.tvwForeign);
            this.Controls.Add(this.tvwLocal);
            this.Controls.Add(this.lblCountry);
            this.Controls.Add(this.pbForeignLeader);
            this.Controls.Add(this.pnlOffer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NegotiationDialog";
            this.ShowInTaskbar = false;
            this.pnlOffer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbForeignLeader)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion


		private void _foreignTreeView_DoubleClick(object sender, System.EventArgs e)
		{
			AddForeignItem();
		}

		private void _addForeignItemButton_Click(object sender, System.EventArgs e)
		{
			AddForeignItem();
		}

		private void AddForeignItem()
		{
			if(this.tvwForeign.SelectedNode.Tag == null)
				return;

			ITradable item =  this.tvwForeign.SelectedNode.Tag as ITradable;
			if(item != null && !this.takenItems.Contains(item))
			{
				if(item.GetType() == typeof(GoldLumpSum))
				{
					GoldLumpSum sum = (GoldLumpSum)item;
					GoldAmountDialog dlg = new GoldAmountDialog();
					if(dlg.ShowDialog(this) == DialogResult.OK)
					{
						sum.LumpSum = dlg.GoldAmount;
					}
					else
					{
						return;
					}
					
				}
				else if(item.GetType() == typeof(GoldPerTurn))
				{
					GoldPerTurn turn = (GoldPerTurn)item;
					GoldAmountDialog dlg = new GoldAmountDialog();
					if(dlg.ShowDialog(this) == DialogResult.OK)
					{
						turn.AmountPerTurn = dlg.GoldAmount;
					}
					else
					{
						return;
					}
				}
				
				this.takenItems.Add(item);
				Rebind();
			}
		}

		private void _localTreeView_DoubleClick(object sender, System.EventArgs e)
		{
			AddLocalItem();
		}

		private void _addLocalItemButton_Click(object sender, System.EventArgs e)
		{
			AddLocalItem();
		}

		private void AddLocalItem()
		{
			if(this.tvwLocal.SelectedNode.Tag == null)
				return;

			ITradable item =  this.tvwLocal.SelectedNode.Tag as ITradable;
			if(item != null && !this.givenItems.Contains(item))
			{
				if(item.GetType() == typeof(GoldLumpSum))
				{
					GoldLumpSum sum = (GoldLumpSum)item;
					GoldAmountDialog dlg = new GoldAmountDialog();
					if(dlg.ShowDialog(this) == DialogResult.OK)
					{
						sum.LumpSum = dlg.GoldAmount;
					}
					else
					{
						return;
					}
				}
				else if(item.GetType() == typeof(GoldPerTurn))
				{
					GoldPerTurn turn = (GoldPerTurn)item;
					GoldAmountDialog dlg = new GoldAmountDialog();
					if(dlg.ShowDialog(this) == DialogResult.OK)
					{
						turn.AmountPerTurn = dlg.GoldAmount;
					}
					else
					{
						return;
					}
				}
				this.givenItems.Add(item);
				Rebind();
			}
		}

		private void _removeLocalItemButton_Click(object sender, System.EventArgs e)
		{
			if(this.lbLocal.SelectedItem != null)
			{
				this.givenItems.Remove((ITradable)this.lbLocal.SelectedItem);
				Rebind();
			}
		}

		

		private void _removeForeignItemButton_Click(object sender, System.EventArgs e)
		{
			if(this.lbForeign.SelectedItem != null)
			{
				this.takenItems.Remove((ITradable)this.lbForeign.SelectedItem);
				Rebind();
			}
		}

		private void _moreHelpLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if(AdvisorHelpRequested != null)
				AdvisorHelpRequested(this, EventArgs.Empty);
		}
	}

	/// <summary>
	/// Windows client implementation of the <c>IDiplomacyTaskLink</c>.
	/// </summary>
	public class DiplomacyTaskLink : LinkLabel, IDiplomacyTaskLink
	{
		/// <summary>
		/// Gets or sets the text of the link.
		/// </summary>
		public string LinkText
		{
			get { return this.Text; }
			set { this.Text = value; }
		}

		private Command diplomacyCommand;

		/// <summary>
		/// The command associated with the task link.
		/// </summary>
		public Command DiplomacyCommand
		{
			get { return this.diplomacyCommand; }
			set { this.diplomacyCommand = value; }
		}
	}

	/// <summary>
	/// Windows client implementation of the <c>IDiplomacyTaskLinkFactory</c> interface.
	/// </summary>
	public class DiplomacyTaskLinkFactory : IDiplomacyTaskLinkFactory
	{
		/// <summary>
		/// Creates and returns a Windows Client implementation of a <c>IDiplomacyTaskLink</c>.
		/// </summary>
		/// <param name="text">The text that the user will see in the link.</param>
		/// <param name="taskCommand">The command to invoke when the link is clicked by the user.</param>
		/// <returns>A valid reference to an <c>IDiplomacyTaskLink</c> interface.</returns>
		public IDiplomacyTaskLink CreateTaskLink(string text, Command taskCommand)
		{
			DiplomacyTaskLink taskLink = new DiplomacyTaskLink();

			taskLink.LinkText = text;
			taskLink.DiplomacyCommand = taskCommand;
			taskLink.LinkBehavior = LinkBehavior.HoverUnderline;
			taskLink.AutoSize = true;
			taskLink.LinkColor = Color.CornflowerBlue;
			taskLink.ActiveLinkColor = Color.CornflowerBlue;
			return taskLink;
		}
	}
}
