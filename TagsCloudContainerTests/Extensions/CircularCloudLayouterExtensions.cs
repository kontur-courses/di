using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.CircularCloudLayouters;

namespace TagsCloudContainerTests.Extensions
{
    public static class CircularCloudLayouterExtensions
    {
        public static List<Rectangle> PutNextRectangles(this ICircularCloudLayouter circularCloudLayouter,
            params Size[] sizes)
            => sizes.Select(circularCloudLayouter.PutNextRectangle).ToList();


        public static List<Rectangle> PutNextRectangles(this ICircularCloudLayouter circularCloudLayouter,
            IEnumerable<Size> sizes)
            => sizes.Select(circularCloudLayouter.PutNextRectangle).ToList();
    }
}