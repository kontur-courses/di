using FluentAssertions;
using NUnit.Framework;
using TagCloud;

namespace TagCloudTests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class CountWordsTaggerTests
{
    [Test]
    public void ToTags_ReturnEmptyCollection_OnEmptyWordsCollection()
    {
        var tagger = new CountWordsTagger();
        var words = Array.Empty<string>();

        var result = tagger.ToTags(words);

        result.Should().BeEmpty();
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(1000)]
    public void ToTags_CorrectCountWords(int count)
    {
        var tagger = new CountWordsTagger();
        const string word = "word";
        var words = Enumerable.Range(0, count).Select(_ => word);

        var result = tagger.ToTags(words);

        result.Should().ContainSingle().Subject.Should().BeEquivalentTo(new Tag(word, count));
    }

    [Test]
    public void ToTags_OrderingTagsByCount()
    {
        var tagger = new CountWordsTagger();
        var random = new Random();
        var words = Enumerable.Range(65, 26)
            .SelectMany(n => Enumerable.Range(1, random.Next(1, 50)).Select(_ => ((char)n).ToString()));

        var result = tagger.ToTags(words);

        result.Should().BeInDescendingOrder(t => t.Weight);
    }
}