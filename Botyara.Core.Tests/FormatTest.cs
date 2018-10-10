using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

using  Botyara.Core;

namespace Botyara.Core.Tests
{
    public class FormatTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
	        var dict = new Dictionary<string, object>
			{
				["abc"] = "abc1",
				["xyz"] = 1
			};
	        var istr = "abc: {abc}, xyz: {xyz}";
	        var res = Botyara.Core.Schedule.Format(istr, dict);
			Assert.AreEqual($"abc: {dict["abc"]}, xyz: {dict["xyz"]}", res);
        }
    }
}