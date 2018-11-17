using System;
using System.IO;
using System.Collections.Generic;
using Botyara.Console;
using System.Linq;
using System.Threading;
using static System.Console;
using Botyara.SfuApi;
using Botyara.Core;
using Botyara.Core.Configs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VkNet.Enums;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
using NLog;
//using System;
using System.Text;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Botyara.Console
{
	/// <summary>
	/// Представляет основной класс программы.
	/// </summary>
	public class Program
	{
		private static Logger Log { get; } = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Получает или задаёт конфигурацию приложения.
		/// </summary>
		public static Config Config { get; set; }

		/// <summary>
		/// Главная функция программы.
		/// </summary>
		/// <param name="args">Аргументы командной строки.</param>
		public static void Main(string[] args)
		{
			LogManager.Configuration.Variables["starttime"] = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-ffff");
			OutputEncoding = Encoding.UTF8;
			CancelKeyPress += Program_CancelKeyPress;
			Log.Trace("1234");
			Log.Info("1234");
			Log.Debug("1234");
			Log.Warn("1234");
			Log.Error("1234");
			Log.Fatal("1234");
			if (String.Join("; ", Directory.GetFiles(".")).Contains("config.json"))
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
				var target = "КИ18-17/1б";

				bool Dialog()
				{
					WriteLine("Ввести AccessToken, GroupId и Target? [Y/n]");
					var ans1 = ReadLine();
					if (!string.IsNullOrEmpty(ans1) && ans1[0].ToString().ToLower() != "y") return false;
					Write("AccessToken: ");
					token = ReadLine();
					Write("GroupId: ");
					groupid = UInt64.Parse(ReadLine());
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
					PeerId = 0,
					Targets = new List<string>(new[] { target })
				};
				Config = new Config
				{
					AccessToken = token,
					GroupId = groupid,
					ChatConfigs = new List<ChatConfig>(new[] { config1 })
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
					return;
				}
			}

			Log.Debug("Авторизация");
			var auth = new Authorizer(Config.AccessToken);

			Log.Debug("Создание и старт LongPollServer");
			var lp = new LongPoller(auth.Api, Config.GroupId);
			lp.Start();
			lp.Run();

			Log.Debug("Создание и старт Answerer");
			var t1 = new Answerer(auth.Api, lp, Config);

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
			var lpe = (LongPollResponseEventArgs)e;
			WriteLine(lpe.RawResponse.RawJson);
		}
	}
}