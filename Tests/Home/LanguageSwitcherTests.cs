using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tokero.Fixtures;
using Tokero.Pages;
using Tokero.TestData;
using TokeroTests.Fixtures;

namespace Tokero.Tests.Home
{
    public class LanguageSwitcherTests: PlaywrightTestFixture
    {
        [Test, MultiBrowserTest(typeof(LanguageCases))]

        public async Task LanguageSwitch_ShowsExpectedWord(BrowserTypeEnum browser, string langCode, string expectedWord)
        {
            var homePage = new HomePage(Page);

            await homePage.GoToAsync();

            await homePage.SelectLanguageAsync(langCode);

            var visible = await homePage.IsKeywordVisibleAsync(expectedWord);

            Assert.IsTrue(visible, $"Expected '{expectedWord}' for '{langCode}' in {browser}");
        }
    }
}
