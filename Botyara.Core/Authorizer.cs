using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace Botyara.Core
{
	public class Authorizer
	{
		public VkApi Api { get; private set; }
		
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