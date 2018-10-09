using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Botyara.Core
{
	[JsonObject]
	class Schedule : Dictionary<DateTime, string>
	{
		[JsonProperty]
		public long PeerId { get; private set; }

		[JsonProperty]
		public IList<string> Targets { get; private set; }

		public Schedule()
		{

		}
	}
}
