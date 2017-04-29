using System;

namespace Mariusz_Stefan
{
	public static class Extensions
	{
		public static string RandomChoice(string[] source)
		{
			var rnd = new Random();
			return source[rnd.Next(0, source.Length)];
		}
	}
}
