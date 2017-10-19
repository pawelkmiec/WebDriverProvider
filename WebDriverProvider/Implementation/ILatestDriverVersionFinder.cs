using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	internal interface ILatestDriverVersionFinder
	{
		Task<string> Find();
	}
}