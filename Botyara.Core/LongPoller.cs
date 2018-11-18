using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using NLog;
using VkNet;
using VkNet.Model;
using VkNet.Utils;

namespace Botyara.Core
{
	/// <summary>
	/// Представляет аргументы события, вызываемым LongPoller.
	/// </summary>
	public class LongPollResponseEventArgs : EventArgs
	{
		/// <summary>
		/// Получает недесериализованный ответ, полученным LongPoll-запросом.
		/// </summary>
		public VkResponse RawResponse { get; }

		/// <summary>
		/// Получает десериализованный ответ, полученным LongPoll-запросом.
		/// </summary>
		public Response.Response Response { get; }

		/// <summary>
		/// Инициализирует новый экземпляр класса <c>LongPollResponseEventArgs</c>.
		/// </summary>
		/// <param name="rawresp">Недесериализованный ответ, полученным LongPoll-запросом.</param>
		public LongPollResponseEventArgs(VkResponse rawresp)
		{
			RawResponse = rawresp;
			Response = JsonConvert.DeserializeObject<Botyara.Core.Response.Response>(rawresp.RawJson);
		}
	}

	/// <summary>
	/// Представляет обработчика LongPool-запросов.
	/// </summary>
	public class LongPoller
	{
		private static Logger Log { get; } = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Получает Vk Api.
		/// </summary>
		public VkApi Api { get; private set; }

		/// <summary>
		/// Получает идентификатор группы.
		/// </summary>
		public ulong GroupId { get; private set; }

		private LongPollServerResponse LongPoolServer { get; set; }
		private IDictionary<string, string> Params { get; set; }

		/// <summary>
		/// Событие, происходящее когда получено сообщение.
		/// </summary>
		public event EventHandler<LongPollResponseEventArgs> ResponseReceived;

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="LongPoller"/>.
		/// </summary>
		/// <param name="api">Vk Api.</param>
		/// <param name="gpoupid">Идентификатор группы.</param>
		public LongPoller(VkApi api, ulong gpoupid)
		{
			Api = api;
			GroupId = gpoupid;
		}

		/// <summary>
		/// Запрашивает данные для запуска обработчик запросов.
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

		protected virtual void OnResponseReceived(LongPollResponseEventArgs e)
		{
			var handler = ResponseReceived;
			handler?.Invoke(this, e);
		}
		
		/// <summary>
		/// Запускает обработчик запросов.
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