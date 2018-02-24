using System.IO;
using WebDriverProvider.Implementation.Utilities;

namespace WebDriverProvider.Implementation.Chrome
{
    internal class LocalChromeDriverFinder : ILocalDriverFinder
	{
		private readonly IFileSystemWrapper _fileSystemWrapper;
		private readonly IShellCommandExecutor _shellCommandExecutor;

		public LocalChromeDriverFinder(IFileSystemWrapper fileSystemWrapper, IShellCommandExecutor shellCommandExecutor)
		{
			_fileSystemWrapper = fileSystemWrapper;
			_shellCommandExecutor = shellCommandExecutor;
		}

		public Option<LocalChromeDriverInfo> FindDriverInfo(string driverDirectory)
		{
			var driverDirectoryInfo = new DirectoryInfo(driverDirectory);

			var driverExists = _fileSystemWrapper.FileExists(Constants.DriverFileName, driverDirectoryInfo);

			if (driverExists)
			{
				return Option.Some(new LocalChromeDriverInfo(driverDirectoryInfo, _shellCommandExecutor));
			}

			return Option.None<LocalChromeDriverInfo>();
		}
	}
}
