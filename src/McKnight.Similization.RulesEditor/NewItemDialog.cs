namespace McKnight.SimilizationRulesEditor
{
	using System;
	using System.Windows.Forms;    


	public class NewItemDialog : Form
	{
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblPath;
		private System.Windows.Forms.TextBox txtLocation;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Label lblSplitter;
		private System.ComponentModel.IContainer components;
        private EditorItem item = EditorItem.None;
        string path;
        string fileName;
	
		public NewItemDialog()
		{
			InitializeComponent();
            this.NewItemLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            this.NewItemName = "Item1";
		}

		public string NewItemLocation
		{
			get { return this.path; }
            set 
            { 
                this.path = value;
                this.txtLocation.Text = this.path;
            }
		}

		public string NewItemName
		{
			get { return this.fileName; }
            set { 
                this.fileName = value;
                this.txtName.Text = this.fileName; 
            }
		}
		
		public EditorItem Item
		{
			get { return this.item; }
		}


		#region Windows Forms Designer Generated Code
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewItemDialog));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblSplitter = new System.Windows.Forms.Label();
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
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "shell32.164.ico");
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listView1.Items"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listView1.Items1"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listView1.Items2")))});
            this.listView1.LargeImageList = this.imageList;
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.Name = "listView1";
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
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
            // lblPath
            // 
            resources.ApplyResources(this.lblPath, "lblPath");
            this.lblPath.Name = "lblPath";
            // 
            // txtLocation
            // 
            resources.ApplyResources(this.txtLocation, "txtLocation");
            this.txtLocation.Name = "txtLocation";
            // 
            // lblDescription
            // 
            this.lblDescription.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // btnBrowse
            // 
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblSplitter
            // 
            this.lblSplitter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lblSplitter, "lblSplitter");
            this.lblSplitter.Name = "lblSplitter";
            // 
            // NewItemDialog
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.lblSplitter);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewItemDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowNewFolderButton = true;            
            DialogResult dr = dlg.ShowDialog();
            if (dr == DialogResult.OK)            
                this.NewItemLocation = dlg.SelectedPath;            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
            {
                this.item = EditorItem.None;
                return;
            }
            switch(listView1.SelectedIndices[0])
            {
                case 0:
                    this.item = EditorItem.Tileset;
                    break;
                case 1:
                    this.item = EditorItem.Ruleset;
                    break;
                case 2:
                    this.item = EditorItem.Map;
                    break;
            }
        }
	}
}