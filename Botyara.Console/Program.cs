using System;
using Botyara.SfuApi;

namespace Botyara.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			//System.Console.WriteLine("Hello World!");
			var c = new TimetableBuilder("КИ18-17/1б");
			var t = c.Get();
			
			
		}
	}
}