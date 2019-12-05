using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer.RectanglesShifter
{
    public class CenterRectanglesShifter : IRectanglesShifter
    {
        private readonly Size imageSize;

        public CenterRectanglesShifter(Size imageSize)
        {
            this.imageSize = imageSize;
        }

        public IList<Rectangle> ShiftRectangles(IList<Rectangle> rectangles, Point oldCenter)
        {
            var center = VisualizerСalculations.GetCenter(imageSize);
            var offset = new Size(center) - new Size(oldCenter);
            return VisualizerСalculations.GetRectanglesWithOptimalLocation(rectangles, offset);
        }
    }
}
