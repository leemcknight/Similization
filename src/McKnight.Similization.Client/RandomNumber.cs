using System;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// A class to help generate random numbers.
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
		public static int GetRandomNumber(int maxValue)
		{
			byte[] seeds = Guid.NewGuid().ToByteArray();
			System.Random rand = new System.Random(seeds[0]);
			int index = rand.Next(maxValue);

			return index;
		}
	}
}
