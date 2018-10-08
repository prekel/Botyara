using System;
using System.Threading;

namespace Botyara.Core
{
	public class LongPoller
	{
		public event EventHandler ResponseReceived;

		public class LongPollResponseEventArgs : EventArgs
		{
			public DateTime Time { get; set; }

			public LongPollResponseEventArgs(DateTime dtn)
			{
				Time = dtn;
			}
		}
		
		protected virtual void OnResponseReceived(EventArgs e)
		{
			var handler = ResponseReceived;
			handler?.Invoke(this, e);
		}

		public void Run()
		{
			while (true)
			{
				var dtn = DateTime.Now;
				if (dtn.Second % 5 == 0)
				{
					OnResponseReceived(new LongPollResponseEventArgs(dtn));
				}
				Thread.Sleep(1000);
			}
		}
	}
}