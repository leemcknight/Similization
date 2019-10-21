using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// Dialog used to ask the user for an amount of gold.
	/// </summary>
	public class GoldAmountDialog : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Label lblAmount;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
        private MaskedTextBox txtAmount;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="GoldAmountDialog"/> class.
		/// </summary>
		public GoldAmountDialog()
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

		/// <summary>
		/// Gets the amount of gold the player has chosen to give.
		/// </summary>
		public int GoldAmount
		{
			get 
			{
				return int.Parse(txtAmount.Text, System.Globalization.NumberStyles.Currency, CultureInfo.CurrentCulture);
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoldAmountDialog));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            // 
            // lblAmount
            // 
            resources.ApplyResources(this.lblAmount, "lblAmount");
            this.lblAmount.Name = "lblAmount";
            // 
            // txtAmount
            // 
            resources.ApplyResources(this.txtAmount, "txtAmount");
            this.txtAmount.Name = "txtAmount";
            // 
            // GoldAmountDialog
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GoldAmountDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
	}
}
