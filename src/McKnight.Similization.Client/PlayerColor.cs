namespace McKnight.Similization.Client
{
	using System;
	using System.Drawing;

	/// <summary>
	/// Class the represents the different colors computer players can be.
	/// </summary>
	public sealed class PlayerColor
	{
		private PlayerColor()
		{
		}

		/// <summary>
		/// Gets an array of possible player colors.
		/// </summary>
		/// <returns></returns>
		public static Color[] GetArray()
		{
			Color[] c = new Color[] {
										Color.Red,
										Color.Blue,
										Color.Green,
										Color.Yellow,
										Color.Purple,
										Color.Pink,
										Color.Brown,
										Color.Orange,
										Color.Gold,
										Color.Tan,
										Color.Crimson,
										Color.Navy,
										Color.MediumTurquoise
									};

			return c;
		}

		/// <summary>
		/// Gets a color for a computer player from an index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public static Color FromIndex(int index)
		{
			Color[] colors = GetArray();

			return colors[index];
		}
	}
}