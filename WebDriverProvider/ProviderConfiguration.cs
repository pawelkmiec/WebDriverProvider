using System.ComponentModel;
using WebDriverProvider.Implementation;
using WebDriverProvider.Implementation.RefreshPolicy;

namespace WebDriverProvider
{
	public class ProviderConfiguration
    {
	    public static ProviderConfiguration For(Browser browser)
	    {
		    return new ProviderConfiguration(browser);
	    }

		private Browser _browser;
	    private DesiredDriver _desiredDriver;
	    private RefreshPolicy _refreshPolicy;
	    private IHttpClientWrapper _httpClientWrapper;
	    private IFileSystemWrapper _fileSystemWrapper;
	    private IDriverFinder _driverFinder;
	    private IRefreshPolicy _refreshPolicyObject;
	    private IDriverDownloader _driverDownloader;
	    private IDriverProvider _provider;

	    private ProviderConfiguration(Browser browser)
	    {
		    _browser = browser;
	    }

	    public ProviderConfiguration WithDesiredDriver(DesiredDriver desiredDriver)
	    {
		    _desiredDriver = desiredDriver;
			return this;
	    }

	    public ProviderConfiguration WithRefreshPolicy(RefreshPolicy refreshPolicy)
	    {
		    _refreshPolicy = refreshPolicy;
			return this;
	    }

		public IDriverProvider BuildDriverProvider()
		{
			BuildFileSystemWrapper();
			BuildHttpClientWrapper();
			CreateRefreshPolicy();
			BuildDriverVersionFinder();
			BuildDownloader();
			BuildChromeDriverProvider();
			return _provider;
	    }

	    private void BuildFileSystemWrapper()
	    {
		    _fileSystemWrapper = new FileSystemWrapper();
	    }

	    private void BuildHttpClientWrapper()
	    {
		    _httpClientWrapper = new HttpClientWrapper();
	    }

	    private void CreateRefreshPolicy()
	    {
		    if (_refreshPolicy == RefreshPolicy.Always)
		    {
			    _refreshPolicyObject = new AlwaysRefreshPolicy();
		    }
		    else if (_refreshPolicy == RefreshPolicy.Default)
		    {
			    _refreshPolicyObject = new DefaultRefreshPolicy(_fileSystemWrapper);
		    }
		    else
		    {
			    throw new InvalidEnumArgumentException(nameof(_refreshPolicy));
		    }
	    }

	    private void BuildDriverVersionFinder()
	    {
		    var chromeLatestReleaseFinder = new LatestDriverVersionFinder(_httpClientWrapper);
		    var chromeDriverSite = new ChromeDriverSite();

			if (_desiredDriver == DesiredDriver.Latest)
		    {
			    _driverFinder = new ChromeLatestDriverFinder(chromeLatestReleaseFinder, chromeDriverSite);

			}
			else if (_desiredDriver == DesiredDriver.LatestCompatible)
		    {
			    var versionDetector = new ChromeVersionDetector();
			    var releaseNotesParser = new ChromeDriverReleaseNotesParser();
			    _driverFinder = new ChromeCompatibleDriverFinder(
				    chromeLatestReleaseFinder,
				    versionDetector,
				    _httpClientWrapper,
				    releaseNotesParser,
				    chromeDriverSite
			    );
			}
		    else
		    {
			    throw new InvalidEnumArgumentException(nameof(_desiredDriver));
		    }
	    }

	    private void BuildDownloader()
	    {
		    _driverDownloader = new DriverDownloader(_httpClientWrapper, _fileSystemWrapper);
	    }

	    private void BuildChromeDriverProvider()
	    {
		    _provider = new ChromeDriverProvider(
			    _driverFinder,
			    _driverDownloader,
			    _fileSystemWrapper,
			    _refreshPolicyObject
		    );
	    }
    }
}
