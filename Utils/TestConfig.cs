using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tokero.Fixtures;

namespace Tokero.Utils
{
    public static class TestConfig
    {
        public static string BaseUrl { get; set; } = "https://tokero.com";
        public static string DefaultLanguage { get; set; } = "EN";
        public static bool IsHeadless { get; set; } = false;
        public static int DefaultTimeout { get; set; } = 5000;

        public static BrowserTypeEnum DefaultBrowser { get; set; } = BrowserTypeEnum.Chromium;
    }
}
