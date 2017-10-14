using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
	    private readonly HttpClient _httpClient;

	    public HttpClientWrapper()
	    {
		    _httpClient = new HttpClient();
	    }

	    public async Task<string> GetStringAsync(Uri requestUrl)
	    {
		    var result = await _httpClient.GetStringAsync(requestUrl);
		    return result.Trim();
	    }

	    public async Task<Stream> GetStreamAsync(Uri requestUrl)
	    {
		    return await _httpClient.GetStreamAsync(requestUrl);
	    }
	}
}
