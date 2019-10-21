using System;
using System.Collections.Generic;
using System.Text;

namespace McKnight.Similization.Core
{
    /// <summary>
    /// Levels of Difficulty for the game.
    /// </summary>
    public enum Difficulty
    {
        /// <summary>
        /// Chieftain difficulty.  This is the easiest level in the game.
        /// </summary>
        Chieftain,

        /// <summary>
        /// Warlord difficulty.  This is the second easiest level in the game.
        /// </summary>
        Warlord,

        /// <summary>
        /// Prince difficulty.  This is a medium difficulty.  Easier than King.
        /// </summary>
        Prince,

        /// <summary>
        /// King difficulty.  This is the third-most difficult level in the game.
        /// </summary>
        King,

        /// <summary>
        /// Emperor difficulty.  This is the second-most difficult level in the game.
        /// </summary>
        Emperor,

        /// <summary>
        /// Deity level.  This is the hardest level in the game.
        /// </summary>
        Deity
    }
}
