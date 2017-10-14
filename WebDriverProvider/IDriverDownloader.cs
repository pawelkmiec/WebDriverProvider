using System;
using System.IO;
using System.Threading.Tasks;

namespace WebDriverProvider
{
	public interface IDriverDownloader
	{
		Task<FileInfo> Download(Uri driverUrl, DirectoryInfo downloadDirectory);
	}
}