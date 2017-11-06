using System.Threading.Tasks;

namespace WebDriverProvider.Implementation.Chrome
{
	internal class ChromeCompatibleDriverFinder : IRemoteDriverFinder
	{
		private readonly IBrowserVersionDetector _browserVersionDetector;
		private readonly IReleaseNotesParser _releaseNotesParser;
		private readonly IChromeDriverSite _chromeDriverSite;
		private readonly IDriverDownloader _driverDownloader;

		public ChromeCompatibleDriverFinder(
			IBrowserVersionDetector browserVersionDetector, 
			IReleaseNotesParser releaseNotesParser, 
			IChromeDriverSite chromeDriverSite,
			IDriverDownloader driverDownloader
		)
		{
			_browserVersionDetector = browserVersionDetector;
			_releaseNotesParser = releaseNotesParser;
			_chromeDriverSite = chromeDriverSite;
			_driverDownloader = driverDownloader;
		}

		public async Task<RemoteChromeDriverInfo> FindDriverInfo()
		{
			var latestReleaseNotes = await _chromeDriverSite.GetLatestReleaseNotes();
			var browserVersion = _browserVersionDetector.Detect();
			var compatibleVersion = _releaseNotesParser.FindCompatibleDriverVersion(latestReleaseNotes, browserVersion);
			var driverUrl = _chromeDriverSite.GetDriverZipUrl(compatibleVersion);
			var result = new RemoteChromeDriverInfo(driverUrl, _driverDownloader);
			return result;
		}
	}
}