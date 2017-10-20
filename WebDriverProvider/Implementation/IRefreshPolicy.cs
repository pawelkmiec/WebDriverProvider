using System.IO;
using WebDriverProvider.Implementation.DriverInfo;

namespace WebDriverProvider.Implementation
{
	internal interface IRefreshPolicy
	{
		bool ShouldDownload(IWebDriverInfo newDriverInfo, DirectoryInfo downloadDirectory);
	}
}