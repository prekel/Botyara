using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Botyara.Core
{
    [JsonObject]
    public class Schedule : Dictionary<DateTime, string>
    {
        [JsonProperty]
        public long PeerId { get; private set; }

        [JsonProperty]
        public IList<string> Targets { get; private set; }
    }
}
