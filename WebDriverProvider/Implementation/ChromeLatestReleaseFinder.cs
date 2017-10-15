using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	public class ChromeLatestReleaseFinder : IChromeLatestReleaseFinder
	{
		private readonly IHttpClientWrapper _httpClientWrapper;

		public ChromeLatestReleaseFinder(IHttpClientWrapper httpClientWrapper)
		{
			_httpClientWrapper = httpClientWrapper;
		}

		public async Task<string> Find()
		{
			var chromeDriverSite = new ChromeDriverSite();
			var latestReleaseUrl = chromeDriverSite.GetLatestReleaseUrl();
			var latestRelease = await _httpClientWrapper.GetStringAsync(latestReleaseUrl);
			return latestRelease.Trim();
		}
	}
}