using System;

namespace WebDriverProvider.Implementation
{
	internal class ChromeDriverSite : IChromeDriverSite
	{
	    public Uri GetLatestReleaseUrl()
	    {
		    return new Uri("http://chromedriver.storage.googleapis.com/LATEST_RELEASE");
	    }

		public Uri GetReleaseNotesUrl(string version)
	    {
		    return new Uri($"http://chromedriver.storage.googleapis.com/{version}/notes.txt");
	    }

	    public Uri GetDriverZipUrl(string version)
	    {
		    return new Uri($"http://chromedriver.storage.googleapis.com/{version}/chromedriver_win32.zip");
	    }
	}
}
