using System;

namespace WebDriverProvider.Implementation
{
	internal interface IWebDriverInfo
	{
		string DriverFileName { get; }

		Uri DownloadUrl { get; }

		string Version { get; }

		string GetDriverVersion(IShellCommandExecutor commandExecutor);
	}
}