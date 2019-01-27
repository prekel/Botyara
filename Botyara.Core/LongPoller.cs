using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;
using Newtonsoft.Json;
using NLog;
using VkNet.Model;
using VkNet.Utils;

namespace Botyara.Core
{
	public class LongPollResponseEventArgs : EventArgs
	{
		public VkResponse RawResponse { get; }
		public Response.Response Response { get; }

		public LongPollResponseEventArgs(VkResponse rawresp)
		{
			RawResponse = rawresp;
			Response = JsonConvert.DeserializeObject<Botyara.Core.Response.Response>(rawresp.RawJson);
		}
	}

	public class LongPoller
	{
		private static Logger Log { get; } = LogManager.GetCurrentClassLogger();

		public VkApi Api { get; private set; }
		public ulong GroupId { get; private set; }

		private LongPollServerResponse LongPoolServer { get; set; }
		private IDictionary<string, string> Params { get; set; }

		public event EventHandler ResponseReceived;
		
		public LongPoller(VkApi api, ulong gpoupid)
		{
			Api = api;
			GroupId = gpoupid;
		}

		public void Start()
		{
			Log.Debug("Получение LongPoolServer");
			LongPoolServer = Api.Groups.GetLongPollServer(GroupId);
			Log.Debug("Получен LongPoolServer");
			//Log.Trace($"{LongPoolServer.Server} {LongPoolServer.Key} {LongPoolServer.Ts}");

			Params = new Dictionary<string, string>
			{
				["act"] = "a_check",
				["key"] = LongPoolServer.Key,
				["ts"] = LongPoolServer.Ts,
				["wait"] = "25"
			};
		}

		protected virtual void OnResponseReceived(EventArgs e)
		{
			var handler = ResponseReceived;
			handler?.Invoke(this, e);
		}

		public async Task RunAsync()
		{
			Log.Debug("Запущен LongPoolServer");
			while (true)
			{
				var task = Api.CallLongPollAsync(LongPoolServer.Server, new VkNet.Utils.VkParameters(Params));
				var timeout = Int32.Parse(Params["ts"]) + 5;
				if (await Task.WhenAny(task, Task.Delay(TimeSpan.FromSeconds(timeout))) == task)
				{
					var resp = task.Result;
					OnResponseReceived(new LongPollResponseEventArgs(resp));
					Params["ts"] = resp["ts"];
				}
				else
				{
					Log.Warn("Таймаут LongPool-запроса, перезапуск");
					Start();
				}
			}
		}
	}
}