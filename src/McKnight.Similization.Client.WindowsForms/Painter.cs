using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace McKnight.Similization.Client.WindowsForms
{
    /// <summary>
    /// Abstract implementation of the <see cref="IPainter"/> interface.
    /// </summary>
    public abstract class Painter : IPainter
    {
        private Graphics graphics;
        private Rectangle bounds;

        /// <summary>
        /// Performs the paint operation.
        /// </summary>
        public abstract void Paint();
                            

        /// <summary>
        /// The <i>Rectangle</i> to paint within.
        /// </summary>
        public Rectangle Bounds
        {
            get { return this.bounds; }
            set { this.bounds = value; }
        }

        /// <summary>
        /// The <i>Graphics</i> object to paint onto.
        /// </summary>
        public System.Drawing.Graphics Graphics
        {
            get
            {
                return this.graphics;
            }
            set
            {
                this.graphics = value;
            }
        }        
    }
}
