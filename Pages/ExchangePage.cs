using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tokero.Utils;

namespace Tokero.Pages
{
    public class ExchangePage : BasePage
    {
        protected override string PageRoute => "/exchange";

        public ExchangePage(IPage page) : base(page) { }


        public new async Task GoToAsync()
        {
            await base.GoToAsync();
        }


        public async Task<string> GetSelectedParityAsync()
        {
            var active = _page.Locator("a.nav-link.active");
            return (await active.InnerTextAsync()).Trim();
        }


        public async Task SelectParityAsync(string parityCode)
        {
            await _page.ClickAsync($"a.nav-link:has-text('{parityCode}')");
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }


        public ILocator DataTableRows => _page.Locator("div.card[class*='exchangeCoin_card']");

    }
}