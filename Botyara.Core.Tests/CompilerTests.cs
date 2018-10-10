using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Botyara.Core;
using Botyara.Core.Configs;
using Botyara.SfuApi;

namespace Botyara.Core.Tests
{
	[TestFixture]
	public class CompilerTests
	{
		private ChatConfig Config { get; set; }
		private Compiler Compiler{ get; set; }

		[SetUp]
		public void Setup()
		{
			Config = new ChatConfig
			{
				FirstString = "Расписание на {OddEvenDayVinPod}:",
				SecondString = "Группа {TargetsList}:",
				LessonString = "{NumberInTimetable}) {Time} {Subject} ({Type}) {Teacher} {Place}",
				PeerId = 0,
				Targets = new List<string>(new[] {"КИ18-17/1б"})
			};
			Compiler = new Compiler(Config);
		}

		[Test]
		public void Test12()
		{
			var res = Compiler.Compile(Day.Monday, Week.Even);
			var exc = @"Расписание на чётный понедельник:
Группа КИ18-17/1б:
1) 08:30-10:05 Введение в инженерную деятельность (практика) Пересунько П. В. пр./УЛК410, УЛК306
2) 10:15-11:50 Основы программирования (практика) Грузенкин Д. В. УЛК423
";
			Assert.AreEqual(exc, res);
		}
	}
}