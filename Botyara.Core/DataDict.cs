using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Botyara.Core.Configs;
using Botyara.SfuApi;

namespace Botyara.Core
{
    public class DataDict : IDictionary<string, object>
    {
        public DataDict(ChatConfig config, IDictionary<string, StudyTimetable> timetables, Day day, Week week)
        {
            Config = config;
            Timetables = timetables;
            Day = day;
            Week = week;
        }

        public ChatConfig Config { get; }
        public IDictionary<string, StudyTimetable> Timetables { get; }
        public Day Day { get; }
        public Week Week { get; }

        public string CurrentTarget { get; set; }
        public int CurrentLessonNumber { get; set; }

        public IList<StudyLesson> CurrentDay =>
            (from value in Timetables[CurrentTarget].Timetable
                where value.Day == Day && value.Week == Week
                select value).ToList();

        public StudyLesson CurrentLesson => CurrentDay[CurrentLessonNumber - 1];

        public int Count { get; }
        public bool IsReadOnly { get; } = true;

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
            set => throw new NotImplementedException();
        }

        private string c_OddEvenDayVinPod() => DayNameVinPod(Day, Week);

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

        private string c_Target() => CurrentTarget;

        private string c_TargetsList() => String.Join(", ", Config.Targets);

        private int c_NumberInTimetable()
        {
            var time = CurrentLesson.Time;
            if (time == "08:30-10:05")
            {
                return 1;
            }

            if (time == "10:15-11:50")
            {
                return 2;
            }

            if (time == "12:00-13:35")
            {
                return 3;
            }

            if (time == "14:10-15:45")
            {
                return 4;
            }

            if (time == "15:55-17:30")
            {
                return 5;
            }

            if (time == "17:40-19:15")
            {
                return 6;
            }

            if (time == "19:25-21:00")
            {
                return 7;
            }

            return 0;
        }

        private int c_NumberInOrder() => CurrentLessonNumber;

        private string c_Time() => CurrentLesson.Time;

        private string c_Subject() => CurrentLesson.Subject;

        private string c_Type() => CurrentLesson.Type;

        private string c_Teacher() => CurrentLesson.Teacher;

        private string c_Place() => CurrentLesson.Place;

        #region NotUsed

        [Obsolete]
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => throw new NotImplementedException();

        [Obsolete]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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
        public bool Contains(KeyValuePair<string, object> item) => throw new NotImplementedException();

        [Obsolete]
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        public bool Remove(KeyValuePair<string, object> item) => throw new NotImplementedException();

        [Obsolete]
        public void Add(string key, object value)
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        public bool ContainsKey(string key) => throw new NotImplementedException();

        [Obsolete]
        public bool Remove(string key) => throw new NotImplementedException();

        [Obsolete]
        public bool TryGetValue(string key, out object value) => throw new NotImplementedException();

        [Obsolete]
        public ICollection<string> Keys { get; }

        [Obsolete]
        public ICollection<object> Values { get; }

        #endregion
    }
}
