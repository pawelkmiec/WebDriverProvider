using System.IO;

namespace WebDriverProvider.Implementation.RefreshPolicy
{
	internal class AlwaysRefreshPolicy : IRefreshPolicy
	{
		public bool ShouldDownload(string driverFileName, DirectoryInfo downloadDirectory)
		{
			return true;
		}
	}
}