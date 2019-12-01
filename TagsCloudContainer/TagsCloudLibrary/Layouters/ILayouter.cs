using System.Drawing;

namespace TagsCloudLibrary.Layouters
{
    public interface ILayouter
    {
        string Name { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
