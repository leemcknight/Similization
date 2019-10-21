namespace McKnight.Similization.Client
{
	using System;
	using McKnight.Similization.Server;

	/// <summary>
	/// Interface representing the military advisor screen.
	/// </summary>
	public interface IMilitaryAdvisorControl : IAdvisorControl
	{
		/// <summary>
		/// The foreign country that the user is viewing.
		/// </summary>
		Country ForeignCountry { get; set; }

		/// <summary>
		/// The text that will be displayed as the header above 
		/// the list of units in the local military.
		/// </summary>
		string CountryHeaderText { get; set; }

		/// <summary>
		/// The text that will be displayed as the header above
		/// the list of units in the selected foreign military.
		/// </summary>
		string ForeignCountryHeaderText { get; set; }

		/// <summary>
		/// Occurs when the user selects a different foreign country 
		/// to show on the screen.
		/// </summary>
		event EventHandler ForeignCountryChanged;
	}
}