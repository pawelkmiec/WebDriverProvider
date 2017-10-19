using System.Threading.Tasks;

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

		public async Task<WebDriverInfo> FindDriverInfo()
		{
			var latestVersion = await _latestDriverVersionFinder.Find();
			var driverUrl = _chromeDriverSite.GetDriverZipUrl(latestVersion);
			var result = new WebDriverInfo(driverUrl, latestVersion);
			return result;
		}
	}
}