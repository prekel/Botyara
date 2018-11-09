using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace Botyara.Core
{
	/// <summary>
	/// Авторизатор, предназначен для получения Vk Api
	/// </summary>
	public class Authorizer
	{
		/// <summary>
		/// Vk Api
		/// </summary>
		public VkApi Api { get; private set; }
		
		/// <summary>
		/// Авторизует с заданным Access Token
		/// </summary>
		/// <param name="accesstoken"></param>
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