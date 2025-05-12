using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tokero.Fixtures;
using Tokero.Utils;

namespace Tokero.Pages
{
    public class HomePage : BasePage
    {
        protected override string PageRoute => "";

        public HomePage(IPage page) : base(page) { }

        // TODO : Create a LanguageService for reusable language switching logic across pages
        // Selects the language from the language switcher dropdown.
        public async Task SelectLanguageAsync(string langCode)
        {
            await _page.ClickAsync("button[class*='languageSwitcher']");
            await _page.ClickAsync($"text={langCode}");
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        // Checks if a given keyword is visible on the page. (e.g. "Buy")
        public async Task<bool> IsKeywordVisibleAsync(string keyword)
        {
            return await _page.Locator($"text={keyword}").First.IsVisibleAsync();
        }
    }
}
