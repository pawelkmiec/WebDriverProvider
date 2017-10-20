using System.Threading.Tasks;
using WebDriverProvider.Implementation.DriverInfo;

namespace WebDriverProvider.Implementation
{
	internal class ChromeLatestDriverFinder : IDriverFinder
	{
		private readonly ILatestDriverVersionFinder _latestDriverVersionFinder;
		private readonly IChromeDriverSite _chromeDriverSite;

		public ChromeLatestDriverFinder(ILatestDriverVersionFinder latestDriverVersionFinder, IChromeDriverSite chromeDriverSite)
		{
			_latestDriverVersionFinder = latestDriverVersionFinder;
			_chromeDriverSite = chromeDriverSite;
		}

		public async Task<IWebDriverInfo> FindDriverInfo()
		{
			var latestVersion = await _latestDriverVersionFinder.Find();
			var driverUrl = _chromeDriverSite.GetDriverZipUrl(latestVersion);
			var result = new RemoteChromeDriverInfo(driverUrl, latestVersion);
			return result;
		}
	}
}