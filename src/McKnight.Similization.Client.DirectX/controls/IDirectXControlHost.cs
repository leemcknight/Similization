using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace LJM.Similization.Client.DirectX.Controls
{
    /// <summary>
    /// Interface representing the class that will host the <see cref="DXControl"/> objects 
    /// in an application.
    /// </summary>
    public interface IDirectXControlHost
    {
        /// <summary>
        /// The <i>Microsoft.DirectX.Direct3D.Device</i> used to render controls onto the screen.
        /// </summary>
        Device Device { get; }

        /// <summary>
        /// Draws a line onto the screen.
        /// </summary>
        /// <param name="vertexList"></param>
        /// <param name="lineColor"></param>
        void DrawLine(Vector2[] vertexList, Color lineColor);

        /// <summary>
        /// The collection of <see cref="DXControl"/> objects being rendered to the host.
        /// </summary>
        DXControlCollection Controls { get; }

        /// <summary>
        /// The <i>Rectangle</i> representing the area of the screen available to the 
        /// <see cref="IDirectXControlHost"/>.
        /// </summary>
        Rectangle ScreenBounds { get; }
        
        /// <summary>
        /// Occurs when action is taken with the mouse.
        /// </summary>
        event EventHandler<DXMouseEventArgs> MouseActionPerformed;

        /// <summary>
        /// Occurs when action is taken with the keyboard.
        /// </summary>
        event EventHandler<DXKeyboardEventArgs> KeyboardActionPerformed;
    }
}
