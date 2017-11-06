using System;
using System.IO;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation.Utilities
{
	internal interface IHttpClientWrapper
	{
		Task<string> GetStringAsync(Uri requestUrl);

		Task<Stream> GetStreamAsync(Uri requestUrl);
	}
}