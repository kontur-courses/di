using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization;

namespace TagsCloudVisualization_Should
{
    public class DrawerShould
    {

        [Test]
        public void DrawImage_ThrowArgumentException_CenterWithNegativeXOrY()
        {
            var rectangles = new List<Rectangle> { new Rectangle(1, 1, 1, 1) };
            Action act = () => Drawer.DrawImage(rectangles, new Point(-1, -1));

            act.ShouldThrow<ArgumentException>().WithMessage("X or Y of center was negative");
        }

        [Test]
        public void DrawImage_ThrowArgumentException_SequenceOfElementsIsEmpty()
        {

            Action act = () => Drawer.DrawImage(new List<Rectangle>(), new Point());

            act.ShouldThrow<ArgumentException>().WithMessage("The sequence contains no elements");
        }

        [Test]
        public void DrawImage_CorrectImageSize_TenRectangles()
        {
            var rectangles = GetRectangles(10);
            var expectedSize = new Size(518, 518);

            var actualBitmap = Drawer.DrawImage(rectangles, new Point(500, 500));

            actualBitmap.Size.Should().Be(expectedSize);

        }


        [Test]
        public void DrawImage_CorrectRawFormat_HundredRectangles()
        {
            var rectangles = GetRectangles(100);
            var expectedFormat = ImageFormat.MemoryBmp;

            var actualBitmap = Drawer.DrawImage(rectangles, new Point(500, 500));

            actualBitmap.RawFormat.Should().Be(expectedFormat);

        }

        private List<Rectangle> GetRectangles(int count)
        {
            var rectangles = new List<Rectangle>();

            for (var i = 0; i < count; i++)
            {
                rectangles.Add(new Rectangle(i, i, i, i));
            }

            return rectangles;
        }
    }
}
