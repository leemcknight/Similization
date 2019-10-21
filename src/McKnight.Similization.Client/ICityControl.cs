using System;
using System.Collections.Generic;
using System.Text;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{
    /// <summary>
    /// Interface representing the control used to display the details about 
    /// a given <see cref="McKnight.Similization.Server.City"/>.
    /// </summary>
    public interface ICityControl : ISimilizationControl
    {
        /// <summary>
        /// Determines whether the information in the city screen should 
        /// be editable.
        /// </summary>
        bool Editable { get; set; }

        /// <summary>
        /// The <see cref="McKnight.Similization.Server.City"/> currently being displayed 
        /// in the control.
        /// </summary>
        City City { get; set; }
        
    }
}
