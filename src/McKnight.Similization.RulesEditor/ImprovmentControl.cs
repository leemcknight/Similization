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
	/// Editor User Control responsible for editing Improvement information.
	/// </summary>
	public class ImprovementControl : EditorUserControl
	{
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.CheckedListBox lstTechnologies;
		private System.Windows.Forms.CheckedListBox lstResources;
		private System.Windows.Forms.CheckedListBox lstImprovements;
		private System.Windows.Forms.TextBox txtCost;
		private System.Windows.Forms.TextBox txtPollution;
		private System.Windows.Forms.TextBox txtMaintenance;
		private System.Windows.Forms.TextBox txtCulture;
		private System.Windows.Forms.ListView lvwImprovement;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ColumnHeader hdrName;
		private System.Windows.Forms.ColumnHeader hdrCost;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label lblCost;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblMaintenance;
		private System.Windows.Forms.Label lblCulture;
		private System.Windows.Forms.Label lblTechnologies;
		private System.Windows.Forms.Label lblResources;
		private System.Windows.Forms.Label lblImprovements;
		private System.Windows.Forms.Label lblPollution;
		private CurrencyManager currencyManager;

		public ImprovementControl() : base()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImprovementControl));
            this.lblCost = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblMaintenance = new System.Windows.Forms.Label();
            this.lblCulture = new System.Windows.Forms.Label();
            this.lblTechnologies = new System.Windows.Forms.Label();
            this.lstTechnologies = new System.Windows.Forms.CheckedListBox();
            this.lblResources = new System.Windows.Forms.Label();
            this.lstResources = new System.Windows.Forms.CheckedListBox();
            this.lblImprovements = new System.Windows.Forms.Label();
            this.lstImprovements = new System.Windows.Forms.CheckedListBox();
            this.lblPollution = new System.Windows.Forms.Label();
            this.txtCost = new System.Windows.Forms.TextBox();
            this.txtPollution = new System.Windows.Forms.TextBox();
            this.txtMaintenance = new System.Windows.Forms.TextBox();
            this.txtCulture = new System.Windows.Forms.TextBox();
            this.lvwImprovement = new System.Windows.Forms.ListView();
            this.hdrName = new System.Windows.Forms.ColumnHeader();
            this.hdrCost = new System.Windows.Forms.ColumnHeader();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCost
            // 
            this.lblCost.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblCost, "lblCost");
            this.lblCost.Name = "lblCost";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            this.txtName.TextChanged += new System.EventHandler(this.ImprovementItemChanged);
            // 
            // lblName
            // 
            this.lblName.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // lblMaintenance
            // 
            this.lblMaintenance.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblMaintenance, "lblMaintenance");
            this.lblMaintenance.Name = "lblMaintenance";
            // 
            // lblCulture
            // 
            this.lblCulture.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblCulture, "lblCulture");
            this.lblCulture.Name = "lblCulture";
            // 
            // lblTechnologies
            // 
            this.lblTechnologies.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblTechnologies, "lblTechnologies");
            this.lblTechnologies.Name = "lblTechnologies";
            // 
            // lstTechnologies
            // 
            this.lstTechnologies.CheckOnClick = true;
            this.lstTechnologies.FormattingEnabled = true;
            resources.ApplyResources(this.lstTechnologies, "lstTechnologies");
            this.lstTechnologies.Name = "lstTechnologies";
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
            // lblImprovements
            // 
            this.lblImprovements.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblImprovements, "lblImprovements");
            this.lblImprovements.Name = "lblImprovements";
            // 
            // lstImprovements
            // 
            this.lstImprovements.CheckOnClick = true;
            this.lstImprovements.FormattingEnabled = true;
            resources.ApplyResources(this.lstImprovements, "lstImprovements");
            this.lstImprovements.Name = "lstImprovements";
            // 
            // lblPollution
            // 
            this.lblPollution.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblPollution, "lblPollution");
            this.lblPollution.Name = "lblPollution";
            // 
            // txtCost
            // 
            resources.ApplyResources(this.txtCost, "txtCost");
            this.txtCost.Name = "txtCost";
            // 
            // txtPollution
            // 
            resources.ApplyResources(this.txtPollution, "txtPollution");
            this.txtPollution.Name = "txtPollution";
            // 
            // txtMaintenance
            // 
            resources.ApplyResources(this.txtMaintenance, "txtMaintenance");
            this.txtMaintenance.Name = "txtMaintenance";
            // 
            // txtCulture
            // 
            resources.ApplyResources(this.txtCulture, "txtCulture");
            this.txtCulture.Name = "txtCulture";
            // 
            // lvwImprovement
            // 
            this.lvwImprovement.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrName,
            this.hdrCost});
            resources.ApplyResources(this.lvwImprovement, "lvwImprovement");
            this.lvwImprovement.FullRowSelect = true;
            this.lvwImprovement.Name = "lvwImprovement";
            this.lvwImprovement.View = System.Windows.Forms.View.Details;
            this.lvwImprovement.SelectedIndexChanged += new System.EventHandler(this.lvwImprovement_SelectedIndexChanged);
            // 
            // hdrName
            // 
            resources.ApplyResources(this.hdrName, "hdrName");
            // 
            // hdrCost
            // 
            resources.ApplyResources(this.hdrCost, "hdrCost");
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtPollution);
            this.panel1.Controls.Add(this.lblResources);
            this.panel1.Controls.Add(this.lstResources);
            this.panel1.Controls.Add(this.txtMaintenance);
            this.panel1.Controls.Add(this.lblCost);
            this.panel1.Controls.Add(this.lblImprovements);
            this.panel1.Controls.Add(this.lblMaintenance);
            this.panel1.Controls.Add(this.txtCulture);
            this.panel1.Controls.Add(this.lstImprovements);
            this.panel1.Controls.Add(this.lblCulture);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.lblPollution);
            this.panel1.Controls.Add(this.lblTechnologies);
            this.panel1.Controls.Add(this.txtCost);
            this.panel1.Controls.Add(this.lstTechnologies);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // ImprovementControl
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lvwImprovement);
            resources.ApplyResources(this, "$this");
            this.Name = "ImprovementControl";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

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

		public override void UndoChanges()
		{
			Validate();
			this.currencyManager.CancelCurrentEdit();
		}


		private void DataBind()
		{
			DataSet ds = EditorApp.Instance.RuleSetData;
			this.txtCost.DataBindings.Add("Text", ds, "Improvement.BuildCost");
			this.txtCulture.DataBindings.Add("Text", ds, "Improvement.Culture");
			this.txtMaintenance.DataBindings.Add("Text", ds, "Improvement.MaintenanceCost");
			this.txtName.DataBindings.Add("Text", ds, "Improvement.Name");
			this.txtPollution.DataBindings.Add("Text", ds, "Improvement.Pollution");
			this.lstImprovements.DataSource = ds.Tables["Improvement"];
			this.lstImprovements.DisplayMember = "Name";
			this.lstImprovements.ValueMember = "ImprovementID";
			this.lstResources.DataSource = ds.Tables["Resource"];
			this.lstResources.DisplayMember = "Name";
			this.lstResources.ValueMember = "ResourceID";
			this.lstTechnologies.DataSource = ds.Tables["Technology"];
			this.lstTechnologies.DisplayMember = "Name";
			this.lstTechnologies.ValueMember = "TechnologyID";
			this.currencyManager = (CurrencyManager)this.BindingContext[ds, "Improvement"];
			RefreshTechnologies();
			RefreshResources();
			RefreshImprovements();
		}


		private void RefreshTechnologies()
		{
			for(int i = 0; i <= lstTechnologies.Items.Count - 1; i++)
				lstTechnologies.SetItemChecked(i, false);
			
			if(this.currencyManager == null)
				return;

			if(this.currencyManager.Position == -1)
				return;

			DataRow improvement = ((DataRowView)this.currencyManager.Current).Row;

			DataRow[] techs = improvement.GetChildRows("Improvement_ImprovementTechnologies");

			if(techs == null)
				return;

			int idx;
			foreach(DataRow row in techs)
			{
				idx = lstTechnologies.Items.IndexOf(row);
				lstTechnologies.SetItemChecked(idx, true);
			}
		}

		private void RefreshResources()
		{
		}

		private void RefreshImprovements()
		{
		}

		private void FillListView()
		{
			DataTable table = EditorApp.Instance.RuleSetData.Tables["Improvement"];

			ListViewItem item;

			lvwImprovement.Items.Clear();

			foreach(DataRow row in table.Rows)
			{
				item = lvwImprovement.Items.Add(row["Name"].ToString());
				item.SubItems.Add(row["BuildCost"].ToString());
				item.Tag = row;
			}
		}


		private void ImprovementItemChanged(object sender, System.EventArgs e)
		{
			this.FormState = FormState.Edit;
		}

		private void lvwImprovement_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataSet ds = EditorApp.Instance.RuleSetData;
	
			if(this.lvwImprovement.SelectedItems.Count == 1)
			{
				DataView dv = (DataView)this.currencyManager.List;
				dv.Sort = "ImprovementID";
				DataRow row = (DataRow)lvwImprovement.SelectedItems[0].Tag;
				int index = dv.Find(row["ImprovementID"]);
				this.currencyManager.Position = index;
			}
			else
			{
				this.currencyManager.Position = -1;
			}
		}

	}
}
