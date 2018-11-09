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

	/// <summary>
	/// Обработчик Long Pool запросов
	/// </summary>
	public class LongPoller
	{
		private static Logger Log { get; } = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Vk Api
		/// </summary>
		public VkApi Api { get; private set; }
		/// <summary>
		/// Id группы
		/// </summary>
		public ulong GroupId { get; private set; }

		private LongPollServerResponse LongPoolServer { get; set; }
		private IDictionary<string, string> Params { get; set; }

		/// <summary>
		/// Событие, происходящее когда получено сообщение
		/// </summary>
		public event EventHandler ResponseReceived;

		/// <summary>
		/// Создаёт обработчика
		/// </summary>
		/// <param name="api">Vk Api</param>
		/// <param name="gpoupid">Id группы</param>
		public LongPoller(VkApi api, ulong gpoupid)
		{
			Api = api;
			GroupId = gpoupid;
		}

		/// <summary>
		/// Получает данные для запуска обработчик запросов
		/// </summary>
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
		
		/// <summary>
		/// Запускает обработчик запросов
		/// </summary>
		public async void Run()
		{
			Log.Debug("Запущен LongPoolServer");
			while (true)
			{
				var resp = await Api.CallLongPollAsync(LongPoolServer.Server, new VkNet.Utils.VkParameters(Params));
				OnResponseReceived(new LongPollResponseEventArgs(resp));
				Params["ts"] = resp["ts"];
			}
		}
	}
}