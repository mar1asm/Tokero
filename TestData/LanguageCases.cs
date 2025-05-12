using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokero.TestData
{
    public class LanguageCases
    {
        public static readonly Dictionary<string, string> Languages = new()
        { 
            { "RO", "Cumpără" },
            { "FR", "Achète" },
            { "IT", "Compra" },
            { "PL", "kupować" }, //idk polish
            { "TR", "satın" },
            { "EN", "Buy" }
        };

        public static IEnumerable<TestCaseData> LanguageTestCases()
        {
            foreach (var (language, expectedWord) in Languages)
            {
                yield return new TestCaseData(language, expectedWord).SetName($"Language_{language}");
            }
        }
    }
}
