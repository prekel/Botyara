using System.Collections.Generic;
using Newtonsoft.Json;

namespace Botyara.Core.Configs
{
	/// <summary>
	/// Конфигурация приложения
	/// </summary>
	[JsonObject]
	public class Config
	{
		/// <summary>
		/// Access Token
		/// </summary>
		[JsonProperty]
		public string AccessToken { get; set; }
		
		/// <summary>
		/// Идентификатор группы
		/// </summary>
		[JsonProperty]
		public ulong GroupId { get; set; }
		
		/// <summary>
		/// Конфигурации чатов
		/// </summary>
		[JsonProperty]
		public IList<ChatConfig> ChatConfigs { get; set; }
	}
}
