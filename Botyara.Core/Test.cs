using System;
using System.Linq;

using Botyara.SfuApi;

using VkNet;
using VkNet.Model.RequestParams;

namespace Botyara.Core
{
    public class Test
    {
        public Test(VkApi api, LongPoller lp, string target)
        {
            Api = api;
            LongPoller = lp;
            lp.ResponseReceived += LpOnResponseReceived;
            Target = target;
        }

        public VkApi Api { get; }
        public LongPoller LongPoller { get; }

        public string Target { get; }

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
                var msg = resp1.Updates[0].Object.Text;

                var spl = msg.Split();
                var a = 0;
                var b = 0;
                if (spl.Length != 2)
                {
                    return;
                }

                a = Int32.Parse(spl[0]);
                b = Int32.Parse(spl[1]);

                var c = new TimetableBuilder("КИ18-17/1б");
                var t = c.Get();
                msg = String.Join(", ",
                    from i in t.Timetable where (int) i.Day == a && (int) i.Week == b select i.Subject);

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
