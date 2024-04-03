using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace APODPhotoDownloader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .Configure(app =>
                {
                    app.Run(async context =>
                    {
                        if (context.Request.Method == "POST" && context.Request.Path == "/process")
                        {
                            string apiKey = "DEMO_KEY"; // Replace with your NASA API key
                            string date = context.Request.Form["date"];
                            string imageUrl = await GetAPODImageUrl(apiKey, date);
                            await context.Response.WriteAsync(imageUrl);
                        }
                        else
                        {
                            string indexHtmlPath = Path.Combine(Directory.GetCurrentDirectory(), "index.html");
                            string htmlContent = await File.ReadAllTextAsync(indexHtmlPath);
                            context.Response.ContentType = "text/html";
                            await context.Response.WriteAsync(htmlContent);
                        }
                    });
                })
                .Build();

            await host.RunAsync();
        }

        static async Task<string> GetAPODImageUrl(string apiKey, string date)
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
                    JObject responseObject = JObject.Parse(responseBody);
                    string imageUrl = responseObject["url"].ToString();

                    return imageUrl;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Error calling NASA API: {e.Message}");
                    return null;
                }
            }
        }
    }
}
