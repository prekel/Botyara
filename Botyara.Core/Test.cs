using System.Collections.Generic;

using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;

namespace Botyara.Core
{
	public class Test
	{
		public VkApi Api { get; private set; }
		
		public Test(VkApi api)
		{
			Api = api;
		}

		public void Run()
		{
			//var chats = Api.Messages.GetConversations(new GetConversationsParams()
			//{
			//	Count = 100,
			//	Filter = GetConversationFilter.All
				
			//});

			//			var msgs = Api.Messages.GetHistory(new MessagesGetHistoryParams()
			//			{
			//				Count = 100,
			//				PeerId = 132740853,
			//			});

			//var msgs = Api.Messages.GetHistory(new MessagesGetHistoryParams()
			//{
			//	Count = 100,
			//	PeerId = 2000000001
			//});

			var srv = Api.Groups.GetLongPollServer(172122256);

			var d = new Dictionary<string, string>
			{
				["act"] = "a_check",
				["key"] = srv.Key,
				["ts"] = srv.Ts,
				["wait"] = "25"
			};
			Api.CallLongPoll(srv.Server, new VkNet.Utils.VkParameters(d));
		}
	}
}