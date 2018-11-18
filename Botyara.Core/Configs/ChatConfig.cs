using System.Collections.Generic;

using Newtonsoft.Json;

namespace Botyara.Core.Configs
{
	/// <summary>
	/// Представляет конфигурацию чата.
	/// </summary>
	[JsonObject]
	public class ChatConfig
	{
		/// <summary>
		/// Получает или задаёт идентификатор беседы или собеседника.
		/// </summary>
		[JsonProperty]
		public long PeerId { get; set; }

		/// <summary>
		/// Получает или задаёт цели.
		/// </summary>
		[JsonProperty]
		public IList<string> Targets { get; set; }

		/// <summary>
		/// Получает или задаёт первую строку составляемого расписания.
		/// </summary>
		/// <remarks>
		/// Например если она <c>"Расписание на {OddEvenDayVinPod}:"</c>, то в составленном расписании будет:
		/// Расписание на нечётный вторник:
		/// </remarks>
		[JsonProperty]
		public string FirstString { get; set; }

		/// <summary>
		/// Получает или задаёт вторую строку составляемого расписания.
		/// </summary>
		/// <remarks>
		/// Например если она <c>"Группа {Target}:"</c>, то в составленном расписании будет:
		/// Группа КИ18-17/1б:
		/// </remarks>
		[JsonProperty]
		public string SecondString { get; set; }

		/// <summary>
		/// Получает или задаёт строку, в которой написана информация про одну пару для составляемого расписания.
		/// </summary>
		/// <remarks>
		/// Например если она <c>"{NumberInTimetable}) {Time} {Subject} ({Type}) {Teacher} {Place}"</c>, то в составленном расписании будет:
		/// 1) 08:30-10:05 Введение в инженерную деятельность (лекция) Евдокимов И. В. УЛК115
		/// </remarks>
		[JsonProperty]
		public string LessonString { get; set; }

		/// <summary>
		/// Получает или задаёт строку, которая выводится если в этот день занятий нет.
		/// </summary>
		[JsonProperty]
		public string NoLessons { get; set; }
	}
}