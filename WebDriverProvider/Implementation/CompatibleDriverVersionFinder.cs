using System;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	public class CompatibleDriverVersionFinder : IDriverVersionFinder
	{
		private readonly IChromeLatestReleaseFinder _releasefinder;
		private readonly IChromeVersionDetector _versionDetector;
		private readonly IHttpClientWrapper _httpClientWrapper;
		private readonly IChromeDriverReleaseNotesParser _releaseNotesParser;
		private readonly IChromeDriverSite _chromeDriverSite;

		public CompatibleDriverVersionFinder(
			IChromeLatestReleaseFinder releasefinder, 
			IChromeVersionDetector versionDetector, 
			IHttpClientWrapper httpClientWrapper, 
			IChromeDriverReleaseNotesParser releaseNotesParser, 
			IChromeDriverSite chromeDriverSite
		)
		{
			_releasefinder = releasefinder;
			_versionDetector = versionDetector;
			_httpClientWrapper = httpClientWrapper;
			_releaseNotesParser = releaseNotesParser;
			_chromeDriverSite = chromeDriverSite;
		}

		public async Task<Uri> GetDriverUrl()
		{
			var latestRelease = await _releasefinder.Find();
			var latestReleaseNotesUrl = _chromeDriverSite.GetReleaseNotesUrl(latestRelease);
			var releaseNotes = await _httpClientWrapper.GetStringAsync(latestReleaseNotesUrl);
			var browserVersion = _versionDetector.Detect();
			var compatibleVersion = _releaseNotesParser.FindCompatibleDriverVersion(releaseNotes, browserVersion);
			return _chromeDriverSite.GetDriverZipUrl(compatibleVersion);
		}
	}
}