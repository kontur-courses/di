using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Infrastructure.Weigher;

namespace TagCloudTests.Infrastructure.WordProcessing;

internal class WordWeigherTests
{
    private readonly WordWeigher sut = new();

    [TestCaseSource(typeof(WordWeigherTestCases), nameof(WordWeigherTestCases.TestCaseDatas))]
    public void GetWeightedWords_Should(IEnumerable<string> lines, Dictionary<string, int> expected)
    {
        var actual = sut.GetWeightedWords(lines);

        actual.Should().BeEquivalentTo(expected);
    }
}