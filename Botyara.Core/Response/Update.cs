using System.Collections.Generic;

using Newtonsoft.Json;

namespace Botyara.Core.Response
{
	/// <summary>
	/// Представляет обновление, полученное LongPoll-запросом.
	/// </summary>
	[JsonObject]
	public class Update
	{
		/// <summary>
		/// Получает тип обновления.
		/// </summary>
		[JsonProperty("type")] public string Type { get; private set; }

		/// <summary>
		/// Получает присланный объект.
		/// </summary>
		[JsonProperty("object")] public Obj Object { get; private set; }
	}
}
