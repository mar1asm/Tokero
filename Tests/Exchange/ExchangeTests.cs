using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tokero.Fixtures;
using Tokero.Pages;
using Tokero.TestData;
using TokeroTests.Fixtures;

namespace Tokero.Tests.Exchange
{
    public class ExchangeTests : PlaywrightTestFixture
    {
        [Test, MultiBrowserTest]
        public async Task Exchange_DefaultParityIsEUR(BrowserTypeEnum browser)
        {
            var exchange = new ExchangePage(Page);
            await exchange.GoToAsync();

            var parity = await exchange.GetSelectedParityAsync();
            Assert.That(parity, Is.EqualTo("EUR"));
        }

        [MultiBrowserTest(typeof(ParitiesCases))]
        public async Task Exchange_SwitchParity_UpdatesTable(BrowserTypeEnum browser, string parity)
        {
            var exchange = new ExchangePage(Page);
            await exchange.GoToAsync();

            await exchange.SelectParityAsync(parity);
            Assert.That(await exchange.GetSelectedParityAsync(), Is.EqualTo(parity));
            Assert.That(await exchange.DataTableRows.CountAsync(), Is.GreaterThan(0));
        }
    }
}
