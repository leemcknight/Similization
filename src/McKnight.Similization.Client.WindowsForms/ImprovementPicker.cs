using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using McKnight.Similization.Client;
using McKnight.Similization.Server;
using McKnight.Similization.Core;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// Dialog asking the user to select an improvement.
	/// </summary>
	public class ImprovementPicker : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ComboBox cboImprovement;
		private System.Windows.Forms.Label lblInstructions;
        private PictureBox pictureBox1;
        private Label label1;		

		private ImprovementPicker()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImprovementPicker"/> class.
		/// </summary>
		/// <param name="city"></param>
		public ImprovementPicker(City city) : this()
		{
			if(city == null)
				throw new ArgumentNullException("city");
			
			this.cboImprovement.DataSource = city.Improvements;
			this.cboImprovement.DisplayMember = "Name";
		}

		/// <summary>
		/// Gets the <see cref="McKnight.Similization.Core.Improvement"/> that the user selected.
		/// </summary>
		public Improvement Improvement
		{
			get { return (Improvement)this.cboImprovement.SelectedItem; }
		}

		#region Windows Forms Designer Generated Code
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImprovementPicker));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboImprovement = new System.Windows.Forms.ComboBox();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            // 
            // cboImprovement
            // 
            this.cboImprovement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboImprovement.FormattingEnabled = true;
            resources.ApplyResources(this.cboImprovement, "cboImprovement");
            this.cboImprovement.Name = "cboImprovement";
            // 
            // lblInstructions
            // 
            resources.ApplyResources(this.lblInstructions, "lblInstructions");
            this.lblInstructions.Name = "lblInstructions";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = McKnight.Similization.Client.WindowsForms.Properties.Resources.woman3;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ImprovementPicker
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.cboImprovement);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImprovementPicker";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
	}
}