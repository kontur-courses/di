using System.Drawing;
using ResultProject;

namespace TagsCloudVisualization.Layouters
{
    internal interface ILayouter<TFigure>
    {
        Result<TFigure> PutNextRectangle(Size rectangleSize);
    }
}