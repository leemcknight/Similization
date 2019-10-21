using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using McKnight.Similization.Core;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// User Control responsible for gathering player setup information.
	/// </summary>
	public class PlayerSetupControl : System.Windows.Forms.UserControl
	{
		private ArrayList civilizationCheckBoxes;
        private Color playerColor;
        private Rules rules;
		private System.Windows.Forms.GroupBox grpRules;
		private System.Windows.Forms.ComboBox cboCivilization;
		private System.Windows.Forms.Label lblCivilization;
		private System.Windows.Forms.Label lblLeaderName;
		private System.Windows.Forms.TextBox txtLeaderName;
		private System.Windows.Forms.GroupBox grpOpponents;
		private System.Windows.Forms.Label lblColor;
		private System.Windows.Forms.Panel pnlColor;
		private System.Windows.Forms.Button btnColor;
		private System.Windows.Forms.CheckBox chkCultural;
		private System.Windows.Forms.CheckBox chkAbilities;
		private System.Windows.Forms.CheckBox chkDiplomatic;
		private System.Windows.Forms.CheckBox chkMilitary;
		private System.Windows.Forms.CheckBox chkDomination;
		private System.Windows.Forms.CheckBox chkSpace;
		private System.Windows.Forms.Label lblDifficulty;
		private System.Windows.Forms.ComboBox cboDifficulty;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initializes a new instance of the <c>PlayerSetupControl</c>
		/// </summary>
		public PlayerSetupControl()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.cboCivilization.DataSource = GameRoot.Instance.Ruleset.Civilizations;
			this.cboCivilization.DisplayMember = "Name";
            txtLeaderName.Text = ((Civilization)cboCivilization.SelectedItem).LeaderName;

			this.rules = new Rules();       
			this.chkCultural.DataBindings.Add("Checked", this.rules, "AllowCulturalVictory");
			this.chkDiplomatic.DataBindings.Add("Checked", this.rules, "AllowDiplomaticVictory");
			this.chkDomination.DataBindings.Add("Checked", this.rules, "AllowDominationVictory");
			this.chkMilitary.DataBindings.Add("Checked", this.rules, "AllowMilitaryVictory");
			this.chkSpace.DataBindings.Add("Checked", this.rules, "AllowSpaceVictory");
			this.chkAbilities.DataBindings.Add("Checked", this.rules, "AllowCivilizationSpecificAbilities");

			//default the player color to red.  No good reason for red, just 
			//needed a default color.  Perhaps in a future release this (default color)
			//should be customizable.
			this.playerColor = Color.Red;

			FillOpponentsGroup();
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

		private void FillOpponentsGroup()
		{
			CheckBox civilizationCheckBox;
			GameRoot root = GameRoot.Instance;
			int y = 20;
			int x = 10;

			this.civilizationCheckBoxes = new ArrayList();
			
			foreach(Civilization civilization in root.Ruleset.Civilizations)
			{
				if(y >= 200)
				{
					y = 20;
					x = 150;
				}
				civilizationCheckBox = new CheckBox();
				civilizationCheckBox.Text = civilization.Name;
				civilizationCheckBox.Tag = civilization;
				civilizationCheckBox.FlatStyle = FlatStyle.System;
				grpOpponents.Controls.Add(civilizationCheckBox);
				civilizationCheckBox.Location = new Point(x,y);
				y += 20;
				this.civilizationCheckBoxes.Add(civilizationCheckBox);
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			if(colorDialog.ShowDialog(this.ParentForm) == DialogResult.OK)
			{
				this.playerColor = colorDialog.Color;
				this.pnlColor.BackColor =  colorDialog.Color;
			}
		}

		private void cboCivilization_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			txtLeaderName.Text = ((Civilization)cboCivilization.SelectedItem).LeaderName;
		}

		/// <summary>
		/// Gets the civilization the player chose for their country.
		/// </summary>
		public Civilization ChosenCivilization
		{
			get 
			{
				return (Civilization)this.cboCivilization.SelectedItem;
			}
		}
		
		/// <summary>
		/// Gets the color the player chose for their country.
		/// </summary>
		public Color PlayerColor
		{
			get { return this.playerColor; }
		}

		/// <summary>
		/// Gets the list of opponents the player chose.
		/// </summary>
		public NamedObjectCollection<Civilization> ChosenOpponents
		{
			get
			{
                NamedObjectCollection<Civilization> enemies;
                enemies = new NamedObjectCollection<Civilization>();
				foreach(CheckBox checkBox in this.civilizationCheckBoxes)
				{
					if(checkBox.Checked)
					{
						enemies.Add((Civilization)checkBox.Tag);
					}
				}
				
				return enemies;
				
			}
		}
		
		/// <summary>
		/// The rules of the game.
		/// </summary>
		public Rules Rules
		{
			get	{	return this.rules; 	}
		}

		/// <summary>
		/// Gets the name of the leader the player chose.
		/// </summary>
		public string LeaderName
		{
			get { return txtLeaderName.Text; }
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerSetupControl));
            this.grpRules = new System.Windows.Forms.GroupBox();
            this.chkCultural = new System.Windows.Forms.CheckBox();
            this.chkAbilities = new System.Windows.Forms.CheckBox();
            this.chkDiplomatic = new System.Windows.Forms.CheckBox();
            this.chkMilitary = new System.Windows.Forms.CheckBox();
            this.chkDomination = new System.Windows.Forms.CheckBox();
            this.chkSpace = new System.Windows.Forms.CheckBox();
            this.cboCivilization = new System.Windows.Forms.ComboBox();
            this.lblCivilization = new System.Windows.Forms.Label();
            this.lblLeaderName = new System.Windows.Forms.Label();
            this.txtLeaderName = new System.Windows.Forms.TextBox();
            this.grpOpponents = new System.Windows.Forms.GroupBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.pnlColor = new System.Windows.Forms.Panel();
            this.btnColor = new System.Windows.Forms.Button();
            this.lblDifficulty = new System.Windows.Forms.Label();
            this.cboDifficulty = new System.Windows.Forms.ComboBox();
            this.grpRules.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpRules
            // 
            this.grpRules.Controls.Add(this.chkCultural);
            this.grpRules.Controls.Add(this.chkAbilities);
            this.grpRules.Controls.Add(this.chkDiplomatic);
            this.grpRules.Controls.Add(this.chkMilitary);
            this.grpRules.Controls.Add(this.chkDomination);
            this.grpRules.Controls.Add(this.chkSpace);
            this.grpRules.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.grpRules, "grpRules");
            this.grpRules.Name = "grpRules";
            this.grpRules.TabStop = false;
            // 
            // chkCultural
            // 
            resources.ApplyResources(this.chkCultural, "chkCultural");
            this.chkCultural.Name = "chkCultural";
            // 
            // chkAbilities
            // 
            resources.ApplyResources(this.chkAbilities, "chkAbilities");
            this.chkAbilities.Name = "chkAbilities";
            // 
            // chkDiplomatic
            // 
            resources.ApplyResources(this.chkDiplomatic, "chkDiplomatic");
            this.chkDiplomatic.Name = "chkDiplomatic";
            // 
            // chkMilitary
            // 
            resources.ApplyResources(this.chkMilitary, "chkMilitary");
            this.chkMilitary.Name = "chkMilitary";
            // 
            // chkDomination
            // 
            resources.ApplyResources(this.chkDomination, "chkDomination");
            this.chkDomination.Name = "chkDomination";
            // 
            // chkSpace
            // 
            resources.ApplyResources(this.chkSpace, "chkSpace");
            this.chkSpace.Name = "chkSpace";
            // 
            // cboCivilization
            // 
            this.cboCivilization.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCivilization.FormattingEnabled = true;
            resources.ApplyResources(this.cboCivilization, "cboCivilization");
            this.cboCivilization.Name = "cboCivilization";
            this.cboCivilization.SelectedIndexChanged += new System.EventHandler(this.cboCivilization_SelectedIndexChanged);
            // 
            // lblCivilization
            // 
            resources.ApplyResources(this.lblCivilization, "lblCivilization");
            this.lblCivilization.Name = "lblCivilization";
            // 
            // lblLeaderName
            // 
            resources.ApplyResources(this.lblLeaderName, "lblLeaderName");
            this.lblLeaderName.Name = "lblLeaderName";
            // 
            // txtLeaderName
            // 
            resources.ApplyResources(this.txtLeaderName, "txtLeaderName");
            this.txtLeaderName.Name = "txtLeaderName";
            // 
            // grpOpponents
            // 
            this.grpOpponents.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.grpOpponents, "grpOpponents");
            this.grpOpponents.Name = "grpOpponents";
            this.grpOpponents.TabStop = false;
            // 
            // lblColor
            // 
            resources.ApplyResources(this.lblColor, "lblColor");
            this.lblColor.Name = "lblColor";
            // 
            // pnlColor
            // 
            this.pnlColor.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.pnlColor, "pnlColor");
            this.pnlColor.Name = "pnlColor";
            // 
            // btnColor
            // 
            resources.ApplyResources(this.btnColor, "btnColor");
            this.btnColor.Name = "btnColor";
            this.btnColor.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblDifficulty
            // 
            resources.ApplyResources(this.lblDifficulty, "lblDifficulty");
            this.lblDifficulty.Name = "lblDifficulty";
            // 
            // cboDifficulty
            // 
            this.cboDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDifficulty.FormattingEnabled = true;
            resources.ApplyResources(this.cboDifficulty, "cboDifficulty");
            this.cboDifficulty.Name = "cboDifficulty";
            // 
            // PlayerSetupControl
            // 
            this.Controls.Add(this.cboDifficulty);
            this.Controls.Add(this.lblDifficulty);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.pnlColor);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.grpOpponents);
            this.Controls.Add(this.cboCivilization);
            this.Controls.Add(this.lblCivilization);
            this.Controls.Add(this.lblLeaderName);
            this.Controls.Add(this.txtLeaderName);
            this.Controls.Add(this.grpRules);
            resources.ApplyResources(this, "$this");
            this.Name = "PlayerSetupControl";
            this.grpRules.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		
	}
}
