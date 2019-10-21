using System;

namespace McKnight.Similization.Core
{	
	/// <summary>
	/// Attitudes an AI opponent can have against another player
	/// </summary>
	public enum Attitude
	{
		/// <summary>
		/// Gracious.  This is the friendliest attitude.
		/// </summary>
		Gracious,

		/// <summary>
		/// The AI is polite toward the other player.
		/// </summary>
		Polite,

		/// <summary>
		/// The AI player is cautious toward the other player.  This 
		/// is common where th AI player doesn't know the other player 
		/// well.
		/// </summary>
		Cautious,

		/// <summary>
		/// The AI player is annoyed with the other player.
		/// </summary>
		Annoyed,

		/// <summary>
		/// The AI player is furious with the other player.  This is 
		/// common when the AI player is being attacked by the other player.
		/// </summary>
		Furious
	}
}

