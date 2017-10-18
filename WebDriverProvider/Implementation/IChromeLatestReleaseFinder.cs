using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	internal interface IChromeLatestReleaseFinder
	{
		Task<string> Find();
	}
}