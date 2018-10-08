using System.Collections.Generic;
using Newtonsoft.Json;

namespace Botyara.Core.Response
{
	[JsonObject]
	public class Obj
	{
		[JsonProperty("date")]
		public long Date { get; set; }

		[JsonProperty("from_id")]
		public long FronId { get; set; }

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("out")]
		public long Out { get; set; }

		[JsonProperty("peer_id")]
		public long PeerId { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("conversation_message_id")]
		public int ConversationMessageId { get; set; }

		[JsonProperty("fwd_messages")]
		public IList<object> FwdMessages { get; set; }

		[JsonProperty("important")]
		public bool Important { get; set; }
		
		[JsonProperty("random_id")]
		public long RandomId { get; set; }

		[JsonProperty("attachments")]
		public IList<object> Attachments { get; set; }

		[JsonProperty("is_hidden")]
		public bool IsHidden { get; set; }
	}
}