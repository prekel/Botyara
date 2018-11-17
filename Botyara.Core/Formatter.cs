using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Botyara.Core
{
	/// <summary>
	/// Представляет форматировщика, который форматирует исходную строку, подставляя значения из словаря со значениями.
	/// </summary>
	public class Formatter
	{
		/// <summary>
		/// Получает или задаёт словарь со значениями.
		/// </summary>
		public IDictionary<string, object> Data { get; set; }

		/// <summary>
		/// Получает или задаёт исходную строку.
		/// </summary>
		public string FormatString { get; set; }

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="Formatter"/> без параметров.
		/// </summary>
		public Formatter()
		{
		}

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="Formatter"/> используя заданный словарь со значениями.
		/// </summary>
		/// <param name="data">Словарь со значениями.</param>
		public Formatter(IDictionary<string, object> data)
		{
			Data = data;
		}

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="Formatter"/> используя заданный словарь со значениями и исходную строку.
		/// </summary>
		/// <param name="data">Словарь со значениями.</param>
		/// <param name="formatString">Строка, в которую подставляются значения.</param>
		public Formatter(IDictionary<string, object> data, string formatString)
		{
			Data = data;
			FormatString = formatString;
		}

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="Formatter"/> используя заданную исходную строку.
		/// </summary>
		/// <param name="formatString">Строка, в которую подставляются значения.</param>
		public Formatter(string formatString)
		{
			FormatString = formatString;
		}

		/// <summary>
		/// Форматирует, используя раннее заданные параметры.
		/// </summary>
		/// <returns>Отформатированная строка.</returns>
		public string Format()
		{
			return Format(FormatString, Data);
		}

		/// <summary>
		/// Форматирует, используя раннее заданную исходную строку и заданный словарь со значениями.
		/// </summary>
		/// <param name="data">Словарь со значениями.</param>
		/// <returns>Отформатированная строка.</returns>
		public string Format(IDictionary<string, object> data)
		{
			return Format(FormatString, data);
		}

		/// <summary>
		/// Форматирует, используя раннее заданный словарь со значениями и заданную исходную строку.
		/// </summary>
		/// <param name="formatString">Строка, в которую подставляются значения.</param>
		/// <returns>Отформатированная строка.</returns>
		public string Format(string formatString)
		{
			return Format(formatString, Data);
		}

		/// <summary>
		/// Форматирует строку без раннее заданных параметров и заданного словаря со значениями и исходной строки.
		/// </summary>
		/// <param name="formatWithNames">Строка, в которую подставляются значения.</param>
		/// <param name="data">Словарь со значениями.</param>
		/// <returns>Отформатированная строка.</returns>
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