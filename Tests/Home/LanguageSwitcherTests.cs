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
    public class LanguageSwitcherTests : PlaywrightTestFixture
    {
        // Test to verify that switching languages shows the expected word on the page
        [Test, MultiBrowserTest(typeof(LanguageCases))]
        public async Task LanguageSwitch_ShowsExpectedWord(BrowserTypeEnum browser, string langCode, string expectedWord)
        {
            var homePage = new HomePage(Page);

            // Go to the home page
            await homePage.GoToAsync();

            // Select the specified language from the language switcher
            await homePage.SelectLanguageAsync(langCode);

            // Check if the expected word is visible on the page after the language switch
            var visible = await homePage.IsKeywordVisibleAsync(expectedWord);

            // Assert that the word is visible
            Assert.That(visible, $"Expected '{expectedWord}' for '{langCode}' in {browser}");
        }
    }
}
