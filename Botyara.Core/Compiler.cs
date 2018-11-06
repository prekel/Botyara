using System;
using System.Collections.Generic;
using System.Text;
using Botyara.Core.Configs;
using Botyara.SfuApi;
using VkNet.Exception;

namespace Botyara.Core
{
	public class Compiler
	{
		public ChatConfig Config { get; private set; }

		public Compiler(ChatConfig config)
		{
			Config = config;
		}

		/// <summary>
		/// Составляет расписание
		/// </summary>
		/// <param name="day">Номер дня недели, пн - 1, вс - 7</param>
		/// <param name="week">1 - нечётная, 2 - чётная</param>
		public string Compile(Day day, Week week)
		{
			var timetables = new Dictionary<string, StudyTimetable>();
			foreach (var i in Config.Targets)
			{
				var tb = new TimetableBuilder(i);
				timetables.Add(i, tb.Get());
			}

			var data = new DataDict(Config, timetables, day, week);

			var form = new Formatter(data);

			var sb = new StringBuilder();

			sb.AppendLine(form.Format(Config.FirstString));

			foreach (var i in Config.Targets)
			{
				data.CurrentTarget = i;
				sb.AppendLine(form.Format(Config.SecondString));

				var n = data.CurrentDay.Count;
				if (n == 0)
				{
					sb.AppendLine(form.Format(Config.NoLessons));
					continue;
				}

				for (var j = 0; j < n; j++)
				{
					data.CurrentLessonNumber = j + 1;
					sb.AppendLine(form.Format(Config.LessonString));
				}
			}

			return sb.ToString();
		}
	}
}