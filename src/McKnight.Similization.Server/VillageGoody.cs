using System;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Base class for all "goodies" you can find in a village.
	/// </summary>
	public abstract class VillageGoody
	{
		private bool applied;
        private string tribeName;

		/// <summary>
		/// Initializes a new instance of the <see cref="VillageGoody"/> class.
		/// </summary>
		/// <param name="tribeName"></param>
		protected VillageGoody(string tribeName)
		{
			this.tribeName = tribeName;
		}
		
		/// <summary>
		/// Gets the name of the tribe the village belongs to.
		/// </summary>
		public string TribeName
		{
			get { return tribeName; }
		}

		/// <summary>
		/// Applies the goody to the country that just received the goody.
		/// </summary>
		/// <param name="goodyOwner"></param>
		public virtual void ApplyGoody(Country goodyOwner)
		{
			if(this.applied)
				throw new InvalidOperationException(ServerResources.GoodyAlreadyApplied);
			this.applied = true;
		}
	}
}