using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Botyara.SfuApi
{
	public class TimetableBuilder
	{
		public string Target { get; private set; }

		public TimetableBuilder()
		{
		}

		public TimetableBuilder(string target)
		{
			Target = target;
		}

		public void BuildTarget(string target)
		{
			Target = target;
		}

		public StudyTimetable Get()
		{
			var webClient = new WebClient();
			webClient.QueryString.Add("target", Target);
			var result = webClient.DownloadString("http://edu.sfu-kras.ru/api/timetable/get");

			return JsonConvert.DeserializeObject<StudyTimetable>(result);
		}
	}
}