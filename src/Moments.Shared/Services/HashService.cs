using System;
using System.IO;

namespace Moments
{
	public class HashService
	{
		public static string CalculateMD5Hash (string s)
		{
			var md5 = MD5.Create ();
			var stream = GenerateStreamFromString (s);

			return BitConverter.ToString (md5.ComputeHash (stream)).Replace ("-", "").ToLower ();
		}

		private static Stream GenerateStreamFromString(string s)
		{
			var stream = new MemoryStream ();
			var writer = new StreamWriter (stream);

			writer.Write(s);
			writer.Flush();
			stream.Position = 0;

			return stream;
		}
	}
}