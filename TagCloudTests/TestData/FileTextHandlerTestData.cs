namespace TagCloudTests.TestData;

public class FileTextHandlerTestData
{
    public static IEnumerable<TestCaseData> Data
    {
        get
        {
            yield return new TestCaseData(
                    "мама\nмыла\nраму\nа\nраму\nмыла\nмама",
                    new Dictionary<string, int> { { "мама", 2 }, { "раму", 2 }, { "мыла", 2 } }
                )
                .SetName("ShouldExcludeConjAndNotExcludeOtherWords");
            yield return new TestCaseData("и а или", new Dictionary<string, int>())
                .SetName("ShouldExcludeAllConj");
            yield return new TestCaseData(
                    "Черешня\nЯблоки\nАпельсины",
                    new Dictionary<string, int> { { "черешня", 1 }, {"яблоки", 1 }, {"апельсины", 1 } }
                )
                .SetName("ShouldTransformToLowerCase");
        }
    }
}