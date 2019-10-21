using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using McKnight.Similization.Server;
using McKnight.Similization.Client;
using System.Collections.ObjectModel;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// Windows Forms implementation of the <see cref="McKnight.Similization.Client.IDiplomaticTiePicker"/> interface.
	/// </summary>
	public class DiplomaticTiePicker : System.Windows.Forms.Form, IDiplomaticTiePicker
    {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        private Button btnOK;
        private Button btnCancel;
        private Panel panel1;
        private Label label1;
        private Label label2;
        private Label lblTitle;
        private PictureBox pictureBox1;
        private ListView lvwTies;
        private Label lblTies;
        private ColumnHeader hdrCountry;
        private ColumnHeader hdrState;
        private ColumnHeader hdrAttitude;
        private Label lblPickerTitle;
        private bool initialized;

		/// <summary>
		/// Initializes a new instance of the <c>NegotiationPicker</c> class.
		/// </summary>
		public DiplomaticTiePicker()
		{
			//
			// Required for Windows Form Designer support
			//
            InitializeComponent();
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
		/// The diplomatic tie that was chosen to negotiate with.
		/// </summary>
		public DiplomaticTie DiplomaticTie
		{
			get 
            {
                if (this.lvwTies.SelectedItems.Count == 0)
                    return null;
                return (DiplomaticTie)lvwTies.SelectedItems[0].Tag; 
            }
		}

        /// <summary>
        /// Windows Forms implementation of the <i>InitializePicker</i> method.
        /// </summary>
        /// <param name="ties"></param>
        public void InitializePicker(Collection<DiplomaticTie> ties)
        {
            if (ties == null)
                ties = ClientApplication.Instance.Player.DiplomaticTies;

            foreach (DiplomaticTie tie in ties)
            {
                ListViewItem lvi = new ListViewItem(tie.ForeignCountry.Name);
                string key = tie.DiplomaticState == DiplomaticState.War ? "atWar" : "atPeace";
                lvi.SubItems.Add(ClientResources.GetString(key));
                lvwTies.Items.Add(lvi);
            }
            this.initialized = true;
        }

		/// <summary>
		/// Shows the control.
		/// </summary>
		public void ShowSimilizationControl()
		{
            if (!this.initialized)
                InitializePicker(null);
			this.ShowDialog();
		}

        /// <summary>
        /// Windows Forms Client Implementation of the <i>PickerTitle</i> property.
        /// </summary>
        public string PickerTitle
        {
            get
            {
                return this.label1.Text;
            }
            set
            {
                this.label1.Text = value;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.lvwTies.SelectedItems.Count == 0)
            {
                string text = ClientResources.GetString("diplomaticTiePicker_noTieChosen");
                MessageBox.Show(
                    text, 
                    ClientResources.GetString(StringKey.GameTitle), 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign
                    );
                this.DialogResult = DialogResult.None;
            }
        }

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiplomaticTiePicker));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPickerTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lvwTies = new System.Windows.Forms.ListView();
            this.hdrCountry = new System.Windows.Forms.ColumnHeader();
            this.hdrState = new System.Windows.Forms.ColumnHeader();
            this.hdrAttitude = new System.Windows.Forms.ColumnHeader();
            this.lblTies = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblPickerTitle);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // lblPickerTitle
            // 
            resources.ApplyResources(this.lblPickerTitle, "lblPickerTitle");
            this.lblPickerTitle.Name = "lblPickerTitle";
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = McKnight.Similization.Client.WindowsForms.Properties.Resources.bullet_square_blue;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lvwTies
            // 
            this.lvwTies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrCountry,
            this.hdrState,
            this.hdrAttitude});
            this.lvwTies.FullRowSelect = true;
            resources.ApplyResources(this.lvwTies, "lvwTies");
            this.lvwTies.MultiSelect = false;
            this.lvwTies.Name = "lvwTies";
            this.lvwTies.View = System.Windows.Forms.View.Details;
            // 
            // hdrCountry
            // 
            resources.ApplyResources(this.hdrCountry, "hdrCountry");
            // 
            // hdrState
            // 
            resources.ApplyResources(this.hdrState, "hdrState");
            // 
            // hdrAttitude
            // 
            resources.ApplyResources(this.hdrAttitude, "hdrAttitude");
            // 
            // lblTies
            // 
            resources.ApplyResources(this.lblTies, "lblTies");
            this.lblTies.Name = "lblTies";
            // 
            // DiplomaticTiePicker
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lblTies);
            this.Controls.Add(this.lvwTies);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DiplomaticTiePicker";
            this.ShowInTaskbar = false;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
	}
}
