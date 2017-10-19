using System;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	internal interface IDriverFinder
	{
		Task<WebDriverInfo> FindDriverInfo();
	}
}