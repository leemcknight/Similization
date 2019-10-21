using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Similization.Objects;
using LJM.Similization.DataObjects;
using LJM.Similization.DataObjects.Relational;

namespace LJM.SimilizationRulesEditor
{
	/// <summary>
	/// Summary description for frmCityDetails.
	/// </summary>
	public class frmCityDetails : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label _cityNameLabel;
		private System.Windows.Forms.TextBox _cityNameTextBox;
		private System.Windows.Forms.Label _nextImprovementLabel;
		private System.Windows.Forms.ComboBox _nextImprovementCombo;
		private System.Windows.Forms.Label _improvementsLabel;
		private System.Windows.Forms.ListBox _improvementsListBox;
		private System.Windows.Forms.Label _populationLabel;
		private System.Windows.Forms.TextBox _populationTextBox;
		private System.Windows.Forms.Label _happyLabel;
		private System.Windows.Forms.TextBox _happyTextBox;
		private System.Windows.Forms.Label _contentLabel;
		private System.Windows.Forms.TextBox _contentTextBox;
		private System.Windows.Forms.Label _sadLabel;
		private System.Windows.Forms.TextBox _sadTextBox;
		private City _city;
		private System.Windows.Forms.Label _progressLabel;
		private System.Windows.Forms.Label _foodLabel;
		private System.Windows.Forms.TextBox _foodTextBox;
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.Label _unitsLabel;
		private System.Windows.Forms.ListBox _unitListBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmCityDetails(City city)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_city = city;
			FillCityDetails();
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

		private void FillCityDetails()
		{
			_cityNameTextBox.Text = _city.Name;
			_populationTextBox.Text = _city.Population.ToString();
			_happyTextBox.Text = _city.HappyPeople.ToString();
			_sadTextBox.Text = _city.SadPeople.ToString();
			_contentTextBox.Text = _city.ContentPeople.ToString();
			_progressLabel.Text = _city.Shields.ToString() + "/" + _city.NextImprovement.Cost.ToString();
			_nextImprovementCombo.DataSource = _city.BuildableItems;
			_nextImprovementCombo.SelectedItem = _city.NextImprovement;
			_improvementsListBox.DataSource = _city.Improvements;
			_foodTextBox.Text = _city.AvailableFood.ToString();
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._cityNameLabel = new System.Windows.Forms.Label();
			this._cityNameTextBox = new System.Windows.Forms.TextBox();
			this._okButton = new System.Windows.Forms.Button();
			this._nextImprovementLabel = new System.Windows.Forms.Label();
			this._nextImprovementCombo = new System.Windows.Forms.ComboBox();
			this._improvementsLabel = new System.Windows.Forms.Label();
			this._improvementsListBox = new System.Windows.Forms.ListBox();
			this._populationLabel = new System.Windows.Forms.Label();
			this._populationTextBox = new System.Windows.Forms.TextBox();
			this._happyLabel = new System.Windows.Forms.Label();
			this._happyTextBox = new System.Windows.Forms.TextBox();
			this._contentLabel = new System.Windows.Forms.Label();
			this._contentTextBox = new System.Windows.Forms.TextBox();
			this._sadLabel = new System.Windows.Forms.Label();
			this._sadTextBox = new System.Windows.Forms.TextBox();
			this._progressLabel = new System.Windows.Forms.Label();
			this._foodLabel = new System.Windows.Forms.Label();
			this._foodTextBox = new System.Windows.Forms.TextBox();
			this._cancelButton = new System.Windows.Forms.Button();
			this._unitsLabel = new System.Windows.Forms.Label();
			this._unitListBox = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// _cityNameLabel
			// 
			this._cityNameLabel.Location = new System.Drawing.Point(8, 8);
			this._cityNameLabel.Name = "_cityNameLabel";
			this._cityNameLabel.Size = new System.Drawing.Size(64, 16);
			this._cityNameLabel.TabIndex = 0;
			this._cityNameLabel.Text = "City Name:";
			// 
			// _cityNameTextBox
			// 
			this._cityNameTextBox.Location = new System.Drawing.Point(128, 8);
			this._cityNameTextBox.Name = "_cityNameTextBox";
			this._cityNameTextBox.Size = new System.Drawing.Size(264, 21);
			this._cityNameTextBox.TabIndex = 1;
			this._cityNameTextBox.Text = "";
			// 
			// _okButton
			// 
			this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._okButton.Location = new System.Drawing.Point(240, 288);
			this._okButton.Name = "_okButton";
			this._okButton.Size = new System.Drawing.Size(72, 24);
			this._okButton.TabIndex = 2;
			this._okButton.Text = "&OK";
			this._okButton.Click += new System.EventHandler(this._okButton_Click);
			// 
			// _nextImprovementLabel
			// 
			this._nextImprovementLabel.Location = new System.Drawing.Point(8, 40);
			this._nextImprovementLabel.Name = "_nextImprovementLabel";
			this._nextImprovementLabel.Size = new System.Drawing.Size(104, 16);
			this._nextImprovementLabel.TabIndex = 3;
			this._nextImprovementLabel.Text = "Currently Building:";
			// 
			// _nextImprovementCombo
			// 
			this._nextImprovementCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._nextImprovementCombo.Location = new System.Drawing.Point(128, 40);
			this._nextImprovementCombo.Name = "_nextImprovementCombo";
			this._nextImprovementCombo.Size = new System.Drawing.Size(120, 21);
			this._nextImprovementCombo.TabIndex = 4;
			// 
			// _improvementsLabel
			// 
			this._improvementsLabel.Location = new System.Drawing.Point(8, 136);
			this._improvementsLabel.Name = "_improvementsLabel";
			this._improvementsLabel.Size = new System.Drawing.Size(120, 16);
			this._improvementsLabel.TabIndex = 5;
			this._improvementsLabel.Text = "Improvements:";
			// 
			// _improvementsListBox
			// 
			this._improvementsListBox.IntegralHeight = false;
			this._improvementsListBox.Location = new System.Drawing.Point(8, 152);
			this._improvementsListBox.Name = "_improvementsListBox";
			this._improvementsListBox.Size = new System.Drawing.Size(184, 128);
			this._improvementsListBox.TabIndex = 6;
			// 
			// _populationLabel
			// 
			this._populationLabel.Location = new System.Drawing.Point(8, 72);
			this._populationLabel.Name = "_populationLabel";
			this._populationLabel.Size = new System.Drawing.Size(104, 16);
			this._populationLabel.TabIndex = 7;
			this._populationLabel.Text = "Population:";
			// 
			// _populationTextBox
			// 
			this._populationTextBox.Location = new System.Drawing.Point(128, 72);
			this._populationTextBox.Name = "_populationTextBox";
			this._populationTextBox.Size = new System.Drawing.Size(64, 21);
			this._populationTextBox.TabIndex = 8;
			this._populationTextBox.Text = "";
			// 
			// _happyLabel
			// 
			this._happyLabel.Location = new System.Drawing.Point(16, 104);
			this._happyLabel.Name = "_happyLabel";
			this._happyLabel.Size = new System.Drawing.Size(48, 16);
			this._happyLabel.TabIndex = 9;
			this._happyLabel.Text = "Happy:";
			// 
			// _happyTextBox
			// 
			this._happyTextBox.Location = new System.Drawing.Point(56, 104);
			this._happyTextBox.Name = "_happyTextBox";
			this._happyTextBox.Size = new System.Drawing.Size(48, 21);
			this._happyTextBox.TabIndex = 10;
			this._happyTextBox.Text = "";
			// 
			// _contentLabel
			// 
			this._contentLabel.Location = new System.Drawing.Point(112, 104);
			this._contentLabel.Name = "_contentLabel";
			this._contentLabel.Size = new System.Drawing.Size(48, 16);
			this._contentLabel.TabIndex = 11;
			this._contentLabel.Text = "Content:";
			// 
			// _contentTextBox
			// 
			this._contentTextBox.Location = new System.Drawing.Point(160, 104);
			this._contentTextBox.Name = "_contentTextBox";
			this._contentTextBox.Size = new System.Drawing.Size(48, 21);
			this._contentTextBox.TabIndex = 12;
			this._contentTextBox.Text = "";
			// 
			// _sadLabel
			// 
			this._sadLabel.Location = new System.Drawing.Point(216, 104);
			this._sadLabel.Name = "_sadLabel";
			this._sadLabel.Size = new System.Drawing.Size(40, 23);
			this._sadLabel.TabIndex = 13;
			this._sadLabel.Text = "Sad:";
			// 
			// _sadTextBox
			// 
			this._sadTextBox.Location = new System.Drawing.Point(248, 104);
			this._sadTextBox.Name = "_sadTextBox";
			this._sadTextBox.Size = new System.Drawing.Size(48, 21);
			this._sadTextBox.TabIndex = 14;
			this._sadTextBox.Text = "";
			// 
			// _progressLabel
			// 
			this._progressLabel.Location = new System.Drawing.Point(264, 40);
			this._progressLabel.Name = "_progressLabel";
			this._progressLabel.Size = new System.Drawing.Size(120, 16);
			this._progressLabel.TabIndex = 15;
			// 
			// _foodLabel
			// 
			this._foodLabel.Location = new System.Drawing.Point(208, 72);
			this._foodLabel.Name = "_foodLabel";
			this._foodLabel.Size = new System.Drawing.Size(104, 16);
			this._foodLabel.TabIndex = 16;
			this._foodLabel.Text = "Food Available:";
			// 
			// _foodTextBox
			// 
			this._foodTextBox.Location = new System.Drawing.Point(328, 72);
			this._foodTextBox.Name = "_foodTextBox";
			this._foodTextBox.Size = new System.Drawing.Size(64, 21);
			this._foodTextBox.TabIndex = 17;
			this._foodTextBox.Text = "";
			// 
			// _cancelButton
			// 
			this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancelButton.Location = new System.Drawing.Point(320, 288);
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.Size = new System.Drawing.Size(72, 24);
			this._cancelButton.TabIndex = 18;
			this._cancelButton.Text = "&Cancel";
			// 
			// _unitsLabel
			// 
			this._unitsLabel.Location = new System.Drawing.Point(216, 136);
			this._unitsLabel.Name = "_unitsLabel";
			this._unitsLabel.Size = new System.Drawing.Size(96, 16);
			this._unitsLabel.TabIndex = 19;
			this._unitsLabel.Text = "Units:";
			// 
			// _unitListBox
			// 
			this._unitListBox.IntegralHeight = false;
			this._unitListBox.Location = new System.Drawing.Point(208, 152);
			this._unitListBox.Name = "_unitListBox";
			this._unitListBox.Size = new System.Drawing.Size(184, 128);
			this._unitListBox.TabIndex = 20;
			// 
			// frmCityDetails
			// 
			this.AcceptButton = this._okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.CancelButton = this._cancelButton;
			this.ClientSize = new System.Drawing.Size(402, 320);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this._unitListBox,
																		  this._unitsLabel,
																		  this._cancelButton,
																		  this._foodTextBox,
																		  this._foodLabel,
																		  this._progressLabel,
																		  this._sadTextBox,
																		  this._sadLabel,
																		  this._contentTextBox,
																		  this._contentLabel,
																		  this._happyTextBox,
																		  this._happyLabel,
																		  this._populationTextBox,
																		  this._populationLabel,
																		  this._improvementsListBox,
																		  this._improvementsLabel,
																		  this._nextImprovementCombo,
																		  this._nextImprovementLabel,
																		  this._okButton,
																		  this._cityNameTextBox,
																		  this._cityNameLabel});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmCityDetails";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "City Details";
			this.ResumeLayout(false);

		}
		#endregion

		private void _okButton_Click(object sender, System.EventArgs e)
		{
			if(_city.NextImprovement != _nextImprovementCombo.SelectedItem)
			{
				_city.NextImprovement = 
					(BuildableDataObject)_nextImprovementCombo.SelectedItem;
			}
		}
	}
}
