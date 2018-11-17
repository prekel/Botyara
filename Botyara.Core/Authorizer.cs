using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace Botyara.Core
{
	/// <summary>
	/// Представляет авторизатора, предназначенного для получения Vk Api.
	/// </summary>
	public class Authorizer
	{
		/// <summary>
		/// Получает Vk Api.
		/// </summary>
		public VkApi Api { get; private set; }

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="Authorizer"/> и авторизует с заданным Access Token.
		/// </summary>
		/// <param name="accesstoken">Access Token.</param>
		public Authorizer(string accesstoken)
		{
			Api = new VkApi();

			Api.Authorize(new ApiAuthParams
			{
				AccessToken = accesstoken
			});
		}
	}
}