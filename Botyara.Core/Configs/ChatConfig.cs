using System.Collections.Generic;
using Newtonsoft.Json;

namespace Botyara.Core.Configs
{
	/// <summary>
	/// Конфигурация чата
	/// </summary>
	[JsonObject]
	public class ChatConfig
	{
		/// <summary>
		/// Идентификатор беседы или собеседника
		/// </summary>
		[JsonProperty]
		public long PeerId { get; set; }
		
		/// <summary>
		/// Цели
		/// </summary>
		[JsonProperty]
		public IList<string> Targets { get; set; }
		
		/// <summary>
		/// Первая строка составляемого расписания
		/// Например если она "Расписание на {OddEvenDayVinPod}:", то в составленном расписании будет:
		/// Расписание на чётный понедельник:
		/// </summary>
		[JsonProperty]
		public string FirstString { get; set; }

		/// <summary>
		/// Вторая строка составляемого расписания
		/// Например если она "Группа {Target}:", то в составленном расписании будет:
		/// Группа КИ18-17/1б:
		/// </summary>
		[JsonProperty]
		public string SecondString { get; set; }

		/// <summary>
		/// Строка, в которой написана информация про одну пару для составляемого расписания
		/// Например если она "{NumberInTimetable}) {Time} {Subject} ({Type}) {Teacher} {Place}", то в составленном расписании будет:
		/// 2) 10:15-11:50 Основы программирования (практика) Грузенкин Д. В. УЛК423
		/// </summary>
		[JsonProperty]
		public string LessonString { get; set; }
		
		/// <summary>
		/// Строка, которая выводится если в этот день занятий нет
		/// </summary>
		[JsonProperty]
		public string NoLessons { get; set; }
	}
}