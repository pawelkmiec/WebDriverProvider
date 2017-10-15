using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
    public class WebDriverProvider
    {
	    public async Task<DirectoryInfo> GetDriverBinary(Browser browser, DriverType driverType)
	    {
		    var driverProvider = CreateDriverProvider(driverType);
		    var binaryLocation = await driverProvider.GetDriverBinary();
			var driverDirectory = Path.GetDirectoryName(binaryLocation.FullName);
			return new DirectoryInfo(driverDirectory);
		}

	    private IDriverProvider CreateDriverProvider(DriverType driverType)
	    {
		    switch (driverType)
		    {
			    case DriverType.Latest:
				    return CreateChromeProviderForLatestDriver();
			    case DriverType.LatestCompatible:
				    return CreateChromeProviderForLatestCompatibleDriver();
		    }
			throw new InvalidEnumArgumentException(nameof(driverType));
	    }

	    private IDriverProvider CreateChromeProviderForLatestDriver()
	    {
		    var fileSystemWrapper = new FileSystemWrapper();
		    var httpClientWrapper = new HttpClientWrapper();
		    var chromeLatestReleaseFinder = new ChromeLatestReleaseFinder(httpClientWrapper);
		    var chromeDriverSite = new ChromeDriverSite();
		    var latestDriverVersionFinder = new LatestDriverVersionFinder(chromeLatestReleaseFinder, chromeDriverSite);
		    var driverDownloader = new DriverDownloader(httpClientWrapper, fileSystemWrapper);
		    var provider = new ChromeDriverProvider(
			    latestDriverVersionFinder,
			    driverDownloader,
			    fileSystemWrapper
		    );
		    return provider;
	    }

	    private IDriverProvider CreateChromeProviderForLatestCompatibleDriver()
	    {
		    var fileSystemWrapper = new FileSystemWrapper();
		    var httpClientWrapper = new HttpClientWrapper();
		    var chromeLatestReleaseFinder = new ChromeLatestReleaseFinder(httpClientWrapper);
		    var chromeDriverSite = new ChromeDriverSite();
		    var versionDetector = new ChromeVersionDetector();
		    var releaseNotesParser = new ChromeDriverReleaseNotesParser();
		    var latestDriverVersionFinder = new CompatibleDriverVersionFinder(
				chromeLatestReleaseFinder,
				versionDetector,
				httpClientWrapper,
				releaseNotesParser,
				chromeDriverSite
			);
		    var driverDownloader = new DriverDownloader(httpClientWrapper, fileSystemWrapper);
		    var provider = new ChromeDriverProvider(
			    latestDriverVersionFinder,
			    driverDownloader,
			    fileSystemWrapper
		    );
		    return provider;
		}
	}
}
