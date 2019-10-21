using System;
using System.Windows.Forms;
using McKnight.Similization.Client;

namespace McKnight.Similization.Client.WindowsForms
{
	/// <summary>
	/// Windows Forms implementation of the <c>IHelpControl</c> interface.
	/// </summary>
	public class HelpControl : IHelpControl
	{

		private const string HELP_FILE = "Similization.chm";

		/// <summary>
		/// Shows the control.
		/// </summary>
		public void ShowSimilizationControl()
		{
			string path = Application.StartupPath + @"\" + HELP_FILE;
			Help.ShowHelp(_parent, path);            
		}

		private Form _parent;
		
		/// <summary>
		/// The parent form of the help form.
		/// </summary>
		public Form Parent
		{
			get { return _parent; }
			set { _parent = value; }
		}
	}
}
