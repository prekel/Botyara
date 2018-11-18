using System.Collections.Generic;

using Newtonsoft.Json;

namespace Botyara.Core.Response
{
	/// <summary>
	/// Представляет ответ, полученный LongPoll-запросом.
	/// </summary>
	[JsonObject]
	public class Response
	{
		/// <summary>
		/// Получает TimeStamp.
		/// </summary>
		[JsonProperty("ts")]
		public int? TimeStamp { get; private set; }

		/// <summary>
		/// Получает обновления.
		/// </summary>
		[JsonProperty("updates")]
		public IList<Update> Updates { get; private set; }

		/// <summary>
		/// Получает значение того, есть ли неудача.
		/// </summary>
		[JsonProperty("failed")]
		public string Failed { get; private set; }
	}
}
