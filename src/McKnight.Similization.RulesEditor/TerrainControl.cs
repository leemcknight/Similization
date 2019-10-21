using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace McKnight.SimilizationRulesEditor
{
	/// <summary>
	/// Editor User Control responsible for editing Terrain information.
	/// </summary>
	public class TerrainControl : EditorUserControl
	{
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.CheckBox chkDry;
		private System.Windows.Forms.TextBox txtFood;
		private System.Windows.Forms.TextBox txtShields;
		private System.Windows.Forms.TextBox txtMovementCost;
		private System.Windows.Forms.TextBox txtCommerce;
		private System.Windows.Forms.TextBox txtDefense;
		private System.Windows.Forms.TextBox txtIrrigationBonus;
		private System.Windows.Forms.TextBox txtMiningBonus;
		private System.Windows.Forms.TextBox txtRoadBonus;
		private System.Windows.Forms.ListView lvwTerrain;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ColumnHeader hdrName;
		private System.Windows.Forms.ColumnHeader hdrFood;
		private System.Windows.Forms.ColumnHeader hdrShields;
		private System.Windows.Forms.ColumnHeader hdrCommerce;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblFood;
		private System.Windows.Forms.Label lblShields;
		private System.Windows.Forms.Label lblCommerce;
		private System.Windows.Forms.Label lblMovement;
		private System.Windows.Forms.Label lblDefense;
		private System.Windows.Forms.Label lblIrrigation;
		private System.Windows.Forms.Label lblMining;
		private System.Windows.Forms.Label lblRoad;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.GroupBox grpBonus;
		private System.Windows.Forms.GroupBox grpFoodShieldsCommerce;
		private System.Windows.Forms.GroupBox grpOther;
		private System.Windows.Forms.TextBox txtMinimumElevation;
		private System.Windows.Forms.TextBox txtMaximumElevation;
		private System.Windows.Forms.Label lblRange;
		private System.Windows.Forms.TextBox txtTemperatureLow;
		private System.Windows.Forms.Label lblTo;
		private System.Windows.Forms.TextBox txtTemperatureHigh;
		private System.Windows.Forms.Label lblElevation;
		private System.Windows.Forms.Label lblElevationTo;
		private System.Windows.Forms.CheckBox chkBorderRiver;
		private System.Windows.Forms.GroupBox grpRanges;
		private System.Windows.Forms.Label lblRainFall;
		private System.Windows.Forms.TextBox txtRainfallLow;
		private System.Windows.Forms.Label lblRainfallTo;
		private System.Windows.Forms.TextBox txtRainfallHigh;
		

		private readonly string DeleteConfirmMessage = "Are you sure you want to delete this terrain?";
		private CurrencyManager currencyManager;

		/// <summary>
		/// Initializes a new instance of the <c>TerrainControl</c> class.
		/// </summary>
		public TerrainControl() : base()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.BindingContext = new BindingContext();

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




		public override void AddNew()
		{
			this.currencyManager.AddNew();
			this.currencyManager.Position = this.currencyManager.Count - 1;
		}


		public override void Delete()
		{
			DialogResult dr = MessageBox.Show(DeleteConfirmMessage, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

			if(dr == DialogResult.Yes)
			{
				int index = this.currencyManager.Position;
				this.currencyManager.List.RemoveAt(this.currencyManager.Position);
				index--;
				this.currencyManager.Position = index;
			}
		}


		public override void UndoChanges()
		{
			this.currencyManager.CancelCurrentEdit();
		}


		private void BindData()
		{
			DataSet ds = EditorApp.Instance.RuleSetData;
			this.txtCommerce.DataBindings.Add("Text", ds, "Terrain.Commerce");
			this.txtDefense.DataBindings.Add("Text", ds, "Terrain.Defense");
			this.txtFood.DataBindings.Add("Text", ds, "Terrain.Food");
			this.txtIrrigationBonus.DataBindings.Add("Text", ds, "Terrain.IrrigationBonus");
			this.txtMiningBonus.DataBindings.Add("Text", ds, "Terrain.MiningBonus");
			this.txtMovementCost.DataBindings.Add("Text", ds, "Terrain.MoveCost");
			this.txtName.DataBindings.Add("Text", ds, "Terrain.Name");
			this.txtRoadBonus.DataBindings.Add("Text", ds, "Terrain.RoadBonus");
			this.txtShields.DataBindings.Add("Text", ds, "Terrain.Shields");
			this.txtMaximumElevation.DataBindings.Add("Text", ds, "Terrain.MaximumElevation");
			this.txtMinimumElevation.DataBindings.Add("Text", ds, "Terrain.MinimumElevation");
			this.txtTemperatureHigh.DataBindings.Add("Text", ds, "Terrain.MaximumTemperature");
			this.txtTemperatureLow.DataBindings.Add("Text", ds, "Terrain.MinimumTemperature");
			this.txtRainfallHigh.DataBindings.Add("Text", ds, "Terrain.MaximumRainfall");
			this.txtRainfallLow.DataBindings.Add("Text", ds, "Terrain.MinimumRainfall");
			Binding boolBinding = new Binding("Checked", ds, "Terrain.IsDry");
			boolBinding.Format += new ConvertEventHandler(this.FormatNullBoolean);
			this.chkDry.DataBindings.Add(boolBinding);
			boolBinding = new Binding("Checked", ds, "Terrain.MustBorderRiver");
			boolBinding.Format += new ConvertEventHandler(this.FormatNullBoolean);
			this.chkBorderRiver.DataBindings.Add(boolBinding);
			this.chkBorderRiver.DataBindings.Add("Enabled", ds, "Terrain.IsDry");
			this.currencyManager = (CurrencyManager)this.BindingContext[ds, "Terrain"];
		}



		private void FillListView()
		{
			DataTable table = EditorApp.Instance.RuleSetData.Tables["Terrain"];

			ListViewItem item;

			lvwTerrain.Items.Clear();

			foreach(DataRow row in table.Rows)
			{
				item = lvwTerrain.Items.Add(row["Name"].ToString());
				item.SubItems.AddRange( new string[] {
														 row["Food"].ToString(),
														 row["Shields"].ToString(),
														 row["Commerce"].ToString()
													 });
				item.Tag = row;
			}
		}


		private void FormatNullBoolean(object sender, ConvertEventArgs e)
		{
			if(e.Value == DBNull.Value)
			{
				e.Value = false;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TerrainControl));
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblFood = new System.Windows.Forms.Label();
            this.lblShields = new System.Windows.Forms.Label();
            this.lblCommerce = new System.Windows.Forms.Label();
            this.lblMovement = new System.Windows.Forms.Label();
            this.lblDefense = new System.Windows.Forms.Label();
            this.lblIrrigation = new System.Windows.Forms.Label();
            this.lblMining = new System.Windows.Forms.Label();
            this.lblRoad = new System.Windows.Forms.Label();
            this.chkDry = new System.Windows.Forms.CheckBox();
            this.txtFood = new System.Windows.Forms.TextBox();
            this.txtShields = new System.Windows.Forms.TextBox();
            this.txtMovementCost = new System.Windows.Forms.TextBox();
            this.txtCommerce = new System.Windows.Forms.TextBox();
            this.txtDefense = new System.Windows.Forms.TextBox();
            this.txtIrrigationBonus = new System.Windows.Forms.TextBox();
            this.txtMiningBonus = new System.Windows.Forms.TextBox();
            this.txtRoadBonus = new System.Windows.Forms.TextBox();
            this.lvwTerrain = new System.Windows.Forms.ListView();
            this.hdrName = new System.Windows.Forms.ColumnHeader();
            this.hdrFood = new System.Windows.Forms.ColumnHeader();
            this.hdrShields = new System.Windows.Forms.ColumnHeader();
            this.hdrCommerce = new System.Windows.Forms.ColumnHeader();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.grpRanges = new System.Windows.Forms.GroupBox();
            this.txtRainfallHigh = new System.Windows.Forms.TextBox();
            this.lblRainfallTo = new System.Windows.Forms.Label();
            this.txtRainfallLow = new System.Windows.Forms.TextBox();
            this.lblRainFall = new System.Windows.Forms.Label();
            this.lblElevationTo = new System.Windows.Forms.Label();
            this.lblElevation = new System.Windows.Forms.Label();
            this.txtMinimumElevation = new System.Windows.Forms.TextBox();
            this.txtMaximumElevation = new System.Windows.Forms.TextBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtTemperatureLow = new System.Windows.Forms.TextBox();
            this.txtTemperatureHigh = new System.Windows.Forms.TextBox();
            this.lblRange = new System.Windows.Forms.Label();
            this.grpOther = new System.Windows.Forms.GroupBox();
            this.chkBorderRiver = new System.Windows.Forms.CheckBox();
            this.grpFoodShieldsCommerce = new System.Windows.Forms.GroupBox();
            this.grpBonus = new System.Windows.Forms.GroupBox();
            this.pnlMain.SuspendLayout();
            this.grpRanges.SuspendLayout();
            this.grpOther.SuspendLayout();
            this.grpFoodShieldsCommerce.SuspendLayout();
            this.grpBonus.SuspendLayout();
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
            // lblMovement
            // 
            this.lblMovement.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblMovement, "lblMovement");
            this.lblMovement.Name = "lblMovement";
            // 
            // lblDefense
            // 
            this.lblDefense.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblDefense, "lblDefense");
            this.lblDefense.Name = "lblDefense";
            // 
            // lblIrrigation
            // 
            this.lblIrrigation.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblIrrigation, "lblIrrigation");
            this.lblIrrigation.Name = "lblIrrigation";
            // 
            // lblMining
            // 
            this.lblMining.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblMining, "lblMining");
            this.lblMining.Name = "lblMining";
            // 
            // lblRoad
            // 
            this.lblRoad.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblRoad, "lblRoad");
            this.lblRoad.Name = "lblRoad";
            // 
            // chkDry
            // 
            resources.ApplyResources(this.chkDry, "chkDry");
            this.chkDry.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkDry.Name = "chkDry";
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
            // txtMovementCost
            // 
            resources.ApplyResources(this.txtMovementCost, "txtMovementCost");
            this.txtMovementCost.Name = "txtMovementCost";
            // 
            // txtCommerce
            // 
            resources.ApplyResources(this.txtCommerce, "txtCommerce");
            this.txtCommerce.Name = "txtCommerce";
            // 
            // txtDefense
            // 
            resources.ApplyResources(this.txtDefense, "txtDefense");
            this.txtDefense.Name = "txtDefense";
            // 
            // txtIrrigationBonus
            // 
            resources.ApplyResources(this.txtIrrigationBonus, "txtIrrigationBonus");
            this.txtIrrigationBonus.Name = "txtIrrigationBonus";
            // 
            // txtMiningBonus
            // 
            resources.ApplyResources(this.txtMiningBonus, "txtMiningBonus");
            this.txtMiningBonus.Name = "txtMiningBonus";
            // 
            // txtRoadBonus
            // 
            resources.ApplyResources(this.txtRoadBonus, "txtRoadBonus");
            this.txtRoadBonus.Name = "txtRoadBonus";
            // 
            // lvwTerrain
            // 
            this.lvwTerrain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrName,
            this.hdrFood,
            this.hdrShields,
            this.hdrCommerce});
            resources.ApplyResources(this.lvwTerrain, "lvwTerrain");
            this.lvwTerrain.FullRowSelect = true;
            this.lvwTerrain.HideSelection = false;
            this.lvwTerrain.MultiSelect = false;
            this.lvwTerrain.Name = "lvwTerrain";
            this.lvwTerrain.View = System.Windows.Forms.View.Details;
            this.lvwTerrain.SelectedIndexChanged += new System.EventHandler(this.lvwTerrain_SelectedIndexChanged);
            // 
            // hdrName
            // 
            resources.ApplyResources(this.hdrName, "hdrName");
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
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grpRanges);
            this.pnlMain.Controls.Add(this.grpOther);
            this.pnlMain.Controls.Add(this.grpFoodShieldsCommerce);
            this.pnlMain.Controls.Add(this.grpBonus);
            this.pnlMain.Controls.Add(this.txtName);
            this.pnlMain.Controls.Add(this.lblName);
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.pnlMain.Name = "pnlMain";
            // 
            // grpRanges
            // 
            this.grpRanges.Controls.Add(this.txtRainfallHigh);
            this.grpRanges.Controls.Add(this.lblRainfallTo);
            this.grpRanges.Controls.Add(this.txtRainfallLow);
            this.grpRanges.Controls.Add(this.lblRainFall);
            this.grpRanges.Controls.Add(this.lblElevationTo);
            this.grpRanges.Controls.Add(this.lblElevation);
            this.grpRanges.Controls.Add(this.txtMinimumElevation);
            this.grpRanges.Controls.Add(this.txtMaximumElevation);
            this.grpRanges.Controls.Add(this.lblTo);
            this.grpRanges.Controls.Add(this.txtTemperatureLow);
            this.grpRanges.Controls.Add(this.txtTemperatureHigh);
            this.grpRanges.Controls.Add(this.lblRange);
            this.grpRanges.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.grpRanges, "grpRanges");
            this.grpRanges.Name = "grpRanges";
            this.grpRanges.TabStop = false;
            // 
            // txtRainfallHigh
            // 
            resources.ApplyResources(this.txtRainfallHigh, "txtRainfallHigh");
            this.txtRainfallHigh.Name = "txtRainfallHigh";
            // 
            // lblRainfallTo
            // 
            resources.ApplyResources(this.lblRainfallTo, "lblRainfallTo");
            this.lblRainfallTo.Name = "lblRainfallTo";
            // 
            // txtRainfallLow
            // 
            resources.ApplyResources(this.txtRainfallLow, "txtRainfallLow");
            this.txtRainfallLow.Name = "txtRainfallLow";
            // 
            // lblRainFall
            // 
            resources.ApplyResources(this.lblRainFall, "lblRainFall");
            this.lblRainFall.Name = "lblRainFall";
            // 
            // lblElevationTo
            // 
            resources.ApplyResources(this.lblElevationTo, "lblElevationTo");
            this.lblElevationTo.Name = "lblElevationTo";
            // 
            // lblElevation
            // 
            resources.ApplyResources(this.lblElevation, "lblElevation");
            this.lblElevation.Name = "lblElevation";
            // 
            // txtMinimumElevation
            // 
            resources.ApplyResources(this.txtMinimumElevation, "txtMinimumElevation");
            this.txtMinimumElevation.Name = "txtMinimumElevation";
            // 
            // txtMaximumElevation
            // 
            resources.ApplyResources(this.txtMaximumElevation, "txtMaximumElevation");
            this.txtMaximumElevation.Name = "txtMaximumElevation";
            // 
            // lblTo
            // 
            resources.ApplyResources(this.lblTo, "lblTo");
            this.lblTo.Name = "lblTo";
            // 
            // txtTemperatureLow
            // 
            resources.ApplyResources(this.txtTemperatureLow, "txtTemperatureLow");
            this.txtTemperatureLow.Name = "txtTemperatureLow";
            // 
            // txtTemperatureHigh
            // 
            resources.ApplyResources(this.txtTemperatureHigh, "txtTemperatureHigh");
            this.txtTemperatureHigh.Name = "txtTemperatureHigh";
            // 
            // lblRange
            // 
            resources.ApplyResources(this.lblRange, "lblRange");
            this.lblRange.Name = "lblRange";
            // 
            // grpOther
            // 
            this.grpOther.Controls.Add(this.chkBorderRiver);
            this.grpOther.Controls.Add(this.lblMovement);
            this.grpOther.Controls.Add(this.txtMovementCost);
            this.grpOther.Controls.Add(this.lblDefense);
            this.grpOther.Controls.Add(this.txtDefense);
            this.grpOther.Controls.Add(this.chkDry);
            this.grpOther.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.grpOther, "grpOther");
            this.grpOther.Name = "grpOther";
            this.grpOther.TabStop = false;
            // 
            // chkBorderRiver
            // 
            resources.ApplyResources(this.chkBorderRiver, "chkBorderRiver");
            this.chkBorderRiver.Name = "chkBorderRiver";
            // 
            // grpFoodShieldsCommerce
            // 
            this.grpFoodShieldsCommerce.Controls.Add(this.lblFood);
            this.grpFoodShieldsCommerce.Controls.Add(this.txtFood);
            this.grpFoodShieldsCommerce.Controls.Add(this.lblShields);
            this.grpFoodShieldsCommerce.Controls.Add(this.txtShields);
            this.grpFoodShieldsCommerce.Controls.Add(this.lblCommerce);
            this.grpFoodShieldsCommerce.Controls.Add(this.txtCommerce);
            this.grpFoodShieldsCommerce.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.grpFoodShieldsCommerce, "grpFoodShieldsCommerce");
            this.grpFoodShieldsCommerce.Name = "grpFoodShieldsCommerce";
            this.grpFoodShieldsCommerce.TabStop = false;
            // 
            // grpBonus
            // 
            this.grpBonus.Controls.Add(this.lblIrrigation);
            this.grpBonus.Controls.Add(this.txtIrrigationBonus);
            this.grpBonus.Controls.Add(this.lblMining);
            this.grpBonus.Controls.Add(this.txtMiningBonus);
            this.grpBonus.Controls.Add(this.lblRoad);
            this.grpBonus.Controls.Add(this.txtRoadBonus);
            this.grpBonus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.grpBonus, "grpBonus");
            this.grpBonus.Name = "grpBonus";
            this.grpBonus.TabStop = false;
            // 
            // TerrainControl
            // 
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lvwTerrain);
            this.Name = "TerrainControl";
            resources.ApplyResources(this, "$this");
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.grpRanges.ResumeLayout(false);
            this.grpRanges.PerformLayout();
            this.grpOther.ResumeLayout(false);
            this.grpOther.PerformLayout();
            this.grpFoodShieldsCommerce.ResumeLayout(false);
            this.grpFoodShieldsCommerce.PerformLayout();
            this.grpBonus.ResumeLayout(false);
            this.grpBonus.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion



		private void lvwTerrain_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataSet ds = EditorApp.Instance.RuleSetData;
	
			if(this.lvwTerrain.SelectedItems.Count == 1)
			{
				DataView dv = (DataView)this.currencyManager.List;
				dv.Sort = "TerrainID";
				DataRow row = (DataRow)lvwTerrain.SelectedItems[0].Tag;
				int index = dv.Find(row["TerrainID"]);
				this.currencyManager.Position = index;
			}
			else
			{
				this.currencyManager.Position = -1;
			}
		}

	}
}
