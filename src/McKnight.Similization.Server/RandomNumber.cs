using System;

namespace McKnight.Similization.Server
{
	/// <summary>
	/// A class to help generate random numbers
	/// </summary>
	public sealed class RandomNumber
	{
		private RandomNumber()
		{
		}

		/// <summary>
		/// Gets a random number from 1 to <c>maxValue</c>
		/// </summary>
		/// <param name="maxValue">The maximum value the random number can be.</param>
		/// <returns>The random number.</returns>
		public static int UpTo(int maxValue)
		{
			byte[] seeds = Guid.NewGuid().ToByteArray();
			System.Random rand = new System.Random(seeds[0]);
			int index = rand.Next(maxValue);

			return index;
		}

		/// <summary>
		/// Gets a random number from <c>minValue</c> to <c>maxValue</c>
		/// </summary>
		/// <param name="minValue"></param>
		/// <param name="maxValue"></param>
		/// <returns>A random number between the parameters</returns>
		public static int Between(int minValue, int maxValue)
		{
			byte[] seeds = Guid.NewGuid().ToByteArray();
			Random random = new Random(seeds[0]);
			int index = random.Next(minValue,maxValue);
			return index;
		}
	}
}