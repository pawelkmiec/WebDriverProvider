using System.Threading.Tasks;

namespace WebDriverProvider.Implementation.Chrome
{
	internal class ChromeLatestDriverFinder : IRemoteDriverFinder
	{
		private readonly IChromeDriverSite _chromeDriverSite;
		private readonly IDriverDownloader _downloader;

		public ChromeLatestDriverFinder(
			IChromeDriverSite chromeDriverSite, 
			IDriverDownloader downloader
		)
		{
			_chromeDriverSite = chromeDriverSite;
			_downloader = downloader;
		}

		public async Task<RemoteChromeDriverInfo> FindDriverInfo()
		{
			var driverUrl = await _chromeDriverSite.GetLatestDriverZipUrl();
			var result = new RemoteChromeDriverInfo(driverUrl, _downloader);
			return result;
		}
	}
}