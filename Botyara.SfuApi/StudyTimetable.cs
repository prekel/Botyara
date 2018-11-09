using System.Collections.Generic;
using Newtonsoft.Json;

namespace Botyara.SfuApi
{
	/// <summary>
	/// Представляет расписание одной цели
	/// </summary>
	[JsonObject]
	public class StudyTimetable
	{
		/// <summary>
		/// Список пар
		/// </summary>
		[JsonProperty("timetable")] public List<StudyLesson> Timetable { get; private set; }
		/// <summary>
		/// Цель (номер группы или преподаватель)
		/// </summary>
		[JsonProperty("target")] public string Target { get; private set; }
		/// <summary>
		/// Тип - group или teacher
		/// </summary>
		[JsonProperty("type")] public string Type { get; private set; }
	}
}