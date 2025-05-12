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

            await Context.Tracing.StartAsync(new()
            {
                Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }

        // Closes the browser and disposes of Playwright resources.
        [TearDown]
        public async Task TearDown()
        {
            //Record a trace only when the test fails.
            var failed = TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Error
            || TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Failure;

            await Context.Tracing.StopAsync(new()
            {
                Path = failed ? Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "TestResults",
                    $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
                    ) : null
            });

            // Close the browser and dispose Playwright resources.
            await Browser?.CloseAsync();
            Playwright?.Dispose();
        }
    }
}
