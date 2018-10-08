using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;
using Newtonsoft.Json;
using VkNet.Model;
using VkNet.Utils;

namespace Botyara.Core
{
	public class LongPollResponseEventArgs : EventArgs
	{
		public VkResponse Response { get; }

		public LongPollResponseEventArgs(VkResponse resp)
		{
			Response = resp;
		}
	}

	public class LongPoller
	{
		public VkApi Api { get; private set; }
		public long GroupId { get; private set; }

		private LongPollServerResponse LongPoolServer { get; set; }
		private IDictionary<string, string> Params { get; set; }

		public event EventHandler ResponseReceived;


		public LongPoller(VkApi api, long gpoupid)
		{
			Api = api;
			GroupId = gpoupid;
		}

		public void Start()
		{
			LongPoolServer = Api.Groups.GetLongPollServer(172122256);

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

		public async void Run()
		{
			while (true)
			{
				var resp = await Api.CallLongPollAsync(LongPoolServer.Server, new VkNet.Utils.VkParameters(Params));
				OnResponseReceived(new LongPollResponseEventArgs(resp));
				Params["ts"] = resp["ts"];
			}
		}
	}
}