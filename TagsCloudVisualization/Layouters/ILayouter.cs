using System.Drawing;
using ResultProject;

namespace TagsCloudVisualization.Layouters
{
    public interface ILayouter<TFigure>
    {
        Result<TFigure> PutNextRectangle(Size rectangleSize);
    }
}