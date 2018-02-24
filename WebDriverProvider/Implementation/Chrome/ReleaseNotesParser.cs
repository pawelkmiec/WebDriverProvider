using System;
using System.IO;
using System.Text.RegularExpressions;

namespace WebDriverProvider.Implementation.Chrome
{
	internal class ReleaseNotesParser : IReleaseNotesParser
	{
		public string FindCompatibleDriverVersion(string releaseNotes, string requiredChromeVersion)
		{
			var requiredChromeVersionAsInt = Int32.Parse(requiredChromeVersion);
			var previousLine = string.Empty;

			using (var reader = new StringReader(releaseNotes))
			{
				var currentLine = reader.ReadLine();
				while (currentLine != null)
				{
					var driverVersion = Analyse(requiredChromeVersionAsInt, currentLine, previousLine);
					if (driverVersion != null)
					{
						return driverVersion;
					}
					previousLine = currentLine;
					currentLine = reader.ReadLine();
				}
			}

			return null;
		}

		private static string Analyse(int requiredChromeVersion, string currentLine, string previousLine)
		{
			var supportedVersions = TryParseSupportedVersions(currentLine);
			if (supportedVersions == null)
			{
				return null;
			}

			var isVersionCompatible = supportedVersions.MinVersion <= requiredChromeVersion && requiredChromeVersion <= supportedVersions.MaxVersion;
			if (isVersionCompatible)
			{
				var chromeDriverVersion = TryParseChromeDriverVersion(previousLine);
				return chromeDriverVersion;
			}

			return null;
		}

		private static SupportedVersions TryParseSupportedVersions(string line)
		{
			var match = Regex.Match(line, "Supports Chrome v(\\d+)\\s*-\\s*(\\d+)");
			if (match.Success)
			{
				var minVersion = Int32.Parse(match.Groups[1].Value);
				var maxVersion = Int32.Parse(match.Groups[2].Value);
				return new SupportedVersions
				{
					MinVersion = minVersion,
					MaxVersion = maxVersion
				};
			}
			return null;
		}

		private static string TryParseChromeDriverVersion(string line)
		{
			var match = Regex.Match(line, "----------ChromeDriver v(\\d+.\\d+).*");
			if (match.Success)
			{
				var driverVersion = match.Groups[1].Value;
				return driverVersion;
			}
			return null;
		}

		private class SupportedVersions
		{
			public int MinVersion { get; set; }
			public int MaxVersion { get; set; }
		}
	}
}