using System;
using System.Drawing;
using McKnight.Similization.Server;
using McKnight.Similization.Core;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// Interface representing a control used to help the player start a new game.
	/// </summary>
	public interface INewGameControl : ISimilizationControl
	{
		/// <summary>
		/// Gets the civilization the the player chose to play as.
		/// </summary>
		Civilization ChosenCivilization
		{
			get;
		}
		

		/// <summary>
		/// Gets a list of computer opponents to play against.
		/// </summary>
		NamedObjectCollection<Civilization> ChosenOpponents
		{
			get;
		}

		/// <summary>
		/// Gets the color the player chose for their country.
		/// </summary>
		Color PlayerColor
		{
			get;
		}

		/// <summary>
		/// Gets the name of the human player.
		/// </summary>
		string LeaderName
		{
			get;
		}

        /// <summary>
        /// Gets the level of difficulty for the game.
        /// </summary>
        Difficulty Difficulty
        {
            get;
        }

		/// <summary>
		/// Gets the size of the world to use in the game.
		/// </summary>
		WorldSize WorldSize
		{
			get;
		}

		/// <summary>
		/// The temperature of the world the player has chosen.
		/// </summary>
		Temperature Temperature
		{
			get;
		}

		/// <summary>
		/// The Age of the world the player has chosen.
		/// </summary>
		Age Age
		{
			get;
		}

		/// <summary>
		/// The Climate of the world the player has chosen.
		/// </summary>
		Climate Climate
		{
			get;
		}

		/// <summary>
		/// The Size of the Land Masses on the world the player has chosen.
		/// </summary>
		Landmass Landmass
		{
			get;
		}

		/// <summary>
		/// The amount of water coverage on the world the player has chosen.
		/// </summary>
		WaterCoverage WaterCoverage
		{
			get;
		}


        /// <summary>
        /// Indicates how aggressive barbarians will be in the game.
        /// </summary>
        BarbarianAggressiveness BarbarianAggressiveness
        {
            get;
        }

		/// <summary>
		/// A <see cref="Rules"/> object representing the rules the player has chosen.
		/// </summary>
		Rules Rules
		{
			get;
		}

		/// <summary>
		/// Occurs when the player has chosen all of the game parameters.
		/// </summary>
		event EventHandler ResultChosen;
	}

}