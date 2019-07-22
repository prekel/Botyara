using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

using VkNet;
using VkNet.Model;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;
using VkNet.Utils;
using NLog;
using Newtonsoft.Json;

using Botyara.Core.Configs;
using Botyara.SfuApi;

namespace Botyara.Core
{
    public class CallbackAnswerer : AbstractAnswerer
    {
        private static Logger Log { get; } = LogManager.GetCurrentClassLogger();

        public CallbackAnswerer(VkApi api, Config config) : base(api, config)
        {

        }

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
