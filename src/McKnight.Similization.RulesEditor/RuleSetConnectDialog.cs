using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace McKnight.SimilizationRulesEditor
{
	/// <summary>
	/// Summary description for frmRuleSetConnect.
	/// </summary>
	public class frmRuleSetConnect : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button _connectButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label _serverLabel;
        private System.Windows.Forms.TextBox _serverTextbox;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.Label _portLabel;
		private System.Windows.Forms.TextBox _portTextBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmRuleSetConnect()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRuleSetConnect));
            this._connectButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._serverLabel = new System.Windows.Forms.Label();
            this._serverTextbox = new System.Windows.Forms.TextBox();
            this._portLabel = new System.Windows.Forms.Label();
            this._portTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _connectButton
            // 
            resources.ApplyResources(this._connectButton, "_connectButton");
            this._connectButton.Name = "_connectButton";
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this._cancelButton, "_cancelButton");
            this._cancelButton.Name = "_cancelButton";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // _serverLabel
            // 
            resources.ApplyResources(this._serverLabel, "_serverLabel");
            this._serverLabel.Name = "_serverLabel";
            // 
            // _serverTextbox
            // 
            resources.ApplyResources(this._serverTextbox, "_serverTextbox");
            this._serverTextbox.Name = "_serverTextbox";
            // 
            // _portLabel
            // 
            resources.ApplyResources(this._portLabel, "_portLabel");
            this._portLabel.Name = "_portLabel";
            // 
            // _portTextBox
            // 
            resources.ApplyResources(this._portTextBox, "_portTextBox");
            this._portTextBox.Name = "_portTextBox";
            // 
            // frmRuleSetConnect
            // 
            this.AcceptButton = this._connectButton;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this._cancelButton;
            this.Controls.Add(this._portTextBox);
            this.Controls.Add(this._portLabel);
            this.Controls.Add(this._serverTextbox);
            this.Controls.Add(this._serverLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._connectButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRuleSetConnect";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
	}
}
