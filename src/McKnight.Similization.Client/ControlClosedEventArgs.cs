using System;
using System.Collections.Generic;
using System.Text;

namespace McKnight.Similization.Client
{
    /// <summary>
    /// Class representing arguments detailing the result of a user closing a <see cref="ISimilizationControl"/>.
    /// </summary>
    public class ControlClosedEventArgs : EventArgs
    {
        private ControlCloseResult closeResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlClosedEventArgs"/> class.
        /// </summary>
        /// <param name="closeResult"></param>
        public ControlClosedEventArgs(ControlCloseResult closeResult)
        {
            this.closeResult = closeResult;
        }

        /// <summary>
        /// The result of the dialog control closed.
        /// </summary>
        public ControlCloseResult CloseResult
        {
            get { return this.closeResult; }
        }
    }

    /// <summary>
    /// Possible results from closing a <see cref="ISimilizationControl"/>.
    /// </summary>
    public enum ControlCloseResult
    {
        /// <summary>
        /// The user chose an action
        /// </summary>
        Ok,

        /// <summary>
        /// The user canceled out of the control without choosing an option or action.
        /// </summary>
        Cancel
    }
}
