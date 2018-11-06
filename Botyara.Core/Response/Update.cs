using System.Collections.Generic;
using Newtonsoft.Json;

namespace Botyara.Core.Response
{
	[JsonObject]
	public class Update
	{
		[JsonProperty("type")] public string Type { get; private set; }

		[JsonProperty("object")] public Obj Object { get; private set; }
	}
}