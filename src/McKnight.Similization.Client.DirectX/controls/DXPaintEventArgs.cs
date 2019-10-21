using System;
using System.Collections.Generic;
using System.Text;

namespace LJM.Similization.Client.DirectX.Controls
{
    /// <summary>
    /// Event information for <i>Render</i> events fired by <see cref="DXControl"/> classes.
    /// </summary>
    public class RenderEventArgs : EventArgs
    {
        private IDirectXControlHost host;
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderEventArgs"/> class.
        /// </summary>
        public RenderEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderEventArgs"/> class.
        /// </summary>
        /// <param name="host"></param>
        public RenderEventArgs(IDirectXControlHost host)
        {
            this.host = host;
        }

        /// <summary>
        /// The object that is hosting the <see cref="DXControl"/> that is being painted.
        /// </summary>
        public IDirectXControlHost ControlHost
        {
            get { return this.host; }
            set { this.host = value; }
        }
    }
}
