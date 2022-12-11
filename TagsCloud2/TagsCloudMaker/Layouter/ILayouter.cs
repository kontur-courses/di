using System.Drawing;

namespace TagsCloud2;

public interface ILayouter
{
    public Rectangle PutNextRectangle(Size size);
}