using System;
using System.IO;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WebDriverProvider.Implementation;

namespace WebDriverProvider.Tests.Unit
{
	public class ChromeDriverProviderTests
	{
		private Mock<IDriverFinder> _driverVersion;
		private Mock<IDriverDownloader> _driverDownloader;
		private Mock<IFileSystemWrapper> _fileSystemWrapper;
		private ChromeDriverProvider _provider;
		private Mock<IRefreshPolicy> _refreshPolicy;

		[SetUp]
		public void Setup()
		{
			_driverVersion = new Mock<IDriverFinder>();
			_driverDownloader = new Mock<IDriverDownloader>();
			_refreshPolicy = new Mock<IRefreshPolicy>();
			_fileSystemWrapper = new Mock<IFileSystemWrapper>();
			_provider = new ChromeDriverProvider(
				_driverVersion.Object, 
				_driverDownloader.Object, 
				_fileSystemWrapper.Object,
				_refreshPolicy.Object
			);

			var driverVersion = "2.33";
			var driverUrl = new Uri("http://google.com/chromedriver.zip");
			var driverInfo = new WebDriverInfo(driverUrl, driverVersion);
			_driverVersion.Setup(s => s.FindDriverInfo()).ReturnsAsync(driverInfo);

			var currentDirectory = new DirectoryInfo(@"c:\\foo");
			_fileSystemWrapper.Setup(s => s.GetCurrentDirectory()).Returns(currentDirectory);

			var needsRefresh = true;
			_refreshPolicy.Setup(s => s.ShouldDownload(It.IsAny<IWebDriverInfo>(), It.IsAny<DirectoryInfo>())).Returns(needsRefresh);

			var driverLocation = new FileInfo("c:\\foo\\chromedriver.exe");
			_driverDownloader.Setup(s => s.Download(It.IsAny<Uri>(), It.IsAny<DirectoryInfo>())).ReturnsAsync(driverLocation);
		}

		[Test]
		public async Task should_query_version_finder()
		{
			//when
			await _provider.GetDriverBinary();

			//then
			_driverVersion.Verify(v => v.FindDriverInfo());
		}

		[Test]
		public async Task should_download_given_driver_version_into_current_directory()
		{
			//given
			var driverVersion = "2.33";
			var driverUrl = new Uri("http://google.com/chrome-driver-2.32");
			var driverInfo = new WebDriverInfo(driverUrl, driverVersion);
			_driverVersion.Setup(s => s.FindDriverInfo()).ReturnsAsync(driverInfo);

			var currentDirectory = new DirectoryInfo(@"c:\\temp");
			_fileSystemWrapper.Setup(s => s.GetCurrentDirectory()).Returns(currentDirectory);

			//when
			await _provider.GetDriverBinary();

			//then
			_driverDownloader.Verify(v => v.Download(driverUrl, currentDirectory));
		}

		[Test]
		public async Task should_not_download_when_policy_says_that_there_is_no_need_to_do_that()
		{
			//given
			_refreshPolicy.Setup(s => s.ShouldDownload(It.IsAny<IWebDriverInfo>(), It.IsAny<DirectoryInfo>())).Returns(false);

			//when
			await _provider.GetDriverBinary();

			//then
			_driverDownloader.Verify(v => v.Download(It.IsAny<Uri>(), It.IsAny<DirectoryInfo>()), Times.Never);
		}

		[Test]
		public async Task should_return_path_to_driver_directory()
		{
			//given
			var driverFullPath = new FileInfo("c:\\temp\\chromedriver.exe");
			_driverDownloader.Setup(s => s.Download(It.IsAny<Uri>(), It.IsAny<DirectoryInfo>())).ReturnsAsync(driverFullPath);

			//when
			var result = await _provider.GetDriverBinary();

			//then
			Assert.That(result, Is.EqualTo(new DirectoryInfo("c:\\temp")));
		}
	}
}
