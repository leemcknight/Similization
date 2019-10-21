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
	/// This is the form that is shown to the user whenever a new improvement is built
	/// by a city.
	/// </summary>
	public class frmImprovementBuilt : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _goToCityButton;
		private System.Windows.Forms.Label _announcementLabel;
		private City _city;
		private System.Windows.Forms.PictureBox _unitPictureBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmImprovementBuilt(ImprovementBuiltEventArgs e)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_announcementLabel.Text = 
				"Sir, I am pleased to announce that " +
				e.City.Name +
				"has produced a " +
				e.Improvement.Name +
				".";

			_city = e.City;
			
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
			this._goToCityButton = new System.Windows.Forms.Button();
			this._announcementLabel = new System.Windows.Forms.Label();
			this._unitPictureBox = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// _okButton
			// 
			this._okButton.Location = new System.Drawing.Point(128, 216);
			this._okButton.Name = "_okButton";
			this._okButton.Size = new System.Drawing.Size(88, 24);
			this._okButton.TabIndex = 0;
			this._okButton.Text = "&OK";
			// 
			// _goToCityButton
			// 
			this._goToCityButton.Location = new System.Drawing.Point(224, 216);
			this._goToCityButton.Name = "_goToCityButton";
			this._goToCityButton.Size = new System.Drawing.Size(88, 24);
			this._goToCityButton.TabIndex = 1;
			this._goToCityButton.Text = "&Go To City...";
			this._goToCityButton.Click += new System.EventHandler(this._goToCityButton_Click);
			// 
			// _announcementLabel
			// 
			this._announcementLabel.Location = new System.Drawing.Point(64, 24);
			this._announcementLabel.Name = "_announcementLabel";
			this._announcementLabel.Size = new System.Drawing.Size(248, 40);
			this._announcementLabel.TabIndex = 2;
			this._announcementLabel.Text = "_announcementLabel";
			// 
			// _unitPictureBox
			// 
			this._unitPictureBox.Location = new System.Drawing.Point(8, 16);
			this._unitPictureBox.Name = "_unitPictureBox";
			this._unitPictureBox.Size = new System.Drawing.Size(48, 48);
			this._unitPictureBox.TabIndex = 3;
			this._unitPictureBox.TabStop = false;
			// 
			// frmImprovementBuilt
			// 
			this.AcceptButton = this._okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(320, 248);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this._unitPictureBox,
																		  this._announcementLabel,
																		  this._goToCityButton,
																		  this._okButton});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmImprovementBuilt";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "City Announcement";
			this.ResumeLayout(false);

		}
		#endregion

		private void _goToCityButton_Click(object sender, System.EventArgs e)
		{
			frmCityDetails cityWindow;

			cityWindow = new frmCityDetails(_city);

			cityWindow.ShowDialog(this.Owner);
			this.Close();
		}
	}
}
