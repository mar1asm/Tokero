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
        [Test]
        public async Task CoinPairPricesApi_ReturnsValidPrices()
        {
            using var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://gate.tokero.com/api/coin-pair-prices-cached/?spendBase=0&receiveBase=0");

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "Mozilla/5.0");

            var response = await client.SendAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected 200 OK response");

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var coinPairs = JsonSerializer.Deserialize<Dictionary<string, CoinPairDetails>>(content, options);

            Assert.That(coinPairs, Is.Not.Null);
            Assert.That(coinPairs, Is.Not.Empty);
        }

        [Test]
        public async Task CoinPairPricesApi_PerformanceTest()
        {
            using var client = new HttpClient();
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://gate.tokero.com/api/coin-pair-prices-cached/?spendBase=0&receiveBase=0");

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "Mozilla/5.0");

            var response = await client.SendAsync(request);

            stopwatch.Stop();

            Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(2000),
                        $"API response took {stopwatch.ElapsedMilliseconds}ms, which is longer than expected.");
        }
    }
}