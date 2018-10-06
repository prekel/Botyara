using System.Collections.Generic;

namespace Botyara.SfuApi
{
	public class StudyTimetable
	{
		public List<StudyDay> Timetable { get; set; }
		public string Target { get; set; }
		public string Type { get; set; }
	}
}