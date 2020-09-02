using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Botyara.Core
{
    public class Formatter
    {
        public Formatter()
        {
        }

        public Formatter(IDictionary<string, object> data) => Data = data;

        public Formatter(IDictionary<string, object> data, string formatString)
        {
            Data = data;
            FormatString = formatString;
        }

        public Formatter(string formatString) => FormatString = formatString;

        public IDictionary<string, object> Data { get; set; }
        public string FormatString { get; set; }

        public string Format() => Format(FormatString, Data);

        public string Format(IDictionary<string, object> data) => Format(FormatString, data);

        public string Format(string formatString) => Format(formatString, Data);

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
