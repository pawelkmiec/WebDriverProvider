using System;
using System.IO;
using System.Threading.Tasks;

namespace WebDriverProvider
{
	public interface IHttpClientWrapper
	{
		Task<string> GetStringAsync(Uri requestUrl);

		Task<Stream> GetStreamAsync(Uri requestUrl);
	}
}