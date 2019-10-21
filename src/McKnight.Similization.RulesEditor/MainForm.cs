using System;
using System.Drawing;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using McKnight.Similization.Core;

namespace McKnight.SimilizationRulesEditor
{
	/// <summary>
	/// Main Form in the Editor Application
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ImageList imglstTreeview;

		private Collection<EditorUserControl> editorPages;
		private UnitControl unitControl;
		private TechnologyControl technologyControl;
		private ResourceControl resourceControl;
		private TerrainControl terrainControl;
		private ImprovementControl improvementControl;
		private EraControl eraControl;
		private CivilizationControl civilizationControl;
		private GovernmentControl governmentControl;
		private RulesetControl rulesetControl;
		private TilesetControl tilesetControl;
		private EditorUserControl activeControl;
		private TileControl tileControl;
        private StatusStrip statusStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem printToolStripMenuItem;
        private ToolStripMenuItem printPreviewToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem customizeToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem contentsToolStripMenuItem;
        private ToolStripMenuItem indexToolStripMenuItem;
        private ToolStripMenuItem searchToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton newToolStripButton;
        private ToolStripButton openToolStripButton;
        private ToolStripButton saveToolStripButton;
        private ToolStripButton printToolStripButton;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripButton cutToolStripButton;
        private ToolStripButton copyToolStripButton;
        private ToolStripButton pasteToolStripButton;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripButton helpToolStripButton;
        private SplitContainer splitContainer1;
        private TreeView tvwMain;
		
		private const string HELP_FILE = "RulesetEditor.chm";

		/// <summary>
		/// Initializes a new instance of the <c>frmMain</c> class.
		/// </summary>
		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			BuildCommands();
			this.editorPages = new Collection<EditorUserControl>();            
			EditorApp.Instance.EditorItemChanged += new EventHandler(HandleEditorItemChanged);
		
		}

		/// <summary>
		/// Clean up any ResourceType being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		private void HandleEditorItemChanged(object sender, System.EventArgs e)
		{
			this.tvwMain.Nodes.Clear();
			this.splitContainer1.Panel2.Controls.Clear();

			EditorItem item = EditorApp.Instance.EditorItem;

			switch(item)
			{
				case EditorItem.None:
					break;
				case EditorItem.Ruleset:
					LoadRulesetNodes();
					break;
				case EditorItem.Tileset:
					LoadTilesetNodes();
					break;
				case EditorItem.Map:
					break;
			}
		}


		private void BuildCommands()
		{
            openToolStripButton.Tag = new OpenCommand();
            openToolStripMenuItem.Tag = openToolStripButton.Tag;
            saveToolStripButton.Tag = new SaveCommand();
            saveToolStripMenuItem.Tag = saveToolStripButton.Tag;
            newToolStripButton.Tag = new AddNewItemCommand();
            newToolStripMenuItem.Tag = newToolStripButton.Tag;	
		}



		private void LoadRulesetNodes()
		{
			this.tvwMain.Nodes.AddRange(new TreeNode[] {
														new TreeNode("Ruleset", 2, 2, new TreeNode[] {
																											new TreeNode("Units", 0, 0),
																											new TreeNode("Technologies", 0, 0),
																											new TreeNode("Resources", 0, 0),
																											new TreeNode("Eras", 0, 0),
																											new TreeNode("Maps", 0, 0),
																											new TreeNode("Improvements", 0, 0),
																											new TreeNode("Terrains", 0, 0),
																											new TreeNode("Governments", 0, 0),
																											new TreeNode("Civilizations", 0, 0)})});
		}


		private void LoadTilesetNodes()
		{
			this.tvwMain.Nodes.AddRange(new TreeNode[] {
														new TreeNode("Tileset", 2, 2, new TreeNode[] {
																										new TreeNode("Tile Images", 0, 1),
																									})});
		}

		public EditorUserControl ActivePlugin
		{
			get { return this.activeControl; }
		}

		/// <summary>
		/// Loads the new editor user control onto the main form.
		/// </summary>
		/// <param name="newControl"></param>
		public void SwapControl(EditorUserControl newControl)
		{
			if(this.activeControl == newControl)
			{
				return;
			}

			if(this.activeControl != null)
			{
				this.activeControl.Hide();
			}

			if( newControl != null )
			{
				if(!this.Controls.Contains(newControl))
				{
					this.Controls.Add(newControl);                 
                    newControl.Parent = this.splitContainer1.Panel2;
					newControl.Dock = DockStyle.Fill;
				}
				this.activeControl = newControl;
				this.activeControl.Show();
				this.activeControl.ShowData();
			}

			if(!editorPages.Contains(newControl))
			{
				editorPages.Add(newControl);
			}
		}


		public Collection<EditorUserControl> EditorPages
		{
			get { return this.editorPages; }
		}



		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imglstTreeview = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvwMain = new System.Windows.Forms.TreeView();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imglstTreeview
            // 
            this.imglstTreeview.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstTreeview.ImageStream")));
            this.imglstTreeview.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstTreeview.Images.SetKeyName(0, "shell32.004.ico");
            this.imglstTreeview.Images.SetKeyName(1, "shell32.164.ico");
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // newToolStripMenuItem
            // 
            resources.ApplyResources(this.newToolStripMenuItem, "newToolStripMenuItem");
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            // 
            // openToolStripMenuItem
            // 
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            resources.ApplyResources(this.toolStripSeparator, "toolStripSeparator");
            // 
            // saveToolStripMenuItem
            // 
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // printToolStripMenuItem
            // 
            resources.ApplyResources(this.printToolStripMenuItem, "printToolStripMenuItem");
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            // 
            // printPreviewToolStripMenuItem
            // 
            resources.ApplyResources(this.printPreviewToolStripMenuItem, "printPreviewToolStripMenuItem");
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator4,
            this.selectAllToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            resources.ApplyResources(this.undoToolStripMenuItem, "undoToolStripMenuItem");
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            resources.ApplyResources(this.redoToolStripMenuItem, "redoToolStripMenuItem");
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // cutToolStripMenuItem
            // 
            resources.ApplyResources(this.cutToolStripMenuItem, "cutToolStripMenuItem");
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            // 
            // copyToolStripMenuItem
            // 
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            // 
            // pasteToolStripMenuItem
            // 
            resources.ApplyResources(this.pasteToolStripMenuItem, "pasteToolStripMenuItem");
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            resources.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            // 
            // customizeToolStripMenuItem
            // 
            this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            resources.ApplyResources(this.customizeToolStripMenuItem, "customizeToolStripMenuItem");
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            resources.ApplyResources(this.contentsToolStripMenuItem, "contentsToolStripMenuItem");
            this.contentsToolStripMenuItem.Click += new System.EventHandler(this.contentsToolStripMenuItem_Click);
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            resources.ApplyResources(this.indexToolStripMenuItem, "indexToolStripMenuItem");
            this.indexToolStripMenuItem.Click += new System.EventHandler(this.indexToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            resources.ApplyResources(this.searchToolStripMenuItem, "searchToolStripMenuItem");
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");            
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.printToolStripButton,
            this.toolStripSeparator6,
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripSeparator7,
            this.helpToolStripButton});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.newToolStripButton, "newToolStripButton");
            this.newToolStripButton.Name = "newToolStripButton";            
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.openToolStripButton, "openToolStripButton");
            this.openToolStripButton.Name = "openToolStripButton";            
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.saveToolStripButton, "saveToolStripButton");
            this.saveToolStripButton.Name = "saveToolStripButton";            
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.printToolStripButton, "printToolStripButton");
            this.printToolStripButton.Name = "printToolStripButton";            
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");            
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.cutToolStripButton, "cutToolStripButton");
            this.cutToolStripButton.Name = "cutToolStripButton";            
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.copyToolStripButton, "copyToolStripButton");
            this.copyToolStripButton.Name = "copyToolStripButton";            
            // 
            // pasteToolStripButton
            // 
            this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.pasteToolStripButton, "pasteToolStripButton");
            this.pasteToolStripButton.Name = "pasteToolStripButton";            
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");            
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.helpToolStripButton, "helpToolStripButton");
            this.helpToolStripButton.Name = "helpToolStripButton";            
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvwMain);
            // 
            // tvwMain
            // 
            resources.ApplyResources(this.tvwMain, "tvwMain");
            this.tvwMain.ImageList = this.imglstTreeview;
            this.tvwMain.Name = "tvwMain";
            this.tvwMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwMain_AfterSelect);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void _openRuleSetMenuItem_Click(object sender, System.EventArgs e)
		{
			frmRuleSetConnect connectForm;

			connectForm = new frmRuleSetConnect();
			connectForm.ShowDialog(this);
		}

		private void _openMapMenuItem_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.DefaultExt = "*.map";
			dlg.Filter = "Map Files (*.map) | *.map";
			dlg.InitialDirectory = EditorApp.Instance.FilePath + "maps";
			if(dlg.ShowDialog() == DialogResult.OK)
			{
				string path = dlg.FileName;
				
			}
		}

		private void tvwMain_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			switch(e.Node.Text)
			{
                case "Ruleset":
                    if (this.rulesetControl == null)
                        this.rulesetControl = new RulesetControl();
                    SwapControl(this.rulesetControl);
                    break;
				case "Units":
					if(this.unitControl == null)
						this.unitControl = new UnitControl();
					SwapControl(this.unitControl);
					break;
				case "Technologies":
					if(this.technologyControl == null)
						this.technologyControl = new TechnologyControl();
					SwapControl(this.technologyControl);
					break;
				case "Resource Tiles":
					
				case "Resources":
					if(this.resourceControl == null)
						this.resourceControl = new ResourceControl();
					SwapControl(this.resourceControl);
					break;
				case "Eras":
					if(this.eraControl == null)
						this.eraControl = new EraControl();
					SwapControl(this.eraControl);
					break;
				case "Terrains":
					if(this.terrainControl == null)
						this.terrainControl = new TerrainControl();
					SwapControl(this.terrainControl);
					break;
				case "Improvements":
					if(this.improvementControl == null)
						this.improvementControl = new ImprovementControl();
					SwapControl(this.improvementControl);
					break;
				case "Civilizations":
					if(this.civilizationControl == null)
						this.civilizationControl = new CivilizationControl();
					SwapControl(this.civilizationControl);
					break;
				case "Governments":
					if(this.governmentControl == null)
						this.governmentControl = new GovernmentControl();
					SwapControl(this.governmentControl);
					break;
				case "Tile Images":
					if(this.tileControl == null)
						this.tileControl = new TileControl();
					SwapControl(this.tileControl);
					break;
				default:
					MessageBox.Show("Not Implemented.");
					break;
			}
		}

        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelpIndex(this, Application.StartupPath + @"\" + HELP_FILE);
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, Application.StartupPath + @"\" + HELP_FILE);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            object o = e.ClickedItem.Tag;
            Command cmd = (Command)o;
            cmd.Invoke();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            object o = e.ClickedItem.Tag;
            Command cmd = o as Command;
            if (cmd != null)
                cmd.Invoke();
        }

	}
}
