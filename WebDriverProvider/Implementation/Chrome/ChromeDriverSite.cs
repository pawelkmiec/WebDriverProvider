using System;
using System.Threading.Tasks;
using WebDriverProvider.Implementation.Utilities;

namespace WebDriverProvider.Implementation.Chrome
{
    internal class ChromeDriverSite : IChromeDriverSite
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        public ChromeDriverSite(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<VersionSpecificUrl> GetLatestDriverZipUrl()
        {
            var latestRelease = await GetLatestRelease();
            var latestDriverUrl = GetDriverZipUrl(latestRelease);
            return latestDriverUrl;
        }

        public VersionSpecificUrl GetDriverZipUrl(string version)
        {
            var driverUrl = new VersionSpecificUrl
            {
                Url = new Uri($"http://chromedriver.storage.googleapis.com/{version}/chromedriver_win32.zip"),
                Version = DriverVersion.ParseUrlVersionString(version)
            };
            return driverUrl;
        }

        public async Task<string> GetLatestReleaseNotes()
        {
            var latestRelease = await GetLatestRelease();
            var latestReleaseNotesUrl = new Uri($"http://chromedriver.storage.googleapis.com/{latestRelease}/notes.txt");
            var latestReleaseNotes = await _httpClientWrapper.GetStringAsync(latestReleaseNotesUrl);
            return latestReleaseNotes;
        }

        private async Task<string> GetLatestRelease()
        {
            var latestReleaseUrl = new Uri("http://chromedriver.storage.googleapis.com/LATEST_RELEASE");
            var latestRelease = await _httpClientWrapper.GetStringAsync(latestReleaseUrl);
            return latestRelease.Trim();
        }
    }
}
