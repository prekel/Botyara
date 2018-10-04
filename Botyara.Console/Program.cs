using System;
using System.Linq;
using Botyara.SfuApi;

namespace Botyara.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var c = new TimetableBuilder("КИ18-17/1б");
			var t = c.Get();

			var q1 = (from i in t.Timetable where i.Day == 5 && i.Week == 1 select i.Subject).ToList();
		}
	}
}