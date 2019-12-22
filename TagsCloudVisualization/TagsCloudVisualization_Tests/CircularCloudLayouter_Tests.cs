using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using com.sun.xml.@internal.bind.v2.schemagen.xmlschema;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization;
using TagsCloudVisualization.Providers.Layouter;
using FakeItEasy;
using TagsCloudVisualization.Providers.Layouter.Interfaces;
using TagsCloudVisualization.Providers.Layouter.Spirals;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.SourcesTypes;

namespace TagsCloudVisualization_Tests
{
    [TestFixture]
    public class CircularCloudLayouter_Tests
    {
        private static Point center;
        private static LayouterSettings settings;

        private static DrawableCloudLayouter cloudLayouter;

        [SetUp]
        public void Setup()
        {
            var factory = new SpiralFactory();
            center = new Point(0, 0);
            settings = new LayouterSettings(center, 1, SpiralType.Ferma);
            cloudLayouter = new DrawableCloudLayouter(factory);
        }

        [TestCase(10, 10)]
        [TestCase(50, 50)]
        public void LayouterRectangles_NotIntersect_WhileAdding(int widthMax, int heightMax)
        {
            var sizes = GetSizableList(50, 50);
            
            cloudLayouter.GetDrawableSource(sizes, settings);
            var rectangles = cloudLayouter.Rectangles;


            var isAnyIntersects = rectangles.Any(r1 => rectangles.Any(r2 => (r1 != r2 && r1.IntersectsWith(r2))));
            isAnyIntersects.Should().BeFalse();
        }

        [Test]
        public void LayouterRectangles_ContainsTheSameSizes_OnAddingRectanglesList()
        {
            var sizableSource= GetSizableList(50, 50);
            var sizes = sizableSource.Select(s => s.DrawSize);
            
            cloudLayouter.GetDrawableSource(sizableSource, settings);
            var rectanglesSizes = cloudLayouter.Rectangles.Select(rect => new Size(rect.Width, rect.Height));

            rectanglesSizes.Should().BeEquivalentTo(sizes);
        }

        [Test]
        public void PutNextRectangle_HasOptimalRectanglesLocations_OnManyRectangles()
        {
            const int accuracy = 40;
            var sizes = GetSizableList(50, 50);
            var drawableSource = cloudLayouter.GetDrawableSource(sizes, settings);
           var info= new CloudInfo(drawableSource.Value);
            var radius = Math.Max(center.LengthTo(info.RightUpperPointOfCloud),
                center.LengthTo(info.LeftDownPointOfCloud));
            var sumOfRectanglesSquares = cloudLayouter.Rectangles.Select(r => r.Width * r.Height).Sum();
            var squareOfCircle = Math.PI * radius * radius;
            var isOptimal = sumOfRectanglesSquares / squareOfCircle * 100 < accuracy;
            isOptimal.Should().BeTrue();
        }

        [TestCase(5, 5)]
        [TestCase(20, 20)]
        public void PutNextRectangle_ReturnSameSizeRectangleOnCenter_OnFirstPutting(int width, int height)
        {
            var sizable = new SizableWord("name",new Size(width, height));
            var rectangle = new Rectangle(center, sizable.DrawSize);

            var rectangleFromLayouter = cloudLayouter
                .GetDrawableSource(new List<SizableWord>() {sizable}, settings).Value
                .FirstOrDefault();

            rectangleFromLayouter?.Place.Should().Be(rectangle);
        }

        [TestCaseSource(nameof(TestCases))]
        public void PutNextRectangle_NotSuccess_OnIncorrectSize(Size size)
        {
           cloudLayouter.PutNextRectangle(size).IsSuccess.Should().BeFalse();
        }

        private static IEnumerable<Size> TestCases
        {
            get
            {
                yield return new Size(0, 10);
                yield return new Size(10, 0);
                yield return Size.Empty;
            }
        }

        private static List<SizableWord> GetSizableList(int widthMax, int heightMax)
        {
            var sizes = new List<SizableWord>();
            for (var width = 5; width < widthMax; width += 2)
            {
                for (var height = 5; height < heightMax; height += 2)
                {
                    sizes.Add(new SizableWord((width + height).ToString(), new Size(width, height)));
                }
            }

            return sizes;
        }
    }

    public static class PointExtension
    {
        public static double LengthTo(this Point first, Point second) =>
            Math.Sqrt((first.X - second.X) * (first.X - second.X) +
                      (first.Y - second.Y) * (first.Y - second.Y));
    }
}