using Tokero.Fixtures;
using TokeroTests.Fixtures;

namespace Tokero.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Tests : PlaywrightTestFixture
    {
        [Test]
        [MultiBrowserTest]
        public async Task TestSetupMultiBrowser(BrowserTypeEnum browser)
        {
            await Page.GotoAsync("https://tokero.com/");
            var title = await Page.TitleAsync();
            Assert.That(title, Does.Contain("TOKERO"));
        }
    }
}
