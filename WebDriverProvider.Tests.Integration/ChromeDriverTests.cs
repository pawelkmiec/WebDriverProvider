using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace WebDriverProvider.Tests.Integration
{
    public class ChromeDriverTests
    {
		[Test]
	    public async Task should_get_latest_chrome_driver_and_run_selenium_go_to_url()
	    {
			//given
		    var driverProvider = ProviderConfiguration.For(Browser.Chrome)
				.WithDesiredDriver(DesiredDriver.Latest)
			    .WithRefreshPolicy(RefreshPolicy.Always)
				.BuildDriverProvider();
		    
			//when
		    var driverDirectory = await driverProvider.GetDriverBinary();
			
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
		    //given
		    var driverProvider = ProviderConfiguration.For(Browser.Chrome)
			    .WithDesiredDriver(DesiredDriver.LatestCompatible)
			    .WithRefreshPolicy(RefreshPolicy.Always)
			    .BuildDriverProvider();

			//when
		    var driverDirectory = await driverProvider.GetDriverBinary();

		    //then
		    Assert.DoesNotThrow(() =>
		    {
			    using (var driver = new ChromeDriver(driverDirectory.FullName))
			    {
				    driver.Navigate().GoToUrl("google.com");
			    }
		    });
	    }

	    //[Test]
	    //public async Task driver_file_should_be_unchanged_when_driver_not_present_policy_is_applied()
	    //{
	    //}
	}
}
