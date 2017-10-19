using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	internal class LatestDriverVersionFinder : ILatestDriverVersionFinder
	{
		private readonly IHttpClientWrapper _httpClientWrapper;

		public LatestDriverVersionFinder(IHttpClientWrapper httpClientWrapper)
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