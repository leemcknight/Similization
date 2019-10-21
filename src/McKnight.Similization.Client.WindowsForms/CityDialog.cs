using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using McKnight.Similization.Core;
using McKnight.Similization.Client;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// This is the form that shows city details.
	/// </summary>
	public class CityDialog : System.Windows.Forms.Form, ICityControl
	{
		private City city;
        private GridCell gridCell;
        private bool editable;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ComboBox cboImprovement;
		private System.Windows.Forms.Label lblImprovements;
		private System.Windows.Forms.ListBox lstImprovements;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblUnits;
		private System.Windows.Forms.ListBox lstUnits;
		private System.Windows.Forms.Label lblLuxuries;
		private System.Windows.Forms.ListBox lstLuxuries;
		private System.Windows.Forms.Label lblResources;
		private System.Windows.Forms.ListBox lstResources;
		private System.Windows.Forms.Button btnRush;
		private System.Windows.Forms.GroupBox grpFood;
		private System.Windows.Forms.GroupBox grpCommerce;
		private System.Windows.Forms.Label lblPollution;
		private System.Windows.Forms.Panel pnlPollution;
		private System.Windows.Forms.Label lblImprovement;
		private System.Windows.Forms.Label lblFood;
		private System.Windows.Forms.Label lblCityName;
		private System.Windows.Forms.Label lblTurnsToComplete;
		private System.Windows.Forms.Label lblGrowth;
		private System.Windows.Forms.Label lblProduced;
		private System.Windows.Forms.Label lblTaxes;
		private System.Windows.Forms.Label lblScience;
		private System.Windows.Forms.Label lblCorruption;
		private System.Windows.Forms.Label lblGoldPerTurn;
		private System.Windows.Forms.Label lblCulture;
		private System.Windows.Forms.Label lblCultureValue;
		private System.Windows.Forms.Label lblCultureRatio;
		private System.Windows.Forms.Label lblFounded;
		private System.Windows.Forms.Label lblFoundedValue;
		private System.Windows.Forms.Label lblGold;
		private System.Windows.Forms.Label lblGovernment;
		private System.Windows.Forms.Button btnNextCity;
		private System.Windows.Forms.Button btnPrevCity;
		private McKnight.Similization.Client.WindowsForms.CityViewControl ctlCity;
		private System.Windows.Forms.ContextMenu mnuUnit;
		private System.Windows.Forms.MenuItem itmDisband;
		private System.Windows.Forms.MenuItem itmUpgrade;
		private System.Windows.Forms.Label lblTurnsUntilGrowth;
		private System.Windows.Forms.Label lblFoodPerTurn;
		private System.Windows.Forms.Label lblFoodAvailable;
		private System.Windows.Forms.Label lblTaxesValue;
		private System.Windows.Forms.Label lblScienceValue;
		private System.Windows.Forms.Label lblCorruptionValue;
		private System.Windows.Forms.Label lblGoldPerTurnValue;
		private System.Windows.Forms.ContextMenu mnuImprovement;
		private System.Windows.Forms.MenuItem itmSell;
		private System.Windows.Forms.GroupBox grpProduction;
		private System.Windows.Forms.Label lblSplitter;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.Panel pnlCulture;
		private System.ComponentModel.IContainer components;

        /// <summary>
        /// Initializes a new instance of the <see cref="CityDialog"/> class.
        /// </summary>
        public CityDialog()
        {
            InitializeComponent();
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="CityDialog"/> class.
		/// </summary>
		/// <param name="city"></param>
		public CityDialog(City city) : this()
		{
			//DataBind the city information to the form.
            Grid grid = ClientApplication.Instance.ServerInstance.Grid;
            this.gridCell = grid.GetCell(city.Coordinates);
            this.city = city;
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

		private void DataBind()
		{			
			this.ctlCity.City = this.city;
			//single line databining statements.  We can bind straight to the city property.
			lblCityName.DataBindings.Add("Text", this.city, "Name");
			lblGoldPerTurnValue.DataBindings.Add("Text", this.city, "GoldPerTurn");
			lblFoodPerTurn.DataBindings.Add("Text", this.city, "FoodPerTurn");
			lblFoodAvailable.DataBindings.Add("Text", this.city, "AvailableFood");
			lblTurnsUntilGrowth.DataBindings.Add("Text", this.city, "TurnsUntilGrowth");
			lblGovernment.DataBindings.Add("Text", this.city, "ParentCountry.Government.Name");
			
			//complex-bound properties
			cboImprovement.DataSource = this.city.BuildableItems;
			cboImprovement.DisplayMember = "Name";
			cboImprovement.DataBindings.Add("SelectedItem", this.city, "NextImprovement");
			lstImprovements.DataSource = this.city.Improvements;
			lstUnits.DataSource = this.gridCell.Units;
			lstUnits.DisplayMember = "Name";

			//databindings that require associated format delegates
			Binding binding;

			//gold
			binding = new Binding("Text", this.city, "ParentCountry.Gold");
			binding.Format += new ConvertEventHandler(this.FormatGoldAmount);
			lblGold.DataBindings.Add(binding);

			//year founded
			binding = new Binding("Text", this.city, "YearFounded");
			binding.Format += new ConvertEventHandler(this.FormatYearFounded);
			lblFoundedValue.DataBindings.Add(binding);		

            //culture
            binding = new Binding("Text", this.city, "CulturePerTurn");
            binding.Format += new ConvertEventHandler(this.FormatCulturePerTurn);
            lblCultureValue.DataBindings.Add(binding);            

			//turns until complete.
			string format = ClientResources.GetString(StringKey.TurnsUntilComplete);
			format = string.Format(
                                CultureInfo.CurrentCulture,
								format, 
								this.city.TurnsUntilComplete.ToString(CultureInfo.CurrentCulture),
								this.city.Shields.ToString(CultureInfo.CurrentCulture),
								this.city.NextImprovement.Cost.ToString(CultureInfo.CurrentCulture),
								this.city.ShieldsPerTurn.ToString(CultureInfo.CurrentCulture));
			lblTurnsToComplete.Text = format;			
			lblCultureRatio.Text = this.city.CulturePoints.ToString(CultureInfo.CurrentCulture) + "/" + this.city.CultureThreshold.ToString(CultureInfo.CurrentCulture);
		}


		private void FormatGoldAmount(object sender, ConvertEventArgs e)
		{
			string format = ClientResources.GetString(StringKey.GoldAmount);
			int amount = Convert.ToInt32(e.Value, CultureInfo.CurrentCulture);
			format = string.Format(CultureInfo.CurrentCulture, format, amount.ToString(CultureInfo.CurrentCulture));
			e.Value = format;
		}

        private void FormatCulturePerTurn(object sender, ConvertEventArgs e)
        {
            string format = ClientResources.GetString("culturePerTurn");
            e.Value = string.Format(CultureInfo.CurrentCulture, format, e.Value.ToString());
        }

		private void FormatYearFounded(object sender, ConvertEventArgs e)
		{
			string format;
			int year = (int)e.Value;
			int dyear = Math.Abs(year);
			if(year >= 0)
				format =  ClientResources.GetString(StringKey.YearAD);
			else
				format = ClientResources.GetString(StringKey.YearBC);
			format = string.Format(CultureInfo.CurrentCulture, format, dyear.ToString(CultureInfo.CurrentCulture));
			e.Value = format;
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CityDialog));
            this.btnOK = new System.Windows.Forms.Button();
            this.lblImprovement = new System.Windows.Forms.Label();
            this.cboImprovement = new System.Windows.Forms.ComboBox();
            this.lblImprovements = new System.Windows.Forms.Label();
            this.lstImprovements = new System.Windows.Forms.ListBox();
            this.lblFood = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblUnits = new System.Windows.Forms.Label();
            this.lstUnits = new System.Windows.Forms.ListBox();
            this.mnuUnit = new System.Windows.Forms.ContextMenu();
            this.itmDisband = new System.Windows.Forms.MenuItem();
            this.itmUpgrade = new System.Windows.Forms.MenuItem();
            this.lblCityName = new System.Windows.Forms.Label();
            this.lblFounded = new System.Windows.Forms.Label();
            this.lblFoundedValue = new System.Windows.Forms.Label();
            this.lblGold = new System.Windows.Forms.Label();
            this.lblGovernment = new System.Windows.Forms.Label();
            this.lblTurnsToComplete = new System.Windows.Forms.Label();
            this.btnNextCity = new System.Windows.Forms.Button();
            this.btnPrevCity = new System.Windows.Forms.Button();
            this.lblLuxuries = new System.Windows.Forms.Label();
            this.lstLuxuries = new System.Windows.Forms.ListBox();
            this.lblResources = new System.Windows.Forms.Label();
            this.lstResources = new System.Windows.Forms.ListBox();
            this.btnRush = new System.Windows.Forms.Button();
            this.grpProduction = new System.Windows.Forms.GroupBox();
            this.grpFood = new System.Windows.Forms.GroupBox();
            this.lblTurnsUntilGrowth = new System.Windows.Forms.Label();
            this.lblGrowth = new System.Windows.Forms.Label();
            this.lblFoodPerTurn = new System.Windows.Forms.Label();
            this.lblProduced = new System.Windows.Forms.Label();
            this.lblFoodAvailable = new System.Windows.Forms.Label();
            this.grpCommerce = new System.Windows.Forms.GroupBox();
            this.lblTaxesValue = new System.Windows.Forms.Label();
            this.lblTaxes = new System.Windows.Forms.Label();
            this.lblScienceValue = new System.Windows.Forms.Label();
            this.lblScience = new System.Windows.Forms.Label();
            this.lblCorruptionValue = new System.Windows.Forms.Label();
            this.lblCorruption = new System.Windows.Forms.Label();
            this.lblGoldPerTurnValue = new System.Windows.Forms.Label();
            this.lblGoldPerTurn = new System.Windows.Forms.Label();
            this.ctlCity = new McKnight.Similization.Client.WindowsForms.CityViewControl();
            this.lblCulture = new System.Windows.Forms.Label();
            this.lblCultureValue = new System.Windows.Forms.Label();
            this.lblCultureRatio = new System.Windows.Forms.Label();
            this.lblPollution = new System.Windows.Forms.Label();
            this.pnlPollution = new System.Windows.Forms.Panel();
            this.mnuImprovement = new System.Windows.Forms.ContextMenu();
            this.itmSell = new System.Windows.Forms.MenuItem();
            this.pnlCulture = new System.Windows.Forms.Panel();
            this.lblSplitter = new System.Windows.Forms.Label();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.grpProduction.SuspendLayout();
            this.grpFood.SuspendLayout();
            this.grpCommerce.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this._okButton_Click);
            // 
            // lblImprovement
            // 
            resources.ApplyResources(this.lblImprovement, "lblImprovement");
            this.lblImprovement.Name = "lblImprovement";
            // 
            // cboImprovement
            // 
            this.cboImprovement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboImprovement.FormattingEnabled = true;
            resources.ApplyResources(this.cboImprovement, "cboImprovement");
            this.cboImprovement.Name = "cboImprovement";
            // 
            // lblImprovements
            // 
            resources.ApplyResources(this.lblImprovements, "lblImprovements");
            this.lblImprovements.Name = "lblImprovements";
            // 
            // lstImprovements
            // 
            this.lstImprovements.FormattingEnabled = true;
            resources.ApplyResources(this.lstImprovements, "lstImprovements");
            this.lstImprovements.Name = "lstImprovements";
            // 
            // lblFood
            // 
            resources.ApplyResources(this.lblFood, "lblFood");
            this.lblFood.Name = "lblFood";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            // 
            // lblUnits
            // 
            resources.ApplyResources(this.lblUnits, "lblUnits");
            this.lblUnits.Name = "lblUnits";
            // 
            // lstUnits
            // 
            this.lstUnits.ContextMenu = this.mnuUnit;
            this.lstUnits.FormattingEnabled = true;
            resources.ApplyResources(this.lstUnits, "lstUnits");
            this.lstUnits.Name = "lstUnits";
            this.lstUnits.SelectedIndexChanged += new System.EventHandler(this._unitListBox_SelectedIndexChanged);
            // 
            // mnuUnit
            // 
            this.mnuUnit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.itmDisband,
            this.itmUpgrade});
            // 
            // itmDisband
            // 
            this.itmDisband.Index = 0;
            resources.ApplyResources(this.itmDisband, "itmDisband");
            // 
            // itmUpgrade
            // 
            this.itmUpgrade.Index = 1;
            resources.ApplyResources(this.itmUpgrade, "itmUpgrade");
            this.itmUpgrade.Click += new System.EventHandler(this._upgradeMenuItem_Click);
            // 
            // lblCityName
            // 
            resources.ApplyResources(this.lblCityName, "lblCityName");
            this.lblCityName.Name = "lblCityName";
            // 
            // lblFounded
            // 
            resources.ApplyResources(this.lblFounded, "lblFounded");
            this.lblFounded.Name = "lblFounded";
            // 
            // lblFoundedValue
            // 
            resources.ApplyResources(this.lblFoundedValue, "lblFoundedValue");
            this.lblFoundedValue.Name = "lblFoundedValue";
            // 
            // lblGold
            // 
            resources.ApplyResources(this.lblGold, "lblGold");
            this.lblGold.Name = "lblGold";
            // 
            // lblGovernment
            // 
            resources.ApplyResources(this.lblGovernment, "lblGovernment");
            this.lblGovernment.Name = "lblGovernment";
            // 
            // lblTurnsToComplete
            // 
            this.lblTurnsToComplete.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblTurnsToComplete, "lblTurnsToComplete");
            this.lblTurnsToComplete.Name = "lblTurnsToComplete";
            // 
            // btnNextCity
            // 
            resources.ApplyResources(this.btnNextCity, "btnNextCity");
            this.btnNextCity.Name = "btnNextCity";
            this.btnNextCity.UseVisualStyleBackColor = false;
            // 
            // btnPrevCity
            // 
            resources.ApplyResources(this.btnPrevCity, "btnPrevCity");
            this.btnPrevCity.Name = "btnPrevCity";
            this.btnPrevCity.UseVisualStyleBackColor = false;
            // 
            // lblLuxuries
            // 
            resources.ApplyResources(this.lblLuxuries, "lblLuxuries");
            this.lblLuxuries.Name = "lblLuxuries";
            // 
            // lstLuxuries
            // 
            this.lstLuxuries.FormattingEnabled = true;
            resources.ApplyResources(this.lstLuxuries, "lstLuxuries");
            this.lstLuxuries.Name = "lstLuxuries";
            // 
            // lblResources
            // 
            resources.ApplyResources(this.lblResources, "lblResources");
            this.lblResources.Name = "lblResources";
            // 
            // lstResources
            // 
            this.lstResources.FormattingEnabled = true;
            resources.ApplyResources(this.lstResources, "lstResources");
            this.lstResources.Name = "lstResources";
            // 
            // btnRush
            // 
            this.btnRush.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.btnRush, "btnRush");
            this.btnRush.Name = "btnRush";
            // 
            // grpProduction
            // 
            this.grpProduction.Controls.Add(this.lblTurnsToComplete);
            this.grpProduction.Controls.Add(this.btnRush);
            this.grpProduction.Controls.Add(this.lblImprovement);
            this.grpProduction.Controls.Add(this.cboImprovement);
            this.grpProduction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpProduction.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.grpProduction, "grpProduction");
            this.grpProduction.Name = "grpProduction";
            this.grpProduction.TabStop = false;
            // 
            // grpFood
            // 
            this.grpFood.Controls.Add(this.lblTurnsUntilGrowth);
            this.grpFood.Controls.Add(this.lblGrowth);
            this.grpFood.Controls.Add(this.lblFoodPerTurn);
            this.grpFood.Controls.Add(this.lblProduced);
            this.grpFood.Controls.Add(this.lblFoodAvailable);
            this.grpFood.Controls.Add(this.lblFood);
            resources.ApplyResources(this.grpFood, "grpFood");
            this.grpFood.Name = "grpFood";
            this.grpFood.TabStop = false;
            // 
            // lblTurnsUntilGrowth
            // 
            resources.ApplyResources(this.lblTurnsUntilGrowth, "lblTurnsUntilGrowth");
            this.lblTurnsUntilGrowth.Name = "lblTurnsUntilGrowth";
            // 
            // lblGrowth
            // 
            resources.ApplyResources(this.lblGrowth, "lblGrowth");
            this.lblGrowth.Name = "lblGrowth";
            // 
            // lblFoodPerTurn
            // 
            resources.ApplyResources(this.lblFoodPerTurn, "lblFoodPerTurn");
            this.lblFoodPerTurn.Name = "lblFoodPerTurn";
            // 
            // lblProduced
            // 
            resources.ApplyResources(this.lblProduced, "lblProduced");
            this.lblProduced.Name = "lblProduced";
            // 
            // lblFoodAvailable
            // 
            resources.ApplyResources(this.lblFoodAvailable, "lblFoodAvailable");
            this.lblFoodAvailable.Name = "lblFoodAvailable";
            // 
            // grpCommerce
            // 
            this.grpCommerce.Controls.Add(this.lblTaxesValue);
            this.grpCommerce.Controls.Add(this.lblTaxes);
            this.grpCommerce.Controls.Add(this.lblScienceValue);
            this.grpCommerce.Controls.Add(this.lblScience);
            this.grpCommerce.Controls.Add(this.lblCorruptionValue);
            this.grpCommerce.Controls.Add(this.lblCorruption);
            this.grpCommerce.Controls.Add(this.lblGoldPerTurnValue);
            this.grpCommerce.Controls.Add(this.lblGoldPerTurn);
            resources.ApplyResources(this.grpCommerce, "grpCommerce");
            this.grpCommerce.Name = "grpCommerce";
            this.grpCommerce.TabStop = false;
            // 
            // lblTaxesValue
            // 
            resources.ApplyResources(this.lblTaxesValue, "lblTaxesValue");
            this.lblTaxesValue.Name = "lblTaxesValue";
            // 
            // lblTaxes
            // 
            resources.ApplyResources(this.lblTaxes, "lblTaxes");
            this.lblTaxes.Name = "lblTaxes";
            // 
            // lblScienceValue
            // 
            resources.ApplyResources(this.lblScienceValue, "lblScienceValue");
            this.lblScienceValue.Name = "lblScienceValue";
            // 
            // lblScience
            // 
            resources.ApplyResources(this.lblScience, "lblScience");
            this.lblScience.Name = "lblScience";
            // 
            // lblCorruptionValue
            // 
            resources.ApplyResources(this.lblCorruptionValue, "lblCorruptionValue");
            this.lblCorruptionValue.Name = "lblCorruptionValue";
            // 
            // lblCorruption
            // 
            resources.ApplyResources(this.lblCorruption, "lblCorruption");
            this.lblCorruption.Name = "lblCorruption";
            // 
            // lblGoldPerTurnValue
            // 
            resources.ApplyResources(this.lblGoldPerTurnValue, "lblGoldPerTurnValue");
            this.lblGoldPerTurnValue.Name = "lblGoldPerTurnValue";
            // 
            // lblGoldPerTurn
            // 
            resources.ApplyResources(this.lblGoldPerTurn, "lblGoldPerTurn");
            this.lblGoldPerTurn.Name = "lblGoldPerTurn";
            // 
            // ctlCity
            // 
            this.ctlCity.CenterCoordinates = Point.Empty;
            this.ctlCity.City = null;
            this.ctlCity.CityNameFont = null;
            this.ctlCity.CityNameFontColor = System.Drawing.Color.Empty;
            resources.ApplyResources(this.ctlCity, "ctlCity");
            this.ctlCity.Name = "ctlCity";
            // 
            // lblCulture
            // 
            resources.ApplyResources(this.lblCulture, "lblCulture");
            this.lblCulture.Name = "lblCulture";
            // 
            // lblCultureValue
            // 
            resources.ApplyResources(this.lblCultureValue, "lblCultureValue");
            this.lblCultureValue.Name = "lblCultureValue";
            // 
            // lblCultureRatio
            // 
            resources.ApplyResources(this.lblCultureRatio, "lblCultureRatio");
            this.lblCultureRatio.Name = "lblCultureRatio";
            // 
            // lblPollution
            // 
            resources.ApplyResources(this.lblPollution, "lblPollution");
            this.lblPollution.Name = "lblPollution";
            // 
            // pnlPollution
            // 
            resources.ApplyResources(this.pnlPollution, "pnlPollution");
            this.pnlPollution.Name = "pnlPollution";
            this.pnlPollution.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // mnuImprovement
            // 
            this.mnuImprovement.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.itmSell});
            // 
            // itmSell
            // 
            this.itmSell.Index = 0;
            resources.ApplyResources(this.itmSell, "itmSell");
            // 
            // pnlCulture
            // 
            resources.ApplyResources(this.pnlCulture, "pnlCulture");
            this.pnlCulture.Name = "pnlCulture";
            this.pnlCulture.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCulture_Paint);
            // 
            // lblSplitter
            // 
            this.lblSplitter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lblSplitter, "lblSplitter");
            this.lblSplitter.Name = "lblSplitter";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.Images.SetKeyName(0, "");
            // 
            // CityDialog
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lblSplitter);
            this.Controls.Add(this.pnlCulture);
            this.Controls.Add(this.ctlCity);
            this.Controls.Add(this.pnlPollution);
            this.Controls.Add(this.lblPollution);
            this.Controls.Add(this.lblCultureRatio);
            this.Controls.Add(this.lblCultureValue);
            this.Controls.Add(this.lblCulture);
            this.Controls.Add(this.grpCommerce);
            this.Controls.Add(this.grpFood);
            this.Controls.Add(this.grpProduction);
            this.Controls.Add(this.lstResources);
            this.Controls.Add(this.lblResources);
            this.Controls.Add(this.lstLuxuries);
            this.Controls.Add(this.lblLuxuries);
            this.Controls.Add(this.btnPrevCity);
            this.Controls.Add(this.btnNextCity);
            this.Controls.Add(this.lblGovernment);
            this.Controls.Add(this.lblGold);
            this.Controls.Add(this.lblFoundedValue);
            this.Controls.Add(this.lblFounded);
            this.Controls.Add(this.lblCityName);
            this.Controls.Add(this.lstUnits);
            this.Controls.Add(this.lblUnits);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lstImprovements);
            this.Controls.Add(this.lblImprovements);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CityDialog";
            this.ShowInTaskbar = false;
            this.grpProduction.ResumeLayout(false);
            this.grpFood.ResumeLayout(false);
            this.grpCommerce.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		
		private void _okButton_Click(object sender, System.EventArgs e)
		{
			if(this.city.NextImprovement != cboImprovement.SelectedItem)
			{
				this.city.NextImprovement = (BuildableItem)cboImprovement.SelectedItem;
			}
		}

		private void _unitListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Unit unit = lstUnits.SelectedItem as Unit;

			if(unit == null)
			{
				itmUpgrade.Enabled = false;
				itmDisband.Enabled = false;
				return;
			}
			else if(unit.Upgrade == null)
			{
				itmUpgrade.Enabled = true;
                itmDisband.Text = ClientResources.GetString("upgrade");
				itmDisband.Enabled = false;
			}
			
			itmDisband.Enabled = true;

			
			Unit upgrade = GetUpgradeableUnit(unit);

			if(upgrade != null)
			{
				itmUpgrade.Enabled = true;
                string format = ClientResources.GetString("upgradeTo");
                string text = string.Format(
                    CultureInfo.CurrentCulture,
                    format,
                    upgrade.Name);
                itmUpgrade.Text = text;
			}
			else
			{
				itmUpgrade.Enabled = true;
                itmDisband.Text = ClientResources.GetString("upgrade");
				itmDisband.Enabled = false;
			}
		}

		private void _upgradeMenuItem_Click(object sender, System.EventArgs e)
		{
			Unit unit = lstUnits.SelectedItem as Unit;

			this.city.UpgradeUnit(unit);
		}

		private Unit GetUpgradeableUnit(Unit unit)
		{
			bool upgradable = false;
			Unit upgrade = unit.Upgrade;

			while(this.city.BuildableItems.Contains(upgrade))
			{
				upgradable = true;
				if(this.city.BuildableItems.Contains(upgrade.Upgrade))
				{
					upgrade = upgrade.Upgrade;
				}
				else
				{
					break;
				}
			}

			if(upgradable)
			{
				return upgrade;
			}
			else
			{
				return null;
			}
		}

		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			int x = 0;

			for(int i = 0; i < this.city.Pollution; i++)
			{
				x = i * imageList.ImageSize.Width;
				g.DrawImage(imageList.Images[0],x,0);
			}
		}

		private void pnlCulture_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			int points = this.city.CulturePoints;
			int required = this.city.CultureThreshold;
			double percentage = (double)((double)points/(double)required);
			int width = (int)(percentage * (double)e.ClipRectangle.Width);
			g.FillRectangle(Brushes.Green, new Rectangle(0,0, width, e.ClipRectangle.Height));
		}
        
        /// <summary>
        /// Implementation of the <i>ICityControl.Editable</i> property.
        /// </summary>
        public bool Editable
        {
            get
            {
                return this.editable;
            }
            set
            {
                this.editable = value;
            }
        }

        /// <summary>
        /// Implementation of the <i>ICityControl.City</i> property.
        /// </summary>
        public City City
        {
            get { return this.city; }
            set 
            { 
                this.city = value;
                DataBind();
            }
        }
        
        /// <summary>
        /// Shows the control.
        /// </summary>
        public void ShowSimilizationControl()
        {
            this.ShowDialog();
        }
    }
}
