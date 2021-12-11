using System;
using System.Drawing;
using NUnit.Framework;
using FluentAssertions;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using FluentAssertions.Extensions;
using TagsCloudVisualization;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Visualizators;

namespace TagCloudContainerTests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        private CircularCloudLayouter layouter;
        private Cloud cloud;
        private readonly PointF layouterCenter = new PointF(0, 0);
        private readonly ITag defaulTag = new Tag("-");

        [SetUp]
        public void InitialiseLayouter()
        {
            InitialiseCustomLayouter(layouterCenter);
        }

        private void InitialiseCustomLayouter(PointF center)
        {
            cloud = new Cloud(center);
            var spiral = new Spiral(center);
            layouter = new CircularCloudLayouter(cloud, spiral);
        }

        [Test]
        public void LayouterCloudShould_BeEmpty_AfterCreation()
        {
            cloud.Elements.Should()
                .BeEmpty();
        }

        [TestCase(5, 0)]
        [TestCase(0, 5)]
        [TestCase(0, 0)]
        [TestCase(-5, 5)]
        [TestCase(5, -5)]
        [TestCase(-5, -5)]
        public void LayouterShould_ThrowException_WhenPutRectangle_WithNotPositiveSize
            (int width, int height)
        {
            var size = new Size(width, height);
            Assert.Throws<ArgumentException>(() =>
                layouter.PutNextTag(size, defaulTag));
        }

        [Test]
        public void LayouterShould_AddOneRectangleToList()
        {
            layouter.PutNextTag(new Size(5, 5), defaulTag);
            cloud.Elements.Should()
                .HaveCount(1);
        }

        [Test]
        public void LayouterShould_AddFirstRectangleAtCenter()
        {
            layouter.PutNextTag(new Size(5, 5), defaulTag);
            cloud.Elements.First()
                .Layout.GetCenter()
                .Should()
                .Be(layouterCenter);
        }

        [TestCase(5)]
        [TestCase(20)]
        public void LayouterShould_AddSeveralRectanglesToList(int count)
        {
            var sizes = GetRandomSizes(count, 1, 10);
            PutSeveralRectangles(sizes);
            var rectSizes = cloud.Elements
                .Select(t => t.Layout.Size)
                .ToList();
            rectSizes.Should().HaveCount(sizes.Count);
            for (var i = 0; i < rectSizes.Count; i++)
                rectSizes[i].Should().Be((SizeF)sizes[i]);
        }

        [TestCase(5)]
        [TestCase(20)]
        public void LayouterShould_AddSeveralRectangles_WithoutIntersections(int count)
        {
            var sizes = GetRandomSizes(count, 2, 20);
            PutSeveralRectangles(sizes);
            var rectangles = cloud.Elements
                .Select(t => t.Layout)
                .ToList();
            for (var i = 0; i < rectangles.Count; i++)
                for (var j = i + 1; j < rectangles.Count; j++)
                    rectangles[i].IntersectsWith(rectangles[j])
                        .Should()
                        .BeFalse();
        }

        [Test]
        public void LayouterShould_PutManyRectangles_FastEnough()
        {
            var sizes = GetRandomSizes(100, 10, 20);
            Action action = () => PutSeveralRectangles(sizes);
            GC.Collect();
            action.ExecutionTime().Should().BeLessThan(1.Seconds());
        }

        [Test]
        public void LayouterShould_PlaceRectangles_CompactEnough()
        {
            var sizes = GetRandomSizes(100, 10, 20);
            PutSeveralRectangles(sizes);
            var boundingRect = RectangleFExtensions.GetRectangleByCenter
                (new Size(1000, 1000), layouterCenter);
            cloud.Elements.Where(t => !boundingRect.Contains(t.Layout))
                .Should()
                .BeEmpty();
        }

        [TestCase(5, 20)]
        [TestCase(20, 50)]
        [TestCase(10, 200)]
        public void LayouterShould_PlaceRectangles_CloseToCircularForm(int min, int max)
        {
            var count = 400;
            var sizes = GetRandomSizes(count, min, max);
            PutSeveralRectangles(sizes);
            var vectorLayouterCenter = layouterCenter.ToVector();
            var radius = count * (max + min) / 2 * 0.05;
            cloud.Elements.ToList()
                .ForEach(r => 
                    vectorLayouterCenter.GetDistanceTo(r.Layout.GetCenter())
                        .Should().BeLessThan(radius));
        }

        [TearDown]
        public void SaveLayout()
        {
            if (TestContext.CurrentContext.Result.FailCount == 0
            || cloud.Elements.Count == 0)
                return;

            var name = TestContext.CurrentContext.Test.Name;
            var path = Path.GetFullPath($"..\\..\\..\\FailTestImages\\{name}.jpg");
            var message = $"Test {name} down!\n" +
                $"Tag cloud visualization saved to file {path}";
            Visualize(path);
            TestContext.WriteLine(message);
        }

        private static List<Size> GetRandomSizes(int count, int min, int max, int seed = 0)
        {
            var rnd = new Random(seed);
            var result = new List<Size>();
            for (var i = 0; i < count; i++)
                result.Add(new Size(rnd.Next(min, max), rnd.Next(min, max)));
            return result;
        }

        private void PutSeveralRectangles(List<Size> sizes)
        {
            foreach (var sz in sizes)
                layouter.PutNextTag(sz, new Tag("-"));
        }

        private void Visualize(string filename)
        {
            var settings = new TagsVisualizatorSettings(filename);
            SetPalettes();
            new TagsVisualizator().Visualize(settings, cloud);
        }

        private void SetPalettes()
        {
            foreach (var tag in cloud.Elements)
            {
                tag.Palette = new Palette(Color.White, Color.Black);
            }
        }
    }
}