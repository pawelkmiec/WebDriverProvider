using System.IO;

namespace WebDriverProvider.Implementation.RefreshPolicy
{
	public class AlwaysRefreshPolicy : IRefreshPolicy
	{
		public bool ShouldDownload(string driverFileName, DirectoryInfo downloadDirectory)
		{
			return true;
		}
	}
}