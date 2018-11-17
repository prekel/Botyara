using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Botyara.Core.Configs;
using Botyara.SfuApi;

namespace Botyara.Core
{
	/// <summary>
	/// Представляет словарь значений (название дня, название предмета и т. д.) для составление отформатированного расписания.
	/// </summary>
	public class DataDict : IDictionary<string, object>
	{
		/// <summary>
		/// Получает конфигурация чата.
		/// </summary>
		public ChatConfig Config { get; private set; }

		/// <summary>
		/// Получает расписания групп.
		/// </summary>
		public IDictionary<string, StudyTimetable> Timetables { get; private set; }

		/// <summary>
		/// Получает день недели.
		/// </summary>
		public Day Day { get; private set; }

		/// <summary>
		/// Получает чётная или нечётную неделю.
		/// </summary>
		public Week Week { get; private set; }

		/// <summary>
		/// Получает или задаёт текущую цель (номер группы или имя преподавателя).
		/// </summary>
		public string CurrentTarget { get; set; }

		/// <summary>
		/// Получает или задаёт номер текущей пары.
		/// </summary>
		public int CurrentLessonNumber { get; set; }

		/// <summary>
		/// Получает текущий учебный день.
		/// </summary>
		public IList<StudyLesson> CurrentDay =>
			(from value in Timetables[CurrentTarget].Timetable
			 where value.Day == Day && value.Week == Week
			 select value).ToList();

		/// <summary>
		/// Получает текущую пару.
		/// </summary>
		public StudyLesson CurrentLesson => CurrentDay[CurrentLessonNumber - 1];

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="DataDict"/>.
		/// </summary>
		/// <param name="config">Конфигурация чата.</param>
		/// <param name="timetables">Расписания.</param>
		/// <param name="day">День недели.</param>
		/// <param name="week">Чётная или нечётная неделя.</param>
		// ReSharper disable once NotNullMemberIsNotInitialized
		public DataDict(ChatConfig config, IDictionary<string, StudyTimetable> timetables, Day day, Week week)
		{
			Config = config;
			Timetables = timetables;
			Day = day;
			Week = week;
		}

		//[Obsolete]
		//public int Count { get; }

		/// <inheritdoc />
		/// <summary>
		/// Уведомляет о том, что изменять значения по ключу нельзя.
		/// </summary>
		public bool IsReadOnly { get; } = true;

		private string c_OddEvenDayVinPod()
		{
			return DayNameVinPod(Day, Week);
		}

		/// <summary>
		/// Возвращает название дня недели вместе с чётной/нечётной неделей и винительном падеже.
		/// </summary>
		/// <param name="day">День недели.</param>
		/// <param name="week">Чётная/нечётная неделя.</param>
		/// <returns>Название дня недели вместе с чётной/нечётной неделей и винительном падеже.</returns>
		public static string DayNameVinPod(Day day, Week week)
		{
			switch (week)
			{
				case Week.Even when day == Day.Monday:
					return "чётный понедельник";
				case Week.Even when day == Day.Tuesday:
					return "чётный вторник";
				case Week.Even when day == Day.Wednesday:
					return "чётную среду";
				case Week.Even when day == Day.Thursday:
					return "чётный четверг";
				case Week.Even when day == Day.Friday:
					return "чётную пятницу";
				case Week.Even when day == Day.Saturday:
					return "чётную субботу";
				case Week.Even when day == Day.Sunday:
					return "чётное воскресенья";
				case Week.Odd when day == Day.Monday:
					return "нечётный понедельник";
				case Week.Odd when day == Day.Tuesday:
					return "нечётный вторник";
				case Week.Odd when day == Day.Wednesday:
					return "нечётную среду";
				case Week.Odd when day == Day.Thursday:
					return "нечётный четверг";
				case Week.Odd when day == Day.Friday:
					return "нечётную пятницу";
				case Week.Odd when day == Day.Saturday:
					return "нечётную субботу";
				case Week.Odd when day == Day.Sunday:
					return "нечётное воскресенье";
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private string c_Target()
		{
			return CurrentTarget;
		}

		private string c_TargetsList()
		{
			return String.Join(", ", Config.Targets);
		}

		private int c_NumberInTimetable()
		{
			var time = CurrentLesson.Time;
			if (time == "08:30-10:05")
				return 1;
			if (time == "10:15-11:50")
				return 2;
			if (time == "12:00-13:35")
				return 3;
			if (time == "14:10-15:45")
				return 4;
			if (time == "15:55-17:30")
				return 5;
			if (time == "17:40-19:15")
				return 6;
			if (time == "19:25-21:00")
				return 7;
			return 0;
		}

		private int c_NumberInOrder()
		{
			return CurrentLessonNumber;
		}

		private string c_Time()
		{
			return CurrentLesson.Time;
		}

		private string c_Subject()
		{
			return CurrentLesson.Subject;
		}

		private string c_Type()
		{
			return CurrentLesson.Type;
		}

		private string c_Teacher()
		{
			return CurrentLesson.Teacher;
		}

		private string c_Place()
		{
			return CurrentLesson.Place;
		}

		/// <summary>
		/// Возвращает значение по заданному ключу.
		/// </summary>
		/// <remarks>
		/// <para><c>OddEvenDayVinPod</c> - Название дня недели вместе с чётной/нечётной неделей и винительном падеже</para>
		/// <para><c>Target</c> - Название цели (номер группы или преподаватель)</para>
		/// <para><c>TargetsList</c> - Цели через запятую</para>
		/// <para><c>NumberInTimetable</c> - Номер пары в расписании пар</para>
		/// <para><c>NumberInOrder</c> - Номер пары по порядку</para>
		/// <para><c>Time</c> - Время пары</para>
		/// <para><c>Subject</c> - Предмет</para>
		/// <para><c>Type</c> - Тип (лекция, практика и т. д.)</para>
		/// <para><c>Teacher</c> - Преподаватель</para>
		/// <para><c>Place</c> - Аудитория</para>
		/// </remarks>
		/// <param name="key">Описание ключей в описании функции.</param>
		/// <returns>Строка или число, представляющее запрашиваемое значение.</returns>
		public object this[string key]
		{
			get
			{
				switch (key)
				{
					case "OddEvenDayVinPod":
						return c_OddEvenDayVinPod();
					case "Target":
						return c_Target();
					case "TargetsList":
						return c_TargetsList();
					case "NumberInTimetable":
						return c_NumberInTimetable();
					case "NumberInOrder":
						return c_NumberInOrder();
					case "Time":
						return c_Time();
					case "Subject":
						return c_Subject();
					case "Type":
						return c_Type();
					case "Teacher":
						return c_Teacher();
					case "Place":
						return c_Place();
					default:
						throw new NotImplementedException();
				}
			}
			set => throw new NotSupportedException();
		}

		#region NotUsed

		[Obsolete]
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		[Obsolete]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		[Obsolete]
		public void Add(KeyValuePair<string, object> item)
		{
			throw new NotImplementedException();
		}

		[Obsolete]
		public void Clear()
		{
			throw new NotImplementedException();
		}

		[Obsolete]
		public bool Contains(KeyValuePair<string, object> item)
		{
			throw new NotImplementedException();
		}

		[Obsolete]
		public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		[Obsolete]
		public bool Remove(KeyValuePair<string, object> item)
		{
			throw new NotImplementedException();
		}

		[Obsolete]
		public int Count { get; }

		[Obsolete]
		public void Add(string key, object value)
		{
			throw new NotImplementedException();
		}

		[Obsolete]
		public bool ContainsKey(string key)
		{
			throw new NotImplementedException();
		}

		[Obsolete]
		public bool Remove(string key)
		{
			throw new NotImplementedException();
		}

		[Obsolete]
		public bool TryGetValue(string key, out object value)
		{
			throw new NotImplementedException();
		}

		[Obsolete] public ICollection<string> Keys { get; }
		[Obsolete] public ICollection<object> Values { get; }

		#endregion
	}
}
