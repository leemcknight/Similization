using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using McKnight.Similization.Client;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// Implementation of the <i>ISaveGameWindow</i> interface for the Similization
	/// Windows Client.
	/// </summary>
	public class SaveGameWindow : ISaveGameWindow
	{
		/// <summary>
		/// Shows the Save Game Window.
		/// </summary>
		public void ShowSimilizationControl()
		{			
			SaveFileDialog dialog;							
			dialog = new SaveFileDialog();
			dialog.Title = "Save Game";
			dialog.Filter = "Similization Game Files (*.sav) | *.sav";

			if(dialog.ShowDialog() == DialogResult.OK)
			{
				_saveGameFile = dialog.FileName;
			}
		}

		private string _saveGameFile;

		/// <summary>
		/// Gets the full path to the save game file.
		/// </summary>
		public string SavedGameFile
		{
			get { return _saveGameFile; }
		}

	}

	/// <summary>
	/// Implementation of the <i>ILoadGameWindow</i> interface for the
	/// Similization Windows Client.
	/// </summary>
	public class LoadGameWindow : ILoadGameWindow
	{
		/// <summary>
		/// Shows the Load Game Window.
		/// </summary>
		public void ShowSimilizationControl()
		{
			OpenFileDialog dialog;
			dialog = new OpenFileDialog();
			dialog.Title = "Load Game";
			dialog.Filter = "Saved game files (*.sav) | *.sav";
			
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				_loadedGameFile = dialog.FileName;
			}

		}

		private string _loadedGameFile;

		/// <summary>
		/// Gets the full path to the file to save the game to.
		/// </summary>
		public string LoadedGameFile
		{
			get { return _loadedGameFile; }
		}
	}
}
