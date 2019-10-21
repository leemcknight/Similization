using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace McKnight.SimilizationRulesEditor
{
	/// <summary>
	/// Editor User Control responsible for editing Unit information.
	/// </summary>
	public class UnitControl : EditorUserControl
	{
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblCost;
		private System.Windows.Forms.TextBox txtCost;
		private System.Windows.Forms.Label lblAttack;
		private System.Windows.Forms.Label lblDefense;
		private System.Windows.Forms.Label lblMovement;
		private System.Windows.Forms.TextBox txtRateOfFire;
		private System.Windows.Forms.TextBox txtBombardment;
		private System.Windows.Forms.TextBox txtRange;
		private System.Windows.Forms.CheckBox chkCanBombard;
		private System.Windows.Forms.CheckedListBox lstResources;
		private System.Windows.Forms.Label lblPrerequisite;
		private System.Windows.Forms.ComboBox cboPrerequisite;
		private System.Windows.Forms.CheckBox chkSettle;
		private System.Windows.Forms.CheckBox chkWork;
		private System.Windows.Forms.Label lblUpdgrade;
		private System.Windows.Forms.ComboBox cboUpgrade;
		private System.Windows.Forms.Label lblPopulation;
		private System.Windows.Forms.TextBox txtPopulation;
		private System.Windows.Forms.GroupBox grpAbilities;
		private System.Windows.Forms.CheckBox chkMerge;
		private System.Windows.Forms.TextBox txtAttack;
		private System.Windows.Forms.TextBox txtDefense;
		private System.Windows.Forms.TextBox txtMovement;
		private System.Windows.Forms.ListView lvwUnit;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ColumnHeader hdrName;
		private System.Windows.Forms.ColumnHeader hdrCost;
		private System.Windows.Forms.ColumnHeader hdrAttack;
		private System.Windows.Forms.ColumnHeader hdrDefense;
		private System.Windows.Forms.ColumnHeader hdrMovement;
		private System.Windows.Forms.Label lblBombardment;
		private System.Windows.Forms.Label lblRange;
		private System.Windows.Forms.Label lblRate;
		private System.Windows.Forms.GroupBox grpBombardment;
		private System.Windows.Forms.Label lblResources;
		private System.Windows.Forms.Panel pnlUnit;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label lblType;
		private System.Windows.Forms.ComboBox cboType;
		private System.Windows.Forms.CheckBox chkPrecision;
        private BindingSource bindingSource;        
        private BindingSource technologySource;

		public UnitControl() : base()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.BindingContext = new BindingContext();
            DataSet ds = EditorApp.Instance.RuleSetData;
            this.bindingSource = new BindingSource(ds, "Unit");
            this.technologySource = new BindingSource(ds, "Technology");
            BindData();
		}

		/// <summary> 
		/// Clean up any ResourceType being used.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitControl));
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblCost = new System.Windows.Forms.Label();
            this.txtCost = new System.Windows.Forms.TextBox();
            this.lblAttack = new System.Windows.Forms.Label();
            this.lblDefense = new System.Windows.Forms.Label();
            this.lblMovement = new System.Windows.Forms.Label();
            this.lblBombardment = new System.Windows.Forms.Label();
            this.lblRange = new System.Windows.Forms.Label();
            this.lblRate = new System.Windows.Forms.Label();
            this.grpBombardment = new System.Windows.Forms.GroupBox();
            this.chkPrecision = new System.Windows.Forms.CheckBox();
            this.txtRateOfFire = new System.Windows.Forms.TextBox();
            this.chkCanBombard = new System.Windows.Forms.CheckBox();
            this.txtBombardment = new System.Windows.Forms.TextBox();
            this.txtRange = new System.Windows.Forms.TextBox();
            this.lblResources = new System.Windows.Forms.Label();
            this.lstResources = new System.Windows.Forms.CheckedListBox();
            this.lblPrerequisite = new System.Windows.Forms.Label();
            this.cboPrerequisite = new System.Windows.Forms.ComboBox();
            this.chkSettle = new System.Windows.Forms.CheckBox();
            this.chkWork = new System.Windows.Forms.CheckBox();
            this.lblUpdgrade = new System.Windows.Forms.Label();
            this.cboUpgrade = new System.Windows.Forms.ComboBox();
            this.lblPopulation = new System.Windows.Forms.Label();
            this.txtPopulation = new System.Windows.Forms.TextBox();
            this.grpAbilities = new System.Windows.Forms.GroupBox();
            this.chkMerge = new System.Windows.Forms.CheckBox();
            this.txtAttack = new System.Windows.Forms.TextBox();
            this.txtDefense = new System.Windows.Forms.TextBox();
            this.txtMovement = new System.Windows.Forms.TextBox();
            this.lvwUnit = new System.Windows.Forms.ListView();
            this.hdrName = new System.Windows.Forms.ColumnHeader();
            this.hdrCost = new System.Windows.Forms.ColumnHeader();
            this.hdrAttack = new System.Windows.Forms.ColumnHeader();
            this.hdrDefense = new System.Windows.Forms.ColumnHeader();
            this.hdrMovement = new System.Windows.Forms.ColumnHeader();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlUnit = new System.Windows.Forms.Panel();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.grpBombardment.SuspendLayout();
            this.grpAbilities.SuspendLayout();
            this.pnlUnit.SuspendLayout();
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
            this.txtName.TextChanged += new System.EventHandler(this.UnitDetailChanged);
            // 
            // lblCost
            // 
            this.lblCost.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblCost, "lblCost");
            this.lblCost.Name = "lblCost";
            // 
            // txtCost
            // 
            resources.ApplyResources(this.txtCost, "txtCost");
            this.txtCost.Name = "txtCost";
            this.txtCost.TextChanged += new System.EventHandler(this.UnitDetailChanged);
            // 
            // lblAttack
            // 
            this.lblAttack.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblAttack, "lblAttack");
            this.lblAttack.Name = "lblAttack";
            // 
            // lblDefense
            // 
            this.lblDefense.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblDefense, "lblDefense");
            this.lblDefense.Name = "lblDefense";
            // 
            // lblMovement
            // 
            this.lblMovement.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblMovement, "lblMovement");
            this.lblMovement.Name = "lblMovement";
            // 
            // lblBombardment
            // 
            this.lblBombardment.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblBombardment, "lblBombardment");
            this.lblBombardment.Name = "lblBombardment";
            // 
            // lblRange
            // 
            this.lblRange.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblRange, "lblRange");
            this.lblRange.Name = "lblRange";
            // 
            // lblRate
            // 
            this.lblRate.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblRate, "lblRate");
            this.lblRate.Name = "lblRate";
            // 
            // grpBombardment
            // 
            this.grpBombardment.Controls.Add(this.chkPrecision);
            this.grpBombardment.Controls.Add(this.txtRateOfFire);
            this.grpBombardment.Controls.Add(this.chkCanBombard);
            this.grpBombardment.Controls.Add(this.lblRate);
            this.grpBombardment.Controls.Add(this.lblBombardment);
            this.grpBombardment.Controls.Add(this.lblRange);
            this.grpBombardment.Controls.Add(this.txtBombardment);
            this.grpBombardment.Controls.Add(this.txtRange);
            this.grpBombardment.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.grpBombardment, "grpBombardment");
            this.grpBombardment.Name = "grpBombardment";
            this.grpBombardment.TabStop = false;
            // 
            // chkPrecision
            // 
            resources.ApplyResources(this.chkPrecision, "chkPrecision");
            this.chkPrecision.Name = "chkPrecision";
            // 
            // txtRateOfFire
            // 
            resources.ApplyResources(this.txtRateOfFire, "txtRateOfFire");
            this.txtRateOfFire.Name = "txtRateOfFire";
            // 
            // chkCanBombard
            // 
            resources.ApplyResources(this.chkCanBombard, "chkCanBombard");
            this.chkCanBombard.Name = "chkCanBombard";
            this.chkCanBombard.CheckedChanged += new System.EventHandler(this._canBombardCheckBox_CheckedChanged);
            // 
            // txtBombardment
            // 
            resources.ApplyResources(this.txtBombardment, "txtBombardment");
            this.txtBombardment.Name = "txtBombardment";
            // 
            // txtRange
            // 
            resources.ApplyResources(this.txtRange, "txtRange");
            this.txtRange.Name = "txtRange";
            // 
            // lblResources
            // 
            this.lblResources.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblResources, "lblResources");
            this.lblResources.Name = "lblResources";
            // 
            // lstResources
            // 
            this.lstResources.CheckOnClick = true;
            this.lstResources.FormattingEnabled = true;
            resources.ApplyResources(this.lstResources, "lstResources");
            this.lstResources.Name = "lstResources";
            // 
            // lblPrerequisite
            // 
            this.lblPrerequisite.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblPrerequisite, "lblPrerequisite");
            this.lblPrerequisite.Name = "lblPrerequisite";
            // 
            // cboPrerequisite
            // 
            this.cboPrerequisite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrerequisite.FormattingEnabled = true;
            resources.ApplyResources(this.cboPrerequisite, "cboPrerequisite");
            this.cboPrerequisite.Name = "cboPrerequisite";
            // 
            // chkSettle
            // 
            resources.ApplyResources(this.chkSettle, "chkSettle");
            this.chkSettle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkSettle.Name = "chkSettle";
            // 
            // chkWork
            // 
            resources.ApplyResources(this.chkWork, "chkWork");
            this.chkWork.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkWork.Name = "chkWork";
            // 
            // lblUpdgrade
            // 
            this.lblUpdgrade.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblUpdgrade, "lblUpdgrade");
            this.lblUpdgrade.Name = "lblUpdgrade";
            // 
            // cboUpgrade
            // 
            this.cboUpgrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUpgrade.FormattingEnabled = true;
            resources.ApplyResources(this.cboUpgrade, "cboUpgrade");
            this.cboUpgrade.Name = "cboUpgrade";
            // 
            // lblPopulation
            // 
            this.lblPopulation.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblPopulation, "lblPopulation");
            this.lblPopulation.Name = "lblPopulation";
            // 
            // txtPopulation
            // 
            resources.ApplyResources(this.txtPopulation, "txtPopulation");
            this.txtPopulation.Name = "txtPopulation";
            // 
            // grpAbilities
            // 
            this.grpAbilities.Controls.Add(this.chkSettle);
            this.grpAbilities.Controls.Add(this.chkMerge);
            this.grpAbilities.Controls.Add(this.chkWork);
            this.grpAbilities.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpAbilities.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.grpAbilities, "grpAbilities");
            this.grpAbilities.Name = "grpAbilities";
            this.grpAbilities.TabStop = false;
            // 
            // chkMerge
            // 
            resources.ApplyResources(this.chkMerge, "chkMerge");
            this.chkMerge.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkMerge.Name = "chkMerge";
            // 
            // txtAttack
            // 
            resources.ApplyResources(this.txtAttack, "txtAttack");
            this.txtAttack.Name = "txtAttack";
            // 
            // txtDefense
            // 
            resources.ApplyResources(this.txtDefense, "txtDefense");
            this.txtDefense.Name = "txtDefense";
            // 
            // txtMovement
            // 
            resources.ApplyResources(this.txtMovement, "txtMovement");
            this.txtMovement.Name = "txtMovement";
            // 
            // lvwUnit
            // 
            this.lvwUnit.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrName,
            this.hdrCost,
            this.hdrAttack,
            this.hdrDefense,
            this.hdrMovement});
            resources.ApplyResources(this.lvwUnit, "lvwUnit");
            this.lvwUnit.FullRowSelect = true;
            this.lvwUnit.HideSelection = false;
            this.lvwUnit.Name = "lvwUnit";
            this.lvwUnit.View = System.Windows.Forms.View.Details;
            this.lvwUnit.SelectedIndexChanged += new System.EventHandler(this.lvwUnit_SelectedIndexChanged);
            // 
            // hdrName
            // 
            resources.ApplyResources(this.hdrName, "hdrName");
            // 
            // hdrCost
            // 
            resources.ApplyResources(this.hdrCost, "hdrCost");
            // 
            // hdrAttack
            // 
            resources.ApplyResources(this.hdrAttack, "hdrAttack");
            // 
            // hdrDefense
            // 
            resources.ApplyResources(this.hdrDefense, "hdrDefense");
            // 
            // hdrMovement
            // 
            resources.ApplyResources(this.hdrMovement, "hdrMovement");
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // pnlUnit
            // 
            this.pnlUnit.Controls.Add(this.cboType);
            this.pnlUnit.Controls.Add(this.lblType);
            this.pnlUnit.Controls.Add(this.txtMovement);
            this.pnlUnit.Controls.Add(this.txtDefense);
            this.pnlUnit.Controls.Add(this.lblDefense);
            this.pnlUnit.Controls.Add(this.txtAttack);
            this.pnlUnit.Controls.Add(this.cboPrerequisite);
            this.pnlUnit.Controls.Add(this.txtCost);
            this.pnlUnit.Controls.Add(this.lblPrerequisite);
            this.pnlUnit.Controls.Add(this.grpAbilities);
            this.pnlUnit.Controls.Add(this.lstResources);
            this.pnlUnit.Controls.Add(this.lblAttack);
            this.pnlUnit.Controls.Add(this.txtPopulation);
            this.pnlUnit.Controls.Add(this.lblPopulation);
            this.pnlUnit.Controls.Add(this.grpBombardment);
            this.pnlUnit.Controls.Add(this.lblCost);
            this.pnlUnit.Controls.Add(this.txtName);
            this.pnlUnit.Controls.Add(this.lblMovement);
            this.pnlUnit.Controls.Add(this.lblResources);
            this.pnlUnit.Controls.Add(this.cboUpgrade);
            this.pnlUnit.Controls.Add(this.lblName);
            this.pnlUnit.Controls.Add(this.lblUpdgrade);
            resources.ApplyResources(this.pnlUnit, "pnlUnit");
            this.pnlUnit.Name = "pnlUnit";
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            resources.ApplyResources(this.cboType, "cboType");
            this.cboType.Name = "cboType";
            // 
            // lblType
            // 
            resources.ApplyResources(this.lblType, "lblType");
            this.lblType.Name = "lblType";
            // 
            // UnitControl
            // 
            this.Controls.Add(this.pnlUnit);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lvwUnit);
            resources.ApplyResources(this, "$this");
            this.Name = "UnitControl";
            this.grpBombardment.ResumeLayout(false);
            this.grpBombardment.PerformLayout();
            this.grpAbilities.ResumeLayout(false);
            this.pnlUnit.ResumeLayout(false);
            this.pnlUnit.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion



		public override void AddNew()
		{
            this.bindingSource.AddNew();
            this.bindingSource.MoveLast();			
		}

		public override void SaveData()
		{
            this.bindingSource.EndEdit();			
			FillListView();	
		}



		public override void Delete()
		{
			DialogResult dr = 
				MessageBox.Show("Are you sure you want to delete this item?", 
								"Confirm Delete",
								MessageBoxButtons.YesNo,
								MessageBoxIcon.Exclamation);

			if(dr == DialogResult.Yes)
			{
				DataSet ds = EditorApp.Instance.RuleSetData;
				DataRow current = ((DataRowView)this.bindingSource.Current).Row;
				ds.Tables["Unit"].Rows.Remove(current);
                this.bindingSource.MoveLast();
				FillListView();
			}
		}



		public override void ShowData()
		{
			FillListView();
		}

		private void BindData()
		{
			DataSet ds = EditorApp.Instance.RuleSetData;
			this.txtAttack.DataBindings.Add("Text", this.bindingSource, "Attack");
            this.txtBombardment.DataBindings.Add("Text", this.bindingSource, "Bombardment");
            this.txtCost.DataBindings.Add("Text", this.bindingSource, "Cost");
            this.txtDefense.DataBindings.Add("Text", this.bindingSource, "Defense");
            this.txtMovement.DataBindings.Add("Text", this.bindingSource, "Movement");
            this.txtName.DataBindings.Add("Text", this.bindingSource, "Name");
            this.txtPopulation.DataBindings.Add("Text", this.bindingSource, "PopulationPoints");
            this.txtRange.DataBindings.Add("Text", this.bindingSource, "BombardmentRange");
            this.txtRateOfFire.DataBindings.Add("Text", this.bindingSource, "RateOfFire");
            this.chkMerge.DataBindings.Add("Checked", this.bindingSource, "CanMergeWithCity");
            this.chkSettle.DataBindings.Add("Checked", this.bindingSource, "CanSettle");
            this.chkWork.DataBindings.Add("Checked", this.bindingSource, "CanWork");
            this.chkPrecision.DataBindings.Add("Checked", this.bindingSource, "PrecisionBombardment");
			this.cboUpgrade.DataSource = ds.Tables["Unit"];
			this.cboUpgrade.DisplayMember = "Name";
			this.cboUpgrade.ValueMember = "UnitID";
            this.cboUpgrade.DataBindings.Add("SelectedValue", this.bindingSource, "Upgrade");
            //this.cboType.DataSource = Enum.GetNames(typeof(McKnight.Similization.Core.UnitType));
            //this.cboType.DataBindings.Add("SelectedValue", this.bindingSource, "Type");
            this.cboPrerequisite.DataSource = technologySource;
			this.cboPrerequisite.DisplayMember = "Name";
			this.cboPrerequisite.ValueMember = "TechnologyID";
            this.cboPrerequisite.DataBindings.Add("SelectedValue", this.bindingSource, "TechnologyID");
			this.lstResources.DataSource = ds.Tables["Resource"];
			this.lstResources.DisplayMember = "Name";			
		}

		private void FillListView()
		{
			DataSet ds = EditorApp.Instance.RuleSetData;

			ListViewItem item;

			lvwUnit.Items.Clear();

			foreach(DataRow row in ds.Tables["Unit"].Rows)
			{
				item = lvwUnit.Items.Add(row["Name"].ToString());
				item.SubItems.Add(row["Cost"].ToString());
				item.SubItems.Add(row["Attack"].ToString());
				item.SubItems.Add(row["Defense"].ToString());
				item.SubItems.Add(row["Movement"].ToString());
				item.Tag = row;
			}
		}

		private void _canBombardCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			txtRange.Enabled = chkCanBombard.Checked;
			txtBombardment.Enabled = chkCanBombard.Checked;
			txtRateOfFire.Enabled = chkCanBombard.Checked;
		}

		private void UnitDetailChanged(object sender, System.EventArgs e)
		{
			this.FormState = FormState.Edit;
		}

		private void lvwUnit_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataSet ds = EditorApp.Instance.RuleSetData;
	
			if(this.lvwUnit.SelectedItems.Count == 1)
			{
				DataView dv = (DataView)this.bindingSource.List;
				dv.Sort = "UnitID";
				DataRow row = (DataRow)lvwUnit.SelectedItems[0].Tag;
				int index = dv.Find(row["UnitID"]);
				this.bindingSource.Position = index;
			}
			else
			{
				this.bindingSource.Position = -1;
			}
		}
	}
}
