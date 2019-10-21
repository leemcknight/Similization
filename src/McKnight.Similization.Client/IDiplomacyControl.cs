using System;
using System.Collections;
using System.Drawing;
using McKnight.Similization.Server;
using System.Collections.ObjectModel;
using McKnight.Similization.Core;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// Interface to a control letting the user conduct diplomacy with a foreign leader.  
	/// This control is used to set up trades, diplomatic agreements, and peace treaties.
	/// </summary>
	public interface IDiplomacyControl : ISimilizationControl
	{
		/// <summary>
		/// The Diplomatic tie behind the two governments involved in the diplomacy.
		/// </summary>
		DiplomaticTie DiplomaticTie
		{
			get; 
			set;
		}

		/// <summary>
		/// The list of tasks to allow the user to choose from.
		/// </summary>
		DiplomacyTaskLinkCollection TaskLinks
		{
			get;
		}

		/// <summary>
		/// The text that describes the foreign country and/or leader doing 
		/// the negotiating.
		/// </summary>
		string ForeignLeaderHeaderText
		{
			get;
			set;
		}

		/// <summary>
		/// The phrase that the foreign leader is currently saying.
		/// </summary>
		string ForeignLeaderPhrase
		{
			get;
			set;
		}


		/// <summary>
		/// The phrase that the advisor (trade/foreign/military) is currently saying.
		/// </summary>
		string AdvisorPhrase
		{
			get;
			set;
		}

		/// <summary>
		/// Gets a client-specific task link factory.
		/// </summary>
		IDiplomacyTaskLinkFactory GetTaskLinkFactory();

		/// <summary>
		/// Ends the current diplomatic relations with the foreign 
		/// country and closes the control.
		/// </summary>
		void EndDiplomacy();

		/// <summary>
		/// Starts negotiations with a blank slate.
		/// </summary>
		void StartNegotiations();

		/// <summary>
		/// Ends the current negotiations and returns the user to the 
		/// main diplomacy choices.
		/// </summary>
		void EndNegotiations();

		/// <summary>
		/// The items that are currently being offered to the foreign country.
		/// </summary>
		Collection<ITradable> GivenItems { get; }

		/// <summary>
		/// The items that are currently being asked of the foreign country.
		/// </summary>
		Collection<ITradable> TakenItems { get; }


		/// <summary>
		/// Occurs when the user clicks the button asking for more help or tips 
		/// from the foreign advisor on the screen.
		/// </summary>
		event EventHandler AdvisorHelpRequested;

	}
}