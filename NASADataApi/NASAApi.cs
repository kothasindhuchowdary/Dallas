using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NASADataApi
{
    public interface INASAApi
    {
        Task<NASADM> GetImagesByDateAsync(DateTime date);
    }

    public class NASAApi : INASAApi
    {
        const string nasaApodUrl = "https://api.nasa.gov/planetary/apod?api_key=kdfcS4uXX7EmqkLrnz2WlgX4vUnPgvk4EHmPyBgb&date=";

        private readonly HttpClient _httpClient;

        public NASAApi(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<NASADM> GetImagesByDateAsync(DateTime date)
        {
            var searchUrl = nasaApodUrl + date.ToString("yyyy-MM-dd");

            try
            {
                var jsonString = await _httpClient.GetStringAsync(searchUrl);
                return JsonConvert.DeserializeObject<NASADM>(jsonString);
            }
            catch (HttpRequestException ex)
            {
                // Handle error
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
