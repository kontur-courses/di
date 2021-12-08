using FluentAssertions;
using NUnit.Framework;
using TagCloud.Infrastructure.Lemmatizer;

namespace TagCloudTests;

internal class RussianLemmatizerTests
{
    private readonly RussianLemmatizer sut = new();

    [TestCase(null, false, TestName = "ReturnFalseOnNullString")]
    [TestCase("", false, TestName = "ReturnFalseOnEmptyString")]
    [TestCase("коровы", true, TestName = "ReturnTrueOnSuccessfulLemmatization")]
    public void TryLemmatize_ShouldReturnFalseOnEmptyString(string word, bool expected)
    {
        var isLemmatized = sut.TryLemmatize(word, out _);

        isLemmatized.Should().Be(expected);
    }

    [TestCaseSource(typeof(RussianLemmatizerTestCases), nameof(RussianLemmatizerTestCases.TestCaseDatas))]
    public void TryLemmatize_Should(string word, Lemma expected)
    {
        sut.TryLemmatize(word, out var lemma);

        lemma.Should().BeEquivalentTo(expected);
    }
}