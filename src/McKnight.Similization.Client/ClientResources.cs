namespace McKnight.Similization.Client
{
	using System;
	using System.Resources;
    using McKnight.Similization.Core;
	using McKnight.Similization.Server;

	/// <summary>
	/// Class used to read resources from the Similization Client assembly.
	/// </summary>
	public sealed class ClientResources
	{
		private const string namespaceName = "McKnight.Similization.Client.SimilizationClient";
		private static ResourceManager mgr;
		private static ResourceManager exmgr;

		private ClientResources()
		{
		}

		/// <summary>
		/// Gets the resource string corresponding to the specified key.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetString(string key)
		{
			if(mgr == null)
				mgr = new ResourceManager(namespaceName, typeof(ClientApplication).Assembly);
			return mgr.GetString(key);
		}

		/// <summary>
		/// Gets the resource string for an exception.  
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetExceptionString(string key)
		{
			if(exmgr == null)
				exmgr = new ResourceManager("McKnight.Similization.Client.Exceptions", typeof(ClientApplication).Assembly);
			return exmgr.GetString(key);
		}

		/// <summary>
		/// Gets a string representation of the specified <see cref="McKnight.Similization.Core.UnitRank"/>
		/// </summary>
		/// <param name="rank"></param>
		/// <returns></returns>
		public static string GetRankString(UnitRank rank)
		{
			string rankString = string.Empty;

			switch(rank)
			{
				case UnitRank.Conscript:
					rankString = GetString(StringKey.RankConscript);		
					break;
				case UnitRank.Elite:
					rankString = GetString(StringKey.RankElite);
					break;
				case UnitRank.Regular:
					rankString = GetString(StringKey.RankRegular);
					break;
				case UnitRank.Veteran:
					rankString = GetString(StringKey.RankVeteran);
					break;
			}

			return rankString;
		}

        /// <summary>
        /// Gets a string representation of the size of city described by 
        /// the <i>sizeClass</i> parameter.
        /// </summary>
        /// <param name="sizeClass"></param>
        /// <returns></returns>
        public static string GetCitySizeString(CitySizeClass sizeClass)
        {
            string sizeString = string.Empty;

            switch (sizeClass)
            {
                case CitySizeClass.City:
                    sizeString = GetString("citySize_city");
                    break;
                case CitySizeClass.Metropolis:
                    sizeString = GetString("citySize_metropolis");
                    break;
                case CitySizeClass.Town:
                    sizeString = GetString("citySize_town");
                    break;
            }

            return sizeString;
        }
	}
}