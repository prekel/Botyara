using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

using NLog;

using VkNet;
using VkNet.Model;
using VkNet.Utils;

namespace Botyara.Core
{
    public class LongPollResponseEventArgs : EventArgs
    {
        public LongPollResponseEventArgs(VkResponse rawresp)
        {
            RawResponse = rawresp;
            Response = JsonConvert.DeserializeObject<Response.Response>(rawresp.RawJson);
        }

        public VkResponse RawResponse { get; }
        public Response.Response Response { get; }
    }

    public class LongPoller
    {
        public LongPoller(VkApi api, ulong gpoupid)
        {
            Api = api;
            GroupId = gpoupid;
        }

        private static Logger Log { get; } = LogManager.GetCurrentClassLogger();

        public VkApi Api { get; }
        public ulong GroupId { get; }

        private LongPollServerResponse LongPoolServer { get; set; }
        private IDictionary<string, string> Params { get; set; }

        public TimeSpan Wait { get; set; } = TimeSpan.FromSeconds(25);
        public TimeSpan TimeOut { get; set; } = TimeSpan.FromSeconds(5);

        public event EventHandler ResponseReceived;

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
                ["wait"] = ((int) Wait.TotalSeconds).ToString()
            };
        }

        public async Task StartAsync()
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
                ["wait"] = ((int) Wait.TotalSeconds).ToString()
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
                var task = Api.CallLongPollAsync(LongPoolServer.Server, new VkParameters(Params));

                var timeout = Task.Delay(Wait + TimeOut);
                if (await Task.WhenAny(task, timeout) == task)
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
