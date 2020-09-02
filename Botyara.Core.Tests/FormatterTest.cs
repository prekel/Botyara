using System.Collections.Generic;

using NUnit.Framework;

namespace Botyara.Core.Tests
{
    public class FormatterTests
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
            var form = new Formatter(dict, istr);
            var res = form.Format();
            Assert.AreEqual($"abc: {dict["abc"]}, xyz: {dict["xyz"]}", res);
        }
    }
}
