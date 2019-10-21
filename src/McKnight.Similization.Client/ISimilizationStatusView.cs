namespace McKnight.Similization.Client
{
	using System;
	using System.Drawing;
	
	/// <summary>
	/// Interface representing a view of the status of the player.  This will contain strings
	/// telling the player the current year, the active unit, how many moves are left, etc...
	/// </summary>
	public interface ISimilizationStatusView
	{
		/// <summary>
		/// Gets or sets the year to show in the status bar.
		/// </summary>
		string Year { get; set; }

		/// <summary>
		/// Gets or sets the number of moves left for the active unit.
		/// </summary>
		string MovesLeft { get; set; }

		/// <summary>
		/// Gets or sets the technology to show to the user.  This is the technology
		/// currently being researched by the player.
		/// </summary>
		string Technology { get; set; }

		/// <summary>
		/// Gets or sets the name of the terrain the active unit is on.
		/// </summary>
		string Terrain { get; set; }

		/// <summary>
		/// Gets or sets the amount of gold to show that the user has.
		/// </summary>
		string Gold { get; set; }

		/// <summary>
		/// Gets or sets a string representation for the active unit in the game.
		/// </summary>
		string ActiveUnit { get; set; }

		/// <summary>
		/// Gets or sets a string value telling the user what goverenment they are currently
		/// using.
		/// </summary>
		string Government { get; set; }

		/// <summary>
		/// The <see cref="System.Drawing.Image"/> used to display the Unit.
		/// </summary>
		Image UnitImage { get; set; }
	}
}