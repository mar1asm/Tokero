using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;
using Tokero.Fixtures;
using Tokero.Pages;
using Tokero.Utils;

namespace TokeroTests.Fixtures
{
    public class PlaywrightTestFixture
    {
        protected IBrowser? Browser;
        protected IPage? Page;
        protected IBrowserContext? Context;
        protected IPlaywright? Playwright;
        private BrowserTypeEnum _currentBrowser;

        // Setup method that runs before each test.
        // Determines the browser to use based on test arguments or defaults to the configured browser.
        [SetUp]
        public async Task SetUp()
        {
            _currentBrowser = TestContext.CurrentContext.Test.Arguments.FirstOrDefault(arg => arg is BrowserTypeEnum) is BrowserTypeEnum browser
                ? browser
                : TestConfig.DefaultBrowser;

            // Initialize Playwright and launch the browser according to the selected browser type.
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            Browser = _currentBrowser switch
            {
                BrowserTypeEnum.Chromium => await Playwright.Chromium.LaunchAsync(new() { Headless = TestConfig.IsHeadless, Timeout = TestConfig.DefaultTimeout }),
                BrowserTypeEnum.Firefox => await Playwright.Firefox.LaunchAsync(new() { Headless = TestConfig.IsHeadless, Timeout = TestConfig.DefaultTimeout }),
                BrowserTypeEnum.Webkit => await Playwright.Webkit.LaunchAsync(new() { Headless = TestConfig.IsHeadless, Timeout = TestConfig.DefaultTimeout }),
                _ => throw new ArgumentOutOfRangeException()
            };

            // Create a new browser context and page for each test.
            Context = await Browser.NewContextAsync();
            Page = await Context.NewPageAsync();
        }

        // Closes the browser and disposes of Playwright resources.
        [TearDown]
        public async Task TearDown()
        {
            // Close the browser and dispose Playwright resources.
            await Browser?.CloseAsync();
            Playwright?.Dispose();
        }
    }
}
