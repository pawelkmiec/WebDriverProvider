using System.Threading.Tasks;

namespace WebDriverProvider.Implementation.Chrome
{
	internal interface IChromeDriverSite
	{
		Task<VersionSpecificUrl> GetLatestDriverZipUrl();
		VersionSpecificUrl GetDriverZipUrl(string version);
		Task<string> GetLatestReleaseNotes();
	}
}