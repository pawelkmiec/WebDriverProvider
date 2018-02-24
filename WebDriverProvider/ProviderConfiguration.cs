using System.ComponentModel;
using WebDriverProvider.Implementation;
using WebDriverProvider.Implementation.Chrome;
using WebDriverProvider.Implementation.Utilities;

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
	    private IHttpClientWrapper _httpClientWrapper;
	    private IFileSystemWrapper _fileSystemWrapper;
	    private IRemoteDriverFinder _remoteDriverFinder;
	    private ILocalDriverFinder _localDriverFinder;
	    private IDriverDownloader _driverDownloader;
	    private IDriverProvider _provider;
	    private IShellCommandExecutor _shellCommandExecutor;

	    private ProviderConfiguration(Browser browser)
	    {
		    _browser = browser;
	    }

	    public ProviderConfiguration WithDesiredDriver(DesiredDriver desiredDriver)
	    {
		    _desiredDriver = desiredDriver;
			return this;
	    }

		public IDriverProvider BuildDriverProvider()
		{
			BuildFileSystemWrapper();
			BuildHttpClientWrapper();
			BuildShellCommandExecutor();
			BuildDownloader();
			BuildRemoteDriverFinder();
			BuildLocalDriverFinder();
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

	    private void BuildShellCommandExecutor()
	    {
			_shellCommandExecutor = new ShellCommandExecutor();
		}

	    private void BuildDownloader()
	    {
		    _driverDownloader = new ChromeDriverDownloader(_httpClientWrapper, _fileSystemWrapper);
	    }

	    private void BuildRemoteDriverFinder()
	    {
		    var chromeDriverSite = new ChromeDriverSite(_httpClientWrapper);

			if (_desiredDriver == DesiredDriver.Latest)
		    {
			    _remoteDriverFinder = new ChromeLatestDriverFinder(chromeDriverSite, _driverDownloader);
			}
			else if (_desiredDriver == DesiredDriver.LatestCompatible)
		    {
			    var versionDetector = new ChromeVersionDetector();
			    var releaseNotesParser = new ReleaseNotesParser();
			    _remoteDriverFinder = new ChromeCompatibleDriverFinder(
				    versionDetector,
				    releaseNotesParser,
				    chromeDriverSite,
					_driverDownloader
			    );
			}
		    else
		    {
			    throw new InvalidEnumArgumentException(nameof(_desiredDriver));
		    }
	    }

	    private void BuildLocalDriverFinder()
	    {
		    _localDriverFinder= new LocalChromeDriverFinder(_fileSystemWrapper, _shellCommandExecutor);
		}

		private void BuildChromeDriverProvider()
	    {
		    _provider = new ChromeDriverProvider(
			    _remoteDriverFinder,
			    _localDriverFinder
		    );
	    }
    }
}
