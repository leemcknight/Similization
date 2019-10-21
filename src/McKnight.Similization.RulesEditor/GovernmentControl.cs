using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace McKnight.SimilizationRulesEditor
{
	/// <summary>
	/// Editor User Control responsible for editing Government information.
	/// </summary>
	public class GovernmentControl : EditorUserControl
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtLeaderTitle;
		private System.Windows.Forms.CheckBox chkStarting;
		private System.Windows.Forms.CheckBox chkFallback;
		private System.Windows.Forms.TextBox txtMetropolis;
		private System.Windows.Forms.TextBox txtCity;
		private System.Windows.Forms.TextBox txtTown;
		private System.Windows.Forms.CheckBox chkFree;
		private System.Windows.Forms.ComboBox cboEfficiency;
		private System.Windows.Forms.ComboBox cboCorruption;
		private System.Windows.Forms.ComboBox cboTechnology;
		private System.Windows.Forms.GroupBox grpUnit;
		private System.Windows.Forms.ListView lvwGovernment;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ColumnHeader hdrName;
		private System.Windows.Forms.ColumnHeader hdrTitle;
		private System.Windows.Forms.ColumnHeader hdrEfficiency;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Label lblMetropolis;
		private System.Windows.Forms.Label lblCity;
		private System.Windows.Forms.Label lblTown;
		private System.Windows.Forms.Label lblEfficiency;
		private System.Windows.Forms.Label lblCorruption;
		private System.Windows.Forms.Label lblTechnology;
		private System.Windows.Forms.Panel pnlGovernment;
		private System.ComponentModel.IContainer components;
        private BindingSource bindingSource;

		public GovernmentControl() : base()
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
            object item = this.bindingSource.AddNew();
            int index = this.bindingSource.IndexOf(item);
            this.bindingSource.Position = index;			
		}

		public override void SaveData()
		{
            this.bindingSource.EndEdit();			
			FillListView();
		}


		public override void UndoChanges()
		{
            this.bindingSource.CancelEdit();
		}

		public override void ShowData()
		{
			if(this.bindingSource == null)
			{
				DataBind();
			}

			FillListView();
		}



		private void DataBind()
		{
			DataSet ds = EditorApp.Instance.RuleSetData;
			this.txtLeaderTitle.DataBindings.Add("Text", ds, "Government.LeaderTitle");
			this.txtName.DataBindings.Add("Text", ds, "Government.Name");
			this.chkFallback.DataBindings.Add("Checked", ds, "Government.IsFallBack");
			this.chkStarting.DataBindings.Add("Checked", ds, "Government.IsPrimary");
			this.txtTown.DataBindings.Add("Text", ds, "Government.FreeTownUnits");
			this.txtCity.DataBindings.Add("Text", ds, "Government.FreeCityUnits");
			this.txtMetropolis.DataBindings.Add("Text", ds, "Government.FreeMetropolisUnits");
            this.bindingSource = new BindingSource(ds, "Government");
		}

		private void FillListView()
		{
			DataTable table = EditorApp.Instance.RuleSetData.Tables["Government"];

			ListViewItem item;

			lvwGovernment.Items.Clear();

			foreach(DataRow row in table.Rows)
			{
				item = lvwGovernment.Items.Add(row["Name"].ToString());
				item.SubItems.Add(row["LeaderTitle"].ToString());
				item.Tag = row;
			}
		}


		private void lvwGovernment_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataSet ds = EditorApp.Instance.RuleSetData;
	
			if(this.lvwGovernment.SelectedItems.Count == 1)
			{
				DataView dv = (DataView)this.bindingSource.List;
				dv.Sort = "GovernmentID";
				DataRow row = (DataRow)lvwGovernment.SelectedItems[0].Tag;
				int index = dv.Find(row["GovernmentID"]);
				this.bindingSource.Position = index;

			}
			else
			{
				this.bindingSource.Position = -1;
			}
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GovernmentControl));
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtLeaderTitle = new System.Windows.Forms.TextBox();
            this.chkStarting = new System.Windows.Forms.CheckBox();
            this.chkFallback = new System.Windows.Forms.CheckBox();
            this.grpUnit = new System.Windows.Forms.GroupBox();
            this.txtMetropolis = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtTown = new System.Windows.Forms.TextBox();
            this.lblMetropolis = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblTown = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkFree = new System.Windows.Forms.CheckBox();
            this.lblEfficiency = new System.Windows.Forms.Label();
            this.lblCorruption = new System.Windows.Forms.Label();
            this.cboEfficiency = new System.Windows.Forms.ComboBox();
            this.cboCorruption = new System.Windows.Forms.ComboBox();
            this.lblTechnology = new System.Windows.Forms.Label();
            this.cboTechnology = new System.Windows.Forms.ComboBox();
            this.lvwGovernment = new System.Windows.Forms.ListView();
            this.hdrName = new System.Windows.Forms.ColumnHeader();
            this.hdrTitle = new System.Windows.Forms.ColumnHeader();
            this.hdrEfficiency = new System.Windows.Forms.ColumnHeader();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlGovernment = new System.Windows.Forms.Panel();
            this.grpUnit.SuspendLayout();
            this.pnlGovernment.SuspendLayout();
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
            // lblTitle
            // 
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // txtLeaderTitle
            // 
            resources.ApplyResources(this.txtLeaderTitle, "txtLeaderTitle");
            this.txtLeaderTitle.Name = "txtLeaderTitle";
            // 
            // chkStarting
            // 
            resources.ApplyResources(this.chkStarting, "chkStarting");
            this.chkStarting.Name = "chkStarting";
            // 
            // chkFallback
            // 
            resources.ApplyResources(this.chkFallback, "chkFallback");
            this.chkFallback.Name = "chkFallback";
            // 
            // grpUnit
            // 
            this.grpUnit.Controls.Add(this.txtMetropolis);
            this.grpUnit.Controls.Add(this.txtCity);
            this.grpUnit.Controls.Add(this.txtTown);
            this.grpUnit.Controls.Add(this.lblMetropolis);
            this.grpUnit.Controls.Add(this.lblCity);
            this.grpUnit.Controls.Add(this.lblTown);
            this.grpUnit.Controls.Add(this.label1);
            this.grpUnit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.grpUnit, "grpUnit");
            this.grpUnit.Name = "grpUnit";
            this.grpUnit.TabStop = false;
            // 
            // txtMetropolis
            // 
            resources.ApplyResources(this.txtMetropolis, "txtMetropolis");
            this.txtMetropolis.Name = "txtMetropolis";
            // 
            // txtCity
            // 
            resources.ApplyResources(this.txtCity, "txtCity");
            this.txtCity.Name = "txtCity";
            // 
            // txtTown
            // 
            resources.ApplyResources(this.txtTown, "txtTown");
            this.txtTown.Name = "txtTown";
            // 
            // lblMetropolis
            // 
            this.lblMetropolis.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblMetropolis, "lblMetropolis");
            this.lblMetropolis.Name = "lblMetropolis";
            // 
            // lblCity
            // 
            this.lblCity.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblCity, "lblCity");
            this.lblCity.Name = "lblCity";
            // 
            // lblTown
            // 
            this.lblTown.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblTown, "lblTown");
            this.lblTown.Name = "lblTown";
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // chkFree
            // 
            resources.ApplyResources(this.chkFree, "chkFree");
            this.chkFree.Name = "chkFree";
            // 
            // lblEfficiency
            // 
            this.lblEfficiency.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblEfficiency, "lblEfficiency");
            this.lblEfficiency.Name = "lblEfficiency";
            // 
            // lblCorruption
            // 
            this.lblCorruption.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblCorruption, "lblCorruption");
            this.lblCorruption.Name = "lblCorruption";
            // 
            // cboEfficiency
            // 
            this.cboEfficiency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEfficiency.FormattingEnabled = true;
            resources.ApplyResources(this.cboEfficiency, "cboEfficiency");
            this.cboEfficiency.Name = "cboEfficiency";
            // 
            // cboCorruption
            // 
            this.cboCorruption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCorruption.FormattingEnabled = true;
            resources.ApplyResources(this.cboCorruption, "cboCorruption");
            this.cboCorruption.Name = "cboCorruption";
            // 
            // lblTechnology
            // 
            this.lblTechnology.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblTechnology, "lblTechnology");
            this.lblTechnology.Name = "lblTechnology";
            // 
            // cboTechnology
            // 
            this.cboTechnology.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTechnology.FormattingEnabled = true;
            resources.ApplyResources(this.cboTechnology, "cboTechnology");
            this.cboTechnology.Name = "cboTechnology";
            // 
            // lvwGovernment
            // 
            this.lvwGovernment.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrName,
            this.hdrTitle,
            this.hdrEfficiency});
            resources.ApplyResources(this.lvwGovernment, "lvwGovernment");
            this.lvwGovernment.FullRowSelect = true;
            this.lvwGovernment.Name = "lvwGovernment";
            this.lvwGovernment.View = System.Windows.Forms.View.Details;
            this.lvwGovernment.SelectedIndexChanged += new System.EventHandler(this.lvwGovernment_SelectedIndexChanged);
            // 
            // hdrName
            // 
            resources.ApplyResources(this.hdrName, "hdrName");
            // 
            // hdrTitle
            // 
            resources.ApplyResources(this.hdrTitle, "hdrTitle");
            // 
            // hdrEfficiency
            // 
            resources.ApplyResources(this.hdrEfficiency, "hdrEfficiency");
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // pnlGovernment
            // 
            this.pnlGovernment.Controls.Add(this.lblTechnology);
            this.pnlGovernment.Controls.Add(this.chkFallback);
            this.pnlGovernment.Controls.Add(this.chkStarting);
            this.pnlGovernment.Controls.Add(this.chkFree);
            this.pnlGovernment.Controls.Add(this.lblEfficiency);
            this.pnlGovernment.Controls.Add(this.cboTechnology);
            this.pnlGovernment.Controls.Add(this.lblCorruption);
            this.pnlGovernment.Controls.Add(this.lblTitle);
            this.pnlGovernment.Controls.Add(this.lblName);
            this.pnlGovernment.Controls.Add(this.txtName);
            this.pnlGovernment.Controls.Add(this.cboEfficiency);
            this.pnlGovernment.Controls.Add(this.grpUnit);
            this.pnlGovernment.Controls.Add(this.cboCorruption);
            this.pnlGovernment.Controls.Add(this.txtLeaderTitle);
            resources.ApplyResources(this.pnlGovernment, "pnlGovernment");
            this.pnlGovernment.Name = "pnlGovernment";
            // 
            // GovernmentControl
            // 
            this.Controls.Add(this.pnlGovernment);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lvwGovernment);
            resources.ApplyResources(this, "$this");
            this.Name = "GovernmentControl";
            this.grpUnit.ResumeLayout(false);
            this.grpUnit.PerformLayout();
            this.pnlGovernment.ResumeLayout(false);
            this.pnlGovernment.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
		
	}
}
