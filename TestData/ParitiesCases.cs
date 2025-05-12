using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokero.TestData
{
    public static class ParitiesCases
    {
        public static readonly List<string> Parities = new()
        {
            "EUR",
            "RON",
            "USD",
            "USDC"
        };

        public static IEnumerable<TestCaseData> ParityTestCases()
        {
            foreach (var parity in Parities) 
            {
                yield return new TestCaseData(parity).SetName($"Switch_To_{parity}");
            }
        }
    }
}
