using System.IO;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation.Chrome
{
	internal class RemoteChromeDriverInfo : IWebDriverInfo
	{
		private readonly VersionSpecificUrl _driverDownloadUrl;
		private readonly IDriverDownloader _downloader;

		public RemoteChromeDriverInfo(VersionSpecificUrl driverDownloadUrl, IDriverDownloader downloader)
		{
			_driverDownloadUrl = driverDownloadUrl;
			_downloader = downloader;
		}

		public DriverVersion GetDriverVersion()
		{
			return _driverDownloadUrl.Version;
		}

		public async Task<Stream> Download()
		{
			return await _downloader.Download(_driverDownloadUrl.Url);
		}
	}
}