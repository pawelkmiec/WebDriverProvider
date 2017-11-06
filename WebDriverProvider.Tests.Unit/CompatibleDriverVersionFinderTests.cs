//using System;
//using System.Threading.Tasks;
//using Moq;
//using NUnit.Framework;
//using WebDriverProvider.Implementation;
//using WebDriverProvider.Implementation.Utilities;

//namespace WebDriverProvider.Tests.Unit
//{
//	public class CompatibleDriverVersionFinderTests
//	{
//		private Mock<IBrowserVersionDetector> _versionDetector;
//		private Mock<ILatestDriverVersionFinder> _latestVersionFinder;
//		private Mock<IHttpClientWrapper> _httpClientWrapper;
//		private Mock<IChromeDriverReleaseNotesParser> _releaseNotesParser;
//		private Mock<IChromeDriverSite> _chromeDriverSite;
//		private CompatibleChromeLocalDriverFinder _finder;

//		[SetUp]
//		public void Setup()
//		{
//			_versionDetector = new Mock<IBrowserVersionDetector>();
//			_latestVersionFinder = new Mock<ILatestDriverVersionFinder>();
//			_httpClientWrapper = new Mock<IHttpClientWrapper>();
//			_releaseNotesParser = new Mock<IChromeDriverReleaseNotesParser>();
//			_chromeDriverSite = new Mock<IChromeDriverSite>();
//			_finder = new CompatibleChromeLocalDriverFinder(
//				_latestVersionFinder.Object, 
//				_versionDetector.Object, 
//				_httpClientWrapper.Object,
//				_releaseNotesParser.Object,
//				_chromeDriverSite.Object
//			);
//		}

//		[Test]
//		public async Task should_get_release_notes_of_latest_driver_version()
//		{
//			//given
//			var latestVersion = "2.33";
//			_latestVersionFinder.Setup(s => s.Find()).ReturnsAsync(latestVersion);

//			var releaseNotesUrl = new Uri("http://google.com/releasenotes.txt");
//			_chromeDriverSite.Setup(s => s.GetReleaseNotesUrl(It.IsAny<string>()))
//				.Returns(releaseNotesUrl);

//			//when
//			await _finder.FindDriverInfo();

//			//then
//			_httpClientWrapper.Verify(v => v.GetStringAsync(releaseNotesUrl));
//			_chromeDriverSite.Verify(v => v.GetReleaseNotesUrl(latestVersion));
//			_latestVersionFinder.Verify(v => v.Find());
//		}
		
//		[Test]
//		public async Task should_parse_compatible_driver_version_for_detected_chrome_version_from_release_notes()
//		{
//			//given
//			var releaseNotes = 
//				"----------ChromeDriver v2.27 (2016-12-23)----------\r\n" +
//				"Supports Chrome v54-56Supports Chrome v60-62";

//			_httpClientWrapper.Setup(v => v.GetStringAsync(It.IsAny<Uri>()))
//				.ReturnsAsync(releaseNotes);

//			var detectedChromeVersion = "60";
//			_versionDetector.Setup(v => v.Detect()).Returns(detectedChromeVersion);

//			//when
//			await _finder.FindDriverInfo();

//			//then
//			_releaseNotesParser.Verify(v => v.FindCompatibleDriverVersion(releaseNotes, detectedChromeVersion));
//		}

//		[Test]
//		public async Task should_return_url_to_latest_compatible_version()
//		{
//			//given
//			var compatibleVersion = "2.27";
//			_releaseNotesParser.Setup(v => v.FindCompatibleDriverVersion(It.IsAny<string>(), It.IsAny<string>()))
//				.Returns(compatibleVersion);

//			var driverZipUrl = new Uri("http://google.com/chromedriver.zip");
//			_chromeDriverSite.Setup(s => s.GetDriverZipUrl(It.IsAny<string>()))
//				.Returns(driverZipUrl);

//			//when
//			var result = await _finder.FindDriverInfo();

//			//then
//			Assert.That(result.DownloadUrl, Is.EqualTo(driverZipUrl));
//			Assert.That(result.Version, Is.EqualTo(compatibleVersion));
//			_chromeDriverSite.Verify(s => s.GetDriverZipUrl(compatibleVersion));
//		}
//	}
//}