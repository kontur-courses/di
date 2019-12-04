using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class PictureInfo
    {
        public Point PictureCenter { get; }
        public Size PictureSize { get; }
        public IEnumerable<Size> RectangleSizes { get; }
        public string Name { get; }

        public PictureInfo(Point pictureCenter, Size pictureSize, IEnumerable<Size> rectangleSizes, string name)
        {
            PictureCenter = pictureCenter;
            PictureSize = pictureSize;
            RectangleSizes = rectangleSizes;
            Name = name;
        }
    }
}