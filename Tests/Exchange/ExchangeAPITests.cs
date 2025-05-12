using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tokero.TestData.Models;
using TokeroTests.Fixtures;

namespace Tokero.Tests.Exchange
{
    public class ExchangeAPITests : PlaywrightTestFixture
    {
        // Test that the Coin Pair Prices API returns valid prices
        [Test]
        public async Task CoinPairPricesApi_ReturnsValidPrices()
        {
            using var client = new HttpClient();

            // Set up the API request
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://gate.tokero.com/api/coin-pair-prices-cached/?spendBase=0&receiveBase=0");

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "Mozilla/5.0");

            // Send the API request
            var response = await client.SendAsync(request);

            // Assert the response status is OK (200)
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected 200 OK response");

            // Parse the response content to a dictionary of coin pairs
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var coinPairs = JsonSerializer.Deserialize<Dictionary<string, CoinPairDetails>>(content, options);

            // Assert the coin pairs are not null and not empty
            Assert.That(coinPairs, Is.Not.Null);
            Assert.That(coinPairs, Is.Not.Empty);
        }

        // Test the performance of the Coin Pair Prices API response
        [Test]
        public async Task CoinPairPricesApi_PerformanceTest()
        {
            using var client = new HttpClient();
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            // Set up the API request for performance testing
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://gate.tokero.com/api/coin-pair-prices-cached/?spendBase=0&receiveBase=0");

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "Mozilla/5.0");

            // Send the API request
            var response = await client.SendAsync(request);

            // Stop the stopwatch after the response is received
            stopwatch.Stop();

            // Assert that the response time is less than 2 seconds
            Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(2000),
                        $"API response took {stopwatch.ElapsedMilliseconds}ms, which is longer than expected.");
        }
    }
}
