using System.Threading.Tasks;
using WebDriverProvider.Implementation.Chrome;

namespace WebDriverProvider.Implementation
{
	internal interface ILocalDriverFinder
	{
		Option<LocalChromeDriverInfo> FindDriverInfo(string driverDirectory);
	}
}