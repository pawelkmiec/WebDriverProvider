using System.IO;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	internal class ChromeDriverProvider : IDriverProvider
	{
		private readonly IDriverFinder _driverFinder;
		private readonly IDriverDownloader _driverDownloader;
		private readonly IFileSystemWrapper _fileSystemWrapper;
		private readonly IRefreshPolicy _refreshPolicy;

		public ChromeDriverProvider(
			IDriverFinder driverFinder, 
			IDriverDownloader driverDownloader, 
			IFileSystemWrapper fileSystemWrapper, 
			IRefreshPolicy refreshPolicy
		)
		{
			_driverFinder = driverFinder;
			_driverDownloader = driverDownloader;
			_fileSystemWrapper = fileSystemWrapper;
			_refreshPolicy = refreshPolicy;
		}

		public async Task<DirectoryInfo> GetDriverBinary()
		{
			var driverInfo = await _driverFinder.FindDriverInfo();
			var currentDirectory = _fileSystemWrapper.GetCurrentDirectory();
			var shouldDownload = _refreshPolicy.ShouldDownload(driverInfo, new DirectoryInfo("c:\\"));
			if (shouldDownload)
			{
				var driverLocation = await _driverDownloader.Download(driverInfo.DownloadUrl, currentDirectory);
				var driverDirectory = Path.GetDirectoryName(driverLocation.FullName);
				return new DirectoryInfo(driverDirectory);
			}
			return null;
		}
	}
}