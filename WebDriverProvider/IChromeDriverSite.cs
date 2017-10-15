using System;

namespace WebDriverProvider
{
	public interface IChromeDriverSite
	{
		Uri GetDriverZipUrl(string version);
		Uri GetLatestReleaseUrl();
		Uri GetReleaseNotesUrl(string version);
	}
}