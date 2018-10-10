using System.Collections.Generic;
using Newtonsoft.Json;

namespace Botyara.SfuApi
{
	[JsonObject]
	public class StudyDay
	{
		[JsonProperty("day")] public Day Day { get; private set; }
		[JsonProperty("week")] public Week Week { get; private set; }
		[JsonProperty("time")] public string Time { get; private set; }
		[JsonProperty("subject")] public string Subject { get; private set; }
		[JsonProperty("type")] public string Type { get; private set; }
		[JsonProperty("place")] public string Place { get; private set; }
		[JsonProperty("teacher")] public string Teacher { get; private set; }
		[JsonProperty("groups")] public List<string> Groups { get; private set; }
	}
}