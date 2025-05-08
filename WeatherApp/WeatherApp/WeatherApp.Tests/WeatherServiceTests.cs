using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;

namespace WeatherApp.Tests
{
    public class WeatherServiceTests
    {
        [Fact]
        public async Task GetWeatherAsync_ReturnsWeatherInfo_WhenCityIsValid()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"main\":{\"temp\":21.0},\"weather":[{\"description\":\"clear sky\"}]}")
                });

            var client = new HttpClient(mockHandler.Object);
            var service = new WeatherServiceForTest(client);

            // Act
            var result = await service.GetWeatherAsync("London");

            // Assert
            Assert.Equal(21.0, result.Temperature);
            Assert.Equal("clear sky", result.Condition);
        }
    }

    public class WeatherServiceForTest : IWeatherService
    {
        private readonly HttpClient _client;
        private const string ApiKey = "test_key";

        public WeatherServiceForTest(HttpClient client)
        {
            _client = client;
        }

        public async Task<WeatherInfo> GetWeatherAsync(string city)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={ApiKey}&units=metric";
            var response = await _client.GetStringAsync(url);
            using var doc = System.Text.Json.JsonDocument.Parse(response);
            var root = doc.RootElement;

            return new WeatherInfo
            {
                Temperature = root.GetProperty("main").GetProperty("temp").GetDouble(),
                Condition = root.GetProperty("weather")[0].GetProperty("description").GetString()
            };
        }
    }
}