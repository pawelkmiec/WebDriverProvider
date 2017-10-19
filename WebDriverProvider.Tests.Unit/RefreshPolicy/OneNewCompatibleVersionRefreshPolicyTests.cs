using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using NUnit.Framework;
using WebDriverProvider.Implementation;

namespace WebDriverProvider.Tests.Unit.RefreshPolicy
{
	public class OneNewCompatibleVersionRefreshPolicyTests
	{
		private OnNewCompatibleVersionRefreshPolicy _onNewCompatibleVersionRefreshPolicy;
		private Mock<IShellCommandExecutor> _shellCommandExecutor;
		private Mock<IWebDriverInfo> _driverInfo;

		[SetUp]
		public void Setup()
		{
			_driverInfo = new Mock<IWebDriverInfo>();
			_shellCommandExecutor = new Mock<IShellCommandExecutor>();
			_onNewCompatibleVersionRefreshPolicy = new OnNewCompatibleVersionRefreshPolicy(_shellCommandExecutor.Object);

			_driverInfo.Setup(v => v.GetDriverVersion(It.IsAny<IShellCommandExecutor>())).Returns("2.15");
			_driverInfo.SetupGet(s => s.Version).Returns("2.51");
		}

		[Test]
		public void should_get_driver_version_from_download_directory()
		{
			//given
			var downloadDirectory = new DirectoryInfo("c:\\temp");

			//when
			_onNewCompatibleVersionRefreshPolicy.ShouldDownload(_driverInfo.Object, downloadDirectory);

			//then
			_shellCommandExecutor.VerifySet(s => s.WorkingDirectory = downloadDirectory.FullName);
			_driverInfo.Verify(v => v.GetDriverVersion(_shellCommandExecutor.Object));
		}

		[Test]
		public void should_download_when_driver_from_download_directory_has_lower_version()
		{
			//given
			_driverInfo.Setup(v => v.GetDriverVersion(It.IsAny<IShellCommandExecutor>()))
				.Returns("2.10");

			_driverInfo.SetupGet(s => s.Version).Returns("2.12");

			//when
			var result = _onNewCompatibleVersionRefreshPolicy.ShouldDownload(_driverInfo.Object, new DirectoryInfo("c:\\foo"));

			//then
			Assert.That(result, Is.True);
		}

		[Test]
		public void should_not_download_when_driver_from_download_directory_has_the_same_version()
		{
			//given
			_driverInfo.Setup(v => v.GetDriverVersion(It.IsAny<IShellCommandExecutor>()))
				.Returns("2.12");

			_driverInfo.SetupGet(s => s.Version).Returns("2.12");

			//when
			var result = _onNewCompatibleVersionRefreshPolicy.ShouldDownload(_driverInfo.Object, new DirectoryInfo("c:\\foo"));

			//then
			Assert.That(result, Is.False);
		}
	}

	
}