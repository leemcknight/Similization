using System;

namespace McKnight.Similization.Server
{	
	/// <summary>
	/// Possible ways an AI player can percieve the culture of another player.
	/// </summary>
	public enum CulturalPerception
	{
		/// <summary>
		/// The AI player is in awe of the culture of the other player.
		/// </summary>
		InAwe,

		/// <summary>
		/// The AI player admires the culture of the other player.
		/// </summary>
		Admirer,

		/// <summary>
		/// The AI player is impressed by the culture of the other player.
		/// </summary>
		Impressed,

		/// <summary>
		/// The AI player is unimpressed by the culture of the other player.
		/// </summary>
		Unimpressed,

		/// <summary>
		/// The AI player is dismissive of the culture of the other player.
		/// </summary>
		Dismissive,

		/// <summary>
		/// The AI player is disdainful of the culture of the other player.
		/// </summary>
		Disdainful
	}

	/// <summary>
	/// Possible results from trying to steal a technology.
	/// </summary>
	public enum StolenTechnologyResult
	{

		/// <summary>
		/// Successfully stole the technology.
		/// </summary>
		Success,

		/// <summary>
		/// Failed to steal the technology.
		/// </summary>
		Failure,

		/// <summary>
		/// Could not attempt to steal the technology because
		/// no embassy has been established.
		/// </summary>
		NoEmbassyEstablished
	}

	/// <summary>
	/// Possible results from try to perform espionage on another country.
	/// </summary>
	public enum EspionageResult
	{
		/// <summary>
		/// Successfully performed espionage.
		/// </summary>
		Success,

        /// <summary>
        /// Successfully performed the espionage, but our spy was caught in the process.
        /// </summary>
        SuccessWithCapturedSpy,

		/// <summary>
		/// Failed to perform espionage.
		/// </summary>
		Failure,

		/// <summary>
		/// The spy who tried to perform the espionage was caught, and the mission failed.
		/// </summary>
		SpyCaught,

		/// <summary>
		/// Could not perform espionage.
		/// </summary>
		ImmuneToEspionage
	}


	/// <summary>
	/// Possible diplomatic states between two countries.
	/// </summary>
	public enum DiplomaticState
	{

		/// <summary>
		/// The two countries are at peace.
		/// </summary>
		Peace,

		/// <summary>
		/// The two countries are at war.
		/// </summary>
		War
	}



	

	/// <summary>
	/// Determines how aggresive barbarians will be in the game.
	/// </summary>
    public enum BarbarianAggressiveness
    {
        /// <summary>
        /// Sedentary.  Barbarians will not attack at all.
        /// </summary>
        Sedentary,

        /// <summary>
        /// Roaming.  Barbarians will wander randomly, but not attack without provocation.
        /// </summary>
        Roaming,

        /// <summary>
        /// Restless.  Barbarians will roam and be somewhat agressive.
        /// </summary>
        Restless,

        /// <summary>
        /// Raging.  Barbarians will hunt out other countries and aggresively fight them.
        /// </summary>
        Raging,

        /// <summary>
        /// Random.  The AI will pick a random setting for the aggresiveness.
        /// </summary>
        Random
    }


	/// <summary>
	/// Possible results from having a unit move to a new cell.
	/// </summary>
	public enum MoveResult
	{
		/// <summary>
		/// successfully moved to the desired cell
		/// </summary>
		MoveSuccess,

		/// <summary>
		///Unable to move to cell because the land type is unreachable by this unit
		/// </summary>
		UnreachableTerrain,

		/// <summary>
		/// killed in combat
		/// </summary>
		Killed,

		/// <summary>
		///the destination cell is taken by a non-foe
		/// </summary>
		CellTaken,

		/// <summary>
		/// there was an enemy in the cell, and
		/// combat was unresolved after the turn.
		/// </summary>
		UnresolvedCombat,

		/// <summary>
		/// the unit does not have any moves left,
		/// and thefore cannot move to the requested
		/// cell.
		/// </summary>
		NoMovesLeft
	}

	/// <summary>
	/// Possible results from entering into combat with another unit.
	/// </summary>
	public enum CombatResult
	{

		/// <summary>
		/// The unit wins the fight.  The other unit is destroyed.
		/// </summary>
		Win,

		/// <summary>
		/// The unit is killed.  
		/// </summary>
		Killed,

		/// <summary>
		/// The unit is captures the other unit.  
		/// </summary>
		Capture,

		/// <summary>
		/// The fight is unresolved this turn.
		/// </summary>
		Unresolved
	}

	/// <summary>
	/// Possible states a unit can be in.
	/// </summary>
	public enum UnitStatus
	{
		/// <summary>
		/// The unit is active.  It will take a turn.
		/// </summary>
		Active,

		/// <summary>
		/// The unit is fortified.  It will be skipped during the turn unit 
		/// it is activated.
		/// </summary>
		Fortified,

		/// <summary>
		/// The unit is on automove.  The computer will move the unit 
		/// automatically.
		/// </summary>
		AutoMove,

		/// <summary>
		/// The unit is in sentry.  It will remain positioned, but if an 
		/// enemy unit apporoaches within 1 square, the unit will become active.
		/// </summary>
		Sentry
	}




	/// <summary>
	/// Possible things a worker can be doing
	/// </summary>
	public enum WorkerStatus
	{
		/// <summary>
		/// The worker is not doing anything.
		/// </summary>
		Idle,

		/// <summary>
		/// The worker is building a new mine.
		/// </summary>
		Mining,

		/// <summary>
		/// The worker is building a road.
		/// </summary>
		BuildingRoad,

		/// <summary>
		/// The worker is irrigating the land.
		/// </summary>
		Irrigating,

		/// <summary>
		/// The worker is cleaning pollution.
		/// </summary>
		CleaningPollution,

		/// <summary>
		/// The worker is building a railroad.
		/// </summary>
		BuildingRailroad,

		/// <summary>
		/// The worker is clearing the jungle.
		/// </summary>
		ClearingJungle,

		/// <summary>
		/// The worker is deforesting the area.
		/// </summary>
		ClearingForest
	}

	/// <summary>
	/// Different results from entering a village.
	/// </summary>
	public enum VillageEntryResult
	{
		/// <summary>
		/// The village was desterted.
		/// </summary>
		Deserted,

		/// <summary>
		/// The village contained barbarians who attacked the entering unit.
		/// </summary>
		Attack,

		/// <summary>
		/// The village contained gold.
		/// </summary>
		Gold,

		/// <summary>
		/// The village gave the units' parent colony a technology advance.
		/// </summary>
		Advance,

		/// <summary>
		/// The village contained a military unit who joined the colony.
		/// </summary>
		MilitaryUnit,

		/// <summary>
		/// The village contained a settler unit who joined the colony.
		/// </summary>
		Settler,

		/// <summary>
		/// The village contained a city that joined the colony.
		/// </summary>
		NewCity
	}


	

	/// <summary>
	/// Describes which side of the grid cell borders 
	/// a different country.
	/// </summary>
	[FlagsAttribute]
	public enum BorderTypes
	{
		/// <summary>
		/// There is no border in the cell.
		/// </summary>
		None = 0,


		/// <summary>
		/// The top (north) side of the grid cell is a border.
		/// </summary>
		Top = 1,

		/// <summary>
		/// The bottom (southern) side of the grid cell is a border.
		/// </summary>
		Bottom = 2,

		/// <summary>
		/// The left (western) side of the grid cell is a border.
		/// </summary>
		Left = 4,


		/// <summary>
		/// The right (eastern) side of the grid cell is a border.
		/// </summary>
		Right = 8
	}


	/// <summary>
	/// Different types of things a city can need.
	/// </summary>
	public enum AICityNeed
	{
		/// <summary>
		/// The city is vunerable.  It needs defending.
		/// </summary>
		UnitDefense,

		/// <summary>
		/// The city needs more culture.
		/// </summary>
		Culture,

		/// <summary>
		/// There needs to be more cash flowing through the city.
		/// </summary>
		Commerce,

		/// <summary>
		/// There needs to be more food in the city.
		/// </summary>
		Food,
	}

	/// <summary>
	/// Different things a worker can do.
	/// </summary>
	public enum WorkerAction
	{
		/// <summary>
		/// No Action
		/// </summary>
		None,

		/// <summary>
		/// Irrigate the cell.
		/// </summary>
		Irrigate,

		/// <summary>
		/// Clean any pollution that might be in the cell.
		/// </summary>
		CleanPollution,

		/// <summary>
		/// Build a road in the cell.
		/// </summary>
		BuildRoad,

		/// <summary>
		/// Build a railroad in the cell.
		/// </summary>
		BuildRailroad,

		/// <summary>
		/// Mine the cell for gold.
		/// </summary>
		Mine,

		/// <summary>
		/// Build a fortress in the cell.
		/// </summary>
		BuildFortress
	}

	/// <summary>
	/// Different types of victories in the game
	/// </summary>
	public enum VictoryType
	{
		/// <summary>
		/// Represents a Military Victory.
		/// </summary>
		MilitaryVictory,

		/// <summary>
		/// Represents a Diplomatic Victory.
		/// </summary>
		DiplomaticVictory,

		/// <summary>
		/// Represents a Cultural Victory.
		/// </summary>
		CulturalVictory,

		/// <summary>
		/// Represents a Domination Victory.
		/// </summary>
		DominationVictory,

		/// <summary>
		/// Represents a Space Victory.
		/// </summary>
		SpaceVictory
	}

}
