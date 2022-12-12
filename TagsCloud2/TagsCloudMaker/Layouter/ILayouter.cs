using System.Drawing;

namespace TagsCloud2.TagsCloudMaker.Layouter;

public interface ILayouter
{
    public Rectangle PutNextRectangle(Size size);
}