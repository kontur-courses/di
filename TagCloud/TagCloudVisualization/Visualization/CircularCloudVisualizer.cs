using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.TagCloudVisualization.Visualization
{
    public class CircularCloudVisualizer : Visualization
    {
        public CircularCloudVisualizer(List<Rectangle> rectangles)
        {
            Rectangles = rectangles;
        }

        protected override void DrawElements()
        {
            foreach (var rectangle in Rectangles)
            {
                var shiftedRectangle = ShiftRectangleToCenter(rectangle);
                Graphics.DrawRectangle(new Pen(Color.Black, 5), shiftedRectangle);
            }
        }

    }
}
