using System;

namespace WebDriverProvider.Implementation
{
	internal interface IChromeDriverSite
	{
		Uri GetDriverZipUrl(string version);
		Uri GetLatestReleaseUrl();
		Uri GetReleaseNotesUrl(string version);
	}
}