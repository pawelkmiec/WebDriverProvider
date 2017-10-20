using System.IO;
using WebDriverProvider.Implementation.DriverInfo;

namespace WebDriverProvider.Implementation.RefreshPolicy
{
	internal class AlwaysRefreshPolicy : IRefreshPolicy
	{
		public bool ShouldDownload(IWebDriverInfo newDriverInfo, DirectoryInfo downloadDirectory)
		{
			return true;
		}
	}
}