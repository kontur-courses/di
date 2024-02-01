using System.Drawing;
using FakeItEasy;
using FluentAssertions;
using TagsCloudPainter.CloudLayouter;
using TagsCloudPainter.Extensions;
using TagsCloudPainter.FormPointer;
using TagsCloudPainter.Settings.Cloud;
using TagsCloudPainter.Settings.FormPointer;
using TagsCloudPainter.Settings.Tag;
using TagsCloudPainter.Sizer;
using TagsCloudPainter.Tags;

namespace TagsCloudPainterTests;

[TestFixture]
public class TagsCloudLayouterTests
{
    [SetUp]
    public void Setup()
    {
        var cloudSettings = new CloudSettings { CloudCenter = new Point(0, 0) };
        tagSettings = new TagSettings { TagFontSize = 32 };
        var pointerSettings = new SpiralPointerSettings { AngleConst = 1, RadiusConst = 0.5, Step = 0.1 };
        var formPointer = new ArchimedeanSpiralPointer(cloudSettings, pointerSettings);
        stringSizer = A.Fake<IStringSizer>();
        A.CallTo(() => stringSizer.GetStringSize(A<string>.Ignored, A<string>.Ignored, A<float>.Ignored))
            .Returns(new Size(10, 10));
        tagsCloudLayouter = new TagsCloudLayouter(cloudSettings, formPointer, tagSettings, stringSizer);
    }

    private TagsCloudLayouter tagsCloudLayouter;
    private ITagSettings tagSettings;
    private IStringSizer stringSizer;

    private static IEnumerable<TestCaseData> PutNextTagArgumentException => new[]
    {
        new TestCaseData(new Size(0, 10)).SetName("WidthNotPossitive"),
        new TestCaseData(new Size(10, 0)).SetName("HeightNotPossitive"),
        new TestCaseData(new Size(0, 0)).SetName("HeightAndWidthNotPossitive")
    };

    [TestCaseSource(nameof(PutNextTagArgumentException))]
    public void PutNextRectangle_ShouldThrowArgumentException_WhenGivenTagWith(Size size)
    {
        A.CallTo(() => stringSizer.GetStringSize(A<string>.Ignored, A<string>.Ignored, A<float>.Ignored))
            .Returns(size);
        Assert.Throws<ArgumentException>(() => tagsCloudLayouter.PutNextTag(new Tag("a", 2, 1)));
    }

    [Test]
    public void PutNextTag_ShouldReturnRectangleOfTheTagValueSize()
    {
        var tag = new Tag("ads", 10, 5);
        var tagSize = stringSizer.GetStringSize(tag.Value, tagSettings.TagFontName, tag.FontSize);

        var resultRectangle = tagsCloudLayouter.PutNextTag(tag);

        resultRectangle.Size.Should().Be(tagSize);
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
        var firstRectangleCenter = firstRectangle.GetCenter();

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