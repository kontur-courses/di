using FluentAssertions;
using NUnit.Framework;
using TagCloud;

namespace TagCloudTests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class MorphWordsProcessorTests
{
    private readonly MorphWordsProcessor processor = new(new[]
    {
        "мест", "предл", "союз", "част", "межд", "неизв"
    });

    [Test]
    public void Process_ReturnEmptyCollection_OnEmptyWordsCollection()
    {
        var words = Array.Empty<string>();

        var result = processor.Process(words);

        result.Should().BeEmpty();
    }

    [Test]
    public void Process_ShouldLemmatize_RussianWords()
    {
        var words = new[] { "предметом", "красивого", "делает", "десятью" };
        var expected = new[] { "предмет", "красивый", "делать", "десять" };

        var result = processor.Process(words);

        result.Should().Equal(expected);
    }

    [Test]
    public void Process_ShouldFilterBored_RussianWords()
    {
        var words = new[] { "мы", "у", "и", "по", "ух" };

        var result = processor.Process(words);

        result.Should().BeEmpty();
    }

    [Test]
    public void Process_ShouldSkip_EnglishWords()
    {
        var words = new[] { "me", "word", "and", "work" };

        var result = processor.Process(words);

        result.Should().BeEmpty();
    }
}