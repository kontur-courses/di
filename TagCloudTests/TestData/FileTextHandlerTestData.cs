namespace TagCloudTests.TestData;

public class FileTextHandlerTestData
{
    public static IEnumerable<TestCaseData> Data
    {
        get
        {
            yield return new TestCaseData(
                    "мама\nмыла\nраму\nа\nраму\nмыла\nмама",
                    new[] { ("мама", 2), ("раму", 2), ("мыла", 2) }
                )
                .SetName("ManyRepeatingWordsAndConjExcluding");
            yield return new TestCaseData("и а или", Array.Empty<(string, int)>())
                .SetName("ExcludingAllConj");
            yield return new TestCaseData(
                    "Черешня\nЯблоки\nАпельсины", 
                    new[] { ("черешня", 1), ("яблоки", 1), ("апельсины", 1) }
                )
                .SetName("ToLowerCase");
        }
    }
}