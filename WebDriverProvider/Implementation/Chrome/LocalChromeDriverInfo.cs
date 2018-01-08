using System.IO;
using WebDriverProvider.Implementation.Utilities;

namespace WebDriverProvider.Implementation.Chrome
{
	internal class LocalChromeDriverInfo : IWebDriverInfo
	{
		private readonly DirectoryInfo _directoryWithDriver;
		private readonly IShellCommandExecutor _shellCommandExecutor;

		public LocalChromeDriverInfo(DirectoryInfo directoryWithDriver, IShellCommandExecutor shellCommandExecutor)
		{
			_directoryWithDriver = directoryWithDriver;
			_shellCommandExecutor = shellCommandExecutor;
		}

		public DriverVersion GetDriverVersion()
		{
			var driverVersionString = _shellCommandExecutor.Execute(_directoryWithDriver.FullName, "chromedriver.exe", "--version");
			var driverVersion = DriverVersion.ParseCommandLineVersionString(driverVersionString);
			return driverVersion;
		}
	}
}