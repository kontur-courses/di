using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public static class TagCloudCreator
    {
        public static TagCloud Create(IEnumerable<Size> rectangleSizes, Point inCenter)
        {
            var circularCloudLayouter = new CircularCloudLayouter(inCenter);

            foreach (var rectangleSize in rectangleSizes)
                circularCloudLayouter.PutNextRectangle(rectangleSize);

            return circularCloudLayouter.GetTagCloud();
        }
    }
}
