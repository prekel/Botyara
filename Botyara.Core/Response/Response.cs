using System.Collections.Generic;

using Newtonsoft.Json;

namespace Botyara.Core.Response
{
    [JsonObject]
    public class Response
    {
        [JsonProperty("ts")]
        public int? TimeStamp { get; private set; }

        [JsonProperty("updates")]
        public IList<Update> Updates { get; private set; }

        [JsonProperty("failed")]
        public string Failed { get; private set; }
    }
}
