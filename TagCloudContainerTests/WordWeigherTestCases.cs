using System.Collections.Generic;
using NUnit.Framework;
using TagCloudContainer.Infrastructure.WordWeigher;

namespace TagCloudContainerTests;

internal class WordWeigherTestCases
{
    public static IEnumerable<TestCaseData> WeightedWordsTestCases
    {
        get
        {
            yield return new TestCaseData(
                new List<string> { "слон", "дом", "книга" }, 
                new List<WeightedWord> { new("слон", 1), new("книга", 1), new("дом", 1) })
                .SetName("ReturnCorrectEnumerable_WhenWordsInInputDiffer");

            yield return new TestCaseData(
                new List<string> { "слон", "дом", "слон" },
                new List<WeightedWord> { new("слон", 2), new("дом", 1) })
                .SetName("ReturnCorrectEnumerable_WhenWordsInInputRepeat");

            yield return new TestCaseData(
                new List<string> { "  слон " }, 
                new List<WeightedWord>
                {
                    new("слон", 1)
                }).SetName("ReturnCorrectEnumerable_WhenInputContainsOpeningOrTrailingSpaces");

            yield return new TestCaseData(
                new List<string> { "слон" , "сЛоН"},
                new List<WeightedWord> { new("слон", 2) })
                .SetName("ReturnCorrectEnumerable_WhenInputContainsDifferentCases");

            yield return new TestCaseData(
                new List<string> { "again" },
                new List<WeightedWord> { new("again", 1) })
                .SetName("ReturnCorrectEnumerable_WhenInputContainsWordsInOtherLanguage");

            yield return new TestCaseData(
                    new List<string> { "я", "и", "под", "вообще"},
                    new List<WeightedWord>())
                .SetName("ReturnCorrectEnumerable_WhenInputContainsAuxiliaryPartsOfSpeech");
        }
    }
}