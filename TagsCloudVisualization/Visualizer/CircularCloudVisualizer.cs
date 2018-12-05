using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudVisualizer : IVisualizer<Rectangle>
    {
        private readonly Color backgroundColor;
        private readonly Brush rectangleColor;
        private readonly Size pictureSize;
        
        public CircularCloudVisualizer(Palette palette, Size size)
        {
            this.backgroundColor = palette.BackgroundColor;
            this.rectangleColor = palette.SecondaryColor;
            this.pictureSize = size;
        }

        public Bitmap Draw(IList<Rectangle> rectangles)
        {
            var bmp = new Bitmap(pictureSize.Width, pictureSize.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(backgroundColor);
                if (!rectangles.Any()) return bmp;
                foreach (var rectangle in rectangles)
                    g.DrawRectangle(new Pen(rectangleColor, 8), rectangle.ShiftRectangleToBitMapCenter(bmp));
            }
            return bmp;
        }
    }
}
