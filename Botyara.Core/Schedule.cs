using System;
using System.Collections.Generic;
using System.Text;

namespace Botyara.Core
{
	class Schedule : Dictionary<DateTime, string>
	{
		public long PeerId { get; private set; }

		public IList<string> Targets { get; private set; }

		public Schedule()
		{

		}
	}
}
