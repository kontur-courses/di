using System.Drawing;

namespace TagsCloudVisualisation.Layouting
{
    public interface ITagCloudLayouter
    {
        Point CloudCenter { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
        void Reset();
    }
}