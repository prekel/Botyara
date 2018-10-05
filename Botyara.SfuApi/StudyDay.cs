using System.Collections.Generic;

namespace Botyara.SfuApi
{
	public class StudyDay
	{
		public int Day { get; set; }
		public int Week { get; set; }
		public string Time { get; set; }
		public string Subject { get; set; }
		public string Type { get; set; }
		public string Place { get; set; }

		public string Teacher { get; set; }
		public List<string> Groups { get; set; }
	}
}