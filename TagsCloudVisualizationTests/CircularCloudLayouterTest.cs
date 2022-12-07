using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class CircularCloudLayouter_Test
{
    private CircularCloudLayouter _layouter;

    [SetUp]
    public void Setup()
    {
        _layouter = new CircularCloudLayouter(new Point(0, 0), 0.01);
    }

    [Test]
    public void Constructor_NotPositiveSpiralStep_Throw()
    {
        new Action(() => { new CircularCloudLayouter(new Point(0, 0), -1); })
            .Should()
            .Throw<ArgumentException>();
    }

    [TestCase(0, 0, TestName = "Zero size")]
    [TestCase(5, 0, TestName = "Zero height")]
    [TestCase(0, 5, TestName = "Zero width")]
    [TestCase(-1, 5, TestName = "Negative width")]
    [TestCase(5, -1, TestName = "Negative height")]
    public void PutNextRectangle_NotPositiveOrSingleSideSize_EmptyRectangle(int width, int height)
    {
        var rectangle = _layouter.PutNextRectangle(new Size(0, 0));
        rectangle.IsEmpty.Should().BeTrue();
    }

    [Test]
    public void PutNextRectangle_ZeroSize_EmptyRectangle()
    {
        var rectangle = _layouter.PutNextRectangle(new Size(0, 0));
        rectangle.IsEmpty.Should().BeTrue();
    }

    [Test]
    public void PutNextRectangle_RightSize_RectangleSizeEqual()
    {
        var random = new Random();
        var width = random.Next(1, 100);
        var height = random.Next(1, 100);

        var rectangle = _layouter.PutNextRectangle(new Size(width, height));

        using (new AssertionScope())
        {
            rectangle.Width.Should().Be(width);
            rectangle.Height.Should().Be(height);
        }
    }


    [Test]
    public void PutNextRectangle_FirstRectangle_ShiftToCenterOfRectangle()
    {
        var x = 4;
        var y = 6;
        var width = 9;
        var height = 12;

        _layouter = new CircularCloudLayouter(new Point(x, y));
        var rectangle = _layouter.PutNextRectangle(new Size(width, height));

        var expectedX = x - width / 2;
        var expectedY = y - height / 2;

        using (new AssertionScope())
        {
            rectangle.X.Should().Be(expectedX);
            rectangle.Y.Should().Be(expectedY);
        }
    }


    [TestCase(4, 4)]
    [TestCase(7, 3)]
    [TestCase(5, 7)]
    public void PutNextRectangle_TwoRectangles_NotIntersect(int width, int height)
    {
        var firstRectangle = _layouter.PutNextRectangle(new Size(width, height));
        var secondRectangle = _layouter.PutNextRectangle(new Size(width, height));

        firstRectangle
            .IntersectsWith(secondRectangle)
            .Should()
            .BeFalse();
    }

    [Test]
    public void PutNextRectangle_ManyRectangles_AllNotIntersect()
    {
        var random = new Random();
        var rectangles = new List<Rectangle>();

        for (int i = 0; i < 100; i++)
        {
            var newX = random.Next(40, 100);
            var newY = random.Next(40, 100);
            rectangles.Add(_layouter.PutNextRectangle(new Size(newX, newY)));
        }

        foreach (var rectangle in rectangles)
        {
            rectangles.Where(rect => rect != rectangle)
                .Should().AllSatisfy(x => rectangle.IntersectsWith(x).Should().BeFalse());
        }
    }


    [Test]
    public void PutNextRectangle_ManyRectangles_AllPutInLayout()
    {
        var random = new Random();
        var rectangles = new List<Rectangle>();

        for (int i = 0; i < 100; i++)
        {
            var newX = random.Next(40, 100);
            var newY = random.Next(40, 100);
            rectangles.Add(_layouter.PutNextRectangle(new Size(newX, newY)));
        }

        _layouter.Rectangles.Should().BeEquivalentTo(rectangles);
    }

    [Test, Description("Тест специально провальный для того, чтобы показать работу сохранения облака при падении")]
    public void PutNextRectangle_ManyRectangles_FailTest()
    {
        var random = new Random();
        var rectangles = new List<Rectangle>();

        for (int i = 0; i < 100; i++)
        {
            var newY = random.Next(40, 70);
            var newX = random.Next(newY, 100);
            rectangles.Add(_layouter.PutNextRectangle(new Size(newX, newY)));
        }

        _layouter.Rectangles.Should().NotBeEquivalentTo(rectangles);
    }


    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome != ResultState.Failure)
            return;

        ITagsCloudVisualization tagsCloudVisualization = new BitmapTagsCloudVisualization();

        var filename = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yy.MM.dd_HH.mm.ss}.bmp";
        var projectDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ("FailedTests"));
        if (!Directory.Exists(projectDirectory))
            Directory.CreateDirectory(projectDirectory);

        var fullPath = Path.Join(projectDirectory, filename);
        tagsCloudVisualization.SaveTagsCloud(_layouter, fullPath);

        Console.WriteLine($"Tag cloud visualization saved to file {fullPath}");
    }
}