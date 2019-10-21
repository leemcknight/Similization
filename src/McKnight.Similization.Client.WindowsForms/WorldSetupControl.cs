using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// Class responsible for getting parameters about the world to play on.
	/// </summary>
	public class WorldSetupControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ComboBox cboWater;
		private System.Windows.Forms.Label lblWater;
		private System.Windows.Forms.ComboBox cboClimate;
		private System.Windows.Forms.Label lblClimate;
		private System.Windows.Forms.Label lblBarbarians;
		private System.Windows.Forms.ComboBox cboAge;
		private System.Windows.Forms.Label lblAge;
		private System.Windows.Forms.ComboBox cboBarbarians;
		private System.Windows.Forms.ComboBox cboTemperature;
		private System.Windows.Forms.Label lblTemperature;
		private System.Windows.Forms.ComboBox cboWorldSize;
		private System.Windows.Forms.Label lblWorldSize;
		private System.Windows.Forms.Label lblLandMass;
		private System.Windows.Forms.ComboBox cboLandMass;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="WorldSetupControl"/> class.
		/// </summary>
		public WorldSetupControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.cboAge.SelectedIndex = 0;
			this.cboBarbarians.SelectedIndex = 0;
			this.cboClimate.SelectedIndex = 0;
			this.cboLandMass.SelectedIndex = 0;
			this.cboTemperature.SelectedIndex = 0;
			this.cboWater.SelectedIndex = 0;
			this.cboWorldSize.SelectedIndex = 0;
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

		/// <summary>
		/// The Size of the map the player chose.
		/// </summary>
		public WorldSize WorldSize
		{
			get 
			{ 
				WorldSize worldSize = WorldSize.Standard;
				switch(cboWorldSize.Text)
				{
					case "Tiny":
						worldSize = WorldSize.Tiny;
						break;
					case "Small":
						worldSize = WorldSize.Small;
						break;
					case "Standard":
						worldSize = WorldSize.Standard;
						break;
					case "Large":
						worldSize = WorldSize.Large;
						break;
					case "Huge":
						worldSize = WorldSize.Huge;
						break;
				}

				return worldSize;
			}
		}

		/// <summary>
		/// The age of the world the player has chosen.
		/// </summary>
		public Age Age
		{
			get
			{
				Age age = Age.FourBillion;
				switch(cboAge.Text)
				{
					case "3 Billion":
						age = Age.ThreeBillion;
						break;
					case "4 Billion":
						age = Age.FourBillion;
						break;
					case "5 Billion":
						age = Age.FiveBillion;
						break;
				}
				return age;
			}
		}

		/// <summary>
		/// The Climate of the world the player has chosen.
		/// </summary>
		public Climate Climate
		{
			get
			{
				Climate climate = Climate.Normal;
				switch(cboClimate.Text)
				{
					case "Arid":
						climate = Climate.Arid;
						break;
					case "Normal":
						climate = Climate.Normal;
						break;
					case "Wet":
						climate = Climate.Wet;
						break;
				}
				return climate;
			}
		}

		/// <summary>
		/// The Size of the continents on the world.
		/// </summary>
		public Landmass Landmass
		{
			get
			{
				Landmass landmass = Landmass.Continents;
				switch(cboLandMass.Text)
				{
					case "Pangaea":
						landmass = Landmass.Pangaea;
						break;
					case "Continents":
						landmass = Landmass.Continents;
						break;
					case "Archipelago":
						landmass = Landmass.Archipelago;
						break;
				}
				return landmass;
			}
		}

		/// <summary>
		/// The temperature of the world.
		/// </summary>
		public Temperature Temperature
		{
			get
			{
				Temperature temperature = Temperature.Temperate;
				switch(cboTemperature.Text)
				{
					case "Warm":
						temperature = Temperature.Warm;
						break;
					case "Temperate":
						temperature = Temperature.Temperate;
						break;
					case "Cool":
						temperature = Temperature.Cool;
						break;
				}
				return temperature;
			}
		}

		/// <summary>
		/// The amount of water on the world.
		/// </summary>
		public WaterCoverage WaterCoverage
		{
			get
			{
				WaterCoverage waterCoverage = WaterCoverage.SeventyPercent;
				switch(cboWater.Text)
				{
					case "Sixty Percent":
						waterCoverage = WaterCoverage.SixtyPercent;
						break;
					case "Seventy Percent":
						waterCoverage = WaterCoverage.SeventyPercent;
						break;
					case "Eighty Percent":
						waterCoverage = WaterCoverage.EightyPercent;
						break;
				}
				return waterCoverage;
			}
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorldSetupControl));
            this.cboWater = new System.Windows.Forms.ComboBox();
            this.lblWater = new System.Windows.Forms.Label();
            this.cboClimate = new System.Windows.Forms.ComboBox();
            this.lblClimate = new System.Windows.Forms.Label();
            this.lblBarbarians = new System.Windows.Forms.Label();
            this.cboAge = new System.Windows.Forms.ComboBox();
            this.lblAge = new System.Windows.Forms.Label();
            this.cboBarbarians = new System.Windows.Forms.ComboBox();
            this.cboTemperature = new System.Windows.Forms.ComboBox();
            this.lblTemperature = new System.Windows.Forms.Label();
            this.cboWorldSize = new System.Windows.Forms.ComboBox();
            this.lblWorldSize = new System.Windows.Forms.Label();
            this.lblLandMass = new System.Windows.Forms.Label();
            this.cboLandMass = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cboWater
            // 
            this.cboWater.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboWater, "cboWater");
            this.cboWater.FormattingEnabled = true;
            this.cboWater.Items.AddRange(new object[] {
            resources.GetString("cboWater.Items"),
            resources.GetString("cboWater.Items1"),
            resources.GetString("cboWater.Items2")});
            this.cboWater.Name = "cboWater";
            // 
            // lblWater
            // 
            resources.ApplyResources(this.lblWater, "lblWater");
            this.lblWater.Name = "lblWater";
            // 
            // cboClimate
            // 
            this.cboClimate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboClimate, "cboClimate");
            this.cboClimate.FormattingEnabled = true;
            this.cboClimate.Items.AddRange(new object[] {
            resources.GetString("cboClimate.Items"),
            resources.GetString("cboClimate.Items1"),
            resources.GetString("cboClimate.Items2")});
            this.cboClimate.Name = "cboClimate";
            // 
            // lblClimate
            // 
            resources.ApplyResources(this.lblClimate, "lblClimate");
            this.lblClimate.Name = "lblClimate";
            // 
            // lblBarbarians
            // 
            resources.ApplyResources(this.lblBarbarians, "lblBarbarians");
            this.lblBarbarians.Name = "lblBarbarians";
            // 
            // cboAge
            // 
            this.cboAge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboAge, "cboAge");
            this.cboAge.FormattingEnabled = true;
            this.cboAge.Items.AddRange(new object[] {
            resources.GetString("cboAge.Items"),
            resources.GetString("cboAge.Items1"),
            resources.GetString("cboAge.Items2")});
            this.cboAge.Name = "cboAge";
            // 
            // lblAge
            // 
            resources.ApplyResources(this.lblAge, "lblAge");
            this.lblAge.Name = "lblAge";
            // 
            // cboBarbarians
            // 
            this.cboBarbarians.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboBarbarians, "cboBarbarians");
            this.cboBarbarians.FormattingEnabled = true;
            this.cboBarbarians.Items.AddRange(new object[] {
            resources.GetString("cboBarbarians.Items"),
            resources.GetString("cboBarbarians.Items1"),
            resources.GetString("cboBarbarians.Items2"),
            resources.GetString("cboBarbarians.Items3"),
            resources.GetString("cboBarbarians.Items4")});
            this.cboBarbarians.Name = "cboBarbarians";
            // 
            // cboTemperature
            // 
            this.cboTemperature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboTemperature, "cboTemperature");
            this.cboTemperature.FormattingEnabled = true;
            this.cboTemperature.Items.AddRange(new object[] {
            resources.GetString("cboTemperature.Items"),
            resources.GetString("cboTemperature.Items1"),
            resources.GetString("cboTemperature.Items2")});
            this.cboTemperature.Name = "cboTemperature";
            // 
            // lblTemperature
            // 
            resources.ApplyResources(this.lblTemperature, "lblTemperature");
            this.lblTemperature.Name = "lblTemperature";
            // 
            // cboWorldSize
            // 
            this.cboWorldSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboWorldSize, "cboWorldSize");
            this.cboWorldSize.FormattingEnabled = true;
            this.cboWorldSize.Items.AddRange(new object[] {
            resources.GetString("cboWorldSize.Items"),
            resources.GetString("cboWorldSize.Items1"),
            resources.GetString("cboWorldSize.Items2"),
            resources.GetString("cboWorldSize.Items3"),
            resources.GetString("cboWorldSize.Items4")});
            this.cboWorldSize.Name = "cboWorldSize";
            // 
            // lblWorldSize
            // 
            resources.ApplyResources(this.lblWorldSize, "lblWorldSize");
            this.lblWorldSize.Name = "lblWorldSize";
            // 
            // lblLandMass
            // 
            resources.ApplyResources(this.lblLandMass, "lblLandMass");
            this.lblLandMass.Name = "lblLandMass";
            // 
            // cboLandMass
            // 
            this.cboLandMass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboLandMass, "cboLandMass");
            this.cboLandMass.FormattingEnabled = true;
            this.cboLandMass.Items.AddRange(new object[] {
            resources.GetString("cboLandMass.Items"),
            resources.GetString("cboLandMass.Items1"),
            resources.GetString("cboLandMass.Items2")});
            this.cboLandMass.Name = "cboLandMass";
            // 
            // WorldSetupControl
            // 
            this.Controls.Add(this.cboLandMass);
            this.Controls.Add(this.lblLandMass);
            this.Controls.Add(this.cboWater);
            this.Controls.Add(this.lblWater);
            this.Controls.Add(this.cboClimate);
            this.Controls.Add(this.lblClimate);
            this.Controls.Add(this.lblBarbarians);
            this.Controls.Add(this.cboAge);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.cboBarbarians);
            this.Controls.Add(this.cboTemperature);
            this.Controls.Add(this.lblTemperature);
            this.Controls.Add(this.cboWorldSize);
            this.Controls.Add(this.lblWorldSize);
            resources.ApplyResources(this, "$this");
            this.Name = "WorldSetupControl";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
	}
}
