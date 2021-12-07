using System.Drawing;

namespace TagsCloudVisualization.Layouters
{
    public interface ILayouter<out TFigure>
    {
        TFigure PutNextRectangle(Size rectangleSize);
    }
}