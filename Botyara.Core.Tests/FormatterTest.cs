using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

using Botyara.Core;

namespace Botyara.Core.Tests
{
	/// <summary>
	/// Тесты для форматировщика
	/// </summary>
    public class FormatterTests
    {
		/// <summary>
		/// Тест форматировщика
		/// Из строки "abc: {abc}, xyz: {xyz}" должна получиться "abc: {abc1}, xyz: {1}"
		/// </summary>
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
