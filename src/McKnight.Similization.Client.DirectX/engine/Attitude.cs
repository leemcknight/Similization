using System;
using System.Collections.Generic;
using System.Text;

namespace LJM.Similization.Client.DirectX.Engine
{
    /// <summary>
    /// Structure representing the attitude of an object in the game engine space.
    /// </summary>
    public struct Attitude
    {
        private float pitch;
        private float heading;
        private float roll;

        /// <summary>
        /// The pitch of the object.
        /// </summary>
        public float Pitch
        {
            get { return this.pitch; }
            set { this.pitch = value; }
        }

        /// <summary>
        /// The heading of the object.
        /// </summary>
        public float Heading
        {
            get { return this.heading; }
            set { this.heading = value; }
        }

        /// <summary>
        /// The roll of the object.
        /// </summary>
        public float Roll
        {
            get { return this.roll; }
            set { this.roll = value; }
        }

        /// <summary>
        /// Determines if the <see cref="Attitude"/> is equal to the specified object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Attitude))
                return false;
            Attitude attidue = (Attitude)obj;
            if (obj == null)
                return false;
            if (attidue.Heading != this.heading)
                return false;
            if (attidue.Pitch != this.pitch)
                return false;
            if (attidue.Roll != this.roll)
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
