using System;
using System.Text;
using Botyara.Core.Configs;
using Botyara.SfuApi;
using VkNet.Exception;

namespace Botyara.Core
{
	public class Compiler
	{
		public ChatConfig Config { get; private set; }
		
		public Compiler(ChatConfig config)
		{
			Config = config;
		}

		/// <summary>
		/// Составляет расписание
		/// </summary>
		/// <param name="day">Номер дня недели, пн - 1, вс - 7</param>
		/// <param name="oddevenweek">1 - нечётная, 2 - чётная</param>
		public string Compile(int day, int oddevenweek)
		{
			var tb = new TimetableBuilder(Config.Targets[0]);
			var tm = tb.Get();
			
			var sb = new StringBuilder();
			sb.AppendLine(Config.FirstString);
			
			throw new NotImplementedException();
		}
	}
}