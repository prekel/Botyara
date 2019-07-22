using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Botyara.Core.Response
{
	[JsonObject]
	public class Update
	{
		[JsonProperty("type")] public string Type { get; private set; }

		[JsonProperty("object")] public Obj Object { get; private set; }


        /// <summary>
        /// Объект, инициировавший событие
        /// Структура объекта зависит от типа уведомления
        /// </summary>
        [JsonProperty("object")]
        public JObject Object1 { get; set; }
    }
}