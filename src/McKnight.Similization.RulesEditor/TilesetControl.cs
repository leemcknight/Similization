namespace McKnight.SimilizationRulesEditor
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Windows.Forms;


	public class TilesetControl : EditorUserControl
	{
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.GroupBox grpSize;
		private System.Windows.Forms.Label lblWidth;
		private System.Windows.Forms.TextBox txtWidth;
		private System.Windows.Forms.Label lblHeight;
		private System.Windows.Forms.TextBox txtHeight;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.TextBox txtDescription;
		private DataSet tileTypes;

		public TilesetControl()
		{
			InitializeComponent();
		}

		public override void ShowData()
		{
			DataSet ds = EditorApp.Instance.TileSetData;
			if(this.BindingContext[ds, "MetaData"] == null)
			{	
				this.txtDescription.DataBindings.Add("Text", ds, "MetaData.Description");
				this.txtHeight.DataBindings.Add("Text", ds, "MetaData.Height");
				this.txtWidth.DataBindings.Add("Text", ds, "MetaData.Width");
				this.txtName.DataBindings.Add("Text", ds, "MetaData.Name");
			}
		}

		public override void SaveData()
		{
			DataSet ds = EditorApp.Instance.TileSetData;
			this.BindingContext[ds, "MetaData"].EndCurrentEdit();
		}


		#region Windows Forms Designer Generated Code
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TilesetControl));
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.grpSize = new System.Windows.Forms.GroupBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.grpSize.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // grpSize
            // 
            this.grpSize.Controls.Add(this.txtHeight);
            this.grpSize.Controls.Add(this.lblHeight);
            this.grpSize.Controls.Add(this.txtWidth);
            this.grpSize.Controls.Add(this.lblWidth);
            this.grpSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.grpSize, "grpSize");
            this.grpSize.Name = "grpSize";
            this.grpSize.TabStop = false;
            // 
            // txtHeight
            // 
            resources.ApplyResources(this.txtHeight, "txtHeight");
            this.txtHeight.Name = "txtHeight";
            // 
            // lblHeight
            // 
            resources.ApplyResources(this.lblHeight, "lblHeight");
            this.lblHeight.Name = "lblHeight";
            // 
            // txtWidth
            // 
            resources.ApplyResources(this.txtWidth, "txtWidth");
            this.txtWidth.Name = "txtWidth";
            // 
            // lblWidth
            // 
            resources.ApplyResources(this.lblWidth, "lblWidth");
            this.lblWidth.Name = "lblWidth";
            // 
            // lblDescription
            // 
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // txtDescription
            // 
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Name = "txtDescription";
            // 
            // TilesetControl
            // 
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.grpSize);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Name = "TilesetControl";
            resources.ApplyResources(this, "$this");
            this.grpSize.ResumeLayout(false);
            this.grpSize.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
	
		
	}

}