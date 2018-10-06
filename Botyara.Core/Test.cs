using System;
using System.Collections.Generic;
using System.IO;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;

using Newtonsoft.Json;

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

			while (true)
			{
				var resp = Api.CallLongPoll(srv.Server, new VkNet.Utils.VkParameters(d));

				var msg = resp["updates"][0]["object"]["text"];
				var typ = resp["updates"][0]["type"];
				if (msg != "" && typ == "message_new")
				{
					Api.Messages.Send(new MessagesSendParams
					{
						PeerId = Int64.Parse(resp["updates"][0]["object"]["peer_id"]),
						Message = msg
					});
				}

				d["ts"] = resp["ts"];
			}
		}
	}
}