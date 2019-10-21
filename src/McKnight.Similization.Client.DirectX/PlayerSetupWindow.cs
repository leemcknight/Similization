using System;
using System.Windows.Forms;
using System.Collections;
using System.Collections.ObjectModel;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using LJM.Similization.Core;
using LJM.Similization.Server;
using LJM.Similization.Client.DirectX.Controls;
using System.Collections.Generic;

namespace LJM.Similization.Client.DirectX
{
	/// <summary>
	/// DirectX Window responsible for capturing information related to player setup.
	/// </summary>
	public class PlayerSetupWindow : DXWindow
	{
		private DXCheckBox chkAllowDomination;
		private DXCheckBox chkAllowDiplomaticVictory;
		private DXCheckBox chkAllowCulturalVictory;
		private DXCheckBox chkAllowSpaceVictory;
		private DXCheckBox chkAllowMilitaryVictory;
		private DXCheckBox chkAllowSpecificAbilities;
		private DXCheckBox chkStandardRules;
		private DXLabel lblRules;
		private DXLabel	lblCiv;
		private DXLabel lblDifficulty;
		private DXLabel lblLeaderName;
		private DXComboBox cboCivilization;
		private DXComboBox cboDifficulty;
		private DXTextBox txtLeaderName;
		private DXLabel lblOpponents;
		private List<DXCheckBox> civilizationCheckBoxes;
		private System.Drawing.Font LabelFont = new System.Drawing.Font("Veranda", 9.25F);
		private Color LabelColor = Color.White;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerSetupWindow"/> class.
        /// </summary>
		public PlayerSetupWindow(IDirectXControlHost controlHost) : base(controlHost)
		{
			InitializeComponent();
			FillOpponentsGroup();            
		}

        /// <summary>
        /// Gets a <see cref="LJM.Similization.Server.Rules"/> object representing the 
        /// rules that will be used during gameplay.
        /// </summary>
		public Rules Rules
		{
			get 
			{
				Rules rules = new Rules();
				rules.AllowDominationVictory = this.chkAllowDomination.Checked;
				rules.AllowCulturalVictory = this.chkAllowCulturalVictory.Checked;
				rules.AllowMilitaryVictory = this.chkAllowMilitaryVictory.Checked;
				rules.AllowDiplomaticVictory = this.chkAllowDiplomaticVictory.Checked;
				rules.AllowSpaceVictory = this.chkAllowSpaceVictory.Checked;
				return rules;
			}
		}

		private void FillOpponentsGroup()
		{
			DXCheckBox civilizationCheckBox;
			GameRoot root = GameRoot.Instance;
			int y = 170;
			int x = 300;

			this.civilizationCheckBoxes = new List<DXCheckBox>();
			
			foreach(Civilization civilization in root.Ruleset.Civilizations)
			{
				if(y >= 400)
				{
					y = 170;
					x = 400;
				}
				civilizationCheckBox = new DXCheckBox(this.ControlHost, this);
				civilizationCheckBox.Font = LabelFont;
				civilizationCheckBox.Text = civilization.Name;
				civilizationCheckBox.Tag = civilization;				
				civilizationCheckBox.ForeColor = LabelColor;
				civilizationCheckBox.Size = new Size(250,20);
				civilizationCheckBox.Location = new Point(x,y);				
				this.Controls.Add(civilizationCheckBox);
				y += 20;
				this.civilizationCheckBoxes.Add(civilizationCheckBox);
			}
		}

        /// <summary>
        /// The list of <see cref="Civilization"/> objects the user chose to play against.
        /// </summary>
		public NamedObjectCollection<Civilization> Opponents
		{
			get 
			{
                NamedObjectCollection<Civilization> enemies;
                enemies = new NamedObjectCollection<Civilization>();
				foreach(DXCheckBox checkBox in this.civilizationCheckBoxes)
				{
					if(checkBox.Checked)					
						enemies.Add((Civilization)checkBox.Tag);					
				}
				
				return enemies;
			}
		}

		/// <summary>
		/// The name of the leader that was chosen.
		/// </summary>
		public string LeaderName
		{
			get { return this.txtLeaderName.Text; }
            set { this.txtLeaderName.Text = value; }
		}

        /// <summary>
        /// The <see cref="Civilization"/> representing the human player.
        /// </summary>
		public Civilization Civilization 
		{
			get { return (Civilization)this.cboCivilization.SelectedItem; }
		}

        /// <summary>
        /// The <see cref="LJM.Similization.Core.Difficulty"/> to use during gameplay.
        /// </summary>
        public Difficulty Difficulty
        {
            get
            {
                string text = this.cboDifficulty.Text;
                Difficulty difficulty = Difficulty.Chieftain;
                if (text == DirectXClientResources.DifficultyWarlord)
                    difficulty = Difficulty.Warlord;
                else if (text == DirectXClientResources.DifficultyPrince)
                    difficulty = Difficulty.Prince;
                else if (text == DirectXClientResources.DifficultyKing)
                    difficulty = Difficulty.King;
                else if (text == DirectXClientResources.DifficultyEmperor)
                    difficulty = Difficulty.Emperor;
                else if (text == DirectXClientResources.DifficultyDeity)
                    difficulty = Difficulty.Deity;

                return difficulty;
            }
        }

		private void InitializeComponent()
		{
            //this						
			this.BackColor = Color.DarkGoldenrod;
            this.BackColor2 = Color.LightGoldenrodYellow;
            this.Text = DirectXClientResources.PlayerSetupTitle;	

            //lblLeaderName
			this.lblLeaderName = new DXLabel(this.ControlHost, this);
            this.lblLeaderName.Text = DirectXClientResources.LeaderNameLabel;
            this.lblLeaderName.Location = new Point(10, 80);            
            this.lblLeaderName.Font = LabelFont;
            this.lblLeaderName.ForeColor = LabelColor;
            this.lblLeaderName.AutoSize = true;
            this.Controls.Add(this.lblLeaderName);

            //txtLeaderName
			this.txtLeaderName = new DXTextBox(this.ControlHost, this);
            this.txtLeaderName.Font = LabelFont;
            this.txtLeaderName.Location = new Point(100, 80);
            this.txtLeaderName.Size = new Size(150, 20);            
            this.Controls.Add(this.txtLeaderName);

            //lblCiv
			this.lblCiv = new DXLabel(this.ControlHost, this);
            this.lblCiv.Text = DirectXClientResources.CivilizationLabel;
            this.lblCiv.Location = new Point(10, 110);            
            this.lblCiv.Font = LabelFont;
            this.lblCiv.ForeColor = LabelColor;
            this.lblCiv.AutoSize = true;
            this.Controls.Add(this.lblCiv);

            //cboCivilization
			this.cboCivilization = new DXComboBox(this.ControlHost, this);
            this.cboCivilization.SelectedIndexChanged += new EventHandler(CivilizationChanged);
			this.cboCivilization.Location = new Point(100,110);
            this.cboCivilization.Size = new Size(150, 20);
            this.cboCivilization.DataSource = ClientApplication.Instance.ServerInstance.Ruleset.Civilizations;
            this.cboCivilization.DisplayMember = "Name";            
			this.Controls.Add(this.cboCivilization);

            //lblDifficulty
			this.lblDifficulty = new DXLabel(this.ControlHost, this);
            this.lblDifficulty.Text = DirectXClientResources.DifficultyLabel;
            this.lblDifficulty.Location = new Point(10, 140);
            this.lblDifficulty.Font = LabelFont;
            this.lblDifficulty.ForeColor = LabelColor;
            this.lblDifficulty.AutoSize = true;
			this.Controls.Add(this.lblDifficulty);

            //cboDifficulty
			this.cboDifficulty = new DXComboBox(this.ControlHost, this);
            this.cboDifficulty.Location = new Point(100, 140);
            this.cboDifficulty.Size = new Size(150, 20);
            this.cboDifficulty.DataSource = CreateDifficultyList();
            this.Controls.Add(this.cboDifficulty);

            //lblRules
			this.lblRules = new DXLabel(this.ControlHost, this);
            this.lblRules.Text = DirectXClientResources.RulesLabel;
			this.lblRules.Location = new Point(10,170);			
			this.lblRules.Font = LabelFont;
            this.lblRules.ForeColor = LabelColor;
            this.lblRules.AutoSize = true;		
			this.Controls.Add(this.lblRules);

			//chkAllowDomination			
			this.chkAllowDomination = new DXCheckBox(this.ControlHost, this);
            this.chkAllowDomination.Location = new Point(10, 200);
            this.chkAllowDomination.Text = DirectXClientResources.RuleDominationVictory;
            this.chkAllowDomination.BackColor = Color.White;
            this.chkAllowDomination.Font = LabelFont;
            this.chkAllowDomination.ForeColor = LabelColor;
            this.chkAllowDomination.Size = new Size(200, 20);
            this.Controls.Add(this.chkAllowDomination);

            //chkAllowDiplomaticVictory
			this.chkAllowDiplomaticVictory = new DXCheckBox(this.ControlHost, this);
            this.chkAllowDiplomaticVictory.Location = new Point(10, 220);
            this.chkAllowDiplomaticVictory.Text = DirectXClientResources.RuleDiplomaticVictory;
            this.chkAllowDiplomaticVictory.Font = LabelFont;
            this.chkAllowDiplomaticVictory.BackColor = Color.White;
            this.chkAllowDiplomaticVictory.ForeColor = LabelColor;
            this.chkAllowDiplomaticVictory.Size = new Size(200, 20);
            this.Controls.Add(this.chkAllowDiplomaticVictory);

            //chkAllowCulturalVictory
			this.chkAllowCulturalVictory = new DXCheckBox(this.ControlHost, this);
            this.chkAllowCulturalVictory.Location = new Point(10, 240);
            this.chkAllowCulturalVictory.Text = DirectXClientResources.RuleCulturalVictory;
            this.chkAllowCulturalVictory.BackColor = Color.White;
            this.chkAllowCulturalVictory.Font = LabelFont;
            this.chkAllowCulturalVictory.ForeColor = LabelColor;
            this.chkAllowCulturalVictory.Size = new Size(200, 20);
            this.Controls.Add(this.chkAllowCulturalVictory);

            //chkAllowSpaceVictory
			this.chkAllowSpaceVictory = new DXCheckBox(this.ControlHost, this);
            this.chkAllowSpaceVictory.Location = new Point(10, 260);
            this.chkAllowSpaceVictory.Text = DirectXClientResources.RuleSpaceVictory;
            this.chkAllowSpaceVictory.BackColor = Color.White;
            this.chkAllowSpaceVictory.Font = LabelFont;
            this.chkAllowSpaceVictory.ForeColor = LabelColor;
            this.chkAllowSpaceVictory.Size = new Size(200, 20);
            this.Controls.Add(this.chkAllowSpaceVictory);

            //chkAllowMilitaryVictory
			this.chkAllowMilitaryVictory = new DXCheckBox(this.ControlHost, this);
            this.chkAllowMilitaryVictory.Location = new Point(10, 280);
            this.chkAllowMilitaryVictory.Text = DirectXClientResources.RuleMilitaryVictory;
            this.chkAllowMilitaryVictory.BackColor = Color.White;
            this.chkAllowMilitaryVictory.Font = LabelFont;
            this.chkAllowMilitaryVictory.ForeColor = LabelColor;
            this.chkAllowMilitaryVictory.Size = new Size(200, 20);
            this.Controls.Add(this.chkAllowMilitaryVictory);

            //chkAllowSpecificAbilities
			this.chkAllowSpecificAbilities = new DXCheckBox(this.ControlHost, this);
            this.chkAllowSpecificAbilities.Location = new Point(10, 300);
            this.chkAllowSpecificAbilities.Text = DirectXClientResources.RuleCivilizationAbilities;
            this.chkAllowSpecificAbilities.Font = LabelFont;
            this.chkAllowSpecificAbilities.ForeColor = LabelColor;
            this.chkAllowSpecificAbilities.BackColor = Color.White;
            this.chkAllowSpecificAbilities.Size = new Size(200, 20);
            this.Controls.Add(this.chkAllowSpecificAbilities);

            //chkStandardRules
			this.chkStandardRules = new DXCheckBox(this.ControlHost, this);
            this.chkStandardRules.Location = new Point(10, 320);
            this.chkStandardRules.Text = DirectXClientResources.RuleStandard;
            this.chkStandardRules.Font = LabelFont;
            this.chkStandardRules.BackColor = Color.White;
            this.chkStandardRules.ForeColor = LabelColor;
            this.chkStandardRules.Size = new Size(200, 20);
            this.Controls.Add(this.chkStandardRules);

            //lblOpponents
			this.lblOpponents = new DXLabel(this.ControlHost, this);
            this.lblOpponents.Location = new Point(300, 140);
            this.lblOpponents.Text = DirectXClientResources.OpponentsLabel;
            this.lblOpponents.Font = LabelFont;
            this.lblOpponents.ForeColor = LabelColor;
            this.lblOpponents.AutoSize = true;
            this.Controls.Add(this.lblOpponents);
		}

        private string[] CreateDifficultyList()
        {
            return new string[] {
                DirectXClientResources.DifficultyChieftain,
                DirectXClientResources.DifficultyWarlord,
                DirectXClientResources.DifficultyPrince,
                DirectXClientResources.DifficultyKing,
                DirectXClientResources.DifficultyEmperor,
                DirectXClientResources.DifficultyDeity
            };
        }

        private void CivilizationChanged(object sender, System.EventArgs e)
        {
            DXComboBox cbo = (DXComboBox)sender;
            Civilization civ = (Civilization)cbo.SelectedItem;
            this.txtLeaderName.Text = civ.LeaderName;
        }
	}
}
