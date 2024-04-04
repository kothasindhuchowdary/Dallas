using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace NASADataApi.Tests
{
    public class NASAApiTests
    {
        [Fact]
        public async Task GetImagesByDateAsync_ValidDate_ReturnsNASADM()
        {
            // Arrange
            var httpClientMock = new Mock<IHttpClient>();
            httpClientMock
                .Setup(client => client.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync(JsonConvert.SerializeObject(new NASADM
                {
                    copyright = "NASA",
                    date = new DateTime(2022, 4, 3),
                    explanation = "Explanation",
                    hdurl = "https://example.com/image_hd.jpg",
                    media_type = "image",
                    service_version = "1.0",
                    title = "Test Image",
                    url = "https://example.com/image.jpg"
                }));

            var serviceProvider = new ServiceCollection()
                .AddSingleton(httpClientMock.Object)
                .AddSingleton<INASAApi, NASAApi>()
                .BuildServiceProvider();

            var nasaApi = serviceProvider.GetService<INASAApi>();

            // Act
            var result = await nasaApi.GetImagesByDateAsync(new DateTime(2022, 4, 3));

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NASA", result.copyright);
            Assert.Equal(new DateTime(2022, 4, 3), result.date);
            Assert.Equal("Explanation", result.explanation);
            Assert.Equal("https://example.com/image_hd.jpg", result.hdurl);
            Assert.Equal("image", result.media_type);
            Assert.Equal("1.0", result.service_version);
            Assert.Equal("Test Image", result.title);
            Assert.Equal("https://example.com/image.jpg", result.url);
        }

        [Fact]
        public async Task GetImagesByDateAsync_InvalidDate_ReturnsNull()
        {
            // Arrange
            var httpClientMock = new Mock<IHttpClient>();
            httpClientMock
                .Setup(client => client.GetStringAsync(It.IsAny<string>()))
                .ThrowsAsync(new HttpRequestException());

            var serviceProvider = new ServiceCollection()
                .AddSingleton(httpClientMock.Object)
                .AddSingleton<INASAApi, NASAApi>()
                .BuildServiceProvider();

            var nasaApi = serviceProvider.GetService<INASAApi>();

            // Act
            var result = await nasaApi.GetImagesByDateAsync(DateTime.MinValue);

            // Assert
            Assert.Null(result);
        }
    }
}
