using System;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	public class ChromeLatestDriverVersionFinder : IDriverVersionFinder
	{
		private readonly IChromeLatestReleaseFinder _latestReleaseFinder;
		private readonly IChromeDriverSite _chromeDriverSite;

		public ChromeLatestDriverVersionFinder(IChromeLatestReleaseFinder latestReleaseFinder, IChromeDriverSite chromeDriverSite)
		{
			_latestReleaseFinder = latestReleaseFinder;
			_chromeDriverSite = chromeDriverSite;
		}

		public async Task<Uri> FindDriverUrl()
		{
			var latestRelease = await _latestReleaseFinder.Find();
			var latestDriverUrl = _chromeDriverSite.GetDriverZipUrl(latestRelease);
			return latestDriverUrl;
		}
	}
}