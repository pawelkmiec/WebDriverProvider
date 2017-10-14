using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
    public class DriverDownloader : IDriverDownloader
    {
	    private readonly IHttpClientWrapper _httpClientWrapper;
	    private readonly IFileSystemWrapper _fileSystemWrapper;

	    public DriverDownloader(IHttpClientWrapper httpClientWrapper, IFileSystemWrapper fileSystemWrapper)
	    {
		    _httpClientWrapper = httpClientWrapper;
		    _fileSystemWrapper = fileSystemWrapper;
	    }

	    public async Task<FileInfo> Download(Uri driverUrl, DirectoryInfo downloadDirectory)
	    {
		    var zippedDriver = await DownloadZippedDriver(driverUrl, downloadDirectory);

			try
			{
			    var unzippedDriver = await _fileSystemWrapper.Unzip(zippedDriver);
			    return unzippedDriver;
		    }
		    finally
		    {
			    _fileSystemWrapper.Delete(zippedDriver);
		    }
	    }

	    private async Task<FileInfo> DownloadZippedDriver(Uri driverUrl, DirectoryInfo downloadDirectory)
	    {
		    using (var driverStream = await _httpClientWrapper.GetStreamAsync(driverUrl))
		    {
			    var lastPartOfUrl = driverUrl.Segments.Last();
			    var driverZipFullPath = Path.Combine(downloadDirectory.FullName, lastPartOfUrl);
			    var zippedDriver = new FileInfo(driverZipFullPath);
			    await _fileSystemWrapper.SaveStream(driverStream, zippedDriver);
			    return zippedDriver;
		    }
	    }
    }
}
