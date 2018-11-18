using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Botyara.Core.Response
{
	/// <summary>
	/// Представляет объект, полученный LongPoll-запросом.
	/// </summary>
	[JsonObject]
	public class Obj
	{
		/// <summary>
		/// Получает время отправки в Unixtime.
		/// </summary>
		[JsonProperty("date")]
		public long Date { get; private set; }

		/// <summary>
		/// Получает идентификатор автора сообщения.
		/// </summary>
		[JsonProperty("from_id")]
		public long FromId { get; private set; }

		/// <summary>
		/// Получает идентификатор сообщения.
		/// </summary>
		[JsonProperty("id")]
		public long Id { get; private set; }

		/// <summary>
		/// Получает тип сообщения.
		/// </summary>
		/// <remarks>
		/// (0 — полученное, 1 — отправленное, не возвращается для пересланных сообщений).
		/// </remarks>
		[JsonProperty("out")]
		public long Out { get; private set; }

		/// <summary>
		/// Получает идентификатор назначения (чат, из которого пришло сообщение (Id беседы или собеседника).
		/// </summary>
		[JsonProperty("peer_id")]
		public long PeerId { get; private set; }

		/// <summary>
		/// Получает текст сообщения.
		/// </summary>
		[JsonProperty("text")]
		public string Text { get; private set; }

		/// <summary>
		/// Получает <c>conversation_message_id</c>.
		/// </summary>
		[JsonProperty("conversation_message_id")]
		public int ConversationMessageId { get; private set; }

		/// <summary>
		/// Получает массив пересланных сообщений.
		/// </summary>
		[JsonProperty("fwd_messages")]
		public IList<JObject> FwdMessages { get; private set; }

		/// <summary>
		/// Получает значение того, является ли сообщение важным.
		/// </summary>
		[JsonProperty("important")]
		public bool Important { get; private set; }

		/// <summary>
		/// Получает идентификатор, используемый при отправке сообщения.
		/// </summary>
		/// <remarks>
		/// Возвращается только для исходящих сообщений.
		/// </remarks>
		[JsonProperty("random_id")]
		public long RandomId { get; private set; }

		/// <summary>
		/// Получает вложения.
		/// </summary>
		[JsonProperty("attachments")]
		public IList<JObject> Attachments { get; private set; }

		/// <summary>
		/// Получает значение того, является ли сообщение скрытым.
		/// </summary>
		[JsonProperty("is_hidden")]
		public bool IsHidden { get; private set; }
	}
}
