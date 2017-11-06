//using System;
//using System.IO;
//using Moq;
//using NUnit.Framework;
//using WebDriverProvider.Implementation;
//using WebDriverProvider.Implementation.DriverInfo;
//using WebDriverProvider.Implementation.RefreshPolicy;
//using WebDriverProvider.Implementation.Utilities;

//namespace WebDriverProvider.Tests.Unit.RefreshPolicy
//{
//	public class OneNewCompatibleVersionRefreshPolicyTests
//	{
//		private RefreshOnNewVersion _onNewCompatibleVersionRefreshPolicy;
//		private Mock<IShellCommandExecutor> _shellCommandExecutor;
//		private Mock<IWebDriverInfo> _driverInfo;

//		[SetUp]
//		public void Setup()
//		{
//			_driverInfo = new Mock<IWebDriverInfo>();
//			_shellCommandExecutor = new Mock<IShellCommandExecutor>();
//			_onNewCompatibleVersionRefreshPolicy = new RefreshOnNewVersion(_shellCommandExecutor.Object);

//			_driverInfo.Setup(v => v.GetDriverVersion()).Returns(new DriverVersion(2, 15));
//			_driverInfo.SetupGet(s => s.Version).Returns("2.51");
//			_shellCommandExecutor.Setup(s => s.Execute(It.IsAny<string>())).Returns("2.12");
//		}

//		[Test]
//		public void should_get_driver_version_from_download_directory()
//		{
//			//given
//			var downloadDirectory = new DirectoryInfo("c:\\temp");

//			//when
//			_onNewCompatibleVersionRefreshPolicy.ShouldDownload(_driverInfo.Object, downloadDirectory);

//			//then
//			_shellCommandExecutor.VerifySet(s => downloadDirectory.FullName);
//			_driverInfo.Verify(v => v.GetDriverVersion());
//		}

//		[Test]
//		public void should_download_when_driver_from_download_directory_has_lower_version()
//		{
//			//given
//			_driverInfo.Setup(v => v.GetDriverVersion())
//				.Returns(new DriverVersion(2, 12));

//			_shellCommandExecutor.Setup(s => s.Execute(It.IsAny<string>())).Returns("2.10");

//			//when
//			var result = _onNewCompatibleVersionRefreshPolicy.ShouldDownload(_driverInfo.Object, new DirectoryInfo("c:\\foo"));

//			//then
//			Assert.That(result, Is.True);
//		}

//		[Test]
//		public void should_not_download_when_driver_from_download_directory_has_the_same_version()
//		{
//			//given
//			_driverInfo.Setup(v => v.GetDriverVersion())
//				.Returns(new DriverVersion(2, 12));

//			_shellCommandExecutor.Setup(s => s.Execute(It.IsAny<string>())).Returns("2.12");

//			//when
//			var result = _onNewCompatibleVersionRefreshPolicy.ShouldDownload(_driverInfo.Object, new DirectoryInfo("c:\\foo"));

//			//then
//			Assert.That(result, Is.False);
//		}
//	}
//}