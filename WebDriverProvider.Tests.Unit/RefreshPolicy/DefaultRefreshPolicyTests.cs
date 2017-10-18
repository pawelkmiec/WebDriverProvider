using System.IO;
using Moq;
using NUnit.Framework;
using WebDriverProvider.Implementation;
using WebDriverProvider.Implementation.RefreshPolicy;

namespace WebDriverProvider.Tests.Unit.RefreshPolicy
{
	public class DefaultRefreshPolicyTests
	{
		private Mock<IFileSystemWrapper> _fileSystemWrapper;
		private DefaultRefreshPolicy _policy;
		private string _driverFileName;
		private DirectoryInfo _downloadDirectory;

		[SetUp]
		public void Setup()
		{
			_fileSystemWrapper = new Mock<IFileSystemWrapper>();
			_policy = new DefaultRefreshPolicy(_fileSystemWrapper.Object);
			_driverFileName = "chromedriver.exe";
			_downloadDirectory = new DirectoryInfo("c:\\temp");
		}

		[Test]
		public void should_not_download_where_driver_file_name_exists_in_download_directory()
		{
			//given
			_fileSystemWrapper.Setup(s => s.FileExists(It.IsAny<string>(), It.IsAny<DirectoryInfo>())).Returns(true);

			//when
			var result = _policy.ShouldDownload(_driverFileName, _downloadDirectory);
			
			//then
			Assert.That(result, Is.False);
			_fileSystemWrapper.Verify(v => v.FileExists(_driverFileName, _downloadDirectory));
		}

		[Test]
		public void should_download_where_driver_file_name_does_not_exists_in_download_directory()
		{
			//given
			_fileSystemWrapper.Setup(s => s.FileExists(It.IsAny<string>(), It.IsAny<DirectoryInfo>())).Returns(false);

			//when
			var result = _policy.ShouldDownload(_driverFileName, _downloadDirectory);

			//then
			Assert.That(result, Is.True);
			_fileSystemWrapper.Verify(v => v.FileExists(_driverFileName, _downloadDirectory));
		}
	}

}