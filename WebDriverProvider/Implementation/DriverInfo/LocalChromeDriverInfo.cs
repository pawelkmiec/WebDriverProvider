using System;
using System.IO;

namespace WebDriverProvider.Implementation.DriverInfo
{
	internal class LocalChromeDriverInfo : IWebDriverInfo
	{
		private readonly DirectoryInfo _directoryWithDriver;

		public LocalChromeDriverInfo(DirectoryInfo directoryWithDriver)
		{
			_directoryWithDriver = directoryWithDriver;
		}

		public string DriverFileName { get; }

		public Uri DownloadUrl { get; }

		public string Version { get; }

		public DriverVersion GetDriverVersion(IShellCommandExecutor commandExecutor)
		{
			commandExecutor.WorkingDirectory = _directoryWithDriver.FullName;
			var driverVersionString = commandExecutor.Execute("chromedriver.exe --version");
			var driverVersion = ParseVersion(driverVersionString);
			return driverVersion;
			// "chromedriver.exe --version"
			// throw new NotImplementedException();
		}

		private DriverVersion ParseVersion(string driverVersionString)
		{
			var parts = driverVersionString.Split('.');

			if (parts.Length == 1)
			{
				var majorNumber = Int32.Parse(parts[0]);
				return new DriverVersion(majorNumber, 0);
			}
			else
			{
				var majorNumber = Int32.Parse(parts[0]);
				var minorNumber = Int32.Parse(parts[1]);
				return new DriverVersion(majorNumber, minorNumber);
			}
		}
	}
}