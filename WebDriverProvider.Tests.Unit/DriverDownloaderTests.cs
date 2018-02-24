using System;
using System.IO;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WebDriverProvider.Implementation.Chrome;
using WebDriverProvider.Implementation.Utilities;

namespace WebDriverProvider.Tests.Unit
{
	public class DriverDownloaderTests
	{
		private Mock<IHttpClientWrapper> _httpClientWrapper;
		private ChromeDriverDownloader _driverDownloader;
		private Mock<IFileSystemWrapper> _fileSystemWrapper;
		private MemoryStream _driverStream;

		[SetUp]
		public void Setup()
		{
			_httpClientWrapper = new Mock<IHttpClientWrapper>();
			_fileSystemWrapper = new Mock<IFileSystemWrapper>();
			_driverDownloader = new ChromeDriverDownloader(_httpClientWrapper.Object, _fileSystemWrapper.Object);

			_driverStream = new MemoryStream();
			_httpClientWrapper.Setup(s => s.GetStreamAsync(It.IsAny<Uri>())).ReturnsAsync(_driverStream);
		}

		[Test]
		public async Task should_return_unzipped_driver()
		{
			//given
			var driverUrl = new Uri("http://google.com/chromedriver.zip");

			var stream = new MemoryStream();
			_fileSystemWrapper.Setup(v => v.UnzipSingleFile(It.IsAny<Stream>()))
				.ReturnsAsync(stream);

			//when
			await _driverDownloader.Download(driverUrl);

			//then
			Assert.That(stream, Is.EqualTo(stream));
		}
	}
}
