using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Builders;
using NUnit.Framework.Internal;
using Tokero.Fixtures;
using Tokero.TestData;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class MultiBrowserTest : NUnitAttribute, ITestBuilder
{
    private readonly Type? _sourceType;

    public MultiBrowserTest() { }

    public MultiBrowserTest(Type sourceType)
    {
        _sourceType = sourceType;
    }

    public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
    {
        IEnumerable<object[]> testCases;

        if (_sourceType != null)
        {
            var member = _sourceType.GetMethod(nameof(LanguageCases.LanguageTestCases));
            if (member == null)
                throw new InvalidOperationException($"TestCaseSource method not found in '{_sourceType.Name}'.");

            var source = member.Invoke(null, null);

            testCases = ExtractArguments(source);
        }
        else
        {
            testCases = new List<object[]> { Array.Empty<object>() };
        }

        var testCaseBuilder = new NUnitTestCaseBuilder();

        foreach (var testCaseArgs in testCases)
        {
            foreach (BrowserTypeEnum browser in Enum.GetValues(typeof(BrowserTypeEnum)))
            {
                var fullParams = new object[] { browser }.Concat(testCaseArgs).ToArray();
                var parameters = new TestCaseParameters(fullParams);
                var testMethod = testCaseBuilder.BuildTestMethod(method, suite, parameters);

                var argPart = testCaseArgs.Length > 0 ? $"_{string.Join("_", testCaseArgs)}" : "";
                testMethod.Name = $"{method.Name}_{browser}{argPart}";
                testMethod.Properties.Set(PropertyNames.Category, browser.ToString());

                yield return testMethod;
            }
        }
    }

    private static IEnumerable<object[]> ExtractArguments(object source)
    {
        if (source is IEnumerable<TestCaseData> testCaseData)
        {
            foreach (var data in testCaseData)
                yield return data.Arguments;
        }
        else if (source is IEnumerable<object[]> objectArrays)
        {
            foreach (var array in objectArrays)
                yield return array;
        }
        else if (source is IEnumerable<object> genericEnumerable)
        {
            foreach (var item in genericEnumerable)
            {
                if (item is object[] array)
                    yield return array;
                else
                    yield return new[] { item };
            }
        }
        else
        {
            throw new InvalidCastException("Test case source must be IEnumerable<TestCaseData> or IEnumerable<object[]>.");
        }
    }
}
