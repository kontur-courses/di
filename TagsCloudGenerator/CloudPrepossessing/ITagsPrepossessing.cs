using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator.CloudPrepossessing
{
    public interface ITagsPrepossessing
    {
        Point Center { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
        IReadOnlyList<Rectangle> GetRectangles();
    }
}