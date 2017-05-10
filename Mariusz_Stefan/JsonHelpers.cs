using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariusz_Stefan
{
	public class Key
	{
		public string name { get; set; }
		public string key { get; set; }
	}

	public class RootObject
	{
		public List<Key> Keys { get; set; }
	}
}
