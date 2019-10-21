using System;
using System.Collections;
using System.Drawing;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// Provides a set of abstract functions and properties to be implemented by
	/// concrete game windows on different types of clients.
	/// </summary>
	public interface IGameWindow : ISimilizationControl
	{
		/// <summary>
		/// Gets or sets a value indicating if the game control is in "go to"
		/// mode.  When in this mode, the active unit will go to the grid cell
		/// selected on the map.  When moving the cursor around the screen, 
		/// the map will show the path the unit will take and the number of 
		/// turns it will take to reach the destination.
		/// </summary>
		bool GoToCursor { get; set; }


		/// <summary>
		/// Tells the <c>IGameWindow</c> to begin the process of bombarding a 
		/// target.  This will change the screen to a mode where the user 
		/// is required to select a <c>GridCell</c> to bombard.
		/// </summary>
		void BeginBombardProcess();

		/// <summary>
		/// Gets the grid coordinates that is currently under the mouse cursor.
		/// </summary>
		Point HotCoordinates { get; }

		/// <summary>
		/// Gets or sets the font to use for city names.
		/// </summary>
		Font CityNameFont { get; set; }

		/// <summary>
		/// Gets or sets the color to use for the city names drawn on the screen.
		/// </summary>
		Color CityNameFontColor { get; set; }

		/// <summary>
		/// Shows a message box with the desired information.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="title"></param>
		void ShowMessageBox(string message, string title);

		/// <summary>
		/// Gets a confirmation from the user for a specified message.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="title"></param>
		/// <returns></returns>
		bool GetUserConfirmation(string message, string title);

		/// <summary>
		/// Shows a message box with the desired information.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="title"></param>
		/// <param name="newCenter"></param>
		void ShowMessageBox(string message, string title, Point newCenter);

		/// <summary>
		/// Gets or sets the coordinates to show in the center of the window.
		/// </summary>
		Point CenterCoordinates { get; set; }

		/// <summary>
		/// Gets or sets the active unit in the game.
		/// </summary>
		Unit ActiveUnit { get; set; }
	}

	
}
