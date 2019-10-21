using System;
using System.Collections.Generic;
using System.Text;

namespace McKnight.Similization.Core
{
    /// <summary>
    /// Base class for all Similization classes.
    /// </summary>
    public abstract class NamedObject
    {
        private string name;

        protected NamedObject()
        {
        }

        /// <summary>
        /// The name of the item.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
    }
}
