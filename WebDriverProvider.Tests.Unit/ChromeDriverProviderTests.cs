using System;
using System.IO;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WebDriverProvider;
using WebDriverProvider.Implementation;

namespace NWebDriverManager.Tests.Unit
{
	public class ChromeDriverProviderTests
	{
		private Mock<IDriverVersionFinder> _driverVersionFinder;
		private Mock<IDriverDownloader> _driverDownloader;
		private Mock<IFileSystemWrapper> _fileSystemWrapper;
		private ChromeDriverProvider _provider;

		[SetUp]
		public void Setup()
		{
			_driverVersionFinder = new Mock<IDriverVersionFinder>();
			_driverDownloader = new Mock<IDriverDownloader>();
			_fileSystemWrapper = new Mock<IFileSystemWrapper>();
			_provider = new ChromeDriverProvider(
				_driverVersionFinder.Object, 
				_driverDownloader.Object, 
				_fileSystemWrapper.Object
			);
		}

		[Test]
		public async Task should_query_version_finder()
		{
			//when
			await _provider.GetDriverBinary();

			//then
			_driverVersionFinder.Verify(v => v.GetDriverUrl());
		}

		[Test]
		public async Task should_download_given_driver_version_into_current_directory()
		{
			//given
			var driverUrl = new Uri("http://google.com/chrome-driver-2.32");
			_driverVersionFinder.Setup(s => s.GetDriverUrl()).ReturnsAsync(driverUrl);

			var currentDirectory = new DirectoryInfo(@"c:\\temp");
			_fileSystemWrapper.Setup(s => s.GetCurrentDirectory()).Returns(currentDirectory);

			//when
			await _provider.GetDriverBinary();

			//then
			_driverDownloader.Verify(v => v.Download(driverUrl, currentDirectory));
		}

		[Test]
		public async Task should_return_driver_location()
		{
			//given
			var driverLocation = new FileInfo("c:\\temp\\chromedriver.exe");
			_driverDownloader.Setup(s => s.Download(It.IsAny<Uri>(), It.IsAny<DirectoryInfo>())).ReturnsAsync(driverLocation);

			//when
			var result = await _provider.GetDriverBinary();

			//then
			Assert.That(result, Is.EqualTo(driverLocation));
		}
	}
}
