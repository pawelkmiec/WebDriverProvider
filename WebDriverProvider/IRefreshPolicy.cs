using System.IO;

namespace WebDriverProvider
{
	public interface IRefreshPolicy
	{
		bool ShouldDownload(string driverFileName, DirectoryInfo downloadDirectory);
	}
}