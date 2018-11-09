using System.Collections.Generic;
using Newtonsoft.Json;

namespace Botyara.SfuApi
{
	/// <summary>
	/// Представляет описание одной пары
	/// </summary>
	[JsonObject]
	public class StudyLesson
	{
		/// <summary>
		/// День недели
		/// </summary>
		[JsonProperty("day")] public Day Day { get; private set; }
		/// <summary>
		/// Чётная или нечётная неделя
		/// </summary>
		[JsonProperty("week")] public Week Week { get; private set; }
		/// <summary>
		/// Время
		/// </summary>
		[JsonProperty("time")] public string Time { get; private set; }
		/// <summary>
		/// Предмет
		/// </summary>
		[JsonProperty("subject")] public string Subject { get; private set; }
		/// <summary>
		/// Тип (лекция, практика и т.д.)
		/// </summary>
		[JsonProperty("type")] public string Type { get; private set; }
		/// <summary>
		/// Аудитория
		/// </summary>
		[JsonProperty("place")] public string Place { get; private set; }
		/// <summary>
		/// Преподаватель
		/// </summary>
		[JsonProperty("teacher")] public string Teacher { get; private set; }
		/// <summary>
		/// Группы
		/// </summary>
		[JsonProperty("groups")] public List<string> Groups { get; private set; }
	}
}