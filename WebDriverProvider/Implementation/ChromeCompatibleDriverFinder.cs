using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	internal class ChromeCompatibleDriverFinder : IDriverFinder
	{
		private readonly ILatestDriverVersionFinder _latestDriverVersionfinder;
		private readonly IBrowserVersionDetector _versionDetector;
		private readonly IHttpClientWrapper _httpClientWrapper;
		private readonly IChromeDriverReleaseNotesParser _releaseNotesParser;
		private readonly IChromeDriverSite _chromeDriverSite;

		public ChromeCompatibleDriverFinder(
			ILatestDriverVersionFinder latestDriverVersionfinder, 
			IBrowserVersionDetector versionDetector, 
			IHttpClientWrapper httpClientWrapper, 
			IChromeDriverReleaseNotesParser releaseNotesParser, 
			IChromeDriverSite chromeDriverSite
		)
		{
			_latestDriverVersionfinder = latestDriverVersionfinder;
			_versionDetector = versionDetector;
			_httpClientWrapper = httpClientWrapper;
			_releaseNotesParser = releaseNotesParser;
			_chromeDriverSite = chromeDriverSite;
		}

		public async Task<WebDriverInfo> FindDriverInfo()
		{
			var latestVersion = await _latestDriverVersionfinder.Find();
			var latestReleaseNotesUrl = _chromeDriverSite.GetReleaseNotesUrl(latestVersion);
			var releaseNotes = await _httpClientWrapper.GetStringAsync(latestReleaseNotesUrl);
			var browserVersion = _versionDetector.Detect();
			var compatibleVersion = _releaseNotesParser.FindCompatibleDriverVersion(releaseNotes, browserVersion);
			var driverUrl = _chromeDriverSite.GetDriverZipUrl(compatibleVersion);
			var result = new WebDriverInfo(driverUrl, compatibleVersion);
			return result;
		}
	}
}