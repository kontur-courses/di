using FluentAssertions;
using TagsCloudPainter.Settings.Tag;
using TagsCloudPainter.Tags;

namespace TagsCloudPainterTests;

[TestFixture]
public class TagsBuilderTests
{
    [SetUp]
    public void Setup()
    {
        var tagSettings = new TagSettings { TagFontSize = 32 };
        tagsBuilder = new TagsBuilder(tagSettings);
    }

    private TagsBuilder tagsBuilder;

    [Test]
    public void GetTags_ShouldReturnTagsWithGivenWords()
    {
        var words = new List<string> { "tag" };
        var tags = tagsBuilder.GetTags(words);

        tags[0].Value.Should().Be("tag");
    }

    [Test]
    public void GetTags_ShouldReturnTagsWithDifferentValues()
    {
        var words = new List<string> { "tag", "tag" };
        var tags = tagsBuilder.GetTags(words);

        tags.Count.Should().Be(1);
    }

    [Test]
    public void GetTags_ShouldReturnTagsWithCorrectCount()
    {
        var words = new List<string> { "tag", "tag" };
        var tags = tagsBuilder.GetTags(words);

        tags[0].Count.Should().Be(2);
    }
}