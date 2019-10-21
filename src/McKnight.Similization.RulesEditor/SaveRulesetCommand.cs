using System;
using System.Data;
using System.Xml;
using System.Windows.Forms;
using McKnight.Similization.Core;

namespace McKnight.SimilizationRulesEditor
{
	public class SaveCommand : Command
	{
		public override void Invoke()
		{
			EditorApp app = EditorApp.Instance;
			switch(app.EditorItem)
			{
				case EditorItem.Map:
					SaveMap();
					break;
				case EditorItem.Ruleset:
					SaveRuleset();
					break;
				case EditorItem.Tileset:
					SaveTileset();
					break;
			}
		}


		//Save the ruleset dataset to disk.  Leverages the fact that datasets can be
		//written straight to disk with the WriteXml() method.
		private void SaveRuleset()
		{
			EditorApp app = EditorApp.Instance;
			string fileName = app.FilePath;
			if(fileName == null || fileName.Length == 0)
			{
				SaveFileDialog dlg = new SaveFileDialog();
				dlg.Filter = "Ruleset Files (*.ruleset) | *.ruleset";

				if(dlg.ShowDialog() == DialogResult.OK)
				{
					fileName = dlg.FileName;
					app.FilePath = fileName;
				}
				else
				{
					return;
				}
			}

			foreach(EditorUserControl page in app.EditorPages)
			{
				page.SaveData();
			}
			
			app.RuleSetData.AcceptChanges();
			app.RuleSetData.WriteXml(fileName);
		}

		private void SaveTileset()
		{
			EditorApp app = EditorApp.Instance;
			string fileName = app.FilePath;
			if(fileName == null || fileName.Length == 0)
			{
				SaveFileDialog dlg = new SaveFileDialog();
				dlg.Filter = "Tileset Files (*.tileset) | *.tileset";

				if(dlg.ShowDialog() == DialogResult.OK)
				{
					fileName = dlg.FileName;
					app.FilePath = fileName;
				}
				else
				{
					return;
				}
			}

			foreach(EditorUserControl page in app.EditorPages)
			{
				page.SaveData();
			}
			
			app.TileSetData.AcceptChanges();
			app.TileSetData.WriteXml(fileName);
		}


		private void SaveMap()
		{
			throw new NotImplementedException();
		}
	}
}