using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagCloudContainer.Infrastructure.WordWeigher;

namespace TagCloudContainerTests;

internal class WordWeigherTests
{
    private readonly WordWeigher sut = new(new RussianLemmatizer());

    [TestCaseSource(typeof(WordWeigherTestCases), nameof(WordWeigherTestCases.WeightedWordsTestCases))]
    public void GetWeightedWords_Should(IEnumerable<string> lines, IEnumerable<WeightedWord> expected)
    {
        var actual = sut.GetWeightedWords(lines);

        actual.Should().BeEquivalentTo(expected);
    }
}