using System;

using Botyara.Core.Configs;

using NLog;

using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Botyara.Core
{
    public class CallbackAnswerer : AbstractAnswerer
    {
        public CallbackAnswerer(VkApi api, Config config) : base(api, config)
        {
        }

        private static Logger Log { get; } = LogManager.GetCurrentClassLogger();

        public void OnCallbackReceived(Message msg)
        {
            var r = new Random();
            try
            {
                var ans = AnswerFromMessage(msg);
                Log.Trace($"Ответ сформирован:\r\n{ans.Trim()}");
                Api.Messages.Send(new MessagesSendParams
                {
                    PeerId = msg.PeerId,
                    Message = ans,
                    RandomId = r.Next()
                });
                Log.Debug("Отвечено");
            }
            catch (ApplicationException e)
            {
                Log.Warn($"{e.Message}");
            }
        }
    }
}
