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
	/// Editor User Control responsible for editing Technology information.
	/// </summary>
	public class TechnologyControl : EditorUserControl
	{
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.CheckedListBox lstRequirements;
		private System.Windows.Forms.TextBox txtResearchUnits;
		private System.Windows.Forms.CheckBox chkNextEra;
		private System.Windows.Forms.ComboBox cboEra;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblRequirements;
		private System.Windows.Forms.Label lblResearchUnits;
		private System.Windows.Forms.Label lblEra;
		private System.Windows.Forms.ListView lvwTechnology;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ColumnHeader hdrName;
		private System.Windows.Forms.ColumnHeader hdrResearch;
		private System.Windows.Forms.ColumnHeader hdrEra;
		private System.ComponentModel.IContainer components;
        private CheckBox chkEspionage;
		private CurrencyManager currencyManager;

		public TechnologyControl() : base()
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


		public override void UndoChanges()
		{
			this.currencyManager.CancelCurrentEdit();
		}



		public override void ShowData()
		{
			if(this.currencyManager == null)
			{
				DataBind();
			}

			FillListView();
		}

		private void DataBind()
		{
			DataSet ds = EditorApp.Instance.RuleSetData;
			this.txtName.DataBindings.Add("Text", ds, "Technology.Name");
			this.txtResearchUnits.DataBindings.Add("Text", ds, "Technology.ResearchUnits");
            Binding eraBinding = new Binding("Checked", ds, "Technology.RequiredForNextEra");
            eraBinding.Format += new ConvertEventHandler(checkboxBinding_Format);
			this.chkNextEra.DataBindings.Add(eraBinding);
            Binding espionageBinding = new Binding("Checked", ds, "Technology.RequiredForEspionage");
            espionageBinding.Format += new ConvertEventHandler(checkboxBinding_Format);
            this.chkEspionage.DataBindings.Add(espionageBinding);
			this.cboEra.DataSource = ds.Tables["Era"];
			this.cboEra.DisplayMember = "Name";
			this.cboEra.ValueMember = "EraID";
			this.cboEra.DataBindings.Add("SelectedValue", ds, "Technology.EraID");
			this.currencyManager = (CurrencyManager)this.BindingContext[ds, "Technology"];
		}

        void checkboxBinding_Format(object sender, ConvertEventArgs e)
        {
            if (e.Value == DBNull.Value)
                e.Value = false;
        }

		private void FillListView()
		{
			DataTable table = EditorApp.Instance.RuleSetData.Tables["Technology"];

			ListViewItem item;

			lvwTechnology.Items.Clear();
			foreach(DataRow row in table.Rows)
			{
				item = lvwTechnology.Items.Add(row["Name"].ToString());
				item.SubItems.Add(row["ResearchUnits"].ToString());
				item.SubItems.Add(GetEra(row));	
				item.Tag = row;
			}
		}


		private string GetEra(DataRow technologyRow)
		{
			try
			{
				DataTable table = EditorApp.Instance.RuleSetData.Tables["Era"];
				string eraID = technologyRow["EraID"].ToString();

				DataRow[] rows = table.Select("EraID=" + eraID);
			
				if(rows.Length != 1)
					throw new ApplicationException("Unexpected Era count.");

				return rows[0]["Name"].ToString();
			}
			catch
			{
				return "None";
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TechnologyControl));
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lstRequirements = new System.Windows.Forms.CheckedListBox();
            this.lblRequirements = new System.Windows.Forms.Label();
            this.lblResearchUnits = new System.Windows.Forms.Label();
            this.txtResearchUnits = new System.Windows.Forms.TextBox();
            this.chkNextEra = new System.Windows.Forms.CheckBox();
            this.lblEra = new System.Windows.Forms.Label();
            this.cboEra = new System.Windows.Forms.ComboBox();
            this.lvwTechnology = new System.Windows.Forms.ListView();
            this.hdrName = new System.Windows.Forms.ColumnHeader();
            this.hdrResearch = new System.Windows.Forms.ColumnHeader();
            this.hdrEra = new System.Windows.Forms.ColumnHeader();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkEspionage = new System.Windows.Forms.CheckBox();
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
            // lstRequirements
            // 
            this.lstRequirements.CheckOnClick = true;
            this.lstRequirements.FormattingEnabled = true;
            resources.ApplyResources(this.lstRequirements, "lstRequirements");
            this.lstRequirements.Name = "lstRequirements";
            // 
            // lblRequirements
            // 
            this.lblRequirements.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblRequirements, "lblRequirements");
            this.lblRequirements.Name = "lblRequirements";
            // 
            // lblResearchUnits
            // 
            this.lblResearchUnits.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblResearchUnits, "lblResearchUnits");
            this.lblResearchUnits.Name = "lblResearchUnits";
            // 
            // txtResearchUnits
            // 
            resources.ApplyResources(this.txtResearchUnits, "txtResearchUnits");
            this.txtResearchUnits.Name = "txtResearchUnits";
            // 
            // chkNextEra
            // 
            resources.ApplyResources(this.chkNextEra, "chkNextEra");
            this.chkNextEra.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkNextEra.Name = "chkNextEra";
            // 
            // lblEra
            // 
            this.lblEra.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblEra, "lblEra");
            this.lblEra.Name = "lblEra";
            // 
            // cboEra
            // 
            this.cboEra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEra.FormattingEnabled = true;
            resources.ApplyResources(this.cboEra, "cboEra");
            this.cboEra.Name = "cboEra";
            // 
            // lvwTechnology
            // 
            this.lvwTechnology.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrName,
            this.hdrResearch,
            this.hdrEra});
            resources.ApplyResources(this.lvwTechnology, "lvwTechnology");
            this.lvwTechnology.FullRowSelect = true;
            this.lvwTechnology.Name = "lvwTechnology";
            this.lvwTechnology.View = System.Windows.Forms.View.Details;
            this.lvwTechnology.SelectedIndexChanged += new System.EventHandler(this.lvwTechnology_SelectedIndexChanged);
            // 
            // hdrName
            // 
            resources.ApplyResources(this.hdrName, "hdrName");
            // 
            // hdrResearch
            // 
            resources.ApplyResources(this.hdrResearch, "hdrResearch");
            // 
            // hdrEra
            // 
            resources.ApplyResources(this.hdrEra, "hdrEra");
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkEspionage);
            this.panel1.Controls.Add(this.lblRequirements);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.chkNextEra);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.lblEra);
            this.panel1.Controls.Add(this.lblResearchUnits);
            this.panel1.Controls.Add(this.cboEra);
            this.panel1.Controls.Add(this.txtResearchUnits);
            this.panel1.Controls.Add(this.lstRequirements);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // chkEspionage
            // 
            resources.ApplyResources(this.chkEspionage, "chkEspionage");
            this.chkEspionage.Name = "chkEspionage";
            // 
            // TechnologyControl
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lvwTechnology);
            this.Name = "TechnologyControl";
            resources.ApplyResources(this, "$this");
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void lvwTechnology_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataSet ds = EditorApp.Instance.RuleSetData;
	
			if(this.lvwTechnology.SelectedItems.Count == 1)
			{
				DataView dv = (DataView)this.currencyManager.List;
				dv.Sort = "TechnologyID";
				DataRow row = (DataRow)lvwTechnology.SelectedItems[0].Tag;
				int index = dv.Find(row["TechnologyID"]);
				this.currencyManager.Position = index;
			}
			else
			{
				this.currencyManager.Position = -1;
			}
		}

		
	}
}
