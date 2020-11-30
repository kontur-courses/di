using System.Drawing;
using FluentAssertions;
using TagsCloudVisualisation.Layouting;

namespace TagsCloudVisualisationTests
{
    public static class LayouterTestExtensions
    {
        public static Rectangle PutAndTest(this ITagCloudLayouter layouter,
            Size size, Point expectedPosition)
        {
            var expected = new Rectangle(expectedPosition, size);
            layouter.PutNextRectangle(size)
                .Should()
                .Be(expected);
            return expected;
        }

        public static ITagCloudLayouter Put(this ITagCloudLayouter layouter, Size size, out Rectangle result)
        {
            result = layouter.PutNextRectangle(size);
            return layouter;
        }
    }
}