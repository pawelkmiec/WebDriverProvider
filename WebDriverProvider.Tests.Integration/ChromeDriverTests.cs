using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace WebDriverProvider.Tests.Integration
{
    public class ChromeDriverTests
    {
	    private Implementation.WebDriverProvider _webDriverProvider;

	    [SetUp]
	    public void Setup()
	    {
			_webDriverProvider = new Implementation.WebDriverProvider();
		}

		[Test]
	    public async Task should_get_latest_chrome_driver_and_run_selenium_go_to_url()
	    {
			//when
		    var driverDirectory = await _webDriverProvider.GetDriverBinary(Browser.Chrome, DriverType.Latest);
			
			//then
			Assert.DoesNotThrow(() =>
			{
				using (var driver = new ChromeDriver(driverDirectory.FullName))
				{
					driver.Navigate().GoToUrl("google.com");
				}
			});
		}

	    [Test]
	    public async Task should_get_compatible_chrome_driver_and_run_selenium_go_to_url()
	    {
		    //when
		    var driverDirectory = await _webDriverProvider.GetDriverBinary(Browser.Chrome, DriverType.LatestCompatible);

		    //then
		    Assert.DoesNotThrow(() =>
		    {
			    using (var driver = new ChromeDriver(driverDirectory.FullName))
			    {
				    driver.Navigate().GoToUrl("google.com");
			    }
		    });
	    }
	}
}
