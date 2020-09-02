using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Botyara.Core;
using Botyara.Core.Configs;

using Newtonsoft.Json;

using NLog;

using static System.Console;

namespace Botyara.Console
{
    public class Program
    {
        private static Logger Log { get; } = LogManager.GetCurrentClassLogger();

        public static Config Config { get; set; }

        public static void Main(string[] args)
        {
            LogManager.Configuration.Variables["starttime"] = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-ffff");
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.Unicode;
            if (args.Contains("utf8"))
            {
                InputEncoding = Encoding.UTF8;
            }

            CancelKeyPress += Program_CancelKeyPress;
            //Log.Trace("1234");
            //Log.Info("1234");
            //Log.Debug("1234");
            //Log.Warn("1234");
            //Log.Error("1234");
            //Log.Fatal("1234");
            if (File.Exists("config.json"))
            {
                Log.Info("Загружается конфирурация");
                Config = JsonConvert.DeserializeObject<Config>(new StreamReader("config.json").ReadToEnd());
                Log.Info("Загружена конфигурация");
            }
            else
            {
                Log.Info("Создаётся конфигурция");
                var token = "access_token";
                var groupid = 1234ul;
                var peerid = 0;
                var target = "КИ18-17/1б";

                bool Dialog()
                {
                    WriteLine("Ввести AccessToken, GroupId и Target? [Y/n]");
                    var ans1 = ReadLine();
                    if (!String.IsNullOrEmpty(ans1) && ans1[0].ToString().ToLower() != "y")
                    {
                        return false;
                    }

                    Write("AccessToken: ");
                    token = ReadLine();
                    Write("GroupId: ");
                    groupid = UInt64.Parse(ReadLine());
                    Write("PeerId: ");
                    peerid = Int32.Parse(ReadLine());
                    Write("Target: ");
                    target = ReadLine();
                    return true;
                }

                Log.Info("Открывается диалог введения параметров");
                var ret = Dialog();

                var config1 = new ChatConfig
                {
                    FirstString = "Расписание на {OddEvenDayVinPod}:",
                    SecondString = "Группа {TargetsList}:",
                    LessonString = "{NumberInTimetable}) {Time} {Subject} ({Type}) {Teacher} {Place}",
                    NoLessons = "Нет пар",
                    PeerId = peerid,
                    Targets = new List<string>(new[] {target})
                };
                Config = new Config
                {
                    AccessToken = token,
                    GroupId = groupid,
                    ChatConfigs = new List<ChatConfig>(new[] {config1})
                };

                Log.Info("Сохраняется конфигурация");
                var json = JsonConvert.SerializeObject(Config, Formatting.Indented);
                using (var w = new StreamWriter("config.json"))
                {
                    w.WriteLine(json);
                }

                if (!ret)
                {
                    Log.Fatal("Завершение программы потому что не были введены параметры");
                    LogManager.Shutdown();
                    Environment.Exit(0);
                }
            }

            Log.Debug("Авторизация");
            var auth = new Authorizer(Config.AccessToken);

            Log.Debug("Создание и старт LongPollServer");
            var lp = new LongPoller(auth.Api, Config.GroupId);
            lp.Start();
            Task.Run(() => lp.RunAsync());

            Log.Debug("Создание и старт Answerer");
            var t1 = new LongPoolAnswerer(auth.Api, lp, Config);

            while (true)
            {
                Log.Debug("Приложение работает");
                Thread.Sleep(300000);
            }
        }

        private static void Program_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Log.Info("Завершение програмы");
            LogManager.Shutdown();
            Environment.Exit(0);
        }

        private static void LpOnResponseReceived(object sender, EventArgs e)
        {
            var lpe = (LongPollResponseEventArgs) e;
            WriteLine(lpe.RawResponse.RawJson);
        }
    }
}
