using System;
using System.Windows.Forms;
using McKnight.Similization.Core;

namespace McKnight.SimilizationRulesEditor
{
	public class AddNewItemCommand : Command
	{
		public override void Invoke()
		{
			if(EditorApp.Instance.ActiveControl != null)
			{
				EditorApp.Instance.ActiveControl.AddNew();
			}
			else
			{
				NewItemDialog dlg = new NewItemDialog();
				EditorApp app = EditorApp.Instance;
				if(dlg.ShowDialog() == DialogResult.OK)
				{
					switch(dlg.Item)
					{
						case EditorItem.Ruleset:
							app.CreateRuleset(dlg.NewItemName, dlg.NewItemLocation);
							break;
						case EditorItem.Tileset:
							app.CreateTileset(dlg.NewItemName, dlg.NewItemLocation);
							break;
						case EditorItem.Map:                            
							break;
						
					}
				}
			}
		}
	}
}