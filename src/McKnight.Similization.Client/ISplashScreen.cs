using System;
using System.Drawing;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// Interface to a splash screen used in the game.
	/// </summary>
	public interface ISplashScreen : ISimilizationControl
	{
		/// <summary>
		/// Closes the screen.
		/// </summary>
		void CloseSplashScreen();

		/// <summary>
		/// The Version Number to display on the splash screen
		/// </summary>
		string VersionNumber { get; set; }
	}
}