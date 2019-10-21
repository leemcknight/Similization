using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using McKnight.Similization.Server;
using McKnight.Similization.Client;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// Windows Forms Client implementation of the <see cref="McKnight.Similization.Client.IMilitaryAdvisorControl"/> interface.
	/// </summary>
	public class MilitaryAdvisorControl : System.Windows.Forms.UserControl, IMilitaryAdvisorControl
	{
		private System.Windows.Forms.Label lblTotalUnits;
        private System.Windows.Forms.Label lblAdvisorText;
		private System.Windows.Forms.Label lblAllowedUnits;
		private System.Windows.Forms.Label lblAllowedUnitsValue;
		private System.Windows.Forms.Label lblSupportCost;
		private System.Windows.Forms.Label lblSupportCostValue;
        private System.Windows.Forms.Label lblListViewTitle;
        private System.Windows.Forms.Label lblForeignHeader;
		private System.Windows.Forms.Label lblTotalUnitsValue;
		private System.Windows.Forms.ComboBox cboForeignCountry;
        private System.Windows.Forms.Label lblForeignCountry;
        private TreeView tvwLocal;
        private TreeView tvwForeign;
        private LinkLabel lnkViewLocal;
        private LinkLabel lnkViewForeign;
		private Country foreignCountry;
        private ViewStyle localViewStyle = ViewStyle.ByUnit;
        private MiniMap miniMap;
        private PictureBox pictureBox1;
        private ViewStyle foreignViewStyle = ViewStyle.ByUnit;
        private enum ViewStyle
        {
            ByCity,
            ByUnit,
        }
	
		#region Windows Designer Generated Code
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MilitaryAdvisorControl));
            this.lblTotalUnits = new System.Windows.Forms.Label();
            this.lblTotalUnitsValue = new System.Windows.Forms.Label();
            this.lblAdvisorText = new System.Windows.Forms.Label();
            this.lblAllowedUnits = new System.Windows.Forms.Label();
            this.lblAllowedUnitsValue = new System.Windows.Forms.Label();
            this.lblSupportCost = new System.Windows.Forms.Label();
            this.lblSupportCostValue = new System.Windows.Forms.Label();
            this.lblListViewTitle = new System.Windows.Forms.Label();
            this.lblForeignHeader = new System.Windows.Forms.Label();
            this.cboForeignCountry = new System.Windows.Forms.ComboBox();
            this.lblForeignCountry = new System.Windows.Forms.Label();
            this.tvwLocal = new System.Windows.Forms.TreeView();
            this.tvwForeign = new System.Windows.Forms.TreeView();
            this.lnkViewLocal = new System.Windows.Forms.LinkLabel();
            this.lnkViewForeign = new System.Windows.Forms.LinkLabel();
            this.miniMap = new McKnight.Similization.Client.WindowsForms.MiniMap();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTotalUnits
            // 
            resources.ApplyResources(this.lblTotalUnits, "lblTotalUnits");
            this.lblTotalUnits.Name = "lblTotalUnits";
            // 
            // lblTotalUnitsValue
            // 
            resources.ApplyResources(this.lblTotalUnitsValue, "lblTotalUnitsValue");
            this.lblTotalUnitsValue.Name = "lblTotalUnitsValue";
            // 
            // lblAdvisorText
            // 
            resources.ApplyResources(this.lblAdvisorText, "lblAdvisorText");
            this.lblAdvisorText.Name = "lblAdvisorText";
            // 
            // lblAllowedUnits
            // 
            resources.ApplyResources(this.lblAllowedUnits, "lblAllowedUnits");
            this.lblAllowedUnits.Name = "lblAllowedUnits";
            // 
            // lblAllowedUnitsValue
            // 
            resources.ApplyResources(this.lblAllowedUnitsValue, "lblAllowedUnitsValue");
            this.lblAllowedUnitsValue.Name = "lblAllowedUnitsValue";
            // 
            // lblSupportCost
            // 
            resources.ApplyResources(this.lblSupportCost, "lblSupportCost");
            this.lblSupportCost.Name = "lblSupportCost";
            // 
            // lblSupportCostValue
            // 
            resources.ApplyResources(this.lblSupportCostValue, "lblSupportCostValue");
            this.lblSupportCostValue.Name = "lblSupportCostValue";
            // 
            // lblListViewTitle
            // 
            this.lblListViewTitle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.lblListViewTitle, "lblListViewTitle");
            this.lblListViewTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblListViewTitle.Name = "lblListViewTitle";
            // 
            // lblForeignHeader
            // 
            this.lblForeignHeader.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.lblForeignHeader, "lblForeignHeader");
            this.lblForeignHeader.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblForeignHeader.Name = "lblForeignHeader";
            // 
            // cboForeignCountry
            // 
            this.cboForeignCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboForeignCountry, "cboForeignCountry");
            this.cboForeignCountry.FormattingEnabled = true;
            this.cboForeignCountry.Name = "cboForeignCountry";
            this.cboForeignCountry.SelectedIndexChanged += new System.EventHandler(this.cboForeignCountry_SelectedIndexChanged);
            // 
            // lblForeignCountry
            // 
            resources.ApplyResources(this.lblForeignCountry, "lblForeignCountry");
            this.lblForeignCountry.Name = "lblForeignCountry";
            // 
            // tvwLocal
            // 
            resources.ApplyResources(this.tvwLocal, "tvwLocal");
            this.tvwLocal.Name = "tvwLocal";
            // 
            // tvwForeign
            // 
            resources.ApplyResources(this.tvwForeign, "tvwForeign");
            this.tvwForeign.Name = "tvwForeign";
            // 
            // lnkViewLocal
            // 
            resources.ApplyResources(this.lnkViewLocal, "lnkViewLocal");
            this.lnkViewLocal.Name = "lnkViewLocal";
            this.lnkViewLocal.TabStop = true;
            this.lnkViewLocal.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkViewLocal_LinkClicked);
            // 
            // lnkViewForeign
            // 
            resources.ApplyResources(this.lnkViewForeign, "lnkViewForeign");
            this.lnkViewForeign.Name = "lnkViewForeign";
            this.lnkViewForeign.TabStop = true;
            this.lnkViewForeign.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkViewForeign_LinkClicked);
            // 
            // miniMap
            // 
            this.miniMap.CenterCell = null;
            resources.ApplyResources(this.miniMap, "miniMap");
            this.miniMap.Name = "miniMap";
            this.miniMap.VisibleBounds = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = McKnight.Similization.Client.WindowsForms.Properties.Resources.woman3;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // MilitaryAdvisorControl
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lnkViewForeign);
            this.Controls.Add(this.lnkViewLocal);
            this.Controls.Add(this.tvwForeign);
            this.Controls.Add(this.tvwLocal);
            this.Controls.Add(this.lblForeignCountry);
            this.Controls.Add(this.cboForeignCountry);
            this.Controls.Add(this.lblForeignHeader);
            this.Controls.Add(this.lblListViewTitle);
            this.Controls.Add(this.lblSupportCostValue);
            this.Controls.Add(this.lblSupportCost);
            this.Controls.Add(this.lblAllowedUnitsValue);
            this.Controls.Add(this.lblAllowedUnits);
            this.Controls.Add(this.miniMap);
            this.Controls.Add(this.lblAdvisorText);
            this.Controls.Add(this.lblTotalUnitsValue);
            this.Controls.Add(this.lblTotalUnits);
            resources.ApplyResources(this, "$this");
            this.Name = "MilitaryAdvisorControl";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
	
		/// <summary>
		/// Initializes a new instance of the <see cref="MilitaryAdvisorControl"/> class.
		/// </summary>
		public MilitaryAdvisorControl()
		{
			InitializeComponent();
			ShowMilitaryInfo();
            RefreshLocalMilitaryInfo();
			ShowForeignCountries();
		}

		private void ShowMilitaryInfo()
		{
			Country player = ClientApplication.Instance.Player;
			this.lblTotalUnitsValue.Text = Convert.ToString(player.Units.Count, CultureInfo.CurrentCulture);
			this.lblSupportCostValue.Text = Convert.ToString(player.CalculateTotalUnitExpensePerTurn(), CultureInfo.CurrentCulture);
			this.lblAllowedUnitsValue.Text = Convert.ToString(player.CalculateNumberOfFreeUnitsAllowed(), CultureInfo.CurrentCulture);
		}

		private void ShowForeignCountries()
		{
			Country player = ClientApplication.Instance.Player;
			foreach(DiplomaticTie tie in player.DiplomaticTies)
			{
				this.cboForeignCountry.Items.Add(tie.ForeignCountry);
			}
			this.cboForeignCountry.DisplayMember = "Name";
		}

        private void RefreshLocalMilitaryInfo()
        {
            this.tvwLocal.Nodes.Clear();
            TreeNode root = new TreeNode();
            TreeNode unitNode;           
            this.tvwLocal.Nodes.Add(root);
            Country player = ClientApplication.Instance.Player;
            if (this.localViewStyle == ViewStyle.ByUnit)
            {
                root.Text = ClientResources.GetString("units");
                foreach (Unit unit in player.Units)
                {
                    bool found = false;
                    unitNode = CreateUnitNode(unit);
                    foreach (TreeNode childNode in root.Nodes)
                    {
                        if (((Unit)childNode.Tag).Name == unit.Name)
                        {
                            found = true;
                            childNode.Nodes.Add(unitNode);
                        }
                    }
                    if (!found)
                    {
                        TreeNode typeNode = new TreeNode(unit.Name);
                        root.Nodes.Add(typeNode);
                        root.Tag = unit;
                        typeNode.Nodes.Add(unitNode);
                    }
                }
            }
            else
            {
                root.Text = ClientResources.GetString("cities");
                Grid grid = ClientApplication.Instance.ServerInstance.Grid;
                foreach (City city in player.Cities)
                {
                    TreeNode cityNode = root.Nodes.Add(city.Name);
                    GridCell cell = grid.GetCell(city.Coordinates);
                    cityNode.Tag = city;
                    foreach (Unit unit in cell.Units)                    
                        cityNode.Nodes.Add(CreateUnitNode(unit));
                    
                }
                TreeNode fieldNode = root.Nodes.Add(ClientResources.GetString("militaryAdvisor_unitsInField"));
                foreach (Unit unit in player.Units)
                {
                    GridCell cell = grid.GetCell(unit.Coordinates);
                    if (cell.City == null)
                        fieldNode.Nodes.Add(CreateUnitNode(unit));
                }
            }
        }

        private void RefreshForeignMilitaryInfo()
        {
            this.tvwForeign.Nodes.Clear();
            TreeNode root = new TreeNode();
            TreeNode unitNode;
            this.tvwForeign.Nodes.Add(root);
            if (this.foreignViewStyle == ViewStyle.ByUnit)
            {
                root.Text = ClientResources.GetString("units");
                foreach (Unit unit in this.foreignCountry.Units)
                {
                    bool found = false;
                    unitNode = CreateUnitNode(unit);
                    foreach (TreeNode childNode in root.Nodes)
                    {
                        if (((Unit)childNode.Tag).Name == unit.Name)
                        {
                            found = true;
                            childNode.Nodes.Add(unitNode);
                        }
                    }
                    if (!found)
                    {
                        TreeNode typeNode = new TreeNode(unit.Name);
                        root.Nodes.Add(typeNode);
                        root.Tag = unit;
                    }
                }
            }
            else
            {
                Grid grid = ClientApplication.Instance.ServerInstance.Grid;
                root.Text = ClientResources.GetString("cities");
                foreach (City city in this.foreignCountry.Cities)
                {
                    TreeNode cityNode = root.Nodes.Add(city.Name);
                    cityNode.Tag = city;
                    GridCell cell = grid.GetCell(city.Coordinates);
                    foreach (Unit unit in cell.Units)                    
                        cityNode.Nodes.Add(CreateUnitNode(unit));
                    
                }
                TreeNode fieldNode = root.Nodes.Add(ClientResources.GetString("militaryAdvisor_unitsInField"));
                foreach (Unit unit in this.foreignCountry.Units)
                {
                    GridCell cell = grid.GetCell(unit.Coordinates);
                    if (cell.City == null)
                        fieldNode.Nodes.Add(CreateUnitNode(unit));
                }
            }
        }

        private static TreeNode CreateUnitNode(Unit unit)
        {
            string format = ClientResources.GetString("militaryAdvisor_unitPlusRank");
            string nodeText = string.Format(
                CultureInfo.CurrentCulture, 
                format, 
                unit.Name, 
                ClientResources.GetRankString(unit.Rank));
            TreeNode node = new TreeNode(nodeText);
            return node;
        }

		/// <summary>
		/// Occurs when the value of the <i>ForeignCountry</i> property changes.
		/// </summary>
		public event EventHandler ForeignCountryChanged;
		

		private void OnForeignCountryChanged()
		{
			if(this.ForeignCountryChanged != null)
				this.ForeignCountryChanged(this, EventArgs.Empty);
            RefreshForeignMilitaryInfo();
		}

		/// <summary>
		/// Windows Forms Client Implementation of the <see cref="McKnight.Similization.Client.IMilitaryAdvisorControl.ForeignCountry"/> property.
		/// </summary>
		public Country ForeignCountry
		{
			get { return this.foreignCountry; }
			set 
			{ 
				if(this.foreignCountry != value)
				{
					this.foreignCountry = value; 
					OnForeignCountryChanged();
				}
			}
		}

		/// <summary>
		/// Gets or sets the text of the military advisor.
		/// </summary>
		public string AdvisorText
		{
			get { return this.lblAdvisorText.Text; }
			set { this.lblAdvisorText.Text = value; }
		}

		/// <summary>
		/// The text that will be displayed as the header above 
		/// the list of units in the local military.
		/// </summary>
		public string CountryHeaderText
		{
			get { return this.lblListViewTitle.Text; }
			set { this.lblListViewTitle.Text = value; }
		}

		/// <summary>
		/// The text that will be displayed as the header above
		/// the list of units in the selected foreign military.
		/// </summary>
		public string ForeignCountryHeaderText
		{
			get { return this.lblForeignHeader.Text; }
			set { this.lblForeignHeader.Text = value; }
		}

		/// <summary>
		/// Shows the <see cref="MilitaryAdvisorControl"/>.
		/// </summary>
		public void ShowSimilizationControl()
		{
			this.miniMap.InitializeMap(ClientApplication.Instance.ServerInstance.Grid);
			this.ParentForm.ShowDialog();
		}

		private void cboForeignCountry_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.ForeignCountry = (Country)cboForeignCountry.SelectedItem;
		}

        private void lnkViewLocal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string linkText;
            if (this.localViewStyle == ViewStyle.ByUnit)
            {
                this.localViewStyle = ViewStyle.ByCity;
                linkText = ClientResources.GetString("militaryAdvisor_viewByUnit");
            }
            else
            {
                this.localViewStyle = ViewStyle.ByUnit;
                linkText = ClientResources.GetString("militaryAdvisor_viewByCity");
            }
            this.lnkViewLocal.Text = linkText;
            RefreshLocalMilitaryInfo();
        }

        private void lnkViewForeign_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string linkText;
            if (this.foreignViewStyle == ViewStyle.ByUnit)
            {
                this.foreignViewStyle = ViewStyle.ByCity;
                linkText = ClientResources.GetString("militaryAdvisor_viewByUnit");
            }
            else
            {
                this.foreignViewStyle = ViewStyle.ByUnit;
                linkText = ClientResources.GetString("militaryAdvisor_viewByCity");
            }
            this.lnkViewForeign.Text = linkText;
            RefreshForeignMilitaryInfo();
        }

	}
}