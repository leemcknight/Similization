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
	/// Editor User Control responsible for editing Era information.
	/// </summary>
	public class EraControl : EditorUserControl
	{
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.ListBox lstResearchers;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.TextBox txtResearcherName;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.ComboBox cboNextEra;
		private System.Windows.Forms.CheckBox chkStartingEra;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ColumnHeader hdrEra;
		private System.Windows.Forms.ColumnHeader hdrNextEra;
		private System.Windows.Forms.ColumnHeader hdrStartingEra;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblResearchers;
		private System.Windows.Forms.Label lblNextEra;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ListView lvwEras;
		private CurrencyManager currencyManager;
		private CurrencyManager researcherCurrencyManager;
		private readonly string researcherPath = "EraResearcher";

		public EraControl() 
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.BindingContext = new BindingContext();

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
				BindData();
			}

			FillListView();
		}

		private void BindData()
		{
			DataSet ds = EditorApp.Instance.RuleSetData;
			this.txtName.DataBindings.Add("Text", ds, "Era.Name");
			this.cboNextEra.DataSource = ds.Tables["Era"];
			this.cboNextEra.DisplayMember = "Name";
			this.cboNextEra.ValueMember = "EraID";
			this.cboNextEra.DataBindings.Add("SelectedValue", ds, "Era.NextEraID");
			this.chkStartingEra.DataBindings.Add("Checked", ds, "Era.IsFirstEra");
			this.currencyManager = (CurrencyManager)this.BindingContext[ds, "Era"];
			this.researcherCurrencyManager = (CurrencyManager)this.BindingContext[ds, "Era.EraResearcher"];
		}

		private void FillListView()
		{
			DataTable table = EditorApp.Instance.RuleSetData.Tables["Era"];

			ListViewItem item;

			lvwEras.Items.Clear();

			foreach(DataRow row in table.Rows)
			{
				item = lvwEras.Items.Add(row["Name"].ToString());
				item.SubItems.Add(row["IsFirstEra"].ToString());
				item.Tag = row;
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EraControl));
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblResearchers = new System.Windows.Forms.Label();
            this.lstResearchers = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtResearcherName = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblNextEra = new System.Windows.Forms.Label();
            this.cboNextEra = new System.Windows.Forms.ComboBox();
            this.chkStartingEra = new System.Windows.Forms.CheckBox();
            this.lvwEras = new System.Windows.Forms.ListView();
            this.hdrEra = new System.Windows.Forms.ColumnHeader();
            this.hdrNextEra = new System.Windows.Forms.ColumnHeader();
            this.hdrStartingEra = new System.Windows.Forms.ColumnHeader();
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
            // lblResearchers
            // 
            this.lblResearchers.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblResearchers, "lblResearchers");
            this.lblResearchers.Name = "lblResearchers";
            // 
            // lstResearchers
            // 
            this.lstResearchers.FormattingEnabled = true;
            resources.ApplyResources(this.lstResearchers, "lstResearchers");
            this.lstResearchers.Name = "lstResearchers";
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtResearcherName
            // 
            resources.ApplyResources(this.txtResearcherName, "txtResearcherName");
            this.txtResearcherName.Name = "txtResearcherName";
            // 
            // btnRemove
            // 
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lblNextEra
            // 
            this.lblNextEra.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblNextEra, "lblNextEra");
            this.lblNextEra.Name = "lblNextEra";
            // 
            // cboNextEra
            // 
            this.cboNextEra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNextEra.FormattingEnabled = true;
            resources.ApplyResources(this.cboNextEra, "cboNextEra");
            this.cboNextEra.Name = "cboNextEra";
            // 
            // chkStartingEra
            // 
            resources.ApplyResources(this.chkStartingEra, "chkStartingEra");
            this.chkStartingEra.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkStartingEra.Name = "chkStartingEra";
            // 
            // lvwEras
            // 
            this.lvwEras.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrEra,
            this.hdrNextEra,
            this.hdrStartingEra});
            resources.ApplyResources(this.lvwEras, "lvwEras");
            this.lvwEras.FullRowSelect = true;
            this.lvwEras.MultiSelect = false;
            this.lvwEras.Name = "lvwEras";
            this.lvwEras.View = System.Windows.Forms.View.Details;
            this.lvwEras.SelectedIndexChanged += new System.EventHandler(this.lvwEras_SelectedIndexChanged);
            // 
            // hdrEra
            // 
            resources.ApplyResources(this.hdrEra, "hdrEra");
            // 
            // hdrNextEra
            // 
            resources.ApplyResources(this.hdrNextEra, "hdrNextEra");
            // 
            // hdrStartingEra
            // 
            resources.ApplyResources(this.hdrStartingEra, "hdrStartingEra");
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblNextEra);
            this.panel1.Controls.Add(this.chkStartingEra);
            this.panel1.Controls.Add(this.txtResearcherName);
            this.panel1.Controls.Add(this.btnRemove);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.cboNextEra);
            this.panel1.Controls.Add(this.lblResearchers);
            this.panel1.Controls.Add(this.lstResearchers);
            this.panel1.Controls.Add(this.btnAdd);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // EraControl
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lvwEras);
            resources.ApplyResources(this, "$this");
            this.Name = "EraControl";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion



		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			DataSet ds = EditorApp.Instance.RuleSetData;
			DataRow eraRow = ((DataRowView)this.currencyManager.Current).Row;
			DataTable researcherTable = ds.Tables["Researcher"];
			DataRow row = researcherTable.NewRow();
			row["EraID"] = eraRow["EraID"];
			row["ResearcherName"] = this.txtResearcherName.Text;
			researcherTable.Rows.Add(row);
			row.SetParentRow(eraRow);
			txtResearcherName.Clear();
			UpdateResearchers();
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			DataRow row = (DataRow)this.lstResearchers.SelectedItem;
			DataTable table = row.Table;
			table.Rows.Remove(row);
			UpdateResearchers();
		}

		

		private void lvwEras_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataSet ds = EditorApp.Instance.RuleSetData;
	
			if(this.lvwEras.SelectedItems.Count == 1)
			{
				DataView dv = (DataView)this.currencyManager.List;
				dv.Sort = "EraID";
				DataRow eraRow = (DataRow)lvwEras.SelectedItems[0].Tag;
				int index = dv.Find(eraRow["EraID"]);
				this.currencyManager.Position = index;
				UpdateResearchers();
			}
			else
			{
				this.currencyManager.Position = -1;
			}
		}


		private void UpdateResearchers()
		{
			this.lstResearchers.DataSource = null;
			this.lstResearchers.DataSource = this.researcherCurrencyManager.List;
			this.lstResearchers.DisplayMember = "ResearcherName";
		}
	}
}
