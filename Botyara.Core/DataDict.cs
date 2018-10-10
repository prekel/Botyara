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
		
		public DataDict(ChatConfig config, StudyTimetable timetable)
		{
			
		}
		
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(KeyValuePair<string, object> item)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

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

		public int Count { get; }

		public bool IsReadOnly { get; } = true;

		public void Add(string key, object value)
		{
			throw new NotImplementedException();
		}

		public bool ContainsKey(string key)
		{
			throw new NotImplementedException();
		}

		public bool Remove(string key)
		{
			throw new NotImplementedException();
		}

		public bool TryGetValue(string key, out object value)
		{
			throw new NotImplementedException();
		}

		public object this[string key]
		{
			get
			{
				if (key == "OddEvenDayVinPod")
				{
					
				}
				throw new NotImplementedException();
			}
			set => throw new NotImplementedException();
		}

		public ICollection<string> Keys { get; }
		public ICollection<object> Values { get; }
	}
}