using System.Collections.Generic;
using Newtonsoft.Json;

namespace Botyara.SfuApi
{
	[JsonObject]
	public class StudyTimetable
	{
		[JsonProperty("timetable")] public List<StudyLesson> Timetable { get; private set; }
		[JsonProperty("target")] public string Target { get; private set; }
		[JsonProperty("type")] public string Type { get; private set; }
	}
}