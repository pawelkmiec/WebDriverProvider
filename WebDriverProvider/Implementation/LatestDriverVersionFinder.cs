using System;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	public class LatestDriverVersionFinder : IDriverVersionFinder
	{
		private readonly IChromeLatestReleaseFinder _latestReleaseFinder;
		private readonly IChromeDriverSite _chromeDriverSite;

		public LatestDriverVersionFinder(IChromeLatestReleaseFinder latestReleaseFinder, IChromeDriverSite chromeDriverSite)
		{
			_latestReleaseFinder = latestReleaseFinder;
			_chromeDriverSite = chromeDriverSite;
		}

		public async Task<Uri> GetDriverUrl()
		{
			var latestRelease = await _latestReleaseFinder.Find();
			var latestDriverUrl = _chromeDriverSite.GetDriverZipUrl(latestRelease);
			return latestDriverUrl;
		}
	}
}