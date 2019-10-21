namespace McKnight.Similization.Server
{
	using System;

	/// <summary>
	/// Enumeration representing various results from an attempt
	/// bombarding an enemy.
	/// </summary>
	public enum BombardmentResult
	{
		/// <summary>
		/// The bombardment was successfull, and improvements were destroyed.
		/// </summary>
		SucceededDestroyingCellImprovement,

		/// <summary>
		/// The city bombardment was successfull, and a city improvement was destroyed.
		/// </summary>
		SucceededDestroyingCityImprovement,

		/// <summary>
		/// The bombardment was successfull, and units were injured.
		/// </summary>
		SucceededInjuredUnit,

		/// <summary>
		/// The bombardment was successfull, and citizens of the city were killed.
		/// </summary>
		SucceededKilledCitizens,

		/// <summary>
		/// The bombardment failed.
		/// </summary>
		Failed,

		/// <summary>
		/// The bombardment failed, and the attacking unit was killed 
		/// in the process.
		/// </summary>
		AttackerKilled
	}
}