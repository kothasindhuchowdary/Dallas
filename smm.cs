using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace APODPhotoDownloader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string apiKey = "YOUR_NASA_API_KEY";
            string date = "2022-03-30"; // Example date, you can change it as needed

            await DownloadAPODPhoto(apiKey, date);
        }

        static async Task DownloadAPODPhoto(string apiKey, string date)
        {
            using (var httpClient = new HttpClient())
            {
                string url = $"https://api.nasa.gov/planetary/apod?date={date}&api_key={apiKey}";

                try
                {
                    var response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // Parse JSON response and extract image URL
                    // This part depends on the structure of the API response
                    // For simplicity, let's assume there's a property "url" containing the image URL
                    // JObject responseObject = JObject.Parse(responseBody);
                    // string imageUrl = responseObject["url"].ToString();

                    // If "url" is not available, check for "hdurl" or other appropriate properties

                    // await DownloadImage(imageUrl);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Error calling NASA API: {e.Message}");
                }
            }
        }

        static async Task DownloadImage(string imageUrl)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync(imageUrl);
                    response.EnsureSuccessStatusCode();

                    // Extract the filename from the URL
                    string filename = Path.GetFileName(imageUrl);

                    using (var fileStream = File.Create(filename))
                    {
                        await response.Content.CopyToAsync(fileStream);
                    }

                    Console.WriteLine($"Downloaded {filename}");
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Error downloading image: {e.Message}");
                }
            }
        }
    }
}
