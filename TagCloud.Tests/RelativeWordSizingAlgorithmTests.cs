using FluentAssertions;
using NUnit.Framework;
using TagCloud.WordSizingAlgorithm;

namespace TagCloud.Tests;

[TestFixture]
[Parallelizable(ParallelScope.Children)]
public class RelativeWordSizingAlgorithmTests
{
    [TestCase(12, TestName = "{m}_With_BaseSize_12")]
    [TestCase(18, TestName = "{m}_With_BaseSize_18")]
    [TestCase(120, TestName = "{m}_With_BaseSize_120")]
    public void Should_ReturnBaseSize_OnSingleWord(int baseSize)
    {
        var algorithm = new RelativeWordSizingAlgorithm(baseSize);
        var tagMap = new TagMap();
        tagMap.AddWord("word");

        algorithm.GetWordSize("word", tagMap).Should().Be(baseSize);
    }
    
    [TestCase(12, TestName = "{m}_With_BaseSize_12")]
    [TestCase(18, TestName = "{m}_With_BaseSize_18")]
    [TestCase(120, TestName = "{m}_With_BaseSize_120")]
    public void Should_ReturnBaseSize_OnOneUniqueWord(int baseSize)
    {
        var algorithm = new RelativeWordSizingAlgorithm(baseSize);
        var tagMap = new TagMap();
        tagMap.AddWord("word");
        tagMap.AddWord("word");
        tagMap.AddWord("word");
        tagMap.AddWord("word");

        algorithm.GetWordSize("word", tagMap).Should().Be(baseSize);
    }
    
    [TestCase(12, TestName = "{m}_With_BaseSize_12")]
    [TestCase(18, TestName = "{m}_With_BaseSize_18")]
    [TestCase(120, TestName = "{m}_With_BaseSize_120")]
    public void Should_ReturnHalfBaseSize_OnTwoUniqueWordsWithEqualAmount(int baseSize)
    {
        var algorithm = new RelativeWordSizingAlgorithm(baseSize);
        var tagMap = new TagMap();
        tagMap.AddWord("word1");
        tagMap.AddWord("word2");

        algorithm.GetWordSize("word1", tagMap).Should().Be(baseSize / 2);
    }
    
    [TestCase(3, TestName = "{m}_For_1_Word_OutOf_3")]
    [TestCase(4, TestName = "{m}_For_1_Word_OutOf_4")]
    [TestCase(6, TestName = "{m}_For_1_Word_OutOf_6")]
    [TestCase(12, TestName = "{m}_For_1_Word_OutOf_12")]
    public void Should_ReturnRelativeSize(int totalWordCount)
    {
        const int baseSize = 12;
        var algorithm = new RelativeWordSizingAlgorithm(baseSize);
        var tagMap = new TagMap();
        for (var i = 0; i < totalWordCount - 1; i++)
        {
            tagMap.AddWord("word1");
        }
        tagMap.AddWord("word2");

        algorithm.GetWordSize("word2", tagMap).Should().Be(baseSize / totalWordCount);
    }
}