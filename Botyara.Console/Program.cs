using System;
using Botyara.SfuApi;

namespace Botyara.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			System.Console.WriteLine("Hello World!");
			var c = new Request();
			c.Run();
		}
	}
}