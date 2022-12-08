using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloud;

namespace TagsCloudTests
{
    [TestFixture]
    public class CircularCloudShould
    {
        private static TestCaseData[] SourceLists
        {
            get
            {
                return new TestCaseData[]
                {
                    new TestCaseData(new Size[]
                    {
                        new Size(100, 100),
                        new Size(50, 100),
                        new Size(50, 50),
                        new Size(20, 30)
                    }),
                    new TestCaseData(new Size[]
                    {
                        new Size(100, 100),
                        new Size(200, 200),
                        new Size(100, 50),
                        new Size(5, 5)
                    }),
                    new TestCaseData(new Size[]
                    {
                        new Size(100, 100),
                        new Size(100, 100),
                        new Size(100, 100),
                        new Size(100, 100),
                        new Size(100, 100),
                        new Size(100, 100),
                        new Size(100, 100),
                    })
                };
            }
        }

        public CircularCloudLayouter layouter;

        [SetUp]
        public void SetUp()
        {
            var center = Point.Empty;

            layouter = new CircularCloudLayouter(center);

            var printSettings = new PrintSettings();
            printSettings.SetFont("Consolas", 64);
            printSettings.SetCentralPen(Color.White, 8);
            printSettings.SetSurroundPen(Color.FromArgb(249, 100, 0), 4);
            printSettings.SetBackgroudColor(Color.FromArgb(0, 34, 43));
        }

        [Test]
        public void CircularCloudLayouter_CreateNewLayouter_ShouldInitComposerAndSpiral()
        {
            layouter.Composer.Should().NotBeNull();
            layouter.Composer.Spiral.Should().NotBeNull();
        }

        [Test]
        public void PutNextRectangle_AddSingleRectInCenter_RectLocationInCenter()
        {
            var center = Point.Empty;
            var rectSize = new Size(100, 100);

            var rect = layouter.PutNextRectangle(rectSize);
            var rectCenter = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

            rectCenter.Should().Be(center);
        }

        [TestCaseSource(nameof(SourceLists))]
        public void PutNextRectangle_AddManyRect_RectsShouldBeAdded(Size[] addRects)
        {
            var expectedList = new List<Rectangle>();

            foreach (var size in addRects)
            {
                var rect = layouter.PutNextRectangle(size);
                expectedList.Add(new Rectangle(rect.Location, size));
            }

            layouter.Composer.Rectangles.Count.Should().Be(addRects.Length);
        }

        [TestCaseSource(nameof(SourceLists))]
        public void PutNextRectangle_AddManyRect_RectsShouldNotIntersect(Size[] addRects)
        {
            var expectedList = new List<Rectangle>();

            foreach (var size in addRects)
            {
                var rect = layouter.PutNextRectangle(size);
                expectedList.Add(new Rectangle(rect.Location, size));
            }

            IsRectanglesIntersect(layouter.Composer.Rectangles).Should().BeFalse();
        }

        [TestCase(0, 0)]
        [TestCase(100, 0)]
        [TestCase(0, 100)]
        [TestCase(0, -100)]
        public void PutNextRectangle_AddWrongSize_RectNotAdded(int width, int height)
        {
            var sizeToAdd = new Size(width, height);

            var nextRect = layouter.PutNextRectangle(sizeToAdd);

            layouter.Composer.Rectangles.Count.Should().Be(0);
            nextRect.Should().Be(Rectangle.Empty);
        }

        public bool IsRectanglesIntersect(List<Rectangle> rects)
        {
            for (int i = 0; i < rects.Count; i++)
            {
                for (int j = i + 1; j < rects.Count; j++)
                {
                    var a = rects[i];
                    var b = rects[j];

                    if (a.IntersectsWith(b))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}