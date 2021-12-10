using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Infrastructure.Lemmatizer;

namespace TagCloudTests.Infrastructure.Lemmatizer;

internal class RussianLemmatizerTests
{
    private readonly RussianLemmatizer sut = new();

    [TestCaseSource(typeof(RussianLemmatizerTestCases), nameof(RussianLemmatizerTestCases.TestCaseDatas))]
    public void TryLemmatize_Should(IEnumerable<string> words, IEnumerable<Lemma> expected)
    {
        var actual = sut.GetLemmas(words);

        actual.Should().BeEquivalentTo(expected);
    }
}