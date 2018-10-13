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
using NLog.Extensions.Logging;
using VkNet.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Botyara.Console
{
	public class Program
	{
		public static Config Config { get; set; }

		public static void Main(string[] args)
		{
			if (String.Join("; ", Directory.GetFiles(".")).Contains("config.json"))
			{
				Config = JsonConvert.DeserializeObject<Config>(new StreamReader("config.json").ReadToEnd());
			}
			else
			{
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

				var ret = Dialog();

				var config1 = new ChatConfig
				{
					FirstString = "Расписание на {OddEvenDayVinPod}:",
					SecondString = "Группа {TargetsList}:",
					LessonString = "{NumberInTimetable}) {Time} {Subject} ({Type}) {Teacher} {Place}",
					NoLessons = "Нет пар",
					PeerId = 0,
					Targets = new List<string>(new[] {target})
				};
				Config = new Config
				{
					AccessToken = token,
					GroupId = groupid,
					ChatConfigs = new List<ChatConfig>(new[] {config1})
				};

				var json = JsonConvert.SerializeObject(Config, Formatting.Indented);
				using (var w = new StreamWriter("config.json"))
				{
					w.WriteLine(json);
				}

				if (!ret) return;
			}

			var auth = new Authorizer(Config.AccessToken);

			var lp = new LongPoller(auth.Api, Config.GroupId);
			lp.Start();
			lp.Run();

			var serviceProvider = BuildDi<Answerer>();
			var t2 = serviceProvider.  GetRequiredService<Answerer>();
			var t1 = new Answerer(auth.Api, lp, Config);

			while (true)
			{
				Thread.Sleep(50000);
			}
		}

		private static void LpOnResponseReceived(object sender, EventArgs e)
		{
			var lpe = (LongPollResponseEventArgs) e;
			WriteLine(lpe.RawResponse.RawJson);
		}
		
		private static IServiceProvider BuildDi<T>()
			where T : class
		{
			var services = new ServiceCollection();

			//Runner is the custom class
			services.AddTransient<T>();

			services.AddSingleton<ILoggerFactory, LoggerFactory>();
			services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
			services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Trace));
     
			var serviceProvider = services.BuildServiceProvider();

			var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

			//configure NLog
			loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties =true });
			NLog.LogManager.LoadConfiguration("nlog.config");

			return serviceProvider;
		}
	}
}