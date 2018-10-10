using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Botyara.Core
{
	[JsonObject]
	public class Schedule : Dictionary<DateTime, string>
	{
		[JsonProperty]
		public long PeerId { get; private set; }

		[JsonProperty]
		public IList<string> Targets { get; private set; }

		public Schedule()
		{

		}

		public static string Format(string formatWithNames, IDictionary<string, object> data)
		{
			var pos = 0;
			var args = new List<object>();
			var fmt = Regex.Replace(
				formatWithNames, @"(?<={)[^}]+(?=})", m =>
				{
					var res = (pos++).ToString();
					var tok = m.Groups[0].Value.Split(':');
					args.Add(data[tok[0]]);
					return tok.Length == 2 ? res + ":" + tok[1] : res;
				}
			);
			return string.Format(fmt, args.ToArray());
		}
	}
}
