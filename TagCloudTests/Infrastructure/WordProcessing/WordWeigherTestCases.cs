using System.Collections.Generic;
using NUnit.Framework;

namespace TagCloudTests.Infrastructure.WordProcessing;

internal class WordWeigherTestCases
{
    public static IEnumerable<TestCaseData> TestCaseDatas
    {
        get
        {
            yield return new TestCaseData(
                    new List<string> { "слон", "дом", "книга" },
                    new Dictionary<string, int> { { "слон", 1 }, { "дом", 1 }, { "книга", 1 } })
                .SetName("ReturnCorrectEnumerable_WhenWordsInInputDiffer");

            yield return new TestCaseData(
                    new List<string> { "слон", "дом", "слон" },
                    new Dictionary<string, int> { { "слон", 2 }, { "дом", 1 } })
                .SetName("ReturnCorrectEnumerable_WhenWordsInInputRepeat");

            yield return new TestCaseData(
                    new List<string> { "  слон " },
                    new Dictionary<string, int> { { "слон", 1 } })
                .SetName("ReturnCorrectEnumerable_WhenInputContainsOpeningOrTrailingSpaces");

            yield return new TestCaseData(
                    new List<string> { "слон", "сЛоН" },
                    new Dictionary<string, int> { { "слон", 2 } })
                .SetName("ReturnCorrectEnumerable_WhenInputContainsDifferentCases");
        }
    }
}