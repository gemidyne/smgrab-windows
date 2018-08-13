using System;
using System.Diagnostics;

namespace SmGrab
{
	internal class DownloadTimer : IDisposable
	{
		private readonly Stopwatch _stopwatch;

		public DownloadTimer()
		{
			_stopwatch = Stopwatch.StartNew();
		}

		public void Dispose()
		{
			_stopwatch.Stop();

			Console.WriteLine($"Completed in {_stopwatch.Elapsed}");
		}
	}
}