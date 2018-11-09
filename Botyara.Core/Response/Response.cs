using System.Collections.Generic;
using Newtonsoft.Json;

namespace Botyara.Core.Response
{
	/// <summary>
	/// Ответ от Long Poll
	/// </summary>
	[JsonObject]
	public class Response
	{
		/// <summary>
		/// TimeStamp
		/// </summary>
		[JsonProperty("ts")] public int? TimeStamp { get; private set; }

		/// <summary>
		/// Обновления
		/// </summary>
		[JsonProperty("updates")] public IList<Update> Updates { get; private set; }

		/// <summary>
		/// Если неудача
		/// </summary>
		[JsonProperty("failed")] public string Failed { get; private set; }
	}
}
