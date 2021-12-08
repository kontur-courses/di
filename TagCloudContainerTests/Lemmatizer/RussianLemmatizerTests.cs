using FluentAssertions;
using NUnit.Framework;
using TagCloud.Infrastructure.Lemmatizer;

namespace TagCloudTests.Lemmatizer;

internal class RussianLemmatizerTests
{
    private readonly RussianLemmatizer sut = new();

    [TestCaseSource(typeof(RussianLemmatizerTestCases), nameof(RussianLemmatizerTestCases.TestCaseDatas))]
    public void TryLemmatize_Should(string word, bool expectedBool, Lemma expectedLemma)
    {
        var (isLemmatized, lemma) = sut.TryLemmatize(word);

        isLemmatized.Should().Be(expectedBool);
        lemma.Should().BeEquivalentTo(expectedLemma);
    }
}