using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	internal class HttpClientWrapper : IHttpClientWrapper
    {
	    private readonly HttpClient _httpClient;

	    public HttpClientWrapper()
	    {
		    _httpClient = new HttpClient();
	    }

	    public async Task<string> GetStringAsync(Uri requestUrl)
	    {
		    return await _httpClient.GetStringAsync(requestUrl);
	    }

	    public async Task<Stream> GetStreamAsync(Uri requestUrl)
	    {
		    return await _httpClient.GetStreamAsync(requestUrl);
	    }
	}
}
