using FluentAssertions;
using NUnit.Framework;
using TagCloudContainer.Infrastructure.WordWeigher;

namespace TagCloudContainerTests;

internal class RussianLemmatizerTests
{
    private readonly RussianLemmatizer sut = new();

    [TestCase(null, false, TestName = "ReturnFalseOnNullString")]
    [TestCase("", false, TestName = "ReturnTrueOnEmptyString")]
    [TestCase("под", false, TestName = "ReturnFalseOnAuxiliaryPartOfSpeech")]
    [TestCase("коровы", true, TestName = "ReturnTrueOnSuccessfulLemmatization")]
    public void TryLemmatize_ShouldReturnFalseOnEmptyString(string word, bool expected)
    {
        var isLemmatized = sut.TryLemmatize(word, out _);

        isLemmatized.Should().Be(expected);
    }

    [TestCase(null, null, TestName = "OutNullOnNullString")]
    [TestCase("", null, TestName = "OutNullOnEmptyString")]
    [TestCase("под", null, TestName = "OutNullOnAuxiliaryPartOfSpeech")]
    [TestCase("коровы", "корова", TestName = "OutCorrectLemmaOnRussianWord")]
    public void TryLemmatize_Should(string word, string expected)
    {
        sut.TryLemmatize(word, out var lemma);

        lemma.Should().Be(expected);
    }
}