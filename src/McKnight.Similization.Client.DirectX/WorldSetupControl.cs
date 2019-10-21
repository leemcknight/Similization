using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using LJM.Similization.Core;
using LJM.Similization.Client;
using LJM.Similization.Client.DirectX.Sound;
using LJM.Similization.Client.DirectX.Controls;
using LJM.Similization.Server;

namespace LJM.Similization.Client.DirectX
{
    /// <summary>
    /// DirectX Client implementation of the <see cref="IWorldSetupControl"/> interface.
    /// </summary>
	public class WorldSetupControl : DXWindow, IWorldSetupControl
	{
		private DXLabel lblWorldSize;
		private DXLabel lblBarbarians;
		private DXLabel lblClimate;
		private DXLabel lblTemperature;
		private DXLabel lblAge;
		private DXLabel lblWaterCoverage;
        private DXLabel lblLandMass;
		private DXComboBox cboWorldSize;
		private DXComboBox cboBarbarians;
		private DXComboBox cboTemperature;
		private DXComboBox cboAge;
		private DXComboBox cboClimate;
		private DXComboBox cboWaterCoverage;
        private DXComboBox cboLandMass;
		private System.Drawing.Font LabelFont = new System.Drawing.Font("Veranda",9.25F);
		private Color LabelColor = Color.White;
		private SoundEffect transitionSound;
		

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldSetupControl"/> class.
        /// </summary>
		public WorldSetupControl(IDirectXControlHost controlHost) : base(controlHost)
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
            this.Text = DirectXClientResources.WorldSetupTitle;
			this.Location = new Point(0,0);
			
			this.BackColor = Color.DarkGoldenrod;
            this.BackColor2 = Color.LightGoldenrodYellow;
			
			//this.transitionSound = new SoundEffect(@"Sound\select.wav", GraphicsMain.Instance.Owner);
			this.lblClimate = new DXLabel(this.ControlHost, this);
			this.lblTemperature = new DXLabel(this.ControlHost, this);
			this.lblAge = new DXLabel(this.ControlHost, this);
            this.lblLandMass = new DXLabel(this.ControlHost, this);
			this.cboBarbarians = new DXComboBox(this.ControlHost, this);
			this.cboWorldSize = new DXComboBox(this.ControlHost, this);
			this.cboClimate = new DXComboBox(this.ControlHost, this);
			this.cboTemperature = new DXComboBox(this.ControlHost, this);
			this.cboAge = new DXComboBox(this.ControlHost, this);
			this.lblBarbarians = new DXLabel(this.ControlHost, this);
			this.lblWorldSize = new DXLabel(this.ControlHost, this);
			this.lblWaterCoverage = new DXLabel(this.ControlHost, this);
			this.cboWaterCoverage = new DXComboBox(this.ControlHost, this);
            this.cboLandMass = new DXComboBox(this.ControlHost, this);

            //lblWorldSize
            this.lblWorldSize.Text = DirectXClientResources.WorldSizeLabel;
			this.lblWorldSize.Location = new Point(10, 140);
			this.lblWorldSize.Font = LabelFont;			
			this.lblWorldSize.ForeColor = LabelColor;
            this.lblWorldSize.AutoSize = true;
			this.Controls.Add(this.lblWorldSize);

            //cboWorldSize
			this.cboWorldSize.Location = new Point(180,140);
            this.cboWorldSize.Size = new Size(160, 20);
            this.cboWorldSize.DataSource = CreateWorldSizeStringList();
			this.Controls.Add(this.cboWorldSize);

            //lblLandMass
            this.lblLandMass.Location = new Point(10, 320);
            this.lblLandMass.Text = DirectXClientResources.LandMassLabel;
            this.lblLandMass.Font = LabelFont;
            this.lblLandMass.ForeColor = LabelColor;
            this.lblLandMass.AutoSize = true;
            this.Controls.Add(this.lblLandMass);

            //cboLandMass
            this.cboLandMass.Location = new Point(180, 320);
            this.cboLandMass.Size = new Size(160, 20);
            this.cboLandMass.DataSource = CreateLandMassList();
            this.Controls.Add(this.cboLandMass);

            //lblBarbarians
            this.lblBarbarians.Text = DirectXClientResources.BarbarianLabel;
			this.lblBarbarians.Location = new Point(10, 170);
			this.lblBarbarians.Font = LabelFont;			
			this.lblBarbarians.ForeColor = LabelColor;
            this.lblBarbarians.AutoSize = true;
			this.Controls.Add(this.lblBarbarians);

            //cboBarbarians
			this.cboBarbarians.Location = new Point(180, 170);
            this.cboBarbarians.Size = new Size(160, 20);
            this.cboBarbarians.DataSource = CreateBarbarianList();
			this.Controls.Add(this.cboBarbarians);

            //lblClimate
            this.lblClimate.Text = DirectXClientResources.ClimateLabel;
			this.lblClimate.Location = new Point(10, 200);
			this.lblClimate.Font = LabelFont;						
			this.lblClimate.ForeColor = LabelColor;
            this.lblClimate.AutoSize = true;
			this.Controls.Add(this.lblClimate);

            //cboClimate
			this.cboClimate.Location = new Point(180, 200);
            this.cboClimate.DataSource = CreateClimateList();
            this.cboClimate.Size = new Size(160, 20);
			this.Controls.Add(this.cboClimate);

            //lblTemperature
            this.lblTemperature.Text = DirectXClientResources.TemperatureLabel;
			this.lblTemperature.Location = new Point(10, 230);
			this.lblTemperature.Font = LabelFont;						
			this.lblTemperature.ForeColor = LabelColor;
            this.lblTemperature.AutoSize = true;
			this.Controls.Add(this.lblTemperature);

            //cboTemperature
			this.cboTemperature.Location = new Point(180, 230);
            this.cboTemperature.Size = new Size(160, 20);
            this.cboTemperature.DataSource = CreateTemperatureList();
			this.Controls.Add(this.cboTemperature);

            //lblAge
            this.lblAge.Text = DirectXClientResources.AgeLabel;
			this.lblAge.Location = new Point(10, 260);
			this.lblAge.Font = LabelFont;			
			this.lblAge.ForeColor = LabelColor;
            this.lblAge.AutoSize = true;
			this.Controls.Add(this.lblAge);

            //cboAge
			this.cboAge.Location = new Point(180, 260);
            this.cboAge.Size = new Size(160, 20);
            this.cboAge.DataSource = CreateAgeList();
			this.Controls.Add(this.cboAge);

            //lblWaterCoverage
            this.lblWaterCoverage.Text = DirectXClientResources.WaterCoverageLabel;
			this.lblWaterCoverage.Location = new Point(10,290);
			this.lblWaterCoverage.Font = LabelFont;						
			this.lblWaterCoverage.ForeColor = LabelColor;
            this.lblWaterCoverage.AutoSize = true;
			this.Controls.Add(this.lblWaterCoverage);

            //cboWaterCoverage
			this.cboWaterCoverage.Location = new Point(180, 290);
            this.cboWaterCoverage.Size = new Size(160, 20);
            this.cboWaterCoverage.DataSource = CreateWaterCoverageList();
			this.Controls.Add(this.cboWaterCoverage);
			
		}

        private object CreateLandMassList()
        {            
            return new string[] {
                DirectXClientResources.LandMassArchipelago,
                DirectXClientResources.LandMassContinents,
                DirectXClientResources.LandMassPangaea
            };
        }

        private string[] CreateWaterCoverageList()
        {
            return new string[]{
                DirectXClientResources.WaterCoverageSixtyPercent,
                DirectXClientResources.WaterCoverageSeventyPercent,
                DirectXClientResources.WaterCoverageEightyPercent
            };
        }

        private string[] CreateBarbarianList()
        {
            return new string[] {
                DirectXClientResources.BarbarianRandom,
                DirectXClientResources.BarbarianSedentary,
                DirectXClientResources.BarbarianRoaming,
                DirectXClientResources.BarbarianRestless,
                DirectXClientResources.BarbarianRaging
            };
        }

        private string[] CreateAgeList()
        {
            return new string[] {
                DirectXClientResources.Age3Billion,
                DirectXClientResources.Age4Billion,
                DirectXClientResources.Age5Billion
            };
        }

        private string[] CreateClimateList()
        {
            return new string[] {
                DirectXClientResources.ClimateArid,
                DirectXClientResources.ClimateNormal,
                DirectXClientResources.ClimateWet
            };
        }

        private string[] CreateTemperatureList()
        {
            return new string[] {
                DirectXClientResources.TemperatureCool,
                DirectXClientResources.TemperatureTemperate,
                DirectXClientResources.TemperatureWarm
            };
        }

        private string[] CreateWorldSizeStringList()
        {
            return new string[] {
                DirectXClientResources.WorldSizeTiny,
                DirectXClientResources.WorldSizeSmall,
                DirectXClientResources.WorldSizeStandard,
                DirectXClientResources.WorldSizeLarge,
                DirectXClientResources.WorldSizeHuge
            };
        }

        /// <summary>
        /// A <see cref="LJM.Similization.Core.WorldSize"/> representing the size of the world to use.
        /// </summary>
		public WorldSize WorldSize
		{
			get 
			{				
                string text = this.cboWorldSize.Text;
                WorldSize size = WorldSize.Standard;

                if (text == DirectXClientResources.WorldSizeHuge)
                    size = WorldSize.Huge;
                else if (text == DirectXClientResources.WorldSizeLarge)
                    size = WorldSize.Large;
                else if (text == DirectXClientResources.WorldSizeSmall)
                    size = WorldSize.Small;
                else if (text == DirectXClientResources.WorldSizeStandard)
                    size = WorldSize.Standard;
                else if (text == DirectXClientResources.WorldSizeTiny)
                    size = WorldSize.Tiny;
                return size;          
			}
		}

        /// <summary>
        /// A <see cref="LJM.Similzation.Core.Landmass"/> representing the size of the land masses 
        /// on the planet.
        /// </summary>
        public Landmass Landmass
        {
            get
            {
                string text = this.cboLandMass.Text;
                Landmass landmass = Landmass.Continents;
                if (text == DirectXClientResources.LandMassPangaea)
                    landmass = Landmass.Pangaea;
                else if (text == DirectXClientResources.LandMassArchipelago)
                    landmass = Landmass.Archipelago;
                return landmass;
            }
        }

        /// <summary>
        /// A <see cref="LJM.Similization.Core.Temperature"/> representing the overall temperature of the world.
        /// </summary>
        public Temperature Temperature
        {
            get 
            { 
                string text = this.cboTemperature.Text;                
                Temperature temp = Temperature.Temperate;
                if (text == DirectXClientResources.TemperatureCool)
                    temp = Temperature.Cool;
                else if (text == DirectXClientResources.TemperatureWarm)
                    temp = Temperature.Warm;
                return temp; 
            }
        }

        /// <summary>
        /// A <see cref="LJM.Similization.Core.Age"/> representing the age of the planet.
        /// </summary>
        public Age Age
        {
            get
            {
                string text = this.cboAge.Text;
                Age age = Age.FourBillion;
                if (text == DirectXClientResources.Age3Billion)
                    age = Age.ThreeBillion;
                else if (text == DirectXClientResources.Age5Billion)
                    age = Age.FiveBillion;
                return age;
            }
        }

        /// <summary>
        /// A <see cref="LJM.Similization.Core.BarbarianAggressiveness"/> representing how 
        /// aggressive the barbarians will be during the game.
        /// </summary>
        public BarbarianAggressiveness BarbarianAggressiveness
		{
			get
			{
                BarbarianAggressiveness barbarians = BarbarianAggressiveness.Random;
                string text = this.cboBarbarians.Text;
                if (text == DirectXClientResources.BarbarianRaging)
                    barbarians = BarbarianAggressiveness.Raging;
                else if (text == DirectXClientResources.BarbarianRestless)
                    barbarians = BarbarianAggressiveness.Restless;
                else if (text == DirectXClientResources.BarbarianRoaming)
                    barbarians = BarbarianAggressiveness.Roaming;
                else if (text == DirectXClientResources.BarbarianSedentary)
                    barbarians = BarbarianAggressiveness.Sedentary;
                return barbarians;
			}

		}

        /// <summary>
        /// The <see cref="LJM.Similization.Core.Climate"/> the user has chosen to use in the world.
        /// </summary>
		public Climate Climate
		{
			get 
			{
				Climate climate = Climate.Normal;
                string text = this.cboClimate.Text;
                if (text == DirectXClientResources.ClimateArid)
                    climate = Climate.Arid;
                else if (text == DirectXClientResources.ClimateWet)
                    climate = Climate.Wet;
                return climate;				
			}
		}

        /// <summary>
        /// Indicates how much of the planet will be oceans.
        /// </summary>
        public WaterCoverage WaterCoverage
        {
            get
            {
                WaterCoverage coverage = WaterCoverage.SeventyPercent;
                string text = this.cboWaterCoverage.Text;
                if (text == DirectXClientResources.WaterCoverageEightyPercent)
                    coverage = WaterCoverage.EightyPercent;
                else if (text == DirectXClientResources.WaterCoverageSixtyPercent)
                    coverage = WaterCoverage.SixtyPercent;
                return coverage;
            }
        }
        
        /// <summary>
        /// Shows the control.
        /// </summary>
        public void ShowSimilizationControl()
        {
            this.Show();
        }     
    }
}
