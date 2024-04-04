using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASADataApi
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string requestUri);
    }

    public class HttpClientWrapper : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetStringAsync(string requestUri)
        {
            return await _httpClient.GetStringAsync(requestUri);
        }
    }
}
