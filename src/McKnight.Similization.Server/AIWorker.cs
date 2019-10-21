using System;
using System.Drawing;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// Represents an AI Worker.
	/// </summary>
	public class AIWorker : Worker
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="AIWorker"/> class.
		/// </summary>
		public AIWorker() : base()
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="AIWorker"/> class.
        /// </summary>
        /// <param name="unitClone"></param>
        public AIWorker(Unit unitClone)
            : base(unitClone)
        {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="AIWorker"/> class.
		/// </summary>
		/// <param name="coordinates"></param>
		/// <param name="unitClone"></param>
		public AIWorker(Point coordinates, Unit unitClone) :base(coordinates, unitClone)
		{
		}
	}
}
