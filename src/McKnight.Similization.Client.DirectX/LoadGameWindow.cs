using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.DirectX.Direct3D;
using LJM.Similization.Client.DirectX.Controls;
using LJM.Similization.Client;
using LJM.Similization.Server;

namespace LJM.Similization.Client.DirectX
{	
	/// <summary>
	/// DirectX client implementation fo the <see cref="ILoadGameWindow"/> interface.
	/// </summary>
	public class LoadGameWindow : DXWindow, ILoadGameWindow
	{
		private DXButton okButton;
		private DXButton cancelButton;
		private DXLabel headerLabel;
		private DXListBox listBox;
		private FileInfo selectedFile;
		private FileInfo[] files;
		

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadGameWindow"/> class.
        /// </summary>
        /// <param name="controlHost"></param>
		public LoadGameWindow(IDirectXControlHost controlHost) : base(controlHost)
		{
			InitializeComponent();
			GetSavedGames();
		}

		private void InitializeComponent()
		{
            this.Text = DirectXClientResources.LoadGameTitle;
			this.Location = new Point(0,0);
            this.Size = new Size(800, 600);
			this.BackColor = Color.White;
            this.BackColor2 = Color.White;			

			this.okButton = new DXButton(this.ControlHost, this);
			this.cancelButton = new DXButton(this.ControlHost, this);
			this.headerLabel = new DXLabel(this.ControlHost, this);

			//Header Label
            this.headerLabel.Text = DirectXClientResources.LoadGameLabel;            
			this.headerLabel.Location = new Point(10, 130);			
			this.headerLabel.ForeColor = Color.White;
            this.headerLabel.AutoSize = true;
			this.Controls.Add(this.headerLabel);

			//list box
			this.listBox = new DXListBox(this.ControlHost, this);
			this.listBox.Location = new Point(10, 150);
			this.listBox.Size = new Size(400, 200);
			this.listBox.BackColor = Color.Black;
			this.listBox.ForeColor = Color.DarkBlue;
			this.listBox.SelectedIndexChanged += new System.EventHandler(SelectedFileChanged);
			this.Controls.Add(this.listBox);

			//OK button
			this.okButton.Text = DirectXClientResources.OK;
			this.okButton.Size = new Size(75, 25);
			this.okButton.Location = new Point(315,365);
			this.okButton.Click += new System.EventHandler(OKButtonPressed);
			this.Controls.Add(this.okButton);
	
			//Cancel Button
			this.cancelButton.Text = DirectXClientResources.Cancel;
            this.cancelButton.Size = new Size(75, 25);
            this.cancelButton.Location = new Point(400, 365);
            this.Controls.Add(this.cancelButton);
		}

		private void GetSavedGames()
		{
			DirectoryInfo info = new DirectoryInfo("Saves");
			this.files = info.GetFiles();
			
			foreach(FileInfo fileInfo in this.files)			
				this.listBox.Items.Add(fileInfo.Name);			
		}

		private void SelectedFileChanged(object sender, System.EventArgs e)
		{
			
		}

        /// <summary>
        /// The filename of the game the user has selected to load.
        /// </summary>
		public string LoadedGameFile
		{
			get 
            {
                if (this.selectedFile == null)
                    return string.Empty;
                return this.selectedFile.FullName; 
            }
		}

        /// <summary>
        /// Shows the <see cref="LoadGameWindow"/>
        /// </summary>
		public void ShowSimilizationControl()
		{
			this.Show();
		}

		private void OKButtonPressed(object sender, System.EventArgs e)
		{
			string file;
			FileInfo selectedFileInfo = null;

			file = this.listBox.SelectedItem.ToString();
			foreach(FileInfo fileInfo in this.files)
			{
				if(fileInfo.Name == file)
				{
					selectedFileInfo = fileInfo;
					break;
				}
			}
			
			if(selectedFileInfo != null)
			{
				
			}
		}
	

	}
}
