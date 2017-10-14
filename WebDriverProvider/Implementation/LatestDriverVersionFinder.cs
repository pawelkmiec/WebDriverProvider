using System;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	public class LatestDriverVersionFinder : IDriverVersionFinder
	{
		private readonly IHttpClientWrapper _httpClientWrapper;

		public LatestDriverVersionFinder(IHttpClientWrapper httpClientWrapper)
		{
			_httpClientWrapper = httpClientWrapper;
		}

		public async Task<Uri> GetDriverUrl()
		{
			var chromeDriverHost = "chromedriver.storage.googleapis.com";
			var latestRelease = await _httpClientWrapper.GetStringAsync(new Uri($"http://{chromeDriverHost}/LATEST_RELEASE"));
			var relaseUrl = new Uri($"http://{chromeDriverHost}/{latestRelease}/chromedriver_win32.zip");
			return relaseUrl;
		}
	}
}