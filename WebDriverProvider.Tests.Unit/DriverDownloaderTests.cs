using System;
using System.IO;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WebDriverProvider.Implementation;
using WebDriverProvider.Implementation.Utilities;

namespace WebDriverProvider.Tests.Unit
{
	public class DriverDownloaderTests
	{
		private Mock<IHttpClientWrapper> _httpClientWrapper;
		private DriverDownloader _driverDownloader;
		private Mock<IFileSystemWrapper> _fileSystemWrapper;
		private MemoryStream _driverStream;

		[SetUp]
		public void Setup()
		{
			_httpClientWrapper = new Mock<IHttpClientWrapper>();
			_fileSystemWrapper = new Mock<IFileSystemWrapper>();
			_driverDownloader = new DriverDownloader(_httpClientWrapper.Object, _fileSystemWrapper.Object);

			_driverStream = new MemoryStream();
			_httpClientWrapper.Setup(s => s.GetStreamAsync(It.IsAny<Uri>())).ReturnsAsync(_driverStream);
		}

		[Test]
		public async Task should_get_stream_from_driver_url()
		{
			//given
			var driverUrl = new Uri("http://google.com/chromedriver.zip");
			var anyDirectory = GetAnyDirectory();

			//when
			await _driverDownloader.Download(driverUrl, anyDirectory);

			//then
			_httpClientWrapper.Verify(v => v.GetStreamAsync(driverUrl));
		}

		[Test]
		public async Task should_save_driver_stream_as_file_in_download_directory_with_name_as_last_part_of_driver_url()
		{
			//given
			var driverUrl = new Uri("http://google.com/chromedriver.zip");
			var downloadDirectory = new DirectoryInfo("C:\\download-directory");
			var downloadedZipFullpath = "C:\\download-directory\\chromedriver.zip";

			//when
			await _driverDownloader.Download(driverUrl, downloadDirectory);

			//then
			_fileSystemWrapper.Verify(v => v.SaveStream(_driverStream, It.Is<FileInfo>(f => f.FullName == downloadedZipFullpath)));
		}

		[Test]
		public async Task should_unzip_downloaded_driver_file()
		{
			//given
			var driverUrl = new Uri("http://google.com/chromedriver.zip");
			var downloadDirectory = new DirectoryInfo("C:\\download-directory");
			var downloadedZipFullpath = "C:\\download-directory\\chromedriver.zip";

			//when
			await _driverDownloader.Download(driverUrl, downloadDirectory);

			//then
			_fileSystemWrapper.Verify(v => v.Unzip(It.Is<FileInfo>(f => f.FullName == downloadedZipFullpath)));
		}

		[Test]
		public async Task should_delete_downloaded_zip()
		{
			//given
			var driverUrl = new Uri("http://google.com/chromedriver.zip");
			var downloadDirectory = new DirectoryInfo("C:\\download-directory");
			var downloadedZipFullpath = "C:\\download-directory\\chromedriver.zip";

			//when
			await _driverDownloader.Download(driverUrl, downloadDirectory);

			//then
			_fileSystemWrapper.Verify(v => v.Delete(It.Is<FileInfo>(f => f.FullName == downloadedZipFullpath)));
		}

		[Test]
		public async Task should_return_unzipped_driver()
		{
			//given
			var driverUrl = new Uri("http://google.com/chromedriver.zip");
			var downloadDirectory = new DirectoryInfo("C:\\download-directory");

			var unzippedDriver = new FileInfo("c:\\download-directory\\chromedriver.exe");
			_fileSystemWrapper.Setup(v => v.Unzip(It.IsAny<FileInfo>()))
				.ReturnsAsync(unzippedDriver);

			//when
			var result = await _driverDownloader.Download(driverUrl, downloadDirectory);

			//then
			Assert.That(result.FullName, Is.EqualTo(unzippedDriver.FullName));
		}

		private static DirectoryInfo GetAnyDirectory()
		{
			return new DirectoryInfo("C:\\temp");
		}
	}
}
