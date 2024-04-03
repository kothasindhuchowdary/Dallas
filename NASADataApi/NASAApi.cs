using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NASADataApi
{
    public class NASAApi
    {
        const string nasaApodUrl = "https://api.nasa.gov/planetary/apod?api_key=oHneS2DMNT9l0ZyMwn6xNgngXMHBpX94dGrnU58Q&date=";

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
