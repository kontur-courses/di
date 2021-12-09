using System.Collections.Generic;
using NUnit.Framework;
using TagCloud.Infrastructure.Lemmatizer;

namespace TagCloudTests.Infrastructure.WordProcessing;

internal class FilterTestCases
{
    public static readonly IReadOnlyList<string> Strings = new List<string> { "1", "11", "2", "22" };

    public static readonly IReadOnlyList<Lemma> Lemmas = new List<Lemma>
    {
        new("1", PartOfSpeech.Noun),
        new("11", PartOfSpeech.Noun),
        new("22", PartOfSpeech.Adjective),
    };

    public static IEnumerable<TestCaseData> FilterStringTestCaseDatas
    {
        get
        {
            yield return new TestCaseData(
                    Strings,
                    new List<string> { "11", "22" })
                .SetName("ShouldReturnCorrectEnumerable_WhenThereIsFilterAndInputAreStrings");
        }
    }

    public static IEnumerable<TestCaseData> FilterLemmaTestCaseDatas
    {
        get
        {
            yield return new TestCaseData(
                    Lemmas,
                    new List<string> { "11" })
                .SetName("ShouldReturnCorrectEnumerable_WhenThereIsFilterAndInputAreLemmas");
        }
    }
}