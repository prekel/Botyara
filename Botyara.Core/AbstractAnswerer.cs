using System;
using System.Linq;

using Botyara.Core.Configs;
using Botyara.SfuApi;

using NLog;

using VkNet;
using VkNet.Model;

namespace Botyara.Core
{
    public abstract class AbstractAnswerer
    {
        public AbstractAnswerer(VkApi api, Config config)
        {
            Api = api;
            Config = config;
        }

        private static Logger Log { get; } = LogManager.GetCurrentClassLogger();

        public VkApi Api { get; }

        public Config Config { get; }

        public string AnswerFromMessage(Message msg)
        {
            //var msg = resp1.Updates[0].Object;
            var msgtext = msg.Text;
            if (msgtext == "")
            {
                throw new ApplicationException("Пустой запрос");
            }

            var spl = msgtext.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var a = 0;
            var b = 0;
            //if (spl.Length != 2) return;
            try
            {
                a = Int32.Parse(spl[0]);
                b = Int32.Parse(spl[1]);
            }
            catch
            {
                throw new ApplicationException("Неправильный запрос");
            }

            var chatConfig = (from i in Config.ChatConfigs where i.PeerId == msg.PeerId select i).First();
            var compiler = new Compiler(chatConfig);
            var ans = compiler.Compile((Day) a, (Week) b);

            Log.Trace($"Ответ сформирован:\r\n{ans.Trim()}");
            return ans;
        }
    }
}
