using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Botyara.Core
{
	/// <summary>
	/// Форматирует отформатированную строку, искользуя исходную строку, куда подставляются значения и словарь со значениями
	/// </summary>
	/// <example></example>
	public class Formatter
	{
		/// <summary>
		/// Словарь со значениями
		/// </summary>
		public IDictionary<string, object> Data { get; set; }
		/// <summary>
		/// Исходная строка
		/// </summary>
		public string FormatString { get; set; }

		/// <summary>
		/// Создаёт форматировщика без параметров
		/// </summary>
		public Formatter()
		{
		}
		
		/// <summary>
		/// Создаёт форматировщика
		/// </summary>
		/// <param name="data">Словарь со значениями</param>
		public Formatter(IDictionary<string, object> data)
		{
			Data = data;
		}
		
		/// <summary>
		/// Создаёт форматировщика
		/// </summary>
		/// <param name="data">Словарь со значениями</param>
		/// <param name="formatString">Строка, в которую подставляются значения</param>
		public Formatter(IDictionary<string, object> data, string formatString)
		{
			Data = data;
			FormatString = formatString;
		}
		
		/// <summary>
		/// Создаёт форматировщика
		/// </summary>
		/// <param name="formatString">Строка, в которую подставляются значения</param>
		public Formatter(string formatString)
		{
			FormatString = formatString;
		}

		/// <summary>
		/// Форматирует, используя раннее заданные параметры
		/// </summary>
		/// <returns>Отформатированная строка</returns>
		public string Format()
		{
			return Format(FormatString, Data);
		}

		/// <summary>
		/// Форматирует, используя раннее заданную исходную строку
		/// </summary>
		/// <param name="data">Словарь со значениями</param>
		/// <returns>Отформатированная строка</returns>
		public string Format(IDictionary<string, object> data)
		{
			return Format(FormatString, data);
		}

		/// <summary>
		/// Форматирует, используя раннее заданный словарь со значениями
		/// </summary>
		/// <param name="formatString">Строка, в которую подставляются значения</param>
		/// <returns>Отформатированная строка</returns>
		public string Format(string formatString)
		{
			return Format(formatString, Data);
		}

		/// <summary>
		/// Форматирует строку без раннее заданных параметров
		/// </summary>
		/// <param name="formatWithNames">Строка, в которую подставляются значения</param>
		/// <param name="data">Словарь со значениями</param>
		/// <returns></returns>
		public static string Format(string formatWithNames, IDictionary<string, object> data)
		{
			var pos = 0;
			var args = new List<object>();
			var fmt = Regex.Replace(
				formatWithNames, @"(?<={)[^}]+(?=})", m =>
				{
					var res = pos++.ToString();
					var tok = m.Groups[0].Value.Split(':');
					args.Add(data[tok[0]]);
					return tok.Length == 2 ? res + ":" + tok[1] : res;
				}
			);
			return String.Format(fmt, args.ToArray());
		}
	}
}