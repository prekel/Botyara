using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Botyara.Core.Configs;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using NLog;
using Botyara.SfuApi;

namespace Botyara.Core
{
	public class Answerer
	{
		public VkApi Api { get; private set; }
		public LongPoller LongPoller { get; private set; }
		public Config Config { get; private set; }

		public Answerer(VkApi api, LongPoller lp, Config config)
		{
			Api = api;
			LongPoller = lp;
			lp.ResponseReceived += LpOnResponseReceived;
			Config = config;
		}

		private void LpOnResponseReceived(object sender, EventArgs e)
		{
			var lpe = (LongPollResponseEventArgs) e;
			var resp = lpe.RawResponse;
			var resp1 = lpe.Response;

			Console.WriteLine(resp.RawJson);

			if (resp1.Failed != null)
			{
				LongPoller.Start();
				return;
			}

			if (resp1.Updates.Count == 0)
			{
				return;
			}

			try
			{
				var msg = resp1.Updates[0].Object;
				var msgtext = msg.Text;

				var spl = msgtext.Split();
				var a = 0;
				var b = 0;
				if (spl.Length != 2) return;
				a = Int32.Parse(spl[0]);
				b = Int32.Parse(spl[1]);

				var chatconf = (from i in Config.ChatConfigs where i.PeerId == msg.PeerId select i).First();
				var compiler = new Compiler(chatconf);
				var ans = compiler.Compile((Day) a, (Week) b);

				var typ = resp1.Updates[0].Type;
				if (msgtext != "" && typ == "message_new")
				{
					Api.Messages.Send(new MessagesSendParams
					{
						PeerId = msg.PeerId,
						Message = ans
					});
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
		
		[Obsolete]
		private void LpOnResponseReceived1(object sender, EventArgs e)
		{
			var lpe = (LongPollResponseEventArgs) e;
			var resp = lpe.RawResponse;
			var resp1 = lpe.Response;

			Console.WriteLine(resp.RawJson);

			if (resp1.Failed != null)
			{
				LongPoller.Start();
				return;
			}

			if (resp1.Updates.Count == 0)
			{
				return;
			}

			try
			{
				var msg = resp1.Updates[0].Object.Text;

				var spl = msg.Split();
				var a = 0;
				var b = 0;
				if (spl.Length != 2) return;
				a = Int32.Parse(spl[0]);
				b = Int32.Parse(spl[1]);

				var c = new TimetableBuilder("КИ18-17/1б");
				var t = c.Get();
				msg = String.Join(", ", from i in t.Timetable where (int)i.Day == a && (int)i.Week == b select i.Subject);

				var typ = resp1.Updates[0].Type;
				if (msg != "" && typ == "message_new")
				{
					Api.Messages.Send(new MessagesSendParams
					{
						PeerId = resp1.Updates[0].Object.PeerId,
						Message = msg
					});
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
}