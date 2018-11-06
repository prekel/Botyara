using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Botyara.Core.Response
{
	[JsonObject]
	public class Obj
	{
		[JsonProperty("date")] public long Date { get; private set; }

		[JsonProperty("from_id")] public long FromId { get; private set; }

		[JsonProperty("id")] public long Id { get; private set; }

		[JsonProperty("out")] public long Out { get; private set; }

		[JsonProperty("peer_id")] public long PeerId { get; private set; }

		[JsonProperty("text")] public string Text { get; private set; }

		[JsonProperty("conversation_message_id")]
		public int ConversationMessageId { get; private set; }

		[JsonProperty("fwd_messages")] public IList<JObject> FwdMessages { get; private set; }

		[JsonProperty("important")] public bool Important { get; private set; }

		[JsonProperty("random_id")] public long RandomId { get; private set; }

		[JsonProperty("attachments")] public IList<JObject> Attachments { get; private set; }

		[JsonProperty("is_hidden")] public bool IsHidden { get; private set; }
	}
}