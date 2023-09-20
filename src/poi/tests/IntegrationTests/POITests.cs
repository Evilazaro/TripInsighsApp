using System;
using poi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests
{
    public class POIIntegrationTests: IClassFixture<CustomWebApplicationFactory<poi.Startup>>
    {
        private readonly CustomWebApplicationFactory<poi.Startup> _factory;

        public POIIntegrationTests(CustomWebApplicationFactory<poi.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/poi/")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            var port = Environment.GetEnvironmentVariable("WEB_INTEGRATION_PORT");

            // Arrange
            var client = _factory.CreateClient(
                new WebApplicationFactoryClientOptions{
                    BaseAddress = new Uri("http://localhost:" + port)
                }
            );

            // Act
            var response = await client.GetAsync(url);


            // Asserts (Check status code, content type and actual response)
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());

            //deserialize response to poi list
            List<POI> pois = JsonConvert.DeserializeObject<List<POI>>(
                await response.Content.ReadAsStringAsync());

            //Check that 3 pois are returned
            Assert.Equal(3,
            pois.Count);
        }
    }
}