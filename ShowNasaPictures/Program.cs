using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using NASADataApi;

namespace ShowNasaPictures
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
                 .ConfigureServices(services =>
                 {
                     // Register HttpClient with HttpClientFactory
                     services.AddHttpClient<INASAApi, NASAApi>(client =>
                     {
                         // Configure HttpClient options if needed
                         client.BaseAddress = new Uri("https://api.nasa.gov/");
                     });
                 })
                 .UseStartup<Startup>();
    }
}
