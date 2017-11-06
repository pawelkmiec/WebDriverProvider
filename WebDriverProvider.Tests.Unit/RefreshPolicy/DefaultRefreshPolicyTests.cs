using System.IO;
using Moq;
using NUnit.Framework;
using WebDriverProvider.Implementation;
using WebDriverProvider.Implementation.RefreshPolicy;
using WebDriverProvider.Implementation.Utilities;

namespace WebDriverProvider.Tests.Unit.RefreshPolicy
{
	public class DefaultRefreshPolicyTests
	{
		private Mock<IFileSystemWrapper> _fileSystemWrapper;
		private DefaultRefreshPolicy _policy;
		private string _driverFileName;
		private DirectoryInfo _downloadDirectory;
		private Mock<IWebDriverInfo> _remoteDriverInfo;
		private Mock<IWebDriverInfo> _localDriverInfo;

		[SetUp]
		public void Setup()
		{
			_fileSystemWrapper = new Mock<IFileSystemWrapper>();
			_policy = new DefaultRefreshPolicy(_fileSystemWrapper.Object);
			_driverFileName = "chromedriver.exe";
			_remoteDriverInfo = new Mock<IWebDriverInfo>();
			_remoteDriverInfo.SetupGet(s => s.DriverFileName).Returns(_driverFileName);
			_downloadDirectory = new DirectoryInfo("c:\\temp");
			_localDriverInfo = new Mock<IWebDriverInfo>();
		}

		[Test]
		public void should_not_download_where_driver_file_name_exists_in_download_directory()
		{
			//given
			_fileSystemWrapper.Setup(s => s.FileExists(It.IsAny<string>(), It.IsAny<DirectoryInfo>())).Returns(true);
			var emptyLocalDriver = Option.None<IWebDriverInfo>();

			//when
			var result = _policy.ShouldDownload(_remoteDriverInfo.Object, emptyLocalDriver);
			
			//then
			Assert.That(result, Is.False);
			_fileSystemWrapper.Verify(v => v.FileExists(_driverFileName, _downloadDirectory));
		}

		[Test]
		public void should_download_where_driver_file_name_does_not_exists_in_download_directory()
		{
			//given
			_fileSystemWrapper.Setup(s => s.FileExists(It.IsAny<string>(), It.IsAny<DirectoryInfo>())).Returns(false);
			var someLocalDriver = Option.Some(_localDriverInfo.Object);

			//when
			var result = _policy.ShouldDownload(_remoteDriverInfo.Object, someLocalDriver);

			//then
			Assert.That(result, Is.True);
			_fileSystemWrapper.Verify(v => v.FileExists(_driverFileName, _downloadDirectory));
		}
	}
}