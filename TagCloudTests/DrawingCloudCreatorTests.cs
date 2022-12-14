using System.Collections.Concurrent;
using System.Drawing;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;
using TagCloud.Abstractions;

namespace TagCloudTests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class DrawingCloudCreatorTests
{
    [SetUp]
    public void SetUp()
    {
        var testId = TestContext.CurrentContext.Test.ID;

        var fakeLayouter = A.Fake<ICloudLayouter>();
        A.CallTo(() => fakeLayouter.PutNextRectangle(A<Size>.Ignored))
            .ReturnsNextFromSequence(TagsAndRectangles.Select(t => t.Item2).ToArray());
        layouters[testId] = fakeLayouter;

        var fakeDrawer = A.Fake<ICloudDrawer>();
        var graphics = Graphics.FromImage(new Bitmap(10, 10));
        A.CallTo(() => fakeDrawer.Graphics).Returns(graphics);
        A.CallTo(() => fakeDrawer.FontFamily).Returns(new FontFamily("Arial"));
        A.CallTo(() => fakeDrawer.MaxFontSize).Returns(30);
        A.CallTo(() => fakeDrawer.MinFontSize).Returns(10);
        drawers[testId] = fakeDrawer;

        creators[testId] = new DrawingCloudCreator(fakeLayouter, fakeDrawer);
    }

    private readonly ConcurrentDictionary<string, ICloudCreator> creators = new();
    private readonly ConcurrentDictionary<string, ICloudLayouter> layouters = new();
    private readonly ConcurrentDictionary<string, ICloudDrawer> drawers = new();

    private static readonly (Tag, Rectangle)[] TagsAndRectangles =
    {
        (new Tag("word", 10), new Rectangle(new Point(1, 1), new Size(10, 10))),
        (new Tag("word", 7), new Rectangle(new Point(2, 2), new Size(10, 10))),
        (new Tag("word", 4), new Rectangle(new Point(3, 3), new Size(10, 10))),
        (new Tag("word", 1), new Rectangle(new Point(4, 4), new Size(10, 10)))
    };

    [Test]
    public void CreateTagCloud_ReturnEmptyCollection_OnEmptyTagsCollection()
    {
        var creator = creators[TestContext.CurrentContext.Test.ID];

        var result = creator.CreateTagCloud(Array.Empty<ITag>());

        result.Should().BeEmpty();
    }

    [Test]
    public void CreateTagCloud_ReturnDrawableTagWithMaxFontSize_OnCollectionWithOneTag()
    {
        var testId = TestContext.CurrentContext.Test.ID;
        var creator = creators[testId];
        var drawer = drawers[testId];

        var result = creator.CreateTagCloud(new[] { TagsAndRectangles[0].Item1 });

        result.Should().ContainSingle().Subject.FontSize.Should().Be(drawer.MaxFontSize);
    }

    [TestCase(0, TestName = "{m}ZeroWeight")]
    [TestCase(-1, TestName = "{m}NegativeWeight")]
    public void CreateTagCloud_ThrowArgumentException_OnTagWithInvalidWeight(int weight)
    {
        var creator = creators[TestContext.CurrentContext.Test.ID];
        var tags = new[] { new Tag("word", 1), new Tag("word", weight) };

        var act = () => creator.CreateTagCloud(tags);

        act.Should().Throw<ArgumentException>()
            .WithMessage($"Weight of Tag should be greater than 0, but {weight}");
    }

    [Test]
    public void CreateTagCloud_SizeOfMoreFrequentWords_IsLarger()
    {
        var testId = TestContext.CurrentContext.Test.ID;
        var layouter = layouters[testId];
        var creator = creators[testId];
        var sizes = new List<Size>();
        var putNextRectangleCall =
            A.CallTo(() => layouter.PutNextRectangle(A<Size>.Ignored));
        putNextRectangleCall.Invokes((Size s) => sizes.Add(s));
        var tags = new Tag[] { new("word", 10), new("word", 1) };

        var drawableTags = creator.CreateTagCloud(tags).ToArray();

        putNextRectangleCall.MustHaveHappened(2, Times.Exactly);
        sizes[0].Area().Should().BeGreaterThan(sizes[1].Area());
        drawableTags.Should().HaveCount(2);
        drawableTags[0].FontSize.Should().BeGreaterThan(drawableTags[1].FontSize);
    }

    [Test]
    public void CreateTagCloud_FontSizeOfAverageFrequentWord_InMiddleBetweenMinAndMaxFontSizes()
    {
        var testId = TestContext.CurrentContext.Test.ID;
        var creator = creators[testId];
        var tags = new Tag[] { new("word", 10), new("word", 6), new("word", 2) };

        var drawableTags = creator.CreateTagCloud(tags).ToArray();

        drawableTags.Should().HaveCount(3);
        drawableTags[1].FontSize.Should().Be((drawableTags[0].FontSize + drawableTags[2].FontSize) / 2);
    }

    [Test]
    public void CreateTagCloud_FontSizeOfAverageFrequentWord_InMiddleBetweenLeftAndRightWordFontSizes()
    {
        var testId = TestContext.CurrentContext.Test.ID;
        var creator = creators[testId];
        var tags = TagsAndRectangles.Select(tr => tr.Item1);

        var drawableTags = creator.CreateTagCloud(tags).ToArray();

        drawableTags.Should().HaveCount(4);
        drawableTags[1].FontSize.Should().Be((drawableTags[0].FontSize + drawableTags[2].FontSize) / 2);
        drawableTags[2].FontSize.Should().Be((drawableTags[1].FontSize + drawableTags[3].FontSize) / 2);
    }

    [Test]
    public void CreateTagCloud_FontSizesProportionalToSizeHeight()
    {
        var testId = TestContext.CurrentContext.Test.ID;
        var layouter = layouters[testId];
        var creator = creators[testId];
        var sizes = new List<Size>();
        var putNextRectangleCall =
            A.CallTo(() => layouter.PutNextRectangle(A<Size>.Ignored));
        putNextRectangleCall.Invokes((Size s) => sizes.Add(s));
        var tags = TagsAndRectangles.Select(tr => tr.Item1);

        var drawableTags = creator.CreateTagCloud(tags).ToArray();

        putNextRectangleCall.MustHaveHappened(4, Times.Exactly);
        drawableTags.Should().HaveCount(4);
        drawableTags.Zip(sizes, (t, s) => (t, s)).Should()
            .AllSatisfy(ts => (ts.t.FontSize * 1.625).Should().BeApproximately(ts.s.Height, 1));
    }

    [Test]
    public void CreateTagCloud_TagLocationEqualsToLayouterRectanglesLocation()
    {
        var testId = TestContext.CurrentContext.Test.ID;
        var layouter = layouters[testId];
        var creator = creators[testId];
        var tags = TagsAndRectangles.Select(tr => tr.Item1);
        var locations = TagsAndRectangles.Select(tr => tr.Item2);

        var drawableTags = creator.CreateTagCloud(tags).ToArray();

        var putNextRectangleCall =
            A.CallTo(() => layouter.PutNextRectangle(A<Size>.Ignored));
        putNextRectangleCall.MustHaveHappened(4, Times.Exactly);
        drawableTags.Zip(locations, (t, r) => (t.Location, r.Location)).Should()
            .AllSatisfy(ts => ts.Item1.Should().Be(ts.Item2));
    }
}