using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NASADataApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;

namespace NASAPhotoDownloader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Start downloading...");
            // Setup DI container
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient<INASAApi, NASAApi>();

            // Build service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Resolve NASAApi instance from DI container
            var nasaApi = serviceProvider.GetRequiredService<INASAApi>();
            await DownloadFilesAsync();
            Console.WriteLine("Download completed.");
        }

        static async Task DownloadFilesAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var nasaapi = new NASAApi(httpClient);
                NASADM data;

                using (var fileStream = new FileStream("dates.txt", FileMode.Open))
                using (var reader = new StreamReader(fileStream))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = await reader.ReadLineAsync();

                        DateTime? date = DateParser.ParseDate(line);
                        if (date != null)
                        {
                            data = await nasaapi.GetImagesByDateAsync((DateTime)date);

                            if (!string.IsNullOrEmpty(data.url))
                            {
                                using (HttpClient client = new HttpClient())
                                {
                                    using (var response = await client.GetAsync(data.url))
                                    {
                                        response.EnsureSuccessStatusCode();

                                        Directory.CreateDirectory("DownloadedImages");
                                        string fileName = $"{data.date.Year}-{data.date.Month:D2}-{data.date.Day:D2}.jpg";
                                        string filePath = Path.Combine("DownloadedImages", fileName);

                                        using (var fileStreamInner = new FileStream(filePath, FileMode.Create))
                                        {
                                            await response.Content.CopyToAsync(fileStreamInner);
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
