using System.Threading.Tasks;
using WebDriverProvider.Implementation.DriverInfo;

namespace WebDriverProvider.Implementation
{
	internal interface IDriverFinder
	{
		Task<IWebDriverInfo> FindDriverInfo();
	}
}