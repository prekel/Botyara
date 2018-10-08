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
		public LongPoller Poller { get; private set; }

		public Test(VkApi api, LongPoller lp)
		{
			Api = api;
			Poller = lp;
			lp.ResponseReceived += LpOnResponseReceived;
		}

		private void LpOnResponseReceived(object sender, EventArgs e)
		{
			var resp = ((LongPollResponseEventArgs) e).Response;
			
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
					var c = new TimetableBuilder("КИ18-17/1б");
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
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		public void Run()
		{
//			var resp = Api.CallLongPoll(srv.Server, new VkNet.Utils.VkParameters(d));
//
//				Console.WriteLine(resp.RawJson.ToString());
//
//				try
//				{
//					var respmsg = resp["updates"][0]["object"]["text"];
//					var msg = respmsg.ToString();
//
//					var spl = msg.ToString().Split();
//					var a = 0;
//					var b = 0;
//					if (spl.Length == 2)
//					{
//						a = Int32.Parse(spl[0]);
//						b = Int32.Parse(spl[1]);
//						var c = new TimetableBuilder("КИ18-17/1б");
//						var t = c.Get();
//						msg = String.Join(", ", from i in t.Timetable where i.Day == a && i.Week == b select i.Subject);
//
//						var typ = resp["updates"][0]["type"];
//						if (msg != "" && typ == "message_new")
//						{
//							Api.Messages.Send(new MessagesSendParams
//							{
//								PeerId = Int64.Parse(resp["updates"][0]["object"]["peer_id"]),
//								Message = msg
//							});
//						}
//					}
//
//				}
//				catch (Exception e)
//				{
//					Console.WriteLine(e);
//				}
//
//				d["ts"] = resp["ts"];
//			}
		}
	}
}