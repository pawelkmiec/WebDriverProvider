using System;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	internal interface IDriverVersionFinder
	{
		Task<Uri> FindDriverUrl();
	}
}