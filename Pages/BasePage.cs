using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tokero.Utils;

namespace Tokero.Pages
{
    public abstract class BasePage
    {
        protected readonly IPage _page;
        protected abstract string PageRoute { get; }

        // Constructor to initialize the page object
        protected BasePage(IPage page)
        {
            _page = page;
            //Task.Run(() => AcceptCookiesAsync()).Wait();
        }

        // Navigates to the specified page using the base URL and route
        public async Task GoToAsync()
        {
            await _page.GotoAsync($"{TestConfig.BaseUrl}/{PageRoute}".TrimEnd('/'));
            await AcceptCookiesAsync();
        }

        // Accepts cookies by clicking the "Accept all cookies" button
        public async Task AcceptCookiesAsync()
        {
            var acceptCookiesButton = _page.Locator("button[role='button']:has-text('Accept all cookies')");

            await acceptCookiesButton.WaitForAsync();

            await acceptCookiesButton.ClickAsync();
        }
    }
}
