using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2
{
    public interface ICloudLayouter
    {
        public Rectangle PutNewRectangle(Size rectangleSize);

        public IEnumerable<Rectangle> GetRectangles();

        public void SetSettings(ICloudSettings settings);
    }
}
