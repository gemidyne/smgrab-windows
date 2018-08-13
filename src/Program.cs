using System;
using System.Linq;
using System.Net;

namespace SmGrab
{
	internal class Program
	{
		private const string _latestVersionInfoUrl = "https://sm.alliedmods.net/smdrop/{0}/sourcemod-latest-windows";
		private const string _dropPathUrlFormat = "https://sm.alliedmods.net/smdrop/{0}/{1}";

		private static void Main(string[] args)
		{
			string version = args.FirstOrDefault()?.Trim();

			if (string.IsNullOrWhiteSpace(version))
			{
				Console.Error.Write("No version specified in arguments.");
				Environment.Exit(-1);
			}

			string infoUrl = string.Format(_latestVersionInfoUrl, version);

			using (var wc = new WebClient())
			{
				Console.Out.WriteLine($"Querying {infoUrl}...");

				string fileName = null;

				try
				{
					fileName = wc.DownloadString(infoUrl);
				}
				catch (WebException ex)
				{
					Console.Error.Write($"Failed to query {infoUrl}: {ex.Message}");
					Environment.Exit(-1);
				}

				if (string.IsNullOrWhiteSpace(fileName))
				{
					Console.Error.Write($"{infoUrl} was 0 bytes!");
					Environment.Exit(-1);
				}

				string fullPath = string.Format(_dropPathUrlFormat, version, fileName);

				Console.Out.WriteLine($"Downloading {fileName}...");

				using (new DownloadTimer())
					wc.DownloadFile(fullPath, "smgrab.zip");

				Console.Out.WriteLine($"Downloaded {fileName} as smgrab.zip.");
			}
		}
	}
}
