using System.Collections.Generic;
using Newtonsoft.Json;

namespace Botyara.Core.Configs
{
	[JsonObject]
	public class ChatConfig
	{
		/// <summary>
		/// ID беседы
		/// </summary>
		[JsonProperty]
		public long PeerId { get; set; }
		
		/// <summary>
		/// Номера групп
		/// </summary>
		[JsonProperty]
		public IList<string> Targets { get; set; }
		
		/// <summary>
		/// "Расписание на {OddEvenDayVinPod}:"
		/// Расписание на чётный понедельник:
		/// </summary>
		[JsonProperty]
		public string FirstString { get; set; }
		
		/// <summary>
		/// "Группа {TargetsList}:"
		/// Группа КИ18-17/1б:
		/// </summary>
		[JsonProperty]
		public string SecondString { get; set; }
		
		/// <summary>
		/// "{NumberInTimetable}) {Time} {Subject} ({Type}) {Teacher} {Place}"
		/// 2) 10:15-11:50 Основы программирования (практика) Грузенкин Д. В. УЛК423
		/// </summary>
		[JsonProperty]
		public string LessonString { get; set; }
	}
}