using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WebDriverProvider.Implementation;

namespace WebDriverProvider.Tests.Unit
{
	public class CompatibleDriverVersionFinderTests
	{
		private Mock<IChromeVersionDetector> _versionDetector;
		private Mock<IChromeLatestReleaseFinder> _releaseFinder;
		private Mock<IHttpClientWrapper> _httpClientWrapper;
		private Mock<IChromeDriverReleaseNotesParser> _releaseNotesParser;
		private Mock<IChromeDriverSite> _chromeDriverSite;
		private CompatibleDriverVersionFinder _versionFinder;

		[SetUp]
		public void Setup()
		{
			_versionDetector = new Mock<IChromeVersionDetector>();
			_releaseFinder = new Mock<IChromeLatestReleaseFinder>();
			_httpClientWrapper = new Mock<IHttpClientWrapper>();
			_releaseNotesParser = new Mock<IChromeDriverReleaseNotesParser>();
			_chromeDriverSite = new Mock<IChromeDriverSite>();
			_versionFinder = new CompatibleDriverVersionFinder(
				_releaseFinder.Object, 
				_versionDetector.Object, 
				_httpClientWrapper.Object,
				_releaseNotesParser.Object,
				_chromeDriverSite.Object
			);
		}

		[Test]
		public async Task should_get_release_notes_of_latest_driver_version()
		{
			//given
			var latestRelease = "2.33";
			_releaseFinder.Setup(s => s.Find()).ReturnsAsync(latestRelease);

			var releaseNotesUrl = new Uri("http://google.com/releasenotes.txt");
			_chromeDriverSite.Setup(s => s.GetReleaseNotesUrl(It.IsAny<string>()))
				.Returns(releaseNotesUrl);

			//when
			await _versionFinder.GetDriverUrl();

			//then
			_httpClientWrapper.Verify(v => v.GetStringAsync(releaseNotesUrl));
			_chromeDriverSite.Verify(v => v.GetReleaseNotesUrl(latestRelease));
			_releaseFinder.Verify(v => v.Find());
		}
		
		[Test]
		public async Task should_parse_compatible_driver_version_for_detected_chrome_version_from_release_notes()
		{
			//given
			var releaseNotes = 
				"----------ChromeDriver v2.27 (2016-12-23)----------\r\n" +
				"Supports Chrome v54-56Supports Chrome v60-62";

			_httpClientWrapper.Setup(v => v.GetStringAsync(It.IsAny<Uri>()))
				.ReturnsAsync(releaseNotes);

			var detectedChromeVersion = "60";
			_versionDetector.Setup(v => v.Detect()).Returns(detectedChromeVersion);

			//when
			await _versionFinder.GetDriverUrl();

			//then
			_releaseNotesParser.Verify(v => v.FindCompatibleDriverVersion(releaseNotes, detectedChromeVersion));
		}

		[Test]
		public async Task should_return_url_to_latest_compatible_version()
		{
			//given
			var compatibleVersion = "2.27";
			_releaseNotesParser.Setup(v => v.FindCompatibleDriverVersion(It.IsAny<string>(), It.IsAny<string>()))
				.Returns(compatibleVersion);

			var driverZipUrl = new Uri("http://google.com/chromedriver.zip");
			_chromeDriverSite.Setup(s => s.GetDriverZipUrl(It.IsAny<string>()))
				.Returns(driverZipUrl);

			//when
			var result = await _versionFinder.GetDriverUrl();

			//then
			Assert.That(result, Is.EqualTo(driverZipUrl));
			_chromeDriverSite.Verify(s => s.GetDriverZipUrl(compatibleVersion));
		}
	}
}