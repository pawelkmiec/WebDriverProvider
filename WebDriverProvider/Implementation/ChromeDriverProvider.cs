using System.IO;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	public class ChromeDriverProvider : IDriverProvider
	{
		private readonly IDriverVersionFinder _driverVersionFinder;
		private readonly IDriverDownloader _driverDownloader;
		private readonly IFileSystemWrapper _fileSystemWrapper;

		public ChromeDriverProvider(
			IDriverVersionFinder driverVersionFinder, 
			IDriverDownloader driverDownloader, 
			IFileSystemWrapper fileSystemWrapper
		)
		{
			_driverVersionFinder = driverVersionFinder;
			_driverDownloader = driverDownloader;
			_fileSystemWrapper = fileSystemWrapper;
		}

		public async Task<FileInfo> GetDriverBinary()
		{
			var driverUrl = await _driverVersionFinder.GetDriverUrl();
			var currentDirectory = _fileSystemWrapper.GetCurrentDirectory();
			var driverLocation = await _driverDownloader.Download(driverUrl, currentDirectory);
			return driverLocation;
		}
	}
}