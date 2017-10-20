using System.IO;
using WebDriverProvider.Implementation.DriverInfo;

namespace WebDriverProvider.Implementation.RefreshPolicy
{
	internal class OnNewCompatibleVersionRefreshPolicy : IRefreshPolicy
	{
		private readonly IShellCommandExecutor _shellCommandExecutor;

		public OnNewCompatibleVersionRefreshPolicy(IShellCommandExecutor shellCommandExecutor)
		{
			_shellCommandExecutor = shellCommandExecutor;
		}

		public bool ShouldDownload(IWebDriverInfo newDriverInfo, DirectoryInfo downloadDirectory)
		{
			var newDriverVersion = newDriverInfo.GetDriverVersion(_shellCommandExecutor);
			
			var localDriverInfo = new LocalChromeDriverInfo(downloadDirectory);
			var localDriverVersion = localDriverInfo.GetDriverVersion(_shellCommandExecutor);

			return DriverVersion.ComparerInstance.Compare(newDriverVersion, localDriverVersion) > 0;
		}
	}
}