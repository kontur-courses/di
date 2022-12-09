using System.Drawing;

namespace TagsCloud.Core.Layouters;

public interface ICloudLayouter
{
    public Rectangle PutNextRectangle(Size rectangleSize);
}