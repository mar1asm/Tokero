using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokero.Utils
{
    public static class TestConfig
    {
        public static string BaseUrl { get; set; } = "https://tokero.com";
        public static string DefaultLanguage { get; set; } = "EN";
        public static bool IsHeadless { get; set; } = true;
        public static int DefaultTimeout { get; set; } = 5000;
    }
}
