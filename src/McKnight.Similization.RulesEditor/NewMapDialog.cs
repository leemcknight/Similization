using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace McKnight.SimilizationRulesEditor
{
	/// <summary>
	/// Summary description for frmNewMap.
	/// </summary>
	public class NewMapDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.GroupBox _sizeGroupBox;
		private System.Windows.Forms.Label _widthLabel;
		private System.Windows.Forms.Label _heightLabel;
		private System.Windows.Forms.TextBox _widthTextBox;
		private System.Windows.Forms.TextBox _heightTextBox;
		private System.Windows.Forms.Label _nameLabel;
		private System.Windows.Forms.TextBox _nameTextBox;
		private System.Windows.Forms.Label _instructionsLabel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NewMapDialog()
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

		public Size MapSize
		{
			get
			{
				Size mapSize;
				int width;
				int height;

				width = Convert.ToInt32(_widthTextBox.Text);
				height = Convert.ToInt32(_heightTextBox.Text);

				mapSize = new Size(width, height);

				return mapSize;
			}
		}

		public string MapName
		{
			get
			{
				return _nameTextBox.Text;
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewMapDialog));
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._sizeGroupBox = new System.Windows.Forms.GroupBox();
            this._heightTextBox = new System.Windows.Forms.TextBox();
            this._widthTextBox = new System.Windows.Forms.TextBox();
            this._heightLabel = new System.Windows.Forms.Label();
            this._widthLabel = new System.Windows.Forms.Label();
            this._nameLabel = new System.Windows.Forms.Label();
            this._nameTextBox = new System.Windows.Forms.TextBox();
            this._instructionsLabel = new System.Windows.Forms.Label();
            this._sizeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _okButton
            // 
            this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this._okButton, "_okButton");
            this._okButton.Name = "_okButton";
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this._cancelButton, "_cancelButton");
            this._cancelButton.Name = "_cancelButton";
            // 
            // _sizeGroupBox
            // 
            this._sizeGroupBox.Controls.Add(this._heightTextBox);
            this._sizeGroupBox.Controls.Add(this._widthTextBox);
            this._sizeGroupBox.Controls.Add(this._heightLabel);
            this._sizeGroupBox.Controls.Add(this._widthLabel);
            this._sizeGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this._sizeGroupBox, "_sizeGroupBox");
            this._sizeGroupBox.Name = "_sizeGroupBox";
            this._sizeGroupBox.TabStop = false;
            // 
            // _heightTextBox
            // 
            resources.ApplyResources(this._heightTextBox, "_heightTextBox");
            this._heightTextBox.Name = "_heightTextBox";
            // 
            // _widthTextBox
            // 
            resources.ApplyResources(this._widthTextBox, "_widthTextBox");
            this._widthTextBox.Name = "_widthTextBox";
            // 
            // _heightLabel
            // 
            resources.ApplyResources(this._heightLabel, "_heightLabel");
            this._heightLabel.Name = "_heightLabel";
            // 
            // _widthLabel
            // 
            resources.ApplyResources(this._widthLabel, "_widthLabel");
            this._widthLabel.Name = "_widthLabel";
            // 
            // _nameLabel
            // 
            resources.ApplyResources(this._nameLabel, "_nameLabel");
            this._nameLabel.Name = "_nameLabel";
            // 
            // _nameTextBox
            // 
            resources.ApplyResources(this._nameTextBox, "_nameTextBox");
            this._nameTextBox.Name = "_nameTextBox";
            // 
            // _instructionsLabel
            // 
            resources.ApplyResources(this._instructionsLabel, "_instructionsLabel");
            this._instructionsLabel.Name = "_instructionsLabel";
            // 
            // NewMapDialog
            // 
            this.AcceptButton = this._okButton;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this._cancelButton;
            this.Controls.Add(this._instructionsLabel);
            this.Controls.Add(this._nameTextBox);
            this.Controls.Add(this._nameLabel);
            this.Controls.Add(this._sizeGroupBox);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewMapDialog";
            this._sizeGroupBox.ResumeLayout(false);
            this._sizeGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
	}
}
