using System;
using System.IO;
using Botyara.Console;
using System.Linq;
using Botyara.SfuApi;
using Botyara.Core;
using static System.Console;

namespace Botyara.Console
{
	class Program
	{
		static void Main(string[] args)
		{
//			var c = new TimetableBuilder("КИ18-17/1б");
//			var t = c.Get();
//			var q1 = (from i in t.Timetable where i.Day == 5 && i.Week == 1 select i.Subject).ToList();
//		
			string token; 
			using (var r = new StreamReader("token.txt"))
			{
				token = r.ReadLine();
			}
			var auth = new Authorizer(token);
//			var t1 = new Test(auth.Api);
//			t1.Run();
			
			var lp = new LongPoller(auth.Api, 172122256);
			lp.ResponseReceived += LpOnResponseReceived;
			lp.Start();
			lp.Run();
		}

		private static void LpOnResponseReceived(object sender, EventArgs e)
		{
			var lpe = (LongPollResponseEventArgs) e;
			WriteLine(lpe.Response.RawJson);
		}
	}
}
