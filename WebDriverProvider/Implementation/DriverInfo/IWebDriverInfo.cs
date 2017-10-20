using System;

namespace WebDriverProvider.Implementation.DriverInfo
{
	internal interface IWebDriverInfo
	{
		string DriverFileName { get; }

		Uri DownloadUrl { get; }

		string Version { get; }

		DriverVersion GetDriverVersion(IShellCommandExecutor commandExecutor);
	}
}