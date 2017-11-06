using System;
using System.IO;
using System.Threading.Tasks;
using WebDriverProvider.Implementation.Utilities;

namespace WebDriverProvider.Implementation.Chrome
{
	internal class ChromeDriverDownloader : IDriverDownloader
    {
	    private readonly IHttpClientWrapper _httpClientWrapper;
	    private readonly IFileSystemWrapper _fileSystemWrapper;

	    public ChromeDriverDownloader(IHttpClientWrapper httpClientWrapper, IFileSystemWrapper fileSystemWrapper)
	    {
		    _httpClientWrapper = httpClientWrapper;
		    _fileSystemWrapper = fileSystemWrapper;
	    }

	    public async Task<Stream> Download(Uri driverUrl)
	    {
		    using (var zippedDriverStream = await _httpClientWrapper.GetStreamAsync(driverUrl))
		    {
				var unzippedDriverStream = await _fileSystemWrapper.UnzipSingleFile(zippedDriverStream);
				return unzippedDriverStream;
		    }
		}
    }
}
