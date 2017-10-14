using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using WebDriverProvider.Implementation;

namespace NWebDriverManager.Tests.Integration
{
    public class ChromeDriverTests
    {
	    [Test]
	    public async Task should_get_chrome_binary_and_run_selenium_go_to_url()
	    {
			//given
		    var provider = new ChromeDriverProvider(
				new LatestDriverVersionFinder(new HttpClientWrapper()),
				new DriverDownloader(new HttpClientWrapper(), new FileSystemWrapper()),
				new FileSystemWrapper()
			);

		    var binaryLocation = await provider.GetDriverBinary();

		    var driverDirectory = Path.GetDirectoryName(binaryLocation.FullName);

			//when
		    var driver = new ChromeDriver(driverDirectory);
			
			//then
			Assert.DoesNotThrow(() =>
			{
				driver.Navigate().GoToUrl("google.com");
			});
		}
    }
}
