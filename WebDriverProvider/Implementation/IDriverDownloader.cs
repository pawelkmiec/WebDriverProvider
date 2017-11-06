using System;
using System.IO;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	internal interface IDriverDownloader
	{
		Task<Stream> Download(Uri driverUrl);
	}
}