using System.IO;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation.Chrome
{
	internal class ChromeDriverProvider : IDriverProvider
	{
		private readonly IRemoteDriverFinder _remoteLocalDriverFinder;
		private readonly ILocalDriverFinder _localLocalDriverFinder;

		public ChromeDriverProvider(
			IRemoteDriverFinder remoteLocalDriverFinder, 
			ILocalDriverFinder localLocalDriverFinder
		)
		{
			_remoteLocalDriverFinder = remoteLocalDriverFinder;
			_localLocalDriverFinder = localLocalDriverFinder;
		}

		public async Task DownloadDriverBinary(string targetDirectory)
		{
			var remoteWebDriver = await _remoteLocalDriverFinder.FindDriverInfo();
			var localWebDriverOption = _localLocalDriverFinder.FindDriverInfo(targetDirectory);

			await localWebDriverOption.Match(
				async localWebDriver =>
				{
					var isRemoteHigher = IsRemoteVersionHigher(remoteWebDriver, localWebDriver);
					if (isRemoteHigher)
					{
						await DownloadDriver(targetDirectory, remoteWebDriver);
					}
				},
				async () =>
				{
					await DownloadDriver(targetDirectory, remoteWebDriver);
				}
			);
		}

		private static bool IsRemoteVersionHigher(IWebDriverInfo remoteWebDriver, IWebDriverInfo localWebDriver)
		{
			var remoteDriverVersion = remoteWebDriver.GetDriverVersion();
			var localDriverVersion = localWebDriver.GetDriverVersion();
			var remoteIsHigher = DriverVersion.ComparerInstance.Compare(remoteDriverVersion, localDriverVersion) > 0;
			return remoteIsHigher;
		}

		private static async Task DownloadDriver(string targetDirectory, RemoteChromeDriverInfo remoteWebDriver)
		{
			using (var downloadedDriver = await remoteWebDriver.Download())
			using (var localDriverFile = File.Open(Path.Combine(targetDirectory, Constants.DriverFileName), FileMode.Create))
			{
				await downloadedDriver.CopyToAsync(localDriverFile);
			}
		}
	}
}