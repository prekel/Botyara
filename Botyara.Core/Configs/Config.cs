using System.Collections.Generic;
using Newtonsoft.Json;

namespace Botyara.Core.Configs
{
	/// <summary>
	/// Представляет конфигурацию приложения.
	/// </summary>
	[JsonObject]
	public class Config
	{
		/// <summary>
		/// Получает или задаёт Access Token.
		/// </summary>
		[JsonProperty]
		public string AccessToken { get; set; }

		/// <summary>
		/// Получает или задаёт идентификатор группы.
		/// </summary>
		[JsonProperty]
		public ulong GroupId { get; set; }

		/// <summary>
		/// Получает или задаёт конфигурации чатов.
		/// </summary>
		[JsonProperty]
		public IList<ChatConfig> ChatConfigs { get; set; }
	}
}
