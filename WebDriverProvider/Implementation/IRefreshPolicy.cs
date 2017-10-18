using System.IO;

namespace WebDriverProvider.Implementation
{
	internal interface IRefreshPolicy
	{
		bool ShouldDownload(string driverFileName, DirectoryInfo downloadDirectory);
	}
}