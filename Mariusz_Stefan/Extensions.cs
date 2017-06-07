using System;

namespace Mariusz_Stefan
{
	public static class Extensions
	{
		public static string RandomChoice(string[] source)
		{
			var rnd = new Random();
			return source[rnd.Next(0, source.Length)].Trim();
        }
        public static string RandomChoiceNext(string[] source)
        {
            var rnd = new Random((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + 666);
            return source[rnd.Next(0, source.Length)].Trim();
        }
    }
}
