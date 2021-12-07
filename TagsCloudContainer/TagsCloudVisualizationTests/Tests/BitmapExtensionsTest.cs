using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualizationTests.TestingLibrary;

namespace TagsCloudVisualizationTests.Tests
{
    public class BitmapExtensionsTest
    {
        [Test]
        public void ToEnumerable_ReturnBlackTransparentColors_WithEmptyBitmap()
        {
            new Bitmap(10, 10)
                .ToEnumerable()
                .Should().OnlyContain(color => color.ToArgb() == 0);
        }

        [Test]
        public void ToEnumerable_ReturnCorrectBitmapColors()
        {
            var bitmap = new Bitmap(2, 2);
            bitmap.SetPixel(1, 0, Color.Red);
            var expected = new List<int> {0, 0, Color.Red.ToArgb(), 0};

            var actual = bitmap.ToEnumerable().Select(color => color.ToArgb()).ToList();

            actual.Should().Equal(expected);
        }
    }
}