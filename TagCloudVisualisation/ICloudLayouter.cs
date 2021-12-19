using System.Collections.Generic;
using System.Drawing;

namespace TagCloudVisualisation
{
    public interface ICloudLayouter
    {
        public Rectangle PutNewRectangle(Size rectangleSize);

        public IEnumerable<Rectangle> GetRectangles();

        public void SetSettings(ICloudSettings settings);
    }
}
