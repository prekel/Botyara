using System;
using System.IO;
using Botyara.Console;
using Botyara.Core;

namespace Botyara.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			string token; 
			using (var r = new StreamReader("token.txt"))
			{
				token = r.ReadLine();
			}
			var auth = new Authorizer(token);
			var t = new Test(auth.Api);
			t.Run();
		}
	}
}