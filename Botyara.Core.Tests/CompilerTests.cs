using System.Collections.Generic;

using Botyara.Core.Configs;
using Botyara.SfuApi;

using NUnit.Framework;

namespace Botyara.Core.Tests
{
    [TestFixture]
    public class CompilerTests
    {
        [SetUp]
        public void Setup()
        {
            Config1 = new ChatConfig
            {
                FirstString = "Расписание на {OddEvenDayVinPod}:",
                SecondString = "Группа {Target}:",
                LessonString = "{NumberInTimetable}) {Time} {Subject} ({Type}) {Teacher} {Place}",
                NoLessons = "Нет пар",
                PeerId = 0,
                Targets = new List<string>(new[] {"КИ18-17/1б"})
            };
            Compiler1 = new Compiler(Config1);

            Config2 = new ChatConfig
            {
                FirstString = "Расписание на {OddEvenDayVinPod}:",
                SecondString = "Группа {Target}:",
                LessonString = "{NumberInTimetable}) {Time} {Subject} ({Type}) {Teacher} {Place}",
                NoLessons = "Нет пар",
                PeerId = 0,
                Targets = new List<string>(new[] {"КИ18-17/1б", "КИ18-17/2б"})
            };
            Compiler2 = new Compiler(Config2);
        }

        private ChatConfig Config1 { get; set; }
        private Compiler Compiler1 { get; set; }

        private ChatConfig Config2 { get; set; }
        private Compiler Compiler2 { get; set; }

        [Test]
        public void Test1_12()
        {
            var res = Compiler1.Compile(Day.Monday, Week.Even);
            var exc = @"Расписание на чётный понедельник:
Группа КИ18-17/1б:
1) 08:30-10:05 Введение в инженерную деятельность (практика) Пересунько П. В. пр./УЛК410, УЛК306
2) 10:15-11:50 Основы программирования (практика) Грузенкин Д. В. УЛК423
";
            Assert.AreEqual(exc, res);
        }

        [Test]
        public void Test1_32()
        {
            var res = Compiler1.Compile(Day.Wednesday, Week.Even);
            var exc = @"Расписание на чётную среду:
Группа КИ18-17/1б:
Нет пар
";
            Assert.AreEqual(exc, res);
        }

        [Test]
        public void Test2_12()
        {
            var res = Compiler2.Compile(Day.Monday, Week.Even);
            var exc = @"Расписание на чётный понедельник:
Группа КИ18-17/1б:
1) 08:30-10:05 Введение в инженерную деятельность (практика) Пересунько П. В. пр./УЛК410, УЛК306
2) 10:15-11:50 Основы программирования (практика) Грузенкин Д. В. УЛК423
Группа КИ18-17/2б:
1) 08:30-10:05 Математический анализ (практика) Белько Е. С. УЛК512
2) 10:15-11:50 Введение в инженерную деятельность (практика) Пересунько П. В. пр./УЛК410, УЛК306
3) 12:00-13:35 Основы программирования (практика) Черниговский А. С. УЛК423
";
            Assert.AreEqual(exc, res);
        }
    }
}
