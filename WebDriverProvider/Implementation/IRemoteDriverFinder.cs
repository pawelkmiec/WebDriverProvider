using System.Threading.Tasks;
using WebDriverProvider.Implementation.Chrome;

namespace WebDriverProvider.Implementation
{
	internal interface IRemoteDriverFinder
	{
		Task<RemoteChromeDriverInfo> FindDriverInfo();
	}
}