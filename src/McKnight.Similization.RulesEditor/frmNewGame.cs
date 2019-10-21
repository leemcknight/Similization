using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using LJM.Similization.DataObjects;
using LJM.Similization.DataObjects.Relational;

namespace LJM.SimilizationRulesEditor
{
	/// <summary>
	/// Summary description for frmNewGame.
	/// </summary>
	public class frmNewGame : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.Label _leaderNameLabel;
		private System.Windows.Forms.TextBox _leaderNameTextBox;
		private System.Windows.Forms.GroupBox _rulesGroupBox;
		private System.Windows.Forms.Label _worldSizeLabel;
		private System.Windows.Forms.ComboBox _worldSizeCombo;
		private System.Windows.Forms.Label _barbarianLabel;
		private System.Windows.Forms.ComboBox _barbarianCombo;
		private System.Windows.Forms.Label _climateLabel;
		private System.Windows.Forms.ComboBox _climateCombo;
		private System.Windows.Forms.Label _temperatureLabel;
		private System.Windows.Forms.ComboBox _temperatureCombo;
		private System.Windows.Forms.Label _ageLabel;
		private System.Windows.Forms.ComboBox _ageCombo;
		private System.Windows.Forms.Label _civilizationLabel;
		private System.Windows.Forms.ComboBox _civilizationCombo;
		private System.Windows.Forms.Label _difficultyLabel;
		private System.Windows.Forms.ComboBox _difficultyCombo;
		private System.Windows.Forms.CheckBox _dominationCheckBox;
		private System.Windows.Forms.CheckBox _diplomaticCheckBox;
		private System.Windows.Forms.CheckBox _culturalCheckBox;
		private System.Windows.Forms.CheckBox _spaceCheckBox;
		private System.Windows.Forms.CheckBox _militaryCheckBox;
		private System.Windows.Forms.CheckBox _civCheckBox;
		private System.Windows.Forms.GroupBox _opponentsGroupBox;
		private RelationalBase _relationalBase;
		private ArrayList _civilizationCheckBoxes;
		private System.Windows.Forms.Label _waterCoverageLabel;
		private System.Windows.Forms.ComboBox _waterCoverageComboBox;
		private System.Windows.Forms.Panel _topPanel;
		private System.Windows.Forms.Label _newGameHeaderLabel;
		private System.Windows.Forms.Label _instructionsLabel;
		private System.Windows.Forms.PictureBox pictureBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmNewGame(RelationalBase relationalBase)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			_relationalBase = relationalBase;
			FillCivilizationCombo();
			FillOpponentsGroup();
			_ageCombo.SelectedIndex = 0;
			_barbarianCombo.SelectedIndex = 0;
			_climateCombo.SelectedIndex = 0;
			_difficultyCombo.SelectedIndex = 0;
			_temperatureCombo.SelectedIndex = 0;
			_worldSizeCombo.SelectedIndex = 0;
			_waterCoverageComboBox.SelectedIndex = 0;
		}

		private void FillCivilizationCombo()
		{
			_civilizationCombo.DataSource = _relationalBase.Civilizations;
		}

		private void FillOpponentsGroup()
		{
			CheckBox civilizationCheckBox;
			int y = 20;
			int x = 10;

			_civilizationCheckBoxes = new ArrayList();
			
			foreach(Civilization civilization in _relationalBase.Civilizations)
			{
				if(y >= 200)
				{
					y = 20;
					x = 150;
				}
				civilizationCheckBox = new CheckBox();
				civilizationCheckBox.Text = civilization.Name;
				civilizationCheckBox.Tag = civilization;
				_opponentsGroupBox.Controls.Add(civilizationCheckBox);
				civilizationCheckBox.Location = new Point(x,y);
				y += 20;
				_civilizationCheckBoxes.Add(civilizationCheckBox);
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmNewGame));
			this._okButton = new System.Windows.Forms.Button();
			this._cancelButton = new System.Windows.Forms.Button();
			this._leaderNameLabel = new System.Windows.Forms.Label();
			this._leaderNameTextBox = new System.Windows.Forms.TextBox();
			this._rulesGroupBox = new System.Windows.Forms.GroupBox();
			this._culturalCheckBox = new System.Windows.Forms.CheckBox();
			this._civCheckBox = new System.Windows.Forms.CheckBox();
			this._diplomaticCheckBox = new System.Windows.Forms.CheckBox();
			this._militaryCheckBox = new System.Windows.Forms.CheckBox();
			this._dominationCheckBox = new System.Windows.Forms.CheckBox();
			this._spaceCheckBox = new System.Windows.Forms.CheckBox();
			this._worldSizeLabel = new System.Windows.Forms.Label();
			this._worldSizeCombo = new System.Windows.Forms.ComboBox();
			this._barbarianLabel = new System.Windows.Forms.Label();
			this._barbarianCombo = new System.Windows.Forms.ComboBox();
			this._climateLabel = new System.Windows.Forms.Label();
			this._climateCombo = new System.Windows.Forms.ComboBox();
			this._temperatureLabel = new System.Windows.Forms.Label();
			this._temperatureCombo = new System.Windows.Forms.ComboBox();
			this._ageLabel = new System.Windows.Forms.Label();
			this._ageCombo = new System.Windows.Forms.ComboBox();
			this._civilizationLabel = new System.Windows.Forms.Label();
			this._civilizationCombo = new System.Windows.Forms.ComboBox();
			this._difficultyLabel = new System.Windows.Forms.Label();
			this._difficultyCombo = new System.Windows.Forms.ComboBox();
			this._opponentsGroupBox = new System.Windows.Forms.GroupBox();
			this._waterCoverageLabel = new System.Windows.Forms.Label();
			this._waterCoverageComboBox = new System.Windows.Forms.ComboBox();
			this._topPanel = new System.Windows.Forms.Panel();
			this._newGameHeaderLabel = new System.Windows.Forms.Label();
			this._instructionsLabel = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this._rulesGroupBox.SuspendLayout();
			this._topPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// _okButton
			// 
			this._okButton.Location = new System.Drawing.Point(328, 480);
			this._okButton.Name = "_okButton";
			this._okButton.Size = new System.Drawing.Size(80, 24);
			this._okButton.TabIndex = 0;
			this._okButton.Text = "&OK";
			this._okButton.Click += new System.EventHandler(this._okButton_Click);
			// 
			// _cancelButton
			// 
			this._cancelButton.Location = new System.Drawing.Point(416, 480);
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.Size = new System.Drawing.Size(80, 24);
			this._cancelButton.TabIndex = 1;
			this._cancelButton.Text = "&Cancel";
			this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
			// 
			// _leaderNameLabel
			// 
			this._leaderNameLabel.Location = new System.Drawing.Point(8, 80);
			this._leaderNameLabel.Name = "_leaderNameLabel";
			this._leaderNameLabel.Size = new System.Drawing.Size(80, 16);
			this._leaderNameLabel.TabIndex = 4;
			this._leaderNameLabel.Text = "Leader Name:";
			// 
			// _leaderNameTextBox
			// 
			this._leaderNameTextBox.Location = new System.Drawing.Point(88, 80);
			this._leaderNameTextBox.Name = "_leaderNameTextBox";
			this._leaderNameTextBox.Size = new System.Drawing.Size(408, 21);
			this._leaderNameTextBox.TabIndex = 5;
			this._leaderNameTextBox.Text = "Lee McKnight";
			// 
			// _rulesGroupBox
			// 
			this._rulesGroupBox.Controls.AddRange(new System.Windows.Forms.Control[] {
																						 this._culturalCheckBox,
																						 this._civCheckBox,
																						 this._diplomaticCheckBox,
																						 this._militaryCheckBox,
																						 this._dominationCheckBox,
																						 this._spaceCheckBox});
			this._rulesGroupBox.Location = new System.Drawing.Point(8, 232);
			this._rulesGroupBox.Name = "_rulesGroupBox";
			this._rulesGroupBox.Size = new System.Drawing.Size(224, 240);
			this._rulesGroupBox.TabIndex = 6;
			this._rulesGroupBox.TabStop = false;
			this._rulesGroupBox.Text = "Rules";
			// 
			// _culturalCheckBox
			// 
			this._culturalCheckBox.Location = new System.Drawing.Point(8, 72);
			this._culturalCheckBox.Name = "_culturalCheckBox";
			this._culturalCheckBox.Size = new System.Drawing.Size(152, 16);
			this._culturalCheckBox.TabIndex = 2;
			this._culturalCheckBox.Text = "Allow Cultural Victory";
			// 
			// _civCheckBox
			// 
			this._civCheckBox.Location = new System.Drawing.Point(8, 144);
			this._civCheckBox.Name = "_civCheckBox";
			this._civCheckBox.Size = new System.Drawing.Size(168, 16);
			this._civCheckBox.TabIndex = 5;
			this._civCheckBox.Text = "Allow Civ-Specific Abilities";
			// 
			// _diplomaticCheckBox
			// 
			this._diplomaticCheckBox.Location = new System.Drawing.Point(8, 48);
			this._diplomaticCheckBox.Name = "_diplomaticCheckBox";
			this._diplomaticCheckBox.Size = new System.Drawing.Size(152, 16);
			this._diplomaticCheckBox.TabIndex = 1;
			this._diplomaticCheckBox.Text = "Allow Diplomatic Victory";
			// 
			// _militaryCheckBox
			// 
			this._militaryCheckBox.Location = new System.Drawing.Point(8, 120);
			this._militaryCheckBox.Name = "_militaryCheckBox";
			this._militaryCheckBox.Size = new System.Drawing.Size(136, 16);
			this._militaryCheckBox.TabIndex = 4;
			this._militaryCheckBox.Text = "Allow Military Victory";
			// 
			// _dominationCheckBox
			// 
			this._dominationCheckBox.Location = new System.Drawing.Point(8, 24);
			this._dominationCheckBox.Name = "_dominationCheckBox";
			this._dominationCheckBox.Size = new System.Drawing.Size(160, 16);
			this._dominationCheckBox.TabIndex = 0;
			this._dominationCheckBox.Text = "Allow Domination Victory";
			// 
			// _spaceCheckBox
			// 
			this._spaceCheckBox.Location = new System.Drawing.Point(8, 96);
			this._spaceCheckBox.Name = "_spaceCheckBox";
			this._spaceCheckBox.Size = new System.Drawing.Size(168, 16);
			this._spaceCheckBox.TabIndex = 3;
			this._spaceCheckBox.Text = "Allow Space Victory";
			// 
			// _worldSizeLabel
			// 
			this._worldSizeLabel.Location = new System.Drawing.Point(8, 144);
			this._worldSizeLabel.Name = "_worldSizeLabel";
			this._worldSizeLabel.Size = new System.Drawing.Size(88, 16);
			this._worldSizeLabel.TabIndex = 7;
			this._worldSizeLabel.Text = "World Size:";
			// 
			// _worldSizeCombo
			// 
			this._worldSizeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._worldSizeCombo.Items.AddRange(new object[] {
																 "Tiny",
																 "Small",
																 "Standard",
																 "Large",
																 "Huge"});
			this._worldSizeCombo.Location = new System.Drawing.Point(88, 144);
			this._worldSizeCombo.Name = "_worldSizeCombo";
			this._worldSizeCombo.Size = new System.Drawing.Size(144, 21);
			this._worldSizeCombo.TabIndex = 8;
			// 
			// _barbarianLabel
			// 
			this._barbarianLabel.Location = new System.Drawing.Point(240, 144);
			this._barbarianLabel.Name = "_barbarianLabel";
			this._barbarianLabel.Size = new System.Drawing.Size(104, 16);
			this._barbarianLabel.TabIndex = 9;
			this._barbarianLabel.Text = "Barbarian Activity:";
			// 
			// _barbarianCombo
			// 
			this._barbarianCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._barbarianCombo.Items.AddRange(new object[] {
																 "Sedentary",
																 "Roaming",
																 "Restless",
																 "Raging",
																 "Random"});
			this._barbarianCombo.Location = new System.Drawing.Point(352, 144);
			this._barbarianCombo.Name = "_barbarianCombo";
			this._barbarianCombo.Size = new System.Drawing.Size(144, 21);
			this._barbarianCombo.TabIndex = 10;
			// 
			// _climateLabel
			// 
			this._climateLabel.Location = new System.Drawing.Point(8, 176);
			this._climateLabel.Name = "_climateLabel";
			this._climateLabel.Size = new System.Drawing.Size(80, 16);
			this._climateLabel.TabIndex = 11;
			this._climateLabel.Text = "Climate:";
			// 
			// _climateCombo
			// 
			this._climateCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._climateCombo.Items.AddRange(new object[] {
															   "Arid",
															   "Normal",
															   "Wet"});
			this._climateCombo.Location = new System.Drawing.Point(88, 176);
			this._climateCombo.Name = "_climateCombo";
			this._climateCombo.Size = new System.Drawing.Size(144, 21);
			this._climateCombo.TabIndex = 12;
			// 
			// _temperatureLabel
			// 
			this._temperatureLabel.Location = new System.Drawing.Point(240, 176);
			this._temperatureLabel.Name = "_temperatureLabel";
			this._temperatureLabel.Size = new System.Drawing.Size(96, 16);
			this._temperatureLabel.TabIndex = 13;
			this._temperatureLabel.Text = "Temperature:";
			// 
			// _temperatureCombo
			// 
			this._temperatureCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._temperatureCombo.Items.AddRange(new object[] {
																   "Warm",
																   "Temperate",
																   "Cool"});
			this._temperatureCombo.Location = new System.Drawing.Point(352, 176);
			this._temperatureCombo.Name = "_temperatureCombo";
			this._temperatureCombo.Size = new System.Drawing.Size(144, 21);
			this._temperatureCombo.TabIndex = 14;
			// 
			// _ageLabel
			// 
			this._ageLabel.Location = new System.Drawing.Point(8, 208);
			this._ageLabel.Name = "_ageLabel";
			this._ageLabel.Size = new System.Drawing.Size(80, 16);
			this._ageLabel.TabIndex = 15;
			this._ageLabel.Text = "Age:";
			// 
			// _ageCombo
			// 
			this._ageCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._ageCombo.Items.AddRange(new object[] {
														   "3 Billion",
														   "4 Billion",
														   "5 Billion"});
			this._ageCombo.Location = new System.Drawing.Point(88, 208);
			this._ageCombo.Name = "_ageCombo";
			this._ageCombo.Size = new System.Drawing.Size(144, 21);
			this._ageCombo.TabIndex = 16;
			// 
			// _civilizationLabel
			// 
			this._civilizationLabel.Location = new System.Drawing.Point(8, 112);
			this._civilizationLabel.Name = "_civilizationLabel";
			this._civilizationLabel.Size = new System.Drawing.Size(80, 16);
			this._civilizationLabel.TabIndex = 17;
			this._civilizationLabel.Text = "Civilization:";
			// 
			// _civilizationCombo
			// 
			this._civilizationCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._civilizationCombo.Location = new System.Drawing.Point(88, 112);
			this._civilizationCombo.Name = "_civilizationCombo";
			this._civilizationCombo.Size = new System.Drawing.Size(144, 21);
			this._civilizationCombo.TabIndex = 18;
			// 
			// _difficultyLabel
			// 
			this._difficultyLabel.Location = new System.Drawing.Point(240, 112);
			this._difficultyLabel.Name = "_difficultyLabel";
			this._difficultyLabel.Size = new System.Drawing.Size(96, 16);
			this._difficultyLabel.TabIndex = 19;
			this._difficultyLabel.Text = "Difficulty:";
			// 
			// _difficultyCombo
			// 
			this._difficultyCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._difficultyCombo.Items.AddRange(new object[] {
																  "Chieftain",
																  "Warlord",
																  "Prince",
																  "King",
																  "Emperor",
																  "Deity"});
			this._difficultyCombo.Location = new System.Drawing.Point(352, 112);
			this._difficultyCombo.Name = "_difficultyCombo";
			this._difficultyCombo.Size = new System.Drawing.Size(144, 21);
			this._difficultyCombo.TabIndex = 20;
			// 
			// _opponentsGroupBox
			// 
			this._opponentsGroupBox.Location = new System.Drawing.Point(240, 232);
			this._opponentsGroupBox.Name = "_opponentsGroupBox";
			this._opponentsGroupBox.Size = new System.Drawing.Size(256, 240);
			this._opponentsGroupBox.TabIndex = 21;
			this._opponentsGroupBox.TabStop = false;
			this._opponentsGroupBox.Text = "Your Opponents";
			// 
			// _waterCoverageLabel
			// 
			this._waterCoverageLabel.Location = new System.Drawing.Point(240, 208);
			this._waterCoverageLabel.Name = "_waterCoverageLabel";
			this._waterCoverageLabel.Size = new System.Drawing.Size(96, 16);
			this._waterCoverageLabel.TabIndex = 22;
			this._waterCoverageLabel.Text = "Water Coverage:";
			// 
			// _waterCoverageComboBox
			// 
			this._waterCoverageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._waterCoverageComboBox.Items.AddRange(new object[] {
																		"Sixty Percent",
																		"Seventy Percent",
																		"Eighty Percent"});
			this._waterCoverageComboBox.Location = new System.Drawing.Point(352, 208);
			this._waterCoverageComboBox.Name = "_waterCoverageComboBox";
			this._waterCoverageComboBox.Size = new System.Drawing.Size(144, 21);
			this._waterCoverageComboBox.TabIndex = 23;
			// 
			// _topPanel
			// 
			this._topPanel.BackColor = System.Drawing.Color.White;
			this._topPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.pictureBox1,
																					this._instructionsLabel,
																					this._newGameHeaderLabel});
			this._topPanel.Name = "_topPanel";
			this._topPanel.Size = new System.Drawing.Size(504, 64);
			this._topPanel.TabIndex = 24;
			// 
			// _newGameHeaderLabel
			// 
			this._newGameHeaderLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._newGameHeaderLabel.Location = new System.Drawing.Point(8, 8);
			this._newGameHeaderLabel.Name = "_newGameHeaderLabel";
			this._newGameHeaderLabel.Size = new System.Drawing.Size(128, 16);
			this._newGameHeaderLabel.TabIndex = 0;
			this._newGameHeaderLabel.Text = "Start a new game";
			// 
			// _instructionsLabel
			// 
			this._instructionsLabel.Location = new System.Drawing.Point(24, 32);
			this._instructionsLabel.Name = "_instructionsLabel";
			this._instructionsLabel.Size = new System.Drawing.Size(328, 16);
			this._instructionsLabel.TabIndex = 1;
			this._instructionsLabel.Text = "Fill out this form about the game parameters and press \"OK\".";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Bitmap)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(424, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(72, 48);
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// frmNewGame
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(506, 509);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this._topPanel,
																		  this._waterCoverageComboBox,
																		  this._waterCoverageLabel,
																		  this._climateCombo,
																		  this._climateLabel,
																		  this._barbarianLabel,
																		  this._okButton,
																		  this._cancelButton,
																		  this._ageCombo,
																		  this._ageLabel,
																		  this._barbarianCombo,
																		  this._rulesGroupBox,
																		  this._civilizationCombo,
																		  this._civilizationLabel,
																		  this._leaderNameLabel,
																		  this._leaderNameTextBox,
																		  this._temperatureCombo,
																		  this._temperatureLabel,
																		  this._worldSizeCombo,
																		  this._worldSizeLabel,
																		  this._opponentsGroupBox,
																		  this._difficultyCombo,
																		  this._difficultyLabel});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmNewGame";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Start a New Game";
			this._rulesGroupBox.ResumeLayout(false);
			this._topPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void _okButton_Click(object sender, System.EventArgs e)
		{
			frmGameWindow gameWindow;
			ArrayList enemies;
			Civilization player;

			if(_leaderNameTextBox.Text == string.Empty)
			{
				MessageBox.Show("Please enter a leader name", "Similization", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			enemies = new ArrayList();
			foreach(CheckBox checkBox in _civilizationCheckBoxes)
			{
				if(checkBox.Checked)
				{
					enemies.Add(checkBox.Tag);
				}
			}

			player = (Civilization)_civilizationCombo.SelectedItem;
			

			gameWindow = new frmGameWindow();
			gameWindow.Show();
			gameWindow.StartGame(player, enemies, _leaderNameTextBox.Text, _worldSizeCombo.Text);
			this.Close();
		}

		private void _cancelButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
