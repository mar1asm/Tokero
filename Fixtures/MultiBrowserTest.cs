using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Builders;
using NUnit.Framework.Internal;
using Tokero.Fixtures;
using Tokero.TestData;
using System.Reflection;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class MultiBrowserTest : NUnitAttribute, ITestBuilder
{
    private readonly Type? _sourceType;

    // Default constructor
    public MultiBrowserTest() { }

    // Constructor with the source type parameter
    public MultiBrowserTest(Type sourceType)
    {
        _sourceType = sourceType;
    }

    // Builds the tests from the specified method and suite, iterating through test cases and browsers.
    public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
    {
        IEnumerable<object[]> testCases;

        if (_sourceType != null)
        {
            // Get the method returning IEnumerable<TestCaseData> from the provided source type
            var methodInfo = _sourceType.GetMethods(BindingFlags.Static | BindingFlags.Public)
                .FirstOrDefault(m => typeof(IEnumerable<TestCaseData>).IsAssignableFrom(m.ReturnType));

            if (methodInfo == null)
                throw new InvalidOperationException($"No method returning IEnumerable<TestCaseData> found in '{_sourceType.Name}'.");

            var source = methodInfo.Invoke(null, null);
            testCases = ExtractArguments(source);
        }
        else
        {
            // Default to an empty test case if no source type is provided
            testCases = new List<object[]> { Array.Empty<object>() };
        }

        var testCaseBuilder = new NUnitTestCaseBuilder();

        // Iterate through the test cases and the available browsers
        foreach (var testCaseArgs in testCases)
        {
            foreach (BrowserTypeEnum browser in Enum.GetValues(typeof(BrowserTypeEnum)))
            {
                // Combine the browser type and the test case arguments
                var fullParams = new object[] { browser }.Concat(testCaseArgs).ToArray();
                var parameters = new TestCaseParameters(fullParams);
                var testMethod = testCaseBuilder.BuildTestMethod(method, suite, parameters);

                // Set the test method's name and category based on the arguments and browser
                var argPart = testCaseArgs.Length > 0 ? $"_{string.Join("_", testCaseArgs)}" : "";
                testMethod.Name = $"{method.Name}_{browser}{argPart}";
                testMethod.Properties.Set(PropertyNames.Category, browser.ToString());

                yield return testMethod;
            }
        }
    }

    // Extracts arguments from the source, which could be IEnumerable<TestCaseData>, IEnumerable<object[]>, or IEnumerable<object>.
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
