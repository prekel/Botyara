using System;
using System.Collections.Generic;
using System.Text;

using Botyara.Core.Configs;
using Botyara.SfuApi;

namespace Botyara.Core
{
	/// <summary>
	/// Представляет составителя отформатированного расписания.
	/// </summary>
	public class Compiler
	{
		/// <summary>
		/// Задаёт конфигурацию чата.
		/// </summary>
		public ChatConfig Config { get; private set; }

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="Compiler"/> используя заданную конфигурацию чата.
		/// </summary>
		/// <param name="config">Конфигурация чата.</param>
		public Compiler(ChatConfig config)
		{
			Config = config;
		}

		/// <summary>
		/// Составляет расписание.
		/// </summary>
		/// <param name="day">День недели.</param>
		/// <param name="week">Чётная или нечётная неделя.</param>
		/// <returns>Отформатированное расписание.</returns>
		public string Compile(Day day, Week week)
		{
			var timetables = new Dictionary<string, StudyTimetable>();
			foreach (var i in Config.Targets)
			{
				var tb = new TimetableGetter(i);
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