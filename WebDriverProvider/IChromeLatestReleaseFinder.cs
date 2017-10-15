using System.Threading.Tasks;

namespace WebDriverProvider
{
	public interface IChromeLatestReleaseFinder
	{
		Task<string> Find();
	}
}