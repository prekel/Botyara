using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Botyara.SfuApi
{
	public class Request
	{
		public Request()
		{
			
		}

		public void Run()
		{
			var webClient = new WebClient();
			webClient.QueryString.Add("target", "КИ18-17/1Б");
			var result = webClient.DownloadString("http://edu.sfu-kras.ru/api/timetable/get");
		}
	}
}