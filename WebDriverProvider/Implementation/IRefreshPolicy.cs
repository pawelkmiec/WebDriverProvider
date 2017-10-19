using System.IO;

namespace WebDriverProvider.Implementation
{
	internal interface IRefreshPolicy
	{
		bool ShouldDownload(IWebDriverInfo newDriverInfo, DirectoryInfo downloadDirectory);
	}
}