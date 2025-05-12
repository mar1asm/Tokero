using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tokero.Fixtures;
using Tokero.Pages;
using Tokero.TestData;
using Tokero.TestData.Models;
using TokeroTests.Fixtures;

namespace Tokero.Tests.Exchange
{
    public class ExchangeTests : PlaywrightTestFixture
    {
        // Test to verify that the default parity is set to EUR
        [Test, MultiBrowserTest]
        public async Task Exchange_DefaultParityIsEUR(BrowserTypeEnum browser)
        {
            var exchange = new ExchangePage(Page);
            await exchange.GoToAsync();

            // Get the selected parity and verify it's set to EUR by default
            var parity = await exchange.GetSelectedParityAsync();
            Assert.That(parity, Is.EqualTo("EUR"));
        }

        // Test to verify that switching parity updates the table rows
        [MultiBrowserTest(typeof(ParitiesCases))]
        public async Task Exchange_SwitchParity_UpdatesTable(BrowserTypeEnum browser, string parity)
        {
            var exchange = new ExchangePage(Page);
            await exchange.GoToAsync();

            // Switch to the selected parity and validate the change
            await exchange.SelectParityAsync(parity);
            Assert.That(await exchange.GetSelectedParityAsync(), Is.EqualTo(parity));

            // Verify that the data table has rows after switching parity
            Assert.That(await exchange.DataTableRows.CountAsync(), Is.GreaterThan(0));
        }
    }
}
