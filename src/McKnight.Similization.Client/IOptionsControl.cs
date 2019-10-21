using System;
using System.Drawing;
using System.Text;

namespace McKnight.Similization.Client
{
    /// <summary>
    /// Interface representing a control used to show options to the user.
    /// </summary>
    public interface IOptionsControl : ISimilizationControl
    {
        /// <summary>
        /// Gets a value indicating whether or not the user is choosing
        /// to wait after each turn of the game.
        /// </summary>
        bool WaitAfterTurn { get; }

        /// <summary>
        /// Gets a value indicating whether or not the user is choosing
        /// to see a message indicating that one of their units has been 
        /// destroyed.
        /// </summary>
        bool ShowKilledMessage { get; }

        /// <summary>
        /// Gets the path of the ruleset to start with by default.
        /// </summary>
        string StartingRulesetPath { get; }


        /// <summary>
        /// Gets the <c>System.Drawing.Font</c> to use when drawing City information.
        /// </summary>
        Font CityNameFont { get; }


        /// <summary>
        /// The <c>System.Drawing.Color</c> to paint the <c>CityNameFont</c> with.
        /// </summary>
        Color CityNameFontColor { get; }


        /// <summary>
        /// The path of the tileset to use in the game.
        /// </summary>
        string TilesetPath { get; }

        /// <summary>
        /// Occurs when the user is finished with the control.
        /// </summary>
        event EventHandler<ControlClosedEventArgs> Closed;
    }
}
