namespace McKnight.Similization.Server
{
	using System;

	/// <summary>
	/// Possible responses to a trade proposal
	/// </summary>
	public enum TradeResponse
	{
		/// <summary>
		/// Trade offer accepted
		/// </summary>
		Accept,

		/// <summary>
		/// Trade offer angrily declined
		/// </summary>
		TotalDecline,

		/// <summary>
		/// Trade offer strongly declined
		/// </summary>
		StrongDecline,

		/// <summary>
		/// Trade offer declined
		/// </summary>
		NeutralDecline,

		/// <summary>
		/// Trade offer declined, but close to an acceptable deal.
		/// </summary>
		WeakDecline
	}
}