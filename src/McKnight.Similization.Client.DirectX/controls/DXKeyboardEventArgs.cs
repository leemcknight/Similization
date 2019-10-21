using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace LJM.Similization.Client.DirectX.Controls
{
    /// <summary>
    /// Actions a user can take with the keyboard.
    /// </summary>
    public enum KeyboardAction
    {
        /// <summary>
        /// A key has been pressed.
        /// </summary>
        KeyPress,

        /// <summary>
        /// A key has been released.
        /// </summary>
        KeyUp,

        /// <summary>
        /// A key has been held down for an interval longer than the keyboard repeat rate.        
        /// </summary>
        KeyHeld
    }

    /// <summary>
    /// Class containing event data for events that occur when keys are pressed on a keyboard.
    /// </summary>
    public class DXKeyboardEventArgs : EventArgs
    {
        private KeyboardState keyboardState;
        private KeyboardAction keyboardAction;
        private Key key;

        /// <summary>
        /// Initializes a new instance of the <see cref="DXKeyboardEventArgs"/> class.
        /// </summary>
        public DXKeyboardEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DXKeyboardEventArgs"/> class.
        /// </summary>
        /// <param name="keyboardState"></param>
        /// <param name="keyboardAction"></param>
        /// <param name="key"></param>
        public DXKeyboardEventArgs(KeyboardState keyboardState, KeyboardAction keyboardAction, Key key)
        {
            this.keyboardState = keyboardState;
            this.keyboardAction = keyboardAction;
            this.key = key;
        }

        /// <summary>
        /// The current state of the keyboard.
        /// </summary>
        public KeyboardState KeyboardState
        {
            get { return this.keyboardState; }
            set { this.keyboardState = value; }
        }

        /// <summary>
        /// The action that was performed with the keyboard.
        /// </summary>
        public KeyboardAction KeyboardAction
        {
            get { return this.keyboardAction; }
            set { this.keyboardAction = value; }
        }

        /// <summary>
        /// The character that was pressed.
        /// </summary>
        public Key Key
        {
            get { return this.key; }
            set { this.key = value; }
        }
    }
}
