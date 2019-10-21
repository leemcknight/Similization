using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace McKnight.SimilizationRulesEditor
{
	/// <summary>
	/// Editor User Control responsible for editing Resource information.
	/// </summary>
	public class ResourceControl : EditorUserControl
	{
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.ComboBox cboType;
		private System.Windows.Forms.CheckedListBox lstTerrains;
		private System.Windows.Forms.CheckBox chkRailroad;
		private System.Windows.Forms.TextBox txtFood;
		private System.Windows.Forms.TextBox txtShields;
		private System.Windows.Forms.TextBox txtCommerce;
		private System.Windows.Forms.ListView lvwResource;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ColumnHeader hdrName;
		private System.Windows.Forms.ColumnHeader hdrType;
		private System.Windows.Forms.ColumnHeader hdrFood;
		private System.Windows.Forms.ColumnHeader hdrShields;
		private System.Windows.Forms.ColumnHeader hdrCommerce;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblFood;
		private System.Windows.Forms.Label lblShields;
		private System.Windows.Forms.Label lblCommerce;
		private System.Windows.Forms.Label lblType;
		private System.Windows.Forms.Label lblTerrain;
		private System.ComponentModel.IContainer components;
		private CurrencyManager currencyManager;

		public ResourceControl() 
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
	
			this.BindingContext = new BindingContext();

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



		public override void AddNew()
		{
			this.currencyManager.AddNew();
			this.currencyManager.Position = this.currencyManager.Count - 1;
		}



		public override void SaveData()
		{
			this.currencyManager.EndCurrentEdit();
			FillListView();
		}



		public override void ShowData()
		{
			if(this.currencyManager == null)
			{
				DataBind();
			}

			FillListView();
		}



		private void FillListView()
		{
			DataTable table = EditorApp.Instance.RuleSetData.Tables["Resource"];

			ListViewItem item;

			lvwResource.Items.Clear();
			foreach(DataRow row in table.Rows)
			{
				item = lvwResource.Items.Add(row["Name"].ToString());
				item.SubItems.Add(row["ResourceType"].ToString());
				item.SubItems.Add(row["Food"].ToString());
				item.SubItems.Add(row["Shields"].ToString());
				item.SubItems.Add(row["Commerce"].ToString());
				item.Tag = row;
			}
		}



		private void DataBind()
		{
			DataSet ds = EditorApp.Instance.RuleSetData;
			this.txtCommerce.DataBindings.Add("Text", ds, "Resource.Commerce");
			this.txtFood.DataBindings.Add("Text", ds, "Resource.Food");
			this.txtName.DataBindings.Add("Text", ds, "Resource.Name");
			this.txtShields.DataBindings.Add("Text", ds, "Resource.Shields");
			this.chkRailroad.DataBindings.Add("Checked", ds, "Resource.IsRailroadPrerequisite");
			this.lstTerrains.DataSource = ds.Tables["Terrain"];
			this.lstTerrains.DisplayMember = "Name";
			this.lstTerrains.ValueMember = "TerrainID";
            this.cboType.DataBindings.Add("Text", ds, "Resource.ResourceType");
			this.currencyManager = (CurrencyManager)this.BindingContext[ds, "Resource"];
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResourceControl));
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblFood = new System.Windows.Forms.Label();
            this.lblShields = new System.Windows.Forms.Label();
            this.lblCommerce = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.lblTerrain = new System.Windows.Forms.Label();
            this.lstTerrains = new System.Windows.Forms.CheckedListBox();
            this.chkRailroad = new System.Windows.Forms.CheckBox();
            this.txtFood = new System.Windows.Forms.TextBox();
            this.txtShields = new System.Windows.Forms.TextBox();
            this.txtCommerce = new System.Windows.Forms.TextBox();
            this.lvwResource = new System.Windows.Forms.ListView();
            this.hdrName = new System.Windows.Forms.ColumnHeader();
            this.hdrType = new System.Windows.Forms.ColumnHeader();
            this.hdrFood = new System.Windows.Forms.ColumnHeader();
            this.hdrShields = new System.Windows.Forms.ColumnHeader();
            this.hdrCommerce = new System.Windows.Forms.ColumnHeader();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // lblFood
            // 
            this.lblFood.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblFood, "lblFood");
            this.lblFood.Name = "lblFood";
            // 
            // lblShields
            // 
            this.lblShields.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblShields, "lblShields");
            this.lblShields.Name = "lblShields";
            // 
            // lblCommerce
            // 
            this.lblCommerce.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblCommerce, "lblCommerce");
            this.lblCommerce.Name = "lblCommerce";
            // 
            // lblType
            // 
            this.lblType.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblType, "lblType");
            this.lblType.Name = "lblType";
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            resources.GetString("cboType.Items"),
            resources.GetString("cboType.Items1"),
            resources.GetString("cboType.Items2"),
            resources.GetString("cboType.Items3")});
            resources.ApplyResources(this.cboType, "cboType");
            this.cboType.Name = "cboType";
            // 
            // lblTerrain
            // 
            this.lblTerrain.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblTerrain, "lblTerrain");
            this.lblTerrain.Name = "lblTerrain";
            // 
            // lstTerrains
            // 
            resources.ApplyResources(this.lstTerrains, "lstTerrains");
            this.lstTerrains.FormattingEnabled = true;
            this.lstTerrains.Name = "lstTerrains";
            // 
            // chkRailroad
            // 
            resources.ApplyResources(this.chkRailroad, "chkRailroad");
            this.chkRailroad.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkRailroad.Name = "chkRailroad";
            // 
            // txtFood
            // 
            resources.ApplyResources(this.txtFood, "txtFood");
            this.txtFood.Name = "txtFood";
            // 
            // txtShields
            // 
            resources.ApplyResources(this.txtShields, "txtShields");
            this.txtShields.Name = "txtShields";
            // 
            // txtCommerce
            // 
            resources.ApplyResources(this.txtCommerce, "txtCommerce");
            this.txtCommerce.Name = "txtCommerce";
            // 
            // lvwResource
            // 
            this.lvwResource.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrName,
            this.hdrType,
            this.hdrFood,
            this.hdrShields,
            this.hdrCommerce});
            resources.ApplyResources(this.lvwResource, "lvwResource");
            this.lvwResource.FullRowSelect = true;
            this.lvwResource.Name = "lvwResource";
            this.lvwResource.View = System.Windows.Forms.View.Details;
            this.lvwResource.SelectedIndexChanged += new System.EventHandler(this.lvwResource_SelectedIndexChanged);
            // 
            // hdrName
            // 
            resources.ApplyResources(this.hdrName, "hdrName");
            // 
            // hdrType
            // 
            resources.ApplyResources(this.hdrType, "hdrType");
            // 
            // hdrFood
            // 
            resources.ApplyResources(this.hdrFood, "hdrFood");
            // 
            // hdrShields
            // 
            resources.ApplyResources(this.hdrShields, "hdrShields");
            // 
            // hdrCommerce
            // 
            resources.ApplyResources(this.hdrCommerce, "hdrCommerce");
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lstTerrains);
            this.panel1.Controls.Add(this.cboType);
            this.panel1.Controls.Add(this.txtShields);
            this.panel1.Controls.Add(this.chkRailroad);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.lblFood);
            this.panel1.Controls.Add(this.lblCommerce);
            this.panel1.Controls.Add(this.lblShields);
            this.panel1.Controls.Add(this.txtCommerce);
            this.panel1.Controls.Add(this.lblType);
            this.panel1.Controls.Add(this.txtFood);
            this.panel1.Controls.Add(this.lblTerrain);
            this.panel1.Controls.Add(this.lblName);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // ResourceControl
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lvwResource);
            this.Name = "ResourceControl";
            resources.ApplyResources(this, "$this");
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion



		private void lvwResource_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataSet ds = EditorApp.Instance.RuleSetData;
	
			if(this.lvwResource.SelectedItems.Count == 1)
			{
				DataView dv = (DataView)this.currencyManager.List;
				dv.Sort = "ResourceID";
				DataRow row = (DataRow)lvwResource.SelectedItems[0].Tag;
				int index = dv.Find(row["ResourceID"]);
				this.currencyManager.Position = index;
			}
			else
			{
				this.currencyManager.Position = -1;
			}
		}

	}
}
