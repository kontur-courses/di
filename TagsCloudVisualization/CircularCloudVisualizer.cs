using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudVisualizer : Visualization
    {
        public CircularCloudVisualizer(List<Rectangle> rectangles)
        {
            this.rectangles = rectangles;
        }

        public override void DrawElements()
        {
            foreach (var rectangle in rectangles)
            {
                var shiftedRectangle = ShiftRectangleToCenter(rectangle);
                graphics.DrawRectangle(new Pen(Color.Black, 5), shiftedRectangle);
            }
        }

    }
}
