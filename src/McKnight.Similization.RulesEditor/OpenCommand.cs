using System;
using System.Data;
using System.Windows.Forms;
using McKnight.Similization.Core;

namespace McKnight.SimilizationRulesEditor
{
	public class OpenCommand : Command
	{
		private readonly string RulesetExtenstion = "ruleset";
		private readonly string TilesetExtension = "tileset";

		public override void Invoke()
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "All Files (*.*) | *.*";
            dlg.Title = EditorResources.OpenRuleset;
			dlg.Multiselect = false;
			dlg.RestoreDirectory = true;

			if(dlg.ShowDialog() == DialogResult.OK)
			{
				string fileName = dlg.FileName;
				int index = fileName.LastIndexOf('.');
				string[] parts = fileName.Split('.');
				string extension = parts[parts.GetUpperBound(0)];
				if(extension == RulesetExtenstion)
				{
					EditorApp.Instance.LoadRuleset(fileName);
				}
				else if(extension == TilesetExtension)
				{
					EditorApp.Instance.LoadTileset(fileName);
				}
				
				EditorApp.Instance.FilePath = fileName;
			}
		}
	}
}