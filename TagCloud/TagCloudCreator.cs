using System.Collections.Generic;
using System.Drawing;
using TagCloud.PointGenerators;

namespace TagCloud
{
    public static class TagCloudCreator
    {
        public static TagCloud Create(IEnumerable<Size> rectangleSizes, Point inCenter)
        {
            var circularCloudLayouter = new CircularCloudLayouter(new SpiralPointGenerator(inCenter));

            foreach (var rectangleSize in rectangleSizes)
                circularCloudLayouter.PutNextRectangle(rectangleSize);

            return circularCloudLayouter.GetTagCloud();
        }
    }
}
