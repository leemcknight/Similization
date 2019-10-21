using System;
using System.Collections.Generic;
using System.Text;
using McKnight.Similization.Server;
using System.Collections.ObjectModel;

namespace McKnight.Similization.Client
{
    /// <summary>
    /// Interface to a control letting the user choose a diplomatic tie. 
    /// The selected diplomatic tie can be retrieved from the 
    /// <c>DiplomaticTie</c> property of the class.
    /// </summary>
    public interface IDiplomaticTiePicker : ISimilizationControl
    {
        /// <summary>
        /// Method to allow the picker to show specific <see cref="DiplomaticTie"/> 
        /// objects.  
        /// </summary>
        /// <remarks>This method is useful in situations where the control should 
        /// not show all available diplomatic ties.  This method is optional, and 
        /// when it is not called, all available diplomatic ties will be presented 
        /// to the user to choose from.</remarks>
        /// <param name="ties">A collection containing 
        /// the list of diplomatic ties a user should choose from.</param>
        void InitializePicker(Collection<DiplomaticTie> ties);

        /// <summary>
        /// The Diplomatic Tie that the user chose.
        /// </summary>
        DiplomaticTie DiplomaticTie
        {
            get;
        }

        /// <summary>
        /// The text that will be displayed to the user prompting them to select 
        /// a <see cref="McKnight.Similization.Server.DiplomaticTie"/>.
        /// </summary>
        string PickerTitle
        {
            get;
            set;
        }
    }
}
