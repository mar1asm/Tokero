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

        protected BasePage(IPage page)
        {
            _page = page;
        }

        public async Task GoToAsync()
        {
            await _page.GotoAsync($"{TestConfig.BaseUrl}/{PageRoute}".TrimEnd('/'));
        }
    }
}
