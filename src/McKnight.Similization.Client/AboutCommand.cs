using System;
using System.Collections.Generic;
using System.Text;
using McKnight.Similization.Core;

namespace McKnight.Similization.Client
{
    /// <summary>
    /// Class representing the command to show the user the about box 
    /// dialog.
    /// </summary>
    public class AboutCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutCommand"/> class.
        /// </summary>
        public AboutCommand()
        {
            this.Name = "AboutCommand";
        }

        /// <summary>
        /// Invokes the command.
        /// </summary>
        public override void Invoke()
        {
            OnInvoking();
            ClientApplication ca = ClientApplication.Instance;
            IAboutBox aboutBox = (IAboutBox)ca.GetControl(typeof(IAboutBox));
            aboutBox.ShowSimilizationControl();
            OnInvoked();
        }
    }
}
