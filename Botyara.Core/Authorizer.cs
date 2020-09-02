using VkNet;
using VkNet.Model;

namespace Botyara.Core
{
    public class Authorizer
    {
        public Authorizer(string accesstoken)
        {
            Api = new VkApi();

            Api.Authorize(new ApiAuthParams
            {
                AccessToken = accesstoken
            });
        }

        public VkApi Api { get; }
    }
}
