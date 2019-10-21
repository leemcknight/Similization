using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Resources;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace McKnight.SimilizationRulesEditor
{
	/// <summary>
	/// This is the singleton class for the map application object in the 
	/// Similization Rules Editor.
	/// </summary>
	public class EditorApp
	{		
		private MainForm mainForm;
		private static EditorApp instance;
		private EditorItem editorItem;
		private string filePath;

		private EditorApp()
		{
			this.editorItem = EditorItem.None;	
		}

		/// <summary>
		/// Gets the singleton instance of the <c>EditorApp</c> class.
		/// </summary>
		public static EditorApp Instance
		{
			get 
			{
				if(instance == null)				
					instance = new EditorApp();									
				return instance;
			}
		}

		public Collection<EditorUserControl> EditorPages
		{
			get { return this.mainForm.EditorPages; }
		}

        [STAThread]
		public static void Main()
		{
			EditorApp app = EditorApp.Instance;
            app.Start();
		}

		/// <summary>
		/// The type of item that is currently open.
		/// </summary>
		public EditorItem EditorItem
		{
			get { return this.editorItem; }
		}

		/// <summary>
		/// Start the Rules Editor.
		/// </summary>
		public void Start()
		{
			try
			{
				this.mainForm = new MainForm();
                Application.EnableVisualStyles();
				Application.Run(this.mainForm);
			}
			catch(System.Exception ex)
			{
				MessageBox.Show(ex.Message,Application.ProductName,MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Creates a new ruleset with the specified name.
		/// </summary>
		/// <param name="rulesetName"></param>
		/// <param name="path"></param>
		public void CreateRuleset(string rulesetName, string path)
		{
			try
			{
                this.FilePath = path + "\\" + rulesetName + ".ruleset";
				this.rulesetData = new DataSet("Ruleset");
                this.rulesetData.ReadXmlSchema("Ruleset.xsd");			
                DataRow row = this.rulesetData.Tables["MetaData"].NewRow();
                row["Name"] = rulesetName;
                this.rulesetData.Tables["MetaData"].Rows.Add(row);
                row.AcceptChanges();
                row = this.rulesetData.Tables["DefaultRules"].NewRow();
                this.rulesetData.Tables["DefaultRules"].Rows.Add(row);
                row.AcceptChanges();
				this.editorItem = EditorItem.Ruleset;
				OnEditorItemChanged();
			}
			catch(System.Exception ex)
			{
				MessageBox.Show(ex.Message,Application.ProductName,MessageBoxButtons.OK, MessageBoxIcon.Error);
			}			
		}

		/// <summary>
		/// Creates a new tileset with the specified name.
		/// </summary>
		/// <param name="tilesetName"></param>
		/// <param name="path"></param>
		public void CreateTileset(string tilesetName, string path)
		{
			try
			{
				this.tilesetData = new DataSet("Tileset");
				this.tilesetData.ReadXmlSchema("Tileset.xsd");				
				this.editorItem = EditorItem.Tileset;
				OnEditorItemChanged();
			}
			catch(System.Exception ex)
			{
				MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Loads the ruleset from the given path.
		/// </summary>
		/// <param name="rulesetPath"></param>
		public void LoadRuleset(string rulesetPath)
		{
			try
			{
				this.rulesetData = new DataSet("Ruleset");
				this.rulesetData.ReadXmlSchema("Ruleset.xsd");
				this.rulesetData.ReadXml(rulesetPath);
				this.editorItem = EditorItem.Ruleset;
				OnEditorItemChanged();
			}
			catch(System.Exception ex)
			{
				MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		/// <summary>
		/// Loads the tileset from the given path.
		/// </summary>
		/// <param name="tilesetPath"></param>
		public void LoadTileset(string tilesetPath)
		{
			try
			{
				this.tilesetData = new DataSet("Tileset");
				this.tilesetData.ReadXmlSchema("Tileset.xsd");
				this.tilesetData.ReadXml(tilesetPath);
				this.editorItem = EditorItem.Tileset;
				OnEditorItemChanged();
			}
			catch(System.Exception ex)
			{
				MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public EditorUserControl ActiveControl
		{
			get { return this.mainForm.ActivePlugin; }
		}

		/// <summary>
		/// The full path to the current file.
		/// </summary>
		public string FilePath
		{
			get { return this.filePath; }
			set 
			{ 
				this.filePath = value; 

				//get the filename out of the path.
				string[] parts = this.filePath.Split(@"\".ToCharArray());
				string file = parts[parts.GetUpperBound(0)];

				this.mainForm.Text = Application.ProductName + " - " + file;
			}
		}

		private DataSet rulesetData;

		/// <summary>
		/// The <c>System.Data.DataSet</c> containing the ruleset information.
		/// </summary>
		public DataSet RuleSetData
		{
			get { return this.rulesetData; }
		}

		private DataSet tilesetData;

		/// <summary>
		/// The <c>System.Data.DataSet</c> containing the tileset information.
		/// </summary>
		public DataSet TileSetData
		{
			get { return this.tilesetData; }
		}

		#region Events

		private event EventHandler editorItemChanged;


		/// <summary>
		/// Occurs when the <c>EditorItem</c> property changes.
		/// </summary>
		public event EventHandler EditorItemChanged
		{
			add { this.editorItemChanged += value; }

			remove { this.editorItemChanged -= value; }
		}


		private void OnEditorItemChanged()
		{
			if(this.editorItemChanged != null)
			{
				this.editorItemChanged(this, EventArgs.Empty);
			}
		}
		#endregion

	}

	public enum EditorItem
	{
		Ruleset,
		Tileset,
		Map,
		None
	}

	
}
