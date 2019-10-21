using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using McKnight.Similization.Client;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// Summary description for TerrainDialog.
	/// </summary>
	public class TerrainDialog : System.Windows.Forms.Form
	{
		private GridCell cell;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.GroupBox grpTerrain;
		private System.Windows.Forms.Label lblGoldAmount;
		private System.Windows.Forms.Label lblShieldsAmount;
		private System.Windows.Forms.Label lblFoodAmount;
		private System.Windows.Forms.Label lblGold;
		private System.Windows.Forms.Label lblShields;
		private System.Windows.Forms.Label lblFood;
		private System.Windows.Forms.PictureBox pbTerrain;
		private System.Windows.Forms.Label lblTerrain;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainDialog"/> class.
        /// </summary>
        /// <param name="cell"></param>
		public TerrainDialog(GridCell cell)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.cell = cell;
			DataBind();
		}




		private void DataBind()
		{
			ClientApplication client = ClientApplication.Instance;
			this.lblTerrain.DataBindings.Add("Text", cell.Terrain, "Name");
			this.lblFoodAmount.DataBindings.Add("Text", cell, "FoodUnits");
			this.lblGoldAmount.DataBindings.Add("Text", cell, "GoldPerTurn");
			this.lblShieldsAmount.DataBindings.Add("Text", cell, "Shields");
			this.pbTerrain.Image = (Image)client.Tileset.TerrainTiles[cell.Terrain.Name].TileImage;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TerrainDialog));
			this.btnOK = new System.Windows.Forms.Button();
			this.grpTerrain = new System.Windows.Forms.GroupBox();
			this.lblGoldAmount = new System.Windows.Forms.Label();
			this.lblShieldsAmount = new System.Windows.Forms.Label();
			this.lblFoodAmount = new System.Windows.Forms.Label();
			this.lblGold = new System.Windows.Forms.Label();
			this.lblShields = new System.Windows.Forms.Label();
			this.lblFood = new System.Windows.Forms.Label();
			this.lblTerrain = new System.Windows.Forms.Label();
			this.pbTerrain = new System.Windows.Forms.PictureBox();
			this.grpTerrain.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.AccessibleDescription = ((string)(resources.GetObject("btnOK.AccessibleDescription")));
			this.btnOK.AccessibleName = ((string)(resources.GetObject("btnOK.AccessibleName")));
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnOK.Anchor")));
			this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnOK.Dock")));
			this.btnOK.Enabled = ((bool)(resources.GetObject("btnOK.Enabled")));
			this.btnOK.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnOK.FlatStyle")));
			this.btnOK.Font = ((System.Drawing.Font)(resources.GetObject("btnOK.Font")));
			this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
			this.btnOK.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOK.ImageAlign")));
			this.btnOK.ImageIndex = ((int)(resources.GetObject("btnOK.ImageIndex")));
			this.btnOK.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnOK.ImeMode")));
			this.btnOK.Location = ((System.Drawing.Point)(resources.GetObject("btnOK.Location")));
			this.btnOK.Name = "btnOK";
			this.btnOK.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnOK.RightToLeft")));
			this.btnOK.Size = ((System.Drawing.Size)(resources.GetObject("btnOK.Size")));
			this.btnOK.TabIndex = ((int)(resources.GetObject("btnOK.TabIndex")));
			this.btnOK.Text = resources.GetString("btnOK.Text");
			this.btnOK.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOK.TextAlign")));
			this.btnOK.Visible = ((bool)(resources.GetObject("btnOK.Visible")));
			// 
			// grpTerrain
			// 
			this.grpTerrain.AccessibleDescription = ((string)(resources.GetObject("grpTerrain.AccessibleDescription")));
			this.grpTerrain.AccessibleName = ((string)(resources.GetObject("grpTerrain.AccessibleName")));
			this.grpTerrain.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpTerrain.Anchor")));
			this.grpTerrain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpTerrain.BackgroundImage")));
			this.grpTerrain.Controls.AddRange(new System.Windows.Forms.Control[] {
																					 this.lblGoldAmount,
																					 this.lblShieldsAmount,
																					 this.lblFoodAmount,
																					 this.lblGold,
																					 this.lblShields,
																					 this.lblFood,
																					 this.lblTerrain});
			this.grpTerrain.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpTerrain.Dock")));
			this.grpTerrain.Enabled = ((bool)(resources.GetObject("grpTerrain.Enabled")));
			this.grpTerrain.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpTerrain.Font = ((System.Drawing.Font)(resources.GetObject("grpTerrain.Font")));
			this.grpTerrain.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpTerrain.ImeMode")));
			this.grpTerrain.Location = ((System.Drawing.Point)(resources.GetObject("grpTerrain.Location")));
			this.grpTerrain.Name = "grpTerrain";
			this.grpTerrain.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpTerrain.RightToLeft")));
			this.grpTerrain.Size = ((System.Drawing.Size)(resources.GetObject("grpTerrain.Size")));
			this.grpTerrain.TabIndex = ((int)(resources.GetObject("grpTerrain.TabIndex")));
			this.grpTerrain.TabStop = false;
			this.grpTerrain.Text = resources.GetString("grpTerrain.Text");
			this.grpTerrain.Visible = ((bool)(resources.GetObject("grpTerrain.Visible")));
			// 
			// lblGoldAmount
			// 
			this.lblGoldAmount.AccessibleDescription = ((string)(resources.GetObject("lblGoldAmount.AccessibleDescription")));
			this.lblGoldAmount.AccessibleName = ((string)(resources.GetObject("lblGoldAmount.AccessibleName")));
			this.lblGoldAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblGoldAmount.Anchor")));
			this.lblGoldAmount.AutoSize = ((bool)(resources.GetObject("lblGoldAmount.AutoSize")));
			this.lblGoldAmount.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblGoldAmount.Dock")));
			this.lblGoldAmount.Enabled = ((bool)(resources.GetObject("lblGoldAmount.Enabled")));
			this.lblGoldAmount.Font = ((System.Drawing.Font)(resources.GetObject("lblGoldAmount.Font")));
			this.lblGoldAmount.Image = ((System.Drawing.Image)(resources.GetObject("lblGoldAmount.Image")));
			this.lblGoldAmount.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGoldAmount.ImageAlign")));
			this.lblGoldAmount.ImageIndex = ((int)(resources.GetObject("lblGoldAmount.ImageIndex")));
			this.lblGoldAmount.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblGoldAmount.ImeMode")));
			this.lblGoldAmount.Location = ((System.Drawing.Point)(resources.GetObject("lblGoldAmount.Location")));
			this.lblGoldAmount.Name = "lblGoldAmount";
			this.lblGoldAmount.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblGoldAmount.RightToLeft")));
			this.lblGoldAmount.Size = ((System.Drawing.Size)(resources.GetObject("lblGoldAmount.Size")));
			this.lblGoldAmount.TabIndex = ((int)(resources.GetObject("lblGoldAmount.TabIndex")));
			this.lblGoldAmount.Text = resources.GetString("lblGoldAmount.Text");
			this.lblGoldAmount.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGoldAmount.TextAlign")));
			this.lblGoldAmount.Visible = ((bool)(resources.GetObject("lblGoldAmount.Visible")));
			// 
			// lblShieldsAmount
			// 
			this.lblShieldsAmount.AccessibleDescription = ((string)(resources.GetObject("lblShieldsAmount.AccessibleDescription")));
			this.lblShieldsAmount.AccessibleName = ((string)(resources.GetObject("lblShieldsAmount.AccessibleName")));
			this.lblShieldsAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblShieldsAmount.Anchor")));
			this.lblShieldsAmount.AutoSize = ((bool)(resources.GetObject("lblShieldsAmount.AutoSize")));
			this.lblShieldsAmount.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblShieldsAmount.Dock")));
			this.lblShieldsAmount.Enabled = ((bool)(resources.GetObject("lblShieldsAmount.Enabled")));
			this.lblShieldsAmount.Font = ((System.Drawing.Font)(resources.GetObject("lblShieldsAmount.Font")));
			this.lblShieldsAmount.Image = ((System.Drawing.Image)(resources.GetObject("lblShieldsAmount.Image")));
			this.lblShieldsAmount.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblShieldsAmount.ImageAlign")));
			this.lblShieldsAmount.ImageIndex = ((int)(resources.GetObject("lblShieldsAmount.ImageIndex")));
			this.lblShieldsAmount.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblShieldsAmount.ImeMode")));
			this.lblShieldsAmount.Location = ((System.Drawing.Point)(resources.GetObject("lblShieldsAmount.Location")));
			this.lblShieldsAmount.Name = "lblShieldsAmount";
			this.lblShieldsAmount.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblShieldsAmount.RightToLeft")));
			this.lblShieldsAmount.Size = ((System.Drawing.Size)(resources.GetObject("lblShieldsAmount.Size")));
			this.lblShieldsAmount.TabIndex = ((int)(resources.GetObject("lblShieldsAmount.TabIndex")));
			this.lblShieldsAmount.Text = resources.GetString("lblShieldsAmount.Text");
			this.lblShieldsAmount.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblShieldsAmount.TextAlign")));
			this.lblShieldsAmount.Visible = ((bool)(resources.GetObject("lblShieldsAmount.Visible")));
			// 
			// lblFoodAmount
			// 
			this.lblFoodAmount.AccessibleDescription = ((string)(resources.GetObject("lblFoodAmount.AccessibleDescription")));
			this.lblFoodAmount.AccessibleName = ((string)(resources.GetObject("lblFoodAmount.AccessibleName")));
			this.lblFoodAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFoodAmount.Anchor")));
			this.lblFoodAmount.AutoSize = ((bool)(resources.GetObject("lblFoodAmount.AutoSize")));
			this.lblFoodAmount.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFoodAmount.Dock")));
			this.lblFoodAmount.Enabled = ((bool)(resources.GetObject("lblFoodAmount.Enabled")));
			this.lblFoodAmount.Font = ((System.Drawing.Font)(resources.GetObject("lblFoodAmount.Font")));
			this.lblFoodAmount.Image = ((System.Drawing.Image)(resources.GetObject("lblFoodAmount.Image")));
			this.lblFoodAmount.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFoodAmount.ImageAlign")));
			this.lblFoodAmount.ImageIndex = ((int)(resources.GetObject("lblFoodAmount.ImageIndex")));
			this.lblFoodAmount.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFoodAmount.ImeMode")));
			this.lblFoodAmount.Location = ((System.Drawing.Point)(resources.GetObject("lblFoodAmount.Location")));
			this.lblFoodAmount.Name = "lblFoodAmount";
			this.lblFoodAmount.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFoodAmount.RightToLeft")));
			this.lblFoodAmount.Size = ((System.Drawing.Size)(resources.GetObject("lblFoodAmount.Size")));
			this.lblFoodAmount.TabIndex = ((int)(resources.GetObject("lblFoodAmount.TabIndex")));
			this.lblFoodAmount.Text = resources.GetString("lblFoodAmount.Text");
			this.lblFoodAmount.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFoodAmount.TextAlign")));
			this.lblFoodAmount.Visible = ((bool)(resources.GetObject("lblFoodAmount.Visible")));
			// 
			// lblGold
			// 
			this.lblGold.AccessibleDescription = ((string)(resources.GetObject("lblGold.AccessibleDescription")));
			this.lblGold.AccessibleName = ((string)(resources.GetObject("lblGold.AccessibleName")));
			this.lblGold.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblGold.Anchor")));
			this.lblGold.AutoSize = ((bool)(resources.GetObject("lblGold.AutoSize")));
			this.lblGold.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblGold.Dock")));
			this.lblGold.Enabled = ((bool)(resources.GetObject("lblGold.Enabled")));
			this.lblGold.Font = ((System.Drawing.Font)(resources.GetObject("lblGold.Font")));
			this.lblGold.Image = ((System.Drawing.Image)(resources.GetObject("lblGold.Image")));
			this.lblGold.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGold.ImageAlign")));
			this.lblGold.ImageIndex = ((int)(resources.GetObject("lblGold.ImageIndex")));
			this.lblGold.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblGold.ImeMode")));
			this.lblGold.Location = ((System.Drawing.Point)(resources.GetObject("lblGold.Location")));
			this.lblGold.Name = "lblGold";
			this.lblGold.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblGold.RightToLeft")));
			this.lblGold.Size = ((System.Drawing.Size)(resources.GetObject("lblGold.Size")));
			this.lblGold.TabIndex = ((int)(resources.GetObject("lblGold.TabIndex")));
			this.lblGold.Text = resources.GetString("lblGold.Text");
			this.lblGold.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGold.TextAlign")));
			this.lblGold.Visible = ((bool)(resources.GetObject("lblGold.Visible")));
			// 
			// lblShields
			// 
			this.lblShields.AccessibleDescription = ((string)(resources.GetObject("lblShields.AccessibleDescription")));
			this.lblShields.AccessibleName = ((string)(resources.GetObject("lblShields.AccessibleName")));
			this.lblShields.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblShields.Anchor")));
			this.lblShields.AutoSize = ((bool)(resources.GetObject("lblShields.AutoSize")));
			this.lblShields.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblShields.Dock")));
			this.lblShields.Enabled = ((bool)(resources.GetObject("lblShields.Enabled")));
			this.lblShields.Font = ((System.Drawing.Font)(resources.GetObject("lblShields.Font")));
			this.lblShields.Image = ((System.Drawing.Image)(resources.GetObject("lblShields.Image")));
			this.lblShields.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblShields.ImageAlign")));
			this.lblShields.ImageIndex = ((int)(resources.GetObject("lblShields.ImageIndex")));
			this.lblShields.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblShields.ImeMode")));
			this.lblShields.Location = ((System.Drawing.Point)(resources.GetObject("lblShields.Location")));
			this.lblShields.Name = "lblShields";
			this.lblShields.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblShields.RightToLeft")));
			this.lblShields.Size = ((System.Drawing.Size)(resources.GetObject("lblShields.Size")));
			this.lblShields.TabIndex = ((int)(resources.GetObject("lblShields.TabIndex")));
			this.lblShields.Text = resources.GetString("lblShields.Text");
			this.lblShields.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblShields.TextAlign")));
			this.lblShields.Visible = ((bool)(resources.GetObject("lblShields.Visible")));
			// 
			// lblFood
			// 
			this.lblFood.AccessibleDescription = ((string)(resources.GetObject("lblFood.AccessibleDescription")));
			this.lblFood.AccessibleName = ((string)(resources.GetObject("lblFood.AccessibleName")));
			this.lblFood.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFood.Anchor")));
			this.lblFood.AutoSize = ((bool)(resources.GetObject("lblFood.AutoSize")));
			this.lblFood.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFood.Dock")));
			this.lblFood.Enabled = ((bool)(resources.GetObject("lblFood.Enabled")));
			this.lblFood.Font = ((System.Drawing.Font)(resources.GetObject("lblFood.Font")));
			this.lblFood.Image = ((System.Drawing.Image)(resources.GetObject("lblFood.Image")));
			this.lblFood.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFood.ImageAlign")));
			this.lblFood.ImageIndex = ((int)(resources.GetObject("lblFood.ImageIndex")));
			this.lblFood.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFood.ImeMode")));
			this.lblFood.Location = ((System.Drawing.Point)(resources.GetObject("lblFood.Location")));
			this.lblFood.Name = "lblFood";
			this.lblFood.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFood.RightToLeft")));
			this.lblFood.Size = ((System.Drawing.Size)(resources.GetObject("lblFood.Size")));
			this.lblFood.TabIndex = ((int)(resources.GetObject("lblFood.TabIndex")));
			this.lblFood.Text = resources.GetString("lblFood.Text");
			this.lblFood.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFood.TextAlign")));
			this.lblFood.Visible = ((bool)(resources.GetObject("lblFood.Visible")));
			// 
			// lblTerrain
			// 
			this.lblTerrain.AccessibleDescription = ((string)(resources.GetObject("lblTerrain.AccessibleDescription")));
			this.lblTerrain.AccessibleName = ((string)(resources.GetObject("lblTerrain.AccessibleName")));
			this.lblTerrain.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblTerrain.Anchor")));
			this.lblTerrain.AutoSize = ((bool)(resources.GetObject("lblTerrain.AutoSize")));
			this.lblTerrain.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblTerrain.Dock")));
			this.lblTerrain.Enabled = ((bool)(resources.GetObject("lblTerrain.Enabled")));
			this.lblTerrain.Font = ((System.Drawing.Font)(resources.GetObject("lblTerrain.Font")));
			this.lblTerrain.Image = ((System.Drawing.Image)(resources.GetObject("lblTerrain.Image")));
			this.lblTerrain.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTerrain.ImageAlign")));
			this.lblTerrain.ImageIndex = ((int)(resources.GetObject("lblTerrain.ImageIndex")));
			this.lblTerrain.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblTerrain.ImeMode")));
			this.lblTerrain.Location = ((System.Drawing.Point)(resources.GetObject("lblTerrain.Location")));
			this.lblTerrain.Name = "lblTerrain";
			this.lblTerrain.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblTerrain.RightToLeft")));
			this.lblTerrain.Size = ((System.Drawing.Size)(resources.GetObject("lblTerrain.Size")));
			this.lblTerrain.TabIndex = ((int)(resources.GetObject("lblTerrain.TabIndex")));
			this.lblTerrain.Text = resources.GetString("lblTerrain.Text");
			this.lblTerrain.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTerrain.TextAlign")));
			this.lblTerrain.Visible = ((bool)(resources.GetObject("lblTerrain.Visible")));
			// 
			// pbTerrain
			// 
			this.pbTerrain.AccessibleDescription = ((string)(resources.GetObject("pbTerrain.AccessibleDescription")));
			this.pbTerrain.AccessibleName = ((string)(resources.GetObject("pbTerrain.AccessibleName")));
			this.pbTerrain.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pbTerrain.Anchor")));
			this.pbTerrain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbTerrain.BackgroundImage")));
			this.pbTerrain.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pbTerrain.Dock")));
			this.pbTerrain.Enabled = ((bool)(resources.GetObject("pbTerrain.Enabled")));
			this.pbTerrain.Font = ((System.Drawing.Font)(resources.GetObject("pbTerrain.Font")));
			this.pbTerrain.Image = ((System.Drawing.Image)(resources.GetObject("pbTerrain.Image")));
			this.pbTerrain.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pbTerrain.ImeMode")));
			this.pbTerrain.Location = ((System.Drawing.Point)(resources.GetObject("pbTerrain.Location")));
			this.pbTerrain.Name = "pbTerrain";
			this.pbTerrain.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pbTerrain.RightToLeft")));
			this.pbTerrain.Size = ((System.Drawing.Size)(resources.GetObject("pbTerrain.Size")));
			this.pbTerrain.SizeMode = ((System.Windows.Forms.PictureBoxSizeMode)(resources.GetObject("pbTerrain.SizeMode")));
			this.pbTerrain.TabIndex = ((int)(resources.GetObject("pbTerrain.TabIndex")));
			this.pbTerrain.TabStop = false;
			this.pbTerrain.Text = resources.GetString("pbTerrain.Text");
			this.pbTerrain.Visible = ((bool)(resources.GetObject("pbTerrain.Visible")));
			// 
			// TerrainDialog
			// 
			this.AcceptButton = this.btnOK;
			this.AccessibleDescription = ((string)(resources.GetObject("$this.AccessibleDescription")));
			this.AccessibleName = ((string)(resources.GetObject("$this.AccessibleName")));
			this.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("$this.Anchor")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.CancelButton = this.btnOK;
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pbTerrain,
																		  this.grpTerrain,
																		  this.btnOK});
			this.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("$this.Dock")));
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "TerrainDialog";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Visible = ((bool)(resources.GetObject("$this.Visible")));
			this.grpTerrain.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
