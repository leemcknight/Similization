using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace McKnight.SimilizationRulesEditor
{
	/// <summary>
	/// Summary description for ctlRuleset.
	/// </summary>
	public class RulesetControl : EditorUserControl
	{
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Label lblOwner;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.TextBox txtOwner;
		private System.Windows.Forms.Label lblClient;
		private System.Windows.Forms.ComboBox cboClient;
		private System.Windows.Forms.Label lblMapType;
		private System.Windows.Forms.ComboBox cboMapType;
		private System.ComponentModel.IContainer components;		
        private GroupBox grpMetaData;
        private GroupBox grpDefaultRules;
        private Label lblTownBinSize;
        private MaskedTextBox txtMetropolisBinSize;
        private Label lblMetropolisBinSize;
        private MaskedTextBox txtCityBinSize;
        private Label lblCityBinSize;
        private MaskedTextBox txtTownBinSize;
        private BindingSource bindingSource;
        private MaskedTextBox txtFoodPerCitizen;
        private Label lblFoodPerCitizen;
        private MaskedTextBox txtRoadMovementBonus;
        private Label lblRoadMovementBonus;
        private BindingSource defaultRulesSource;

		public RulesetControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.BindingContext = new BindingContext();
            DataSet ds = EditorApp.Instance.RuleSetData;
            this.bindingSource = new BindingSource(ds, "MetaData");
            this.defaultRulesSource = new BindingSource(ds, "DefaultRules");
            DataBind();
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
			DataSet ds = EditorApp.Instance.RuleSetData;
            this.bindingSource.AddNew();
            this.defaultRulesSource.AddNew();
		}

		public override void SaveData()
		{
            this.bindingSource.EndEdit();
            this.defaultRulesSource.EndEdit();
            base.SaveData();
		}

		public override void ShowData()
		{
            this.bindingSource.ResetBindings(false);
            this.defaultRulesSource.ResetBindings(false);
			this.AllowAdd = (this.bindingSource.Count == 0);
            this.bindingSource.MoveFirst();
            this.defaultRulesSource.MoveFirst();
		}

		public override void UndoChanges()
		{
            this.bindingSource.CancelEdit();
            this.defaultRulesSource.CancelEdit();
		}

		private void DataBind()
		{                     
			this.txtDescription.DataBindings.Add("Text", this.bindingSource, "Description");
            this.txtName.DataBindings.Add("Text", this.bindingSource, "Name");
            this.txtOwner.DataBindings.Add("Text", this.bindingSource, "Owner");
            this.txtMetropolisBinSize.DataBindings.Add("Text", this.defaultRulesSource, "MetropolisBinSize");
            this.txtTownBinSize.DataBindings.Add("Text", this.defaultRulesSource, "TownBinSize");
            this.txtCityBinSize.DataBindings.Add("Text", this.defaultRulesSource, "CityBinSize");
            this.txtFoodPerCitizen.DataBindings.Add("Text", this.defaultRulesSource, "FoodPerCitizen");
            this.txtRoadMovementBonus.DataBindings.Add("Text", this.defaultRulesSource, "RoadMovementBonus");
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RulesetControl));
            this.lblName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblOwner = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtOwner = new System.Windows.Forms.TextBox();
            this.lblClient = new System.Windows.Forms.Label();
            this.cboClient = new System.Windows.Forms.ComboBox();
            this.lblMapType = new System.Windows.Forms.Label();
            this.cboMapType = new System.Windows.Forms.ComboBox();
            this.grpMetaData = new System.Windows.Forms.GroupBox();
            this.grpDefaultRules = new System.Windows.Forms.GroupBox();
            this.txtTownBinSize = new System.Windows.Forms.MaskedTextBox();
            this.lblTownBinSize = new System.Windows.Forms.Label();
            this.txtMetropolisBinSize = new System.Windows.Forms.MaskedTextBox();
            this.lblMetropolisBinSize = new System.Windows.Forms.Label();
            this.txtCityBinSize = new System.Windows.Forms.MaskedTextBox();
            this.lblCityBinSize = new System.Windows.Forms.Label();
            this.lblFoodPerCitizen = new System.Windows.Forms.Label();
            this.txtFoodPerCitizen = new System.Windows.Forms.MaskedTextBox();
            this.lblRoadMovementBonus = new System.Windows.Forms.Label();
            this.txtRoadMovementBonus = new System.Windows.Forms.MaskedTextBox();
            this.grpMetaData.SuspendLayout();
            this.grpDefaultRules.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // lblDescription
            // 
            this.lblDescription.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // lblOwner
            // 
            this.lblOwner.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblOwner, "lblOwner");
            this.lblOwner.Name = "lblOwner";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // txtDescription
            // 
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Name = "txtDescription";
            // 
            // txtOwner
            // 
            resources.ApplyResources(this.txtOwner, "txtOwner");
            this.txtOwner.Name = "txtOwner";
            // 
            // lblClient
            // 
            this.lblClient.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblClient, "lblClient");
            this.lblClient.Name = "lblClient";
            // 
            // cboClient
            // 
            this.cboClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClient.FormattingEnabled = true;
            this.cboClient.Items.AddRange(new object[] {
            resources.GetString("cboClient.Items"),
            resources.GetString("cboClient.Items1")});
            resources.ApplyResources(this.cboClient, "cboClient");
            this.cboClient.Name = "cboClient";
            // 
            // lblMapType
            // 
            this.lblMapType.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblMapType, "lblMapType");
            this.lblMapType.Name = "lblMapType";
            // 
            // cboMapType
            // 
            this.cboMapType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMapType.FormattingEnabled = true;
            this.cboMapType.Items.AddRange(new object[] {
            resources.GetString("cboMapType.Items")});
            resources.ApplyResources(this.cboMapType, "cboMapType");
            this.cboMapType.Name = "cboMapType";
            // 
            // grpMetaData
            // 
            this.grpMetaData.Controls.Add(this.txtDescription);
            this.grpMetaData.Controls.Add(this.cboMapType);
            this.grpMetaData.Controls.Add(this.lblName);
            this.grpMetaData.Controls.Add(this.lblMapType);
            this.grpMetaData.Controls.Add(this.lblDescription);
            this.grpMetaData.Controls.Add(this.cboClient);
            this.grpMetaData.Controls.Add(this.lblOwner);
            this.grpMetaData.Controls.Add(this.lblClient);
            this.grpMetaData.Controls.Add(this.txtName);
            this.grpMetaData.Controls.Add(this.txtOwner);
            resources.ApplyResources(this.grpMetaData, "grpMetaData");
            this.grpMetaData.Name = "grpMetaData";
            this.grpMetaData.TabStop = false;
            // 
            // grpDefaultRules
            // 
            this.grpDefaultRules.Controls.Add(this.txtRoadMovementBonus);
            this.grpDefaultRules.Controls.Add(this.lblRoadMovementBonus);
            this.grpDefaultRules.Controls.Add(this.txtFoodPerCitizen);
            this.grpDefaultRules.Controls.Add(this.lblFoodPerCitizen);
            this.grpDefaultRules.Controls.Add(this.txtTownBinSize);
            this.grpDefaultRules.Controls.Add(this.lblTownBinSize);
            this.grpDefaultRules.Controls.Add(this.txtMetropolisBinSize);
            this.grpDefaultRules.Controls.Add(this.lblMetropolisBinSize);
            this.grpDefaultRules.Controls.Add(this.txtCityBinSize);
            this.grpDefaultRules.Controls.Add(this.lblCityBinSize);
            resources.ApplyResources(this.grpDefaultRules, "grpDefaultRules");
            this.grpDefaultRules.Name = "grpDefaultRules";
            this.grpDefaultRules.TabStop = false;
            // 
            // txtTownBinSize
            // 
            resources.ApplyResources(this.txtTownBinSize, "txtTownBinSize");
            this.txtTownBinSize.Name = "txtTownBinSize";
            this.txtTownBinSize.ValidatingType = typeof(int);
            // 
            // lblTownBinSize
            // 
            resources.ApplyResources(this.lblTownBinSize, "lblTownBinSize");
            this.lblTownBinSize.Name = "lblTownBinSize";
            // 
            // txtMetropolisBinSize
            // 
            resources.ApplyResources(this.txtMetropolisBinSize, "txtMetropolisBinSize");
            this.txtMetropolisBinSize.Name = "txtMetropolisBinSize";
            this.txtMetropolisBinSize.ValidatingType = typeof(int);
            // 
            // lblMetropolisBinSize
            // 
            resources.ApplyResources(this.lblMetropolisBinSize, "lblMetropolisBinSize");
            this.lblMetropolisBinSize.Name = "lblMetropolisBinSize";
            // 
            // txtCityBinSize
            // 
            resources.ApplyResources(this.txtCityBinSize, "txtCityBinSize");
            this.txtCityBinSize.Name = "txtCityBinSize";
            this.txtCityBinSize.ValidatingType = typeof(int);
            // 
            // lblCityBinSize
            // 
            resources.ApplyResources(this.lblCityBinSize, "lblCityBinSize");
            this.lblCityBinSize.Name = "lblCityBinSize";
            // 
            // lblFoodPerCitizen
            // 
            resources.ApplyResources(this.lblFoodPerCitizen, "lblFoodPerCitizen");
            this.lblFoodPerCitizen.Name = "lblFoodPerCitizen";
            // 
            // txtFoodPerCitizen
            // 
            resources.ApplyResources(this.txtFoodPerCitizen, "txtFoodPerCitizen");
            this.txtFoodPerCitizen.Name = "txtFoodPerCitizen";
            this.txtFoodPerCitizen.ValidatingType = typeof(int);
            // 
            // lblRoadMovementBonus
            // 
            resources.ApplyResources(this.lblRoadMovementBonus, "lblRoadMovementBonus");
            this.lblRoadMovementBonus.Name = "lblRoadMovementBonus";
            // 
            // txtRoadMovementBonus
            // 
            resources.ApplyResources(this.txtRoadMovementBonus, "txtRoadMovementBonus");
            this.txtRoadMovementBonus.Name = "txtRoadMovementBonus";
            this.txtRoadMovementBonus.ValidatingType = typeof(int);
            // 
            // RulesetControl
            // 
            this.Controls.Add(this.grpDefaultRules);
            this.Controls.Add(this.grpMetaData);
            this.Name = "RulesetControl";
            resources.ApplyResources(this, "$this");
            this.grpMetaData.ResumeLayout(false);
            this.grpMetaData.PerformLayout();
            this.grpDefaultRules.ResumeLayout(false);
            this.grpDefaultRules.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		
	}
}
