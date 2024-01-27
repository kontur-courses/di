using NUnit.Framework;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.TextHandlers;
using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization.Tags;
using TagsCloudVisualization.PointCreators;

namespace TagsCloudVisualizationTests.TagLayouterTests;

[TestFixture]
public class TagCloudLayouterTests
{
    private ICloudLayouter cloudLayouter;
    private TagsLayouterSettings settings;

    [SetUp]
    public void SetUp()
    {
        var imageSettings = new ImageSettings(200, 200);
        var spiralSettings = new SpiralSettings(0.05, 0.1);
        var spiral = new Spiral(imageSettings, spiralSettings);
        cloudLayouter = new CircularCloudLayouter(spiral);
        settings = new TagsLayouterSettings("Arial", 10, 20);
    }

    [Test]
    public void GetTags_OneTag_ShouldReturnCorrectTag()
    {
        var textHandler = new TextHandler("a", "");
        var layouter = new TagsLayouter(cloudLayouter, textHandler, settings);

        var result = layouter.GetTags().ToList();
        result[0].Size.Should().Be(20);
        result[0].Rectangle.Size.Should().Be(new Size(25, 34));
        result[0].Content.Should().Be("a");
    }

    [Test]
    public void GetTags_TwoTags_ShouldReturnCorrectTags()
    {
        var textHandler = new TextHandler("a b", "");
        var layouter = new TagsLayouter(cloudLayouter, textHandler, settings);

        var result = layouter.GetTags().OrderByDescending(t => t.Size).ToList();
        var excepted = new List<Tag> { new Tag("a", 20, new Rectangle(new Point(0, 0), new Size(25, 34)), settings.FontFamily),
                                       new Tag("b", 20, new Rectangle(new Point(0, 0), new Size(25, 34)), settings.FontFamily)};
        for (var i = 0; i < excepted.Count; i++)
        {
            result[i].Size.Should().Be(excepted[i].Size);
            result[i].Rectangle.Size.Should().Be(excepted[i].Rectangle.Size);
        }
    }

    [Test]
    public void GetTags_ThreeTagsWithDifferentCount_ShouldReturnCorrectTags()
    {
        var textHandler = new TextHandler("a a a b b c", "");
        var layouter = new TagsLayouter(cloudLayouter, textHandler, settings);

        var result = layouter.GetTags().OrderByDescending(t => t.Size).ToList();
        var excepted = new List<Tag> { new Tag("a", 20, new Rectangle(new Point(0, 0), new Size(25, 34)), settings.FontFamily),
                                       new Tag("b", 15, new Rectangle(new Point(0, 0), new Size(19, 25)), settings.FontFamily),
                                       new Tag("c", 10, new Rectangle(new Point(0, 0), new Size(12, 17)), settings.FontFamily) };
        for (var i = 0; i < excepted.Count; i++)
        {
            result[i].Size.Should().Be(excepted[i].Size);
            result[i].Rectangle.Size.Should().Be(excepted[i].Rectangle.Size);
            result[i].Content.Should().Be(excepted[i].Content);
        }
    }
}