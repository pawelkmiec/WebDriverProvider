using System;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace WebDriverProvider.Tests.Integration
{
    public class ChromeDriverTests
    {
	    private readonly string _driverDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "driver-directory");

	    [SetUp]
	    public void Setup()
	    {
            CreateDirectoryIfNotExists(_driverDirectory);
		}

	    private static void CreateDirectoryIfNotExists(string directoryPath)
	    {
		    if (!Directory.Exists(directoryPath))
		    {
			    Directory.CreateDirectory(directoryPath);
		    }
	    }

	    [Test]
	    public async Task should_get_latest_chrome_driver_and_run_selenium_go_to_url()
	    {
			//given
		    var driverProvider = ProviderConfiguration.For(Browser.Chrome)
				.WithDesiredDriver(DesiredDriver.Latest)
				.BuildDriverProvider();
		    
			//when
			await driverProvider.DownloadDriverBinary(_driverDirectory);
			
			//then
			Assert.DoesNotThrow(() =>
			{
				using (var driver = new ChromeDriver(_driverDirectory))
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
			    .BuildDriverProvider();

			//when
			await driverProvider.DownloadDriverBinary(_driverDirectory);

			//then
			Assert.DoesNotThrow(() =>
		    {
			    using (var driver = new ChromeDriver(_driverDirectory))
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
