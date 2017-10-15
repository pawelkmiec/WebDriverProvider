using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WebDriverProvider.Implementation;

namespace WebDriverProvider.Tests.Unit
{
    public class LatestDriverVersionFinderTests
    {
	    [Test]
	    public async Task should_return_url_to_latest_driver_release()
	    {
			//given
		    var latestReleaseFinder = new Mock<IChromeLatestReleaseFinder>();
		    var chromeDriverSite = new Mock<IChromeDriverSite>();
		    var finder = new ChromeLatestDriverVersionFinder(latestReleaseFinder.Object, chromeDriverSite.Object);

		    var latestRelease = "2.33";
		    latestReleaseFinder.Setup(s => s.Find()).ReturnsAsync(latestRelease);

		    var driverUrl = new Uri("http://google.com/chromedriver.zip");
		    chromeDriverSite.Setup(s => s.GetDriverZipUrl(It.IsAny<string>()))
			    .Returns(driverUrl);

			//when
			var result = await finder.FindDriverUrl();

			//then
			Assert.That(result, Is.EqualTo(driverUrl));
			chromeDriverSite.Verify(v => v.GetDriverZipUrl(latestRelease));
		    latestReleaseFinder.Verify(s => s.Find());
		}
    }
}
