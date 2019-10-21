using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace LJM.SimilizationRulesEditor
{
	/// <summary>
	/// Summary description for frmNewCityWindow.
	/// </summary>
	public class frmNewCityWindow : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.Label _cityNameLabel;
		private System.Windows.Forms.TextBox _cityNameTextBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmNewCityWindow()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this._okButton = new System.Windows.Forms.Button();
			this._cancelButton = new System.Windows.Forms.Button();
			this._cityNameLabel = new System.Windows.Forms.Label();
			this._cityNameTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// _okButton
			// 
			this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._okButton.Location = new System.Drawing.Point(48, 72);
			this._okButton.Name = "_okButton";
			this._okButton.Size = new System.Drawing.Size(80, 24);
			this._okButton.TabIndex = 0;
			this._okButton.Text = "&OK";
			// 
			// _cancelButton
			// 
			this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancelButton.Location = new System.Drawing.Point(136, 72);
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.Size = new System.Drawing.Size(80, 24);
			this._cancelButton.TabIndex = 1;
			this._cancelButton.Text = "&Cancel";
			// 
			// _cityNameLabel
			// 
			this._cityNameLabel.Location = new System.Drawing.Point(8, 16);
			this._cityNameLabel.Name = "_cityNameLabel";
			this._cityNameLabel.Size = new System.Drawing.Size(120, 16);
			this._cityNameLabel.TabIndex = 2;
			this._cityNameLabel.Text = "Name you new city:";
			// 
			// _cityNameTextBox
			// 
			this._cityNameTextBox.Location = new System.Drawing.Point(8, 40);
			this._cityNameTextBox.Name = "_cityNameTextBox";
			this._cityNameTextBox.Size = new System.Drawing.Size(208, 21);
			this._cityNameTextBox.TabIndex = 0;
			this._cityNameTextBox.Text = "";
			// 
			// frmNewCityWindow
			// 
			this.AcceptButton = this._okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.CancelButton = this._cancelButton;
			this.ClientSize = new System.Drawing.Size(226, 104);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this._cityNameTextBox,
																		  this._cityNameLabel,
																		  this._cancelButton,
																		  this._okButton});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmNewCityWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Build a New City";
			this.ResumeLayout(false);

		}
		#endregion

		public string CityName
		{
			get { return _cityNameTextBox.Text; }
		}
	}
}
