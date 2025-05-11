using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Builders;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokero.Fixtures
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MultiBrowserTest : NUnitAttribute, ITestBuilder
    {
        public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
        {
            foreach (BrowserTypeEnum browser in Enum.GetValues(typeof(BrowserTypeEnum)))
            {
                var parameters = new TestCaseParameters(new object[] { browser });

                var testMethod = new NUnitTestCaseBuilder().BuildTestMethod(method, suite, parameters);

                testMethod.Name = $"{method.Name}_{browser}";

                yield return testMethod;
            }
        }
    }
}
