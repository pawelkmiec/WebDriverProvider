using System;

namespace WebDriverProvider.Implementation
{
	internal class WebDriverInfo : IWebDriverInfo
	{
		public WebDriverInfo(Uri downloadUrl, string version)
		{
			DownloadUrl = downloadUrl;
			Version = version;
		}

		public string DriverFileName => "chromedriver.exe";

		public Uri DownloadUrl { get; }

		public string Version { get; }

		public string GetDriverVersion(IShellCommandExecutor commandExecutor)
		{
			// "chromedriver.exe --version"
			throw new NotImplementedException();
		}
	}
}