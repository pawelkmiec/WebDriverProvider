using System;

namespace WebDriverProvider.Implementation.DriverInfo
{
	internal class RemoteChromeDriverInfo : IWebDriverInfo
	{
		public RemoteChromeDriverInfo(Uri downloadUrl, string version)
		{
			DownloadUrl = downloadUrl;
			Version = version;
		}

		public string DriverFileName => "chromedriver.exe";

		public Uri DownloadUrl { get; }

		public string Version { get; }

		public DriverVersion GetDriverVersion(IShellCommandExecutor commandExecutor)
		{
			// "chromedriver.exe --version"
			throw new NotImplementedException();
		}
	}
}