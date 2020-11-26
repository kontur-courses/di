using System.Drawing;

namespace TagsCloudVisualisation.Layouter
{
    public interface ICircularCloudLayouter
    {
        public Point CloudCenter { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}