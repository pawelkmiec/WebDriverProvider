using System.IO;

namespace WebDriverProvider.Implementation.RefreshPolicy
{
	internal class DefaultRefreshPolicy : IRefreshPolicy
	{
		private readonly IFileSystemWrapper _fileSystemWrapper;

		public DefaultRefreshPolicy(IFileSystemWrapper fileSystemWrapper)
		{
			_fileSystemWrapper = fileSystemWrapper;
		}

		public bool ShouldDownload(string driverFileName, DirectoryInfo downloadDirectory)
		{
			var fileExists = _fileSystemWrapper.FileExists(driverFileName, downloadDirectory);
			var shouldDownload = !fileExists;
			return shouldDownload;
		}
	}
}