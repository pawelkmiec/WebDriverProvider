using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WebDriverProvider;
using WebDriverProvider.Implementation;

namespace NWebDriverManager.Tests.Unit
{
    public class LatestDriverVersionFinderTests
    {
	    private LatestDriverVersionFinder _finder;
	    private Mock<IHttpClientWrapper> _httpClientWrapper;

	    [SetUp]
	    public void Setup()
	    {
		    _httpClientWrapper = new Mock<IHttpClientWrapper>();
		    _finder = new LatestDriverVersionFinder(_httpClientWrapper.Object);

		    _httpClientWrapper.Setup(s => s.GetStringAsync(It.IsAny<Uri>())).ReturnsAsync("1.111");
		}

		[Test]
	    public async Task should_get_latest_release()
	    {
			//when
		    await _finder.GetDriverUrl();

			//then
		    _httpClientWrapper.Verify(s => s.GetStringAsync(new Uri("http://chromedriver.storage.googleapis.com/LATEST_RELEASE")));
	    }

	    [Test]
	    public async Task should_return_url_with_result_of_latest_release_reqiest()
	    {
			//given
		    _httpClientWrapper.Setup(s => s.GetStringAsync(It.IsAny<Uri>())).ReturnsAsync("2.33");

			//when
			var driverUrl = await _finder.GetDriverUrl();

			//then
			Assert.That(driverUrl, Is.EqualTo(new Uri("http://chromedriver.storage.googleapis.com/2.33/chromedriver_win32.zip")));
	    }
    }
}
