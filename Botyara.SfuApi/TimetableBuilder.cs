using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Botyara.SfuApi
{
	/// <summary>
	/// Класс, который служит для получения расписания
	/// </summary>
	public class TimetableBuilder
	{
		/// <summary>
		/// Цель (номер группы или преподаватель)
		/// </summary>
		public string Target { get; private set; }

		/// <summary>
		/// Создаёт новый TimetableBuilder
		/// </summary>
		public TimetableBuilder()
		{
		}

		/// <summary>
		/// Создаёт новый TimetableBuilder
		/// </summary>
		/// <param name="target">Цель (номер группы или преподаватель)</param>
		public TimetableBuilder(string target)
		{
			Target = target;
		}

		/// <summary>
		/// Устанавливает значения цели
		/// </summary>
		/// <param name="target">Цель (номер группы или преподаватель)</param>
		public void BuildTarget(string target)
		{
			Target = target;
		}

		/// <summary>
		/// Получает расписание
		/// </summary>
		/// <returns>Расписание</returns>
		public StudyTimetable Get()
		{
			var webClient = new WebClient();
			webClient.QueryString.Add("target", Target);
			var result = webClient.DownloadString("http://edu.sfu-kras.ru/api/timetable/get");

			return JsonConvert.DeserializeObject<StudyTimetable>(result);
		}
	}
}