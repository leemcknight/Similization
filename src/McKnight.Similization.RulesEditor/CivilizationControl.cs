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
	///	Editor User Control responsible for editing Civilization information.
	/// </summary>
	public class CivilizationControl : EditorUserControl
	{
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.CheckBox chkCommercial;
		private System.Windows.Forms.CheckBox chkExpansionist;
		private System.Windows.Forms.CheckBox chkIndustrious;
		private System.Windows.Forms.CheckBox chkMilitaristic;
		private System.Windows.Forms.CheckBox chkScientific;
		private System.Windows.Forms.CheckBox chkReligious;
		private System.Windows.Forms.Label lblCulture;
		private System.Windows.Forms.ComboBox cboCulture;
		private System.Windows.Forms.Label ldlNames;
		private System.Windows.Forms.ListBox lstNames;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.TextBox txtCityName;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Label lblNoun;
		private System.Windows.Forms.TextBox txtNoun;
		private System.Windows.Forms.Label lblAdjective;
		private System.Windows.Forms.TextBox txtAdjective;
		private System.Windows.Forms.Label lblLeader;
		private System.Windows.Forms.TextBox txtLeader;
		private System.Windows.Forms.GroupBox grpAttributes;
		private System.Windows.Forms.ListView lvwCivilization;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ColumnHeader hdrName;
		private System.Windows.Forms.ColumnHeader hdrNoun;
		private System.Windows.Forms.ColumnHeader hdrAdjective;
		private System.Windows.Forms.ColumnHeader hdrLeaderName;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.CheckedListBox lstTechnologies;
		private System.Windows.Forms.Label lblTechnologies;
		private CurrencyManager currencyManager;
		private CurrencyManager technologyCurrencyManager;

		public CivilizationControl() : base()
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CivilizationControl));
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkCommercial = new System.Windows.Forms.CheckBox();
            this.chkExpansionist = new System.Windows.Forms.CheckBox();
            this.chkIndustrious = new System.Windows.Forms.CheckBox();
            this.chkMilitaristic = new System.Windows.Forms.CheckBox();
            this.chkScientific = new System.Windows.Forms.CheckBox();
            this.chkReligious = new System.Windows.Forms.CheckBox();
            this.lblCulture = new System.Windows.Forms.Label();
            this.cboCulture = new System.Windows.Forms.ComboBox();
            this.ldlNames = new System.Windows.Forms.Label();
            this.lstNames = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtCityName = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblNoun = new System.Windows.Forms.Label();
            this.txtNoun = new System.Windows.Forms.TextBox();
            this.lblAdjective = new System.Windows.Forms.Label();
            this.txtAdjective = new System.Windows.Forms.TextBox();
            this.lblLeader = new System.Windows.Forms.Label();
            this.txtLeader = new System.Windows.Forms.TextBox();
            this.grpAttributes = new System.Windows.Forms.GroupBox();
            this.lvwCivilization = new System.Windows.Forms.ListView();
            this.hdrName = new System.Windows.Forms.ColumnHeader();
            this.hdrNoun = new System.Windows.Forms.ColumnHeader();
            this.hdrAdjective = new System.Windows.Forms.ColumnHeader();
            this.hdrLeaderName = new System.Windows.Forms.ColumnHeader();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTechnologies = new System.Windows.Forms.Label();
            this.lstTechnologies = new System.Windows.Forms.CheckedListBox();
            this.grpAttributes.SuspendLayout();
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
            // chkCommercial
            // 
            resources.ApplyResources(this.chkCommercial, "chkCommercial");
            this.chkCommercial.Name = "chkCommercial";
            // 
            // chkExpansionist
            // 
            resources.ApplyResources(this.chkExpansionist, "chkExpansionist");
            this.chkExpansionist.Name = "chkExpansionist";
            // 
            // chkIndustrious
            // 
            resources.ApplyResources(this.chkIndustrious, "chkIndustrious");
            this.chkIndustrious.Name = "chkIndustrious";
            // 
            // chkMilitaristic
            // 
            resources.ApplyResources(this.chkMilitaristic, "chkMilitaristic");
            this.chkMilitaristic.Name = "chkMilitaristic";
            // 
            // chkScientific
            // 
            resources.ApplyResources(this.chkScientific, "chkScientific");
            this.chkScientific.Name = "chkScientific";
            // 
            // chkReligious
            // 
            resources.ApplyResources(this.chkReligious, "chkReligious");
            this.chkReligious.Name = "chkReligious";
            // 
            // lblCulture
            // 
            this.lblCulture.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblCulture, "lblCulture");
            this.lblCulture.Name = "lblCulture";
            // 
            // cboCulture
            // 
            this.cboCulture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCulture.FormattingEnabled = true;
            this.cboCulture.Items.AddRange(new object[] {
            resources.GetString("cboCulture.Items"),
            resources.GetString("cboCulture.Items1"),
            resources.GetString("cboCulture.Items2"),
            resources.GetString("cboCulture.Items3"),
            resources.GetString("cboCulture.Items4")});
            resources.ApplyResources(this.cboCulture, "cboCulture");
            this.cboCulture.Name = "cboCulture";
            // 
            // ldlNames
            // 
            this.ldlNames.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.ldlNames, "ldlNames");
            this.ldlNames.Name = "ldlNames";
            // 
            // lstNames
            // 
            this.lstNames.FormattingEnabled = true;
            resources.ApplyResources(this.lstNames, "lstNames");
            this.lstNames.Name = "lstNames";
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this._addButton_Click);
            // 
            // txtCityName
            // 
            resources.ApplyResources(this.txtCityName, "txtCityName");
            this.txtCityName.Name = "txtCityName";
            // 
            // btnRemove
            // 
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Click += new System.EventHandler(this._removeButton_Click);
            // 
            // lblNoun
            // 
            this.lblNoun.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblNoun, "lblNoun");
            this.lblNoun.Name = "lblNoun";
            // 
            // txtNoun
            // 
            resources.ApplyResources(this.txtNoun, "txtNoun");
            this.txtNoun.Name = "txtNoun";
            // 
            // lblAdjective
            // 
            this.lblAdjective.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblAdjective, "lblAdjective");
            this.lblAdjective.Name = "lblAdjective";
            // 
            // txtAdjective
            // 
            resources.ApplyResources(this.txtAdjective, "txtAdjective");
            this.txtAdjective.Name = "txtAdjective";
            // 
            // lblLeader
            // 
            this.lblLeader.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblLeader, "lblLeader");
            this.lblLeader.Name = "lblLeader";
            // 
            // txtLeader
            // 
            resources.ApplyResources(this.txtLeader, "txtLeader");
            this.txtLeader.Name = "txtLeader";
            // 
            // grpAttributes
            // 
            this.grpAttributes.Controls.Add(this.chkCommercial);
            this.grpAttributes.Controls.Add(this.chkReligious);
            this.grpAttributes.Controls.Add(this.chkExpansionist);
            this.grpAttributes.Controls.Add(this.chkMilitaristic);
            this.grpAttributes.Controls.Add(this.chkIndustrious);
            this.grpAttributes.Controls.Add(this.chkScientific);
            resources.ApplyResources(this.grpAttributes, "grpAttributes");
            this.grpAttributes.Name = "grpAttributes";
            this.grpAttributes.TabStop = false;
            // 
            // lvwCivilization
            // 
            this.lvwCivilization.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrName,
            this.hdrNoun,
            this.hdrAdjective,
            this.hdrLeaderName});
            resources.ApplyResources(this.lvwCivilization, "lvwCivilization");
            this.lvwCivilization.FullRowSelect = true;
            this.lvwCivilization.Name = "lvwCivilization";
            this.lvwCivilization.View = System.Windows.Forms.View.Details;
            this.lvwCivilization.SelectedIndexChanged += new System.EventHandler(this.lvwCivilization_SelectedIndexChanged);
            // 
            // hdrName
            // 
            resources.ApplyResources(this.hdrName, "hdrName");
            // 
            // hdrNoun
            // 
            resources.ApplyResources(this.hdrNoun, "hdrNoun");
            // 
            // hdrAdjective
            // 
            resources.ApplyResources(this.hdrAdjective, "hdrAdjective");
            // 
            // hdrLeaderName
            // 
            resources.ApplyResources(this.hdrLeaderName, "hdrLeaderName");
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTechnologies);
            this.panel1.Controls.Add(this.lstTechnologies);
            this.panel1.Controls.Add(this.cboCulture);
            this.panel1.Controls.Add(this.txtNoun);
            this.panel1.Controls.Add(this.lblLeader);
            this.panel1.Controls.Add(this.grpAttributes);
            this.panel1.Controls.Add(this.txtLeader);
            this.panel1.Controls.Add(this.ldlNames);
            this.panel1.Controls.Add(this.btnRemove);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.lblCulture);
            this.panel1.Controls.Add(this.lstNames);
            this.panel1.Controls.Add(this.lblAdjective);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.txtAdjective);
            this.panel1.Controls.Add(this.txtCityName);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.lblNoun);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // lblTechnologies
            // 
            resources.ApplyResources(this.lblTechnologies, "lblTechnologies");
            this.lblTechnologies.Name = "lblTechnologies";
            // 
            // lstTechnologies
            // 
            this.lstTechnologies.FormattingEnabled = true;
            resources.ApplyResources(this.lstTechnologies, "lstTechnologies");
            this.lstTechnologies.Name = "lstTechnologies";
            this.lstTechnologies.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstTechnologies_ItemCheck);
            // 
            // CivilizationControl
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lvwCivilization);
            this.Name = "CivilizationControl";
            resources.ApplyResources(this, "$this");
            this.grpAttributes.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

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

		public override void AddNew()
		{
			this.currencyManager.AddNew();
			this.currencyManager.Position = this.currencyManager.Count - 1;
		}

		public override void Delete()
		{
			this.currencyManager.RemoveAt(this.currencyManager.Position);
			this.currencyManager.Position = this.currencyManager.Count - 1;
		}

		public override void UndoChanges()
		{
			this.currencyManager.CancelCurrentEdit();
		}

		private void DataBind()
		{
			DataSet ds = EditorApp.Instance.RuleSetData;       
			this.txtAdjective.DataBindings.Add("Text", ds, "Civilization.Adjective");
			this.txtName.DataBindings.Add("Text", ds, "Civilization.Name");
			this.txtLeader.DataBindings.Add("Text", ds, "Civilization.LeaderName");
			this.txtNoun.DataBindings.Add("Text", ds, "Civilization.Noun");
			this.chkCommercial.DataBindings.Add("Checked", ds, "Civilization.Commercial");
			this.chkExpansionist.DataBindings.Add("Checked", ds, "Civilization.Expansionist");
			this.chkIndustrious.DataBindings.Add("Checked", ds, "Civilization.Industrious");
			this.chkMilitaristic.DataBindings.Add("Checked", ds, "Civilization.Militaristic");
			this.chkReligious.DataBindings.Add("Checked", ds, "Civilization.Religious");
			this.chkScientific.DataBindings.Add("Checked", ds, "Civilization.Scientific");
			this.lstTechnologies.DataSource = ds.Tables["Technology"];
			this.lstTechnologies.DisplayMember = "Name";            
			this.currencyManager = (CurrencyManager)this.BindingContext[ds, "Civilization"];
            this.currencyManager.PositionChanged += new EventHandler(currencyManager_PositionChanged);
		}

        void currencyManager_PositionChanged(object sender, EventArgs e)
        {
            FilterCityNames();
            SelectStartingTechnologies();
        }

		private void FillListView()
		{
			DataTable table = EditorApp.Instance.RuleSetData.Tables["Civilization"];
			ListViewItem item;
			lvwCivilization.Items.Clear();
			foreach(DataRow row in table.Rows)
			{
				item = lvwCivilization.Items.Add(row["Name"].ToString());
				item.SubItems.Add(row["Noun"].ToString());
				item.SubItems.Add(row["Adjective"].ToString());
				item.SubItems.Add(row["LeaderName"].ToString());
				item.Tag = row;
			}
		}

		private void FilterCityNames()
		{
			DataTable table = EditorApp.Instance.RuleSetData.Tables["CityName"];
			DataRow row = ((DataRowView)this.currencyManager.Current).Row;
			string rowFilter = "CivilizationID=" + row["CivilizationID"];
			string sort = "Name";
			DataView dv = new DataView(table, rowFilter, sort, DataViewRowState.CurrentRows);
			this.lstNames.DataSource = dv;
			this.lstNames.DisplayMember = "Name";
		}

        private void SelectStartingTechnologies()
        {
            
        }

		private void _addButton_Click(object sender, System.EventArgs e)
		{
			DataTable table = EditorApp.Instance.RuleSetData.Tables["CityName"];
			DataRow newRow = table.NewRow();
			DataRow civilizationRow = ((DataRowView)this.currencyManager.Current).Row;
			newRow["CivilizationID"] = civilizationRow["civilizationID"];
			newRow["Name"] = txtCityName.Text;
			table.Rows.Add(newRow);
			txtCityName.Clear();
		}

		private void _removeButton_Click(object sender, System.EventArgs e)
		{
			DataView dv = (DataView)this.lstNames.DataSource;
			DataRow row = ((DataRowView)this.lstNames.SelectedItem).Row;
			dv.Table.Rows.Remove(row);
		}

		private void lvwCivilization_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataSet ds = EditorApp.Instance.RuleSetData;
	
			if(this.lvwCivilization.SelectedItems.Count == 1)
			{
				DataView dv = (DataView)this.currencyManager.List;
				dv.Sort = "CivilizationID";
				DataRow row = (DataRow)lvwCivilization.SelectedItems[0].Tag;
				int index = dv.Find(row["CivilizationID"]);
				this.currencyManager.Position = index;				
			}
			else
			{
				this.currencyManager.Position = -1;
			}
		}

		private void lstTechnologies_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			DataSet ds = EditorApp.Instance.RuleSetData;
			DataTable table = ds.Tables["CivilizationStartingTechnology"];
			DataRowView rowView = (DataRowView)this.currencyManager.Current;
			DataRow currentRow = rowView.Row;
			DataRowView technologyRowView = (DataRowView)this.lstTechnologies.SelectedItem;
			DataRow technologyRow = technologyRowView.Row;
			DataRow row = table.NewRow();
			row["CivilizationID"] = currentRow["CivilizationID"];
			row["TechnologyID"] = technologyRow["TechnologyID"];
			table.Rows.Add(row);
		}
	}
}
