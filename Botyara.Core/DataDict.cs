using System;
using System.Collections;
using System.Collections.Generic;
using Botyara.Core.Configs;
using Botyara.SfuApi;

namespace Botyara.Core
{
	public class DataDict : IDictionary<string, object>
	{
		public ChatConfig Config { get; private set; }
		public StudyTimetable Timetable { get; private set; }
		public Day Day { get; private set; }
		public Week Week { get; private set; }

		public int CurrentTarget { get; set; }
		public int CurrentLesson { get; set; }

		public DataDict(ChatConfig config, StudyTimetable timetable, Day day, Week week)
		{
			Config = config;
			Timetable = timetable;
			Day = day;
			Week = week;
		}

		public int Count { get; }
		public bool IsReadOnly { get; } = true;

		public object this[string key]
		{
			get
			{
				switch (key)
				{
					case "OddEvenDayVinPod":
						return 1;
					case "NumberInOrder":
						return 2;
					default:
						throw new NotImplementedException();
				}
			}
			set => throw new NotImplementedException();
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