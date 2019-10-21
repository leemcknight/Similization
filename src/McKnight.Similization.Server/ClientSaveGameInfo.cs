using System;
using System.Collections.Generic;
using System.Text;

namespace McKnight.Similization.Server
{
    /// <summary>
    /// Represents client-side information that needs to be saved along with the rest of the save-game info.
    /// </summary>
    /// <remarks>Information contianed within the <c>ClientSaveGameInfo</c> class does not exist 
    /// on the server during gameplay.  For example, the cell that is currently focused on the client
    /// needs to be saved so that when the game is loaded the user starts at the same point.</remarks>
    public class ClientSaveGameInfo
    {
        private GridCell centerCell;


        /// <summary>
        /// Gets or sets the <c>GridCell</c> that should be displayed in the 
        /// center of the screen the next time this game is loaded.
        /// </summary>
        public GridCell CenterCell
        {
            get { return this.centerCell; }
            set { this.centerCell = value; }
        }


        private Country player;

        /// <summary>
        /// Gets or sets the country that is information refers to.
        /// </summary>
        public Country Country
        {
            get { return this.player; }
            set { this.player = value; }
        }
    }
}
