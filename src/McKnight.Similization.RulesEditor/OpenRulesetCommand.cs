using System;
using System.Data;
using System.Windows.Forms;
using McKnight.Similization.Core;

namespace McKnight.SimilizationRulesEditor
{
	public class OpenRulesetCommand : Command
	{
		public override void Invoke()
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Ruleset files(*.ruleset) | *.ruleset";
			dlg.Title = "Open a Ruleset";
			dlg.RestoreDirectory = true;
			dlg.Multiselect = false;
			
			if(dlg.ShowDialog() == DialogResult.OK)
			{
				string fileName = dlg.FileName;
				EditorApp.Instance.LoadRuleset(fileName);
				EditorApp.Instance.FilePath = fileName;
			}
		}
	}
}