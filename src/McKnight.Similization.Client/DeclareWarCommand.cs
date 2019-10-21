namespace McKnight.Similization.Client
{
	using System;
	using McKnight.Similization.Server;

	/// <summary>
	/// A command to declare war on a foreign country.
	/// </summary>
	public class DeclareWarCommand : DiplomacyCommand
	{
		private Country enemy;

		/// <summary>
		/// Invokes the command.
		/// </summary>
		public override void Invoke()
		{
			OnInvoking();
			Country player = ClientApplication.Instance.Player;
			player.DeclareWar(this.enemy);
			OnInvoked();
		}

		/// <summary>
		/// The <see cref="McKnight.Similization.Server.Country"/> that is being declared war upon.
		/// </summary>
		public Country Enemy
		{
			get { return this.enemy; }
			set { this.enemy = value; }
		}

	}
}