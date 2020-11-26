using System.Drawing;
using FluentAssertions;
using TagsCloudVisualisation.Layouter;

namespace TagsCloudVisualisationTests
{
    public static class LayouterTestExtensions
    {
        public static Rectangle PutAndTest(this ICircularCloudLayouter layouter,
            Size size, Point expectedPosition)
        {
            var expected = new Rectangle(expectedPosition, size);
            layouter.PutNextRectangle(size)
                .Should()
                .Be(expected);
            return expected;
        }

        public static ICircularCloudLayouter Put(this ICircularCloudLayouter layouter, Size size, out Rectangle result)
        {
            result = layouter.PutNextRectangle(size);
            return layouter;
        }
    }
}