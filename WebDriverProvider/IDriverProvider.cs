using System.Threading.Tasks;

namespace WebDriverProvider
{
	public interface IDriverProvider
	{
		Task DownloadDriverBinary(string targetDirectory);
	}
}