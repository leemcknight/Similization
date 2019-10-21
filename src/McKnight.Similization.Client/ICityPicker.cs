using System;
using System.Collections.Generic;
using System.Text;
using McKnight.Similization.Server;

namespace McKnight.Similization.Client
{
    /// <summary>
    /// Interface to a control asking the user to select a <see cref="McKnight.Similization.Server.City"/> 
    /// from a list.
    /// </summary>
    public interface ICityPicker : ISimilizationControl
    {
        /// <summary>
        /// Initializes the <see cref="ICityPicker"/> with cities from the 
        /// specified <see cref="McKnight.Similization.Server.Country"/>.
        /// </summary>
        /// <param name="country"></param>
        void InitializePicker(Country country);

        /// <summary>
        /// The <see cref="McKnight.Similization.Server.City"/> that the user has chosen.
        /// </summary>
        City City { get; }

        /// <summary>
        /// The text to display to the user asking them to select a <see cref="McKnight.Similization.Server.City"/>.
        /// </summary>
        string PickerTitle { get; set; }
    }
}
