using System.Drawing;
using FluentAssertions;
using TagsCloudPainter.CloudLayouter;
using TagsCloudPainter.FormPointer;
using TagsCloudPainter.Settings;
using TagsCloudPainter.Tags;
using TagsCloudPainter.Utils;

namespace TagsCloudPainterTests;

[TestFixture]
public class TagsCloudLayouterTests
{
    [SetUp]
    public void Setup()
    {
        var cloudSettings = new Lazy<CloudSettings> (new CloudSettings { CloudCenter = new Point(0, 0) });
        tagSettings = new TagSettings { TagFontSize = 32 };
        var pointerSettings = new SpiralPointerSettings { AngleConst = 1, RadiusConst = 0.5, Step = 0.1 };
        var formPointer = new ArchimedeanSpiralPointer(cloudSettings.Value, pointerSettings);
        tagsCloudLayouter = new TagsCloudLayouter(cloudSettings, formPointer, tagSettings);
    }

    private TagsCloudLayouter tagsCloudLayouter;
    private TagSettings tagSettings;

    private static IEnumerable<TestCaseData> PutNextTagArgumentException => new[]
    {
        new TestCaseData(new Tag("", 10, 1)).SetName("WhenGivenTagWithEmptyValue"),
        new TestCaseData(new Tag("das", 0, 1)).SetName("WhenGivenTagWithFontSizeLessThanOne")
    };

    [TestCaseSource(nameof(PutNextTagArgumentException))]
    public void PutNextRectangle_ShouldThrowArgumentException(Tag tag)
    {
        Assert.Throws<ArgumentException>(() => tagsCloudLayouter.PutNextTag(tag));
    }

    [Test]
    public void PutNextTag_ShouldReturnRectangleOfTheTagValueSize()
    {
        var tag = new Tag("ads", 10, 5);
        var tagRectangle = Utils.GetStringSize(tag.Value, tagSettings.TagFontName, tag.FontSize);

        var resultRectangle = tagsCloudLayouter.PutNextTag(tag);

        resultRectangle.Size.Should().Be(tagRectangle);
    }

    [Test]
    public void PutNextTag_ShouldReturnRectangleThatDoesNotIntersectWithAlreadyPutOnes()
    {
        var firstTag = new Tag("ads", 10, 5);
        var secondTag = new Tag("ads", 10, 5);
        var firstPutRectangle = tagsCloudLayouter.PutNextTag(firstTag);
        var secondPutRectangle = tagsCloudLayouter.PutNextTag(secondTag);

        var doesRectanglesIntersect = firstPutRectangle.IntersectsWith(secondPutRectangle);

        doesRectanglesIntersect.Should().BeFalse();
    }

    [Test]
    public void PutNextRectangle_ShouldPutRectangleWithCenterInTheCloudCenter()
    {
        var center = tagsCloudLayouter.GetCloud().Center;
        var tag = new Tag("ads", 10, 5);
        var firstRectangle = tagsCloudLayouter.PutNextTag(tag);
        var firstRectangleCenter = Utils.GetRectangleCenter(firstRectangle);

        firstRectangleCenter.Should().Be(center);
    }

    [Test]
    public void PutTags_ThrowsArgumentNullException_WhenGivenEmptyDictionary()
    {
        Assert.Throws<ArgumentException>(() => tagsCloudLayouter.PutTags([]));
    }

    [Test]
    public void GetCloud_ReturnsAsManyTagsAsWasPut()
    {
        tagsCloudLayouter.PutNextTag(new Tag("ads", 10, 5));
        tagsCloudLayouter.PutNextTag(new Tag("ads", 10, 5));
        var rectanglesAmount = tagsCloudLayouter.GetCloud().Tags.Count;

        rectanglesAmount.Should().Be(2);
    }
}