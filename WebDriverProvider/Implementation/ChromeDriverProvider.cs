using System.IO;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	public class ChromeDriverProvider : IDriverProvider
	{
		private readonly IDriverVersionFinder _driverVersionFinder;
		private readonly IDriverDownloader _driverDownloader;
		private readonly IFileSystemWrapper _fileSystemWrapper;
		private readonly IRefreshPolicy _refreshPolicy;

		public ChromeDriverProvider(
			IDriverVersionFinder driverVersionFinder, 
			IDriverDownloader driverDownloader, 
			IFileSystemWrapper fileSystemWrapper, 
			IRefreshPolicy refreshPolicy
		)
		{
			_driverVersionFinder = driverVersionFinder;
			_driverDownloader = driverDownloader;
			_fileSystemWrapper = fileSystemWrapper;
			_refreshPolicy = refreshPolicy;
		}

		public async Task<DirectoryInfo> GetDriverBinary()
		{
			var driverUrl = await _driverVersionFinder.FindDriverUrl();
			var currentDirectory = _fileSystemWrapper.GetCurrentDirectory();
			var shouldDownload = _refreshPolicy.ShouldDownload("", new DirectoryInfo("c:\\"));
			if (shouldDownload)
			{
				var driverLocation = await _driverDownloader.Download(driverUrl, currentDirectory);
				var driverDirectory = Path.GetDirectoryName(driverLocation.FullName);
				return new DirectoryInfo(driverDirectory);
			}
			return null;
		}
	}
}