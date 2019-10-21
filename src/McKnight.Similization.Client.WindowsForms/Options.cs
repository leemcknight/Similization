using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace LJM.Similization.WindowsClient
{
	/// <summary>
	/// Represents the options for the game.
	/// </summary>
	public class Options
	{
		private const string OPTIONS_FILE = "options.xml";

		private bool _showKilledMessage;
		/// <summary>
		/// Gets or sets a value indicating if the user will see a 
		/// notification when a unit is killed.
		/// </summary>
		public bool ShowKilledMessage
		{
			get { return _showKilledMessage; }
			set { _showKilledMessage = value; }
		}

		private bool _waitAfterTurn;

		/// <summary>
		/// Gets or sets a value indicating whether the user has 
		/// to manually end their turn before the computer players
		/// take their turn.
		/// </summary>
		public bool WaitAfterTurn
		{
			get { return _waitAfterTurn; }
			set { _waitAfterTurn = value; }
		}


		/// <summary>
		/// Serializes the options to disk in Xml format.
		/// </summary>
		public void Serialize()
		{
			XmlSerializer serializer = 
				new XmlSerializer(GetType());

			FileStream stream = 
				File.Open("options.xml",
				FileMode.OpenOrCreate, 
				FileAccess.ReadWrite);
			
			serializer.Serialize(stream, this);
		}

		/// <summary>
		/// Reads the game options from the disk.
		/// </summary>
		/// <returns></returns>
		public static Options FromFile()
		{
			XmlSerializer serializer = 
				new XmlSerializer(typeof(Options));

			FileStream stream =	File.Open(
				OPTIONS_FILE,
				FileMode.Open,
				FileAccess.Read);

			Options opt;

			try
			{
				opt = serializer.Deserialize(stream) as Options;
			}
			catch
			{
				opt = new Options();
			}
			

			return opt;
		}
	}
}
