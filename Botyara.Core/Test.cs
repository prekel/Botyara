using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;
using Newtonsoft.Json;
using Botyara.SfuApi;

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

				Console.WriteLine(resp.RawJson.ToString());

				try
				{
					var respmsg = resp["updates"][0]["object"]["text"];
					var msg = respmsg.ToString();

					var spl = msg.ToString().Split();
					var a = 0;
					var b = 0;
					if (spl.Length == 2)
					{
						a = Int32.Parse(spl[0]);
						b = Int32.Parse(spl[1]);
						var c = new TimetableBuilder(" »18-17/1·");
						var t = c.Get();
						msg = String.Join(", ", from i in t.Timetable where i.Day == a && i.Week == b select i.Subject);

						var typ = resp["updates"][0]["type"];
						if (msg != "" && typ == "message_new")
						{
							Api.Messages.Send(new MessagesSendParams
							{
								PeerId = Int64.Parse(resp["updates"][0]["object"]["peer_id"]),
								Message = msg
							});
						}
					}

				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}

				d["ts"] = resp["ts"];
			}
		}
	}
}