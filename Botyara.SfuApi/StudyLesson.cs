using System.Collections.Generic;
using Newtonsoft.Json;

namespace Botyara.SfuApi
{
	/// <summary>
	/// Представляет описание одной пары.
	/// </summary>
	[JsonObject]
	public class StudyLesson
	{
		/// <summary>
		/// Получает день недели.
		/// </summary>
		[JsonProperty("day")]
		public Day Day { get; private set; }

		/// <summary>
		/// Получает чётную или нечётную неделя.
		/// </summary>
		[JsonProperty("week")]
		public Week Week { get; private set; }

		/// <summary>
		/// Получает время.
		/// </summary>
		[JsonProperty("time")]
		public string Time { get; private set; }

		/// <summary>
		/// Получает предмет.
		/// </summary>
		[JsonProperty("subject")]
		public string Subject { get; private set; }

		/// <summary>
		/// Получает тип (лекция, практика и т.д.).
		/// </summary>
		[JsonProperty("type")]
		public string Type { get; private set; }

		/// <summary>
		/// Получает аудиторию.
		/// </summary>
		[JsonProperty("place")]
		public string Place { get; private set; }

		/// <summary>
		/// Получает преподавателя.
		/// </summary>
		/// <remarks>
		///	Если цель - группа.
		/// </remarks>
		[JsonProperty("teacher")]
		public string Teacher { get; private set; }

		/// <summary>
		/// Получает группы.
		/// </summary>
		/// <remarks>
		/// Если цель - преподаватель.
		/// </remarks>
		[JsonProperty("groups")]
		public List<string> Groups { get; private set; }
	}
}