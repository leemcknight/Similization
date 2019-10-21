namespace McKnight.SimilizationRulesEditor
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Windows.Forms;

	/// <summary>
	/// Control used to display and edit the various types of tile images used throughout 
	/// the game.
	/// </summary>
	public class TileControl : EditorUserControl
	{
		private CurrencyManager tileCurrencyManager;
        private System.Windows.Forms.ComboBox cboTileType;
		private System.Windows.Forms.Label lblImage;
		private System.Windows.Forms.Label lblKey;
		private System.Windows.Forms.TextBox txtImageKey;
		private System.Windows.Forms.Button btnImage;
		private System.Windows.Forms.ListBox lstKeys;
		private System.Windows.Forms.Label lblType;
        private PictureBox picBox;
		private DataSet tileTypes;

		/// <summary>
		/// Initializes a new instance of the <see cref="McKnight.SimilizationRulesEditor.TileControl"/> class.
		/// </summary>
		public TileControl()
		{
			InitializeComponent();
			this.tileTypes = new DataSet();
			this.tileTypes.ReadXmlSchema("TileTypes.xsd");
			this.tileTypes.ReadXml("TileTypes.xml");
			this.cboTileType.DataSource = this.tileTypes.Tables["TileType"];
			this.cboTileType.DisplayMember = "TileTypeName";
			this.cboTileType.ValueMember = "TileTypeID";
		}

		#region Windows Designer Generated Code
		private void InitializeComponent()
		{
            this.cboTileType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblImage = new System.Windows.Forms.Label();
            this.lblKey = new System.Windows.Forms.Label();
            this.txtImageKey = new System.Windows.Forms.TextBox();
            this.btnImage = new System.Windows.Forms.Button();
            this.lstKeys = new System.Windows.Forms.ListBox();
            this.picBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cboTileType
            // 
            this.cboTileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTileType.FormattingEnabled = true;
            this.cboTileType.Location = new System.Drawing.Point(112, 8);
            this.cboTileType.Name = "cboTileType";
            this.cboTileType.Size = new System.Drawing.Size(248, 21);
            this.cboTileType.TabIndex = 1;
            this.cboTileType.SelectedIndexChanged += new System.EventHandler(this.cboTileType_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(8, 8);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(104, 16);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "Select a Tile T&ype:";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblImage
            // 
            this.lblImage.Location = new System.Drawing.Point(16, 232);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(40, 16);
            this.lblImage.TabIndex = 5;
            this.lblImage.Text = "Image:";
            // 
            // lblKey
            // 
            this.lblKey.Location = new System.Drawing.Point(16, 208);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(64, 16);
            this.lblKey.TabIndex = 3;
            this.lblKey.Text = "Image Key:";
            // 
            // txtImageKey
            // 
            this.txtImageKey.Location = new System.Drawing.Point(80, 208);
            this.txtImageKey.Name = "txtImageKey";
            this.txtImageKey.Size = new System.Drawing.Size(280, 20);
            this.txtImageKey.TabIndex = 4;
            // 
            // btnImage
            // 
            this.btnImage.Location = new System.Drawing.Point(8, 320);
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(152, 23);
            this.btnImage.TabIndex = 6;
            this.btnImage.Text = "Click To C&hange Image...";
            this.btnImage.Click += new System.EventHandler(this.btnImage_Click);
            // 
            // lstKeys
            // 
            this.lstKeys.FormattingEnabled = true;
            this.lstKeys.Location = new System.Drawing.Point(8, 40);
            this.lstKeys.Name = "lstKeys";
            this.lstKeys.Size = new System.Drawing.Size(352, 160);
            this.lstKeys.TabIndex = 2;
            this.lstKeys.SelectedIndexChanged += new System.EventHandler(this.lstKeys_SelectedIndexChanged);
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(16, 248);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(88, 64);
            this.picBox.TabIndex = 2;
            this.picBox.TabStop = false;
            // 
            // TileControl
            // 
            this.Controls.Add(this.lstKeys);
            this.Controls.Add(this.btnImage);
            this.Controls.Add(this.txtImageKey);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.lblImage);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.cboTileType);
            this.Name = "TileControl";
            this.Size = new System.Drawing.Size(536, 432);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        public override void AddNew()
        {
            this.tileCurrencyManager.AddNew();
        }

        public override void SaveData()
        {
            this.tileCurrencyManager.EndCurrentEdit();
        }

		private void cboTileType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataRowView drv = (DataRowView)this.cboTileType.SelectedItem;
			string tablePath = drv.Row["DataTablePath"].ToString();
			string displayName = drv.Row["DisplayFieldName"].ToString();
			this.lstKeys.DataSource = null;
			this.lstKeys.Items.Clear();
			this.lstKeys.DataSource = EditorApp.Instance.TileSetData;
			this.lstKeys.DisplayMember = tablePath + "." + displayName;
            this.txtImageKey.DataBindings.Clear();
            this.txtImageKey.DataBindings.Add("Text", EditorApp.Instance.TileSetData, tablePath + "." + displayName);
			tileCurrencyManager = (CurrencyManager)this.BindingContext[EditorApp.Instance.TileSetData, tablePath];
		}

		private void lstKeys_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.tileCurrencyManager != null && this.tileCurrencyManager.Position >= 0)
			{
				DataRowView drv = (DataRowView)this.tileCurrencyManager.Current;
                if (!drv.Row.IsNull("TilePath"))
                {
                    Image img = Image.FromFile(drv.Row["TilePath"].ToString());
                    this.picBox.Image = img;
                    this.picBox.Size = img.Size;
                }
                else
                {
                    this.picBox.Image = null;
                }
			}
			else
			{
				this.picBox.Image = null;
			}
			
		}

        private void btnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files (*.png, *.jpg, *.bmp, *.gif) | *.png;*.jpg;*.bmp;*.gif";
            
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.picBox.Image = Image.FromFile(dlg.FileName);
                DataRowView drv = (DataRowView)this.tileCurrencyManager.Current;
                drv.Row["TilePath"] = dlg.FileName;
            }
        }
	}
}