using System;
using System.Collections;
using System.Drawing;
using McKnight.Similization.Server;
using McKnight.Similization.Core;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// Interface representing a control used to save a game in Similization.
	/// </summary>
	public interface ISaveGameWindow : ISimilizationControl
	{
		/// <summary>
		/// Gets the name of the file the game is to be saved to.
		/// </summary>
		string SavedGameFile { get; }
	}

	/// <summary>
	/// Interface representing a control used to load a game in Similization.
	/// </summary>
	public interface ILoadGameWindow : ISimilizationControl
	{
		/// <summary>
		/// Gets the name of the file the game is to be loaded from.
		/// </summary>
		string LoadedGameFile { get; }
	}

	/// <summary>
	/// Represents the interface to be implemented to represent a world 
	/// setup control in the concrete client implementations.
	/// </summary>
	public interface IWorldSetupControl : ISimilizationControl
	{
	}

	

	/// <summary>
	/// Interface representing a basic similization control.
	/// </summary>
	public interface ISimilizationControl
	{
		/// <summary>
		/// Shows the control.
		/// </summary>
		void ShowSimilizationControl();
	}

	/// <summary>
	/// Interface representing an advisor control.
	/// </summary>
	/// <remarks>There are advisor windows (dialogs) for each of the 
	/// advisors in the game.</remarks>
	public interface IAdvisorControl : ISimilizationControl
	{
		/// <summary>
		/// The text that the advisor is currently telling the player.
		/// </summary>
		string AdvisorText { get; set; }
	}

	/// <summary>
	/// Interface representing the domestic advisor screen.
	/// </summary>
	public interface IDomesticAdvisorControl : IAdvisorControl
	{

	}

	/// <summary>
	/// Interface representing the foreign advisor screen.
	/// </summary>
	public interface IForeignAdvisorControl : IAdvisorControl
	{
	}



	/// <summary>
	/// Interface representing a control used to gather new city information.
	/// </summary>
	public interface INewCityControl : ISimilizationControl
	{
		/// <summary>
		/// Gets the name of the new city built by the player.
		/// </summary>
		string CityName { get; }
	}

	/// <summary>
	/// Interface representing the initial starting welcome screen.
	/// </summary>
	public interface IStartingScreen : ISimilizationControl
	{

	}

	/// <summary>
	/// Interface representing a control used to show histographic information to the user.
	/// </summary>
	public interface IHistograph : ISimilizationControl
	{
		
	}

	/// <summary>
	/// Interface representing a control or screen used to show help to the user.
	/// </summary>
	public interface IHelpControl : ISimilizationControl
	{

	}

	/// <summary>
	/// Interface representing a control used to notify the user that a new improvement has
	/// been built in a city.
	/// </summary>
	public interface IImprovementBuiltControl : ISimilizationControl
	{
		/// <summary>
		/// Gets or sets the message to display to the user.
		/// </summary>
		string Message { get; set; }

		/// <summary>
		/// Gets or sets the city that built the improvement.
		/// </summary>
		City City { get; set; }
	}

	/// <summary>
	/// Interface representing a control used to ask the player to choose a new government.
	/// </summary>
	public interface IGovernmentSelectionControl : ISimilizationControl
	{
		/// <summary>
		/// Gets or sets the message to display to the user.
		/// </summary>
		string Message { get; set; }

		/// <summary>
		/// Gets or sets the Governement to initially show to the user.
		/// </summary>
		Government NextGovernment { get; set; }
	}

	/// <summary>
	/// Interface representing a control used to notify the user that they need to start
	/// researching a new technology.
	/// </summary>
	public interface IResearchNeededControl : ISimilizationControl
	{
		/// <summary>
		/// Gets the technology that the user has chosen to research.
		/// </summary>
		Technology ChosenTechnology { get; }

		/// <summary>
		/// Gets or sets the message to show to the user.
		/// </summary>
		string Message { get; set; }
	}

	/// <summary>
	/// Interface representing a control used to notify the user that they have learned a 
	/// new technology, and need to select a different technology to research.
	/// </summary>
	public interface ITechnologyControl : ISimilizationControl
	{
		/// <summary>
		/// Gets the technology that the user has chosen to research.
		/// </summary>
		Technology ChosenTechnology { get; }

		/// <summary>
		/// Gets or sets the message to show to the user.
		/// </summary>
		string Message { get; set; }
	}

	
	/// <summary>
	/// Interface to a control asking the user if they would like to switch governments.
	/// </summary>
	public interface INewGovernmentControl : ISimilizationControl
	{
		/// <summary>
		/// Gets or sets the government that is now available.
		/// </summary>
		Government Government
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the message to display to the user.
		/// </summary>
		string Message
		{
			get;
			set;
		}
	}

	/// <summary>
	/// Interface to a console showing messages to the user.
	/// </summary>
	public interface IConsole
	{
		/// <summary>
		/// Writes a line to the console.
		/// </summary>
		/// <param name="message"></param>
		void WriteLine(string message);

		/// <summary>
		/// Shows the console.
		/// </summary>
		void ShowConsole();

		/// <summary>
		/// Hides the console.
		/// </summary>
		void HideConsole();
	}
}
