using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.DirectInput;
using System.Drawing;

namespace LJM.Similization.Client.DirectX.Controls
{
    /// <summary>
    /// Actions a user can perform on a mouse
    /// </summary>
    public enum MouseAction
    {
        /// <summary>
        /// No action was taken.
        /// </summary>
        None,

        /// <summary>
        /// The user moves the mouse
        /// </summary>
        Move,

        /// <summary>
        /// The user clicks a mouse button
        /// </summary>
        Click,

        /// <summary>
        /// The user releases a mouse button
        /// </summary>
        Release,

        /// <summary>
        /// The user double clicks the mouse
        /// </summary>
        DoubleClick,

        /// <summary>
        /// The user hovers the mouse
        /// </summary>
        Hover
    }

    /// <summary>
    /// Class holding event data for mouse events.
    /// </summary>
    public class DXMouseEventArgs : EventArgs
    {
        private MouseAction mouseAction;
        private MouseState mouseState;
        private Point mousePosition;
        private IDirectXControlHost controlHost;
        private int buttonIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="DXMouseEventArgs"/> class.
        /// </summary>
        public DXMouseEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DXMouseEventArgs"/> class.
        /// </summary>
        /// <param name="mouseAction"></param>
        /// <param name="mouseState"></param>
        /// <param name="mousePosition"></param>
        /// <param name="buttonIndex"></param>
        /// <param name="controlHost"></param>
        public DXMouseEventArgs(MouseAction mouseAction, MouseState mouseState, Point mousePosition, int buttonIndex, IDirectXControlHost controlHost)
        {
            this.mouseAction = mouseAction;
            this.mouseState = mouseState;
            this.mousePosition = mousePosition;
            this.controlHost = controlHost;
            this.buttonIndex = buttonIndex;
        }

        /// <summary>
        /// Determines which actions were performed by the mouse.
        /// </summary>
        public MouseAction MouseAction
        {
            get { return this.mouseAction; }
            set { this.mouseAction = value; }
        }

        /// <summary>
        /// The index into the button array that determines which button was pressed or released.
        /// </summary>
        public int ButtonIndex
        {
            get { return this.buttonIndex; }
            set { this.buttonIndex = value; }
        }

        /// <summary>
        /// The current position of the mouse in screen coordinates.
        /// </summary>
        public Point MousePosition
        {
            get { return this.mousePosition; }
            set { this.mousePosition = value; }
        }

        /// <summary>
        /// The current state of the mouse.
        /// </summary>
        public MouseState MouseState
        {
            get { return this.mouseState; }
            set { this.mouseState = value; }
        }

        /// <summary>
        /// The object hosting the <see cref="DXControl"/> that is responding to the mouse.
        /// </summary>
        public IDirectXControlHost ControlHost
        {
            get { return this.controlHost; }
            set { this.controlHost = value; }
        }
    }
}
