using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tokero.Fixtures;
using Tokero.Pages;
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

        [Test, MultiBrowserTest]
        public async Task Exchange_SwitchParity_UpdatesTable(BrowserTypeEnum browser)
        {
            var exchange = new ExchangePage(Page);
            await exchange.GoToAsync();

            await exchange.SelectParityAsync("USD");
            Assert.That(await exchange.GetSelectedParityAsync(), Is.EqualTo("USD"));
            var a = await exchange.DataTableRows.CountAsync();
            Assert.That(await exchange.DataTableRows.CountAsync() > 0);
        }
    }
}
