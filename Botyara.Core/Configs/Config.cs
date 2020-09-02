using System.Collections.Generic;

using Newtonsoft.Json;

namespace Botyara.Core.Configs
{
    [JsonObject]
    public class Config
    {
        [JsonProperty]
        public string AccessToken { get; set; }

        [JsonProperty]
        public ulong GroupId { get; set; }

        [JsonProperty]
        public IList<ChatConfig> ChatConfigs { get; set; }
    }
}
