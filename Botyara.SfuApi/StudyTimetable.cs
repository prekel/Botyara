using System.Collections.Generic;

using Newtonsoft.Json;

namespace Botyara.SfuApi
{
	/// <summary>
	/// Представляет расписание одной цели.
	/// </summary>
	[JsonObject]
	public class StudyTimetable
	{
		/// <summary>
		/// Получает список пар.
		/// </summary>
		[JsonProperty("timetable")]
		public List<StudyLesson> Timetable { get; private set; }

		/// <summary>
		/// Получает цель (номер группы или имя преподавателя).
		/// </summary>
		[JsonProperty("target")]
		public string Target { get; private set; }

		/// <summary>
		/// Получает тип.
		/// </summary>
		/// <remarks>
		/// <c>group</c> или <c>teacher</c>.
		/// </remarks>
		[JsonProperty("type")]
		public string Type { get; private set; }
	}
}