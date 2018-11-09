using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Botyara.Core.Response
{
	/// <summary>
	/// Объект от Long Poll
	/// </summary>
	[JsonObject]
	public class Obj
	{
		/// <summary>
		/// Время отправки в Unixtime
		/// </summary>
		[JsonProperty("date")] public long Date { get; private set; }

		/// <summary>
		/// Идентификатор автора сообщения
		/// </summary>
		[JsonProperty("from_id")] public long FromId { get; private set; }

		/// <summary>
		/// Идентификатор сообщения
		/// </summary>
		[JsonProperty("id")] public long Id { get; private set; }

		/// <summary>
		/// тип сообщения (0 — полученное, 1 — отправленное, не возвращается для пересланных сообщений)
		/// </summary>
		[JsonProperty("out")] public long Out { get; private set; }

		/// <summary>
		/// Идентификатор назначения (чат, из которого пришло сообщение (Id беседы или собеседника)
		/// </summary>
		[JsonProperty("peer_id")] public long PeerId { get; private set; }

		/// <summary>
		/// Текст сообщения
		/// </summary>
		[JsonProperty("text")] public string Text { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("conversation_message_id")]
		public int ConversationMessageId { get; private set; }

		/// <summary>
		/// Массив пересланных сообщений 
		/// </summary>
		[JsonProperty("fwd_messages")] public IList<JObject> FwdMessages { get; private set; }

		/// <summary>
		/// Является ли сообщение важным
		/// </summary>
		[JsonProperty("important")] public bool Important { get; private set; }

		/// <summary>
		/// Идентификатор, используемый при отправке сообщения, возвращается только для исходящих сообщений
		/// </summary>
		[JsonProperty("random_id")] public long RandomId { get; private set; }

		/// <summary>
		/// Вложения
		/// </summary>
		[JsonProperty("attachments")] public IList<JObject> Attachments { get; private set; }

		/// <summary>
		/// Является ли сообщение скрытым
		/// </summary>
		[JsonProperty("is_hidden")] public bool IsHidden { get; private set; }
	}
}
