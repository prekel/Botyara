using System;
using System.Net;
using Newtonsoft.Json;

namespace Botyara.SfuApi
{
	/// <summary>
	/// Представляет получателя расписания, используя Api СФУ.
	/// </summary>
	public class TimetableGetter
	{
		/// <summary>
		/// Получает цель (номер группы или имя преподавателя).
		/// </summary>
		public string Target { get; }

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="TimetableGetter"/> используя заданную цель.
		/// </summary>
		/// <param name="target">Цель (номер группы или имя преподавателя).</param>
		public TimetableGetter(string target)
		{
			Target = target;
		}

		/// <summary>
		/// Получает расписание.
		/// </summary>
		/// <returns>Расписание.</returns>
		public StudyTimetable Get()
		{
			var webClient = new WebClient();
			webClient.QueryString.Add("target", Target);
			var result = webClient.DownloadString("http://edu.sfu-kras.ru/api/timetable/get");

			return JsonConvert.DeserializeObject<StudyTimetable>(result);
		}
	}
}