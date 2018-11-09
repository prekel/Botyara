using System.Collections.Generic;
using Newtonsoft.Json;

namespace Botyara.Core.Response
{
	/// <summary>
	/// Обновление от Long Poll
	/// </summary>
	[JsonObject]
	public class Update
	{
		/// <summary>
		/// Тип обновления
		/// </summary>
		[JsonProperty("type")] public string Type { get; private set; }

		/// <summary>
		/// Присланный объект
		/// </summary>
		[JsonProperty("object")] public Obj Object { get; private set; }
	}
}
