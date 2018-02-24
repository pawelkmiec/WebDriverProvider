//using System;
//using System.Threading.Tasks;
//using Moq;
//using NUnit.Framework;
//using WebDriverProvider.Implementation;

//namespace WebDriverProvider.Tests.Unit
//{
//    public class LatestDriverVersionFinderTests
//    {
//	    [Test]
//	    public async Task should_return_url_to_latest_driver_release()
//	    {
//			//given
//		    var versionFinder = new Mock<ILatestDriverVersionFinder>();
//		    var chromeDriverSite = new Mock<IChromeDriverSite>();
//		    var driverFinder = new LatestChromeDriverFinder(versionFinder.Object, chromeDriverSite.Object);

//		    var latestVersion = "2.33";
//		    versionFinder.Setup(s => s.Find()).ReturnsAsync(latestVersion);

//		    var driverUrl = new Uri("http://google.com/chromedriver.zip");
//		    chromeDriverSite.Setup(s => s.GetDriverZipUrl(It.IsAny<string>()))
//			    .Returns(driverUrl);

//			//when
//			var result = await driverFinder.FindDriverInfo();

//			//then
//			Assert.That(result.DownloadUrl, Is.EqualTo(driverUrl));
//		    Assert.That(result.Version, Is.EqualTo(latestVersion));
//			chromeDriverSite.Verify(v => v.GetDriverZipUrl(latestVersion));
//		    versionFinder.Verify(s => s.Find());
//		}
//    }
//}
